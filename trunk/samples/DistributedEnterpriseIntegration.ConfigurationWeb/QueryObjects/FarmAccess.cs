using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NIntegrate.Data;

namespace DistributedEnterpriseIntegration.ConfigurationWeb.QueryObjects
{
    public class FarmAccess : QueryTable
    {
        public FarmAccess()
            : base("vFarmAccess", "ConfigurationDB", true)
        {
        }

        public Int32Column ToFarm_id = new Int32Column("ToFarm_id");
        public StringColumn FarmAddress = new StringColumn("FarmAddress", false);
        public StringColumn LoadBalancePath = new StringColumn("LoadBalancePath", false);
        public StringColumn BindingTypeCode = new StringColumn("BindingTypeCode", false);
    }
}
