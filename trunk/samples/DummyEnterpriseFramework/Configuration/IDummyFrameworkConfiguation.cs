using System;
using System.Configuration;
using NIntegrate.ServiceModel.Configuration;

namespace DummyEnterpriseFramework.Configuration
{
    public interface IDummyFrameworkConfiguation
    {
        string GetAppVariable(string key);
        Type GetImplementationType(Type type);
        ConnectionStringSettings GetConnectionString(string key);
        WcfClientEndpoint GetWcfClientEndpointConfiguration(Type serviceContractType);
    }
}
