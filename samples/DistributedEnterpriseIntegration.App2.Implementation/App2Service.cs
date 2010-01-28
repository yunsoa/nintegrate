using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DistributedEnterpriseIntegration.App2.Contracts;
using DistributedEnterpriseIntegration.Framework;
using DistributedEnterpriseIntegration.App1.Contracts;

namespace DistributedEnterpriseIntegration.App2.Implementation
{
    public class App2Service : IApp2Service
    {
        #region IApp2Service Members

        public string SayHello()
        {
            using (var app1ReadonlyServiceChannel = DistributedFramework.Instance.CreateWcfChannel<IApp1ReadonlyService>())
            {
                var sayHelloOfApp1ReadonlyService = app1ReadonlyServiceChannel.Channel.SayHello();
                return GetType().Name + ", internally calling " + sayHelloOfApp1ReadonlyService;
            }
        }

        #endregion
    }
}
