using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel.Activation;
using System.ServiceModel;
using NIntegrateExtensions;
using NIntegrate.ServiceModel.Configuration;

namespace WFExtensions
{
    public class WFServiceHostFactory : EAWcfServiceHostFactory
    {
        public override WcfService LoadServiceConfiguration(Type serviceType)
        {
            var serviceConfig = base.LoadServiceConfiguration(serviceType);
            serviceConfig.CustomServiceHostType = typeof(WFServiceHost).AssemblyQualifiedName;

            return serviceConfig;
        }
    }
}
