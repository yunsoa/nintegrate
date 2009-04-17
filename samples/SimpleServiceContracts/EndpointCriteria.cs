using NIntegrate.Query;

namespace SimpleServiceContracts
{
    public sealed class EndpointCriteria : Criteria
    {
        public EndpointCriteria()
            : base("Endpoint", "Sample - SimpleService")
        {
        }

        public Int32Column Endpoint_id = new Int32Column("Endpoint_id");
        public Int32Column Binding_id = new Int32Column("Binding_id");
        public StringColumn EndpointAddress = new StringColumn("EndpointAddress", false);
    }
}
