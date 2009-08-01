using Microsoft.VisualStudio.TestTools.UnitTesting;
using NIntegrate.Data;
using NIntegrate.Test.Query.TestClasses;

namespace NIntegrate.Test.Query
{
    /// <summary>
    /// Test QueryCommandFactory members
    /// </summary>
    [TestClass]
    public class QueryCommandFactoryTest
    {
        [TestMethod]
        public void TestSqlQueryCommandFactory()
        {
            var table = new TestTable();
            var criteria = table.CreateCriteria();
            var fac = new QueryCommandFactory();

            criteria.SetMaxResults(10);
            criteria.AddSortBy(table.Int32Column, true).AddSortBy(table.StringColumn, false);
            criteria.And(table.Int32Column == null).Or(table.StringColumn.Like("test"));

            var predefinedColumnsCmd = fac.CreateCommand(criteria);
            Assert.AreEqual("SELECT TOP 10 [BooleanColumn], [ByteColumn], [Int16Column], [Int32Column], [Int64Column], [DateTimeColumn], [StringColumn], [GuidColumn], [DoubleColumn], [DecimalColumn] FROM [TestTable] (NOLOCK) WHERE [Int32Column] IS NULL OR [StringColumn] LIKE @p1 ORDER BY [Int32Column] DESC, [StringColumn]", predefinedColumnsCmd.CommandText);
            Assert.AreEqual(1, predefinedColumnsCmd.Parameters.Count);
            Assert.AreEqual("SELECT COUNT(1) FROM [TestTable] (NOLOCK) WHERE [Int32Column] IS NULL OR [StringColumn] LIKE @p1", fac.CreateCountCommand(criteria).CommandText);

            criteria.Select((table.Int32Column + 1).As("ID"), (table.StringColumn.As("Name")));
            var noPagingCmd = fac.CreateCommand(criteria);
            Assert.AreEqual("SELECT TOP 10 [Int32Column] + @p1 AS [ID], [StringColumn] AS [Name] FROM [TestTable] (NOLOCK) WHERE [Int32Column] IS NULL OR [StringColumn] LIKE @p2 ORDER BY [Int32Column] DESC, [StringColumn]", noPagingCmd.CommandText);
            Assert.AreEqual(2, noPagingCmd.Parameters.Count);
            Assert.AreEqual("SELECT COUNT(1) FROM [TestTable] (NOLOCK) WHERE [Int32Column] IS NULL OR [StringColumn] LIKE @p1", fac.CreateCountCommand(criteria).CommandText);

            criteria.SetSkipResults(10);
            var pagingCmd = fac.CreateCommand(criteria);
            Assert.AreEqual("WITH [__T] AS (SELECT TOP 20 [Int32Column] + @p1 AS [ID], [StringColumn] AS [Name], ROW_NUMBER() OVER (ORDER BY [Int32Column] DESC, [StringColumn]) AS [__Pos] FROM [TestTable] (NOLOCK) WHERE [Int32Column] IS NULL OR [StringColumn] LIKE @p2) SELECT [__T].[ID], [__T].[Name] FROM [__T] WHERE [__T].[__Pos] > 10 AND [__T].[__Pos] <= 20", pagingCmd.CommandText);
            Assert.AreEqual(2, pagingCmd.Parameters.Count);
            Assert.AreEqual("SELECT COUNT(1) FROM [TestTable] (NOLOCK) WHERE [Int32Column] IS NULL OR [StringColumn] LIKE @p1", fac.CreateCountCommand(criteria).CommandText);

            criteria.SetIsDistinct(true);

            pagingCmd = fac.CreateCommand(criteria);
            Assert.AreEqual("WITH [__T] AS (SELECT DISTINCT TOP 20 [Int32Column] + @p1 AS [ID], [StringColumn] AS [Name], ROW_NUMBER() OVER (ORDER BY [Int32Column] DESC, [StringColumn]) AS [__Pos] FROM [TestTable] (NOLOCK) WHERE [Int32Column] IS NULL OR [StringColumn] LIKE @p2) SELECT [__T].[ID], [__T].[Name] FROM [__T] WHERE [__T].[__Pos] > 10 AND [__T].[__Pos] <= 20", pagingCmd.CommandText);
            Assert.AreEqual(2, pagingCmd.Parameters.Count);
            Assert.AreEqual("SELECT COUNT(1) FROM (SELECT DISTINCT [Int32Column] + @p1 AS [ID], [StringColumn] AS [Name] FROM [TestTable] (NOLOCK) WHERE [Int32Column] IS NULL OR [StringColumn] LIKE @p2) [__T]", fac.CreateCountCommand(criteria).CommandText);

            Assert.AreEqual(1, pagingCmd.Parameters[0].Value);
            Assert.AreEqual("test", pagingCmd.Parameters[1].Value);

            pagingCmd.Parameters[0].Value = null;

            pagingCmd = fac.CreateCommand(criteria);
            Assert.AreEqual(1, pagingCmd.Parameters[0].Value);
            Assert.AreEqual("test", pagingCmd.Parameters[1].Value);

            Assert.AreNotEqual(pagingCmd, fac.CreateCommand(criteria));
        }

        [TestMethod]
        public void TestOracleQueryCommandFactory()
        {
            var table = new OracleJobTable();
            var criteria = table.CreateCriteria();
            var fac = new QueryCommandFactory();

            criteria.AddSortBy(table.JOB_ID, true).AddSortBy(table.JOB_TITLE, false);
            criteria.And(table.JOB_ID == null).Or(table.JOB_TITLE.Like("test"));

            var predefinedColumnsCmd = fac.CreateCommand(criteria);
            Assert.AreEqual("SELECT \"JOB_ID\", \"JOB_TITLE\", \"MIN_SALARY\", \"MAX_SALARY\" FROM \"HR\".\"JOBS\" WHERE \"JOB_ID\" IS NULL OR \"JOB_TITLE\" LIKE :p1 ORDER BY \"JOB_ID\" DESC, \"JOB_TITLE\"", predefinedColumnsCmd.CommandText);
            Assert.AreEqual(1, predefinedColumnsCmd.Parameters.Count);
            Assert.AreEqual("SELECT COUNT(1) FROM \"HR\".\"JOBS\" WHERE \"JOB_ID\" IS NULL OR \"JOB_TITLE\" LIKE :p1", fac.CreateCountCommand(criteria).CommandText);

            criteria.SetMaxResults(10);
            criteria.Select((table.JOB_ID + 1).As("ID"), table.JOB_TITLE.As("Name"));
            var noPagingCmd = fac.CreateCommand(criteria);
            Assert.AreEqual("SELECT \"__T\".\"ID\", \"__T\".\"Name\" FROM (SELECT \"JOB_ID\" + :p1 AS \"ID\", \"JOB_TITLE\" AS \"Name\", ROW_NUMBER() OVER (ORDER BY \"JOB_ID\" DESC, \"JOB_TITLE\") AS \"__Pos\" FROM \"HR\".\"JOBS\" WHERE \"JOB_ID\" IS NULL OR \"JOB_TITLE\" LIKE :p2) \"__T\" WHERE \"__T\".\"__Pos\" > 0 AND \"__T\".\"__Pos\" <= 10", noPagingCmd.CommandText);
            Assert.AreEqual(2, noPagingCmd.Parameters.Count);
            Assert.AreEqual("SELECT COUNT(1) FROM \"HR\".\"JOBS\" WHERE \"JOB_ID\" IS NULL OR \"JOB_TITLE\" LIKE :p1", fac.CreateCountCommand(criteria).CommandText);

            criteria.SetSkipResults(10);
            var pagingCmd = fac.CreateCommand(criteria);
            Assert.AreEqual("SELECT \"__T\".\"ID\", \"__T\".\"Name\" FROM (SELECT \"JOB_ID\" + :p1 AS \"ID\", \"JOB_TITLE\" AS \"Name\", ROW_NUMBER() OVER (ORDER BY \"JOB_ID\" DESC, \"JOB_TITLE\") AS \"__Pos\" FROM \"HR\".\"JOBS\" WHERE \"JOB_ID\" IS NULL OR \"JOB_TITLE\" LIKE :p2) \"__T\" WHERE \"__T\".\"__Pos\" > 10 AND \"__T\".\"__Pos\" <= 20", pagingCmd.CommandText);
            Assert.AreEqual(2, pagingCmd.Parameters.Count);
            Assert.AreEqual("SELECT COUNT(1) FROM \"HR\".\"JOBS\" WHERE \"JOB_ID\" IS NULL OR \"JOB_TITLE\" LIKE :p1", fac.CreateCountCommand(criteria).CommandText);

            criteria.SetIsDistinct(true);

            pagingCmd = fac.CreateCommand(criteria);
            Assert.AreEqual("SELECT \"__T\".\"ID\", \"__T\".\"Name\" FROM (SELECT DISTINCT \"JOB_ID\" + :p1 AS \"ID\", \"JOB_TITLE\" AS \"Name\", ROW_NUMBER() OVER (ORDER BY \"JOB_ID\" DESC, \"JOB_TITLE\") AS \"__Pos\" FROM \"HR\".\"JOBS\" WHERE \"JOB_ID\" IS NULL OR \"JOB_TITLE\" LIKE :p2) \"__T\" WHERE \"__T\".\"__Pos\" > 10 AND \"__T\".\"__Pos\" <= 20", pagingCmd.CommandText);
            Assert.AreEqual(2, pagingCmd.Parameters.Count);
            Assert.AreEqual("SELECT COUNT(1) FROM (SELECT DISTINCT \"JOB_ID\" + :p1 AS \"ID\", \"JOB_TITLE\" AS \"Name\" FROM \"HR\".\"JOBS\" WHERE \"JOB_ID\" IS NULL OR \"JOB_TITLE\" LIKE :p2) \"__T\"", fac.CreateCountCommand(criteria).CommandText);

            Assert.AreEqual(1, pagingCmd.Parameters[0].Value);
            Assert.AreEqual("test", pagingCmd.Parameters[1].Value);

            pagingCmd.Parameters[0].Value = null;

            pagingCmd = fac.CreateCommand(criteria);
            Assert.AreEqual(1, pagingCmd.Parameters[0].Value);
            Assert.AreEqual("test", pagingCmd.Parameters[1].Value);

            Assert.AreNotEqual(pagingCmd, fac.CreateCommand(criteria));
        }
    }
}
