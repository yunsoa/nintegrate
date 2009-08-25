using System.Runtime.Serialization;

namespace NIntegrate.Configuration
{
    /// <summary>
    /// The Endpoint ListenUri Modes, 
    /// please refer to System.ServiceModel.Description.ListenUriMode
    /// for details
    /// </summary>
    [DataContract]
    public enum EndpointListenUriMode
    {
        /// <summary>
        /// Explicit
        /// </summary>
        [EnumMember]
        Explicit = 1,
        /// <summary>
        /// Unique
        /// </summary>
        [EnumMember]
        Unique = 2
    }
}
