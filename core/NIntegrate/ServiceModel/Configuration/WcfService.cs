using System.Runtime.Serialization;

namespace NIntegrate.ServiceModel.Configuration
{
    /// <summary>
    /// The WcfService class contains all the configuration required for hosting a WCF service.
    /// </summary>
    [DataContract]
    public sealed class WcfService
    {
        [DataMember]
        public string ServiceType { get; set; }

        [DataMember]
        public string CustomServiceHostType { get; set; }

        [DataMember]
        public ServiceBehaviorXml ServiceBehaviorXml { get; set; }

        [DataMember]
        public HostXml HostXml { get; set; }

        [DataMember]
        public WcfServiceEndpoint[] Endpoints { get; set; }
    }
}
