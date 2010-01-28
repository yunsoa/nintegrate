using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using NIntegrate.ServiceModel;
using System.ServiceModel;

namespace DistributedEnterpriseIntegration.Framework
{
    public class DistributedFramework
    {
        public static readonly DistributedFramework Instance = new DistributedFramework();

        private const string WCFCONFIGURATIONSERVICE_ENDPOINT_NAME = "WcfConfigurationService";
        private static readonly Dictionary<Type, ChannelFactory> _cfCache = new Dictionary<Type, ChannelFactory>();
        private static readonly object _cfCacheLock = new object();

        private DistributedFramework()
        {
        }

        public T GetLocalService<T>()
            where T : class
        {
            var localServiceType = ConfigurationManager.AppSettings[typeof(T).ToString()];
            if (!string.IsNullOrEmpty(localServiceType))
            {
                var type = Type.GetType(localServiceType);

                if (type != null)
                {
                    try
                    {
                        return (T)Activator.CreateInstance(type);
                    }
                    catch (Exception ex)
                    {
                        //log exception
                        //...

                        throw;
                    }
                }
            }

            return null;
        }

        public WcfChannelWrapper<T> CreateWcfChannel<T>()
            where T : class
        {
            ChannelFactory cf;

            if (!_cfCache.TryGetValue(typeof(T), out cf))
            {
                lock (_cfCacheLock)
                {
                    if (!_cfCache.TryGetValue(typeof(T), out cf))
                    {
                        if (typeof(T) == typeof(IWcfConfigurationService))
                        {
                            cf = new ChannelFactory<IWcfConfigurationService>(WCFCONFIGURATIONSERVICE_ENDPOINT_NAME);
                        }
                        else
                        {
                            var endpoint = WcfConfigurationServiceProxy.GetWcfClientEndpoint(typeof(T));
                            cf = WcfChannelFactoryFactory.CreateChannelFactory<T>(endpoint);
                        }
                        _cfCache[typeof(T)] = cf;
                    }
                }
            }

            return new WcfChannelWrapper<T>((cf as ChannelFactory<T>).CreateChannel());
        }
    }
}
