using System.Runtime.Serialization;

namespace NIntegrate.ServiceModel.Configuration
{
    /// <summary>
    /// The WcfServiceEndpoint contains the configuration of a service endpoint for hosting.
    /// </summary>
    [DataContract]
    public sealed class WcfServiceEndpoint : WcfEndpoint
    {
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
