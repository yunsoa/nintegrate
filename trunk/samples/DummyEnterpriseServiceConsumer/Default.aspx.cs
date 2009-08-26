using System;
using DummyEnterpriseFramework;
using DummyEnterpriseFramework.Configuration;
using DummyEnterpriseService.Interface;
using NIntegrate.ServiceModel;

namespace DummyEnterpriseServiceConsumer
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var df = new DummyFramework(DummyFrameworkConfiguationManager.GetConfiguration());
            using (var cf = df.GetWcfChannelFactory<IDummyService>())
            {
                Response.Write(cf.CreateChannel().SayHello());
            }
        }
    }
}
