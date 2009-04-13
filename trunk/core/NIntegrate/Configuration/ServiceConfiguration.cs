using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NIntegrate.Configuration
{
    [DataContract]
    public sealed class ServiceConfiguration
    {
        [DataMember]
        public string ServiceBehaviorXML { get; set; }
        [DataMember]
        public int ServiceHostType_id { get; set; }
        [DataMember]
        public string HostXML { get; set; }

        [DataMember]
        public IList<EndpointConfiguration> Endpoints { get; set; }
    }
}
