using System;

namespace NIntegrate.Data.OracleClient
{
    /// <summary>
    /// Query extension methods for Oracle database
    /// </summary>
    public static class OracleExtensionMethods
    {
        #region QueryCriteria

        /// <summary>
        /// Gets database side current date.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        public static DateTimeExpression GetCurrentDate(this QueryCriteria criteria)
        {
            return new DateTimeExpression("CURRENT_TIMESTAMP", null);
        }

        #endregion

        #region Expression

        #region Int32 Expression

        /// <summary>
        /// To a char.
        /// </summary>
        /// <param name="expr">The expr.</param>
        /// <returns></returns>
        public static StringExpression ToChar(this Int32Expression expr)
        {
            return new StringExpression(false, "CHR(" + expr.Sql + ")", ((Int32Expression)expr.Clone()).ChildExpressions);
        }

        #endregion

        #region DateTime Expression

        /// <summary>
        /// Add months to current datetime expression.
        /// </summary>
        /// <param name="expr">The expr.</param>
        /// <param name="n">The number of methods.</param>
        /// <returns></returns>
        public static DateTimeExpression AddMonth(this DateTimeExpression expr, int n)
        {
            var newExpr = (DateTimeExpression)expr.Clone();

            newExpr.Sql = "ADD_MONTHS(month, ?, " + newExpr.Sql + ")";
            newExpr.ChildExpressions.Insert(0, new Int32ParameterExpression(n));

            return newExpr;
        }

        /// <summary>
        /// Add months to current datetime expression.
        /// </summary>
        /// <param name="expr">The expr.</param>
        /// <param name="n">The number of methods.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Determines whether current string expression contains specified substring.
        /// </summary>
        /// <param name="expr">The expr.</param>
        /// <param name="value">The substring value.</param>
        /// <returns></returns>
        public static Condition Contains(this StringExpression expr, string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("value");

            return Contains(expr, new StringParameterExpression(value, expr.IsUnicode));
        }

        /// <summary>
        /// Determines whether current string expression contains specified substring.
        /// </summary>
        /// <param name="expr">The expr.</param>
        /// <param name="value">The substring value.</param>
        /// <returns></returns>
        public static Condition Contains(this StringExpression expr, StringExpression value)
        {
            if (ReferenceEquals(value, null))
                throw new ArgumentNullException("value");

            var escapedLikeValue = (StringExpression)value.Clone();
            escapedLikeValue.Sql = "'%' + " + escapedLikeValue.Sql + " + '%'";

            return new Condition(expr, ExpressionOperator.Like, escapedLikeValue);
        }

        /// <summary>
        /// Determines whether current string expression ends with specified string
        /// </summary>
        /// <param name="expr">The expr.</param>
        /// <param name="value">The string value.</param>
        /// <returns></returns>
        public static Condition EndsWith(this StringExpression expr, string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("value");

            return EndsWith(expr, new StringParameterExpression(value, expr.IsUnicode));
        }

        /// <summary>
        /// Determines whether current string expression ends with specified string
        /// </summary>
        /// <param name="expr">The expr.</param>
        /// <param name="value">The string value.</param>
        /// <returns></returns>
        public static Condition EndsWith(this StringExpression expr, StringExpression value)
        {
            if (ReferenceEquals(value, null))
                throw new ArgumentNullException("value");

            var escapedLikeValue = (StringExpression)value.Clone();
            escapedLikeValue.Sql = "'%' + " + escapedLikeValue.Sql;

            return new Condition(expr, ExpressionOperator.Like, escapedLikeValue);
        }

        /// <summary>
        /// Determines whether current string expression starts with specified string
        /// </summary>
        /// <param name="expr">The expr.</param>
        /// <param name="value">The string value.</param>
        /// <returns></returns>
        public static Condition StartsWith(this StringExpression expr, string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("value");

            return StartsWith(expr, new StringParameterExpression(value, expr.IsUnicode));
        }

        /// <summary>
        /// Determines whether current string expression starts with specified string
        /// </summary>
        /// <param name="expr">The expr.</param>
        /// <param name="value">The string value.</param>
        /// <returns></returns>
        public static Condition StartsWith(this StringExpression expr, StringExpression value)
        {
            if (ReferenceEquals(value, null))
                throw new ArgumentNullException("value");

            var escapedLikeValue = (StringExpression)value.Clone();
            escapedLikeValue.Sql = escapedLikeValue.Sql + " + '%'";

            return new Condition(expr, ExpressionOperator.Like, escapedLikeValue);
        }

        /// <summary>
        /// Index of specified substring in current string expression.
        /// </summary>
        /// <param name="expr">The expr.</param>
        /// <param name="value">The substring value.</param>
        /// <returns></returns>
        public static Int32Expression IndexOf(this StringExpression expr, string value)
        {
            var newExpr = new Int32Expression("INSTR(" + expr.Sql + ", ?) - 1", ((StringExpression)expr.Clone()).ChildExpressions);

            newExpr.ChildExpressions.Insert(0, new StringParameterExpression(value, expr.IsUnicode));

            return newExpr;
        }

        /// <summary>
        /// Index of specified substring in current string expression.
        /// </summary>
        /// <param name="expr">The expr.</param>
        /// <param name="value">The substring value.</param>
        /// <returns></returns>
        public static Int32Expression IndexOf(this StringExpression expr, StringExpression value)
        {
            var newExpr = new Int32Expression("INSTR(" + expr.Sql + ", ?) - 1", ((StringExpression)expr.Clone()).ChildExpressions);

            newExpr.ChildExpressions.Insert(0, value);

            return newExpr;
        }

        /// <summary>
        /// Replaces the specified expr.
        /// </summary>
        /// <param name="expr">The expr.</param>
        /// <param name="find">The find.</param>
        /// <param name="replace">The replace.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Replaces the specified expr.
        /// </summary>
        /// <param name="expr">The expr.</param>
        /// <param name="find">The find.</param>
        /// <param name="replace">The replace.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Replaces the specified expr.
        /// </summary>
        /// <param name="expr">The expr.</param>
        /// <param name="find">The find.</param>
        /// <param name="replace">The replace.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Replaces the specified expr.
        /// </summary>
        /// <param name="expr">The expr.</param>
        /// <param name="find">The find.</param>
        /// <param name="replace">The replace.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Substring of the specified expr.
        /// </summary>
        /// <param name="expr">The expr.</param>
        /// <param name="begin">The begin.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static StringExpression Substring(this StringExpression expr, int begin, int length)
        {
            var newExpr = (StringExpression)expr.Clone();

            newExpr.Sql = "SUBSTR(" + newExpr.Sql + ", ? + 1, ?)";
            newExpr.ChildExpressions.Add(new Int32ParameterExpression(begin));
            newExpr.ChildExpressions.Add(new Int32ParameterExpression(length));

            return newExpr;
        }

        /// <summary>
        /// Substring of specified expr.
        /// </summary>
        /// <param name="expr">The expr.</param>
        /// <param name="begin">The begin.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Substring of specified expr.
        /// </summary>
        /// <param name="expr">The expr.</param>
        /// <param name="begin">The begin.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Substring of specified expr.
        /// </summary>
        /// <param name="expr">The expr.</param>
        /// <param name="begin">The begin.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Left trim.
        /// </summary>
        /// <param name="expr">The expr.</param>
        /// <returns></returns>
        public static StringExpression LTrim(this StringExpression expr)
        {
            var newExpr = (StringExpression)expr.Clone();

            newExpr.Sql = "LTRIM(" + expr.Sql + ")";

            return newExpr;
        }

        /// <summary>
        /// Right trim.
        /// </summary>
        /// <param name="expr">The expr.</param>
        /// <returns></returns>
        public static StringExpression RTrim(this StringExpression expr)
        {
            var newExpr = (StringExpression)expr.Clone();

            newExpr.Sql = "RTRIM(" + expr.Sql + ")";

            return newExpr;
        }

        /// <summary>
        /// To ASCII code.
        /// </summary>
        /// <param name="expr">The expr.</param>
        /// <returns></returns>
        public static Int32Expression ToAscii(this StringExpression expr)
        {
            return new Int32Expression("ASCII(" + expr.Sql + ")", ((StringExpression)expr.Clone()).ChildExpressions);
        }

        /// <summary>
        /// Gets the length of a current string expression.
        /// </summary>
        /// <param name="expr">The expr.</param>
        /// <returns></returns>
        public static Int32Expression GetLength(this StringExpression expr)
        {
            return new Int32Expression("LENGTH(" + expr.Sql + ")", ((StringExpression)expr.Clone()).ChildExpressions);
        }

        #endregion

        #endregion
    }
}
