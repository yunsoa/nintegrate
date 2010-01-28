using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NIntegrate.Data;

namespace DistributedEnterpriseIntegration.ConfigurationWeb.QueryObjects
{
    public class Service : QueryTable
    {
        public Service()
            : base("Service", "ConfigurationDB", true)
        {
        }

        public StringColumn ServiceName = new StringColumn("ServiceName", false);
        public Int32Column Farm_id = new Int32Column("Farm_id");
  }
}
