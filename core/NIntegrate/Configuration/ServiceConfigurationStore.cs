﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace NIntegrate.Configuration
{
    public sealed class ServiceConfigurationStore
    {
        #region Private Singleton

        private static readonly ServiceConfigurationStore _singleton = new ServiceConfigurationStore();

        #endregion

        #region Private Constructor

        private readonly IServiceConfigurationProvider _provider;

        private ServiceConfigurationStore()
        {
            var providerTypeName = ConfigurationManager.AppSettings[Constants.ServiceConfigurationProviderTypeAppSettingName];
            if (!string.IsNullOrEmpty(providerTypeName))
            {
                var providerType = Type.GetType(providerTypeName);
                if (providerType != null)
                    _provider = (IServiceConfigurationProvider)Activator.CreateInstance(providerType);
            }

            if (_provider == null)
                _provider = new DefaultServiceConfigurationProvider();

            _cachedBindingTypes = _provider.GetBindingTypes();
            _cachedCustomBehaviorTypes = _provider.GetCustomBehaviorTypes();
            _cachedServiceHostTypes = _provider.GetServiceHostTypes();
        }

        #endregion

        #region Private Service Configuration Cache

        private static IList<BindingType> _cachedBindingTypes;
        private static IList<CustomBehaviorType> _cachedCustomBehaviorTypes;
        private static IList<ServiceHostType> _cachedServiceHostTypes;
        private static readonly Dictionary<string, ServiceConfiguration> _cachedServiceConfigurations
            = new Dictionary<string, ServiceConfiguration>();
        private static readonly Dictionary<Type, ClientConfiguration> _cachedClientConfigurations
            = new Dictionary<Type, ClientConfiguration>();

        #endregion

        #region Private Methods

        private static T GetTypeLookupById<T>(IEnumerable en, int id)
            where T : TypeLookup
        {
            if (en == null)
                throw new ArgumentNullException("en");

            foreach (TypeLookup item in en)
            {
                if (item.Type_id == id)
                    return (T)item;
            }

            throw new ConfigurationErrorsException(
                string.Format("Could not find specified {0} whose id = {1}", typeof(T).Name, id));
        }

        #endregion

        public static BindingType GetBindingType(int bindingType_id)
        {
            return GetTypeLookupById<BindingType>(
                _cachedBindingTypes, bindingType_id);
        }

        public static CustomBehaviorType GetCustomBehaviorType(string customBehaviorExtensionName)
        {
            foreach (var item in _cachedCustomBehaviorTypes)
            {
                if (item.ExtensionName == customBehaviorExtensionName)
                    return item;
            }

            return null;
        }

        public static ServiceHostType GetServiceHostType(int serviceHostType_id)
        {
            return GetTypeLookupById<ServiceHostType>(
                _cachedServiceHostTypes, serviceHostType_id);
        }

        public static ServiceConfiguration GetServiceConfiguration(string serviceName)
        {
            if (string.IsNullOrEmpty(serviceName))
                throw new ArgumentNullException("serviceName");
            var appCode = ConfigurationManager.AppSettings[Constants.AppCodeAppSettingName];
            if (string.IsNullOrEmpty(appCode))
                throw new ConfigurationErrorsException(string.Format("Could not find the {0} appSetting in application configuration file.", Constants.AppCodeAppSettingName));

            if (!_cachedServiceConfigurations.ContainsKey(serviceName))
            {
                lock (_cachedServiceConfigurations)
                {
                    if (!_cachedServiceConfigurations.ContainsKey(serviceName))
                    {
                        var config = _singleton._provider.GetServiceConfiguration(serviceName, appCode);
                        if (config == null)
                            throw new ConfigurationErrorsException(string.Format("Specified service name - {0}, appCode - {1} could not be found in configuration store!",
                                serviceName, appCode));
                        _cachedServiceConfigurations.Add(serviceName, config);
                    }
                }
            }

            return _cachedServiceConfigurations[serviceName];
        }

        public static ClientConfiguration GetClientConfiguration(Type serviceContract)
        {
            if (serviceContract == null)
                throw new ArgumentNullException("serviceContract");
            var appCode = ConfigurationManager.AppSettings[Constants.AppCodeAppSettingName];
            if (string.IsNullOrEmpty(appCode))
                throw new ConfigurationErrorsException(string.Format("Could not find the {0} appSetting in application configuration file.", Constants.AppCodeAppSettingName));

            if (!_cachedClientConfigurations.ContainsKey(serviceContract))
            {
                lock (_cachedClientConfigurations)
                {
                    if (!_cachedClientConfigurations.ContainsKey(serviceContract))
                    {
                        var config = _singleton._provider.GetClientConfiguration(serviceContract, appCode);
                        if (config == null)
                            throw new ConfigurationErrorsException(string.Format("Specified Service Contract - {0},appCode - {1} could not be found in configuration store!",
                                serviceContract, appCode));
                        _cachedClientConfigurations.Add(serviceContract, config);
                    }
                }
            }

            return _cachedClientConfigurations[serviceContract];
        }
    }
}