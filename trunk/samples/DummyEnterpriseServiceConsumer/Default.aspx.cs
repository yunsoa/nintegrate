using System;
using DummyEnterpriseFramework;
using DummyEnterpriseFramework.Configuration;
using DummyEnterpriseService.Interface;

namespace DummyEnterpriseServiceConsumer
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var df = new DummyFramework(DummyFrameworkConfiguationManager.GetConfiguration());
            using (var wrapper = new WcfChannelWrapper<IDummyService>(df.CreateWcfChannelFactory<IDummyService>().CreateChannel()))
            {
                Response.Write(wrapper.Channel.SayHello());
            }
        }
    }
}
