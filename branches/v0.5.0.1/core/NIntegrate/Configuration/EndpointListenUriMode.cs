using System.Runtime.Serialization;

namespace NIntegrate.Configuration
{
    [DataContract]
    public enum EndpointListenUriMode
    {
        [EnumMember]
        Explicit = 1,
        [EnumMember]
        Unique = 2
    }
}
