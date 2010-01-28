using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DistributedEnterpriseIntegration.Framework;
using NIntegrate.Web;
using System.Data;
using DistributedEnterpriseIntegration.ConfigurationWeb.QueryObjects;

namespace DistributedEnterpriseIntegration.ConfigurationWeb
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dsFarms.Endpoint = WcfConfigurationServiceProxy.GetWcfClientEndpoint(typeof(IQueryService));
            }
        }

        protected DataView GetServers(int farm_id)
        {
            var server = new Server();
            var criteria = server.Select().And(server.Farm_id == farm_id);
            using (var channel = DistributedFramework.Instance.CreateWcfChannel<IQueryService>())
            {
                return new DataView(channel.Channel.Query(criteria));
            }
        }

        protected DataView GetServices(int farm_id)
        {
            var service = new Service();
            var criteria = service.Select().And(service.Farm_id == farm_id);
            using (var channel = DistributedFramework.Instance.CreateWcfChannel<IQueryService>())
            {
                return new DataView(channel.Channel.Query(criteria));
            }
        }

        protected DataView GetVisibility(int farm_id)
        {
            var farmAccess = new FarmAccess();
            var criteria = farmAccess.Select().And(farmAccess.ToFarm_id == farm_id);
            using (var channel = DistributedFramework.Instance.CreateWcfChannel<IQueryService>())
            {
                return new DataView(channel.Channel.Query(criteria));
            }
        }
    }
}
