using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using NIntegrate.Configuration.UI.Code.Criterias;

namespace NIntegrate.Configuration.UI
{
    public partial class ManageServiceEnpoints : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var serviceEndpointCriteria = new ServiceEndpointCriteria();
                dsServiceEndpoints.Criteria = serviceEndpointCriteria.AddSortBy(serviceEndpointCriteria.Active, true).AddSortBy(serviceEndpointCriteria.Service_id, false);
                var serviceCriteria = new ServiceCriteria();
                dsServices.Criteria = serviceCriteria.AddSortBy(serviceCriteria.Service_id, false);
                var endpointCriteria = new EndpointCriteria();
                dsEndpoints.Criteria = endpointCriteria.AddSortBy(endpointCriteria.Endpoint_id, false);
                var farmCriteria = new FarmCriteria();
                dsFarms.Criteria = farmCriteria.AddSortBy(farmCriteria.Farm_id, false);
            }
        }

        protected void btnShowAddNewPanel_Click(object sender, EventArgs e)
        {
            gvServiceEndpoints.SelectedIndex = -1;
            panelBottom.Controls.Hide();
            dvAddServiceEndpoint.Visible = true;
        }

        protected void gvServiceEndpoints_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            gvServiceEndpoints.SelectedIndex = -1;
            panelBottom.Controls.Hide();
        }

        protected void dvAddServiceEndpoint_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            gvServiceEndpoints.DataBind();
        }

        protected void dvAddServiceEndpoint_ItemCommand(object sender, DetailsViewCommandEventArgs e)
        {
            if (e.CommandName == "Cancel")
            {
                gvServiceEndpoints.SelectedIndex = -1;
                panelBottom.Controls.Hide();
            }
        }
    }
}
