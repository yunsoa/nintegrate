using System.Runtime.Serialization;

namespace NIntegrate.ServiceModel.Configuration
{
    /// <summary>
    /// The WcfServiceEndpoint contains the configuration of a service endpoint for hosting.
    /// </summary>
    [DataContract(Namespace = "http://nintegrate.com")]
    public sealed class WcfServiceEndpoint : WcfEndpoint
    {
        public WcfServiceEndpoint()
        {
        }

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

        [DataMember]
        public WcfListenUriMode? ListenUriMode { get; set; }
    }
}
