using Microsoft.VisualStudio.TestTools.UnitTesting;
using NIntegrate.Query;
using NIntegrate.Test.Query.TestClasses;
using NIntegrate.Query.Command;

namespace NIntegrate.Test.Query
{
    /// <summary>
    /// Test QueryCommandFactory members
    /// </summary>
    [TestClass]
    public class QueryCommandFactoryTest
    {
        [TestMethod]
        public void TestQueryCommandFactory()
        {
            var criteria = new TestCriteria();
            var fac = new QueryCommandFactory(criteria);

            criteria.SetMaxResults(10);
            criteria.AddSortBy(criteria.Int32Column, true).AddSortBy(criteria.StringColumn, false);
            criteria.And(criteria.Int32Column == null).Or(criteria.StringColumn.Like("test"));

            var predefinedColumnsCmd = fac.GetQueryCommand();
            Assert.AreEqual("SELECT TOP 10 [BooleanColumn], [ByteColumn], [Int16Column], [Int32Column], [Int64Column], [DateTimeColumn], [StringColumn], [GuidColumn], [DoubleColumn], [DecimalColumn] FROM [TestTable] (NOLOCK) WHERE [Int32Column] IS NULL OR [StringColumn] LIKE @p1 ORDER BY [Int32Column] DESC, [StringColumn]", predefinedColumnsCmd.CommandText);
            Assert.AreEqual(1, predefinedColumnsCmd.Parameters.Count);
            Assert.AreEqual("SELECT COUNT(1) FROM [TestTable] (NOLOCK) WHERE [Int32Column] IS NULL OR [StringColumn] LIKE @p1", fac.GetCountCommand().CommandText);

            criteria.AddResultColumn((criteria.Int32Column + 1).As("ID")).AddResultColumn(criteria.StringColumn.As("Name"));
            var noPagingCmd = fac.GetQueryCommand();
            Assert.AreEqual("SELECT TOP 10 [Int32Column] + @p1 AS [ID], [StringColumn] AS [Name] FROM [TestTable] (NOLOCK) WHERE [Int32Column] IS NULL OR [StringColumn] LIKE @p2 ORDER BY [Int32Column] DESC, [StringColumn]", noPagingCmd.CommandText);
            Assert.AreEqual(2, noPagingCmd.Parameters.Count);
            Assert.AreEqual("SELECT COUNT(1) FROM [TestTable] (NOLOCK) WHERE [Int32Column] IS NULL OR [StringColumn] LIKE @p1", fac.GetCountCommand().CommandText);

            criteria.SetSkipResults(10);
            var pagingCmd = fac.GetQueryCommand();
            Assert.AreEqual("WITH [__T] AS (SELECT TOP 20 [Int32Column] + @p1 AS [ID], [StringColumn] AS [Name], ROW_NUMBER() OVER (ORDER BY [Int32Column] DESC, [StringColumn]) AS [__Pos] FROM [TestTable] (NOLOCK) WHERE [Int32Column] IS NULL OR [StringColumn] LIKE @p2) SELECT [__T].[ID], [__T].[Name] FROM [__T] WHERE [__T].[__Pos] > 10 AND [__T].[__Pos] <= 20", pagingCmd.CommandText);
            Assert.AreEqual(2, pagingCmd.Parameters.Count);
            Assert.AreEqual("SELECT COUNT(1) FROM [TestTable] (NOLOCK) WHERE [Int32Column] IS NULL OR [StringColumn] LIKE @p1", fac.GetCountCommand().CommandText);

            criteria.SetIsDistinct(true);

            pagingCmd = fac.GetQueryCommand();
            Assert.AreEqual("WITH [__T] AS (SELECT DISTINCT TOP 20 [Int32Column] + @p1 AS [ID], [StringColumn] AS [Name], ROW_NUMBER() OVER (ORDER BY [Int32Column] DESC, [StringColumn]) AS [__Pos] FROM [TestTable] (NOLOCK) WHERE [Int32Column] IS NULL OR [StringColumn] LIKE @p2) SELECT [__T].[ID], [__T].[Name] FROM [__T] WHERE [__T].[__Pos] > 10 AND [__T].[__Pos] <= 20", pagingCmd.CommandText);
            Assert.AreEqual(2, pagingCmd.Parameters.Count);
            Assert.AreEqual("SELECT COUNT(1) FROM (SELECT DISTINCT TOP 10 [Int32Column] + @p1 AS [ID], [StringColumn] AS [Name] FROM [TestTable] (NOLOCK) WHERE [Int32Column] IS NULL OR [StringColumn] LIKE @p2) [__T]", fac.GetCountCommand().CommandText);

            Assert.AreEqual(1, pagingCmd.Parameters[0].Value);
            Assert.AreEqual("test", pagingCmd.Parameters[1].Value);

            pagingCmd.Parameters[0].Value = null;

            pagingCmd = fac.GetQueryCommand();
            Assert.AreEqual(1, pagingCmd.Parameters[0].Value);
            Assert.AreEqual("test", pagingCmd.Parameters[1].Value);

            Assert.AreNotEqual(pagingCmd, fac.GetQueryCommand());
        }
    }
}
