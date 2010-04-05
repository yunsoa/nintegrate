using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace NIntegrate.ServiceModel.Configuration
{
    /// <summary>
    /// Maps to the &lt;headers&gt; configuration element.
    /// </summary>
    [DataContract(Namespace = "http://nintegrate.com")]
    public sealed class HeadersXml : ConfigurationXml
    {
        private AddressHeaderCollection _headers;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HeadersXml"/> class.
        /// </summary>
        /// <param name="xml">The XML.</param>
        public HeadersXml(string xml)
            : base(xml)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates the address headers.
        /// </summary>
        /// <returns></returns>
        public AddressHeaderCollection CreateAddressHeaders()
        {
            if (_headers != null)
                return _headers;

            lock (SyncLock)
            {
                if (_headers != null)
                    return _headers;

                var element = new AddressHeaderCollectionElement();
                Deserialize(element);
                _headers = element.Headers;

                return _headers;
            }
        }

        #endregion
    }
}
