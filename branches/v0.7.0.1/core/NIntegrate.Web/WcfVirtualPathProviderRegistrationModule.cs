using System.Web;
using System.Web.Hosting;

namespace NIntegrate.Web
{
    /// <summary>
    /// The http modual registers the WcfVirtualPathProvider automatically.
    /// </summary>
    public class WcfVirtualPathProviderRegistrationModule : IHttpModule
    {
        private static bool _initialized;

        #region IHttpModule Members

        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpApplication"/> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
        public void Init(HttpApplication context)
        {
            if (!_initialized)
            {
                lock (typeof(WcfVirtualPathProviderRegistrationModule))
                {
                    if (!_initialized)
                    {
                        var provider = new WcfVirtualPathProvider();
                        HostingEnvironment.RegisterVirtualPathProvider(provider);
                        _initialized = true;
                    }
                }
            }
        }

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule"/>.
        /// </summary>
        public void Dispose()
        {
        }

        #endregion
    }
}

