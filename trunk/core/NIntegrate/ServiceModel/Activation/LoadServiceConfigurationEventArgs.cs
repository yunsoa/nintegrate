using System;
using NIntegrate.ServiceModel.Configuration;

namespace NIntegrate.ServiceModel.Activation
{
    /// <summary>
    /// 
    /// </summary>
    public class LoadServiceConfigurationEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the type of the service.
        /// </summary>
        /// <value>The type of the service.</value>
        public Type ServiceType { get; internal set; }
        /// <summary>
        /// Gets or sets the service configuration.
        /// </summary>
        /// <value>The service.</value>
        public WcfService Service { get; set; }
    }
}
