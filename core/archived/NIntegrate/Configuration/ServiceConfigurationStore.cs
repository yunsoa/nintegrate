using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace NIntegrate.Configuration
{
    /// <summary>
    /// The ServiceConfiguration Store.
    /// </summary>
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

        /// <summary>
        /// Gets the type of the binding.
        /// </summary>
        /// <param name="bindingType_id">The binding type_id.</param>
        /// <returns></returns>
        public static BindingType GetBindingType(int bindingType_id)
        {
            return GetTypeLookupById<BindingType>(
                _cachedBindingTypes, bindingType_id);
        }

        /// <summary>
        /// Gets the type of the custom behavior.
        /// </summary>
        /// <param name="customBehaviorExtensionName">Name of the custom behavior extension.</param>
        /// <returns></returns>
        public static CustomBehaviorType GetCustomBehaviorType(string customBehaviorExtensionName)
        {
            foreach (var item in _cachedCustomBehaviorTypes)
            {
                if (item.ExtensionName == customBehaviorExtensionName)
                    return item;
            }

            return null;
        }

        /// <summary>
        /// Gets the type of the service host.
        /// </summary>
        /// <param name="serviceHostType_id">The service host type_id.</param>
        /// <returns></returns>
        public static ServiceHostType GetServiceHostType(int serviceHostType_id)
        {
            return GetTypeLookupById<ServiceHostType>(
                _cachedServiceHostTypes, serviceHostType_id);
        }

        /// <summary>
        /// Gets the service configuration.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the client configuration.
        /// </summary>
        /// <param name="serviceContract">The service contract.</param>
        /// <returns></returns>
        public static ClientConfiguration GetClientConfiguration(Type serviceContract)
        {
            if (serviceContract == null)
                throw new ArgumentNullException("serviceContract");

            if (!_cachedClientConfigurations.ContainsKey(serviceContract))
            {
                lock (_cachedClientConfigurations)
                {
                    if (!_cachedClientConfigurations.ContainsKey(serviceContract))
                    {
                        var config = _singleton._provider.GetClientConfiguration(serviceContract);
                        if (config == null)
                            throw new ConfigurationErrorsException(string.Format("Specified Service Contract - {0} could not be found in configuration store!",
                                serviceContract));
                        _cachedClientConfigurations.Add(serviceContract, config);
                    }
                }
            }

            return _cachedClientConfigurations[serviceContract];
        }

        /// <summary>
        /// Resets the cache.
        /// </summary>
        public static void ResetCache()
        {
            lock (typeof(ServiceConfigurationStore))
            {
                if (_cachedBindingTypes != null)
                    _cachedBindingTypes.Clear();
                if (_cachedCustomBehaviorTypes != null)
                    _cachedCustomBehaviorTypes.Clear();
                if (_cachedServiceHostTypes != null)
                    _cachedServiceHostTypes.Clear();
                _cachedServiceConfigurations.Clear();
                _cachedClientConfigurations.Clear();

                _cachedBindingTypes = _singleton._provider.GetBindingTypes();
                _cachedCustomBehaviorTypes = _singleton._provider.GetCustomBehaviorTypes();
                _cachedServiceHostTypes = _singleton._provider.GetServiceHostTypes();
            }
        }
    }
}
