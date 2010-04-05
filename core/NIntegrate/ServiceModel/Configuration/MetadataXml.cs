using System;
using System.Runtime.Serialization;
using System.ServiceModel.Description;
using System.ServiceModel.Configuration;

namespace NIntegrate.ServiceModel.Configuration
{
    /// <summary>
    /// Maps to the &lt;metadata&gt; configuration element.
    /// </summary>
    [DataContract(Namespace = "http://nintegrate.com")]
    public sealed class MetadataXml : ConfigurationXml
    {
        private MetadataElement _element;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataXml"/> class.
        /// </summary>
        /// <param name="xml">The XML.</param>
        public MetadataXml(string xml)
            : base(xml)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Applies the policy importers configuration.
        /// </summary>
        /// <param name="importer">The importer.</param>
        public void ApplyPolicyImportersConfiguration(MetadataImporter importer)
        {
            if (importer == null)
                return;

            if (_element == null)
            {
                lock (SyncLock)
                {
                    if (_element == null)
                        Deserialize();
                }
            }

            foreach (PolicyImporterElement item in _element.PolicyImporters)
            {
                var type = Activation.WcfServiceHostFactory.GetType(item.Type, true);
                var extension = Activator.CreateInstance(type) as IPolicyImportExtension;
                importer.PolicyImportExtensions.Add(extension);
            }
        }

        /// <summary>
        /// Applies the WSDL importers configuration.
        /// </summary>
        /// <param name="importer">The importer.</param>
        public void ApplyWsdlImportersConfiguration(WsdlImporter importer)
        {
            if (importer == null)
                return;

            if (_element == null)
            {
                lock (SyncLock)
                {
                    if (_element == null)
                        Deserialize();
                }
            }

            foreach (WsdlImporterElement item in _element.WsdlImporters)
            {
                var type = Activation.WcfServiceHostFactory.GetType(item.Type, true);
                var extension = Activator.CreateInstance(type) as IWsdlImportExtension;
                importer.WsdlImportExtensions.Add(extension);
            }
        }

        #endregion

        #region Non-Public Methods

        private void Deserialize()
        {
            _element = new MetadataElement();
            Deserialize(_element);
        }

        #endregion
    }
}
