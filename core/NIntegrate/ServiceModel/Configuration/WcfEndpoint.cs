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

        internal WcfEndpoint(
            string serviceContractType,
            string bindingTypeCode,
            string bindingXml,
            string address,
            string identityXml,
            string headersXml,
            string endpointBehaviorXml)
        {
            ServiceContractType = serviceContractType;
            if (!string.IsNullOrEmpty(bindingTypeCode))
                BindingXml = new BindingXml(bindingTypeCode, bindingXml);
            Address = address;
            if (!string.IsNullOrEmpty(identityXml))
                IdentityXml = new IdentityXml(identityXml);
            if (!string.IsNullOrEmpty(headersXml))
                HeadersXml = new HeadersXml(headersXml);
            if (!string.IsNullOrEmpty(endpointBehaviorXml))
                EndpointBehaviorXml = new EndpointBehaviorXml(endpointBehaviorXml);
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
