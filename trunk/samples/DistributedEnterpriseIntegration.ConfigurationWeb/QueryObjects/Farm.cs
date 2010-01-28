using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NIntegrate.Data;

namespace DistributedEnterpriseIntegration.ConfigurationWeb.QueryObjects
{
    public class Farm : QueryTable
    {
        public Farm()
            : base("Farm", "ConfigurationDB", true)
        {
        }

        public Int32Column Farm_id = new Int32Column("Farm_id");
        public StringColumn FarmAddress = new StringColumn("FarmAddress", false);
        public StringColumn LoadBalancePath = new StringColumn("LoadBalancePath", false);
    }
}
