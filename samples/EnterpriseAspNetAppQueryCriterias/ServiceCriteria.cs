using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using NIntegrate.Query;
using NIntegrate.Query.SqlClient;

namespace EnterpriseAspNetAppQueryCriterias
{
    [DataContract]
    [ComVisible(true)]
    [Guid("A9414E93-DF32-4fb5-8041-D367075A2975")]
    public class ServiceCriteria : SqlCriteria
    {
        public ServiceCriteria()
            : base("Service", "Enterprise Sample Shared")
        {
        }

        public SqlInt32Column Service_id = new SqlInt32Column("Service_id");
        public SqlStringColumn ServiceName = new SqlStringColumn("ServiceName", false);
        public SqlStringColumn HostXML = new SqlStringColumn("HostXML", false);
    }
}
