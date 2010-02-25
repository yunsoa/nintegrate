using System.Runtime.Serialization;

namespace NIntegrate.ServiceModel.Configuration
{
    [DataContract(Namespace = "http://nintegrate.com")]
    public enum WcfListenUriMode
    {
        [DataMember]
        Explicit,

        [DataMember]
        Unique
    }
}
