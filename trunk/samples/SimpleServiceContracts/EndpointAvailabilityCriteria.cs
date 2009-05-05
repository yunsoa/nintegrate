using NIntegrate.Query.SqlClient;

namespace SimpleServiceContracts
{
    public sealed class EndpointAvailabilityCriteria : SqlCriteria
    {
        public EndpointAvailabilityCriteria()
            : base("ServiceEndpoint_lnk", "Sample - SimpleService")
        {
        }

        public SqlInt32Column Service_id = new SqlInt32Column("Service_id");
        public SqlInt32Column Endpoint_id = new SqlInt32Column("Endpoint_id");
        public SqlInt32Column Farm_id = new SqlInt32Column("Farm_id");
        public SqlBooleanColumn Active = new SqlBooleanColumn("Active");
    }
}
