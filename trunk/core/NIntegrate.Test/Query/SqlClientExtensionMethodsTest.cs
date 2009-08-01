using Microsoft.VisualStudio.TestTools.UnitTesting;
using NIntegrate.Data;
using NIntegrate.Data.SqlClient;
using NIntegrate.Test.Query.TestClasses;

namespace NIntegrate.Test.Query
{
    /// <summary>
    /// Test SqlClient Extension Methods
    /// </summary>
    [TestClass]
    public class SqlClientExtensionMethodsTest
    {
        [TestMethod]
        public void TestSqlClientCriteriaExtensionMethods()
        {
            var table = new TestTable();
            var criteria = table.CreateCriteria();
            var en = criteria.AddSortByRandom().SortBys.Keys.GetEnumerator();
            en.MoveNext();
            Assert.AreEqual("newid()", en.Current.ColumnName);

            Assert.AreEqual("getdate()", criteria.GetCurrentDate().Sql);
            Assert.AreEqual("getutcdate()", criteria.GetCurrentUtcDate().Sql);
        }

        [TestMethod]
        public void TestSqlClientInt32ExpressionExtenstionMethods()
        {
            var criteria = new TestTable();
            Assert.AreEqual("CHAR([Int32Column])", criteria.Int32Column.ToChar().ToExpressionCacheableSql());
            Assert.AreEqual("NCHAR([Int32Column])", criteria.Int32Column.ToNChar().ToExpressionCacheableSql());
        }

        [TestMethod]
        public void TestSqlClientDateTimeExpressionExtenstionMethods()
        {
            var criteria = new TestTable();
            Assert.AreEqual("DATEADD(day, ?, [DateTimeColumn])", criteria.DateTimeColumn.AddDay(1).ToExpressionCacheableSql());
            Assert.AreEqual("DATEADD(day, ?, [DateTimeColumn])", criteria.DateTimeColumn.AddDay(new Int32ParameterExpression(1)).ToExpressionCacheableSql());
            Assert.AreEqual("DATEADD(month, ?, [DateTimeColumn])", criteria.DateTimeColumn.AddMonth(1).ToExpressionCacheableSql());
            Assert.AreEqual("DATEADD(month, ?, [DateTimeColumn])", criteria.DateTimeColumn.AddMonth(new Int32ParameterExpression(1)).ToExpressionCacheableSql());
            Assert.AreEqual("DATEADD(year, ?, [DateTimeColumn])", criteria.DateTimeColumn.AddYear(1).ToExpressionCacheableSql());
            Assert.AreEqual("DATEADD(year, ?, [DateTimeColumn])", criteria.DateTimeColumn.AddYear(new Int32ParameterExpression(1)).ToExpressionCacheableSql());
            Assert.AreEqual("DATEADD(hour, ?, [DateTimeColumn])", criteria.DateTimeColumn.AddHour(1).ToExpressionCacheableSql());
            Assert.AreEqual("DATEADD(hour, ?, [DateTimeColumn])", criteria.DateTimeColumn.AddHour(new Int32ParameterExpression(1)).ToExpressionCacheableSql());
            Assert.AreEqual("DATEADD(minute, ?, [DateTimeColumn])", criteria.DateTimeColumn.AddMinute(1).ToExpressionCacheableSql());
            Assert.AreEqual("DATEADD(minute, ?, [DateTimeColumn])", criteria.DateTimeColumn.AddMinute(new Int32ParameterExpression(1)).ToExpressionCacheableSql());
            Assert.AreEqual("DATEADD(second, ?, [DateTimeColumn])", criteria.DateTimeColumn.AddSecond(1).ToExpressionCacheableSql());
            Assert.AreEqual("DATEADD(second, ?, [DateTimeColumn])", criteria.DateTimeColumn.AddSecond(new Int32ParameterExpression(1)).ToExpressionCacheableSql());

            Assert.AreEqual("DATEPART(day, [DateTimeColumn])", criteria.DateTimeColumn.GetDay().ToExpressionCacheableSql());
            Assert.AreEqual("DATEPART(month, [DateTimeColumn])", criteria.DateTimeColumn.GetMonth().ToExpressionCacheableSql());
            Assert.AreEqual("DATEPART(year, [DateTimeColumn])", criteria.DateTimeColumn.GetYear().ToExpressionCacheableSql());
            Assert.AreEqual("DATEPART(hour, [DateTimeColumn])", criteria.DateTimeColumn.GetHour().ToExpressionCacheableSql());
            Assert.AreEqual("DATEPART(minute, [DateTimeColumn])", criteria.DateTimeColumn.GetMinute().ToExpressionCacheableSql());
            Assert.AreEqual("DATEPART(second, [DateTimeColumn])", criteria.DateTimeColumn.GetSecond().ToExpressionCacheableSql());
        }

        [TestMethod]
        public void TestSqlClientStringExpressionExtenstionMethods()
        {
            var criteria = new TestTable();
            Assert.AreEqual("[StringColumn] LIKE '%' + ? + '%'", criteria.StringColumn.Contains("a").ToConditionCacheableSql());
            Assert.AreEqual("[StringColumn] LIKE ? + '%'", criteria.StringColumn.StartsWith("a").ToConditionCacheableSql());
            Assert.AreEqual("[StringColumn] LIKE '%' + ?", criteria.StringColumn.EndsWith("a").ToConditionCacheableSql());
            Assert.AreEqual("CHARINDEX(?, [StringColumn]) - 1", criteria.StringColumn.IndexOf("a").ToExpressionCacheableSql());
            Assert.AreEqual("CHARINDEX(?, [StringColumn]) - 1", criteria.StringColumn.IndexOf(new StringParameterExpression("a", true)).ToExpressionCacheableSql());
            Assert.AreEqual("REPLACE([StringColumn], ?, ?)", criteria.StringColumn.Replace("a", "b").ToExpressionCacheableSql());
            Assert.AreEqual("REPLACE([StringColumn], ?, ?)", criteria.StringColumn.Replace("a", new StringParameterExpression("b", true)).ToExpressionCacheableSql());
            Assert.AreEqual("REPLACE([StringColumn], ?, ?)", criteria.StringColumn.Replace(new StringParameterExpression("a", true), "b").ToExpressionCacheableSql());
            Assert.AreEqual("REPLACE([StringColumn], ?, ?)", criteria.StringColumn.Replace(new StringParameterExpression("a", true), new StringParameterExpression("b", true)).ToExpressionCacheableSql());
            Assert.AreEqual("SUBSTRING([StringColumn], ? + 1, ?)", criteria.StringColumn.Substring(1, 1).ToExpressionCacheableSql());
            Assert.AreEqual("SUBSTRING([StringColumn], ? + 1, ?)", criteria.StringColumn.Substring(1, new Int32ParameterExpression(1)).ToExpressionCacheableSql());
            Assert.AreEqual("SUBSTRING([StringColumn], ? + 1, ?)", criteria.StringColumn.Substring(new Int32ParameterExpression(1), 1).ToExpressionCacheableSql());
            Assert.AreEqual("SUBSTRING([StringColumn], ? + 1, ?)", criteria.StringColumn.Substring(new Int32ParameterExpression(1), new Int32ParameterExpression(1)).ToExpressionCacheableSql());
            Assert.AreEqual("LEFT([StringColumn], ?)", criteria.StringColumn.Left(new Int32ParameterExpression(1)).ToExpressionCacheableSql());
            Assert.AreEqual("LEFT([StringColumn], ?)", criteria.StringColumn.Left(1).ToExpressionCacheableSql());
            Assert.AreEqual("RIGHT([StringColumn], ?)", criteria.StringColumn.Right(new Int32ParameterExpression(1)).ToExpressionCacheableSql());
            Assert.AreEqual("RIGHT([StringColumn], ?)", criteria.StringColumn.Right(1).ToExpressionCacheableSql());
            Assert.AreEqual("LTRIM([StringColumn])", criteria.StringColumn.LTrim().ToExpressionCacheableSql());
            Assert.AreEqual("RTRIM([StringColumn])", criteria.StringColumn.RTrim().ToExpressionCacheableSql());
            Assert.AreEqual("ASCII([StringColumn])", criteria.StringColumn.ToAscii().ToExpressionCacheableSql());
            Assert.AreEqual("UNICODE([StringColumn])", criteria.StringColumn.ToUnicode().ToExpressionCacheableSql());
            Assert.AreEqual("LEN([StringColumn])", criteria.StringColumn.GetLength().ToExpressionCacheableSql());
        }
    }
}
