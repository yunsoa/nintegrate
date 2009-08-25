using System.Collections.Generic;
using System;
using System.ServiceModel;

namespace NIntegrate.Configuration
{
    /// <summary>
    /// The interface for ServiceConfigurationProviders.
    /// </summary>
    [ServiceContract]
    public interface IServiceConfigurationProvider
    {
        /// <summary>
        /// Gets the binding types.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IList<BindingType> GetBindingTypes();
        /// <summary>
        /// Gets the custom behavior types.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IList<CustomBehaviorType> GetCustomBehaviorTypes();
        /// <summary>
        /// Gets the service host types.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IList<ServiceHostType> GetServiceHostTypes();
        /// <summary>
        /// Gets the service configuration.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="appCode">The app code.</param>
        /// <returns></returns>
        [OperationContract]
        ServiceConfiguration GetServiceConfiguration(string serviceName, string appCode);
        /// <summary>
        /// Gets the client configuration.
        /// </summary>
        /// <param name="serviceContract">The service contract.</param>
        /// <returns></returns>
        [OperationContract]
        ClientConfiguration GetClientConfiguration(Type serviceContract);
    }
}
