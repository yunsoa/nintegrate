using System;
using System.Data;
using System.Web.UI.WebControls;
using SimpleServiceContracts;
using NIntegrate;
using NIntegrate.Configuration;
using NIntegrate.Query;

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
                    var clientConfig = ServiceConfigurationStore.GetClientConfiguration(typeof(ISimpleServiceDemo));
                    litSayHello.Text += string.Format(" [Binding Type: {0}]", ServiceConfigurationStore.GetBindingType(clientConfig.Endpoint.BindingType_id).FriendlyName);
                }
            }
        }

        protected bool IsEndpointActive(int endpoint_id)
        {
            using (var sericeLocator = ServiceManager.GetServiceLocator(typeof(IQueryService)))
            {
                var queryService = sericeLocator.GetService<IQueryService>();
                var criteria = new EndpointAvailabilityCriteria();
                var result = queryService.Select(criteria.AddResultColumn(criteria.Active).And(criteria.Endpoint_id == endpoint_id).ToSerializableCriteria());
                if (result != null && result.Rows.Count > 0)
                    return Convert.ToBoolean(result.Rows[0][0]);
            }

            return false;
        }

        protected void gvEndpoint_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ChangeActive")
            {
                var splittedArgs = e.CommandArgument.ToString().Split('|');
                var endpoint_id = int.Parse(splittedArgs[0]);
                var isActive = bool.Parse(splittedArgs[1]);

                using (var sericeLocator = ServiceManager.GetServiceLocator(typeof(IQueryService)))
                {
                    var queryService = sericeLocator.GetService<IQueryService>();
                    var criteria = new EndpointAvailabilityCriteria();
                    //when passing criteria to query service, 
                    //ensure to call its ToSerializableCriteria() method 
                    //to ensure it is serializable by WCF
                    var result = queryService.Select(criteria.And(criteria.Endpoint_id == endpoint_id).ToSerializableCriteria());
                    if (result != null && result.Rows.Count > 0)
                    {
                        result.Rows[0]["Active"] = !isActive;
                        //when passing criteria to query service, 
                        //ensure to call its ToSerializableCriteria() method 
                        //to ensure it is serializable by WCF
                        queryService.Update(criteria.ToSerializableCriteria(),
                            result, ConflictOption.OverwriteChanges);
                    }
                }

                gvEndpoint.DataBind();
            }
        }
    }
}
