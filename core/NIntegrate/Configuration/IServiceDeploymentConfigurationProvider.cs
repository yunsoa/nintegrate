using System.Collections.Generic;
using System.ServiceModel;

namespace NIntegrate.Configuration
{
    [ServiceContract]
    public interface IServiceDeploymentConfigurationProvider
    {
        [OperationContract]
        IList<ServiceDeploymentConfiguration> GetServiceDeploymentConfiguration(string appCode);
    }
}
