using NIntegrate.Query.SqlClient;

namespace NIntegrate.Configuration.UI.Code.Criterias
{
    public sealed class EndpointClientCriteria : CriteriaBase
    {
        public EndpointClientCriteria() : base("EndpointClient")
        {
        }

        public SqlInt32Column Endpoint_id = new SqlInt32Column("Endpoint_id");
        public SqlInt32Column ClientFarm_id = new SqlInt32Column("ClientFarm_id");
        public SqlInt32Column ClientEndpointBehavior_id = new SqlInt32Column("ClientEndpointBehavior_id");
    }
}
