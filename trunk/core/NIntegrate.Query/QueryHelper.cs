using System;
using System.Text;
using System.Runtime.InteropServices;

namespace NBear.Query
{
    [ComVisible(false)]
    internal static class QueryHelper
    {
        internal static void GetLeftRightOperatorsForBetween(bool includeLeft, bool includeRight
            , out ExpressionOperator leftOp, out ExpressionOperator rightOp)
        {
            leftOp = includeLeft ? ExpressionOperator.GreaterThanOrEquals : ExpressionOperator.GreaterThan;
            rightOp = includeRight ? ExpressionOperator.LessThanOrEquals : ExpressionOperator.LessThan;
        }

        internal static string ToExpressionCacheableSql(this IExpression expr)
        {
            if (ReferenceEquals(expr, null))
                throw new ArgumentNullException("expr");

            if (expr.ChildExpressions.Count == 0)
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

        internal static string ToConditionCacheableSql(this Condition condition)
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

            if (condition.Operator != ExpressionOperator.None)
            {
                sb.Append(ToCacheableSql(condition.LeftExpression, condition.Operator, condition.RightExpression));
            }

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

        internal static string ToDatabaseObjectName(this string name)
        {
            if (name.Contains(")"))
                return name;

            return "[" + name.TrimStart('[').TrimEnd(']') + "]";
        }
    }
}
