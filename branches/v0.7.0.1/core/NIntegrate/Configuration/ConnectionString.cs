using System.Runtime.Serialization;

namespace NIntegrate.Configuration
{
    /// <summary>
    /// The ConnectingString entity.
    /// </summary>
    [DataContract]
    public sealed class ConnectionString
    {
        /// <summary>
        /// Gets or sets the connecting string value.
        /// </summary>
        /// <value>The value.</value>
        [DataMember]
        public string Value { get; set; }
        /// <summary>
        /// Gets or sets the name of the database provider.
        /// </summary>
        /// <value>The name of the database provider.</value>
        [DataMember]
        public string ProviderName { get; set; }
    }
}
