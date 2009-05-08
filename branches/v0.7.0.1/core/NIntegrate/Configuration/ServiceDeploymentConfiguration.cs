using System.Runtime.Serialization;

namespace NIntegrate.Configuration
{
    /// <summary>
    /// The ServiceDeploymentConfiguration entity.
    /// </summary>
    [DataContract]
    public sealed class ServiceDeploymentConfiguration
    {
        [DataMember]
        private string _listenUri;

        /// <summary>
        /// Gets or sets the name of the service.
        /// </summary>
        /// <value>The name of the service.</value>
        [DataMember]
        public string ServiceName { get; set; }
        /// <summary>
        /// Gets or sets the host XML.
        /// </summary>
        /// <value>The host XML.</value>
        [DataMember]
        public string HostXML { get; set; }
        /// <summary>
        /// Gets or sets the endpoint address.
        /// </summary>
        /// <value>The endpoint address.</value>
        [DataMember]
        public string EndpointAddress { get; set; }
        /// <summary>
        /// Gets or sets the listen URI.
        /// </summary>
        /// <value>The listen URI.</value>
        public string ListenUri
        {
            get { return _listenUri ?? EndpointAddress; }
            set { _listenUri = value; }
        }
    }
}
