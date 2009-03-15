using System.Runtime.Serialization;

namespace NIntegrate.Configuration
{
    [DataContract]
    public sealed class Endpoint
    {
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string ChannelType { get; set; }
        [DataMember]
        public int CloseTimeout { get; set; }
        [DataMember]
        public string FarmAddress { get; set; }
        [DataMember]
        public bool IncludeExceptionDetailInFaults { get; set; }
        [DataMember]
        public int ListenBacklog { get; set; }
        [DataMember]
        public int MaxBufferPoolSize { get; set; }
        [DataMember]
        public int MaxBufferSize { get; set; }
        [DataMember]
        public int MaxConcurrentCalls { get; set; }
        [DataMember]
        public int MaxConcurrentInstances { get; set; }
        [DataMember]
        public int MaxConcurrentSessions { get; set; }
        [DataMember]
        public int MaxConnections { get; set; }
        [DataMember]
        public int MaxReceivedMessageSize { get; set; }
        [DataMember]
        public int OpenTimeout { get; set; }
        [DataMember]
        public bool PortSharingEnabled { get; set; }
        [DataMember]
        public int ReceiveTimeout { get; set; }
        [DataMember]
        public string SecurityMode { get; set; }
        [DataMember]
        public string ClientCredentialTypeName { get; set; }
        [DataMember]
        public int SendTimeout { get; set; }
        [DataMember]
        public bool TransactionFlow { get; set; }
        [DataMember]
        public int TransactionTimeout { get; set; }
        [DataMember]
        public string TransferMode { get; set; }
        [DataMember]
        public bool ReliableSessionEnabled { get; set; }
        [DataMember]
        public int ReliableSessionInactivityTimeout { get; set; }
        [DataMember]
        public bool ReliableSessionOrdered { get; set; }
    }
}
