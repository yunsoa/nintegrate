using System.Runtime.Serialization;

namespace NIntegrate.Configuration
{
    [DataContract]
    public sealed class BindingType : TypeLookup
    {
        [DataMember]
        public ChannelType ChannelType { get; set; }
    }
}
