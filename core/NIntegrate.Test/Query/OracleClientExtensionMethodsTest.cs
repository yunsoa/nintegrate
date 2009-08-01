using Microsoft.VisualStudio.TestTools.UnitTesting;
using NIntegrate.Data;
using NIntegrate.Data.OracleClient;
using NIntegrate.Test.Query.TestClasses;

namespace NIntegrate.Test.Query
{
    /// <summary>
    /// Test OracleClient Extension Methods
    /// </summary>
    [TestClass]
    public class OracleClientExtensionMethodsTest
    {
        [TestMethod]
        public void TestOracleClientCriteriaExtensionMethods()
        {
            var table = new TestTable();
            var criteria = table.CreateCriteria();
            Assert.AreEqual("CURRENT_TIMESTAMP", criteria.GetCurrentDate().Sql);
        }

        [TestMethod]
        public void TestOracleClientInt32ExpressionExtenstionMethods()
        {
            var table = new TestTable();
            Assert.AreEqual("CHR([Int32Column])", table.Int32Column.ToChar().ToExpressionCacheableSql());
        }

        [TestMethod]
        public void TestOracleClientDateTimeExpressionExtenstionMethods()
        {
            var table = new TestTable();
            Assert.AreEqual("ADD_MONTHS(month, ?, [DateTimeColumn])", table.DateTimeColumn.AddMonth(1).ToExpressionCacheableSql());
            Assert.AreEqual("ADD_MONTHS(month, ?, [DateTimeColumn])", table.DateTimeColumn.AddMonth(new Int32ParameterExpression(1)).ToExpressionCacheableSql());
        }

        [TestMethod]
        public void TestOracleClientStringExpressionExtenstionMethods()
        {
            var table = new TestTable();
            Assert.AreEqual("[StringColumn] LIKE '%' + ? + '%'", table.StringColumn.Contains("a").ToConditionCacheableSql());
            Assert.AreEqual("[StringColumn] LIKE ? + '%'", table.StringColumn.StartsWith("a").ToConditionCacheableSql());
            Assert.AreEqual("[StringColumn] LIKE '%' + ?", table.StringColumn.EndsWith("a").ToConditionCacheableSql());
            Assert.AreEqual("INSTR([StringColumn], ?) - 1", table.StringColumn.IndexOf("a").ToExpressionCacheableSql());
            Assert.AreEqual("INSTR([StringColumn], ?) - 1", table.StringColumn.IndexOf(new StringParameterExpression("a", true)).ToExpressionCacheableSql());
            Assert.AreEqual("REPLACE([StringColumn], ?, ?)", table.StringColumn.Replace("a", "b").ToExpressionCacheableSql());
            Assert.AreEqual("REPLACE([StringColumn], ?, ?)", table.StringColumn.Replace("a", new StringParameterExpression("b", true)).ToExpressionCacheableSql());
            Assert.AreEqual("REPLACE([StringColumn], ?, ?)", table.StringColumn.Replace(new StringParameterExpression("a", true), "b").ToExpressionCacheableSql());
            Assert.AreEqual("REPLACE([StringColumn], ?, ?)", table.StringColumn.Replace(new StringParameterExpression("a", true), new StringParameterExpression("b", true)).ToExpressionCacheableSql());
            Assert.AreEqual("SUBSTR([StringColumn], ? + 1, ?)", table.StringColumn.Substring(1, 1).ToExpressionCacheableSql());
            Assert.AreEqual("SUBSTR([StringColumn], ? + 1, ?)", table.StringColumn.Substring(1, new Int32ParameterExpression(1)).ToExpressionCacheableSql());
            Assert.AreEqual("SUBSTR([StringColumn], ? + 1, ?)", table.StringColumn.Substring(new Int32ParameterExpression(1), 1).ToExpressionCacheableSql());
            Assert.AreEqual("SUBSTR([StringColumn], ? + 1, ?)", table.StringColumn.Substring(new Int32ParameterExpression(1), new Int32ParameterExpression(1)).ToExpressionCacheableSql());
            Assert.AreEqual("LTRIM([StringColumn])", table.StringColumn.LTrim().ToExpressionCacheableSql());
            Assert.AreEqual("RTRIM([StringColumn])", table.StringColumn.RTrim().ToExpressionCacheableSql());
            Assert.AreEqual("ASCII([StringColumn])", table.StringColumn.ToAscii().ToExpressionCacheableSql());
            Assert.AreEqual("LENGTH([StringColumn])", table.StringColumn.GetLength().ToExpressionCacheableSql());
        }
    }
}
