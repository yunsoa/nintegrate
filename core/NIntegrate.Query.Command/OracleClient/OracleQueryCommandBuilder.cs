using System;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Text;

namespace NIntegrate.Query.Command.OracleClient
{
    /// <summary>
    /// The IQueryCommandBuilder implementation for Oracle database.
    /// </summary>
    public class OracleQueryCommandBuilder : QueryCommandBuilder
    {
        #region Private Methods

        private string BuildCountCacheableSql(string tableName, Criteria criteria)
        {
            var sb = new StringBuilder();
            sb.Append("SELECT COUNT(1) ");

            if (criteria.IsDistinct)
            {
                sb.Append("FROM (SELECT DISTINCT ");
                AppendResultColumns(criteria, sb);
                sb.Append(" ");
                AppendFrom(tableName, sb);
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
                AppendFrom(tableName, sb);
                if (criteria.Conditions.Count > 0)
                {
                    sb.Append(" ");
                    sb.Append("WHERE ");
                    AppendConditions(criteria, sb);
                }
            }

            return sb.ToString();
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// Builds the paging cacheable SQL.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        protected override string BuildPagingCacheableSql(string tableName, Criteria criteria)
        {
            var sb = new StringBuilder();
            sb.Append("SELECT ");
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
            sb.Append("FROM ");
            sb.Append("(SELECT ");
            if (criteria.IsDistinct)
            {
                sb.Append("DISTINCT ");
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

            AppendFrom(tableName, sb);

            if (criteria.Conditions.Count > 0)
            {
                sb.Append(" ");
                sb.Append("WHERE ");
                AppendConditions(criteria, sb);
            }

            sb.Append(") [__T] WHERE [__T].[__Pos] > ");

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
        /// <param name="tableName">Name of the table.</param>
        /// <param name="criteria">The criteria.</param>
        /// <param name="isCountCommand">if set to <c>true</c> [is count command].</param>
        /// <returns></returns>
        protected override string BuildNoPagingCacheableSql(string tableName, Criteria criteria, bool isCountCommand)
        {
            if (isCountCommand)
                return BuildCountCacheableSql(tableName, criteria);

            if (criteria.MaxResults > 0)
                return BuildPagingCacheableSql(tableName, criteria);

            var sb = new StringBuilder();
            sb.Append("SELECT ");
            if (criteria.IsDistinct)
            {
                sb.Append("DISTINCT ");
            }

            AppendResultColumns(criteria, sb);

            sb.Append(" ");

            AppendFrom(tableName, sb);
            if (criteria.Conditions.Count > 0)
            {
                sb.Append(" ");
                sb.Append("WHERE ");
                AppendConditions(criteria, sb);
            }
            if (criteria.SortBys.Count > 0)
            {
                sb.Append(" ");
                sb.Append("ORDER BY ");
                AppendSortBys(criteria, sb);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Toes the name of the parameter.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public override string ToParameterName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            return ":" + name.TrimStart(':');
        }

        /// <summary>
        /// Adjusts the parameter properties.
        /// </summary>
        /// <param name="parameterExpr">The parameter expr.</param>
        /// <param name="parameter">The parameter.</param>
        protected override void AdjustParameterProperties(IParameterExpression parameterExpr, DbParameter parameter)
        {
            var oracleParameter = parameter as OracleParameter;

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
                oracleParameter.OracleType = OracleType.Char;
                oracleParameter.Size = 36;
                parameter.Value = parameter.Value.ToString();
                return;
            }
            if (type == typeof(byte[]))
            {
                if (((byte[])parameter.Value).Length > 8000)
                {
                    oracleParameter.OracleType = OracleType.Blob;
                }
                return;
            }
            if (type == typeof(DateTime))
            {
                oracleParameter.OracleType = OracleType.DateTime;
                return;
            }
            if (type != typeof(string)) return;
            var stringParameterExpr = parameterExpr as StringParameterExpression;
            if (stringParameterExpr.IsUnicode)
            {
                oracleParameter.OracleType = parameter.Value.ToString().Length > 2000 ? OracleType.NClob : OracleType.NVarChar;
            }
            else
            {
                oracleParameter.OracleType = parameter.Value.ToString().Length > 4000 ? OracleType.Clob : OracleType.VarChar;
            }
            return;
        }

        /// <summary>
        /// Gets the db provider factory.
        /// </summary>
        /// <returns></returns>
        public override DbProviderFactory GetDbProviderFactory()
        {
            return OracleClientFactory.Instance;
        }

        /// <summary>
        /// Gets the database object name quote characters.
        /// </summary>
        /// <returns></returns>
        public override string GetDatabaseObjectNameQuoteCharacters()
        {
            return "\"\"";
        }

        #endregion

        #region Singleton

        /// <summary>
        /// Initializes a new instance of the <see cref="OracleQueryCommandBuilder"/> class.
        /// </summary>
        protected OracleQueryCommandBuilder()
        {
        }

        /// <summary>
        /// The singleton instance.
        /// </summary>
        public static readonly OracleQueryCommandBuilder Instance = new OracleQueryCommandBuilder();

        #endregion
    }
}
