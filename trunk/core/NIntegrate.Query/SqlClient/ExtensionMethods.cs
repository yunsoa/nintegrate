using System;

namespace NBear.Query.SqlClient
{
    public static class ExtensionMethods
    {
        #region Criteria

        public static Criteria AddSortByRandom(this Criteria criteria)
        {
            criteria._sortBys.Add(new GuidColumn(new GuidExpression("newid()", null), "newid()"), false);

            return criteria;
        }

        #endregion

        #region Expression

        #region Int32 Expression

        public static StringExpression ToChar(this Int32Expression expr)
        {
            return new StringExpression(false, "CHAR(" + expr._sql + ")", ((Int32Expression)expr.Clone())._childExpressions);
        }

        public static StringExpression ToNChar(this Int32Expression expr)
        {
            return new StringExpression(false, "NCHAR(" + expr._sql + ")", ((Int32Expression)expr.Clone())._childExpressions);
        }

        #endregion

        #region DateTime Expression

        public static DateTimeExpression AddDay(this DateTimeExpression expr, int n)
        {
            var newExpr = (DateTimeExpression)expr.Clone();

            newExpr._sql = "DATEADD(day, ?, " + newExpr._sql + ")";
            newExpr._childExpressions.Insert(0, new Int32ParameterExpression(n));

            return expr;
        }

        public static DateTimeExpression AddDay(this DateTimeExpression expr, Int32Expression n)
        {
            if (n == null)
                throw new ArgumentNullException("n");

            var newExpr = (DateTimeExpression)expr.Clone();

            newExpr._sql = "DATEADD(day, ?, " + newExpr._sql + ")";
            newExpr._childExpressions.Insert(0, n);

            return expr;
        }

        public static DateTimeExpression AddMonth(this DateTimeExpression expr, int n)
        {
            var newExpr = (DateTimeExpression)expr.Clone();

            newExpr._sql = "DATEADD(month, ?, " + newExpr._sql + ")";
            newExpr._childExpressions.Insert(0, new Int32ParameterExpression(n));

            return expr;
        }

        public static DateTimeExpression AddMonth(this DateTimeExpression expr, Int32Expression n)
        {
            if (n == null)
                throw new ArgumentNullException("n");

            var newExpr = (DateTimeExpression)expr.Clone();

            newExpr._sql = "DATEADD(month, ?, " + newExpr._sql + ")";
            newExpr._childExpressions.Insert(0, n);

            return expr;
        }

        public static DateTimeExpression AddYear(this DateTimeExpression expr, int n)
        {
            var newExpr = (DateTimeExpression)expr.Clone();

            newExpr._sql = "DATEADD(year, ?, " + newExpr._sql + ")";
            newExpr._childExpressions.Insert(0, new Int32ParameterExpression(n));

            return expr;
        }

        public static DateTimeExpression AddYear(this DateTimeExpression expr, Int32Expression n)
        {
            if (n == null)
                throw new ArgumentNullException("n");

            var newExpr = (DateTimeExpression)expr.Clone();

            newExpr._sql = "DATEADD(year, ?, " + newExpr._sql + ")";
            newExpr._childExpressions.Insert(0, n);

            return expr;
        }

        public static DateTimeExpression AddHour(this DateTimeExpression expr, int n)
        {
            var newExpr = (DateTimeExpression)expr.Clone();

            newExpr._sql = "DATEADD(hour, ?, " + newExpr._sql + ")";
            newExpr._childExpressions.Insert(0, new Int32ParameterExpression(n));

            return expr;
        }

        public static DateTimeExpression AddHour(this DateTimeExpression expr, Int32Expression n)
        {
            if (n == null)
                throw new ArgumentNullException("n");

            var newExpr = (DateTimeExpression)expr.Clone();

            newExpr._sql = "DATEADD(hour, ?, " + newExpr._sql + ")";
            newExpr._childExpressions.Insert(0, n);

            return expr;
        }

        public static DateTimeExpression AddMinute(this DateTimeExpression expr, int n)
        {
            var newExpr = (DateTimeExpression)expr.Clone();

            newExpr._sql = "DATEADD(minute, ?, " + newExpr._sql + ")";
            newExpr._childExpressions.Insert(0, new Int32ParameterExpression(n));

            return expr;
        }

        public static DateTimeExpression AddMinute(this DateTimeExpression expr, Int32Expression n)
        {
            if (n == null)
                throw new ArgumentNullException("n");

            var newExpr = (DateTimeExpression)expr.Clone();

            newExpr._sql = "DATEADD(minute, ?, " + newExpr._sql + ")";
            newExpr._childExpressions.Insert(0, n);

            return expr;
        }

        public static DateTimeExpression AddSecond(this DateTimeExpression expr, int n)
        {
            var newExpr = (DateTimeExpression)expr.Clone();

            newExpr._sql = "DATEADD(second, ?, " + newExpr._sql + ")";
            newExpr._childExpressions.Insert(0, new Int32ParameterExpression(n));

            return expr;
        }

        public static DateTimeExpression AddSecond(this DateTimeExpression expr, Int32Expression n)
        {
            if (n == null)
                throw new ArgumentNullException("n");

            var newExpr = (DateTimeExpression)expr.Clone();

            newExpr._sql = "DATEADD(second, ?, " + newExpr._sql + ")";
            newExpr._childExpressions.Insert(0, n);

            return expr;
        }

        public static Int32Expression GetDay(this DateTimeExpression expr)
        {
            return new Int32Expression("DATEPART(day, " + expr._sql + ")", ((DateTimeExpression)expr.Clone())._childExpressions);
        }

        public static Int32Expression GetMonth(this DateTimeExpression expr)
        {
            return new Int32Expression("DATEPART(month, " + expr._sql + ")", ((DateTimeExpression)expr.Clone())._childExpressions);
        }

        public static Int32Expression GetYear(this DateTimeExpression expr)
        {
            return new Int32Expression("DATEPART(year, " + expr._sql + ")", ((DateTimeExpression)expr.Clone())._childExpressions);
        }


        public static Int32Expression GetHour(this DateTimeExpression expr)
        {
            return new Int32Expression("DATEPART(hour, " + expr._sql + ")", ((DateTimeExpression)expr.Clone())._childExpressions);
        }


        public static Int32Expression GetMinute(this DateTimeExpression expr)
        {
            return new Int32Expression("DATEPART(minute, " + expr._sql + ")", ((DateTimeExpression)expr.Clone())._childExpressions);
        }


        public static Int32Expression GetSecond(this DateTimeExpression expr)
        {
            return new Int32Expression("DATEPART(second, " + expr._sql + ")", ((DateTimeExpression)expr.Clone())._childExpressions);
        }


        #endregion

        #region String Expression

        public static Int32Expression IndexOf(this StringExpression expr, string value)
        {
            var newExpr = new Int32Expression("CHARINDEX(?, " + expr._sql + ") - 1", ((StringExpression)expr.Clone())._childExpressions);

            newExpr.ChildExpressions.Insert(0, new StringParameterExpression(value, expr.IsUnicode));

            return newExpr;
        }

        public static Int32Expression IndexOf(this StringExpression expr, StringExpression value)
        {
            var newExpr = new Int32Expression("CHARINDEX(?, " + expr._sql + ") - 1", ((StringExpression)expr.Clone())._childExpressions);

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

            return expr;
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

            return expr;
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

            return expr;
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

            return expr;
        }

        public static StringExpression Substring(this StringExpression expr, int begin, int length)
        {
            var newExpr = (StringExpression)expr.Clone();

            newExpr._sql = "SUBSTRING(" + newExpr._sql + ", ? + 1, ?)";
            newExpr._childExpressions.Add(new Int32ParameterExpression(begin));
            newExpr._childExpressions.Add(new Int32ParameterExpression(length));

            return expr;
        }

        public static StringExpression Substring(this StringExpression expr, Int32Expression begin, int length)
        {
            if (ReferenceEquals(begin, null))
                throw new ArgumentNullException("begin");

            var newExpr = (StringExpression)expr.Clone();

            newExpr._sql = "SUBSTRING(" + newExpr._sql + ", ? + 1, ?)";
            newExpr._childExpressions.Add(begin);
            newExpr._childExpressions.Add(new Int32ParameterExpression(length));

            return expr;
        }

        public static StringExpression Substring(this StringExpression expr, int begin, StringExpression length)
        {
            if (ReferenceEquals(length, null))
                throw new ArgumentNullException("length");

            var newExpr = (StringExpression)expr.Clone();

            newExpr._sql = "SUBSTRING(" + newExpr._sql + ", ? + 1, ?)";
            newExpr._childExpressions.Add(new Int32ParameterExpression(begin));
            newExpr._childExpressions.Add(length);

            return expr;
        }

        public static StringExpression Substring(this StringExpression expr, StringExpression begin, StringExpression length)
        {
            if (ReferenceEquals(begin, null))
                throw new ArgumentNullException("begin");
            if (ReferenceEquals(length, null))
                throw new ArgumentNullException("length");

            var newExpr = (StringExpression)expr.Clone();

            newExpr._sql = "SUBSTRING(" + newExpr._sql + ", ? + 1, ?)";
            newExpr._childExpressions.Add(begin);
            newExpr._childExpressions.Add(length);

            return expr;
        }

        public static StringExpression Left(this StringExpression expr, int length)
        {
            var newExpr = (StringExpression)expr.Clone();

            newExpr._sql = "LEFT(" + newExpr._sql + ", ?)";
            newExpr._childExpressions.Add(new Int32ParameterExpression(length));

            return expr;
        }

        public static StringExpression Left(this StringExpression expr, Int32Expression length)
        {
            if (ReferenceEquals(length, null))
                throw new ArgumentNullException("length");

            var newExpr = (StringExpression)expr.Clone();

            newExpr._sql = "LEFT(" + newExpr._sql + ", ?)";
            newExpr._childExpressions.Add(length);

            return expr;
        }

        public static StringExpression Right(this StringExpression expr, int length)
        {
            var newExpr = (StringExpression)expr.Clone();

            newExpr._sql = "RIGHT(" + newExpr._sql + ", ?)";
            newExpr._childExpressions.Add(new Int32ParameterExpression(length));

            return expr;
        }

        public static StringExpression Right(this StringExpression expr, Int32Expression length)
        {
            if (ReferenceEquals(length, null))
                throw new ArgumentNullException("length");

            var newExpr = (StringExpression)expr.Clone();

            newExpr._sql = "RIGHT(" + newExpr._sql + ", ?)";
            newExpr._childExpressions.Add(length);

            return expr;
        }

        public static StringExpression LTrim(this StringExpression expr)
        {
            var newExpr = (StringExpression)expr.Clone();

            newExpr._sql = "LTRIM(" + expr._sql + ")";

            return expr;
        }

        public static StringExpression RTrim(this StringExpression expr)
        {
            var newExpr = (StringExpression)expr.Clone();

            newExpr._sql = "RTRIM(" + expr._sql + ")";

            return expr;
        }

        public static Int32Expression ToAscii(this StringExpression expr)
        {
            return new Int32Expression("ASCII(" + expr._sql + ")", ((StringExpression)expr.Clone())._childExpressions);
        }

        public static Int32Expression ToUnicode(this StringExpression expr)
        {
            return new Int32Expression("UNICODE(" + expr._sql + ")", ((StringExpression)expr.Clone())._childExpressions);
        }

        public static Int32Expression GetLength(this StringExpression expr)
        {
            return new Int32Expression("LEN(" + expr._sql + ")", ((StringExpression)expr.Clone())._childExpressions);
        }

        #endregion

        #endregion
    }
}
