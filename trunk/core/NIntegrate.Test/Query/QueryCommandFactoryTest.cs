using System;
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
            var criteria = table.Select();
            var fac = new QueryCommandFactory();

            criteria.SetMaxResults(10);
            criteria.SortBy(table.Int32Column, true).ThenSortBy(table.StringColumn, false);
            criteria.And(table.Int32Column == null).Or(table.StringColumn.Like("test"));

            var predefinedColumnsCmd = fac.CreateCommand(criteria, false);
            Assert.AreEqual("SELECT TOP 10 [BooleanColumn], [ByteColumn], [Int16Column], [Int32Column], [Int64Column], [DateTimeColumn], [StringColumn], [GuidColumn], [DoubleColumn], [DecimalColumn] FROM [TestTable] (NOLOCK) WHERE [Int32Column] IS NULL OR [StringColumn] LIKE @p1 ORDER BY [Int32Column] DESC, [StringColumn]", predefinedColumnsCmd.CommandText);
            Assert.AreEqual(1, predefinedColumnsCmd.Parameters.Count);
            Assert.AreEqual("SELECT COUNT(1) FROM [TestTable] (NOLOCK) WHERE [Int32Column] IS NULL OR [StringColumn] LIKE @p1", fac.CreateCommand(criteria, true).CommandText);

            criteria.Select((table.Int32Column + 1).As("ID"), (table.StringColumn.As("Name")));
            var noPagingCmd = fac.CreateCommand(criteria, false);
            Assert.AreEqual("SELECT TOP 10 [Int32Column] + @p1 AS [ID], [StringColumn] AS [Name] FROM [TestTable] (NOLOCK) WHERE [Int32Column] IS NULL OR [StringColumn] LIKE @p2 ORDER BY [Int32Column] DESC, [StringColumn]", noPagingCmd.CommandText);
            Assert.AreEqual(2, noPagingCmd.Parameters.Count);
            Assert.AreEqual("SELECT COUNT(1) FROM [TestTable] (NOLOCK) WHERE [Int32Column] IS NULL OR [StringColumn] LIKE @p1", fac.CreateCommand(criteria, true).CommandText);

            criteria.SetSkipResults(10);
            var pagingCmd = fac.CreateCommand(criteria, false);
            Assert.AreEqual("WITH [__T] AS (SELECT TOP 20 [Int32Column] + @p1 AS [ID], [StringColumn] AS [Name], ROW_NUMBER() OVER (ORDER BY [Int32Column] DESC, [StringColumn]) AS [__Pos] FROM [TestTable] (NOLOCK) WHERE [Int32Column] IS NULL OR [StringColumn] LIKE @p2) SELECT [__T].[ID], [__T].[Name] FROM [__T] WHERE [__T].[__Pos] > 10 AND [__T].[__Pos] <= 20", pagingCmd.CommandText);
            Assert.AreEqual(2, pagingCmd.Parameters.Count);
            Assert.AreEqual("SELECT COUNT(1) FROM [TestTable] (NOLOCK) WHERE [Int32Column] IS NULL OR [StringColumn] LIKE @p1", fac.CreateCommand(criteria, true).CommandText);

            criteria.SetIsDistinct(true);

            pagingCmd = fac.CreateCommand(criteria, false);
            Assert.AreEqual("WITH [__T] AS (SELECT DISTINCT TOP 20 [Int32Column] + @p1 AS [ID], [StringColumn] AS [Name], ROW_NUMBER() OVER (ORDER BY [Int32Column] DESC, [StringColumn]) AS [__Pos] FROM [TestTable] (NOLOCK) WHERE [Int32Column] IS NULL OR [StringColumn] LIKE @p2) SELECT [__T].[ID], [__T].[Name] FROM [__T] WHERE [__T].[__Pos] > 10 AND [__T].[__Pos] <= 20", pagingCmd.CommandText);
            Assert.AreEqual(2, pagingCmd.Parameters.Count);
            Assert.AreEqual("SELECT COUNT(1) FROM (SELECT DISTINCT [Int32Column] + @p1 AS [ID], [StringColumn] AS [Name] FROM [TestTable] (NOLOCK) WHERE [Int32Column] IS NULL OR [StringColumn] LIKE @p2) [__T]", fac.CreateCommand(criteria, true).CommandText);

            Assert.AreEqual(1, pagingCmd.Parameters[0].Value);
            Assert.AreEqual("test", pagingCmd.Parameters[1].Value);

            pagingCmd.Parameters[0].Value = null;

            pagingCmd = fac.CreateCommand(criteria, false);
            Assert.AreEqual(1, pagingCmd.Parameters[0].Value);
            Assert.AreEqual("test", pagingCmd.Parameters[1].Value);

            Assert.AreNotEqual(pagingCmd, fac.CreateCommand(criteria, false));


            var insertCriteria =
                table.Insert(table.Int32Column.Set(1), table.DateTimeColumn.Set(DateTime.Now));
            var insertCmd = fac.CreateCommand(insertCriteria, false);
            Assert.AreEqual("INSERT INTO [TestTable] ([Int32Column], [DateTimeColumn]) VALUES (@p1, @p2)", insertCmd.CommandText);
            Assert.AreEqual(2, insertCmd.Parameters.Count);

            var updateCriteria =
                table.Update(table.Int32Column.Set(1), table.DateTimeColumn.Set(DateTime.Now)).Where(table.GuidColumn == Guid.NewGuid());
            var updateCmd = fac.CreateCommand(updateCriteria, false);
            Assert.AreEqual("UPDATE [TestTable] SET [Int32Column] = @p1, [DateTimeColumn] = @p2 WHERE [GuidColumn] = @p3", updateCmd.CommandText);
            Assert.AreEqual(3, updateCmd.Parameters.Count);

            var deleteCriteria =
                table.Delete(table.GuidColumn == Guid.NewGuid());
            var deleteCmd = fac.CreateCommand(deleteCriteria, false);
            Assert.AreEqual("DELETE FROM [TestTable] WHERE [GuidColumn] = @p1", deleteCmd.CommandText);
            Assert.AreEqual(1, deleteCmd.Parameters.Count);
        }

        [TestMethod]
        public void TestOracleQueryCommandFactory()
        {
            var table = new OracleJobTable();
            var criteria = table.Select();
            var fac = new QueryCommandFactory();

            criteria.SortBy(table.JOB_ID, true).ThenSortBy(table.JOB_TITLE, false);
            criteria.And(table.JOB_ID == null).Or(table.JOB_TITLE.Like("test"));

            var predefinedColumnsCmd = fac.CreateCommand(criteria, false);
            Assert.AreEqual("SELECT \"JOB_ID\", \"JOB_TITLE\", \"MIN_SALARY\", \"MAX_SALARY\" FROM \"HR\".\"JOBS\" WHERE \"JOB_ID\" IS NULL OR \"JOB_TITLE\" LIKE :p1 ORDER BY \"JOB_ID\" DESC, \"JOB_TITLE\"", predefinedColumnsCmd.CommandText);
            Assert.AreEqual(1, predefinedColumnsCmd.Parameters.Count);
            Assert.AreEqual("SELECT COUNT(1) FROM \"HR\".\"JOBS\" WHERE \"JOB_ID\" IS NULL OR \"JOB_TITLE\" LIKE :p1", fac.CreateCommand(criteria, true).CommandText);

            criteria.SetMaxResults(10);
            criteria.Select((table.JOB_ID + 1).As("ID"), table.JOB_TITLE.As("Name"));
            var noPagingCmd = fac.CreateCommand(criteria, false);
            Assert.AreEqual("SELECT \"__T\".\"ID\", \"__T\".\"Name\" FROM (SELECT \"JOB_ID\" + :p1 AS \"ID\", \"JOB_TITLE\" AS \"Name\", ROW_NUMBER() OVER (ORDER BY \"JOB_ID\" DESC, \"JOB_TITLE\") AS \"__Pos\" FROM \"HR\".\"JOBS\" WHERE \"JOB_ID\" IS NULL OR \"JOB_TITLE\" LIKE :p2) \"__T\" WHERE \"__T\".\"__Pos\" > 0 AND \"__T\".\"__Pos\" <= 10", noPagingCmd.CommandText);
            Assert.AreEqual(2, noPagingCmd.Parameters.Count);
            Assert.AreEqual("SELECT COUNT(1) FROM \"HR\".\"JOBS\" WHERE \"JOB_ID\" IS NULL OR \"JOB_TITLE\" LIKE :p1", fac.CreateCommand(criteria, true).CommandText);

            criteria.SetSkipResults(10);
            var pagingCmd = fac.CreateCommand(criteria, false);
            Assert.AreEqual("SELECT \"__T\".\"ID\", \"__T\".\"Name\" FROM (SELECT \"JOB_ID\" + :p1 AS \"ID\", \"JOB_TITLE\" AS \"Name\", ROW_NUMBER() OVER (ORDER BY \"JOB_ID\" DESC, \"JOB_TITLE\") AS \"__Pos\" FROM \"HR\".\"JOBS\" WHERE \"JOB_ID\" IS NULL OR \"JOB_TITLE\" LIKE :p2) \"__T\" WHERE \"__T\".\"__Pos\" > 10 AND \"__T\".\"__Pos\" <= 20", pagingCmd.CommandText);
            Assert.AreEqual(2, pagingCmd.Parameters.Count);
            Assert.AreEqual("SELECT COUNT(1) FROM \"HR\".\"JOBS\" WHERE \"JOB_ID\" IS NULL OR \"JOB_TITLE\" LIKE :p1", fac.CreateCommand(criteria, true).CommandText);

            criteria.SetIsDistinct(true);

            pagingCmd = fac.CreateCommand(criteria, false);
            Assert.AreEqual("SELECT \"__T\".\"ID\", \"__T\".\"Name\" FROM (SELECT DISTINCT \"JOB_ID\" + :p1 AS \"ID\", \"JOB_TITLE\" AS \"Name\", ROW_NUMBER() OVER (ORDER BY \"JOB_ID\" DESC, \"JOB_TITLE\") AS \"__Pos\" FROM \"HR\".\"JOBS\" WHERE \"JOB_ID\" IS NULL OR \"JOB_TITLE\" LIKE :p2) \"__T\" WHERE \"__T\".\"__Pos\" > 10 AND \"__T\".\"__Pos\" <= 20", pagingCmd.CommandText);
            Assert.AreEqual(2, pagingCmd.Parameters.Count);
            Assert.AreEqual("SELECT COUNT(1) FROM (SELECT DISTINCT \"JOB_ID\" + :p1 AS \"ID\", \"JOB_TITLE\" AS \"Name\" FROM \"HR\".\"JOBS\" WHERE \"JOB_ID\" IS NULL OR \"JOB_TITLE\" LIKE :p2) \"__T\"", fac.CreateCommand(criteria, true).CommandText);

            Assert.AreEqual(1, pagingCmd.Parameters[0].Value);
            Assert.AreEqual("test", pagingCmd.Parameters[1].Value);

            pagingCmd.Parameters[0].Value = null;

            pagingCmd = fac.CreateCommand(criteria, false);
            Assert.AreEqual(1, pagingCmd.Parameters[0].Value);
            Assert.AreEqual("test", pagingCmd.Parameters[1].Value);

            Assert.AreNotEqual(pagingCmd, fac.CreateCommand(criteria, false));
        }
    }
}
