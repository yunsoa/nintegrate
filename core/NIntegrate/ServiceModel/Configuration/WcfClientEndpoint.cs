using System.Runtime.Serialization;

namespace NIntegrate.ServiceModel.Configuration
{
    [DataContract]
    public sealed class WcfClientEndpoint : WcfEndpoint
    {
        [DataMember]
        public MetadataXml MetadataXml { get; set; }
    }
}
