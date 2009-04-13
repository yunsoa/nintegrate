using System.Runtime.Serialization;

namespace NIntegrate.Configuration
{
    [DataContract]
    public sealed class ClientConfiguration
    {
        [DataMember]
        public string HostXML { get; set; }
        [DataMember]
        public EndpointConfiguration Endpoint { get; set; }
    }
}
