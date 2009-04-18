using System;
using SimpleServiceContracts;
using NIntegrate;

namespace SimpleServiceConsumer
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var serviceCriteria = new ServiceCriteria();
                dsService.Criteria = serviceCriteria.AddSortBy(serviceCriteria.ServiceName, false);

                var bindingCriteria = new BindingCriteria();
                dsBinding.Criteria = bindingCriteria.AddSortBy(bindingCriteria.BindingName, false);

                var bindingTypeCriteria = new BindingTypeCriteria();
                dsBindingType.Criteria = bindingTypeCriteria.AddSortBy(bindingTypeCriteria.BindingTypeFriendlyName, false);

                var endpointCriteria = new EndpointCriteria();
                dsEndpoint.Criteria = endpointCriteria;

                using (var serviceLocator = ServiceManager.GetServiceLocator(typeof(ISimpleServiceDemo)))
                {
                    litSayHello.Text = serviceLocator.GetService<ISimpleServiceDemo>().SayHellod();
                }
            }
        }
    }
}
