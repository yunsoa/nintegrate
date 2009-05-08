using System.Runtime.Serialization;
namespace NIntegrate.Configuration
{
    /// <summary>
    /// Base class of lookup entities for Extensible Class Types.
    /// </summary>
    [DataContract]
    public abstract class TypeLookup
    {
        /// <summary>
        /// Gets or sets the type_id.
        /// </summary>
        /// <value>The type_id.</value>
        [DataMember]
        public int Type_id { get; set; }
        /// <summary>
        /// Gets or sets the the friendly name of a class type.
        /// </summary>
        /// <value>The name of the friendly.</value>
        [DataMember]
        public string FriendlyName { get; set; }
        /// <summary>
        /// Gets or sets the qualified name of the class.
        /// </summary>
        /// <value>The qualified name of the class.</value>
        [DataMember]
        public string ClassName { get; set; }
    }
}
