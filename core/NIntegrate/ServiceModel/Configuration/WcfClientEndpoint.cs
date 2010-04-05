using System.Runtime.Serialization;

namespace NIntegrate.ServiceModel.Configuration
{
    /// <summary>
    /// The WcfClientEndpoint class contains all the configuration required for consuming a WCF service.
    /// </summary>
    [DataContract(Namespace = "http://nintegrate.com")]
    public sealed class WcfClientEndpoint : WcfEndpoint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WcfClientEndpoint"/> class.
        /// </summary>
        public WcfClientEndpoint()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WcfClientEndpoint"/> class.
        /// </summary>
        /// <param name="serviceContractType">Type of the service contract.</param>
        /// <param name="bindingTypeCode">The binding type code.</param>
        /// <param name="bindingXml">The binding XML.</param>
        /// <param name="address">The address.</param>
        /// <param name="identityXml">The identity XML.</param>
        /// <param name="headersXml">The headers XML.</param>
        /// <param name="endpointBehaviorXml">The endpoint behavior XML.</param>
        /// <param name="metadataXml">The metadata XML.</param>
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

        /// <summary>
        /// Gets or sets the metadata XML.
        /// </summary>
        /// <value>The metadata XML.</value>
        [DataMember]
        public MetadataXml MetadataXml { get; set; }
    }
}
