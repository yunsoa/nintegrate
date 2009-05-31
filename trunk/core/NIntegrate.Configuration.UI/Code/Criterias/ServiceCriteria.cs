using NIntegrate.Query.SqlClient;

namespace NIntegrate.Configuration.UI.Code.Criterias
{
    public sealed class ServiceCriteria : CriteriaBase
    {
        public ServiceCriteria()
            : base("Service")
        {
        }

        public SqlInt32Column Service_id = new SqlInt32Column("Service_id");
        public SqlStringColumn ServiceName = new SqlStringColumn("ServiceName", false);
        public SqlStringColumn AppCode = new SqlStringColumn("AppCode", false);
        public SqlInt32Column ServiceCategory_id = new SqlInt32Column("ServiceCategory_id");
        public SqlInt32Column ServiceBehavior_id = new SqlInt32Column("ServiceBehavior_id");
        public SqlInt32Column ServiceHostType_id = new SqlInt32Column("ServiceHostType_id");
        public SqlStringColumn HostXML = new SqlStringColumn("HostXML", false);
    }
}
