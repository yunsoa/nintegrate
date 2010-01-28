using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NIntegrate.Data;

namespace DistributedEnterpriseIntegration.ConfigurationCenter.Sprocs
{
    public class GetWcfService : QuerySproc
    {
        public GetWcfService()
            : base("sp_GetWcfService", "ConfigurationDB")
        {
        }

        public StringParameterExpression ServiceType = new StringParameterExpression("serviceType", SprocParameterDirection.Input, false);

        public StringParameterExpression ServerName = new StringParameterExpression("serverName", SprocParameterDirection.Input, false);

        public StringParameterExpression LoadBalancePath = new StringParameterExpression("loadBalancePath", SprocParameterDirection.Input, false);
    }
}
