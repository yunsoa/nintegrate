using System;
using System.Configuration;
using System.ServiceModel;
using DummyEnterpriseFramework.Configuration;
using NIntegrate.ServiceModel;
using NIntegrate.ServiceModel.Configuration;

namespace DummyEnterpriseFramework
{
    public class DummyFramework
    {
        private readonly IDummyFrameworkConfiguation _config;

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

        public ChannelFactory<T> GetWcfChannelFactory<T>()
        {
            var endpoint = _config.GetWcfClientEndpointConfiguration(typeof(T));
            return WcfChannelFactoryFactory.CreateChannelFactory<T>(endpoint);
        }
    }
}
