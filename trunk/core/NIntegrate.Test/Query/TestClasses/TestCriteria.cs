using NIntegrate.Data;
namespace NIntegrate.Test.Query.TestClasses
{
    public class TestTable : QueryTable
    {
        public TestTable()
            : base("TestTable", "Test", false)
        {
        }

        public BooleanColumn BooleanColumn = new BooleanColumn("BooleanColumn");
        public ByteColumn ByteColumn = new ByteColumn("ByteColumn");
        public Int16Column Int16Column = new Int16Column("Int16Column");
        public Int32Column Int32Column = new Int32Column("Int32Column");
        public Int64Column Int64Column = new Int64Column("Int64Column");
        public DateTimeColumn DateTimeColumn = new DateTimeColumn("DateTimeColumn");
        public StringColumn StringColumn = new StringColumn("StringColumn", true);
        public GuidColumn GuidColumn = new GuidColumn("GuidColumn");
        public DoubleColumn DoubleColumn = new DoubleColumn("DoubleColumn");
        public DecimalColumn DecimalColumn = new DecimalColumn("DecimalColumn");
    }

    public class TestTable2 : QueryTable
    {
        public TestTable2()
            : base("TestTable2", "Test", false)
        {
        }

        public Int32Column ID = new Int32Column("ID");
        public BinaryColumn Data = new BinaryColumn("Data");
    }
}
