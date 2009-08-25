using System;

namespace NIntegrate.Query.OracleClient
{
    public static class OracleExtensionMethods
    {
        #region Criteria

        public static DateTimeExpression GetCurrentDate(this Criteria criteria)
        {
            return new DateTimeExpression("CURRENT_TIMESTAMP", null);
        }

        #endregion

        #region Expression

        #region Int32 Expression

        public static StringExpression ToChar(this Int32Expression expr)
        {
            return new StringExpression(false, "CHR(" + expr._sql + ")", ((Int32Expression)expr.Clone())._childExpressions);
        }

        #endregion

        #region DateTime Expression

        public static DateTimeExpression AddMonth(this DateTimeExpression expr, int n)
        {
            var newExpr = (DateTimeExpression)expr.Clone();

            newExpr._sql = "ADD_MONTHS(month, ?, " + newExpr._sql + ")";
            newExpr._childExpressions.Insert(0, new Int32ParameterExpression(n));

            return newExpr;
        }

        public static DateTimeExpression AddMonth(this DateTimeExpression expr, Int32Expression n)
        {
            if (n == null)
                throw new ArgumentNullException("n");

            var newExpr = (DateTimeExpression)expr.Clone();

            newExpr._sql = "ADD_MONTHS(month, ?, " + newExpr._sql + ")";
            newExpr._childExpressions.Insert(0, n);

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
            escapedLikeValue._sql = "'%' + " + escapedLikeValue._sql + " + '%'";

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
            escapedLikeValue._sql = "'%' + " + escapedLikeValue._sql;

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
            escapedLikeValue._sql = escapedLikeValue._sql + " + '%'";

            return new Condition(expr, ExpressionOperator.Like, escapedLikeValue);
        }

        public static Int32Expression IndexOf(this StringExpression expr, string value)
        {
            var newExpr = new Int32Expression("INSTR(" + expr._sql + ", ?) - 1", ((StringExpression)expr.Clone())._childExpressions);

            newExpr.ChildExpressions.Insert(0, new StringParameterExpression(value, expr.IsUnicode));

            return newExpr;
        }

        public static Int32Expression IndexOf(this StringExpression expr, StringExpression value)
        {
            var newExpr = new Int32Expression("INSTR(" + expr._sql + ", ?) - 1", ((StringExpression)expr.Clone())._childExpressions);

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

            newExpr._sql = "REPLACE(" + newExpr._sql + ", ?, ?)";
            newExpr._childExpressions.Add(new StringParameterExpression(find, expr.IsUnicode));
            newExpr._childExpressions.Add(new StringParameterExpression(replace, expr.IsUnicode));

            return newExpr;
        }

        public static StringExpression Replace(this StringExpression expr, StringExpression find, string replace)
        {
            if (ReferenceEquals(find, null))
                throw new ArgumentNullException("find");
            if (string.IsNullOrEmpty(replace))
                throw new ArgumentNullException("replace");

            var newExpr = (StringExpression)expr.Clone();

            newExpr._sql = "REPLACE(" + newExpr._sql + ", ?, ?)";
            newExpr._childExpressions.Add(find);
            newExpr._childExpressions.Add(new StringParameterExpression(replace, expr.IsUnicode));

            return newExpr;
        }

        public static StringExpression Replace(this StringExpression expr, string find, StringExpression replace)
        {
            if (string.IsNullOrEmpty(find))
                throw new ArgumentNullException("find");
            if (ReferenceEquals(replace, null))
                throw new ArgumentNullException("replace");

            var newExpr = (StringExpression)expr.Clone();

            newExpr._sql = "REPLACE(" + newExpr._sql + ", ?, ?)";
            newExpr._childExpressions.Add(new StringParameterExpression(find, expr.IsUnicode));
            newExpr._childExpressions.Add(replace);

            return newExpr;
        }

        public static StringExpression Replace(this StringExpression expr, StringExpression find, StringExpression replace)
        {
            if (ReferenceEquals(find, null))
                throw new ArgumentNullException("find");
            if (ReferenceEquals(replace, null))
                throw new ArgumentNullException("replace");

            var newExpr = (StringExpression)expr.Clone();

            newExpr._sql = "REPLACE(" + newExpr._sql + ", ?, ?)";
            newExpr._childExpressions.Add(find);
            newExpr._childExpressions.Add(replace);

            return newExpr;
        }

        public static StringExpression Substring(this StringExpression expr, int begin, int length)
        {
            var newExpr = (StringExpression)expr.Clone();

            newExpr._sql = "SUBSTR(" + newExpr._sql + ", ? + 1, ?)";
            newExpr._childExpressions.Add(new Int32ParameterExpression(begin));
            newExpr._childExpressions.Add(new Int32ParameterExpression(length));

            return newExpr;
        }

        public static StringExpression Substring(this StringExpression expr, Int32Expression begin, int length)
        {
            if (ReferenceEquals(begin, null))
                throw new ArgumentNullException("begin");

            var newExpr = (StringExpression)expr.Clone();

            newExpr._sql = "SUBSTR(" + newExpr._sql + ", ? + 1, ?)";
            newExpr._childExpressions.Add(begin);
            newExpr._childExpressions.Add(new Int32ParameterExpression(length));

            return newExpr;
        }

        public static StringExpression Substring(this StringExpression expr, int begin, Int32Expression length)
        {
            if (ReferenceEquals(length, null))
                throw new ArgumentNullException("length");

            var newExpr = (StringExpression)expr.Clone();

            newExpr._sql = "SUBSTR(" + newExpr._sql + ", ? + 1, ?)";
            newExpr._childExpressions.Add(new Int32ParameterExpression(begin));
            newExpr._childExpressions.Add(length);

            return newExpr;
        }

        public static StringExpression Substring(this StringExpression expr, Int32Expression begin, Int32Expression length)
        {
            if (ReferenceEquals(begin, null))
                throw new ArgumentNullException("begin");
            if (ReferenceEquals(length, null))
                throw new ArgumentNullException("length");

            var newExpr = (StringExpression)expr.Clone();

            newExpr._sql = "SUBSTR(" + newExpr._sql + ", ? + 1, ?)";
            newExpr._childExpressions.Add(begin);
            newExpr._childExpressions.Add(length);

            return newExpr;
        }

        public static StringExpression LTrim(this StringExpression expr)
        {
            var newExpr = (StringExpression)expr.Clone();

            newExpr._sql = "LTRIM(" + expr._sql + ")";

            return newExpr;
        }

        public static StringExpression RTrim(this StringExpression expr)
        {
            var newExpr = (StringExpression)expr.Clone();

            newExpr._sql = "RTRIM(" + expr._sql + ")";

            return newExpr;
        }

        public static Int32Expression ToAscii(this StringExpression expr)
        {
            return new Int32Expression("ASCII(" + expr._sql + ")", ((StringExpression)expr.Clone())._childExpressions);
        }

        public static Int32Expression GetLength(this StringExpression expr)
        {
            return new Int32Expression("LENGTH(" + expr._sql + ")", ((StringExpression)expr.Clone())._childExpressions);
        }

        #endregion

        #endregion
    }
}
