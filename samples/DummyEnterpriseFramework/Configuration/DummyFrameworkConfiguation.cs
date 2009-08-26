using System;
using System.Configuration;
using NIntegrate.ServiceModel.Configuration;

namespace DummyEnterpriseFramework.Configuration
{
    public class DummyFrameworkConfiguation : IDummyFrameworkConfiguation
    {
        public virtual string GetAppVariable(string key)
        {
            //get app variable from somewhere, e.g. DB
            return ConfigurationManager.AppSettings[key];
        }

        public virtual Type GetImplementationType(Type type)
        {
            //integrate with some ioc container
            if (type == null)
                throw new ArgumentNullException("type");

            var implTypeStr = GetAppVariable(type.FullName);
            if (!string.IsNullOrEmpty(implTypeStr))
            {
                var implType = Type.GetType(implTypeStr);
                if (implType != null)
                    return implType;
            }

            return type;
        }

        public virtual ConnectionStringSettings GetConnectionString(string key)
        {
            //get connection string from somewhere, e.g. DB
            return ConfigurationManager.ConnectionStrings[key];
        }

        public virtual WcfClientEndpoint GetWcfClientEndpointConfiguration(Type serviceContractType)
        {
            //get wcf client endpoint configuratin from somewhere, e.g. DB
            //WcfClientEndpoint is a wcf datacontract, so you could persist it anywhere
            //here, it is load from application configuration file
            return AppConfigLoader.Default.LoadClientEndpoint(serviceContractType);
        }
    }
}
