﻿using System;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NIntegrate.Data;
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
            var table = new TestTable();
            var criteria = table.Select(table.Int32Column, table.StringColumn, table.StringColumn);
            Assert.AreEqual(2, criteria.ResultColumns.Count);

            criteria.SetIsDistinct(true);
            Assert.IsTrue(criteria.IsDistinct);
            criteria.SetMaxResults(10).SetMaxResults(10);
            Assert.AreEqual(10, criteria.MaxResults);
            criteria.SetSkipResults(10).SetSkipResults(10);
            Assert.AreEqual(10, criteria.SkipResults);

            criteria.SortBy(table.Int32Column, true).ThenSortBy(table.StringColumn, false);
            Assert.AreEqual(2, criteria.SortBys.Count);

            criteria.And(table.Int32Column == 1 && table.StringColumn.Like("%abc%"))
                .Or(!(table.DateTimeColumn < DateTime.Now || table.GuidColumn != new Guid()));
            Assert.AreEqual(2, criteria.Conditions.Count);
            Assert.AreEqual(2, criteria.ConditionAndOrs.Count);

            var serializer = new DataContractSerializer(typeof(QueryCriteria));
            var stream = new MemoryStream();
            serializer.WriteObject(stream, criteria);
            stream.Seek(0, SeekOrigin.Begin);
            var deserializedCriteria = (QueryCriteria)serializer.ReadObject(stream);
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
