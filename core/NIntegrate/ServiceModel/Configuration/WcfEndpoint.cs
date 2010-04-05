using System.Runtime.Serialization;

namespace NIntegrate.ServiceModel.Configuration
{
    /// <summary>
    /// The base class of endpoint configuration.
    /// </summary>
    [DataContract(Namespace = "http://nintegrate.com")]
    public class WcfEndpoint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WcfEndpoint"/> class.
        /// </summary>
        public WcfEndpoint()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WcfEndpoint"/> class.
        /// </summary>
        /// <param name="serviceContractType">Type of the service contract.</param>
        /// <param name="bindingTypeCode">The binding type code.</param>
        /// <param name="bindingXml">The binding XML.</param>
        /// <param name="address">The address.</param>
        /// <param name="identityXml">The identity XML.</param>
        /// <param name="headersXml">The headers XML.</param>
        /// <param name="endpointBehaviorXml">The endpoint behavior XML.</param>
        public WcfEndpoint(
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

        /// <summary>
        /// Gets or sets the type of the service contract.
        /// </summary>
        /// <value>The type of the service contract.</value>
        [DataMember]
        public string ServiceContractType { get; set; }

        /// <summary>
        /// Gets or sets the binding XML.
        /// </summary>
        /// <value>The binding XML.</value>
        [DataMember]
        public BindingXml BindingXml { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        [DataMember]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the identity XML.
        /// </summary>
        /// <value>The identity XML.</value>
        [DataMember]
        public IdentityXml IdentityXml { get; set; }

        /// <summary>
        /// Gets or sets the headers XML.
        /// </summary>
        /// <value>The headers XML.</value>
        [DataMember]
        public HeadersXml HeadersXml { get; set; }

        /// <summary>
        /// Gets or sets the endpoint behavior XML.
        /// </summary>
        /// <value>The endpoint behavior XML.</value>
        [DataMember]
        public EndpointBehaviorXml EndpointBehaviorXml { get; set; }
    }
}
