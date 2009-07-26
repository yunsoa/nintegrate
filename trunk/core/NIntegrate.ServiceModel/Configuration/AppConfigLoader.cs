using System;
using System.Configuration;
using System.IO;
using System.ServiceModel.Configuration;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Globalization;

namespace NIntegrate.ServiceModel.Configuration
{
    internal static class AppConfigLoader
    {
        private static bool _defaultExtensionsLoaded;
        private static readonly object _syncLock = new object();

        private static readonly MethodInfo _methodSerializeElement = typeof(ConfigurationElement).GetMethod(
                "SerializeElement",
                BindingFlags.NonPublic | BindingFlags.Instance);

        #region Public Methods

        public static void EnsureExtensionsLoaded()
        {
            if (!_defaultExtensionsLoaded)
            {
                lock (_syncLock)
                {
                    if (!_defaultExtensionsLoaded)
                    {

                        var extensions =
                            ConfigurationManager.GetSection("system.serviceModel/extensions") as ExtensionsSection;
                        if (extensions.BehaviorExtensions != null)
                        {
                            foreach (ExtensionElement item in extensions.BehaviorExtensions)
                            {
                                var type = Type.GetType(item.Type);
                                BehaviorExtensionRegistry.Instance.AddItem(item.Name, type);
                            }
                        }
                        if (extensions.BindingElementExtensions != null)
                        {
                            foreach (ExtensionElement item in extensions.BindingElementExtensions)
                            {
                                var type = Type.GetType(item.Type);
                                BindingElementExtensionRegistry.Instance.AddItem(item.Name, type);
                            }
                        }
                        if (extensions.BindingExtensions != null)
                        {
                            foreach (ExtensionElement item in extensions.BindingExtensions)
                            {
                                var type = Type.GetType(item.Type);
                                while (type != typeof (object) &&
                                       string.Compare(type.Name, "StandardBindingCollectionElement",
                                                      StringComparison.OrdinalIgnoreCase) != 0)
                                {
                                    type = type.BaseType;
                                }

                                //get binding type and binding element type from generic types
                                if (type.IsGenericType &&
                                    string.Compare(type.Name, "StandardBindingCollectionElement",
                                                   StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    var genericTypes = type.GetGenericArguments();
                                    if (genericTypes != null && genericTypes.Length == 2)
                                    {
                                        var bindingTypeDesc =
                                            new BindingTypeDescription
                                                {
                                                    BindingType = genericTypes[0],
                                                    BindingConfigurationElementType = genericTypes[1]
                                                };
                                        var bindingTypeCode = item.Name.ToLowerInvariant();
                                        if (bindingTypeCode.Contains("http"))
                                            bindingTypeDesc.AvailableProtocols = new[] {"http", "https"};
                                        else if (bindingTypeCode.Contains("tcp"))
                                            bindingTypeDesc.AvailableProtocols = new[] {"net.tcp"};
                                        else if (bindingTypeCode.Contains("msmq"))
                                            bindingTypeDesc.AvailableProtocols = new[] {"net.msmq"};
                                        else if (bindingTypeCode.Contains("pipe") || bindingTypeCode.Contains("ipc"))
                                            bindingTypeDesc.AvailableProtocols = new[] {"net.pipe"};
                                        else
                                            bindingTypeDesc.AvailableProtocols = new[] {"http", "https"};

                                        BindingTypeRegistry.Instance.AddItem(bindingTypeCode, bindingTypeDesc);
                                    }
                                }
                            }
                        }

                        _defaultExtensionsLoaded = true;
                    }
                }
            }
        }

        public static WcfService LoadService(Type serviceType)
        {
            var servicesSection = ConfigurationManager.GetSection("system.serviceModel/services") as ServicesSection;
            var behaviorsSection = ConfigurationManager.GetSection("system.serviceModel/behaviors") as BehaviorsSection;
            var bindingsSection = ConfigurationManager.GetSection("system.serviceModel/bindings") as BindingsSection;

            if (servicesSection != null)
            {
                foreach (ServiceElement item in servicesSection.Services)
                {
                    if (string.Compare(serviceType.FullName, item.Name, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        var service = new WcfService();
                        if (item.Host != null)
                            service.HostXml = new HostXml(Serialize(item.Host));
                        if (!string.IsNullOrEmpty(item.BehaviorConfiguration))
                        {
                            if (behaviorsSection != null && behaviorsSection.ServiceBehaviors != null)
                            {
                                foreach (ServiceBehaviorElement behavior in behaviorsSection.ServiceBehaviors)
                                {
                                    if (string.Compare(item.BehaviorConfiguration, behavior.Name, StringComparison.OrdinalIgnoreCase) == 0)
                                    {
                                        service.ServiceBehaviorXml = new ServiceBehaviorXml(Serialize(behavior));
                                        break;
                                    }
                                }
                            }
                        }

                        if (item.Endpoints != null)
                        {
                            var i = 0;
                            service.Endpoints = new WcfServiceEndpoint[item.Endpoints.Count];
                            foreach (ServiceEndpointElement serviceEndpoint in item.Endpoints)
                            {
                                var endpoint = new WcfServiceEndpoint();
                                if (serviceEndpoint.Address != default(Uri))
                                    endpoint.Address = serviceEndpoint.Address.ToString();
                                if (!string.IsNullOrEmpty(serviceEndpoint.Binding))
                                {
                                    string xml = null;
                                    if (!string.IsNullOrEmpty(serviceEndpoint.BindingConfiguration))
                                    {
                                        foreach (var bindingElement in bindingsSection[serviceEndpoint.Binding].ConfiguredBindings)
                                        {
                                            if (string.Compare(serviceEndpoint.BindingConfiguration, bindingElement.Name, StringComparison.OrdinalIgnoreCase) == 0)
                                            {
                                                xml = Serialize(bindingElement as ConfigurationElement);
                                                break;
                                            }
                                        }
                                    }
                                    endpoint.BindingXml = new BindingXml(serviceEndpoint.Binding, xml);
                                }
                                if (!string.IsNullOrEmpty(serviceEndpoint.BehaviorConfiguration))
                                {
                                    if (behaviorsSection != null && behaviorsSection.ServiceBehaviors != null)
                                    {
                                        foreach (EndpointBehaviorElement behavior in behaviorsSection.EndpointBehaviors)
                                        {
                                            if (string.Compare(serviceEndpoint.BehaviorConfiguration, behavior.Name, StringComparison.OrdinalIgnoreCase) == 0)
                                            {
                                                endpoint.EndpointBehaviorXml = new EndpointBehaviorXml(Serialize(behavior));
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (serviceEndpoint.Headers != null)
                                    endpoint.HeadersXml = new HeadersXml(Serialize(serviceEndpoint.Headers));
                                if (serviceEndpoint.Identity != null)
                                    endpoint.IdentityXml = new IdentityXml(Serialize(serviceEndpoint.Identity));
                                if (serviceEndpoint.ListenUri != default(Uri))
                                    endpoint.ListenUri = serviceEndpoint.ListenUri.ToString();
                                endpoint.ListenUriMode = (WcfListenUriMode)Enum.Parse(typeof(WcfListenUriMode), serviceEndpoint.ListenUri.ToString());
                                endpoint.ServiceContractType = serviceEndpoint.Contract;

                                service.Endpoints[i++] = endpoint;
                            }
                        }

                        return service;
                    }
                }
            }

            return null;
        }

        public static WcfClientEndpoint LoadClientEndpoint(Type serviceContractType)
        {
            if (serviceContractType == null)
                return null;

            var clientSection = ConfigurationManager.GetSection("system.serviceModel/client") as ClientSection;
            var behaviorsSection = ConfigurationManager.GetSection("system.serviceModel/behaviors") as BehaviorsSection;
            var bindingsSection = ConfigurationManager.GetSection("system.serviceModel/bindings") as BindingsSection;

            if (clientSection != null && clientSection.Endpoints != null)
            {
                foreach (ChannelEndpointElement channelEndpoint in clientSection.Endpoints)
                {
                    if (
                        string.Compare(serviceContractType.FullName, channelEndpoint.Contract,
                                       StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        var endpoint = new WcfClientEndpoint();

                        if (channelEndpoint.Address != default(Uri))
                            endpoint.Address = channelEndpoint.Address.ToString();
                        if (!string.IsNullOrEmpty(channelEndpoint.Binding))
                        {
                            string xml = null;
                            if (!string.IsNullOrEmpty(channelEndpoint.BindingConfiguration))
                            {
                                foreach (
                                    var bindingElement in bindingsSection[channelEndpoint.Binding].ConfiguredBindings)
                                {
                                    if (
                                        string.Compare(channelEndpoint.BindingConfiguration, bindingElement.Name,
                                                       StringComparison.OrdinalIgnoreCase) == 0)
                                    {
                                        xml = Serialize(bindingElement as ConfigurationElement);
                                        break;
                                    }
                                }
                            }
                            endpoint.BindingXml = new BindingXml(channelEndpoint.Binding, xml);
                        }
                        if (!string.IsNullOrEmpty(channelEndpoint.BehaviorConfiguration))
                        {
                            if (behaviorsSection != null && behaviorsSection.ServiceBehaviors != null)
                            {
                                foreach (EndpointBehaviorElement behavior in behaviorsSection.EndpointBehaviors)
                                {
                                    if (
                                        string.Compare(channelEndpoint.BehaviorConfiguration, behavior.Name,
                                                       StringComparison.OrdinalIgnoreCase) == 0)
                                    {
                                        endpoint.EndpointBehaviorXml = new EndpointBehaviorXml(Serialize(behavior));
                                        break;
                                    }
                                }
                            }
                        }
                        if (channelEndpoint.Headers != null)
                            endpoint.HeadersXml = new HeadersXml(Serialize(channelEndpoint.Headers));
                        if (channelEndpoint.Identity != null)
                            endpoint.IdentityXml = new IdentityXml(Serialize(channelEndpoint.Identity));
                        endpoint.ServiceContractType = channelEndpoint.Contract;

                        return endpoint;
                    }
                }
            }

            return null;
        }

        #endregion 

        #region Non-Public Methods

        private static string Serialize(ConfigurationElement element)
        {
            if (element == null)
                throw new ArgumentNullException("element");

            var sb = new StringBuilder();
            var wr = new XmlTextWriter(new StringWriter(sb, CultureInfo.InvariantCulture));
            _methodSerializeElement.Invoke(element, new object[] { wr, false });

            return sb.ToString();
        }

        #endregion

    }
}
