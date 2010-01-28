using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DistributedEnterpriseIntegration.App1.Contracts;

namespace DistributedEnterpriseIntegration.App1.Implementation
{
    public class App1ReadonlyService : IApp1ReadonlyService
    {
        #region IApp1ReadonlyService Members

        public string SayHello()
        {
            return GetType().Name;
        }

        #endregion
    }
}
