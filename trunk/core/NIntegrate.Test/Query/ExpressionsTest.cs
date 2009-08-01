using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NIntegrate.Test.Query.TestClasses;
using NIntegrate.Data;

namespace NIntegrate.Test.Query
{
    /// <summary>
    /// Test all expression class members
    /// </summary>
    [TestClass]
    public class ExpressionsTest
    {
        private readonly TestTable _criteria = new TestTable();

        [TestMethod]
        public void TestBooleanExpression()
        {
            Assert.AreEqual("[BooleanColumn] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.BooleanColumn.Equals(true)));
            Assert.AreEqual("[BooleanColumn] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.BooleanColumn.Equals(new BooleanParameterExpression(true))));
            Assert.AreEqual("[BooleanColumn] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.BooleanColumn == true));
            Assert.AreEqual("[BooleanColumn] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.BooleanColumn == new BooleanParameterExpression(true)));
            Assert.AreEqual("[BooleanColumn] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.BooleanColumn.NotEquals(true)));
            Assert.AreEqual("[BooleanColumn] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.BooleanColumn.NotEquals(new BooleanParameterExpression(true))));
            Assert.AreEqual("[BooleanColumn] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.BooleanColumn != true));
            Assert.AreEqual("[BooleanColumn] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.BooleanColumn != new BooleanParameterExpression(true)));

            Assert.AreEqual("[BooleanColumn] IN (?, ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.BooleanColumn.In(new[] { true, false })));

            Assert.AreEqual("[BooleanColumn] AS [C]", _criteria.BooleanColumn.As("C").ToSelectColumnName());
        }

        [TestMethod]
        public void TestByteExpression()
        {
            Assert.AreEqual("[ByteColumn] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.ByteColumn.Equals(2)));
            Assert.AreEqual("[ByteColumn] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.ByteColumn.Equals(new ByteParameterExpression(2))));
            Assert.AreEqual("[ByteColumn] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.ByteColumn == 2));
            Assert.AreEqual("[ByteColumn] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.ByteColumn == new ByteParameterExpression(2)));
            Assert.AreEqual("[ByteColumn] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.ByteColumn.NotEquals(2)));
            Assert.AreEqual("[ByteColumn] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.ByteColumn.NotEquals(new ByteParameterExpression(2))));
            Assert.AreEqual("[ByteColumn] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.ByteColumn != 2));
            Assert.AreEqual("[ByteColumn] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.ByteColumn != new ByteParameterExpression(2)));

            Assert.AreEqual("[ByteColumn] & ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.ByteColumn.BitwiseAnd(3)));
            Assert.AreEqual("[ByteColumn] & ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.ByteColumn.BitwiseAnd(new ByteParameterExpression(3))));
            Assert.AreEqual("[ByteColumn] | ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.ByteColumn.BitwiseOr(3)));
            Assert.AreEqual("[ByteColumn] | ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.ByteColumn.BitwiseOr(new ByteParameterExpression(3))));
            Assert.AreEqual("[ByteColumn] ^ ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.ByteColumn.BitwiseXor(3)));
            Assert.AreEqual("[ByteColumn] ^ ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.ByteColumn.BitwiseXor(new ByteParameterExpression(3))));
            Assert.AreEqual("~([ByteColumn])", ExpressionHelper.ToExpressionCacheableSql(_criteria.ByteColumn.BitwiseNot()));

            Assert.AreEqual("([ByteColumn] >= ? AND [ByteColumn] <= ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.ByteColumn.Between(1, 3, true, true)));
            Assert.AreEqual("([ByteColumn] >= ? AND [ByteColumn] < ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.ByteColumn.Between(1, new ByteParameterExpression(3), true, false)));
            Assert.AreEqual("([ByteColumn] > ? AND [ByteColumn] <= ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.ByteColumn.Between(new ByteParameterExpression(1), 3, false, true)));
            Assert.AreEqual("([ByteColumn] > ? AND [ByteColumn] < ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.ByteColumn.Between(new ByteParameterExpression(1), new ByteParameterExpression(3), false, false)));

            Assert.AreEqual("[ByteColumn] > ?", ExpressionHelper.ToConditionCacheableSql(_criteria.ByteColumn.GreaterThan(2)));
            Assert.AreEqual("[ByteColumn] > ?", ExpressionHelper.ToConditionCacheableSql(_criteria.ByteColumn.GreaterThan(new ByteParameterExpression(2))));
            Assert.AreEqual("[ByteColumn] > ?", ExpressionHelper.ToConditionCacheableSql(_criteria.ByteColumn > 2));
            Assert.AreEqual("[ByteColumn] > ?", ExpressionHelper.ToConditionCacheableSql(_criteria.ByteColumn > new ByteParameterExpression(2)));
            Assert.AreEqual("[ByteColumn] < ?", ExpressionHelper.ToConditionCacheableSql(_criteria.ByteColumn.LessThan(2)));
            Assert.AreEqual("[ByteColumn] < ?", ExpressionHelper.ToConditionCacheableSql(_criteria.ByteColumn.LessThan(new ByteParameterExpression(2))));
            Assert.AreEqual("[ByteColumn] < ?", ExpressionHelper.ToConditionCacheableSql(_criteria.ByteColumn < 2));
            Assert.AreEqual("[ByteColumn] < ?", ExpressionHelper.ToConditionCacheableSql(_criteria.ByteColumn < new ByteParameterExpression(2)));
            Assert.AreEqual("[ByteColumn] >= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.ByteColumn.GreaterThanOrEquals(2)));
            Assert.AreEqual("[ByteColumn] >= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.ByteColumn.GreaterThanOrEquals(new ByteParameterExpression(2))));
            Assert.AreEqual("[ByteColumn] >= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.ByteColumn >= 2));
            Assert.AreEqual("[ByteColumn] >= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.ByteColumn >= new ByteParameterExpression(2)));
            Assert.AreEqual("[ByteColumn] <= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.ByteColumn.LessThanOrEquals(2)));
            Assert.AreEqual("[ByteColumn] <= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.ByteColumn.LessThanOrEquals(new ByteParameterExpression(2))));
            Assert.AreEqual("[ByteColumn] <= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.ByteColumn <= 2));
            Assert.AreEqual("[ByteColumn] <= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.ByteColumn <= new ByteParameterExpression(2)));

            Assert.AreEqual("[ByteColumn] + ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.ByteColumn.Add(3)));
            Assert.AreEqual("[ByteColumn] + ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.ByteColumn.Add(new ByteParameterExpression(3))));
            Assert.AreEqual("[ByteColumn] + ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.ByteColumn + 3));
            Assert.AreEqual("[ByteColumn] + ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.ByteColumn + new ByteParameterExpression(3)));
            Assert.AreEqual("[ByteColumn] - ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.ByteColumn.Subtract(3)));
            Assert.AreEqual("[ByteColumn] - ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.ByteColumn.Subtract(new ByteParameterExpression(3))));
            Assert.AreEqual("[ByteColumn] - ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.ByteColumn - 3));
            Assert.AreEqual("[ByteColumn] - ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.ByteColumn - new ByteParameterExpression(3)));
            Assert.AreEqual("[ByteColumn] * ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.ByteColumn.Multiply(3)));
            Assert.AreEqual("[ByteColumn] * ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.ByteColumn.Multiply(new ByteParameterExpression(3))));
            Assert.AreEqual("[ByteColumn] * ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.ByteColumn * 3));
            Assert.AreEqual("[ByteColumn] * ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.ByteColumn * new ByteParameterExpression(3)));
            Assert.AreEqual("[ByteColumn] / ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.ByteColumn.Divide(3)));
            Assert.AreEqual("[ByteColumn] / ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.ByteColumn.Divide(new ByteParameterExpression(3))));
            Assert.AreEqual("[ByteColumn] / ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.ByteColumn / 3));
            Assert.AreEqual("[ByteColumn] / ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.ByteColumn / new ByteParameterExpression(3)));
            Assert.AreEqual("[ByteColumn] % ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.ByteColumn.Mod(3)));
            Assert.AreEqual("[ByteColumn] % ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.ByteColumn.Mod(new ByteParameterExpression(3))));
            Assert.AreEqual("[ByteColumn] % ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.ByteColumn % 3));
            Assert.AreEqual("[ByteColumn] % ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.ByteColumn % new ByteParameterExpression(3)));

            Assert.AreEqual("COUNT([ByteColumn])", ExpressionHelper.ToExpressionCacheableSql(_criteria.ByteColumn.Count()));
            Assert.AreEqual("COUNT(DISTINCT [ByteColumn])", ExpressionHelper.ToExpressionCacheableSql(_criteria.ByteColumn.Count(true)));
            Assert.AreEqual("AVG([ByteColumn])", ExpressionHelper.ToExpressionCacheableSql(_criteria.ByteColumn.Avg()));
            Assert.AreEqual("MAX([ByteColumn])", ExpressionHelper.ToExpressionCacheableSql(_criteria.ByteColumn.Max()));
            Assert.AreEqual("MIN([ByteColumn])", ExpressionHelper.ToExpressionCacheableSql(_criteria.ByteColumn.Min()));
            Assert.AreEqual("SUM([ByteColumn])", ExpressionHelper.ToExpressionCacheableSql(_criteria.ByteColumn.Sum()));

            Assert.AreEqual("[ByteColumn] IN (?, ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.ByteColumn.In(new byte[] { 2, 3 })));

            Assert.AreEqual("[ByteColumn] AS [C]", _criteria.ByteColumn.As("C").ToSelectColumnName());
        }

        [TestMethod]
        public void TestInt16Expression()
        {
            Assert.AreEqual("[Int16Column] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int16Column.Equals(2)));
            Assert.AreEqual("[Int16Column] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int16Column.Equals(new Int16ParameterExpression(2))));
            Assert.AreEqual("[Int16Column] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int16Column == 2));
            Assert.AreEqual("[Int16Column] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int16Column == new Int16ParameterExpression(2)));
            Assert.AreEqual("[Int16Column] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int16Column.NotEquals(2)));
            Assert.AreEqual("[Int16Column] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int16Column.NotEquals(new Int16ParameterExpression(2))));
            Assert.AreEqual("[Int16Column] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int16Column != 2));
            Assert.AreEqual("[Int16Column] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int16Column != new Int16ParameterExpression(2)));

            Assert.AreEqual("[Int16Column] & ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int16Column.BitwiseAnd(3)));
            Assert.AreEqual("[Int16Column] & ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int16Column.BitwiseAnd(new Int16ParameterExpression(3))));
            Assert.AreEqual("[Int16Column] | ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int16Column.BitwiseOr(3)));
            Assert.AreEqual("[Int16Column] | ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int16Column.BitwiseOr(new Int16ParameterExpression(3))));
            Assert.AreEqual("[Int16Column] ^ ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int16Column.BitwiseXor(3)));
            Assert.AreEqual("[Int16Column] ^ ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int16Column.BitwiseXor(new Int16ParameterExpression(3))));
            Assert.AreEqual("~([Int16Column])", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int16Column.BitwiseNot()));

            Assert.AreEqual("([Int16Column] >= ? AND [Int16Column] <= ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.Int16Column.Between(1, 3, true, true)));
            Assert.AreEqual("([Int16Column] >= ? AND [Int16Column] < ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.Int16Column.Between(1, new Int16ParameterExpression(3), true, false)));
            Assert.AreEqual("([Int16Column] > ? AND [Int16Column] <= ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.Int16Column.Between(new Int16ParameterExpression(1), 3, false, true)));
            Assert.AreEqual("([Int16Column] > ? AND [Int16Column] < ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.Int16Column.Between(new Int16ParameterExpression(1), new Int16ParameterExpression(3), false, false)));

            Assert.AreEqual("[Int16Column] > ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int16Column.GreaterThan(2)));
            Assert.AreEqual("[Int16Column] > ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int16Column.GreaterThan(new Int16ParameterExpression(2))));
            Assert.AreEqual("[Int16Column] > ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int16Column > 2));
            Assert.AreEqual("[Int16Column] > ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int16Column > new Int16ParameterExpression(2)));
            Assert.AreEqual("[Int16Column] < ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int16Column.LessThan(2)));
            Assert.AreEqual("[Int16Column] < ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int16Column.LessThan(new Int16ParameterExpression(2))));
            Assert.AreEqual("[Int16Column] < ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int16Column < 2));
            Assert.AreEqual("[Int16Column] < ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int16Column < new Int16ParameterExpression(2)));
            Assert.AreEqual("[Int16Column] >= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int16Column.GreaterThanOrEquals(2)));
            Assert.AreEqual("[Int16Column] >= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int16Column.GreaterThanOrEquals(new Int16ParameterExpression(2))));
            Assert.AreEqual("[Int16Column] >= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int16Column >= 2));
            Assert.AreEqual("[Int16Column] >= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int16Column >= new Int16ParameterExpression(2)));
            Assert.AreEqual("[Int16Column] <= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int16Column.LessThanOrEquals(2)));
            Assert.AreEqual("[Int16Column] <= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int16Column.LessThanOrEquals(new Int16ParameterExpression(2))));
            Assert.AreEqual("[Int16Column] <= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int16Column <= 2));
            Assert.AreEqual("[Int16Column] <= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int16Column <= new Int16ParameterExpression(2)));

            Assert.AreEqual("[Int16Column] + ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int16Column.Add(3)));
            Assert.AreEqual("[Int16Column] + ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int16Column.Add(new Int16ParameterExpression(3))));
            Assert.AreEqual("[Int16Column] + ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int16Column + 3));
            Assert.AreEqual("[Int16Column] + ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int16Column + new Int16ParameterExpression(3)));
            Assert.AreEqual("[Int16Column] - ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int16Column.Subtract(3)));
            Assert.AreEqual("[Int16Column] - ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int16Column.Subtract(new Int16ParameterExpression(3))));
            Assert.AreEqual("[Int16Column] - ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int16Column - 3));
            Assert.AreEqual("[Int16Column] - ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int16Column - new Int16ParameterExpression(3)));
            Assert.AreEqual("[Int16Column] * ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int16Column.Multiply(3)));
            Assert.AreEqual("[Int16Column] * ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int16Column.Multiply(new Int16ParameterExpression(3))));
            Assert.AreEqual("[Int16Column] * ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int16Column * 3));
            Assert.AreEqual("[Int16Column] * ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int16Column * new Int16ParameterExpression(3)));
            Assert.AreEqual("[Int16Column] / ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int16Column.Divide(3)));
            Assert.AreEqual("[Int16Column] / ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int16Column.Divide(new Int16ParameterExpression(3))));
            Assert.AreEqual("[Int16Column] / ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int16Column / 3));
            Assert.AreEqual("[Int16Column] / ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int16Column / new Int16ParameterExpression(3)));
            Assert.AreEqual("[Int16Column] % ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int16Column.Mod(3)));
            Assert.AreEqual("[Int16Column] % ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int16Column.Mod(new Int16ParameterExpression(3))));
            Assert.AreEqual("[Int16Column] % ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int16Column % 3));
            Assert.AreEqual("[Int16Column] % ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int16Column % new Int16ParameterExpression(3)));

            Assert.AreEqual("COUNT([Int16Column])", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int16Column.Count()));
            Assert.AreEqual("COUNT(DISTINCT [Int16Column])", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int16Column.Count(true)));
            Assert.AreEqual("AVG([Int16Column])", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int16Column.Avg()));
            Assert.AreEqual("MAX([Int16Column])", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int16Column.Max()));
            Assert.AreEqual("MIN([Int16Column])", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int16Column.Min()));
            Assert.AreEqual("SUM([Int16Column])", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int16Column.Sum()));

            Assert.AreEqual("[Int16Column] IN (?, ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.Int16Column.In(new short[] { 2, 3 })));

            Assert.AreEqual("[Int16Column] AS [C]", _criteria.Int16Column.As("C").ToSelectColumnName());
        }

        [TestMethod]
        public void TestInt32Expression()
        {
            Assert.AreEqual("[Int32Column] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int32Column.Equals(2)));
            Assert.AreEqual("[Int32Column] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int32Column.Equals(new Int32ParameterExpression(2))));
            Assert.AreEqual("[Int32Column] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int32Column == 2));
            Assert.AreEqual("[Int32Column] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int32Column == new Int32ParameterExpression(2)));
            Assert.AreEqual("[Int32Column] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int32Column.NotEquals(2)));
            Assert.AreEqual("[Int32Column] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int32Column.NotEquals(new Int32ParameterExpression(2))));
            Assert.AreEqual("[Int32Column] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int32Column != 2));
            Assert.AreEqual("[Int32Column] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int32Column != new Int32ParameterExpression(2)));

            Assert.AreEqual("[Int32Column] & ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int32Column.BitwiseAnd(3)));
            Assert.AreEqual("[Int32Column] & ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int32Column.BitwiseAnd(new Int32ParameterExpression(3))));
            Assert.AreEqual("[Int32Column] | ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int32Column.BitwiseOr(3)));
            Assert.AreEqual("[Int32Column] | ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int32Column.BitwiseOr(new Int32ParameterExpression(3))));
            Assert.AreEqual("[Int32Column] ^ ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int32Column.BitwiseXor(3)));
            Assert.AreEqual("[Int32Column] ^ ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int32Column.BitwiseXor(new Int32ParameterExpression(3))));
            Assert.AreEqual("~([Int32Column])", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int32Column.BitwiseNot()));

            Assert.AreEqual("([Int32Column] >= ? AND [Int32Column] <= ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.Int32Column.Between(1, 3, true, true)));
            Assert.AreEqual("([Int32Column] >= ? AND [Int32Column] < ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.Int32Column.Between(1, new Int32ParameterExpression(3), true, false)));
            Assert.AreEqual("([Int32Column] > ? AND [Int32Column] <= ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.Int32Column.Between(new Int32ParameterExpression(1), 3, false, true)));
            Assert.AreEqual("([Int32Column] > ? AND [Int32Column] < ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.Int32Column.Between(new Int32ParameterExpression(1), new Int32ParameterExpression(3), false, false)));

            Assert.AreEqual("[Int32Column] > ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int32Column.GreaterThan(2)));
            Assert.AreEqual("[Int32Column] > ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int32Column.GreaterThan(new Int32ParameterExpression(2))));
            Assert.AreEqual("[Int32Column] > ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int32Column > 2));
            Assert.AreEqual("[Int32Column] > ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int32Column > new Int32ParameterExpression(2)));
            Assert.AreEqual("[Int32Column] < ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int32Column.LessThan(2)));
            Assert.AreEqual("[Int32Column] < ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int32Column.LessThan(new Int32ParameterExpression(2))));
            Assert.AreEqual("[Int32Column] < ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int32Column < 2));
            Assert.AreEqual("[Int32Column] < ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int32Column < new Int32ParameterExpression(2)));
            Assert.AreEqual("[Int32Column] >= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int32Column.GreaterThanOrEquals(2)));
            Assert.AreEqual("[Int32Column] >= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int32Column.GreaterThanOrEquals(new Int32ParameterExpression(2))));
            Assert.AreEqual("[Int32Column] >= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int32Column >= 2));
            Assert.AreEqual("[Int32Column] >= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int32Column >= new Int32ParameterExpression(2)));
            Assert.AreEqual("[Int32Column] <= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int32Column.LessThanOrEquals(2)));
            Assert.AreEqual("[Int32Column] <= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int32Column.LessThanOrEquals(new Int32ParameterExpression(2))));
            Assert.AreEqual("[Int32Column] <= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int32Column <= 2));
            Assert.AreEqual("[Int32Column] <= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int32Column <= new Int32ParameterExpression(2)));

            Assert.AreEqual("[Int32Column] + ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int32Column.Add(3)));
            Assert.AreEqual("[Int32Column] + ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int32Column.Add(new Int32ParameterExpression(3))));
            Assert.AreEqual("[Int32Column] + ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int32Column + 3));
            Assert.AreEqual("[Int32Column] + ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int32Column + new Int32ParameterExpression(3)));
            Assert.AreEqual("[Int32Column] - ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int32Column.Subtract(3)));
            Assert.AreEqual("[Int32Column] - ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int32Column.Subtract(new Int32ParameterExpression(3))));
            Assert.AreEqual("[Int32Column] - ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int32Column - 3));
            Assert.AreEqual("[Int32Column] - ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int32Column - new Int32ParameterExpression(3)));
            Assert.AreEqual("[Int32Column] * ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int32Column.Multiply(3)));
            Assert.AreEqual("[Int32Column] * ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int32Column.Multiply(new Int32ParameterExpression(3))));
            Assert.AreEqual("[Int32Column] * ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int32Column * 3));
            Assert.AreEqual("[Int32Column] * ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int32Column * new Int32ParameterExpression(3)));
            Assert.AreEqual("[Int32Column] / ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int32Column.Divide(3)));
            Assert.AreEqual("[Int32Column] / ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int32Column.Divide(new Int32ParameterExpression(3))));
            Assert.AreEqual("[Int32Column] / ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int32Column / 3));
            Assert.AreEqual("[Int32Column] / ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int32Column / new Int32ParameterExpression(3)));
            Assert.AreEqual("[Int32Column] % ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int32Column.Mod(3)));
            Assert.AreEqual("[Int32Column] % ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int32Column.Mod(new Int32ParameterExpression(3))));
            Assert.AreEqual("[Int32Column] % ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int32Column % 3));
            Assert.AreEqual("[Int32Column] % ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int32Column % new Int32ParameterExpression(3)));

            Assert.AreEqual("COUNT([Int32Column])", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int32Column.Count()));
            Assert.AreEqual("COUNT(DISTINCT [Int32Column])", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int32Column.Count(true)));
            Assert.AreEqual("AVG([Int32Column])", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int32Column.Avg()));
            Assert.AreEqual("MAX([Int32Column])", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int32Column.Max()));
            Assert.AreEqual("MIN([Int32Column])", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int32Column.Min()));
            Assert.AreEqual("SUM([Int32Column])", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int32Column.Sum()));

            Assert.AreEqual("[Int32Column] IN (?, ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.Int32Column.In(new[] { 2, 3 })));

            Assert.AreEqual("[Int32Column] AS [C]", _criteria.Int32Column.As("C").ToSelectColumnName());
        }

        [TestMethod]
        public void TestInt64Expression()
        {
            Assert.AreEqual("[Int64Column] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int64Column.Equals(2)));
            Assert.AreEqual("[Int64Column] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int64Column.Equals(new Int64ParameterExpression(2))));
            Assert.AreEqual("[Int64Column] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int64Column == 2));
            Assert.AreEqual("[Int64Column] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int64Column == new Int64ParameterExpression(2)));
            Assert.AreEqual("[Int64Column] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int64Column.NotEquals(2)));
            Assert.AreEqual("[Int64Column] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int64Column.NotEquals(new Int64ParameterExpression(2))));
            Assert.AreEqual("[Int64Column] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int64Column != 2));
            Assert.AreEqual("[Int64Column] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int64Column != new Int64ParameterExpression(2)));

            Assert.AreEqual("[Int64Column] & ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int64Column.BitwiseAnd(3)));
            Assert.AreEqual("[Int64Column] & ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int64Column.BitwiseAnd(new Int64ParameterExpression(3))));
            Assert.AreEqual("[Int64Column] | ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int64Column.BitwiseOr(3)));
            Assert.AreEqual("[Int64Column] | ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int64Column.BitwiseOr(new Int64ParameterExpression(3))));
            Assert.AreEqual("[Int64Column] ^ ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int64Column.BitwiseXor(3)));
            Assert.AreEqual("[Int64Column] ^ ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int64Column.BitwiseXor(new Int64ParameterExpression(3))));
            Assert.AreEqual("~([Int64Column])", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int64Column.BitwiseNot()));

            Assert.AreEqual("([Int64Column] >= ? AND [Int64Column] <= ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.Int64Column.Between(1, 3, true, true)));
            Assert.AreEqual("([Int64Column] >= ? AND [Int64Column] < ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.Int64Column.Between(1, new Int64ParameterExpression(3), true, false)));
            Assert.AreEqual("([Int64Column] > ? AND [Int64Column] <= ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.Int64Column.Between(new Int64ParameterExpression(1), 3, false, true)));
            Assert.AreEqual("([Int64Column] > ? AND [Int64Column] < ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.Int64Column.Between(new Int64ParameterExpression(1), new Int64ParameterExpression(3), false, false)));

            Assert.AreEqual("[Int64Column] > ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int64Column.GreaterThan(2)));
            Assert.AreEqual("[Int64Column] > ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int64Column.GreaterThan(new Int64ParameterExpression(2))));
            Assert.AreEqual("[Int64Column] > ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int64Column > 2));
            Assert.AreEqual("[Int64Column] > ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int64Column > new Int64ParameterExpression(2)));
            Assert.AreEqual("[Int64Column] < ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int64Column.LessThan(2)));
            Assert.AreEqual("[Int64Column] < ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int64Column.LessThan(new Int64ParameterExpression(2))));
            Assert.AreEqual("[Int64Column] < ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int64Column < 2));
            Assert.AreEqual("[Int64Column] < ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int64Column < new Int64ParameterExpression(2)));
            Assert.AreEqual("[Int64Column] >= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int64Column.GreaterThanOrEquals(2)));
            Assert.AreEqual("[Int64Column] >= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int64Column.GreaterThanOrEquals(new Int64ParameterExpression(2))));
            Assert.AreEqual("[Int64Column] >= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int64Column >= 2));
            Assert.AreEqual("[Int64Column] >= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int64Column >= new Int64ParameterExpression(2)));
            Assert.AreEqual("[Int64Column] <= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int64Column.LessThanOrEquals(2)));
            Assert.AreEqual("[Int64Column] <= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int64Column.LessThanOrEquals(new Int64ParameterExpression(2))));
            Assert.AreEqual("[Int64Column] <= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int64Column <= 2));
            Assert.AreEqual("[Int64Column] <= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.Int64Column <= new Int64ParameterExpression(2)));

            Assert.AreEqual("[Int64Column] + ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int64Column.Add(3)));
            Assert.AreEqual("[Int64Column] + ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int64Column.Add(new Int64ParameterExpression(3))));
            Assert.AreEqual("[Int64Column] + ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int64Column + 3));
            Assert.AreEqual("[Int64Column] + ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int64Column + new Int64ParameterExpression(3)));
            Assert.AreEqual("[Int64Column] - ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int64Column.Subtract(3)));
            Assert.AreEqual("[Int64Column] - ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int64Column.Subtract(new Int64ParameterExpression(3))));
            Assert.AreEqual("[Int64Column] - ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int64Column - 3));
            Assert.AreEqual("[Int64Column] - ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int64Column - new Int64ParameterExpression(3)));
            Assert.AreEqual("[Int64Column] * ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int64Column.Multiply(3)));
            Assert.AreEqual("[Int64Column] * ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int64Column.Multiply(new Int64ParameterExpression(3))));
            Assert.AreEqual("[Int64Column] * ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int64Column * 3));
            Assert.AreEqual("[Int64Column] * ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int64Column * new Int64ParameterExpression(3)));
            Assert.AreEqual("[Int64Column] / ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int64Column.Divide(3)));
            Assert.AreEqual("[Int64Column] / ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int64Column.Divide(new Int64ParameterExpression(3))));
            Assert.AreEqual("[Int64Column] / ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int64Column / 3));
            Assert.AreEqual("[Int64Column] / ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int64Column / new Int64ParameterExpression(3)));
            Assert.AreEqual("[Int64Column] % ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int64Column.Mod(3)));
            Assert.AreEqual("[Int64Column] % ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int64Column.Mod(new Int64ParameterExpression(3))));
            Assert.AreEqual("[Int64Column] % ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int64Column % 3));
            Assert.AreEqual("[Int64Column] % ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int64Column % new Int64ParameterExpression(3)));

            Assert.AreEqual("COUNT([Int64Column])", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int64Column.Count()));
            Assert.AreEqual("COUNT(DISTINCT [Int64Column])", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int64Column.Count(true)));
            Assert.AreEqual("AVG([Int64Column])", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int64Column.Avg()));
            Assert.AreEqual("MAX([Int64Column])", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int64Column.Max()));
            Assert.AreEqual("MIN([Int64Column])", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int64Column.Min()));
            Assert.AreEqual("SUM([Int64Column])", ExpressionHelper.ToExpressionCacheableSql(_criteria.Int64Column.Sum()));

            Assert.AreEqual("[Int64Column] IN (?, ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.Int64Column.In(new long[] { 2, 3 })));

            Assert.AreEqual("[Int64Column] AS [C]", _criteria.Int64Column.As("C").ToSelectColumnName());
        }

        [TestMethod]
        public void TestDateTimeExpression()
        {
            Assert.AreEqual("[DateTimeColumn] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DateTimeColumn.Equals(DateTime.Now)));
            Assert.AreEqual("[DateTimeColumn] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DateTimeColumn.Equals(new DateTimeParameterExpression(DateTime.Now))));
            Assert.AreEqual("[DateTimeColumn] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DateTimeColumn == DateTime.Now));
            Assert.AreEqual("[DateTimeColumn] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DateTimeColumn == new DateTimeParameterExpression(DateTime.Now)));
            Assert.AreEqual("[DateTimeColumn] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DateTimeColumn.NotEquals(DateTime.Now)));
            Assert.AreEqual("[DateTimeColumn] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DateTimeColumn.NotEquals(new DateTimeParameterExpression(DateTime.Now))));
            Assert.AreEqual("[DateTimeColumn] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DateTimeColumn != DateTime.Now));
            Assert.AreEqual("[DateTimeColumn] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DateTimeColumn != new DateTimeParameterExpression(DateTime.Now)));

            Assert.AreEqual("([DateTimeColumn] >= ? AND [DateTimeColumn] <= ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.DateTimeColumn.Between(DateTime.Now, DateTime.Now, true, true)));
            Assert.AreEqual("([DateTimeColumn] >= ? AND [DateTimeColumn] < ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.DateTimeColumn.Between(DateTime.Now, new DateTimeParameterExpression(DateTime.Now), true, false)));
            Assert.AreEqual("([DateTimeColumn] > ? AND [DateTimeColumn] <= ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.DateTimeColumn.Between(new DateTimeParameterExpression(DateTime.Now), DateTime.Now, false, true)));
            Assert.AreEqual("([DateTimeColumn] > ? AND [DateTimeColumn] < ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.DateTimeColumn.Between(new DateTimeParameterExpression(DateTime.Now), new DateTimeParameterExpression(DateTime.Now), false, false)));

            Assert.AreEqual("[DateTimeColumn] > ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DateTimeColumn.GreaterThan(DateTime.Now)));
            Assert.AreEqual("[DateTimeColumn] > ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DateTimeColumn.GreaterThan(new DateTimeParameterExpression(DateTime.Now))));
            Assert.AreEqual("[DateTimeColumn] > ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DateTimeColumn > DateTime.Now));
            Assert.AreEqual("[DateTimeColumn] > ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DateTimeColumn > new DateTimeParameterExpression(DateTime.Now)));
            Assert.AreEqual("[DateTimeColumn] < ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DateTimeColumn.LessThan(DateTime.Now)));
            Assert.AreEqual("[DateTimeColumn] < ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DateTimeColumn.LessThan(new DateTimeParameterExpression(DateTime.Now))));
            Assert.AreEqual("[DateTimeColumn] < ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DateTimeColumn < DateTime.Now));
            Assert.AreEqual("[DateTimeColumn] < ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DateTimeColumn < new DateTimeParameterExpression(DateTime.Now)));
            Assert.AreEqual("[DateTimeColumn] >= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DateTimeColumn.GreaterThanOrEquals(DateTime.Now)));
            Assert.AreEqual("[DateTimeColumn] >= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DateTimeColumn.GreaterThanOrEquals(new DateTimeParameterExpression(DateTime.Now))));
            Assert.AreEqual("[DateTimeColumn] >= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DateTimeColumn >= DateTime.Now));
            Assert.AreEqual("[DateTimeColumn] >= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DateTimeColumn >= new DateTimeParameterExpression(DateTime.Now)));
            Assert.AreEqual("[DateTimeColumn] <= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DateTimeColumn.LessThanOrEquals(DateTime.Now)));
            Assert.AreEqual("[DateTimeColumn] <= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DateTimeColumn.LessThanOrEquals(new DateTimeParameterExpression(DateTime.Now))));
            Assert.AreEqual("[DateTimeColumn] <= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DateTimeColumn <= DateTime.Now));
            Assert.AreEqual("[DateTimeColumn] <= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DateTimeColumn <= new DateTimeParameterExpression(DateTime.Now)));

            Assert.AreEqual("[DateTimeColumn] + ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DateTimeColumn.Add(DateTime.Now)));
            Assert.AreEqual("[DateTimeColumn] + ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DateTimeColumn.Add(new DateTimeParameterExpression(DateTime.Now))));
            Assert.AreEqual("[DateTimeColumn] + ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DateTimeColumn + DateTime.Now));
            Assert.AreEqual("[DateTimeColumn] + ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DateTimeColumn + new DateTimeParameterExpression(DateTime.Now)));
            Assert.AreEqual("[DateTimeColumn] - ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DateTimeColumn.Subtract(DateTime.Now)));
            Assert.AreEqual("[DateTimeColumn] - ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DateTimeColumn.Subtract(new DateTimeParameterExpression(DateTime.Now))));
            Assert.AreEqual("[DateTimeColumn] - ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DateTimeColumn - DateTime.Now));
            Assert.AreEqual("[DateTimeColumn] - ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DateTimeColumn - new DateTimeParameterExpression(DateTime.Now)));

            Assert.AreEqual("COUNT([DateTimeColumn])", ExpressionHelper.ToExpressionCacheableSql(_criteria.DateTimeColumn.Count()));
            Assert.AreEqual("COUNT(DISTINCT [DateTimeColumn])", ExpressionHelper.ToExpressionCacheableSql(_criteria.DateTimeColumn.Count(true)));
            Assert.AreEqual("MAX([DateTimeColumn])", ExpressionHelper.ToExpressionCacheableSql(_criteria.DateTimeColumn.Max()));
            Assert.AreEqual("MIN([DateTimeColumn])", ExpressionHelper.ToExpressionCacheableSql(_criteria.DateTimeColumn.Min()));

            Assert.AreEqual("[DateTimeColumn] IN (?, ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.DateTimeColumn.In(new[] { DateTime.Now, DateTime.MaxValue })));

            Assert.AreEqual("[DateTimeColumn] AS [C]", _criteria.DateTimeColumn.As("C").ToSelectColumnName());
        }

        [TestMethod]
        public void TestDecimalExpression()
        {
            Assert.AreEqual("[DecimalColumn] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DecimalColumn.Equals(2)));
            Assert.AreEqual("[DecimalColumn] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DecimalColumn.Equals(new DecimalParameterExpression(2))));
            Assert.AreEqual("[DecimalColumn] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DecimalColumn == 2));
            Assert.AreEqual("[DecimalColumn] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DecimalColumn == new DecimalParameterExpression(2)));
            Assert.AreEqual("[DecimalColumn] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DecimalColumn.NotEquals(2)));
            Assert.AreEqual("[DecimalColumn] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DecimalColumn.NotEquals(new DecimalParameterExpression(2))));
            Assert.AreEqual("[DecimalColumn] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DecimalColumn != 2));
            Assert.AreEqual("[DecimalColumn] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DecimalColumn != new DecimalParameterExpression(2)));

            Assert.AreEqual("([DecimalColumn] >= ? AND [DecimalColumn] <= ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.DecimalColumn.Between(1, 3, true, true)));
            Assert.AreEqual("([DecimalColumn] >= ? AND [DecimalColumn] < ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.DecimalColumn.Between(1, new DecimalParameterExpression(3), true, false)));
            Assert.AreEqual("([DecimalColumn] > ? AND [DecimalColumn] <= ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.DecimalColumn.Between(new DecimalParameterExpression(1), 3, false, true)));
            Assert.AreEqual("([DecimalColumn] > ? AND [DecimalColumn] < ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.DecimalColumn.Between(new DecimalParameterExpression(1), new DecimalParameterExpression(3), false, false)));

            Assert.AreEqual("[DecimalColumn] > ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DecimalColumn.GreaterThan(2)));
            Assert.AreEqual("[DecimalColumn] > ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DecimalColumn.GreaterThan(new DecimalParameterExpression(2))));
            Assert.AreEqual("[DecimalColumn] > ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DecimalColumn > 2));
            Assert.AreEqual("[DecimalColumn] > ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DecimalColumn > new DecimalParameterExpression(2)));
            Assert.AreEqual("[DecimalColumn] < ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DecimalColumn.LessThan(2)));
            Assert.AreEqual("[DecimalColumn] < ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DecimalColumn.LessThan(new DecimalParameterExpression(2))));
            Assert.AreEqual("[DecimalColumn] < ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DecimalColumn < 2));
            Assert.AreEqual("[DecimalColumn] < ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DecimalColumn < new DecimalParameterExpression(2)));
            Assert.AreEqual("[DecimalColumn] >= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DecimalColumn.GreaterThanOrEquals(2)));
            Assert.AreEqual("[DecimalColumn] >= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DecimalColumn.GreaterThanOrEquals(new DecimalParameterExpression(2))));
            Assert.AreEqual("[DecimalColumn] >= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DecimalColumn >= 2));
            Assert.AreEqual("[DecimalColumn] >= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DecimalColumn >= new DecimalParameterExpression(2)));
            Assert.AreEqual("[DecimalColumn] <= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DecimalColumn.LessThanOrEquals(2)));
            Assert.AreEqual("[DecimalColumn] <= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DecimalColumn.LessThanOrEquals(new DecimalParameterExpression(2))));
            Assert.AreEqual("[DecimalColumn] <= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DecimalColumn <= 2));
            Assert.AreEqual("[DecimalColumn] <= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DecimalColumn <= new DecimalParameterExpression(2)));

            Assert.AreEqual("[DecimalColumn] + ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DecimalColumn.Add(3)));
            Assert.AreEqual("[DecimalColumn] + ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DecimalColumn.Add(new DecimalParameterExpression(3))));
            Assert.AreEqual("[DecimalColumn] + ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DecimalColumn + 3));
            Assert.AreEqual("[DecimalColumn] + ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DecimalColumn + new DecimalParameterExpression(3)));
            Assert.AreEqual("[DecimalColumn] - ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DecimalColumn.Subtract(3)));
            Assert.AreEqual("[DecimalColumn] - ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DecimalColumn.Subtract(new DecimalParameterExpression(3))));
            Assert.AreEqual("[DecimalColumn] - ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DecimalColumn - 3));
            Assert.AreEqual("[DecimalColumn] - ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DecimalColumn - new DecimalParameterExpression(3)));
            Assert.AreEqual("[DecimalColumn] * ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DecimalColumn.Multiply(3)));
            Assert.AreEqual("[DecimalColumn] * ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DecimalColumn.Multiply(new DecimalParameterExpression(3))));
            Assert.AreEqual("[DecimalColumn] * ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DecimalColumn * 3));
            Assert.AreEqual("[DecimalColumn] * ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DecimalColumn * new DecimalParameterExpression(3)));
            Assert.AreEqual("[DecimalColumn] / ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DecimalColumn.Divide(3)));
            Assert.AreEqual("[DecimalColumn] / ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DecimalColumn.Divide(new DecimalParameterExpression(3))));
            Assert.AreEqual("[DecimalColumn] / ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DecimalColumn / 3));
            Assert.AreEqual("[DecimalColumn] / ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DecimalColumn / new DecimalParameterExpression(3)));
            Assert.AreEqual("[DecimalColumn] % ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DecimalColumn.Mod(3)));
            Assert.AreEqual("[DecimalColumn] % ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DecimalColumn.Mod(new DecimalParameterExpression(3))));
            Assert.AreEqual("[DecimalColumn] % ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DecimalColumn % 3));
            Assert.AreEqual("[DecimalColumn] % ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DecimalColumn % new DecimalParameterExpression(3)));

            Assert.AreEqual("COUNT([DecimalColumn])", ExpressionHelper.ToExpressionCacheableSql(_criteria.DecimalColumn.Count()));
            Assert.AreEqual("COUNT(DISTINCT [DecimalColumn])", ExpressionHelper.ToExpressionCacheableSql(_criteria.DecimalColumn.Count(true)));
            Assert.AreEqual("AVG([DecimalColumn])", ExpressionHelper.ToExpressionCacheableSql(_criteria.DecimalColumn.Avg()));
            Assert.AreEqual("MAX([DecimalColumn])", ExpressionHelper.ToExpressionCacheableSql(_criteria.DecimalColumn.Max()));
            Assert.AreEqual("MIN([DecimalColumn])", ExpressionHelper.ToExpressionCacheableSql(_criteria.DecimalColumn.Min()));
            Assert.AreEqual("SUM([DecimalColumn])", ExpressionHelper.ToExpressionCacheableSql(_criteria.DecimalColumn.Sum()));

            Assert.AreEqual("[DecimalColumn] IN (?, ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.DecimalColumn.In(new decimal[] { 2, 3 })));

            Assert.AreEqual("[DecimalColumn] AS [C]", _criteria.DecimalColumn.As("C").ToSelectColumnName());
        }

        [TestMethod]
        public void TestDoubleExpression()
        {
            Assert.AreEqual("[DoubleColumn] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DoubleColumn.Equals(2)));
            Assert.AreEqual("[DoubleColumn] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DoubleColumn.Equals(new DoubleParameterExpression(2))));
            Assert.AreEqual("[DoubleColumn] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DoubleColumn == 2));
            Assert.AreEqual("[DoubleColumn] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DoubleColumn == new DoubleParameterExpression(2)));
            Assert.AreEqual("[DoubleColumn] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DoubleColumn.NotEquals(2)));
            Assert.AreEqual("[DoubleColumn] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DoubleColumn.NotEquals(new DoubleParameterExpression(2))));
            Assert.AreEqual("[DoubleColumn] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DoubleColumn != 2));
            Assert.AreEqual("[DoubleColumn] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DoubleColumn != new DoubleParameterExpression(2)));

            Assert.AreEqual("([DoubleColumn] >= ? AND [DoubleColumn] <= ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.DoubleColumn.Between(1, 3, true, true)));
            Assert.AreEqual("([DoubleColumn] >= ? AND [DoubleColumn] < ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.DoubleColumn.Between(1, new DoubleParameterExpression(3), true, false)));
            Assert.AreEqual("([DoubleColumn] > ? AND [DoubleColumn] <= ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.DoubleColumn.Between(new DoubleParameterExpression(1), 3, false, true)));
            Assert.AreEqual("([DoubleColumn] > ? AND [DoubleColumn] < ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.DoubleColumn.Between(new DoubleParameterExpression(1), new DoubleParameterExpression(3), false, false)));

            Assert.AreEqual("[DoubleColumn] > ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DoubleColumn.GreaterThan(2)));
            Assert.AreEqual("[DoubleColumn] > ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DoubleColumn.GreaterThan(new DoubleParameterExpression(2))));
            Assert.AreEqual("[DoubleColumn] > ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DoubleColumn > 2));
            Assert.AreEqual("[DoubleColumn] > ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DoubleColumn > new DoubleParameterExpression(2)));
            Assert.AreEqual("[DoubleColumn] < ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DoubleColumn.LessThan(2)));
            Assert.AreEqual("[DoubleColumn] < ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DoubleColumn.LessThan(new DoubleParameterExpression(2))));
            Assert.AreEqual("[DoubleColumn] < ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DoubleColumn < 2));
            Assert.AreEqual("[DoubleColumn] < ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DoubleColumn < new DoubleParameterExpression(2)));
            Assert.AreEqual("[DoubleColumn] >= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DoubleColumn.GreaterThanOrEquals(2)));
            Assert.AreEqual("[DoubleColumn] >= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DoubleColumn.GreaterThanOrEquals(new DoubleParameterExpression(2))));
            Assert.AreEqual("[DoubleColumn] >= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DoubleColumn >= 2));
            Assert.AreEqual("[DoubleColumn] >= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DoubleColumn >= new DoubleParameterExpression(2)));
            Assert.AreEqual("[DoubleColumn] <= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DoubleColumn.LessThanOrEquals(2)));
            Assert.AreEqual("[DoubleColumn] <= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DoubleColumn.LessThanOrEquals(new DoubleParameterExpression(2))));
            Assert.AreEqual("[DoubleColumn] <= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DoubleColumn <= 2));
            Assert.AreEqual("[DoubleColumn] <= ?", ExpressionHelper.ToConditionCacheableSql(_criteria.DoubleColumn <= new DoubleParameterExpression(2)));

            Assert.AreEqual("[DoubleColumn] + ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DoubleColumn.Add(3)));
            Assert.AreEqual("[DoubleColumn] + ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DoubleColumn.Add(new DoubleParameterExpression(3))));
            Assert.AreEqual("[DoubleColumn] + ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DoubleColumn + 3));
            Assert.AreEqual("[DoubleColumn] + ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DoubleColumn + new DoubleParameterExpression(3)));
            Assert.AreEqual("[DoubleColumn] - ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DoubleColumn.Subtract(3)));
            Assert.AreEqual("[DoubleColumn] - ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DoubleColumn.Subtract(new DoubleParameterExpression(3))));
            Assert.AreEqual("[DoubleColumn] - ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DoubleColumn - 3));
            Assert.AreEqual("[DoubleColumn] - ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DoubleColumn - new DoubleParameterExpression(3)));
            Assert.AreEqual("[DoubleColumn] * ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DoubleColumn.Multiply(3)));
            Assert.AreEqual("[DoubleColumn] * ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DoubleColumn.Multiply(new DoubleParameterExpression(3))));
            Assert.AreEqual("[DoubleColumn] * ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DoubleColumn * 3));
            Assert.AreEqual("[DoubleColumn] * ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DoubleColumn * new DoubleParameterExpression(3)));
            Assert.AreEqual("[DoubleColumn] / ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DoubleColumn.Divide(3)));
            Assert.AreEqual("[DoubleColumn] / ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DoubleColumn.Divide(new DoubleParameterExpression(3))));
            Assert.AreEqual("[DoubleColumn] / ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DoubleColumn / 3));
            Assert.AreEqual("[DoubleColumn] / ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DoubleColumn / new DoubleParameterExpression(3)));
            Assert.AreEqual("[DoubleColumn] % ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DoubleColumn.Mod(3)));
            Assert.AreEqual("[DoubleColumn] % ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DoubleColumn.Mod(new DoubleParameterExpression(3))));
            Assert.AreEqual("[DoubleColumn] % ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DoubleColumn % 3));
            Assert.AreEqual("[DoubleColumn] % ?", ExpressionHelper.ToExpressionCacheableSql(_criteria.DoubleColumn % new DoubleParameterExpression(3)));

            Assert.AreEqual("COUNT([DoubleColumn])", ExpressionHelper.ToExpressionCacheableSql(_criteria.DoubleColumn.Count()));
            Assert.AreEqual("COUNT(DISTINCT [DoubleColumn])", ExpressionHelper.ToExpressionCacheableSql(_criteria.DoubleColumn.Count(true)));
            Assert.AreEqual("AVG([DoubleColumn])", ExpressionHelper.ToExpressionCacheableSql(_criteria.DoubleColumn.Avg()));
            Assert.AreEqual("MAX([DoubleColumn])", ExpressionHelper.ToExpressionCacheableSql(_criteria.DoubleColumn.Max()));
            Assert.AreEqual("MIN([DoubleColumn])", ExpressionHelper.ToExpressionCacheableSql(_criteria.DoubleColumn.Min()));
            Assert.AreEqual("SUM([DoubleColumn])", ExpressionHelper.ToExpressionCacheableSql(_criteria.DoubleColumn.Sum()));

            Assert.AreEqual("[DoubleColumn] IN (?, ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.DoubleColumn.In(new double[] { 2, 3 })));

            Assert.AreEqual("[DoubleColumn] AS [C]", _criteria.DoubleColumn.As("C").ToSelectColumnName());
        }

        [TestMethod]
        public void TestGuidExpression()
        {
            Assert.AreEqual("[GuidColumn] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.GuidColumn.Equals(Guid.NewGuid())));
            Assert.AreEqual("[GuidColumn] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.GuidColumn.Equals(new GuidParameterExpression(Guid.NewGuid()))));
            Assert.AreEqual("[GuidColumn] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.GuidColumn == Guid.NewGuid()));
            Assert.AreEqual("[GuidColumn] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.GuidColumn == new GuidParameterExpression(Guid.NewGuid())));
            Assert.AreEqual("[GuidColumn] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.GuidColumn.NotEquals(Guid.NewGuid())));
            Assert.AreEqual("[GuidColumn] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.GuidColumn.NotEquals(new GuidParameterExpression(Guid.NewGuid()))));
            Assert.AreEqual("[GuidColumn] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.GuidColumn != Guid.NewGuid()));
            Assert.AreEqual("[GuidColumn] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.GuidColumn != new GuidParameterExpression(Guid.NewGuid())));

            Assert.AreEqual("[GuidColumn] IN (?, ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.GuidColumn.In(new[] { Guid.NewGuid(), Guid.NewGuid() })));

            Assert.AreEqual("[GuidColumn] AS [C]", _criteria.GuidColumn.As("C").ToSelectColumnName());
        }

        [TestMethod]
        public void TestStringExpression()
        {
            Assert.AreEqual("[StringColumn] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.StringColumn.Equals("a")));
            Assert.AreEqual("[StringColumn] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.StringColumn.Equals(new StringParameterExpression("a", true))));
            Assert.AreEqual("[StringColumn] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.StringColumn == "a"));
            Assert.AreEqual("[StringColumn] = ?", ExpressionHelper.ToConditionCacheableSql(_criteria.StringColumn == new StringParameterExpression("a", true)));
            Assert.AreEqual("[StringColumn] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.StringColumn.NotEquals("a")));
            Assert.AreEqual("[StringColumn] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.StringColumn.NotEquals(new StringParameterExpression("a", true))));
            Assert.AreEqual("[StringColumn] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.StringColumn != "a"));
            Assert.AreEqual("[StringColumn] <> ?", ExpressionHelper.ToConditionCacheableSql(_criteria.StringColumn != new StringParameterExpression("a", true)));

            Assert.AreEqual("[StringColumn] LIKE ?", ExpressionHelper.ToConditionCacheableSql(_criteria.StringColumn.Like("%abc%")));
            Assert.AreEqual("[StringColumn] LIKE ?", ExpressionHelper.ToConditionCacheableSql(_criteria.StringColumn.Like(new StringParameterExpression("%abc%", true))));
            Assert.AreEqual("LOWER([StringColumn])", ExpressionHelper.ToExpressionCacheableSql(_criteria.StringColumn.ToLower()));
            Assert.AreEqual("UPPER([StringColumn])", ExpressionHelper.ToExpressionCacheableSql(_criteria.StringColumn.ToUpper()));
            Assert.AreEqual("TRIM([StringColumn])", ExpressionHelper.ToExpressionCacheableSql(_criteria.StringColumn.Trim()));

            Assert.AreEqual("[StringColumn] IN (?, ?)", ExpressionHelper.ToConditionCacheableSql(_criteria.StringColumn.In(new[] { "a", "a" })));

            Assert.AreEqual("[StringColumn] AS [C]", _criteria.StringColumn.As("C").ToSelectColumnName());
        }
    }
}
