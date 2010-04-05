using System.Runtime.Serialization;

namespace NIntegrate.ServiceModel.Configuration
{
    /// <summary>
    /// The WcfListenUriMode enumeration
    /// </summary>
    [DataContract(Namespace = "http://nintegrate.com")]
    public enum WcfListenUriMode
    {
        /// <summary>
        /// Explicit
        /// </summary>
        [DataMember]
        Explicit,

        /// <summary>
        /// Unique
        /// </summary>
        [DataMember]
        Unique
    }
}
