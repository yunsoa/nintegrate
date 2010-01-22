using System;
using System.Runtime.Serialization;
using System.ServiceModel.Description;
using System.ServiceModel.Configuration;

namespace NIntegrate.ServiceModel.Configuration
{
    /// <summary>
    /// Maps to the &lt;metadata&gt; configuration element.
    /// </summary>
    [DataContract]
    public sealed class MetadataXml : ConfigurationXml
    {
        private MetadataElement _element;

        #region Constructors

        public MetadataXml(string xml)
            : base(xml)
        {
        }

        #endregion

        #region Public Methods

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
