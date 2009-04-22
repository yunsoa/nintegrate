using System.Runtime.Serialization;

namespace NIntegrate.Configuration
{
    [DataContract]
    public sealed class ConnectionString
    {
        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public string ProviderName { get; set; }
    }
}
