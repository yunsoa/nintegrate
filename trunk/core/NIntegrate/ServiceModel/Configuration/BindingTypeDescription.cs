using System;
using System.Runtime.Serialization;

namespace NIntegrate.ServiceModel.Configuration
{
    /// <summary>
    /// Description of a binding type
    /// </summary>
    [DataContract(Namespace = "http://nintegrate.com")]
    public sealed class BindingTypeDescription
    {
        /// <summary>
        /// Gets or sets the type of the binding.
        /// </summary>
        /// <value>The type of the binding.</value>
        [DataMember]
        public Type BindingType { get; set; }

        /// <summary>
        /// Gets or sets the type of the binding configuration element.
        /// </summary>
        /// <value>The type of the binding configuration element.</value>
        [DataMember]
        public Type BindingConfigurationElementType { get; set; }

        /// <summary>
        /// Gets or sets the available protocols.
        /// </summary>
        /// <value>The available protocols.</value>
        [DataMember]
        public string[] AvailableProtocols { get; set; }
    }
}