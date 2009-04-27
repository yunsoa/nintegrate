using System.Runtime.Serialization;

namespace NIntegrate.Configuration
{
    [DataContract]
    public sealed class EndpointConfiguration
    {
        [DataMember]
        private string _listenUri;

        [DataMember]
        public string EndpointAddress { get; set; }
        [DataMember]
        public string FarmAddress { get; set; }
        [DataMember]
        public string ServiceContract { get; set; }
        [DataMember]
        public string EndpointBehaviorXML { get; set; }
        [DataMember]
        public string BindingNamespace { get; set; }
        [DataMember]
        public int BindingType_id { get; set; }
        [DataMember]
        public string BindingXML { get; set; }
        [DataMember]
        public bool MexBindingEnabled { get; set; }
        [DataMember]
        public bool AddMexBindingOnly { get; set; }
        [DataMember]
        public string IdentityXML { get; set; }
        public string ListenUri
        {
            get { return _listenUri ?? EndpointAddress; }
            set { _listenUri = value; }
        }
        [DataMember]
        public EndpointListenUriMode? ListenUriMode { get; set; }
    }
}
