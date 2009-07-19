using System.Runtime.Serialization;

namespace NIntegrate.ServiceModel.Configuration
{
    [DataContract]
    public enum WcfListenUriMode
    {
        [DataMember]
        Explicit,

        [DataMember]
        Unique
    }
}
