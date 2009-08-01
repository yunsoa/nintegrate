using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace NIntegrate.Data.SqlClient
{
    /// <summary>
    /// The IQueryCommandBuilder implementation for SQL Server database.
    /// </summary>
    [ComVisible(false)]
    public class SqlQueryCommandBuilder : QueryCommandBuilder
    {
        #region Override Methods

        /// <summary>
        /// Builds the paging cacheable SQL.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        protected override string BuildPagingCacheableSql(QueryCriteria criteria)
        {
            var sb = new StringBuilder();
            sb.Append("WITH [__T] AS (SELECT ");
            if (criteria.IsDistinct)
            {
                sb.Append("DISTINCT ");
            }
            if (criteria.MaxResults + criteria.SkipResults > 0)
            {
                sb.Append("TOP ");
                sb.Append(criteria.MaxResults + criteria.SkipResults);
                sb.Append(" ");
            }

            AppendResultColumns(criteria, sb);

            sb.Append(", ROW_NUMBER() OVER (ORDER BY ");
            if (criteria.SortBys.Count > 0)
            {
                AppendSortBys(criteria, sb);
            }
            else
            {
                if (criteria.ResultColumns.Count == 0)
                {
                    if (criteria.PredefinedColumns.Count > 0)
                    {
                        sb.Append(criteria.PredefinedColumns[0].ToExpressionCacheableSql());
                    }
                    else
                    {
                        sb.Append("1");
                    }
                }
                else
                {
                    sb.Append(criteria.ResultColumns[0].ToExpressionCacheableSql());
                }
            }
            sb.Append(") AS [__Pos] ");

            AppendFrom(criteria.TableName, sb);
            sb.Append(" (NOLOCK)");

            if (criteria.Conditions.Count > 0)
            {
                sb.Append(" ");
                sb.Append("WHERE ");
                AppendConditions(criteria, sb);
            }

            sb.Append(") SELECT ");
            if (criteria.ResultColumns.Count == 0)
            {
                var separate = "";
                if (criteria.PredefinedColumns.Count > 0)
                {
                    foreach (var column in criteria.PredefinedColumns)
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
                foreach (var column in criteria.ResultColumns)
                {
                    sb.Append(separate);
                    sb.Append("[__T].");
                    sb.Append(column.ColumnName.ToDatabaseObjectName());

                    separate = ", ";
                }
            }
            sb.Append(" ");
            sb.Append("FROM [__T] WHERE [__T].[__Pos] > ");
            sb.Append(criteria.SkipResults);
            if (criteria.MaxResults > 0)
            {
                sb.Append(" ");
                sb.Append("AND [__T].[__Pos] <= ");
                sb.Append(criteria.MaxResults + criteria.SkipResults);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Builds the no paging cacheable SQL.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        protected override string BuildNoPagingCacheableSql(QueryCriteria criteria, bool isCountCommand)
        {
            var sb = new StringBuilder();
            sb.Append("SELECT ");
            if (criteria.QueryType == QueryType.Select)
            {
                if (criteria.IsDistinct)
                {
                    sb.Append("DISTINCT ");
                }
                if (criteria.MaxResults > 0)
                {
                    sb.Append("TOP ");
                    sb.Append(criteria.MaxResults);
                    sb.Append(" ");
                }

                AppendResultColumns(criteria, sb);
            }
            else
            {
                sb.Append("COUNT(1)");
            }
            sb.Append(" ");

            if (isCountCommand && criteria.IsDistinct)
            {
                sb.Append("FROM (SELECT DISTINCT ");
                AppendResultColumns(criteria, sb);
                sb.Append(" ");
                AppendFrom(criteria.TableName, sb);
                sb.Append(" (NOLOCK)");
                if (criteria.Conditions.Count > 0)
                {
                    sb.Append(" ");
                    sb.Append("WHERE ");
                    AppendConditions(criteria, sb);
                    sb.Append(") [__T]");
                }
            }
            else
            {
                AppendFrom(criteria.TableName, sb);
                sb.Append(" (NOLOCK)");
                if (criteria.Conditions.Count > 0)
                {
                    sb.Append(" ");
                    sb.Append("WHERE ");
                    AppendConditions(criteria, sb);
                }
                if (criteria.QueryType == QueryType.Select)
                {
                    if (criteria.SortBys.Count > 0)
                    {
                        sb.Append(" ");
                        sb.Append("ORDER BY ");
                        AppendSortBys(criteria, sb);
                    }
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Toes the name of the parameter.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        protected override string ToParameterName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            return "@" + name.TrimStart('@');
        }

        /// <summary>
        /// Adjusts the parameter properties.
        /// </summary>
        /// <param name="parameterExpr">The parameter expr.</param>
        /// <param name="parameter">The parameter.</param>
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
                parameter.Value = Convert.ToInt32(parameter.Value, CultureInfo.InvariantCulture);
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

        /// <summary>
        /// Gets the db provider factory.
        /// </summary>
        /// <returns></returns>
        public override DbProviderFactory GetDbProviderFactory()
        {
            return SqlClientFactory.Instance;
        }

        /// <summary>
        /// Gets the database object name quote characters.
        /// </summary>
        /// <returns></returns>
        protected override string GetDatabaseObjectNameQuoteCharacters()
        {
            return "[]";
        }

        #endregion

        #region Singleton

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlQueryCommandBuilder"/> class.
        /// </summary>
        protected SqlQueryCommandBuilder()
        {
        }

        /// <summary>
        /// The singleton instance.
        /// </summary>
        public static readonly SqlQueryCommandBuilder Instance = new SqlQueryCommandBuilder();

        #endregion
    }
}
