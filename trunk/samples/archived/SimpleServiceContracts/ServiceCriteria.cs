using System.Runtime.Serialization;
using NIntegrate.Query.SqlClient;

namespace SimpleServiceContracts
{
    [DataContract]
    public sealed class ServiceCriteria : SqlCriteria
    {
        public ServiceCriteria()
            : base("Service", "Sample - SimpleService")
        {
        }

        public SqlInt32Column Service_id = new SqlInt32Column("Service_id");
        public SqlStringColumn ServiceName = new SqlStringColumn("ServiceName", false);
        public SqlStringColumn HostXML = new SqlStringColumn("HostXML", false);
    }
}
