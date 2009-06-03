using System;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NIntegrate.Test.Query.TestClasses;

namespace NIntegrate.Test.Query
{
    /// <summary>
    /// Test criteria class members
    /// </summary>
    [TestClass]
    public class CriteriaTest
    {
        [TestMethod]
        public void TestCriteriaMembers()
        {
            var criteria = new TestCriteria();
            criteria.AddResultColumn(criteria.Int32Column).AddResultColumn(criteria.StringColumn).AddResultColumn(criteria.StringColumn);
            Assert.AreEqual(2, criteria.ResultColumns.Count);

            criteria.SetIsDistinct(true);
            Assert.IsTrue(criteria.IsDistinct);
            criteria.SetMaxResults(10).SetMaxResults(10);
            Assert.AreEqual(10, criteria.MaxResults);
            criteria.SetSkipResults(10).SetSkipResults(10);
            Assert.AreEqual(10, criteria.SkipResults);

            criteria.AddSortBy(criteria.Int32Column, true).AddSortBy(criteria.StringColumn, false);
            Assert.AreEqual(2, criteria.SortBys.Count);

            criteria.And(criteria.Int32Column == 1 && criteria.StringColumn.Like("%abc%"))
                .Or(!(criteria.DateTimeColumn < DateTime.Now || criteria.GuidColumn != new Guid()));
            Assert.AreEqual(2, criteria.Conditions.Count);
            Assert.AreEqual(2, criteria.ConditionAndOrs.Count);

            var serializer = new DataContractSerializer(typeof(TestCriteria));
            var stream = new MemoryStream();
            serializer.WriteObject(stream, criteria);
            stream.Seek(0, SeekOrigin.Begin);
            var deserializedCriteria = (TestCriteria)serializer.ReadObject(stream);
            Assert.AreEqual(criteria.ConditionAndOrs.Count, deserializedCriteria.ConditionAndOrs.Count);
            Assert.AreEqual(criteria.Conditions.Count, deserializedCriteria.Conditions.Count);
            Assert.AreEqual(criteria.IsDistinct, deserializedCriteria.IsDistinct);
            Assert.AreEqual(criteria.MaxResults, deserializedCriteria.MaxResults);
            Assert.AreEqual(criteria.ResultColumns.Count, deserializedCriteria.ResultColumns.Count);
            Assert.AreEqual(criteria.SkipResults, deserializedCriteria.SkipResults);
            Assert.AreEqual(criteria.SortBys.Count, deserializedCriteria.SortBys.Count);
        }
    }
}
