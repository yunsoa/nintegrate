using System.Runtime.Serialization;

namespace NIntegrate.Configuration
{
    [DataContract]
    public sealed class ServiceDeploymentConfiguration
    {
        [DataMember]
        private string _listenUri;

        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public string HostXML { get; set; }
        [DataMember]
        public string EndpointAddress { get; set; }
        public string ListenUri
        {
            get { return _listenUri ?? EndpointAddress; }
            set { _listenUri = value; }
        }
    }
}
