using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace NIntegrate.Query.Command.SqlClient
{
    public class SqlQueryCommandBuilder : QueryCommandBuilder
    {
        #region Override Methods

        protected override string BuildPagingCacheableSql(string tableName, Criteria criteria)
        {
            var sb = new StringBuilder();
            sb.Append("WITH [__T] AS (SELECT ");
            if (criteria._isDistinct)
            {
                sb.Append("DISTINCT ");
            }
            if (criteria._maxResults > 0)
            {
                sb.Append("TOP ");
                sb.Append(criteria._maxResults + criteria._skipResults);
                sb.Append(" ");
            }

            AppendResultColumns(criteria, sb);

            sb.Append(", ROW_NUMBER() OVER (ORDER BY ");
            if (criteria._sortBys.Count > 0)
            {
                AppendSortBys(criteria, sb);
            }
            else
            {
                if (criteria._resultColumns.Count == 0)
                {
                    if (criteria._predefinedColumns.Count > 0)
                    {
                        sb.Append(criteria._predefinedColumns[0].ToExpressionCacheableSql());
                    }
                    else
                    {
                        throw new ArgumentException("No sort by columns or predefined result columns could be found!");
                    }
                }
                else
                {
                    sb.Append(criteria._resultColumns[0].ToExpressionCacheableSql());
                }
            }
            sb.Append(") AS [__Pos] ");

            AppendFrom(tableName, sb);
            sb.Append(" (NOLOCK)");

            if (criteria._conditions.Count > 0)
            {
                sb.Append(" ");
                sb.Append("WHERE ");
                AppendConditions(criteria, sb);
            }

            sb.Append(") SELECT ");
            if (criteria._resultColumns.Count == 0)
            {
                var separate = "";
                if (criteria._predefinedColumns.Count > 0)
                {
                    foreach (var column in criteria._predefinedColumns)
                    {
                        sb.Append(separate);
                        sb.Append("[__T].");
                        sb.Append(column.ColumnName.ToDatabaseObjectName());

                        separate = ", ";
                    }
                }
                else
                {
                    sb.Append("*");
                }
            }
            else
            {
                var separate = "";
                foreach (var column in criteria._resultColumns)
                {
                    sb.Append(separate);
                    sb.Append("[__T].");
                    sb.Append(column.ColumnName.ToDatabaseObjectName());

                    separate = ", ";
                }
            }
            sb.Append(" ");
            sb.Append("FROM [__T] WHERE [__T].[__Pos] > ");
            sb.Append(criteria._skipResults);
            if (criteria._maxResults > 0)
            {
                sb.Append(" ");
                sb.Append("AND [__T].[__Pos] <= ");
                sb.Append(criteria._maxResults + criteria._skipResults);
            }

            return sb.ToString();
        }

        protected override string BuildNoPagingCacheableSql(string tableName, Criteria criteria, bool isCountCommand)
        {
            var sb = new StringBuilder();
            sb.Append("SELECT ");
            if (!isCountCommand)
            {
                if (criteria._isDistinct)
                {
                    sb.Append("DISTINCT ");
                }
                if (criteria._maxResults > 0)
                {
                    sb.Append("TOP ");
                    sb.Append(criteria._maxResults);
                    sb.Append(" ");
                }

                AppendResultColumns(criteria, sb);
            }
            else
            {
                sb.Append("COUNT(1)");
            }
            sb.Append(" ");

            if (isCountCommand && criteria._isDistinct)
            {
                sb.Append("FROM (SELECT DISTINCT ");
                if (criteria._maxResults > 0)
                {
                    sb.Append("TOP ");
                    sb.Append(criteria._maxResults);
                    sb.Append(" ");
                }

                AppendResultColumns(criteria, sb);
                sb.Append(" ");
                AppendFrom(tableName, sb);
                sb.Append(" (NOLOCK)");
                if (criteria._conditions.Count > 0)
                {
                    sb.Append(" ");
                    sb.Append("WHERE ");
                    AppendConditions(criteria, sb);
                    sb.Append(") [__T]");
                }
            }
            else
            {
                AppendFrom(tableName, sb);
                sb.Append(" (NOLOCK)");
                if (criteria._conditions.Count > 0)
                {
                    sb.Append(" ");
                    sb.Append("WHERE ");
                    AppendConditions(criteria, sb);
                }
                if (!isCountCommand)
                {
                    if (criteria._sortBys.Count > 0)
                    {
                        sb.Append(" ");
                        sb.Append("ORDER BY ");
                        AppendSortBys(criteria, sb);
                    }
                }
            }

            return sb.ToString();
        }

        public override string ToParameterName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            return "@" + name.TrimStart('@');
        }

        protected override void AdjustParameterProperties(IParameterExpression parameterExpr, DbParameter parameter)
        {
            var sqlParameter = parameter as SqlParameter;

            if (parameter.Value == null)
            {
                parameter.Value = DBNull.Value;
                return;
            }

            var type = parameter.Value.GetType();
            if (type.IsEnum)
            {
                parameter.DbType = DbType.Int32;
                parameter.Value = Convert.ToInt32(parameter.Value);
                return;
            }
            if (type == typeof(Boolean))
            {
                parameter.Value = (((bool)parameter.Value) ? 1 : 0);
                return;
            }
            if (type == typeof(Guid))
            {
                sqlParameter.SqlDbType = SqlDbType.UniqueIdentifier;
                return;
            }
            if (type == typeof(byte[]))
            {
                if (((byte[])parameter.Value).Length > 8000)
                {
                    sqlParameter.SqlDbType = SqlDbType.Image;
                }
                return;
            }
            if (type == typeof(DateTime))
            {
                sqlParameter.SqlDbType = SqlDbType.DateTime;
                return;
            }
            if (type != typeof(string)) return;
            var stringParameterExpr = parameterExpr as StringParameterExpression;
            if (stringParameterExpr.IsUnicode)
            {
                sqlParameter.SqlDbType = parameter.Value.ToString().Length > 4000 ? SqlDbType.NText : SqlDbType.NVarChar;
            }
            else
            {
                sqlParameter.SqlDbType = parameter.Value.ToString().Length > 8000 ? SqlDbType.Text : SqlDbType.VarChar;
            }
            return;
        }

        public override DbProviderFactory GetDbProviderFactory()
        {
            return SqlClientFactory.Instance;
        }

        public override string GetDatabaseObjectNameQuoteCharacters()
        {
            return "[]";
        }

        #endregion

        #region Singleton

        protected SqlQueryCommandBuilder()
        {
        }

        public static readonly SqlQueryCommandBuilder Instance = new SqlQueryCommandBuilder();

        #endregion
    }
}
