using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NIntegrate.ServiceModel.Configuration;

namespace DistributedEnterpriseIntegration.Framework
{
    /// <summary>
    /// The proxy for loading load WCF configuration from local or ConfigurationCenter/WcfConfigurationService.svc
    /// </summary>
    public static class WcfConfigurationServiceProxy
    {
        private static readonly IWcfConfigurationService _localService = DistributedFramework.Instance.GetLocalService<IWcfConfigurationService>();

        public static WcfService GetWcfService(Type serviceType, string loadBalancePath)
        {
            if (_localService != null)
            {
                return _localService.GetWcfService(GetTypeName(serviceType), Environment.MachineName, loadBalancePath);
            }

            using (var channel = DistributedFramework.Instance.CreateWcfChannel<IWcfConfigurationService>())
            {
                return channel.Channel.GetWcfService(GetTypeName(serviceType), Environment.MachineName, loadBalancePath);
            }

            return null;
        }

        public static WcfClientEndpoint GetWcfClientEndpoint(Type serviceContractType)
        {
            if (_localService != null)
            {
                return _localService.GetWcfClientEndpoint(GetTypeName(serviceContractType), Environment.MachineName);
            }

            using (var channel = DistributedFramework.Instance.CreateWcfChannel<IWcfConfigurationService>())
            {
                return channel.Channel.GetWcfClientEndpoint(GetTypeName(serviceContractType), Environment.MachineName);
            }

            return null;
        }

        private static string GetTypeName(Type type)
        {
            return type.ToString() + ", " + type.Assembly.FullName.Substring(0, type.Assembly.FullName.IndexOf(','));
        }
    }
}
