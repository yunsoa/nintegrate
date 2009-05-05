using System.Collections.Generic;
using System.ServiceModel;

namespace NIntegrate.Configuration
{
    /// <summary>
    /// The interface for ServiceDeploymentConfigurationProviders.
    /// </summary>
    [ServiceContract]
    public interface IServiceDeploymentConfigurationProvider
    {
        /// <summary>
        /// Gets the service deployment configuration.
        /// </summary>
        /// <param name="appCode">The app code.</param>
        /// <returns></returns>
        [OperationContract]
        IList<ServiceDeploymentConfiguration> GetServiceDeploymentConfiguration(string appCode);
    }
}
