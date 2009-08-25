using NIntegrate.Query.SqlClient;

namespace NIntegrate.Configuration.UI.Code.Criterias
{
    public sealed class EndpointCriteria : CriteriaBase
    {
        public EndpointCriteria() : base("Endpoint")
        {
        }

        public SqlInt32Column Endpoint_id = new SqlInt32Column("Endpoint_id");
        public SqlStringColumn EndpointName = new SqlStringColumn("EndpointName", false);
        public SqlStringColumn EndpointAddress = new SqlStringColumn("EndpointAddress", false);
        public SqlStringColumn ServiceContract = new SqlStringColumn("ServiceContract", false);
        public SqlInt32Column Binding_id = new SqlInt32Column("Binding_id");
        public SqlInt32Column EndpointBehavior_id = new SqlInt32Column("EndpointBehavior_id");
        public SqlStringColumn BindingNamespace = new SqlStringColumn("BindingNamespace", false);
        public SqlStringColumn ListenUri = new SqlStringColumn("ListenUri", false);
        public SqlInt32Column ListenUriMode_id = new SqlInt32Column("ListenUriMode_id");
        public SqlStringColumn IdentityXML = new SqlStringColumn("IdentityXML", false);
    }
}
