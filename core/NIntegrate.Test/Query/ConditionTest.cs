using System;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NIntegrate.Test.Query.TestClasses;
using NIntegrate.Data;

namespace NIntegrate.Test.Query
{
    /// <summary>
    /// Test condition class members
    /// </summary>
    [TestClass]
    public class ConditionTest
    {
        private readonly TestTable _table = new TestTable();

        [TestMethod]
        public void TestConstructor()
        {
            Assert.AreEqual("[BooleanColumn] = NULL", new Condition(_table.BooleanColumn, ExpressionOperator.Equals, NullExpression.Value).ToConditionCacheableSql());
            Assert.AreEqual("[ByteColumn] <> NULL", new Condition(_table.ByteColumn, ExpressionOperator.NotEquals, NullExpression.Value).ToConditionCacheableSql());
            Assert.AreEqual("[Int16Column] > NULL", new Condition(_table.Int16Column, ExpressionOperator.GreaterThan, NullExpression.Value).ToConditionCacheableSql());
            Assert.AreEqual("[Int32Column] >= NULL", new Condition(_table.Int32Column, ExpressionOperator.GreaterThanOrEquals, NullExpression.Value).ToConditionCacheableSql());
            Assert.AreEqual("[Int64Column] < NULL", new Condition(_table.Int64Column, ExpressionOperator.LessThan, NullExpression.Value).ToConditionCacheableSql());
            Assert.AreEqual("[DateTimeColumn] <= NULL", new Condition(_table.DateTimeColumn, ExpressionOperator.LessThanOrEquals, NullExpression.Value).ToConditionCacheableSql());
            Assert.AreEqual("[StringColumn] LIKE NULL", new Condition(_table.StringColumn, ExpressionOperator.Like, NullExpression.Value).ToConditionCacheableSql());
            Assert.AreEqual("[GuidColumn] IS NULL", new Condition(_table.GuidColumn, ExpressionOperator.Is, NullExpression.Value).ToConditionCacheableSql());
            Assert.AreEqual("[DoubleColumn] IS NOT NULL", new Condition(_table.DoubleColumn, ExpressionOperator.IsNot, NullExpression.Value).ToConditionCacheableSql());
            Assert.AreEqual("[DecimalColumn] IN NULL", new Condition(_table.DecimalColumn, ExpressionOperator.In, NullExpression.Value).ToConditionCacheableSql());
        }

        [TestMethod]
        public void TestAndOrNotClone()
        {
            var condition = _table.Int32Column == 1 && _table.StringColumn.Like("%abc%")
                || (!(_table.DateTimeColumn < DateTime.Now || _table.GuidColumn != new Guid()));
            Assert.AreEqual("([Int32Column] = ? AND [StringColumn] LIKE ? OR NOT ([DateTimeColumn] < ? OR [GuidColumn] <> ?))", condition.ToConditionCacheableSql());
            Assert.AreEqual(2, condition.LinkedConditionAndOrs.Count);
            Assert.AreEqual(2, condition.LinkedConditions.Count);
            var clone = (Condition)condition.Clone();
            Assert.IsFalse(ReferenceEquals(condition, clone));
            Assert.AreEqual(condition.ToConditionCacheableSql(), clone.ToConditionCacheableSql());
        }

        [TestMethod]
        public void TestConditionSerialization()
        {
            var condition = new Condition(
                new Int32ParameterExpression(2),
                ExpressionOperator.GreaterThan,
                new Int32ParameterExpression(1)
            );
            var combinedCondition = condition.And(condition.Or(condition));

            var serializer = new DataContractSerializer(typeof(Condition));
            var stream = new MemoryStream();
            serializer.WriteObject(stream, combinedCondition);
            stream.Seek(0, SeekOrigin.Begin);
            var deserializedCombinedCondition = (Condition)serializer.ReadObject(stream);
            Assert.AreEqual("(? > ? AND (? > ? OR ? > ?))", deserializedCombinedCondition.ToConditionCacheableSql());
        }
    }
}
