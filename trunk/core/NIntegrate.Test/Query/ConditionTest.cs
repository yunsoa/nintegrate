using System;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NIntegrate.Test.Query.TestClasses;
using NIntegrate.Query;

namespace NIntegrate.Test.Query
{
    /// <summary>
    /// Test condition class members
    /// </summary>
    [TestClass]
    public class ConditionTest
    {
        private readonly TestCriteria _criteria = new TestCriteria();

        [TestMethod]
        public void TestConstructor()
        {
            Assert.AreEqual("[BooleanColumn] = NULL", QueryHelper.ToConditionCacheableSql(new Condition(_criteria.BooleanColumn, ExpressionOperator.Equals, NullExpression.Value)));
            Assert.AreEqual("[ByteColumn] <> NULL", QueryHelper.ToConditionCacheableSql(new Condition(_criteria.ByteColumn, ExpressionOperator.NotEquals, NullExpression.Value)));
            Assert.AreEqual("[Int16Column] > NULL", QueryHelper.ToConditionCacheableSql(new Condition(_criteria.Int16Column, ExpressionOperator.GreaterThan, NullExpression.Value)));
            Assert.AreEqual("[Int32Column] >= NULL", QueryHelper.ToConditionCacheableSql(new Condition(_criteria.Int32Column, ExpressionOperator.GreaterThanOrEquals, NullExpression.Value)));
            Assert.AreEqual("[Int64Column] < NULL", QueryHelper.ToConditionCacheableSql(new Condition(_criteria.Int64Column, ExpressionOperator.LessThan, NullExpression.Value)));
            Assert.AreEqual("[DateTimeColumn] <= NULL", QueryHelper.ToConditionCacheableSql(new Condition(_criteria.DateTimeColumn, ExpressionOperator.LessThanOrEquals, NullExpression.Value)));
            Assert.AreEqual("[StringColumn] LIKE NULL", QueryHelper.ToConditionCacheableSql(new Condition(_criteria.StringColumn, ExpressionOperator.Like, NullExpression.Value)));
            Assert.AreEqual("[GuidColumn] IS NULL", QueryHelper.ToConditionCacheableSql(new Condition(_criteria.GuidColumn, ExpressionOperator.Is, NullExpression.Value)));
            Assert.AreEqual("[DoubleColumn] IS NOT NULL", QueryHelper.ToConditionCacheableSql(new Condition(_criteria.DoubleColumn, ExpressionOperator.IsNot, NullExpression.Value)));
            Assert.AreEqual("[DecimalColumn] IN NULL", QueryHelper.ToConditionCacheableSql(new Condition(_criteria.DecimalColumn, ExpressionOperator.In, NullExpression.Value)));
        }

        [TestMethod]
        public void TestAndOrNotClone()
        {
            var condition = _criteria.Int32Column == 1 && _criteria.StringColumn.Like("%abc%")
                || (!(_criteria.DateTimeColumn < DateTime.Now || _criteria.GuidColumn != new Guid()));
            Assert.AreEqual("([Int32Column] = ? AND [StringColumn] LIKE ? OR NOT ([DateTimeColumn] < ? OR [GuidColumn] <> ?))", QueryHelper.ToConditionCacheableSql(condition));
            Assert.AreEqual(2, condition.LinkedConditionAndOrs.Count);
            Assert.AreEqual(2, condition.LinkedConditions.Count);
            var clone = (Condition)condition.Clone();
            Assert.IsFalse(ReferenceEquals(condition, clone));
            Assert.AreEqual(QueryHelper.ToConditionCacheableSql(condition), QueryHelper.ToConditionCacheableSql(clone));
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
            Assert.AreEqual("(? > ? AND (? > ? OR ? > ?))", QueryHelper.ToConditionCacheableSql(deserializedCombinedCondition));
            Assert.AreEqual(2, (deserializedCombinedCondition.LinkedConditions[0].LeftExpression as Int32ParameterExpression).Value);
            Assert.AreEqual(ConditionAndOr.And, (deserializedCombinedCondition.LinkedConditionAndOrs[0]));
        }
    }
}
