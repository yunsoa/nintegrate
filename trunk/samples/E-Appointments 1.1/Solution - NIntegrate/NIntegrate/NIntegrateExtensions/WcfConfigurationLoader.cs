using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NIntegrate.ServiceModel.Configuration;
using System.Xml;
using System.Reflection;
using System.Configuration;

namespace NIntegrateExtensions
{
    /// <summary>
    /// Demo class loading Wcf configuration from an embbed xml file, not thread-safe
    /// </summary>
    public static class WcfConfigurationLoader
    {
        private static readonly XmlDocument _xmlDoc;

        static WcfConfigurationLoader()
        {
            _xmlDoc = new XmlDocument();

            using (var stream = typeof(WcfConfigurationLoader).Assembly.GetManifestResourceStream("NIntegrateExtensions.WcfConfiguration.xml"))
            {
                _xmlDoc.Load(stream);
                stream.Close();
            }
        }

        public static WcfService LoadWcfServiceConfiguration(Type serviceType)
        {
            var serviceElement = _xmlDoc.SelectSingleNode("/WcfConfiguration/Host/service[@name='" + serviceType.ToString() + "']");
            var serviceConfig = new WcfService { ServiceType = serviceType.AssemblyQualifiedName };
            var behavirConfigName = serviceElement.Attributes["behaviorConfiguration"].Value;
            var behaviorConfig = _xmlDoc.SelectSingleNode("/WcfConfiguration/Behaviors/behavior[@name='" + behavirConfigName + "']");
            serviceConfig.ServiceBehaviorXml = new ServiceBehaviorXml(behaviorConfig.OuterXml);
            var endpointConfigs = new List<WcfServiceEndpoint>();
            foreach (XmlNode endpointElement in serviceElement.SelectNodes("endpoint"))
            {
                var endpointConfig = new WcfServiceEndpoint
                {
                    ServiceContractType = endpointElement.Attributes["contract"].Value,
                };
                if (endpointElement.Attributes["address"] != null)
                    endpointConfig.Address = endpointElement.Attributes["address"].Value;
                var bindingTypeCode = endpointElement.Attributes["binding"].Value;
                if (endpointElement.Attributes["bindingConfiguration"] == null)
                    endpointConfig.BindingXml = new BindingXml(bindingTypeCode, null);
                else
                {
                    var bindingConfigName = endpointElement.Attributes["bindingConfiguration"].Value;
                    var bindingElement = _xmlDoc.SelectSingleNode("/WcfConfiguration/Bindings/binding[@name='" + bindingConfigName + "']");
                    endpointConfig.BindingXml = new BindingXml(bindingTypeCode, bindingElement.OuterXml);
                }
                endpointConfigs.Add(endpointConfig);
            }
            serviceConfig.Endpoints = endpointConfigs.ToArray();
            return serviceConfig;
        }

        public static WcfClientEndpoint LoadWcfClientEndpointConfiguration(Type serviceContractType)
        {
            var endpointElement = _xmlDoc.SelectSingleNode("/WcfConfiguration/Client/endpoint[@contract='" + serviceContractType.ToString() + "']");
            var endpointConfig = new WcfClientEndpoint
            {
                ServiceContractType = endpointElement.Attributes["contract"].Value,
            };
            if (endpointElement.Attributes["address"] != null)
                endpointConfig.Address = endpointElement.Attributes["address"].Value;
            var bindingTypeCode = endpointElement.Attributes["binding"].Value;
            if (endpointElement.Attributes["bindingConfiguration"] == null)
                endpointConfig.BindingXml = new BindingXml(bindingTypeCode, null);
            else
            {
                var bindingConfigName = endpointElement.Attributes["bindingConfiguration"].Value;
                var bindingElement = _xmlDoc.SelectSingleNode("/WcfConfiguration/Bindings/binding[@name='" + bindingConfigName + "']");
                endpointConfig.BindingXml = new BindingXml(bindingTypeCode, bindingElement.OuterXml);
            }
            return endpointConfig;
        }
    }
}
