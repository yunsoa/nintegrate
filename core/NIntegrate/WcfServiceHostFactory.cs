using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.Xml;
using NIntegrate.Configuration;
using System.Runtime.InteropServices;

namespace NIntegrate
{
    /// <summary>
    /// The WCF ServiceHost Factory
    /// </summary>
    [ComVisible(false)]
    public class WcfServiceHostFactory : ServiceHostFactory
    {
        #region Private Methods

        private static void ApplyServiceBehaviorConfiguration(ServiceHost serviceHost, ServiceConfiguration config)
        {
            if (!string.IsNullOrEmpty(config.ServiceBehaviorXML))
            {
                var doc = new XmlDocument();
                doc.LoadXml(config.ServiceBehaviorXML);
                var customBehaviorElements = new List<BehaviorExtensionElement>();
                foreach (XmlNode node in doc.FirstChild.ChildNodes)
                {
                    var customBehaviorTypeDesc = ServiceConfigurationStore.GetCustomBehaviorType(node.Name);
                    if (customBehaviorTypeDesc != null)
                    {
                        var customBehaviorElementType =
                            Type.GetType(customBehaviorTypeDesc.ConfigurationElementTypeClassName);
                        if (customBehaviorElementType == null)
                            throw new ConfigurationErrorsException(
                                string.Format(
                                    "Specified service behavior configuration element type - {0} could not be loaded!",
                                    customBehaviorTypeDesc.ConfigurationElementTypeClassName));
                        var customBehaviorElement =
                            Activator.CreateInstance(customBehaviorElementType) as BehaviorExtensionElement;
                        if (customBehaviorElement == null)
                            throw new ConfigurationErrorsException(
                                string.Format(
                                    "Specified service behavior configuration element type - {0} could not be initialized!",
                                    customBehaviorTypeDesc.ConfigurationElementTypeClassName));
                        customBehaviorElement.DeserializeElement(node.OuterXml);
                        customBehaviorElements.Add(customBehaviorElement);
                        node.ParentNode.RemoveChild(node);
                    }
                }
                var serviceBehaviorElement = new ServiceBehaviorElement();
                serviceBehaviorElement.DeserializeElement(config.ServiceBehaviorXML);
                foreach (var item in serviceBehaviorElement)
                {
                    serviceHost.Description.Behaviors.Add(item.CreateServiceBehavior());
                }
                foreach (var item in customBehaviorElements)
                {
                    serviceHost.Description.Behaviors.Add(item.CreateServiceBehavior());
                }
            }

            if (!IsBehaviorConfigured<ServiceMetadataBehavior>(serviceHost))
            {
                var smb = new ServiceMetadataBehavior();
                serviceHost.Description.Behaviors.Add(smb);
            }
        }

        private static void ApplyServiceHostConfiguration(ServiceHost serviceHost, HostElement hostElement)
        {
            if (hostElement != null && hostElement.Timeouts != null)
            {
                serviceHost.OpenTimeout = hostElement.Timeouts.OpenTimeout;
                serviceHost.CloseTimeout = hostElement.Timeouts.CloseTimeout;
            }
        }

        private ServiceHost GetServiceHost(ServiceConfiguration config, Type serviceImplType, object singleton, Uri[] baseAddresses)
        {
            var serviceHostTypeDesc = ServiceConfigurationStore.GetServiceHostType(config.ServiceHostType_id);
            var serviceHostType = Type.GetType(serviceHostTypeDesc.ClassName);
            if (serviceHostType == null)
                throw new ConfigurationErrorsException(string.Format("Specified service host type - {0} could not be loaded!", serviceHostTypeDesc.ClassName));
            baseAddresses = BuildBaseAddresses(baseAddresses, config);
            var serviceHost = (singleton == null ?
                Activator.CreateInstance(serviceHostType, new object[] { serviceImplType, baseAddresses }) as ServiceHost :
                Activator.CreateInstance(serviceHostType, new[] { singleton, baseAddresses }) as ServiceHost
            );
            if (serviceHost == null)
                throw new ConfigurationErrorsException(string.Format("Specified service host type - {0} could not be initialized!", serviceHostTypeDesc.ClassName));

            return serviceHost;
        }

        private static Uri[] GetBaseAddressesFromEndpoints(IList<EndpointConfiguration> endpoints)
        {
            var list = new List<Uri>();
            for (var i = 0; i < endpoints.Count; ++i)
            {
                var uri = new Uri(endpoints[i].EndpointAddress);
                if (!list.Contains(uri))
                {
                    list.Add(uri);
                }
            }
            return list.ToArray();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Check if the specified service behavior type - T is aleady configured
        /// </summary>
        /// <typeparam name="T">The service behavior type</typeparam>
        /// <param name="host">The service host being created</param>
        /// <returns>true or false</returns>
        internal protected static bool IsBehaviorConfigured<T>(ServiceHost host)
            where T : IServiceBehavior
        {
            if (host != null)
            {
                for (var i = 0; i < host.Description.Behaviors.Count; ++i)
                {
                    if (host.Description.Behaviors[i] is T)
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Build base addresses from endpoints
        /// </summary>
        /// <param name="baseAddresses">The baseAddresses passed in from outside</param>
        /// <param name="config">The service configuration</param>
        /// <returns>The base addresses</returns>
        protected virtual Uri[] BuildBaseAddresses(Uri[] baseAddresses, ServiceConfiguration config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            var list = new List<Uri>();
            foreach (var endpoint in config.Endpoints)
            {
                var address = WcfServiceHelper.BuildEndpointAddress(endpoint, baseAddresses);
                address = string.Format(address, Environment.MachineName.ToLowerInvariant());
                var uri = new Uri(address);
                if (!list.Contains(uri))
                    list.Add(uri);
            }

            return list.ToArray();
        }

        /// <summary>
        /// To be overriden to get service contracts from
        /// the service type specified in .svc file
        /// </summary>
        /// <param name="type">The service type specified in .svc file</param>
        /// <returns></returns>
        protected virtual IList<Type> GetServiceContractTypes(Type type)
        {
            return WcfServiceHelper.GetServiceContracts(type);
        }

        /// <summary>
        /// Create service host
        /// </summary>
        /// <param name="serviceType">The service implementation type</param>
        /// <param name="baseAddresses">
        ///     The baseAddress are always ignored because we could get the 
        ///     addresses from the endpoint config
        /// </param>
        /// <returns></returns>
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            if (serviceType == null)
                throw new ArgumentNullException("serviceType");

            var config = ServiceConfigurationStore.GetServiceConfiguration(serviceType.GetQualifiedTypeName());
            HostElement hostElement = null;
            if (!string.IsNullOrEmpty(config.HostXML))
            {
                hostElement = new HostElement();
                hostElement.DeserializeElement(config.HostXML);
                if (baseAddresses == null || baseAddresses.Length == 0)
                {
                    var baseAddressTemplates = WcfServiceHelper.GetBaseAddressesFromHostElement(hostElement);
                    if (baseAddressTemplates != null && baseAddressTemplates.Length > 0)
                    {
                        baseAddresses = new Uri[baseAddressTemplates.Length];
                        for (var i = 0; i < baseAddressTemplates.Length; ++i)
                        {
                            baseAddresses[i] = new Uri(string.Format(baseAddressTemplates[i], Environment.MachineName.ToLowerInvariant()));
                        }
                    }
                }
            }
            if (baseAddresses == null || baseAddresses.Length == 0)
            {
                baseAddresses = GetBaseAddressesFromEndpoints(config.Endpoints);
            }
            object singleton;
            var serviceImplType = ServiceManager.GetServiceImplementationType(serviceType, out singleton);
            var serviceHost = GetServiceHost(config, serviceImplType, singleton, baseAddresses);
            ApplyServiceHostConfiguration(serviceHost, hostElement);
            ApplyServiceBehaviorConfiguration(serviceHost, config);

            foreach (var endpointConfig in config.Endpoints)
            {
                var address = endpointConfig.ListenUri;
                address = (address == null ? string.Empty : string.Format(address, Environment.MachineName.ToLowerInvariant()));
                var serviceContract = Type.GetType(endpointConfig.ServiceContract);
                if (serviceContract == null)
                    throw new ConfigurationErrorsException(string.Format("Specified service contract - {0} could not be loaded!", endpointConfig.ServiceContract));

                var binding = WcfServiceHelper.GetBinding(endpointConfig);
                if (binding == null) continue;

                if (endpointConfig.MexBindingEnabled)
                {
                    serviceHost.AddServiceEndpoint(typeof(IMetadataExchange), new CustomBinding(binding), "mex");
                }

                if (!endpointConfig.AddMexBindingOnly)
                {
                    ServiceEndpoint serviceEndpoint;
                    if (endpointConfig.EndpointAddress != endpointConfig.ListenUri)
                    {
                        serviceEndpoint = serviceHost.AddServiceEndpoint(
                            serviceContract, binding,
                            endpointConfig.EndpointAddress, new Uri(address));
                    }
                    else
                    {
                        serviceEndpoint = serviceHost.AddServiceEndpoint(
                            serviceContract, binding, address);
                    }
                    if (endpointConfig.ListenUriMode.HasValue)
                        serviceEndpoint.ListenUriMode = (ListenUriMode) Enum.Parse(
                                                                            typeof (ListenUriMode),
                                                                            endpointConfig.ListenUriMode.ToString());

                    WcfServiceHelper.ApplyEndpointBehaviorConfiguration(serviceEndpoint, endpointConfig);
                }
            }

            return serviceHost;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Create service host
        /// </summary>
        /// <param name="serviceType">The service implementation type</param>
        /// <returns></returns>
        public ServiceHost CreateServiceHost(Type serviceType)
        {
            return CreateServiceHost(serviceType, new Uri[0]);
        }

        #endregion
    }
}
