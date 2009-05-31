using NIntegrate.Query.SqlClient;

namespace NIntegrate.Configuration.UI.Code.Criterias
{
    public sealed class ServiceEndpointCriteria : CriteriaBase
    {
        public ServiceEndpointCriteria()
            : base("ServiceEndpoint_lnk")
        {
        }

        public SqlInt32Column Service_id = new SqlInt32Column("Service_id");
        public SqlInt32Column Endpoint_id = new SqlInt32Column("Endpoint_id");
        public SqlInt32Column Farm_id = new SqlInt32Column("Farm_id");
        public SqlBooleanColumn Active = new SqlBooleanColumn("Active");
    }
}
