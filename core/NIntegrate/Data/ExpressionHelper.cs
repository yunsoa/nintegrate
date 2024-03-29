﻿using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace NIntegrate.Data
{
    internal static class ExpressionHelper
    {
        #region Public Methods

        public static string ToSelectColumnName(this IColumn column)
        {
            var sql = column.ToExpressionCacheableSql();
            var columnName = column.ColumnName.ToDatabaseObjectName();

            if (sql == columnName)
                return sql;
            return sql + " AS " + columnName;
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

            var en = expr.ChildExpressions.GetEnumerator();
            for (int i = 1; i < splittedSql.Length; ++i)
            {
                en.MoveNext(); //en.Current === expr.ChildExpressions[i - 1]
                sb.Append(en.Current.ToExpressionCacheableSql());
                sb.Append(splittedSql[i]);
            }

            return sb.ToString();
        }

        public static string ToConditionCacheableSql(this Condition condition)
        {
            if (ReferenceEquals(condition, null))
                throw new ArgumentNullException("condition");

            var sb = new StringBuilder();

            if (condition.LinkedConditions.Count > 0 || condition.IsNot)
            {
                if (condition.IsNot)
                {
                    sb.Append("NOT ");
                }
                sb.Append("(");
            }

            sb.Append(ToCacheableSql(condition.LeftExpression, condition.Operator, condition.RightExpression));

            var en = condition.LinkedConditions.GetEnumerator();
            var enAndOr = condition.LinkedConditionAndOrs.GetEnumerator();
            while (en.MoveNext() && enAndOr.MoveNext())
            {
                if (enAndOr.Current == ((int)ConditionAndOr.And))
                {
                    sb.Append(" AND ");
                }
                else
                {
                    sb.Append(" OR ");
                }

                sb.Append(en.Current.ToConditionCacheableSql());
            }

            if (condition.LinkedConditions.Count > 0 || condition.IsNot)
            {
                sb.Append(")");
            }

            return sb.ToString();
        }

        public static string ToDatabaseObjectName(this string name)
        {
            if (name.Contains(")"))
                return name;

            return "[" + name.TrimStart('[').TrimEnd(']').Replace(".", "].[") + "]";
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

        public static string Serialize<T>(T value)
        {
            if (ReferenceEquals(value, default(T)))
                throw new ArgumentNullException("value");

            var ms = new MemoryStream();
            string xml;
            try
            {
                var dcs = new DataContractSerializer(typeof(T));

                using (var xmlTextWriter = new XmlTextWriter(ms, Encoding.Default))
                {
                    xmlTextWriter.Formatting = Formatting.None;
                    dcs.WriteObject(xmlTextWriter, value);
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

        public static T Deserialize<T>(string xml)
        {
            if (string.IsNullOrEmpty(xml))
                throw new ArgumentNullException("xml");

            object result;
            var dcs = new DataContractSerializer(typeof(T));
            using (var reader = new StringReader(xml))
            using (XmlReader xmlReader = new XmlTextReader(reader))
            {
                result = dcs.ReadObject(xmlReader);
            }

            return (T)result;
        }

        public static string ToString(ExpressionOperator op)
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

        public static void GetLeftRightOperatorsForBetween(bool includeLeft, bool includeRight
            , out ExpressionOperator leftOp, out ExpressionOperator rightOp)
        {
            leftOp = includeLeft ? ExpressionOperator.GreaterThanOrEquals : ExpressionOperator.GreaterThan;
            rightOp = includeRight ? ExpressionOperator.LessThanOrEquals : ExpressionOperator.LessThan;
        }

        public static T DefaultValue<T>()
        {
            return default(T);
        }

        public static object DefaultValue(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            return typeof(ExpressionHelper).GetMethod("DefaultValue", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, Type.EmptyTypes, null).MakeGenericMethod(type).Invoke(null, null);
        }

        #endregion

        #region Non-Public Methods

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

        #endregion
    }
}
