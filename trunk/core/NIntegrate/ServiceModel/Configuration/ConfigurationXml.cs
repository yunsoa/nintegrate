using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;

namespace NIntegrate.ServiceModel.Configuration
{
    /// <summary>
    /// Base class for containing Xml clip for configuration
    /// </summary>
    [DataContract(Namespace = "http://nintegrate.com")]
    public abstract class ConfigurationXml
    {
        [DataMember]
        private readonly string _xml;

        private static readonly MethodInfo _methodDeserializeElement;

        #region Constructors

        static ConfigurationXml()
        {
            _methodDeserializeElement = typeof (ConfigurationElement).GetMethod(
                "DeserializeElement",
                BindingFlags.NonPublic | BindingFlags.Instance);
        }

        protected ConfigurationXml(string xml)
        {
            _xml = xml;
        }

        #endregion

        #region Properties

        public virtual string Xml
        {
            get { return _xml; }
        }

        protected object SyncLock
        {
            get { return this; }
        }

        #endregion

        #region Non-Public Methods

        protected void Deserialize(ConfigurationElement element)
        {
            if (string.IsNullOrEmpty(_xml))
                return;

            Deserialize(_xml, element);
        }

        protected static void Deserialize(string xml, ConfigurationElement element)
        {
            if (element == null)
                throw new ArgumentNullException("element");

            if (string.IsNullOrEmpty(xml))
            {
                return;
            }

            var rdr = new XmlTextReader(new StringReader(xml));
            rdr.Read();
            rdr.ReadSubtree();
            _methodDeserializeElement.Invoke(element, new object[] { rdr, false });
        }

        #endregion
    }
}
