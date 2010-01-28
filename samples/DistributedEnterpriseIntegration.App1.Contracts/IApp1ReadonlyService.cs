using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace DistributedEnterpriseIntegration.App1.Contracts
{
    [ServiceContract]
    public interface IApp1ReadonlyService
    {
        [OperationContract]
        string SayHello();
    }
}
