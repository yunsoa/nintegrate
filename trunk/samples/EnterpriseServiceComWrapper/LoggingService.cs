using System;
using System.Runtime.InteropServices;
using EnterpriseSharedServiceContracts;
using NIntegrate;

namespace EnterpriseServiceComWrapper
{
    [ComVisible(true)]
    [Guid("E9DBBDDA-094B-4014-B629-2BA8FD015963")]
    public class LoggingService : IDisposable
    {
        #region Private Fields

        private readonly WcfServiceLocator _locator = new WcfServiceLocator();

        #endregion

        #region Public Methods

        public void WriteLog(string message)
        {
            _locator.GetService<ILoggingService>().WriteLog(message);
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

        ~LoggingService()
        {
            Dispose(false);
        }

        #endregion
    }
}
