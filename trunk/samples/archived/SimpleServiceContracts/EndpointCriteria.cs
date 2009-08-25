using NIntegrate.Query.SqlClient;

namespace SimpleServiceContracts
{
    public sealed class EndpointCriteria : SqlCriteria
    {
        public EndpointCriteria()
            : base("Endpoint", "Sample - SimpleService")
        {
        }

        public SqlInt32Column Endpoint_id = new SqlInt32Column("Endpoint_id");
        public SqlStringColumn EndpointName = new SqlStringColumn("EndpointName", false);
        public SqlInt32Column Binding_id = new SqlInt32Column("Binding_id");
        public SqlStringColumn EndpointAddress = new SqlStringColumn("EndpointAddress", false);
    }
}
