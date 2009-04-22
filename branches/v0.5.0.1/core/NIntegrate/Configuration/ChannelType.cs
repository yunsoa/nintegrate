using System.Runtime.Serialization;

namespace NIntegrate.Configuration
{
    [DataContract]
    public enum ChannelType
    {
        [EnumMember]
        MSMQ = 1,
        [EnumMember]
        HTTP = 2,
        [EnumMember]
        TCP = 3,
        [EnumMember]
        IPC = 4
    }
}
