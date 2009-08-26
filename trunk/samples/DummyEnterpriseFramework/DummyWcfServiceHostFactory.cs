using System;
using NIntegrate.ServiceModel.Activation;
using NIntegrate.ServiceModel.Configuration;

namespace DummyEnterpriseFramework
{
    public class DummyWcfServiceHostFactory : WcfServiceHostFactory
    {
        public override WcfService LoadServiceConfiguration(Type serviceType)
        {
            //get wcf service configuratin from somewhere, e.g. DB
            //WcfService is a wcf datacontract, so you could persist it anywhere
            //here, it is load from application configuration file
            return AppConfigLoader.Default.LoadService(serviceType);
        }
    }
}
