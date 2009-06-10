using NIntegrate.Query.OracleClient;
using NIntegrate.Query;

namespace NIntegrate.Test.Query.TestClasses
{
    public class OracleJobCriteria : OracleCriteria
    {
        public OracleJobCriteria() : base("HR.JOBS", "OracleTest")
        {
        }

        public Int32Column JOB_ID = new Int32Column("JOB_ID");
        public StringColumn JOB_TITLE = new StringColumn("JOB_TITLE", true);
        public DecimalColumn MIN_SALARY = new DecimalColumn("MIN_SALARY");
        public DecimalColumn MAX_SALARY = new DecimalColumn("MAX_SALARY");
    }
}
