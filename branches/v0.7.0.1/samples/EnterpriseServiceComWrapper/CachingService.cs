using System;
using System.Runtime.InteropServices;
using NIntegrate;
using EnterpriseSharedServiceContracts;

namespace EnterpriseServiceComWrapper
{
    [ComVisible(true)]
    [Guid("3D6750EF-0374-48c1-8D91-755C202C410D")]
    public class CachingService : IDisposable
    {
        #region Private Fields

        private readonly WcfServiceLocator _locator = new WcfServiceLocator();

        #endregion

        #region Public Methods

        public byte[] GetCache(string key)
        {
            return _locator.GetService<ICachingService>().GetCache(key);
        }

        public void SetCache(string key, byte[] data, int expireTimeSeconds)
        {
            _locator.GetService<ICachingService>().SetCache(key, data, new TimeSpan(0, 0, 0, expireTimeSeconds));
        }

        #endregion

        #region IDisposable Members

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
                _locator.Dispose();
            }

            disposed = true;
        }

        ~CachingService()
        {
            Dispose(false);
        }

        #endregion
    }
}
