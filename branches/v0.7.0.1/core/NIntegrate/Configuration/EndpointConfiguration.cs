using System.Runtime.Serialization;

namespace NIntegrate.Configuration
{
    /// <summary>
    /// The EndpointConfiguration entity.
    /// </summary>
    [DataContract]
    public sealed class EndpointConfiguration
    {
        [DataMember]
        private string _listenUri;

        /// <summary>
        /// Gets or sets the endpoint address.
        /// </summary>
        /// <value>The endpoint address.</value>
        [DataMember]
        public string EndpointAddress { get; set; }
        /// <summary>
        /// Gets or sets the farm address.
        /// </summary>
        /// <value>The farm address.</value>
        [DataMember]
        public string FarmAddress { get; set; }
        /// <summary>
        /// Gets or sets the service contract.
        /// </summary>
        /// <value>The service contract.</value>
        [DataMember]
        public string ServiceContract { get; set; }
        /// <summary>
        /// Gets or sets the endpoint behavior XML.
        /// </summary>
        /// <value>The endpoint behavior XML.</value>
        [DataMember]
        public string EndpointBehaviorXML { get; set; }
        /// <summary>
        /// Gets or sets the binding namespace.
        /// </summary>
        /// <value>The binding namespace.</value>
        [DataMember]
        public string BindingNamespace { get; set; }
        /// <summary>
        /// Gets or sets the binding type_id.
        /// </summary>
        /// <value>The binding type_id.</value>
        [DataMember]
        public int BindingType_id { get; set; }
        /// <summary>
        /// Gets or sets the binding XML.
        /// </summary>
        /// <value>The binding XML.</value>
        [DataMember]
        public string BindingXML { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether mex binding enabled.
        /// </summary>
        /// <value><c>true</c> if mex binding enabled; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool MexBindingEnabled { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether add mex binding only.
        /// </summary>
        /// <value><c>true</c> if add mex binding only; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool AddMexBindingOnly { get; set; }
        /// <summary>
        /// Gets or sets the identity XML.
        /// </summary>
        /// <value>The identity XML.</value>
        [DataMember]
        public string IdentityXML { get; set; }
        /// <summary>
        /// Gets or sets the listen URI.
        /// </summary>
        /// <value>The listen URI.</value>
        public string ListenUri
        {
            get { return _listenUri ?? EndpointAddress; }
            set { _listenUri = value; }
        }
        /// <summary>
        /// Gets or sets the listen URI mode.
        /// </summary>
        /// <value>The listen URI mode.</value>
        [DataMember]
        public EndpointListenUriMode? ListenUriMode { get; set; }
    }
}
