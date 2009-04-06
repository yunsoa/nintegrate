using System.Runtime.Serialization;
namespace NIntegrate.Configuration
{
    [DataContract]
    public abstract class TypeLookup
    {
        [DataMember]
        public int Type_id { get; set; }
        [DataMember]
        public string FriendlyName { get; set; }
        [DataMember]
        public string ClassName { get; set; }
        [DataMember]
        public string ConfigurationElementTypeClassName { get; set; }
    }
}
