using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NIntegrate.Test.Query.TestClasses;
using NIntegrate.Data;

namespace NIntegrate.Test.Query
{
    /// <summary>
    /// Test all column class members
    /// </summary>
    [TestClass]
    public class ColumnsTest
    {
        private readonly TestTable _criteria = new TestTable();

        [TestMethod]
        public void TestColumnName()
        {
            Assert.AreEqual("BooleanColumn", _criteria.BooleanColumn.ColumnName);
            Assert.AreEqual("ByteColumn", _criteria.ByteColumn.ColumnName);
            Assert.AreEqual("Int16Column", _criteria.Int16Column.ColumnName);
            Assert.AreEqual("Int32Column", _criteria.Int32Column.ColumnName);
            Assert.AreEqual("Int64Column", _criteria.Int64Column.ColumnName);
            Assert.AreEqual("DateTimeColumn", _criteria.DateTimeColumn.ColumnName);
            Assert.AreEqual("StringColumn", _criteria.StringColumn.ColumnName);
            Assert.AreEqual("GuidColumn", _criteria.GuidColumn.ColumnName);
            Assert.AreEqual("DoubleColumn", _criteria.DoubleColumn.ColumnName);
            Assert.AreEqual("DecimalColumn", _criteria.DecimalColumn.ColumnName);
        }

        [TestMethod]
        public void TestClone()
        {
            Assert.AreEqual("BooleanColumn", ((BooleanColumn)_criteria.BooleanColumn.Clone()).ColumnName);
            Assert.AreEqual("ByteColumn", ((ByteColumn)_criteria.ByteColumn.Clone()).ColumnName);
            Assert.AreEqual("Int16Column", ((Int16Column)_criteria.Int16Column.Clone()).ColumnName);
            Assert.AreEqual("Int32Column", ((Int32Column)_criteria.Int32Column.Clone()).ColumnName);
            Assert.AreEqual("Int64Column", ((Int64Column)_criteria.Int64Column.Clone()).ColumnName);
            Assert.AreEqual("DateTimeColumn", ((DateTimeColumn)_criteria.DateTimeColumn.Clone()).ColumnName);
            Assert.AreEqual("StringColumn", ((StringColumn)_criteria.StringColumn.Clone()).ColumnName);
            Assert.AreEqual("GuidColumn", ((GuidColumn)_criteria.GuidColumn.Clone()).ColumnName);
            Assert.AreEqual("DoubleColumn", ((DoubleColumn)_criteria.DoubleColumn.Clone()).ColumnName);
            Assert.AreEqual("DecimalColumn", ((DecimalColumn)_criteria.DecimalColumn.Clone()).ColumnName);
        }

        [TestMethod]
        public void TestToSelectColumnName()
        {
            Assert.AreEqual("[BooleanColumn]", _criteria.BooleanColumn.ToSelectColumnName());
            Assert.AreEqual("[ByteColumn]", _criteria.ByteColumn.ToSelectColumnName());
            Assert.AreEqual("[Int16Column]", _criteria.Int16Column.ToSelectColumnName());
            Assert.AreEqual("[Int32Column]", _criteria.Int32Column.ToSelectColumnName());
            Assert.AreEqual("[Int64Column]", _criteria.Int64Column.ToSelectColumnName());
            Assert.AreEqual("[DateTimeColumn]", _criteria.DateTimeColumn.ToSelectColumnName());
            Assert.AreEqual("[StringColumn]", _criteria.StringColumn.ToSelectColumnName());
            Assert.AreEqual("[GuidColumn]", _criteria.GuidColumn.ToSelectColumnName());
            Assert.AreEqual("[DoubleColumn]", _criteria.DoubleColumn.ToSelectColumnName());
            Assert.AreEqual("[DecimalColumn]", _criteria.DecimalColumn.ToSelectColumnName());

            Assert.AreEqual("? AS [C]", new BooleanColumn(new BooleanParameterExpression(true), "C").ToSelectColumnName());
            Assert.AreEqual("? AS [C]", new ByteColumn(new ByteParameterExpression(0), "C").ToSelectColumnName());
            Assert.AreEqual("? AS [C]", new Int16Column(new Int16ParameterExpression(0), "C").ToSelectColumnName());
            Assert.AreEqual("? AS [C]", new Int32Column(new Int32ParameterExpression(0), "C").ToSelectColumnName());
            Assert.AreEqual("? AS [C]", new Int64Column(new Int64ParameterExpression(0), "C").ToSelectColumnName());
            Assert.AreEqual("? AS [C]", new DateTimeColumn(new DateTimeParameterExpression(DateTime.Now), "C").ToSelectColumnName());
            Assert.AreEqual("? AS [C]", new StringColumn(new StringParameterExpression(string.Empty, true), "C", true).ToSelectColumnName());
            Assert.AreEqual("? AS [C]", new GuidColumn(new GuidParameterExpression(Guid.NewGuid()), "C").ToSelectColumnName());
            Assert.AreEqual("? AS [C]", new DoubleColumn(new DoubleParameterExpression(0), "C").ToSelectColumnName());
            Assert.AreEqual("? AS [C]", new DecimalColumn(new DecimalParameterExpression(0), "C").ToSelectColumnName());
        }

        [TestMethod]
        public void TestColumnsSerialization()
        {
            var childExprs = new List<IExpression> {new Int32ParameterExpression(1)};

            var column = new BooleanColumn(new BooleanExpression("2 > ?", childExprs), "column");
            var serializer = new DataContractSerializer(typeof(BooleanColumn));
            var stream = new MemoryStream();
            serializer.WriteObject(stream, column);
            stream.Seek(0, SeekOrigin.Begin);
            var deserializedColumn = (BooleanColumn)serializer.ReadObject(stream);
            Assert.AreEqual(column.Sql, deserializedColumn.Sql);
            Assert.AreEqual(column.ChildExpressions.Count, deserializedColumn.ChildExpressions.Count);
            Assert.AreEqual(1, (deserializedColumn.ChildExpressions[0] as Int32ParameterExpression).Value);
            Assert.AreEqual(column.ColumnName, deserializedColumn.ColumnName);

            var binaryExpr = new BinaryParameterExpression("id1", new byte[] { 0, 1, 2 });
            serializer = new DataContractSerializer(typeof(BinaryParameterExpression));
            stream = new MemoryStream();
            serializer.WriteObject(stream, binaryExpr);
            stream.Seek(0, SeekOrigin.Begin);
            var deserializedExpr = (BinaryParameterExpression)serializer.ReadObject(stream);
            Assert.AreEqual(binaryExpr.ID, deserializedExpr.ID);
            Assert.AreEqual(binaryExpr.Value.Length, deserializedExpr.Value.Length);
            Assert.AreEqual(binaryExpr.Value[1], deserializedExpr.Value[1]);
        }
    }
}
