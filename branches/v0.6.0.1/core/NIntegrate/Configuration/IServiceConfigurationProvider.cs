using System.Collections.Generic;
using System;
using System.ServiceModel;

namespace NIntegrate.Configuration
{
    [ServiceContract]
    public interface IServiceConfigurationProvider
    {
        [OperationContract]
        IList<BindingType> GetBindingTypes();
        [OperationContract]
        IList<CustomBehaviorType> GetCustomBehaviorTypes();
        [OperationContract]
        IList<ServiceHostType> GetServiceHostTypes();
        [OperationContract]
        ServiceConfiguration GetServiceConfiguration(string serviceName, string appCode);
        [OperationContract]
        ClientConfiguration GetClientConfiguration(Type serviceContract);
    }
}
