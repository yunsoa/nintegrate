using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
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
            for (var i = 0; i < host.Description.Behaviors.Count; ++i)
            {
                if (host.Description.Behaviors[i] is T)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Build base addresses from endpoints
        /// </summary>
        /// <param name="serviceContracts">The service contracts</param>
        /// <returns>The base addresses</returns>
        protected static Uri[] BuildBaseAddresses(IList<Type> serviceContracts)
        {
            var list = new List<Uri>();

            foreach (var serviceContract in serviceContracts)
            {
                var endpoints = EndpointStore.GetServerEndpoints(serviceContract);
                foreach (var endpoint in endpoints)
                {
                    var address = WcfServiceHelper.BuildAddress(endpoint);
                    if (address != default(Uri))
                        list.Add(address);
                }
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
        /// To be overriden to get service implementation type from
        /// the service type specified in .svc file
        /// </summary>
        /// <param name="type">The service type specified in .svc file</param>
        /// <returns></returns>
        protected virtual Type GetServiceImplementationType(Type type)
        {
            return type;
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
            var serviceContracts = GetServiceContractTypes(serviceType);
            var serviceImplType = GetServiceImplementationType(serviceType);

            var host = new ServiceHost(serviceImplType, BuildBaseAddresses(serviceContracts));
            foreach (var serviceContract in serviceContracts)
            {
                var endpoints = EndpointStore.GetServerEndpoints(serviceContract);
                if (endpoints.Count == 0)
                    return host;

                foreach (var endpoint in endpoints)
                {
                    var address = WcfServiceHelper.BuildAddress(endpoint);
                    if (address == default(Uri)) continue;

                    var binding = WcfServiceHelper.BuildBinding(serviceContract, endpoint);

                    if (binding == null) continue;

                    if (!IsBehaviorConfigured<ServiceMetadataBehavior>(host))
                    {
                        var smb = new ServiceMetadataBehavior();
                        host.Description.Behaviors.Add(smb);
                    }

                    if (endpoint.MexBindingEnabled)
                    {
                        host.AddServiceEndpoint(typeof (IMetadataExchange), new CustomBinding(binding), "mex");
                    }

                    if (!IsBehaviorConfigured<ServiceThrottlingBehavior>(host))
                    {
                        var serviceThrottle = new ServiceThrottlingBehavior();
                        if (endpoint.MaxConcurrentCalls.HasValue)
                            serviceThrottle.MaxConcurrentCalls = endpoint.MaxConcurrentCalls.Value;
                        if (endpoint.MaxConcurrentInstances.HasValue)
                            serviceThrottle.MaxConcurrentInstances = endpoint.MaxConcurrentInstances.Value;
                        if (endpoint.MaxConcurrentSessions.HasValue)
                            serviceThrottle.MaxConcurrentSessions = endpoint.MaxConcurrentSessions.Value;
                        host.Description.Behaviors.Add(serviceThrottle);
                    }

                    if (!IsBehaviorConfigured<ServiceDebugBehavior>(host) && endpoint.IncludeExceptionDetailInFaults.HasValue && endpoint.IncludeExceptionDetailInFaults.Value)
                    {
                        var serviceDebug = new ServiceDebugBehavior
                                               {
                                                   IncludeExceptionDetailInFaults =
                                                       endpoint.IncludeExceptionDetailInFaults.Value
                                               };
                        host.Description.Behaviors.Add(serviceDebug);
                    }

                    host.AddServiceEndpoint(serviceContract, binding, address);
                }
            }

            return host;
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
