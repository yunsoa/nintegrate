using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using NIntegrate.ServiceModel.Configuration;

namespace DistributedEnterpriseIntegration.Framework
{
    [ServiceContract]
    public interface IWcfConfigurationService
    {
        [OperationContract]
        WcfService GetWcfService(string serviceType, string serverName, string loadBalancePath);

        [OperationContract]
        WcfClientEndpoint GetWcfClientEndpoint(string serviceContractType, string serverName);
    }
}
