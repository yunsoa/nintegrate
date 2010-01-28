using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DistributedEnterpriseIntegration.Framework;
using DistributedEnterpriseIntegration.App1.Contracts;
using DistributedEnterpriseIntegration.App2.Contracts;

namespace DistributedEnterpriseIntegration.Farm3App1Web
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var app1ReadonlyServiceChannel = DistributedFramework.Instance.CreateWcfChannel<IApp1ReadonlyService>())
            {
                Response.Write(app1ReadonlyServiceChannel.Channel.SayHello() + "<br />");
            }

            using (var app1ReadWriteServiceChannel = DistributedFramework.Instance.CreateWcfChannel<IApp1ReadWriteService>())
            {
                Response.Write(app1ReadWriteServiceChannel.Channel.SayHello() + "<br />");
            }

            using (var app2ServiceChannel = DistributedFramework.Instance.CreateWcfChannel<IApp2Service>())
            {
                Response.Write(app2ServiceChannel.Channel.SayHello() + "<br />");
            }
        }
    }
}
