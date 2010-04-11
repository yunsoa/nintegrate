using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using NIntegrate.ServiceModel.Configuration;

namespace NIntegrate.ServiceModel.Activation
{
    /// <summary>
    /// The derived ServiceHostFacory which reads service configuration from custom location
    /// </summary>
    public class WcfServiceHostFactory : ServiceHostFactory
    {
        #region Events

        /// <summary>
        /// Occurs on load service configuration.
        /// </summary>
        public event EventHandler<LoadServiceConfigurationEventArgs> OnLoadServiceConfiguration;

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates the service host.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        public ServiceHost CreateServiceHost(Type serviceType)
        {
            return CreateServiceHost(serviceType, new Uri[0]);
        }

        /// <summary>
        /// Creates the service host.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="baseAddresses">The base addresses.</param>
        /// <param name="serviceInstance">The service instance.</param>
        /// <param name="service">The service.</param>
        /// <returns></returns>
        public static ServiceHost CreateServiceHost(Type serviceType, ref Uri[] baseAddresses, object serviceInstance, WcfService service)
        {
            if (serviceType == null)
                throw new ArgumentNullException("serviceType");
            if (service == null)
                throw new ArgumentNullException("service");

            try
            {
                if (service.HostXml != null && (baseAddresses == null || baseAddresses.Length == 0))
                    baseAddresses = service.HostXml.CreateBaseAddresses();

                var serviceHost = InitializeServiceHost(serviceType, baseAddresses, serviceInstance, service);

                if (service.HostXml != null)
                    service.HostXml.ApplyHostTimeoutsConfiguration(serviceHost);

                if (service.ServiceBehaviorXml != null)
                    service.ServiceBehaviorXml.ApplyServiceBehaviorConfiguration(serviceHost);

                if (service.Endpoints != null)
                    AddEndpoints(serviceHost, baseAddresses, service);

                return serviceHost;
            }
            catch (Exception ex)
            {
                throw new ServiceHostCreationException(serviceType, baseAddresses, ex);
            }
        }

        /// <summary>
        /// Loads the service configuration.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        public virtual WcfService LoadServiceConfiguration(Type serviceType)
        {
            if (serviceType == null)
                return null;

            if (OnLoadServiceConfiguration != null)
            {
                var args = new LoadServiceConfigurationEventArgs { ServiceType = serviceType };
                OnLoadServiceConfiguration(this, args);
                if (args.Service != null)
                    return args.Service;
            }

            return AppConfigLoader.Default.LoadService(serviceType);
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <param name="typeName">Name of the type.</param>
        /// <param name="throwOnError">if set to <c>true</c> [throw on error].</param>
        /// <returns></returns>
        public static Type GetType(string typeName, bool throwOnError)
        {
            var type = Type.GetType(typeName);

            if (type == null)
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                for (var i = 0; i < assemblies.Length; i++)
                {
                    type = assemblies[i].GetType(typeName, false);
                    if (type != null)
                    {
                        break;
                    }
                }
            }

            if (type == null && throwOnError)
                throw new TypeLoadException(string.Format(SR.COULD_NOT_LOAD_TYPE, typeName));

            return type;
        }

        #endregion

        #region Non-Public Methods

        /// <summary>
        /// Called when service host creation exception raised.
        /// </summary>
        /// <param name="ex">The ex.</param>
        protected virtual void OnServiceHostCreationExceptionRaised(ServiceHostCreationException ex)
        {
        }

        /// <summary>
        /// Gets the service instance.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        protected virtual object GetServiceInstance(Type serviceType)
        {
            return null;
        }

        /// <summary>
        /// Creates a <see cref="T:System.ServiceModel.ServiceHost"/> for a specified type of service with a specific base address.
        /// </summary>
        /// <param name="serviceType">Specifies the type of service to host.</param>
        /// <param name="baseAddresses">The <see cref="T:System.Array"/> of type <see cref="T:System.Uri"/> that contains the base addresses for the service hosted.</param>
        /// <returns>
        /// A <see cref="T:System.ServiceModel.ServiceHost"/> for the type of service specified with a specific base address.
        /// </returns>
        protected sealed override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            if (serviceType == null)
                throw new ArgumentNullException("serviceType");

            try
            {
                var service = LoadServiceConfiguration(serviceType);
                var serviceHost = CreateServiceHost(serviceType, ref baseAddresses, GetServiceInstance(serviceType), service);
                return serviceHost;
            }
            catch (ServiceHostCreationException ex)
            {
                OnServiceHostCreationExceptionRaised(ex);

                throw;
            }
        }

        private static ServiceHost InitializeServiceHost(Type serviceType, Uri[] baseAddresses, object serviceInstance, WcfService service)
        {
            ServiceHost serviceHost;

            if (!string.IsNullOrEmpty(service.CustomServiceHostType))
            {
                var serviceHostType = GetType(service.CustomServiceHostType, true);
                if (serviceInstance != null)
                {
                    serviceHost =
                        Activator.CreateInstance(
                            serviceHostType,
                            new[] { serviceInstance, baseAddresses }, null) as ServiceHost;
                }
                else
                {
                    serviceHost =
                        Activator.CreateInstance(
                            serviceHostType,
                            new object[] { serviceType, baseAddresses }, null) as ServiceHost;
                }
            }
            else
            {
                if (serviceInstance != null)
                    serviceHost = new ServiceHost(serviceInstance, baseAddresses);
                else
                    serviceHost = new ServiceHost(serviceType, baseAddresses);
            }

            return serviceHost;
        }

        private static void AddEndpoints(ServiceHost serviceHost, Uri[] baseAddresses, WcfService service)
        {
            serviceHost.Description.Endpoints.Clear();

            var bindingCache = new Dictionary<string, Binding>();
            foreach (var endpoint in service.Endpoints)
            {
                var serviceContract =
                    string.Compare(
                        "IMetadataExchange",
                        endpoint.ServiceContractType, StringComparison.OrdinalIgnoreCase) == 0
                        ? typeof (IMetadataExchange)
                        : GetType(endpoint.ServiceContractType, true);
                if (serviceContract == null)
                    throw new ConfigurationErrorsException(string.Format(CultureInfo.InvariantCulture, SR.SPECIFIED_SERVICECONTRACT_COULD_NOT_BE_LOADED, endpoint.ServiceContractType));
                if (endpoint.BindingXml == null)
                    throw new ConfigurationErrorsException(string.Format(CultureInfo.InvariantCulture, SR.MISSING_BINDING_CONFIGURATION_FOR_ENDPOINT, endpoint.ListenUri));

                var listenUri = endpoint.BindingXml.CreateAddress(endpoint.ListenUri, baseAddresses);

                if (serviceContract == typeof(IMetadataExchange))
                {
                    var binding = endpoint.BindingXml.CreateBinding();
                    serviceHost.AddServiceEndpoint(serviceContract, binding, listenUri);
                }
                else
                {
                    // if endpoints share same address, they should share the same binding object instance
                    // so the binding configuration for the non-first endpoint sharing the same address 
                    // will be ignored.
                    Binding binding;
                    if (!string.IsNullOrEmpty(listenUri))
                    {
                        if (!bindingCache.TryGetValue(listenUri, out binding))
                        {
                            binding = endpoint.BindingXml.CreateBinding();
                            bindingCache[listenUri] = binding;
                        }
                    }
                    else
                    {
                        binding = endpoint.BindingXml.CreateBinding();
                    }

                    ServiceEndpoint serviceEndpoint;
                    if (endpoint.Address != endpoint.ListenUri)
                    {
                        serviceEndpoint = serviceHost.AddServiceEndpoint(
                            serviceContract, binding,
                            endpoint.Address, new Uri(listenUri));
                    }
                    else
                    {
                        serviceEndpoint = serviceHost.AddServiceEndpoint(
                            serviceContract, binding, listenUri);
                    }
                    var endpointAddress = CreateEndpointAddressWithHeadersAndIdentity(serviceEndpoint, endpoint);
                    serviceHost.Description.Endpoints.Remove(serviceEndpoint);
                    serviceEndpoint = new ServiceEndpoint(serviceEndpoint.Contract, binding, endpointAddress);
                    serviceHost.Description.Endpoints.Add(serviceEndpoint);

                    if (!string.IsNullOrEmpty(listenUri))
                        serviceEndpoint.ListenUri = new Uri(listenUri);
                    if (endpoint.ListenUriMode.HasValue)
                        serviceEndpoint.ListenUriMode = (ListenUriMode) Enum.Parse(typeof(ListenUriMode), endpoint.ListenUriMode.ToString());

                    if (endpoint.EndpointBehaviorXml != null)
                        endpoint.EndpointBehaviorXml.ApplyEndpointBehaviorConfiguration(serviceEndpoint);
                }
            }
        }

        private static EndpointAddress CreateEndpointAddressWithHeadersAndIdentity(ServiceEndpoint serviceEndpoint, WcfServiceEndpoint endpoint)
        {
            var endpointAddress =
                new EndpointAddress(serviceEndpoint.Address.Uri,
                                    endpoint.IdentityXml == null
                                        ? null
                                        : endpoint.IdentityXml.CreateEndpointIdentity(),
                                    endpoint.HeadersXml == null
                                        ? null
                                        : endpoint.HeadersXml.CreateAddressHeaders());
            return endpointAddress;
        }

        #endregion
    }
}