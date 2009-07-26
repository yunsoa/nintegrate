using System;

namespace NIntegrate.Data.OracleClient
{
    public static class OracleExtensionMethods
    {
        #region QueryCriteria

        public static DateTimeExpression GetCurrentDate(this QueryCriteria criteria)
        {
            return new DateTimeExpression("CURRENT_TIMESTAMP", null);
        }

        #endregion

        #region Expression

        #region Int32 Expression

        public static StringExpression ToChar(this Int32Expression expr)
        {
            return new StringExpression(false, "CHR(" + expr.Sql + ")", ((Int32Expression)expr.Clone()).ChildExpressions);
        }

        #endregion

        #region DateTime Expression

        public static DateTimeExpression AddMonth(this DateTimeExpression expr, int n)
        {
            var newExpr = (DateTimeExpression)expr.Clone();

            newExpr.Sql = "ADD_MONTHS(month, ?, " + newExpr.Sql + ")";
            newExpr.ChildExpressions.Insert(0, new Int32ParameterExpression(n));

            return newExpr;
        }

        public static DateTimeExpression AddMonth(this DateTimeExpression expr, Int32Expression n)
        {
            if (n == null)
                throw new ArgumentNullException("n");

            var newExpr = (DateTimeExpression)expr.Clone();

            newExpr.Sql = "ADD_MONTHS(month, ?, " + newExpr.Sql + ")";
            newExpr.ChildExpressions.Insert(0, n);

            return newExpr;
        }

        #endregion

        #region String Expression

        public static Condition Contains(this StringExpression expr, string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("value");

            return Contains(expr, new StringParameterExpression(value, expr.IsUnicode));
        }

        public static Condition Contains(this StringExpression expr, StringExpression value)
        {
            if (ReferenceEquals(value, null))
                throw new ArgumentNullException("value");

            var escapedLikeValue = (StringExpression)value.Clone();
            escapedLikeValue.Sql = "'%' + " + escapedLikeValue.Sql + " + '%'";

            return new Condition(expr, ExpressionOperator.Like, escapedLikeValue);
        }

        public static Condition EndsWith(this StringExpression expr, string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("value");

            return EndsWith(expr, new StringParameterExpression(value, expr.IsUnicode));
        }

        public static Condition EndsWith(this StringExpression expr, StringExpression value)
        {
            if (ReferenceEquals(value, null))
                throw new ArgumentNullException("value");

            var escapedLikeValue = (StringExpression)value.Clone();
            escapedLikeValue.Sql = "'%' + " + escapedLikeValue.Sql;

            return new Condition(expr, ExpressionOperator.Like, escapedLikeValue);
        }

        public static Condition StartsWith(this StringExpression expr, string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("value");

            return StartsWith(expr, new StringParameterExpression(value, expr.IsUnicode));
        }

        public static Condition StartsWith(this StringExpression expr, StringExpression value)
        {
            if (ReferenceEquals(value, null))
                throw new ArgumentNullException("value");

            var escapedLikeValue = (StringExpression)value.Clone();
            escapedLikeValue.Sql = escapedLikeValue.Sql + " + '%'";

            return new Condition(expr, ExpressionOperator.Like, escapedLikeValue);
        }

        public static Int32Expression IndexOf(this StringExpression expr, string value)
        {
            var newExpr = new Int32Expression("INSTR(" + expr.Sql + ", ?) - 1", ((StringExpression)expr.Clone()).ChildExpressions);

            newExpr.ChildExpressions.Insert(0, new StringParameterExpression(value, expr.IsUnicode));

            return newExpr;
        }

        public static Int32Expression IndexOf(this StringExpression expr, StringExpression value)
        {
            var newExpr = new Int32Expression("INSTR(" + expr.Sql + ", ?) - 1", ((StringExpression)expr.Clone()).ChildExpressions);

            newExpr.ChildExpressions.Insert(0, value);

            return newExpr;
        }

        public static StringExpression Replace(this StringExpression expr, string find, string replace)
        {
            if (string.IsNullOrEmpty(find))
                throw new ArgumentNullException("find");
            if (string.IsNullOrEmpty(replace))
                throw new ArgumentNullException("replace");

            var newExpr = (StringExpression)expr.Clone();

            newExpr.Sql = "REPLACE(" + newExpr.Sql + ", ?, ?)";
            newExpr.ChildExpressions.Add(new StringParameterExpression(find, expr.IsUnicode));
            newExpr.ChildExpressions.Add(new StringParameterExpression(replace, expr.IsUnicode));

            return newExpr;
        }

        public static StringExpression Replace(this StringExpression expr, StringExpression find, string replace)
        {
            if (ReferenceEquals(find, null))
                throw new ArgumentNullException("find");
            if (string.IsNullOrEmpty(replace))
                throw new ArgumentNullException("replace");

            var newExpr = (StringExpression)expr.Clone();

            newExpr.Sql = "REPLACE(" + newExpr.Sql + ", ?, ?)";
            newExpr.ChildExpressions.Add(find);
            newExpr.ChildExpressions.Add(new StringParameterExpression(replace, expr.IsUnicode));

            return newExpr;
        }

        public static StringExpression Replace(this StringExpression expr, string find, StringExpression replace)
        {
            if (string.IsNullOrEmpty(find))
                throw new ArgumentNullException("find");
            if (ReferenceEquals(replace, null))
                throw new ArgumentNullException("replace");

            var newExpr = (StringExpression)expr.Clone();

            newExpr.Sql = "REPLACE(" + newExpr.Sql + ", ?, ?)";
            newExpr.ChildExpressions.Add(new StringParameterExpression(find, expr.IsUnicode));
            newExpr.ChildExpressions.Add(replace);

            return newExpr;
        }

        public static StringExpression Replace(this StringExpression expr, StringExpression find, StringExpression replace)
        {
            if (ReferenceEquals(find, null))
                throw new ArgumentNullException("find");
            if (ReferenceEquals(replace, null))
                throw new ArgumentNullException("replace");

            var newExpr = (StringExpression)expr.Clone();

            newExpr.Sql = "REPLACE(" + newExpr.Sql + ", ?, ?)";
            newExpr.ChildExpressions.Add(find);
            newExpr.ChildExpressions.Add(replace);

            return newExpr;
        }

        public static StringExpression Substring(this StringExpression expr, int begin, int length)
        {
            var newExpr = (StringExpression)expr.Clone();

            newExpr.Sql = "SUBSTR(" + newExpr.Sql + ", ? + 1, ?)";
            newExpr.ChildExpressions.Add(new Int32ParameterExpression(begin));
            newExpr.ChildExpressions.Add(new Int32ParameterExpression(length));

            return newExpr;
        }

        public static StringExpression Substring(this StringExpression expr, Int32Expression begin, int length)
        {
            if (ReferenceEquals(begin, null))
                throw new ArgumentNullException("begin");

            var newExpr = (StringExpression)expr.Clone();

            newExpr.Sql = "SUBSTR(" + newExpr.Sql + ", ? + 1, ?)";
            newExpr.ChildExpressions.Add(begin);
            newExpr.ChildExpressions.Add(new Int32ParameterExpression(length));

            return newExpr;
        }

        public static StringExpression Substring(this StringExpression expr, int begin, Int32Expression length)
        {
            if (ReferenceEquals(length, null))
                throw new ArgumentNullException("length");

            var newExpr = (StringExpression)expr.Clone();

            newExpr.Sql = "SUBSTR(" + newExpr.Sql + ", ? + 1, ?)";
            newExpr.ChildExpressions.Add(new Int32ParameterExpression(begin));
            newExpr.ChildExpressions.Add(length);

            return newExpr;
        }

        public static StringExpression Substring(this StringExpression expr, Int32Expression begin, Int32Expression length)
        {
            if (ReferenceEquals(begin, null))
                throw new ArgumentNullException("begin");
            if (ReferenceEquals(length, null))
                throw new ArgumentNullException("length");

            var newExpr = (StringExpression)expr.Clone();

            newExpr.Sql = "SUBSTR(" + newExpr.Sql + ", ? + 1, ?)";
            newExpr.ChildExpressions.Add(begin);
            newExpr.ChildExpressions.Add(length);

            return newExpr;
        }

        public static StringExpression LTrim(this StringExpression expr)
        {
            var newExpr = (StringExpression)expr.Clone();

            newExpr.Sql = "LTRIM(" + expr.Sql + ")";

            return newExpr;
        }

        public static StringExpression RTrim(this StringExpression expr)
        {
            var newExpr = (StringExpression)expr.Clone();

            newExpr.Sql = "RTRIM(" + expr.Sql + ")";

            return newExpr;
        }

        public static Int32Expression ToAscii(this StringExpression expr)
        {
            return new Int32Expression("ASCII(" + expr.Sql + ")", ((StringExpression)expr.Clone()).ChildExpressions);
        }

        public static Int32Expression GetLength(this StringExpression expr)
        {
            return new Int32Expression("LENGTH(" + expr.Sql + ")", ((StringExpression)expr.Clone()).ChildExpressions);
        }

        #endregion

        #endregion
    }
}
