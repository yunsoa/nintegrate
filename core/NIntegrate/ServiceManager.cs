﻿using System;
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
                var locator = (IServiceLocator)Activator.CreateInstance(_singleton._externalServiceLocatorType);
                if (locator != null)
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
                        return locator;
                    }
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

        #endregion
    }
}
