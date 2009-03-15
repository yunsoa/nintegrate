using System;
using System.Collections.Generic;
using System.ServiceModel;
using NIntegrate.Configuration;

namespace NIntegrate
{
    /// <summary>
    /// The WCF Service Locator
    /// </summary>
    public class WcfServiceLocator : IDisposable
    {
        #region Private Fields

        /// <summary>
        /// The cached channel factories, all channel factories in this list 
        /// will be closed in the Dispose() method
        /// </summary>
        private readonly Dictionary<Type, ChannelFactory> _channelFactories = new Dictionary<Type, ChannelFactory>();
        /// <summary>
        /// The cached service proxy instances created from channel factory
        /// </summary>
        private readonly Dictionary<Type, object> _serviceProxies = new Dictionary<Type, object>();

        #endregion

        #region Private Methods

        /// <summary>
        /// Create channel factory from service config db
        /// </summary>
        /// <typeparam name="T">The service contract</typeparam>
        /// <returns>The built channel factory</returns>
        private static ChannelFactory<T> CreateChannelFactory<T>()
        {
            var endpoints = EndpointStore.GetClientEndpoints(typeof(T));
            if (endpoints.Count > 0)
            {
                //endpoints are ordered by channel type in NetNamedPipe, NetTcp and WSHttp order,
                //so the top one in the list should be the fastest one for current caller
                var endpoint = endpoints[0];
                var binding = WcfServiceHelper.BuildBinding(typeof(T), endpoint);
                var address = WcfServiceHelper.BuildAddress(endpoint);

                if (binding != null && address != null)
                {
                    var cf = new ChannelFactory<T>(binding, new EndpointAddress(address));
                    return cf;
                }
            }

            return null;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get service
        /// </summary>
        /// <param name="serviceContract">The service contract type</param>
        /// <returns>The service proxy instance</returns>
        public object GetService(Type serviceContract)
        {
            if (serviceContract != null)
            {
                var retObj = typeof(WcfServiceLocator).GetMethod("GetService", Type.EmptyTypes)
                    .MakeGenericMethod(serviceContract).Invoke(this, null);
                return retObj;
            }

            return null;
        }

        /// <summary>
        /// Generic version of GetService
        /// </summary>
        /// <typeparam name="T">The service contract type</typeparam>
        /// <returns>The service proxy instance</returns>
        public T GetService<T>()
        {
            if (!_channelFactories.ContainsKey(typeof(T)))
            {
                lock (_channelFactories)
                {
                    if (!_channelFactories.ContainsKey(typeof(T)))
                    {
                        var cf = CreateChannelFactory<T>();
                        if (cf != null)
                        {
                            _channelFactories.Add(typeof(T), cf);
                            try
                            {
                                _serviceProxies.Add(typeof(T), cf.CreateChannel());
                            }
                            catch
                            {
                                _serviceProxies.Add(typeof(T), null);
                            }
                        }
                        else
                        {
                            _channelFactories.Add(typeof(T), null);
                            _serviceProxies.Add(typeof(T), null);
                        }
                    }
                }
            }

            return (T)_serviceProxies[typeof(T)];
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// The dispose method, 
        /// closing all created channel factories in this service locator
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed;

        private void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                foreach (var item in _channelFactories.Values)
                {
                    if (item != null)
                    {
                        //close channel factory in best practice
                        //refer to: http://bloggingabout.net/blogs/erwyn/archive/2006/12/09/WCF-Service-Proxy-Helper.aspx
                        try
                        {
                            item.Close();
                        }
                        catch (CommunicationException)
                        {
                            item.Abort();
                        }
                        catch (TimeoutException)
                        {
                            item.Abort();
                        }
                        catch (Exception)
                        {
                            item.Abort();
                            throw;
                        }
                    }
                }
            }

            disposed = true;
        }

        ~WcfServiceLocator()
        {
            Dispose(false);
        }

        #endregion
    }
}
