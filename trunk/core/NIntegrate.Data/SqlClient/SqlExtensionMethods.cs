using System;

namespace NIntegrate.Data.SqlClient
{
    public static class SqlExtensionMethods
    {
        #region QueryCriteria

        public static QueryCriteria AddSortByRandom(this QueryCriteria criteria)
        {
            criteria.SortBys.Add(new GuidColumn(new GuidExpression("newid()", null), "newid()"), false);

            return criteria;
        }

        public static DateTimeExpression GetCurrentDate(this QueryCriteria criteria)
        {
            return new DateTimeExpression("getdate()", null);
        }

        public static DateTimeExpression GetCurrentUtcDate(this QueryCriteria criteria)
        {
            return new DateTimeExpression("getutcdate()", null);
        }

        #endregion

        #region Expression

        #region Int32 Expression

        public static StringExpression ToChar(this Int32Expression expr)
        {
            return new StringExpression(false, "CHAR(" + expr.Sql + ")", ((Int32Expression)expr.Clone()).ChildExpressions);
        }

        public static StringExpression ToNChar(this Int32Expression expr)
        {
            return new StringExpression(false, "NCHAR(" + expr.Sql + ")", ((Int32Expression)expr.Clone()).ChildExpressions);
        }

        #endregion

        #region DateTime Expression

        public static DateTimeExpression AddDay(this DateTimeExpression expr, int n)
        {
            var newExpr = (DateTimeExpression)expr.Clone();

            newExpr.Sql = "DATEADD(day, ?, " + newExpr.Sql + ")";
            newExpr.ChildExpressions.Insert(0, new Int32ParameterExpression(n));

            return newExpr;
        }

        public static DateTimeExpression AddDay(this DateTimeExpression expr, Int32Expression n)
        {
            if (n == null)
                throw new ArgumentNullException("n");

            var newExpr = (DateTimeExpression)expr.Clone();

            newExpr.Sql = "DATEADD(day, ?, " + newExpr.Sql + ")";
            newExpr.ChildExpressions.Insert(0, n);

            return newExpr;
        }

        public static DateTimeExpression AddMonth(this DateTimeExpression expr, int n)
        {
            var newExpr = (DateTimeExpression)expr.Clone();

            newExpr.Sql = "DATEADD(month, ?, " + newExpr.Sql + ")";
            newExpr.ChildExpressions.Insert(0, new Int32ParameterExpression(n));

            return newExpr;
        }

        public static DateTimeExpression AddMonth(this DateTimeExpression expr, Int32Expression n)
        {
            if (n == null)
                throw new ArgumentNullException("n");

            var newExpr = (DateTimeExpression)expr.Clone();

            newExpr.Sql = "DATEADD(month, ?, " + newExpr.Sql + ")";
            newExpr.ChildExpressions.Insert(0, n);

            return newExpr;
        }

        public static DateTimeExpression AddYear(this DateTimeExpression expr, int n)
        {
            var newExpr = (DateTimeExpression)expr.Clone();

            newExpr.Sql = "DATEADD(year, ?, " + newExpr.Sql + ")";
            newExpr.ChildExpressions.Insert(0, new Int32ParameterExpression(n));

            return newExpr;
        }

        public static DateTimeExpression AddYear(this DateTimeExpression expr, Int32Expression n)
        {
            if (n == null)
                throw new ArgumentNullException("n");

            var newExpr = (DateTimeExpression)expr.Clone();

            newExpr.Sql = "DATEADD(year, ?, " + newExpr.Sql + ")";
            newExpr.ChildExpressions.Insert(0, n);

            return newExpr;
        }

        public static DateTimeExpression AddHour(this DateTimeExpression expr, int n)
        {
            var newExpr = (DateTimeExpression)expr.Clone();

            newExpr.Sql = "DATEADD(hour, ?, " + newExpr.Sql + ")";
            newExpr.ChildExpressions.Insert(0, new Int32ParameterExpression(n));

            return newExpr;
        }

        public static DateTimeExpression AddHour(this DateTimeExpression expr, Int32Expression n)
        {
            if (n == null)
                throw new ArgumentNullException("n");

            var newExpr = (DateTimeExpression)expr.Clone();

            newExpr.Sql = "DATEADD(hour, ?, " + newExpr.Sql + ")";
            newExpr.ChildExpressions.Insert(0, n);

            return newExpr;
        }

        public static DateTimeExpression AddMinute(this DateTimeExpression expr, int n)
        {
            var newExpr = (DateTimeExpression)expr.Clone();

            newExpr.Sql = "DATEADD(minute, ?, " + newExpr.Sql + ")";
            newExpr.ChildExpressions.Insert(0, new Int32ParameterExpression(n));

            return newExpr;
        }

        public static DateTimeExpression AddMinute(this DateTimeExpression expr, Int32Expression n)
        {
            if (n == null)
                throw new ArgumentNullException("n");

            var newExpr = (DateTimeExpression)expr.Clone();

            newExpr.Sql = "DATEADD(minute, ?, " + newExpr.Sql + ")";
            newExpr.ChildExpressions.Insert(0, n);

            return newExpr;
        }

        public static DateTimeExpression AddSecond(this DateTimeExpression expr, int n)
        {
            var newExpr = (DateTimeExpression)expr.Clone();

            newExpr.Sql = "DATEADD(second, ?, " + newExpr.Sql + ")";
            newExpr.ChildExpressions.Insert(0, new Int32ParameterExpression(n));

            return newExpr;
        }

        public static DateTimeExpression AddSecond(this DateTimeExpression expr, Int32Expression n)
        {
            if (n == null)
                throw new ArgumentNullException("n");

            var newExpr = (DateTimeExpression)expr.Clone();

            newExpr.Sql = "DATEADD(second, ?, " + newExpr.Sql + ")";
            newExpr.ChildExpressions.Insert(0, n);

            return newExpr;
        }

        public static Int32Expression GetDay(this DateTimeExpression expr)
        {
            return new Int32Expression("DATEPART(day, " + expr.Sql + ")", ((DateTimeExpression)expr.Clone()).ChildExpressions);
        }

        public static Int32Expression GetMonth(this DateTimeExpression expr)
        {
            return new Int32Expression("DATEPART(month, " + expr.Sql + ")", ((DateTimeExpression)expr.Clone()).ChildExpressions);
        }

        public static Int32Expression GetYear(this DateTimeExpression expr)
        {
            return new Int32Expression("DATEPART(year, " + expr.Sql + ")", ((DateTimeExpression)expr.Clone()).ChildExpressions);
        }


        public static Int32Expression GetHour(this DateTimeExpression expr)
        {
            return new Int32Expression("DATEPART(hour, " + expr.Sql + ")", ((DateTimeExpression)expr.Clone()).ChildExpressions);
        }


        public static Int32Expression GetMinute(this DateTimeExpression expr)
        {
            return new Int32Expression("DATEPART(minute, " + expr.Sql + ")", ((DateTimeExpression)expr.Clone()).ChildExpressions);
        }


        public static Int32Expression GetSecond(this DateTimeExpression expr)
        {
            return new Int32Expression("DATEPART(second, " + expr.Sql + ")", ((DateTimeExpression)expr.Clone()).ChildExpressions);
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
            var newExpr = new Int32Expression("CHARINDEX(?, " + expr.Sql + ") - 1", ((StringExpression)expr.Clone()).ChildExpressions);

            newExpr.ChildExpressions.Insert(0, new StringParameterExpression(value, expr.IsUnicode));

            return newExpr;
        }

        public static Int32Expression IndexOf(this StringExpression expr, StringExpression value)
        {
            var newExpr = new Int32Expression("CHARINDEX(?, " + expr.Sql + ") - 1", ((StringExpression)expr.Clone()).ChildExpressions);

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

            newExpr.Sql = "SUBSTRING(" + newExpr.Sql + ", ? + 1, ?)";
            newExpr.ChildExpressions.Add(new Int32ParameterExpression(begin));
            newExpr.ChildExpressions.Add(new Int32ParameterExpression(length));

            return newExpr;
        }

        public static StringExpression Substring(this StringExpression expr, Int32Expression begin, int length)
        {
            if (ReferenceEquals(begin, null))
                throw new ArgumentNullException("begin");

            var newExpr = (StringExpression)expr.Clone();

            newExpr.Sql = "SUBSTRING(" + newExpr.Sql + ", ? + 1, ?)";
            newExpr.ChildExpressions.Add(begin);
            newExpr.ChildExpressions.Add(new Int32ParameterExpression(length));

            return newExpr;
        }

        public static StringExpression Substring(this StringExpression expr, int begin, Int32Expression length)
        {
            if (ReferenceEquals(length, null))
                throw new ArgumentNullException("length");

            var newExpr = (StringExpression)expr.Clone();

            newExpr.Sql = "SUBSTRING(" + newExpr.Sql + ", ? + 1, ?)";
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

            newExpr.Sql = "SUBSTRING(" + newExpr.Sql + ", ? + 1, ?)";
            newExpr.ChildExpressions.Add(begin);
            newExpr.ChildExpressions.Add(length);

            return newExpr;
        }

        public static StringExpression Left(this StringExpression expr, int length)
        {
            var newExpr = (StringExpression)expr.Clone();

            newExpr.Sql = "LEFT(" + newExpr.Sql + ", ?)";
            newExpr.ChildExpressions.Add(new Int32ParameterExpression(length));

            return newExpr;
        }

        public static StringExpression Left(this StringExpression expr, Int32Expression length)
        {
            if (ReferenceEquals(length, null))
                throw new ArgumentNullException("length");

            var newExpr = (StringExpression)expr.Clone();

            newExpr.Sql = "LEFT(" + newExpr.Sql + ", ?)";
            newExpr.ChildExpressions.Add(length);

            return newExpr;
        }

        public static StringExpression Right(this StringExpression expr, int length)
        {
            var newExpr = (StringExpression)expr.Clone();

            newExpr.Sql = "RIGHT(" + newExpr.Sql + ", ?)";
            newExpr.ChildExpressions.Add(new Int32ParameterExpression(length));

            return newExpr;
        }

        public static StringExpression Right(this StringExpression expr, Int32Expression length)
        {
            if (ReferenceEquals(length, null))
                throw new ArgumentNullException("length");

            var newExpr = (StringExpression)expr.Clone();

            newExpr.Sql = "RIGHT(" + newExpr.Sql + ", ?)";
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

        public static Int32Expression ToUnicode(this StringExpression expr)
        {
            return new Int32Expression("UNICODE(" + expr.Sql + ")", ((StringExpression)expr.Clone()).ChildExpressions);
        }

        public static Int32Expression GetLength(this StringExpression expr)
        {
            return new Int32Expression("LEN(" + expr.Sql + ")", ((StringExpression)expr.Clone()).ChildExpressions);
        }

        #endregion

        #endregion
    }
}
