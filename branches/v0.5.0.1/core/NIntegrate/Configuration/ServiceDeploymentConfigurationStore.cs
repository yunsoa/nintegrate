using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel.Configuration;

namespace NIntegrate.Configuration
{
    public sealed class ServiceDeploymentConfigurationStore
    {
        #region Private Singleton

        private static readonly ServiceDeploymentConfigurationStore _singleton 
            = new ServiceDeploymentConfigurationStore();

        #endregion

        #region Private Constructor

        private readonly IServiceDeploymentConfigurationProvider _provider;

        private ServiceDeploymentConfigurationStore()
        {
            var providerTypeName = ConfigurationManager.AppSettings[Constants.ServiceDeploymentConfigurationProviderTypeAppSettingName];
            if (!string.IsNullOrEmpty(providerTypeName))
            {
                var providerType = Type.GetType(providerTypeName);
                if (providerType != null)
                    _provider = (IServiceDeploymentConfigurationProvider)Activator.CreateInstance(providerType);
            }

            if (_provider == null)
                _provider = new DefaultServiceDeploymentConfigurationProvider();

            _cachedServicesToDeploy = LoadServicesToDeploy();
        }

        private Dictionary<string, string> LoadServicesToDeploy()
        {
            var appCode = ConfigurationManager.AppSettings[Constants.AppCodeAppSettingName];
            if (string.IsNullOrEmpty(appCode))
                throw new ConfigurationErrorsException(string.Format("Could not find the {0} appSetting in application configuration file.", Constants.AppCodeAppSettingName));

            var servicesToDeploy = new Dictionary<string, string>();
            foreach (var item in _provider.GetServiceDeploymentConfiguration(appCode))
            {
                var deployPath = GetServiceDeployPath(item);
                if (!servicesToDeploy.ContainsKey(deployPath))
                    servicesToDeploy.Add(deployPath, item.ServiceName);
            }
            return servicesToDeploy;
        }

        private string GetServiceDeployPath(ServiceDeploymentConfiguration item)
        {
            string address = null;
            if (!string.IsNullOrEmpty(item.ListenUri))
            {
                if (item.ListenUri.Contains("://")) //is absolute url
                {
                    address = item.ListenUri;
                }
                else // is relative url to baseAddress
                {
                    if (!string.IsNullOrEmpty(item.HostXML))
                    {
                        var baseAddresses = GetBaseAddresses(item);
                        if (baseAddresses.Length > 0)
                        {
                            address = baseAddresses[0].TrimEnd('/') + "/" + item.ListenUri;
                        }
                    }
                }
            }
            else //use first baseAddress directly
            {
                var baseAddresses = GetBaseAddresses(item);
                if (baseAddresses.Length > 0)
                {
                    address = baseAddresses[0];
                }
            }

            var deployPath = GetDeployPathFromAddress(address);
            return deployPath;
        }

        private static string GetDeployPathFromAddress(string address)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentNullException("address");

            address = address.ToLowerInvariant();
            if (!address.EndsWith(Constants.DefaultServiceExtension) && address.Contains(Constants.DefaultServiceExtension))
                address = address.Substring(0, address.IndexOf(Constants.DefaultServiceExtension) + Constants.DefaultServiceExtension.Length);
            return new Uri(string.Format(address, Environment.MachineName)).AbsolutePath;
        }

        private static string[] GetBaseAddresses(ServiceDeploymentConfiguration item)
        {
            var host = new HostElement();
            host.DeserializeElement(item.HostXML);
            return WcfServiceHelper.GetBaseAddressesFromHostElement(host);
        }

        #endregion

        #region Private Service Deployment Configuration Cache

        private static Dictionary<string, string> _cachedServicesToDeploy;

        #endregion

        public static string GetServiceToDeployByPath(string deployPath)
        {
            if (string.IsNullOrEmpty(deployPath))
                throw new ArgumentNullException("deployPath");
            var appCode = ConfigurationManager.AppSettings[Constants.AppCodeAppSettingName];
            if (string.IsNullOrEmpty(appCode))
                throw new ConfigurationErrorsException(string.Format("Could not find the {0} appSetting in application configuration file.", Constants.AppCodeAppSettingName));

            string serviceName;
            _cachedServicesToDeploy.TryGetValue(deployPath.ToLowerInvariant(), out serviceName);
            return serviceName;
        }

        public static void ResetCache()
        {
            lock (_cachedServicesToDeploy)
            {
                if (_cachedServicesToDeploy != null)
                    _cachedServicesToDeploy.Clear();

                _cachedServicesToDeploy = _singleton.LoadServicesToDeploy();
            }
        }
    }
}
