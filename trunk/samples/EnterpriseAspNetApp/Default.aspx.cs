using System;
using System.Web.UI;
using EnterpriseAspNetAppQueryCriterias;
using NIntegrate;
using EnterpriseSharedServiceContracts;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Data;

namespace EnterpriseAspNetApp
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var criteria = new ServiceCriteria();
                QueryDataSource1.Criteria = criteria.AddSortBy(criteria.ServiceName, false);
            }
        }

        protected void QueryDataSource1_Selecting(object sender, NIntegrate.Web.EventArgs.DataSourceSelectingEventArgs e)
        {
            using (var loggingLocator = ServiceManager.GetServiceLocator(typeof(ILoggingService)))
            {
                var logging = loggingLocator.GetService<ILoggingService>();
                logging.WriteLog("Selecting services");
                logging.WriteLog("Try getting services from cache");
                using (var cachingLocator = ServiceManager.GetServiceLocator(typeof(ICachingService)))
                {
                    var caching = cachingLocator.GetService<ICachingService>();
                    var cachedResult = caching.GetCache("Services");
                    if (cachedResult!= null)
                    {
                        logging.WriteLog("Got services from cache");
                        using (var ms = new MemoryStream(cachedResult))
                        {
                            var formatter = new BinaryFormatter();
                            e.Result = (DataTable) formatter.Deserialize(ms);
                            e.Cancel = true;
                            logging.WriteLog("Cancelled database selecting");
                            return;
                        }
                    }
                }
            }
        }

        protected void QueryDataSource1_Selected(object sender, NIntegrate.Web.EventArgs.DataSourceSelectedEventArgs e)
        {
            using (var loggingLocator = ServiceManager.GetServiceLocator(typeof(ILoggingService)))
            {
                var logging = loggingLocator.GetService<ILoggingService>();
                logging.WriteLog("Selected services");
                using (var cachingLocator = ServiceManager.GetServiceLocator(typeof(ICachingService)))
                {
                    var caching = cachingLocator.GetService<ICachingService>();
                    logging.WriteLog("Updating services to cache");
                    using (var ms = new MemoryStream())
                    {
                        var formatter = new BinaryFormatter();
                        formatter.Serialize(ms, e.Result);
                        ms.Seek(0, SeekOrigin.Begin);
                        caching.SetCache("Services", ms.ToArray(), new TimeSpan(0, 0, 1, 0));
                    }
                }
            }
        }
    }
}
