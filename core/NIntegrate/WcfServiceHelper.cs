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
