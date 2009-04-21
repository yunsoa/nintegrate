using System;
using System.Collections;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;
using NIntegrate.Configuration;

namespace NIntegrate.Web
{
    public sealed class WcfVirtualPathProvider : VirtualPathProvider
    {
        public override bool FileExists(string virtualPath)
        {
            if (string.IsNullOrEmpty(virtualPath))
                throw new ArgumentNullException("virtualPath");

            virtualPath = VirtualPathUtility.ToAppRelative(virtualPath);
            if (VirtualPathUtility.IsAppRelative(virtualPath) && virtualPath.Contains(Constants.DefaultServiceExtension))
            {
                var absPath = VirtualPathUtility.ToAbsolute(virtualPath);
                if (!string.IsNullOrEmpty(ServiceDeploymentConfigurationStore.GetServiceToDeployByPath(absPath)))
                {
                    return true;
                }
            }

            return Previous.FileExists(virtualPath);
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            if (string.IsNullOrEmpty(virtualPath))
                throw new ArgumentNullException("virtualPath");

            virtualPath = VirtualPathUtility.ToAppRelative(virtualPath);
            if (VirtualPathUtility.IsAppRelative(virtualPath) && virtualPath.Contains(Constants.DefaultServiceExtension))
            {
                var absPath = VirtualPathUtility.ToAbsolute(virtualPath);
                var serviceName = ServiceDeploymentConfigurationStore.GetServiceToDeployByPath(absPath);
                if (!string.IsNullOrEmpty(serviceName))
                {
                    return new WcfVirtualFile(virtualPath, serviceName, typeof(WcfServiceHostFactory).AssemblyQualifiedName);
                }
            }

            return Previous.GetFile(virtualPath);
        }

        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            return null;
        }
    }
}
