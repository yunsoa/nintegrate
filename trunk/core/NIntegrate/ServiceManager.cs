using System;
using System.Collections.Generic;
using System.Configuration;

namespace NIntegrate
{
    public sealed class ServiceManager
    {
        #region Private Singleton

        private static readonly ServiceManager _singleton = new ServiceManager();

        #endregion

        #region Private Constructor

        private readonly Type _externalServiceLocatorType;
        private readonly List<Type> _cachedExternalServiceTypes = new List<Type>();
        private readonly List<Type> _cachedWcfServiceTypes = new List<Type>();

        private ServiceManager()
        {
            var serviceLocatorTypeName = ConfigurationManager.AppSettings[Constants.ExternalServiceLocatorTypeAppSettingName];
            if (!string.IsNullOrEmpty(serviceLocatorTypeName))
            {
                var serviceLocatorType = Type.GetType(serviceLocatorTypeName);
                if (serviceLocatorType != null)
                {
                    if (serviceLocatorType != typeof(WcfServiceLocator))
                    {
                        _externalServiceLocatorType = serviceLocatorType;
                    }
                }
            }

            if (_externalServiceLocatorType == null)
                _externalServiceLocatorType = typeof(AppVariableServiceLocator);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get service locator of specified service contract type
        /// </summary>
        /// <param name="serviceContract">The service contract type</param>
        /// <returns>The service instance</returns>
        public static IServiceLocator GetServiceLocator(Type serviceContract)
        {
            if (serviceContract == null)
                throw new ArgumentNullException("serviceContract");

            if (_singleton._externalServiceLocatorType != null)
            {
                if (!_singleton._cachedExternalServiceTypes.Contains(serviceContract))
                {
                    if (_singleton._cachedWcfServiceTypes.Contains(serviceContract))
                    {
                        return new WcfServiceLocator();
                    }
                    lock (_singleton)
                    {
                        if (!_singleton._cachedExternalServiceTypes.Contains(serviceContract))
                        {
                            if (_singleton._cachedWcfServiceTypes.Contains(serviceContract))
                            {
                                return new WcfServiceLocator();
                            }

                            //rethrow the exception in initializing the locator instance directly
                            var locator = (IServiceLocator)Activator.CreateInstance(_singleton._externalServiceLocatorType);

                            object service = null;
                            try
                            {
                                service = locator.GetService(serviceContract);
                                if (service != null)
                                {
                                    _singleton._cachedExternalServiceTypes.Add(serviceContract);
                                    return locator;
                                }
                            }
                            catch
                            {
                                //if could not locate the service
                                _singleton._cachedWcfServiceTypes.Add(serviceContract);
                                return new WcfServiceLocator();
                            }
                            finally
                            {
                                if (service != null)
                                {
                                    var disposable = service as IDisposable;
                                    if (disposable != null)
                                        disposable.Dispose();
                                }
                            }
                        }
                    }
                }
                else
                {
                    var locator = (IServiceLocator)Activator.CreateInstance(_singleton._externalServiceLocatorType);
                    return locator;
                }
            }

            _singleton._cachedWcfServiceTypes.Add(serviceContract);
            return new WcfServiceLocator();
        }

        /// <summary>
        /// Get service locator of specified service contract type
        /// </summary>
        /// <typeparam name="T">The service contract type</typeparam>
        /// <returns></returns>
        public static IServiceLocator GetServiceLocator<T>()
        {
            return GetServiceLocator(typeof(T));
        }

        /// <summary>
        /// To get service implementation type from
        /// the service interface type
        /// </summary>
        /// <param name="serviceInterfaceType">The service interface type</param>
        /// <param name="singleton">If the service is singleton, return the singleton instance</param>
        /// <returns></returns>
        internal static Type GetServiceImplementationType(Type serviceInterfaceType, out object singleton)
        {
            singleton = null;

            if (serviceInterfaceType != null)
            {
                using (var serviceLocator = GetServiceLocator(serviceInterfaceType))
                {
                    if (!(serviceLocator is WcfServiceLocator))
                    {
                        var serviceInstance = serviceLocator.GetService(serviceInterfaceType);
                        if (serviceInstance != null)
                        {
                            if (serviceLocator.IsSingleton(serviceInterfaceType))
                            {
                                singleton = serviceInstance;
                                return serviceInstance.GetType();
                            }

                            try
                            {
                                return serviceInstance.GetType();
                            }
                            finally
                            {
                                var dispose = serviceInstance as IDisposable;
                                if (dispose != null)
                                    dispose.Dispose();
                            }
                        }
                    }
                    serviceLocator.Dispose();
                }
            }

            return serviceInterfaceType;
        }

        #endregion
    }
}
