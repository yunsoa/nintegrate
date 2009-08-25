using System;
using System.Runtime.InteropServices;
using EnterpriseAspNetAppServiceContracts;
using NIntegrate;

namespace EnterpriseServiceComWrapper
{
    [ComVisible(true)]
    [Guid("7ED91593-37CD-42e5-A174-2C2A44892341")]
    public class BackIncompatibleService : IDisposable
    {
        #region Private Fields

        private readonly WcfServiceLocator _locator = new WcfServiceLocator();

        #endregion

        #region Public Methods

        public BackIncompatibleResultV2 GetIncompatibleResult()
        {
            return _locator.GetService<IBackIncompatibleServiceV2>().GetIncompatibleResultV2();
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

        ~BackIncompatibleService()
        {
            Dispose(false);
        }

        #endregion
    }
}
