using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NIntegrate.Data;

namespace DistributedEnterpriseIntegration.ConfigurationWeb.QueryObjects
{
    public class Server : QueryTable
    {
        public Server()
            : base("Server", "ConfigurationDB", true)
        {
        }

        public StringColumn ServerName = new StringColumn("ServerName", false);
        public Int32Column Farm_id = new Int32Column("Farm_id");
    }
}
