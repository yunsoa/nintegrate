using Microsoft.VisualStudio.TestTools.UnitTesting;
using NIntegrate.Query;
using NIntegrate.Query.OracleClient;
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
            var criteria = new TestCriteria();

            Assert.AreEqual("CURRENT_TIMESTAMP", criteria.GetCurrentDate()._sql);
        }

        [TestMethod]
        public void TestOracleClientInt32ExpressionExtenstionMethods()
        {
            var criteria = new TestCriteria();
            Assert.AreEqual("CHR([Int32Column])", QueryHelper.ToExpressionCacheableSql(criteria.Int32Column.ToChar()));
        }

        [TestMethod]
        public void TestOracleClientDateTimeExpressionExtenstionMethods()
        {
            var criteria = new TestCriteria();
            Assert.AreEqual("ADD_MONTHS(month, ?, [DateTimeColumn])", QueryHelper.ToExpressionCacheableSql(criteria.DateTimeColumn.AddMonth(1)));
            Assert.AreEqual("ADD_MONTHS(month, ?, [DateTimeColumn])", QueryHelper.ToExpressionCacheableSql(criteria.DateTimeColumn.AddMonth(new Int32ParameterExpression(1))));
        }

        [TestMethod]
        public void TestOracleClientStringExpressionExtenstionMethods()
        {
            var criteria = new TestCriteria();
            Assert.AreEqual("[StringColumn] LIKE '%' + ? + '%'", QueryHelper.ToConditionCacheableSql(criteria.StringColumn.Contains("a")));
            Assert.AreEqual("[StringColumn] LIKE ? + '%'", QueryHelper.ToConditionCacheableSql(criteria.StringColumn.StartsWith("a")));
            Assert.AreEqual("[StringColumn] LIKE '%' + ?", QueryHelper.ToConditionCacheableSql(criteria.StringColumn.EndsWith("a")));
            Assert.AreEqual("INSTR([StringColumn], ?) - 1", QueryHelper.ToExpressionCacheableSql(criteria.StringColumn.IndexOf("a")));
            Assert.AreEqual("INSTR([StringColumn], ?) - 1", QueryHelper.ToExpressionCacheableSql(criteria.StringColumn.IndexOf(new StringParameterExpression("a", true))));
            Assert.AreEqual("REPLACE([StringColumn], ?, ?)", QueryHelper.ToExpressionCacheableSql(criteria.StringColumn.Replace("a", "b")));
            Assert.AreEqual("REPLACE([StringColumn], ?, ?)", QueryHelper.ToExpressionCacheableSql(criteria.StringColumn.Replace("a", new StringParameterExpression("b", true))));
            Assert.AreEqual("REPLACE([StringColumn], ?, ?)", QueryHelper.ToExpressionCacheableSql(criteria.StringColumn.Replace(new StringParameterExpression("a", true), "b")));
            Assert.AreEqual("REPLACE([StringColumn], ?, ?)", QueryHelper.ToExpressionCacheableSql(criteria.StringColumn.Replace(new StringParameterExpression("a", true), new StringParameterExpression("b", true))));
            Assert.AreEqual("SUBSTR([StringColumn], ? + 1, ?)", QueryHelper.ToExpressionCacheableSql(criteria.StringColumn.Substring(1, 1)));
            Assert.AreEqual("SUBSTR([StringColumn], ? + 1, ?)", QueryHelper.ToExpressionCacheableSql(criteria.StringColumn.Substring(1, new Int32ParameterExpression(1))));
            Assert.AreEqual("SUBSTR([StringColumn], ? + 1, ?)", QueryHelper.ToExpressionCacheableSql(criteria.StringColumn.Substring(new Int32ParameterExpression(1), 1)));
            Assert.AreEqual("SUBSTR([StringColumn], ? + 1, ?)", QueryHelper.ToExpressionCacheableSql(criteria.StringColumn.Substring(new Int32ParameterExpression(1), new Int32ParameterExpression(1))));
            Assert.AreEqual("LTRIM([StringColumn])", QueryHelper.ToExpressionCacheableSql(criteria.StringColumn.LTrim()));
            Assert.AreEqual("RTRIM([StringColumn])", QueryHelper.ToExpressionCacheableSql(criteria.StringColumn.RTrim()));
            Assert.AreEqual("ASCII([StringColumn])", QueryHelper.ToExpressionCacheableSql(criteria.StringColumn.ToAscii()));
            Assert.AreEqual("LENGTH([StringColumn])", QueryHelper.ToExpressionCacheableSql(criteria.StringColumn.GetLength()));
        }
    }
}
