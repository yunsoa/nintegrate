using System.Runtime.Serialization;

namespace NIntegrate.Configuration
{
    [DataContract]
    public sealed class EndpointConfiguration
    {
        [DataMember]
        public string EndpointAddress { get; set; }
        [DataMember]
        public BindingConfiguration Binding { get; set; }
        [DataMember]
        public string EndpointBehaviorXML { get; set; }
        [DataMember]
        public string BindingNamespace { get; set; }
        [DataMember]
        public string IdentityXML { get; set; }
        [DataMember]
        public string ListenUri { get; set; }
        [DataMember]
        public EndpointListenUriMode? ListenUriMode { get; set; }
    }
}
