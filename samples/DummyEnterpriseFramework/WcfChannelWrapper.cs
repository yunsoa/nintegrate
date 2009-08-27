using System;
using System.ServiceModel;

namespace DummyEnterpriseFramework
{
    public class WcfChannelWrapper<T> : IDisposable
        where T : class 
    {
        private readonly T _channel;

        public WcfChannelWrapper(T channel)
        {
            if (channel == null)
                throw new ArgumentNullException("channel");

            _channel = channel;
        }

        public T Channel { get { return _channel; } }

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
                var commObj = _channel as ICommunicationObject;
                if (commObj != null)
                {
                    //close channel factory in best practice
                    //refer to: http://bloggingabout.net/blogs/erwyn/archive/2006/12/09/WCF-Service-Proxy-Helper.aspx
                    try
                    {
                        commObj.Close();
                    }
                    catch (CommunicationException)
                    {
                        commObj.Abort();
                    }
                    catch (TimeoutException)
                    {
                        commObj.Abort();
                    }
                    catch (Exception)
                    {
                        commObj.Abort();
                        throw;
                    }
                }

            }

            disposed = true;
        }

        ~WcfChannelWrapper()
        {
            Dispose(false);
        }

        #endregion

    }
}
