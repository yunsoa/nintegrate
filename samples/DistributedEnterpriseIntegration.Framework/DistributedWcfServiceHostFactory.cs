using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NIntegrate.ServiceModel.Activation;

namespace DistributedEnterpriseIntegration.Framework
{
    public class DistributedWcfServiceHostFactory : WcfServiceHostFactory
    {
        private Uri[] _baseAddresses;

        public override System.ServiceModel.ServiceHostBase CreateServiceHost(string constructorString, Uri[] baseAddresses)
        {
            _baseAddresses = baseAddresses;

            return base.CreateServiceHost(constructorString, baseAddresses);
        }

        public override NIntegrate.ServiceModel.Configuration.WcfService LoadServiceConfiguration(Type serviceType)
        {
            return WcfConfigurationServiceProxy.GetWcfService(serviceType, GetLoadBalancePath(_baseAddresses));
        }

        private string GetLoadBalancePath(Uri[] baseAddresses)
        {
            string path = "/";

            if (baseAddresses != null && baseAddresses.Length > 0)
            {
                Uri uri = baseAddresses[0];
                string[] slashSegments = uri.AbsolutePath.Split('/');
                path = uri.AbsolutePath.Substring(0, uri.AbsolutePath.Length - 1 - slashSegments[slashSegments.Length - 1].Length);
            }

            return path;
        }
    }
}
