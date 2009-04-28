using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Xml;
using NIntegrate.Configuration;
using System.ServiceModel.Configuration;

namespace NIntegrate
{
    internal static class WcfServiceHelper
    {
        internal static string GetQualifiedTypeName(this Type type)
        {
            var typeName = type.AssemblyQualifiedName;
            if (typeName.EndsWith("PublicKeyToken=null"))
                return type.FullName + ", " + type.Assembly.GetName().Name;
            return typeName;
        }

        private static string _configurationConnectionString;

        internal static string GetConfigurationConnectionString()
        {
            if (_configurationConnectionString == null)
            {
                lock (typeof(WcfServiceHelper))
                {
                    if (_configurationConnectionString == null)
                    {
                        var connStr =
                            ConfigurationManager.ConnectionStrings[Constants.ConfigurationDatabaseConnectionStringName];
                        if (connStr == null)
                            throw new ConfigurationErrorsException(
                                "Could not find ConnectionString setting for \"NIntegrate.Configuration\".");
                        _configurationConnectionString = connStr.ConnectionString;
                    }
                }
            }

            return _configurationConnectionString;
        }

        internal static Binding GetBinding(EndpointConfiguration endpoint)
        {
            var bindingTypeDesc = ServiceConfigurationStore.GetBindingType(endpoint.BindingType_id);
            var bindingType = Type.GetType(bindingTypeDesc.ClassName);
            if (bindingType == null)
                throw new ConfigurationErrorsException(string.Format("Specified binding type - {0} could not be loaded!", bindingTypeDesc.ClassName));
            var binding = Activator.CreateInstance(bindingType) as Binding;
            if (binding == null)
                throw new ConfigurationErrorsException(string.Format("Specified binding type - {0} could not be initialized!", bindingTypeDesc.ClassName));

            var bindingElementType = Type.GetType(bindingTypeDesc.ConfigurationElementTypeClassName);
            if (bindingElementType == null)
                throw new ConfigurationErrorsException(string.Format("Specified binding configuration element type - {0} could not be loaded!", bindingTypeDesc.ConfigurationElementTypeClassName));
            var bindingElement = Activator.CreateInstance(bindingElementType) as StandardBindingElement;
            if (bindingElement == null)
                throw new ConfigurationErrorsException(string.Format("Specified binding binding configuration element type - {0} could not be initialized!", bindingTypeDesc.ConfigurationElementTypeClassName));
            bindingElement.DeserializeElement(endpoint.BindingXML);
            bindingElement.ApplyConfiguration(binding);

            return binding;
        }

        /// <summary>
        /// Get all implemented interfaces of the service implementation type 
        /// marked with ServiceContractAttribute
        /// </summary>
        /// <param name="serviceType">The service implementation type</param>
        /// <returns></returns>
        internal static List<Type> GetServiceContracts(Type serviceType)
        {
            var list = new List<Type>();

            var attrs = serviceType.GetCustomAttributes(typeof(ServiceContractAttribute), false);
            if (attrs != null && attrs.Length > 0)
                list.Add(serviceType);

            foreach (var type in serviceType.GetInterfaces())
            {
                attrs = type.GetCustomAttributes(typeof(ServiceContractAttribute), false);
                if (attrs != null && attrs.Length > 0 && !list.Contains(type))
                    list.Add(type);
            }

            return list;
        }

        internal static string BuildEndpointAddress(EndpointConfiguration endpoint, IEnumerable baseAddresses)
        {
            string address;
            if (!string.IsNullOrEmpty(endpoint.ListenUri))
            {
                if (endpoint.ListenUri.Contains("://")) //is absolute url
                {
                    address = endpoint.ListenUri;
                }
                else // is relative url to baseAddress
                {
                    var baseAddress = GetBaseAddress(baseAddresses, endpoint);
                    address = baseAddress.TrimEnd('/') + '/' + endpoint.ListenUri;
                }
            }
            else //use baseAddress directly
            {
                var baseAddress = GetBaseAddress(baseAddresses, endpoint);
                address = baseAddress;
            }
            return address;
        }

        private static string GetBaseAddress(IEnumerable baseAddresses, EndpointConfiguration endpointConfig)
        {
            var channelType = ServiceConfigurationStore.GetBindingType(endpointConfig.BindingType_id).ChannelType;
            var addressPrefix = "http";
            switch (channelType)
            {
                case ChannelType.HTTP:
                    addressPrefix = "http";
                    break;
                case ChannelType.TCP:
                    addressPrefix = "net.tcp";
                    break;
                case ChannelType.IPC:
                    addressPrefix = "net.pipe";
                    break;
                case ChannelType.MSMQ:
                    addressPrefix = "net.msmq";
                    break;
            }
            if (baseAddresses != null)
            {
                foreach (var item in baseAddresses)
                {
                    if (item.ToString().ToLowerInvariant().StartsWith(addressPrefix))
                    {
                        return item.ToString();
                    }
                }
            }

            throw new ConfigurationErrorsException("Could not find a base address in configuration store for channel type - " + channelType);
        }

        internal static string[] GetBaseAddressesFromHostElement(HostElement hostElement)
        {
            var list = new List<string>();
            if (hostElement != null && hostElement.BaseAddresses != null)
            {
                for (var i = 0; i < hostElement.BaseAddresses.Count; ++i)
                {
                    if (!list.Contains(hostElement.BaseAddresses[i].BaseAddress))
                        list.Add(hostElement.BaseAddresses[i].BaseAddress);
                }
            }
            return list.ToArray();
        }

        internal static void ApplyEndpointBehaviorConfiguration(ServiceEndpoint endpoint, EndpointConfiguration config)
        {
            if (string.IsNullOrEmpty(config.EndpointBehaviorXML))
                return;

            var doc = new XmlDocument();
            doc.LoadXml(config.EndpointBehaviorXML);
            var customBehaviorElements = new List<BehaviorExtensionElement>();
            foreach (XmlNode node in doc.FirstChild.ChildNodes)
            {
                var customBehaviorTypeDesc = ServiceConfigurationStore.GetCustomBehaviorType(node.Name);
                if (customBehaviorTypeDesc != null)
                {
                    var customBehaviorElementType = Type.GetType(customBehaviorTypeDesc.ConfigurationElementTypeClassName);
                    if (customBehaviorElementType == null)
                        throw new ConfigurationErrorsException(string.Format("Specified endpoint behavior configuration element type - {0} could not be loaded!", customBehaviorTypeDesc.ConfigurationElementTypeClassName));
                    var customBehaviorElement = Activator.CreateInstance(customBehaviorElementType) as BehaviorExtensionElement;
                    if (customBehaviorElement == null)
                        throw new ConfigurationErrorsException(string.Format("Specified endpoint behavior configuration element type - {0} could not be initialized!", customBehaviorTypeDesc.ConfigurationElementTypeClassName));
                    customBehaviorElement.DeserializeElement(node.OuterXml);
                    customBehaviorElements.Add(customBehaviorElement);
                    node.ParentNode.RemoveChild(node);
                }
            }
            var endpointBehaviorElement = new EndpointBehaviorElement();
            endpointBehaviorElement.DeserializeElement(config.EndpointBehaviorXML);
            foreach (var item in endpointBehaviorElement)
            {
                endpoint.Behaviors.Add(item.CreateEndpointBehavior());
            }
            foreach (var item in customBehaviorElements)
            {
                endpoint.Behaviors.Add(item.CreateEndpointBehavior());
            }
        }

        #region DeserializeElement

        private static readonly MethodInfo _configElementDeserializeMethod
            = typeof(ConfigurationElement).GetMethod("DeserializeElement", BindingFlags.NonPublic | BindingFlags.Instance);

        internal static void DeserializeElement(this ConfigurationElement element, string serializedXML)
        {
            if (string.IsNullOrEmpty(serializedXML))
                return;

            var rdr = new XmlTextReader(new StringReader(serializedXML));
            rdr.Read();
            rdr.ReadSubtree();
            _configElementDeserializeMethod.Invoke(element, new object[] { rdr, false });
        }

        #endregion

        #region CreateBehavior

        private static readonly MethodInfo _createBehaviorMethod
            = typeof(BehaviorExtensionElement).GetMethod("CreateBehavior", BindingFlags.NonPublic | BindingFlags.Instance);

        internal static IServiceBehavior CreateServiceBehavior(this BehaviorExtensionElement element)
        {
            return (IServiceBehavior)_createBehaviorMethod.Invoke(element, null);
        }

        internal static IEndpointBehavior CreateEndpointBehavior(this BehaviorExtensionElement element)
        {
            return (IEndpointBehavior)_createBehaviorMethod.Invoke(element, null);
        }

        #endregion
    }
}
