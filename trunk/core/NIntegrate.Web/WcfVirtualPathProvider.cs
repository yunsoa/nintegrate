using System;
using System.Collections;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;
using NIntegrate.Configuration;

namespace NIntegrate.Web
{
    /// <summary>
    /// The VirtualPathProvider implementation for WCF services.
    /// </summary>
    public sealed class WcfVirtualPathProvider : VirtualPathProvider
    {
        /// <summary>
        /// Gets a value that indicates whether a file exists in the virtual file system.
        /// </summary>
        /// <param name="virtualPath">The path to the virtual file.</param>
        /// <returns>
        /// true if the file exists in the virtual file system; otherwise, false.
        /// </returns>
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

        /// <summary>
        /// Gets a virtual file from the virtual file system.
        /// </summary>
        /// <param name="virtualPath">The path to the virtual file.</param>
        /// <returns>
        /// A descendent of the <see cref="T:System.Web.Hosting.VirtualFile"/> class that represents a file in the virtual file system.
        /// </returns>
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

        /// <summary>
        /// Creates a cache dependency based on the specified virtual paths.
        /// </summary>
        /// <param name="virtualPath">The path to the primary virtual resource.</param>
        /// <param name="virtualPathDependencies">An array of paths to other resources required by the primary virtual resource.</param>
        /// <param name="utcStart">The UTC time at which the virtual resources were read.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Caching.CacheDependency"/> object for the specified virtual resources.
        /// </returns>
        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            return null;
        }
    }
}
