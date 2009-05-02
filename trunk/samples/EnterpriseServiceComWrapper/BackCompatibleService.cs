using System;
using System.Runtime.InteropServices;
using EnterpriseAspNetAppServiceContracts;
using NIntegrate;

namespace EnterpriseServiceComWrapper
{
    [ComVisible(true)]
    [Guid("27EB4D27-615F-4db0-9D83-25B264FA88BF")]
    public class BackCompatibleService : IDisposable
    {
        #region Private Fields

        private readonly WcfServiceLocator _locator = new WcfServiceLocator();

        #endregion

        #region Public Methods

        public BackCompatibleResultV2 GetCompatibleResult()
        {
            return _locator.GetService<IBackCompatibleService>().GetCompatibleResult();
        }

        public string SayHello()
        {
            return _locator.GetService<IBackCompatibleService>().SayHello();
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

        ~BackCompatibleService()
        {
            Dispose(false);
        }

        #endregion
    }
}
