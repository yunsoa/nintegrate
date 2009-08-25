using System.Runtime.Serialization;

namespace NIntegrate.Configuration
{
    /// <summary>
    /// WCF communication channel types.
    /// </summary>
    [DataContract]
    public enum ChannelType
    {
        /// <summary>
        /// MSMQ
        /// </summary>
        [EnumMember]
        MSMQ = 1,
        /// <summary>
        /// HTTP
        /// </summary>
        [EnumMember]
        HTTP = 2,
        /// <summary>
        /// TCP
        /// </summary>
        [EnumMember]
        TCP = 3,
        /// <summary>
        /// IPC
        /// </summary>
        [EnumMember]
        IPC = 4
    }
}
