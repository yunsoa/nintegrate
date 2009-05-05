using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NIntegrate.Configuration
{
    /// <summary>
    /// The ServiceConfiguration entity.
    /// </summary>
    [DataContract]
    public sealed class ServiceConfiguration
    {
        /// <summary>
        /// Gets or sets the service behavior XML.
        /// </summary>
        /// <value>The service behavior XML.</value>
        [DataMember]
        public string ServiceBehaviorXML { get; set; }
        /// <summary>
        /// Gets or sets the service host type_id.
        /// </summary>
        /// <value>The service host type_id.</value>
        [DataMember]
        public int ServiceHostType_id { get; set; }
        /// <summary>
        /// Gets or sets the host XML.
        /// </summary>
        /// <value>The host XML.</value>
        [DataMember]
        public string HostXML { get; set; }

        /// <summary>
        /// Gets or sets the endpoints.
        /// </summary>
        /// <value>The endpoints.</value>
        [DataMember]
        public IList<EndpointConfiguration> Endpoints { get; set; }
    }
}
