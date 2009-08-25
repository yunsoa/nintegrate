using System.Runtime.Serialization;

namespace NIntegrate.Configuration
{
    /// <summary>
    /// The CustomBehaviorTyp entity.
    /// </summary>
    [DataContract]
    public sealed class CustomBehaviorType : TypeLookup
    {
        /// <summary>
        /// Gets or sets the name of the extension, which is the XML element name
        /// for this custom behavior.
        /// </summary>
        /// <value>The name of the extension.</value>
        [DataMember]
        public string ExtensionName { get; set; }
        /// <summary>
        /// Gets or sets the name of the configuration element type class.
        /// </summary>
        /// <value>The name of the configuration element type class.</value>
        [DataMember]
        public string ConfigurationElementTypeClassName { get; set; }
    }
}
