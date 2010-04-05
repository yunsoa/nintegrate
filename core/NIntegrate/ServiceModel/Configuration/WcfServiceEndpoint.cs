using System.Runtime.Serialization;

namespace NIntegrate.ServiceModel.Configuration
{
    /// <summary>
    /// The WcfServiceEndpoint contains the configuration of a service endpoint for hosting.
    /// </summary>
    [DataContract(Namespace = "http://nintegrate.com")]
    public sealed class WcfServiceEndpoint : WcfEndpoint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WcfServiceEndpoint"/> class.
        /// </summary>
        public WcfServiceEndpoint()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WcfServiceEndpoint"/> class.
        /// </summary>
        /// <param name="serviceContractType">Type of the service contract.</param>
        /// <param name="bindingTypeCode">The binding type code.</param>
        /// <param name="bindingXml">The binding XML.</param>
        /// <param name="address">The address.</param>
        /// <param name="identityXml">The identity XML.</param>
        /// <param name="headersXml">The headers XML.</param>
        /// <param name="endpointBehaviorXml">The endpoint behavior XML.</param>
        /// <param name="listenUri">The listen URI.</param>
        /// <param name="listenUriMode">The listen URI mode.</param>
        public WcfServiceEndpoint(
            string serviceContractType,
            string bindingTypeCode,
            string bindingXml,
            string address,
            string identityXml,
            string headersXml,
            string endpointBehaviorXml,
            string listenUri,
            WcfListenUriMode? listenUriMode)
            : base(
            serviceContractType,
            bindingTypeCode,
            bindingXml,
            address,
            identityXml,
            headersXml,
            endpointBehaviorXml)
        {
            ListenUri = listenUri;
            ListenUriMode = listenUriMode;
        }

        [DataMember]
        private string _listenUri;

        /// <summary>
        /// Gets or sets the listen URI.
        /// </summary>
        /// <value>The listen URI.</value>
        public string ListenUri
        {
            get
            {
                return _listenUri ?? Address;
            }
            set
            {
                _listenUri = value;
            }
        }

        /// <summary>
        /// Gets or sets the listen URI mode.
        /// </summary>
        /// <value>The listen URI mode.</value>
        [DataMember]
        public WcfListenUriMode? ListenUriMode { get; set; }
    }
}
