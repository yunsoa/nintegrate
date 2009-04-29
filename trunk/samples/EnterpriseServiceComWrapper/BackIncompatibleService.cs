using System;
using System.Runtime.InteropServices;
using EnterpriseAspNetAppServiceContractsV1;
using EnterpriseAspNetAppServiceContractsV2;
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

        public BackIncompatibleResult GetIncompatibleResult()
        {
            return _locator.GetService<IBackIncompatibleService>().GetIncompatibleResult();
        }

        public BackIncompatibleResultV2 GetIncompatibleResultV2()
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
