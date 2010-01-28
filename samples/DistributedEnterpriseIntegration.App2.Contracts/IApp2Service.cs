using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace DistributedEnterpriseIntegration.App2.Contracts
{
    [ServiceContract]
    public interface IApp2Service
    {
        [OperationContract]
        string SayHello();
    }
}
