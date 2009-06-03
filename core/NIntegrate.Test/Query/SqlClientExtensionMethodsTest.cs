using Microsoft.VisualStudio.TestTools.UnitTesting;
using NIntegrate.Query;
using NIntegrate.Query.SqlClient;
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
            var criteria = new TestCriteria();
            var en = criteria.AddSortByRandom().SortBys.Keys.GetEnumerator();
            en.MoveNext();
            Assert.AreEqual("newid()", en.Current.ColumnName);

            Assert.AreEqual("getdate()", criteria.GetCurrentDate()._sql);
            Assert.AreEqual("getutcdate()", criteria.GetCurrentUtcDate()._sql);
        }

        [TestMethod]
        public void TestSqlClientInt32ExpressionExtenstionMethods()
        {
            var criteria = new TestCriteria();
            Assert.AreEqual("CHAR([Int32Column])", QueryHelper.ToExpressionCacheableSql(criteria.Int32Column.ToChar()));
            Assert.AreEqual("NCHAR([Int32Column])", QueryHelper.ToExpressionCacheableSql(criteria.Int32Column.ToNChar()));
        }

        [TestMethod]
        public void TestSqlClientDateTimeExpressionExtenstionMethods()
        {
            var criteria = new TestCriteria();
            Assert.AreEqual("DATEADD(day, ?, [DateTimeColumn])", QueryHelper.ToExpressionCacheableSql(criteria.DateTimeColumn.AddDay(1)));
            Assert.AreEqual("DATEADD(day, ?, [DateTimeColumn])", QueryHelper.ToExpressionCacheableSql(criteria.DateTimeColumn.AddDay(new Int32ParameterExpression(1))));
            Assert.AreEqual("DATEADD(month, ?, [DateTimeColumn])", QueryHelper.ToExpressionCacheableSql(criteria.DateTimeColumn.AddMonth(1)));
            Assert.AreEqual("DATEADD(month, ?, [DateTimeColumn])", QueryHelper.ToExpressionCacheableSql(criteria.DateTimeColumn.AddMonth(new Int32ParameterExpression(1))));
            Assert.AreEqual("DATEADD(year, ?, [DateTimeColumn])", QueryHelper.ToExpressionCacheableSql(criteria.DateTimeColumn.AddYear(1)));
            Assert.AreEqual("DATEADD(year, ?, [DateTimeColumn])", QueryHelper.ToExpressionCacheableSql(criteria.DateTimeColumn.AddYear(new Int32ParameterExpression(1))));
            Assert.AreEqual("DATEADD(hour, ?, [DateTimeColumn])", QueryHelper.ToExpressionCacheableSql(criteria.DateTimeColumn.AddHour(1)));
            Assert.AreEqual("DATEADD(hour, ?, [DateTimeColumn])", QueryHelper.ToExpressionCacheableSql(criteria.DateTimeColumn.AddHour(new Int32ParameterExpression(1))));
            Assert.AreEqual("DATEADD(minute, ?, [DateTimeColumn])", QueryHelper.ToExpressionCacheableSql(criteria.DateTimeColumn.AddMinute(1)));
            Assert.AreEqual("DATEADD(minute, ?, [DateTimeColumn])", QueryHelper.ToExpressionCacheableSql(criteria.DateTimeColumn.AddMinute(new Int32ParameterExpression(1))));
            Assert.AreEqual("DATEADD(second, ?, [DateTimeColumn])", QueryHelper.ToExpressionCacheableSql(criteria.DateTimeColumn.AddSecond(1)));
            Assert.AreEqual("DATEADD(second, ?, [DateTimeColumn])", QueryHelper.ToExpressionCacheableSql(criteria.DateTimeColumn.AddSecond(new Int32ParameterExpression(1))));

            Assert.AreEqual("DATEPART(day, [DateTimeColumn])", QueryHelper.ToExpressionCacheableSql(criteria.DateTimeColumn.GetDay()));
            Assert.AreEqual("DATEPART(month, [DateTimeColumn])", QueryHelper.ToExpressionCacheableSql(criteria.DateTimeColumn.GetMonth()));
            Assert.AreEqual("DATEPART(year, [DateTimeColumn])", QueryHelper.ToExpressionCacheableSql(criteria.DateTimeColumn.GetYear()));
            Assert.AreEqual("DATEPART(hour, [DateTimeColumn])", QueryHelper.ToExpressionCacheableSql(criteria.DateTimeColumn.GetHour()));
            Assert.AreEqual("DATEPART(minute, [DateTimeColumn])", QueryHelper.ToExpressionCacheableSql(criteria.DateTimeColumn.GetMinute()));
            Assert.AreEqual("DATEPART(second, [DateTimeColumn])", QueryHelper.ToExpressionCacheableSql(criteria.DateTimeColumn.GetSecond()));
        }

        [TestMethod]
        public void TestSqlClientStringExpressionExtenstionMethods()
        {
            var criteria = new TestCriteria();
            Assert.AreEqual("[StringColumn] LIKE '%' + ? + '%'", QueryHelper.ToConditionCacheableSql(criteria.StringColumn.Contains("a")));
            Assert.AreEqual("[StringColumn] LIKE ? + '%'", QueryHelper.ToConditionCacheableSql(criteria.StringColumn.StartsWith("a")));
            Assert.AreEqual("[StringColumn] LIKE '%' + ?", QueryHelper.ToConditionCacheableSql(criteria.StringColumn.EndsWith("a")));
            Assert.AreEqual("CHARINDEX(?, [StringColumn]) - 1", QueryHelper.ToExpressionCacheableSql(criteria.StringColumn.IndexOf("a")));
            Assert.AreEqual("CHARINDEX(?, [StringColumn]) - 1", QueryHelper.ToExpressionCacheableSql(criteria.StringColumn.IndexOf(new StringParameterExpression("a", true))));
            Assert.AreEqual("REPLACE([StringColumn], ?, ?)", QueryHelper.ToExpressionCacheableSql(criteria.StringColumn.Replace("a", "b")));
            Assert.AreEqual("REPLACE([StringColumn], ?, ?)", QueryHelper.ToExpressionCacheableSql(criteria.StringColumn.Replace("a", new StringParameterExpression("b", true))));
            Assert.AreEqual("REPLACE([StringColumn], ?, ?)", QueryHelper.ToExpressionCacheableSql(criteria.StringColumn.Replace(new StringParameterExpression("a", true), "b")));
            Assert.AreEqual("REPLACE([StringColumn], ?, ?)", QueryHelper.ToExpressionCacheableSql(criteria.StringColumn.Replace(new StringParameterExpression("a", true), new StringParameterExpression("b", true))));
            Assert.AreEqual("SUBSTRING([StringColumn], ? + 1, ?)", QueryHelper.ToExpressionCacheableSql(criteria.StringColumn.Substring(1, 1)));
            Assert.AreEqual("SUBSTRING([StringColumn], ? + 1, ?)", QueryHelper.ToExpressionCacheableSql(criteria.StringColumn.Substring(1, new Int32ParameterExpression(1))));
            Assert.AreEqual("SUBSTRING([StringColumn], ? + 1, ?)", QueryHelper.ToExpressionCacheableSql(criteria.StringColumn.Substring(new Int32ParameterExpression(1), 1)));
            Assert.AreEqual("SUBSTRING([StringColumn], ? + 1, ?)", QueryHelper.ToExpressionCacheableSql(criteria.StringColumn.Substring(new Int32ParameterExpression(1), new Int32ParameterExpression(1))));
            Assert.AreEqual("LEFT([StringColumn], ?)", QueryHelper.ToExpressionCacheableSql(criteria.StringColumn.Left(new Int32ParameterExpression(1))));
            Assert.AreEqual("LEFT([StringColumn], ?)", QueryHelper.ToExpressionCacheableSql(criteria.StringColumn.Left(1)));
            Assert.AreEqual("RIGHT([StringColumn], ?)", QueryHelper.ToExpressionCacheableSql(criteria.StringColumn.Right(new Int32ParameterExpression(1))));
            Assert.AreEqual("RIGHT([StringColumn], ?)", QueryHelper.ToExpressionCacheableSql(criteria.StringColumn.Right(1)));
            Assert.AreEqual("LTRIM([StringColumn])", QueryHelper.ToExpressionCacheableSql(criteria.StringColumn.LTrim()));
            Assert.AreEqual("RTRIM([StringColumn])", QueryHelper.ToExpressionCacheableSql(criteria.StringColumn.RTrim()));
            Assert.AreEqual("ASCII([StringColumn])", QueryHelper.ToExpressionCacheableSql(criteria.StringColumn.ToAscii()));
            Assert.AreEqual("UNICODE([StringColumn])", QueryHelper.ToExpressionCacheableSql(criteria.StringColumn.ToUnicode()));
            Assert.AreEqual("LEN([StringColumn])", QueryHelper.ToExpressionCacheableSql(criteria.StringColumn.GetLength()));
        }
    }
}
