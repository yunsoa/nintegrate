using System.Runtime.Serialization;

namespace NIntegrate.ServiceModel.Configuration
{
    /// <summary>
    /// The WcfClientEndpoint class contains all the configuration required for consuming a WCF service.
    /// </summary>
    [DataContract(Namespace = "http://nintegrate.com")]
    public sealed class WcfClientEndpoint : WcfEndpoint
    {
        public WcfClientEndpoint()
        {
        }

        public WcfClientEndpoint(
            string serviceContractType,
            string bindingTypeCode,
            string bindingXml,
            string address,
            string identityXml,
            string headersXml,
            string endpointBehaviorXml,
            string metadataXml)
            : base(
            serviceContractType,
            bindingTypeCode,
            bindingXml,
            address,
            identityXml,
            headersXml,
            endpointBehaviorXml)
        {
            if (!string.IsNullOrEmpty(metadataXml))
                MetadataXml = new MetadataXml(metadataXml);
        }

        [DataMember]
        public MetadataXml MetadataXml { get; set; }
    }
}
