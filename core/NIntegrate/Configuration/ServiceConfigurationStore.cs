using System;
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
        private static readonly Dictionary<Type, ServiceConfiguration> _cachedServiceConfigurations
            = new Dictionary<Type, ServiceConfiguration>();

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

        public static CustomBehaviorType GetCustomBehaviorType(int customBehaviorType_id)
        {
            return GetTypeLookupById<CustomBehaviorType>(
                _cachedCustomBehaviorTypes, customBehaviorType_id);
        }

        public static ServiceHostType GetServiceHostType(int serviceHostType_id)
        {
            return GetTypeLookupById<ServiceHostType>(
                _cachedServiceHostTypes, serviceHostType_id);
        }

        public static ServiceConfiguration GetServiceConfiguration(Type serviceContract)
        {
            if (serviceContract == null)
                throw new ArgumentNullException("serviceContract");

            if (!_cachedServiceConfigurations.ContainsKey(serviceContract))
            {
                lock (_cachedServiceConfigurations)
                {
                    if (!_cachedServiceConfigurations.ContainsKey(serviceContract))
                    {
                        var connString = _singleton._provider.GetServiceConfiguration(serviceContract);
                        if (connString == null)
                            throw new ConfigurationErrorsException(string.Format("Specified Service Contract - {0} could not be found in configuration store!", serviceContract));
                        _cachedServiceConfigurations.Add(serviceContract, connString);
                    }
                }
            }

            return _cachedServiceConfigurations[serviceContract];
        }
    }
}
