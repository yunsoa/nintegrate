using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using NIntegrate.ServiceModel.Configuration;

namespace NIntegrate.ServiceModel.Activation
{
    public abstract class WcfServiceHostFactory : ServiceHostFactory
    {
        #region Public Methods

        public ServiceHost CreateServiceHost(Type serviceType)
        {
            return CreateServiceHost(serviceType, new Uri[0]);
        }

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

        #endregion

        #region Non-Public Methods

        #region LoadServiceConfiguration Default Implementation


        public virtual WcfService LoadServiceConfiguration(Type serviceType)
        {
            if (serviceType == null)
                return null;

            AppConfigLoader.EnsureExtensionsLoaded();

            return AppConfigLoader.LoadService(serviceType);
        }

        #endregion

        protected virtual void OnServiceHostCreationExceptionRaised(ServiceHostCreationException ex)
        {
        }

        protected virtual object GetServiceInstance(Type serviceType)
        {
            return null;
        }

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
                var serviceHostType = Type.GetType(service.CustomServiceHostType, true);
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
            var bindingCache = new Dictionary<string, Binding>();
            foreach (var endpoint in service.Endpoints)
            {
                var serviceContract =
                    string.Compare(
                        "IMetadataExchange",
                        endpoint.ServiceContractType, StringComparison.OrdinalIgnoreCase) == 0
                        ? typeof (IMetadataExchange)
                        : Type.GetType(endpoint.ServiceContractType);
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
                    EndpointAddress endpointAddress;
                    if (endpoint.Address != endpoint.ListenUri)
                    {
                        serviceEndpoint = serviceHost.AddServiceEndpoint(
                            serviceContract, binding,
                            endpoint.Address, new Uri(listenUri));

                        endpointAddress = CreateEndpointAddressWithHeadersAndIdentity(serviceEndpoint, endpoint);
                    }
                    else
                    {
                        serviceEndpoint = serviceHost.AddServiceEndpoint(
                            serviceContract, binding, listenUri);

                        endpointAddress = CreateEndpointAddressWithHeadersAndIdentity(serviceEndpoint, endpoint);
                    }
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