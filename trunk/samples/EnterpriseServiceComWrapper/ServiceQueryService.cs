using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using EnterpriseAspNetAppQueryCriterias;
using NIntegrate;
using NIntegrate.Query;
using System.Data;

namespace EnterpriseServiceComWrapper
{
    [ComVisible(true)]
    [Guid("E5993BA6-6A1E-42ea-A919-FAD5EFA2F97A")]
    public class ServiceQueryService : IDisposable
    {
        #region Private Fields

        private readonly WcfServiceLocator _locator = new WcfServiceLocator();

        #endregion

        #region Public Methods

        public ServiceCollection Select(ServiceCriteria criteria)
        {
            var table = _locator.GetService<IQueryService>().Select(criteria.ToBaseCriteria());
            var services = new ServiceCollection();
            foreach (DataRow row in table.Rows)
            {
                var service = new Service
                                  {
                                      Service_id = (int)row["Service_id"],
                                      ServiceName = (string)row["ServiceName"],
                                      HostXML = (string)row["HostXML"]
                                  };
                services.Add(service);
            }
            return services;
        }

        public int SelectCount(ServiceCriteria criteria)
        {
            return _locator.GetService<IQueryService>().SelectCount(criteria.ToBaseCriteria());
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

        ~ServiceQueryService()
        {
            Dispose(false);
        }

        #endregion
    }
}
