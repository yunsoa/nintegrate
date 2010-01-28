using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace NIntegrate.ServiceModel.Configuration
{
    /// <summary>
    /// The WcfService class contains all the configuration required for hosting a WCF service.
    /// </summary>
    [DataContract]
    public sealed class WcfService
    {
        public WcfService()
        {
        }

        public WcfService(
            string serviceType,
            string customServiceHostType,
            string serviceBehaviorXml,
            string hostXml,
            IEnumerable<WcfServiceEndpoint> endpoints)
        {
            ServiceType = serviceType;
            CustomServiceHostType = customServiceHostType;
            if (!string.IsNullOrEmpty(serviceBehaviorXml))
                ServiceBehaviorXml = new ServiceBehaviorXml(serviceBehaviorXml);
            if (!string.IsNullOrEmpty(hostXml))
                HostXml = new HostXml(hostXml);
            if (endpoints != null)
                Endpoints = endpoints.ToArray();
        }

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
