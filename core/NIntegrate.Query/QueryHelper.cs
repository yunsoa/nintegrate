using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Runtime.InteropServices;
using System.Xml;
using NIntegrate.Query.SqlClient;

namespace NIntegrate.Query
{
    [ComVisible(false)]
    public static class QueryHelper
    {
        internal static Type[] KnownTypes()
        {
            return new[]
                       {
                           typeof(Condition),
                           typeof(NullExpression),
                           typeof(BooleanExpression),
                           typeof(ByteExpression),
                           typeof(Int16Expression),
                           typeof(Int32Expression),
                           typeof(Int64Expression),
                           typeof(DateTimeExpression),
                           typeof(StringExpression),
                           typeof(GuidExpression),
                           typeof(DoubleExpression),
                           typeof(DecimalExpression),
                           typeof(ExpressionCollection),
                           typeof(BooleanParameterExpression),
                           typeof(ByteParameterExpression),
                           typeof(Int16ParameterExpression),
                           typeof(Int32ParameterExpression),
                           typeof(Int64ParameterExpression),
                           typeof(DateTimeParameterExpression),
                           typeof(StringParameterExpression),
                           typeof(GuidParameterExpression),
                           typeof(DoubleParameterExpression),
                           typeof(DecimalParameterExpression),
                           typeof(BooleanColumn),
                           typeof(ByteColumn),
                           typeof(Int16Column),
                           typeof(Int32Column),
                           typeof(Int64Column),
                           typeof(DateTimeColumn),
                           typeof(StringColumn),
                           typeof(GuidColumn),
                           typeof(DoubleColumn),
                           typeof(DecimalColumn),
                           typeof(SqlBooleanColumn),
                           typeof(SqlByteColumn),
                           typeof(SqlInt16Column),
                           typeof(SqlInt32Column),
                           typeof(SqlInt64Column),
                           typeof(SqlDateTimeColumn),
                           typeof(SqlStringColumn),
                           typeof(SqlGuidColumn),
                           typeof(SqlDoubleColumn),
                           typeof(SqlDecimalColumn)
                       };
        }

        public static string ToSelectColumnName(this IColumn column)
        {
            var sql = column.ToExpressionCacheableSql();
            var columnName = column.ColumnName.ToDatabaseObjectName();

            if (sql == columnName)
                return sql;
            return sql + " AS " + columnName;
        }

        internal static void GetLeftRightOperatorsForBetween(bool includeLeft, bool includeRight
            , out ExpressionOperator leftOp, out ExpressionOperator rightOp)
        {
            leftOp = includeLeft ? ExpressionOperator.GreaterThanOrEquals : ExpressionOperator.GreaterThan;
            rightOp = includeRight ? ExpressionOperator.LessThanOrEquals : ExpressionOperator.LessThan;
        }

        public static string ToExpressionCacheableSql(this IExpression expr)
        {
            if (ReferenceEquals(expr, null))
                throw new ArgumentNullException("expr");

            if (expr.ChildExpressions == null || expr.ChildExpressions.Count == 0)
                return expr.Sql;

            var splittedSql = expr.Sql.Split('?');
            var sb = new StringBuilder();
            sb.Append(splittedSql[0]);
            for (int i = 1; i < splittedSql.Length; ++i)
            {
                sb.Append(expr.ChildExpressions[i - 1].ToExpressionCacheableSql());
                sb.Append(splittedSql[i]);
            }

            return sb.ToString();
        }

        public static string ToConditionCacheableSql(this Condition condition)
        {
            if (ReferenceEquals(condition, null))
                throw new ArgumentNullException("condition");

            var sb = new StringBuilder();

            if (condition.LinkedConditions.Count > 0 || condition.NotFlag)
            {
                if (condition.NotFlag)
                {
                    sb.Append("NOT ");
                }
                sb.Append("(");
            }

            sb.Append(ToCacheableSql(condition.LeftExpression, condition.Operator, condition.RightExpression));

            for (int i = 0; i < condition.LinkedConditions.Count; ++i)
            {
                if (condition.LinkedConditionAndOrs[i] == ((int)ConditionAndOr.And))
                {
                    sb.Append(" AND ");
                }
                else
                {
                    sb.Append(" OR ");
                }

                sb.Append(condition.LinkedConditions[i].ToConditionCacheableSql());
            }

            if (condition.LinkedConditions.Count > 0 || condition.NotFlag)
            {
                sb.Append(")");
            }

            return sb.ToString();
        }

        private static string ToCacheableSql(IExpression leftExpression, ExpressionOperator op, IExpression rightExpression)
        {
            if (ReferenceEquals(leftExpression, null))
                throw new ArgumentNullException("leftExpression");
            if (op != ExpressionOperator.None)
            {
                if (ReferenceEquals(rightExpression, null))
                    throw new ArgumentNullException("rightExpression");
            }

            switch (op)
            {
                case ExpressionOperator.Like:
                    return leftExpression.ToExpressionCacheableSql() + " LIKE " + rightExpression.ToExpressionCacheableSql();
                case ExpressionOperator.Equals:
                    return leftExpression.ToExpressionCacheableSql() + " = " + rightExpression.ToExpressionCacheableSql();
                case ExpressionOperator.NotEquals:
                    return leftExpression.ToExpressionCacheableSql() + " <> " + rightExpression.ToExpressionCacheableSql();
                case ExpressionOperator.GreaterThan:
                    return leftExpression.ToExpressionCacheableSql() + " > " + rightExpression.ToExpressionCacheableSql();
                case ExpressionOperator.GreaterThanOrEquals:
                    return leftExpression.ToExpressionCacheableSql() + " >= " + rightExpression.ToExpressionCacheableSql();
                case ExpressionOperator.In:
                    return leftExpression.ToExpressionCacheableSql() + " IN " + rightExpression.ToExpressionCacheableSql();
                case ExpressionOperator.LessThan:
                    return leftExpression.ToExpressionCacheableSql() + " < " + rightExpression.ToExpressionCacheableSql();
                case ExpressionOperator.LessThanOrEquals:
                    return leftExpression.ToExpressionCacheableSql() + " <= " + rightExpression.ToExpressionCacheableSql();
                case ExpressionOperator.Is:
                    return leftExpression.ToExpressionCacheableSql() + " IS " + rightExpression.ToExpressionCacheableSql();
                case ExpressionOperator.IsNot:
                    return leftExpression.ToExpressionCacheableSql() + " IS NOT " + rightExpression.ToExpressionCacheableSql();
                case ExpressionOperator.None:
                    return leftExpression.ToExpressionCacheableSql();

                //arthmetric & bitwise operators please call ToString()
            }

            return string.Empty;
        }

        internal static string ToString(ExpressionOperator op)
        {
            switch (op)
            {
                case ExpressionOperator.Add:
                    return "+";
                case ExpressionOperator.Subtract:
                    return "-";
                case ExpressionOperator.Multiply:
                    return "*";
                case ExpressionOperator.Divide:
                    return "/";
                case ExpressionOperator.Mod:
                    return "%";
                case ExpressionOperator.BitwiseAnd:
                    return "&";
                case ExpressionOperator.BitwiseOr:
                    return "|";
                case ExpressionOperator.BitwiseXor:
                    return "^";
                case ExpressionOperator.BitwiseNot:
                    return "~";

                //logic operators please call ToConditionCacheableSql()
            }

            return string.Empty;
        }

        public static string ToDatabaseObjectName(this string name)
        {
            if (name.Contains(")"))
                return name;

            return "[" + name.TrimStart('[').TrimEnd(']').Replace(".", "].[") + "]";
        }

        internal static string CriteriaSerialize(Criteria criteria)
        {
            if (criteria == null)
                throw new ArgumentNullException("criteria");

            var ms = new MemoryStream();
            string xml;
            try
            {
                var dcs = new DataContractSerializer(typeof(Criteria));

                using (var xmlTextWriter = new XmlTextWriter(ms, Encoding.Default))
                {
                    xmlTextWriter.Formatting = Formatting.None;
                    dcs.WriteObject(xmlTextWriter, criteria);
                    xmlTextWriter.Flush();
                    ms = (MemoryStream)xmlTextWriter.BaseStream;
                    ms.Flush();
                    xml = Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            finally
            {
                if (ms != null)
                {
                    ms.Close();
                }
            }

            return xml;
        }

        internal static Criteria CriteriaDeserialize(string xml)
        {
            if (string.IsNullOrEmpty(xml))
                throw new ArgumentNullException("xml");

            object result;
            var dcs = new DataContractSerializer(typeof(Criteria));
            using (var reader = new StringReader(xml))
            using (XmlReader xmlReader = new XmlTextReader(reader))
            {
                result = dcs.ReadObject(xmlReader);
            }

            return (Criteria)result;
        }

        public static IExpression CreateParameterExpression(object value)
        {
            if (value == null || value == DBNull.Value)
                return NullExpression.Value;

            switch (Type.GetTypeCode(value.GetType()))
            {
                case TypeCode.Boolean:
                    return new BooleanParameterExpression((bool)value);
                case TypeCode.Byte:
                case TypeCode.SByte:
                    return new ByteParameterExpression((byte)value);
                case TypeCode.Char:
                case TypeCode.String:
                    return new StringParameterExpression((string)value, true);
                case TypeCode.DateTime:
                    return new DateTimeParameterExpression((DateTime)value);
                case TypeCode.Decimal:
                    return new DecimalParameterExpression((decimal)value);
                case TypeCode.Single:
                case TypeCode.Double:
                    return new DoubleParameterExpression((double)value);
                case TypeCode.UInt16:
                case TypeCode.Int16:
                    return new Int16ParameterExpression((short)value);
                case TypeCode.UInt32:
                case TypeCode.Int32:
                    return new Int32ParameterExpression((int)value);
                case TypeCode.UInt64:
                case TypeCode.Int64:
                    return new Int64ParameterExpression((long)value);
            }

            return new StringParameterExpression(value.ToString(), true);
        }

        #region DefaultValue

        /// <summary>
        /// Gets the default value of a specified Type.
        /// </summary>
        /// <returns>The default value.</returns>
        public static T DefaultValue<T>()
        {
            return default(T);
        }

        /// <summary>
        /// Gets the default value of a specified Type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static object DefaultValue(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            return typeof(QueryHelper).GetMethod("DefaultValue", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, Type.EmptyTypes, null).MakeGenericMethod(type).Invoke(null, null);
        }

        #endregion
    }
}
