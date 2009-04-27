using System.Runtime.Serialization;
using NIntegrate.Query;

namespace EnterpriseAspNetAppQueryCriterias
{
    [DataContract]
    public sealed class ServiceCriteria : Criteria
    {
        public ServiceCriteria()
            : base("Service", "Enterprise Sample Shared")
        {
        }

        public Int32Column Service_id = new Int32Column("Service_id");
        public StringColumn ServiceName = new StringColumn("ServiceName", false);
        public StringColumn HostXML = new StringColumn("HostXML", false);
    }
}
