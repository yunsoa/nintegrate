using System.Runtime.Serialization;

namespace NIntegrate.Configuration
{
    [DataContract]
    public sealed class CustomBehaviorType : TypeLookup
    {
        [DataMember]
        public string ExtensionName { get; set; }
        [DataMember]
        public string ConfigurationElementTypeClassName { get; set; }
    }
}
