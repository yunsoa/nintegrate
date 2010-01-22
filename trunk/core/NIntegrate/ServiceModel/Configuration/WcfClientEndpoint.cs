using System.Runtime.Serialization;

namespace NIntegrate.ServiceModel.Configuration
{
    /// <summary>
    /// The WcfClientEndpoint class contains all the configuration required for consuming a WCF service.
    /// </summary>
    [DataContract]
    public sealed class WcfClientEndpoint : WcfEndpoint
    {
        [DataMember]
        public MetadataXml MetadataXml { get; set; }
    }
}
