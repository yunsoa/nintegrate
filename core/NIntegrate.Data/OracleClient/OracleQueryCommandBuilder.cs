using System;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Runtime.InteropServices;
using System.Text;
using System.Globalization;

namespace NIntegrate.Data.OracleClient
{
    /// <summary>
    /// The IQueryCommandBuilder implementation for Oracle database.
    /// </summary>
    [ComVisible(false)]
    public class OracleQueryCommandBuilder : QueryCommandBuilder
    {
        /// <summary>
        /// The singleton.
        /// </summary>
        public static readonly OracleQueryCommandBuilder Instance;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OracleQueryCommandBuilder"/> class.
        /// </summary>
        protected OracleQueryCommandBuilder()
        {
        }

        /// <summary>
        /// Initializes the <see cref="OracleQueryCommandBuilder"/> class.
        /// </summary>
        static OracleQueryCommandBuilder()
        {
            Instance = new OracleQueryCommandBuilder();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the db provider factory.
        /// </summary>
        /// <returns></returns>
        public override DbProviderFactory GetDbProviderFactory()
        {
            return OracleClientFactory.Instance;
        }

        #endregion

        #region Non-Public Methods

        protected override string BuildPagingCacheableSql(QueryCriteria criteria)
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

            AppendFrom(criteria.TableName, sb);

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

        protected override string BuildNoPagingCacheableSql(QueryCriteria criteria, bool isCountCommand)
        {
            if (isCountCommand)
                return BuildCountCacheableSql(criteria);

            if (criteria.MaxResults > 0)
                return BuildPagingCacheableSql(criteria);

            var sb = new StringBuilder();
            sb.Append("SELECT ");
            if (criteria.IsDistinct)
            {
                sb.Append("DISTINCT ");
            }

            AppendResultColumns(criteria, sb);

            sb.Append(" ");

            AppendFrom(criteria.TableName, sb);
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

        protected override string ToParameterName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            return ":" + name.TrimStart(':');
        }

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

        protected override string GetDatabaseObjectNameQuoteCharacters()
        {
            return "\"\"";
        }

        private static string BuildCountCacheableSql(QueryCriteria criteria)
        {
            var sb = new StringBuilder();
            sb.Append("SELECT COUNT(1) ");

            if (criteria.IsDistinct)
            {
                sb.Append("FROM (SELECT DISTINCT ");
                AppendResultColumns(criteria, sb);
                sb.Append(" ");
                AppendFrom(criteria.TableName, sb);
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
    }
}
