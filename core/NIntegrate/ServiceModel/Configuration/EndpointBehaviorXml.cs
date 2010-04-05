using System.Collections.Generic;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.Runtime.Serialization;
using System.Xml;

namespace NIntegrate.ServiceModel.Configuration
{
    /// <summary>
    /// Maps to the &lt;behavior&gt; configuration element in &lt;endpointBehaviors&gt; element.
    /// </summary>
    [DataContract(Namespace = "http://nintegrate.com")]
    public sealed class EndpointBehaviorXml : BehaviorXml
    {
        private List<BehaviorExtensionElement> _customBehaviorElements;
        private EndpointBehaviorElement _endpointBehaviorElement;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EndpointBehaviorXml"/> class.
        /// </summary>
        /// <param name="xml">The XML.</param>
        public EndpointBehaviorXml(string xml)
            : base(xml)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Applies the endpoint behavior configuration.
        /// </summary>
        /// <param name="serviceEndpoint">The service endpoint.</param>
        public void ApplyEndpointBehaviorConfiguration(ServiceEndpoint serviceEndpoint)
        {
            if (_endpointBehaviorElement == null)
            {
                lock (SyncLock)
                {
                    if (_endpointBehaviorElement == null)
                    {
                        var doc = new XmlDocument();
                        doc.LoadXml(Xml);
                        _customBehaviorElements = FilterCustomBehaviorElements(doc);
                        _endpointBehaviorElement = new EndpointBehaviorElement();
                        Deserialize(doc.OuterXml, _endpointBehaviorElement);
                    }
                }
            }

            foreach (var item in _endpointBehaviorElement)
            {
                SetBehavior(serviceEndpoint.Behaviors, item);
            }
            foreach (var item in _customBehaviorElements)
            {
                SetBehavior(serviceEndpoint.Behaviors, item);
            }
        }

        #endregion
    }
}
