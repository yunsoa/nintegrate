using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.Xml;

namespace NIntegrate.ServiceModel.Configuration
{
    /// <summary>
    /// Maps to the &lt;behavior&gt; configuration element in &lt;serviceBehaviors&gt; element.
    /// </summary>
    [DataContract(Namespace = "http://nintegrate.com")]
    public sealed class ServiceBehaviorXml : BehaviorXml
    {
        private List<BehaviorExtensionElement> _customBehaviorElements;
        private ServiceBehaviorElement _serviceBehaviorElement;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBehaviorXml"/> class.
        /// </summary>
        /// <param name="xml">The XML.</param>
        public ServiceBehaviorXml(string xml)
            : base(xml)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Applies the service behavior configuration.
        /// </summary>
        /// <param name="serviceHost">The service host.</param>
        public void ApplyServiceBehaviorConfiguration(ServiceHost serviceHost)
        {
            if (_serviceBehaviorElement == null)
            {
                lock (SyncLock)
                {
                    if (_serviceBehaviorElement == null)
                    {
                        var doc = new XmlDocument();
                        doc.LoadXml(Xml);
                        _customBehaviorElements = FilterCustomBehaviorElements(doc);
                        _serviceBehaviorElement = new ServiceBehaviorElement();
                        Deserialize(doc.OuterXml, _serviceBehaviorElement);
                    }
                }
            }

            foreach (var item in _serviceBehaviorElement)
            {
                SetBehavior(serviceHost.Description.Behaviors, item);
            }
            foreach (var item in _customBehaviorElements)
            {
                SetBehavior(serviceHost.Description.Behaviors, item);
            }

            if (!IsServiceMetadataBehaviorConfigured(serviceHost))
            {
                var smb = new ServiceMetadataBehavior();
                serviceHost.Description.Behaviors.Add(smb);
            }
        }

        #endregion

        #region Non-Public Methods

        private static bool IsServiceMetadataBehaviorConfigured(ServiceHost serviceHost)
        {
            if (serviceHost != null)
            {
                foreach (var behavior in serviceHost.Description.Behaviors)
                {
                    if (behavior is ServiceMetadataBehavior)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion
    }
}
