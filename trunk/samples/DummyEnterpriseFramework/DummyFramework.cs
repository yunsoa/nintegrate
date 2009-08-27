using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using DummyEnterpriseFramework.Configuration;
using NIntegrate.ServiceModel;

namespace DummyEnterpriseFramework
{
    public class DummyFramework
    {
        private readonly IDummyFrameworkConfiguation _config;
        private static readonly Dictionary<Type, ChannelFactory> _cfCache = new Dictionary<Type, ChannelFactory>();
        private static readonly object _cfCacheLock = new object();


        public DummyFramework(IDummyFrameworkConfiguation config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            _config = config;
        }

        public string GetAppVariable(string key)
        {
            return _config.GetAppVariable(key);
        }

        public T CreateInstance<T>()
        {
            var type = _config.GetImplementationType(typeof (T));
            return (T)Activator.CreateInstance(type);
        }

        public ConnectionStringSettings GetConnectionString(string key)
        {
            return _config.GetConnectionString(key);
        }

        public ChannelFactory<T> CreateWcfChannelFactory<T>()
        {
            ChannelFactory cf;

            if (!_cfCache.TryGetValue(typeof(T), out cf))
            {
                lock (_cfCacheLock)
                {
                    if (!_cfCache.TryGetValue(typeof(T), out cf))
                    {
                        var endpoint = _config.GetWcfClientEndpointConfiguration(typeof (T));
                        cf = WcfChannelFactoryFactory.CreateChannelFactory<T>(endpoint);
                        _cfCache[typeof (T)] = cf;
                    }
                }
            }

            return cf as ChannelFactory<T>;
        }
    }
}
