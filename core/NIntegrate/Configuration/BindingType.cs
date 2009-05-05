using System.Runtime.Serialization;

namespace NIntegrate.Configuration
{
    /// <summary>
    /// The BindingType entity.
    /// </summary>
    [DataContract]
    public sealed class BindingType : TypeLookup
    {
        /// <summary>
        /// Gets or sets the ChannelType of this Binding.
        /// </summary>
        /// <value>The type of the channel.</value>
        [DataMember]
        public ChannelType ChannelType { get; set; }
        /// <summary>
        /// Gets or sets the name of the configuration element type class.
        /// </summary>
        /// <value>The name of the configuration element type class.</value>
        [DataMember]
        public string ConfigurationElementTypeClassName { get; set; }
    }
}
