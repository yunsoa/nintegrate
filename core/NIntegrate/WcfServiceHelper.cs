using System;
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

        internal static Uri BuildEndpointAddress(EndpointConfiguration endpoint, Uri[] baseAddresses)
        {
            Uri address;
            if (!string.IsNullOrEmpty(endpoint.EndpointAddress))
            {
                if (endpoint.EndpointAddress.Contains("://")) //is absolute url
                {
                    address = new Uri(endpoint.EndpointAddress);
                }
                else // is relative url to baseAddress
                {
                    var baseAddress = GetBaseAddress(baseAddresses, endpoint);
                    address = new Uri(baseAddress + endpoint.EndpointAddress);
                }
            }
            else //use baseAddress directly
            {
                var baseAddress = GetBaseAddress(baseAddresses, endpoint);
                address = new Uri(baseAddress);
            }
            return address;
        }

        private static string GetBaseAddress(Uri[] baseAddresses, EndpointConfiguration endpointConfig)
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
            foreach (var item in baseAddresses)
            {
                if (item.ToString().ToLowerInvariant().StartsWith(addressPrefix))
                {
                    return item.ToString();
                }
            }

            return null;
        }

        internal static Uri[] GetBaseAddressesFromHostElement(HostElement hostElement)
        {
            Uri[] addresses = null;
            addresses = new Uri[hostElement.BaseAddresses.Count];
            for (var i = 0; i < hostElement.BaseAddresses.Count; ++i)
            {
                addresses[i] = new Uri(hostElement.BaseAddresses[i].BaseAddress);
            }
            return addresses;
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
