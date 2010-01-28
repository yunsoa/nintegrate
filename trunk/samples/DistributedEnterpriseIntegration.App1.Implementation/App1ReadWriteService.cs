using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DistributedEnterpriseIntegration.App1.Contracts;

namespace DistributedEnterpriseIntegration.App1.Implementation
{
    public class App1ReadWriteService : IApp1ReadWriteService
    {
        #region IApp1ReadWriteService Members

        public string SayHello()
        {
            return GetType().Name;
        }

        #endregion
    }
}
