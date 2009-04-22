using System.Runtime.Serialization;
using NIntegrate.Query;

namespace SimpleServiceContracts
{
    [DataContract]
    public sealed class ServiceCriteria : Criteria
    {
        public ServiceCriteria()
            : base("Service", "Sample - SimpleService")
        {
        }

        public Int32Column Service_id = new Int32Column("Service_id");
        public StringColumn ServiceName = new StringColumn("ServiceName", false);
        public StringColumn HostXML = new StringColumn("HostXML", false);
    }
}
