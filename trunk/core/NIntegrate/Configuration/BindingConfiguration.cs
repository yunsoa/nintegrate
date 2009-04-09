using System.Runtime.Serialization;

namespace NIntegrate.Configuration
{
    [DataContract]
    public sealed class BindingConfiguration
    {
        [DataMember]
        public int BindingType_id { get; set; }
        [DataMember]
        public string BindingXML { get; set; }
        [DataMember]
        public bool MexBindingEnabled { get; set; }
    }
}
