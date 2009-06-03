using System;
using System.Runtime.InteropServices;
using NIntegrate;
using NIntegrate.Query;
using System.Data;

namespace EnterpriseServiceComWrapper
{
    [ComVisible(true)]
    [Guid("E5993BA6-6A1E-42ea-A919-FAD5EFA2F97B")]
    public class QueryService : IDisposable
    {
        #region Private Fields

        private readonly WcfServiceLocator _locator = new WcfServiceLocator();

        #endregion

        #region Public Methods

        public DataTable Select(Criteria criteria)
        {
            return _locator.GetService<IQueryService>().Select(criteria.ToSerializableCriteria());
        }

        public int SelectCount(Criteria criteria)
        {
            return _locator.GetService<IQueryService>().SelectCount(criteria.ToSerializableCriteria());
        }

        //for update query, it's better to use stored procedure

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

        ~QueryService()
        {
            Dispose(false);
        }

        #endregion
    }
}
