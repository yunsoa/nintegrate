using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NIntegrate.ServiceModel;
using NIntegrate.Test.Query.TestClasses;
using NIntegrate.Data;
using NIntegrate.ServiceModel.Configuration;

namespace NIntegrate.WebTest
{
    public partial class TestActiveRecord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var fac = WcfChannelFactoryFactory.CreateChannelFactory<IActiveRecordConnection<Farm>>(new WcfClientEndpoint
                {
                    Address = "http://localhost:2166/FarmConnectionService.svc",
                    BindingXml = new BindingXml("basichttpbinding", null),
                    ServiceContractType = typeof(IActiveRecordConnection<Farm>).FullName
                });

            using (var svc = new WcfChannelWrapper<IActiveRecordConnection<Farm>>(fac.CreateChannel()))
            {
                Farm farm = new Farm();
                farm.Attach(svc.Channel);

                farm.FarmID = 999;
                farm.FarmAddress = "999";
                farm.LoadBalancePath = "999";
                farm.Delete();
                farm.Save();
                var loadFarm = farm.FindOne(farm.GetObjectId());
                loadFarm.Delete();
            }
        }
    }
}
