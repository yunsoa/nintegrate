using System.Web;
using System.Web.Hosting;

namespace NIntegrate.Web
{
    public class WcfVirtualPathProviderRegistrationModule : IHttpModule
    {
        private static bool _initialized;

        #region IHttpModule Members

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

        public void Dispose()
        {
        }

        #endregion
    }
}

