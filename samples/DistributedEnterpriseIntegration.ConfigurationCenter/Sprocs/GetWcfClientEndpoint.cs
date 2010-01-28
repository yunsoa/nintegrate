using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NIntegrate.Data;

namespace DistributedEnterpriseIntegration.ConfigurationCenter.Sprocs
{
    public class GetWcfClientEndpoint : QuerySproc
    {
        public GetWcfClientEndpoint()
            : base("sp_GetWcfClientEndpoint", "ConfigurationDB")
        {
        }

        public StringParameterExpression ServiceContractType = new StringParameterExpression("serviceContractType", SprocParameterDirection.Input, false);

        public StringParameterExpression ServerName = new StringParameterExpression("serverName", SprocParameterDirection.Input, false);
    }
}
