using System;
using System.Collections.Generic;
using System.Configuration;

namespace NIntegrate.Configuration
{
    public sealed class EndpointStore
    {
        #region Private Singleton

        private static readonly EndpointStore _singleton = new EndpointStore();

        #endregion

        #region Private Constructor

        private readonly IEndpointProvider _provider;

        private EndpointStore()
        {
            var providerTypeName = ConfigurationManager.AppSettings[Constants.EndpointProviderTypeAppSettingName];
            if (!string.IsNullOrEmpty(providerTypeName))
            {
                var providerType = Type.GetType(providerTypeName);
                if (providerType != null)
                    _provider = (IEndpointProvider) Activator.CreateInstance(providerType);
            }

            if (_provider == null)
                _provider = new DefaultEndpointProvider();
        }

        #endregion

        #region Private Endpoint Cache

        private static readonly Dictionary<Type, IList<Endpoint>> _cachedServerEndpoints 
            = new Dictionary<Type, IList<Endpoint>>();
        private static readonly Dictionary<Type, IList<Endpoint>> _cachedClientEndpoints 
            = new Dictionary<Type, IList<Endpoint>>();

        #endregion

        public static IList<Endpoint> GetServerEndpoints(Type serviceContract)
        {
            if (serviceContract == null)
                throw new ArgumentNullException("serviceContract");

            if (!_cachedServerEndpoints.ContainsKey(serviceContract))
            {
                lock (_cachedServerEndpoints)
                {
                    if (!_cachedServerEndpoints.ContainsKey(serviceContract))
                    {
                        var endpoints = _singleton._provider.GetServerEndpoints(serviceContract);
                        if (endpoints != null)
                            _cachedServerEndpoints.Add(serviceContract, endpoints);
                    }
                }
            }

            return _cachedServerEndpoints[serviceContract];
        }

        public static IList<Endpoint> GetClientEndpoints(Type serviceContract)
        {
            if (serviceContract == null)
                throw new ArgumentNullException("serviceContract");

            if (!_cachedClientEndpoints.ContainsKey(serviceContract))
            {
                lock (_cachedClientEndpoints)
                {
                    if (!_cachedClientEndpoints.ContainsKey(serviceContract))
                    {
                        var endpoints = _singleton._provider.GetClientEndpoints(serviceContract);
                        if (endpoints != null)
                            _cachedClientEndpoints.Add(serviceContract, endpoints);
                    }
                }
            }

            return _cachedClientEndpoints[serviceContract];
        }
    }
}
