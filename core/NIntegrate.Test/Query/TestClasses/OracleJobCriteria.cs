using NIntegrate.Data;

namespace NIntegrate.Test.Query.TestClasses
{
    public class OracleJobTable : QueryTable
    {
        public OracleJobTable()
            : base("HR.JOBS", "OracleTest", false)
        {
        }

        public Int32Column JOB_ID = new Int32Column("JOB_ID");
        public StringColumn JOB_TITLE = new StringColumn("JOB_TITLE", true);
        public DecimalColumn MIN_SALARY = new DecimalColumn("MIN_SALARY");
        public DecimalColumn MAX_SALARY = new DecimalColumn("MAX_SALARY");
    }
}
