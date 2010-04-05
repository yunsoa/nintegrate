using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace NIntegrate.ServiceModel.Configuration
{
    /// <summary>
    /// The WcfService class contains all the configuration required for hosting a WCF service.
    /// </summary>
    [DataContract(Namespace = "http://nintegrate.com")]
    public sealed class WcfService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WcfService"/> class.
        /// </summary>
        public WcfService()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WcfService"/> class.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="customServiceHostType">Type of the custom service host.</param>
        /// <param name="serviceBehaviorXml">The service behavior XML.</param>
        /// <param name="hostXml">The host XML.</param>
        /// <param name="endpoints">The endpoints.</param>
        public WcfService(
            string serviceType,
            string customServiceHostType,
            string serviceBehaviorXml,
            string hostXml,
            IEnumerable<WcfServiceEndpoint> endpoints)
        {
            ServiceType = serviceType;
            CustomServiceHostType = customServiceHostType;
            if (!string.IsNullOrEmpty(serviceBehaviorXml))
                ServiceBehaviorXml = new ServiceBehaviorXml(serviceBehaviorXml);
            if (!string.IsNullOrEmpty(hostXml))
                HostXml = new HostXml(hostXml);
            if (endpoints != null)
                Endpoints = endpoints.ToArray();
        }

        /// <summary>
        /// Gets or sets the type of the service.
        /// </summary>
        /// <value>The type of the service.</value>
        [DataMember]
        public string ServiceType { get; set; }

        /// <summary>
        /// Gets or sets the type of the custom service host.
        /// </summary>
        /// <value>The type of the custom service host.</value>
        [DataMember]
        public string CustomServiceHostType { get; set; }

        /// <summary>
        /// Gets or sets the service behavior XML.
        /// </summary>
        /// <value>The service behavior XML.</value>
        [DataMember]
        public ServiceBehaviorXml ServiceBehaviorXml { get; set; }

        /// <summary>
        /// Gets or sets the host XML.
        /// </summary>
        /// <value>The host XML.</value>
        [DataMember]
        public HostXml HostXml { get; set; }

        /// <summary>
        /// Gets or sets the endpoints.
        /// </summary>
        /// <value>The endpoints.</value>
        [DataMember]
        public WcfServiceEndpoint[] Endpoints { get; set; }
    }
}
