using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using NIntegrate.Configuration.UI.Code;
using NIntegrate.Configuration.UI.Code.Criterias;

namespace NIntegrate.Configuration.UI
{
    public partial class ManageEndpoints : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var endpointCriteria = new EndpointCriteria();
                dsEndpoints.Criteria = endpointCriteria.AddSortBy(endpointCriteria.Endpoint_id, false);
                var bindingCriteria = new BindingCriteria();
                dsBindings.Criteria = bindingCriteria.AddSortBy(bindingCriteria.Binding_id, false);
                var behaviorCriteria = new BehaviorCriteria();
                dsEndpointBehaviors.Criteria = behaviorCriteria.AddSortBy(behaviorCriteria.Behavior_id, false).And(behaviorCriteria.BehaviorCategory_id == (int)BehaviorCategory.Endpoint);
            }
        }

        protected void btnShowAddNewPanel_Click(object sender, EventArgs e)
        {
            gvEndpoints.SelectedIndex = -1;
            panelBottom.Controls.Hide();
            dvAddEndpoint.Visible = true;
        }

        protected void gvEndpoints_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            gvEndpoints.SelectedIndex = -1;
            panelBottom.Controls.Hide();
        }

        protected void dvAddEndpoint_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            gvEndpoints.DataBind();
        }

        protected void dvAddEndpoint_ItemCommand(object sender, DetailsViewCommandEventArgs e)
        {
            if (e.CommandName == "Cancel")
            {
                gvEndpoints.SelectedIndex = -1;
                panelBottom.Controls.Hide();
            }
        }
    }
}
