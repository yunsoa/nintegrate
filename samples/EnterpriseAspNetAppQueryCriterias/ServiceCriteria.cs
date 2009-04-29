using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using NIntegrate.Query;

namespace EnterpriseAspNetAppQueryCriterias
{
    [DataContract]
    [ComVisible(true)]
    [Guid("A9414E93-DF32-4fb5-8041-D367075A2975")]
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
