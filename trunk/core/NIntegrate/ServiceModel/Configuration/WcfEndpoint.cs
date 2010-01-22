using System.Runtime.Serialization;

namespace NIntegrate.ServiceModel.Configuration
{
    /// <summary>
    /// The base class of endpoint configuration.
    /// </summary>
    [DataContract]
    public class WcfEndpoint
    {
        internal WcfEndpoint()
        {
        }

        [DataMember]
        public string ServiceContractType { get; set; }

        [DataMember]
        public BindingXml BindingXml { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public IdentityXml IdentityXml { get; set; }

        [DataMember]
        public HeadersXml HeadersXml { get; set; }

        [DataMember]
        public EndpointBehaviorXml EndpointBehaviorXml { get; set; }
    }
}
