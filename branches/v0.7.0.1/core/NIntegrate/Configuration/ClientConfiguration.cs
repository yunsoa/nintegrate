using System.Runtime.Serialization;

namespace NIntegrate.Configuration
{
    /// <summary>
    /// The ClientConfiguration entity.
    /// </summary>
    [DataContract]
    public sealed class ClientConfiguration
    {
        /// <summary>
        /// Gets or sets the host XML.
        /// </summary>
        /// <value>The host XML.</value>
        [DataMember]
        public string HostXML { get; set; }
        /// <summary>
        /// Gets or sets the endpoint.
        /// </summary>
        /// <value>The endpoint.</value>
        [DataMember]
        public EndpointConfiguration Endpoint { get; set; }
    }
}
