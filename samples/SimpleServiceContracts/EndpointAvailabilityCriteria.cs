using NIntegrate.Query;

namespace SimpleServiceContracts
{
    public sealed class EndpointAvailabilityCriteria : Criteria
    {
        public EndpointAvailabilityCriteria()
            : base("ServiceEndpoint_lnk", "Sample - SimpleService")
        {
        }

        public Int32Column Service_id = new Int32Column("Service_id");
        public Int32Column Endpoint_id = new Int32Column("Endpoint_id");
        public Int32Column Farm_id = new Int32Column("Farm_id");
        public BooleanColumn Active = new BooleanColumn("Active");
    }
}
