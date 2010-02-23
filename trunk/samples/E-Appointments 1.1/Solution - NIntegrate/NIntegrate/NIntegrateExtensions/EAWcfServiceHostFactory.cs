using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NIntegrate.ServiceModel.Activation;
using NIntegrate.ServiceModel.Configuration;

namespace NIntegrateExtensions
{
    public class EAWcfServiceHostFactory : WcfServiceHostFactory
    {
        public override WcfService LoadServiceConfiguration(Type serviceType)
        {
            return WcfConfigurationLoader.LoadWcfServiceConfiguration(serviceType);
        }
    }
}
