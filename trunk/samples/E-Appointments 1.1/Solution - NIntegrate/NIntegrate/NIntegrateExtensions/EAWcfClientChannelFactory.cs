using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using NIntegrate.ServiceModel;

namespace NIntegrateExtensions
{
    public static class EAWcfClientChannelFactory
    {
        private static readonly Dictionary<Type, ChannelFactory> _cfCache = new Dictionary<Type, ChannelFactory>();
        private static readonly object _cfCacheLock = new object();

        public static WcfChannelWrapper<T> CreateWcfChannel<T>()
            where T : class 
        {
            ChannelFactory cf;

            if (!_cfCache.TryGetValue(typeof(T), out cf))
            {
                lock (_cfCacheLock)
                {
                    if (!_cfCache.TryGetValue(typeof(T), out cf))
                    {
                        var endpoint = WcfConfigurationLoader.LoadWcfClientEndpointConfiguration(typeof (T));
                        cf = WcfChannelFactoryFactory.CreateChannelFactory<T>(endpoint);
                        _cfCache[typeof (T)] = cf;
                    }
                }
            }

            return new WcfChannelWrapper<T>((cf as ChannelFactory<T>).CreateChannel());
        }
    }
}
