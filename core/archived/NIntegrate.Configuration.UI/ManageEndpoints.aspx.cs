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
                var farmCriteria = new FarmCriteria();
                dsFarms.Criteria = farmCriteria.AddSortBy(farmCriteria.Farm_id, false);
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
            gvClientBehaviors.EditIndex = -1;
            panelBottom.Controls.Hide();
            panelAddClientBehavior.Visible = false;

            if (e.CommandName == "Select")
            {
                panelBottom.Style["display"] = "block";

                var parameters = e.CommandArgument.ToString().Split('|');

                if (parameters.Length == 2)
                {
                    panelBottom.Controls.Hide();
                    if (parameters[0] == "ViewClientBehaviors")
                    {
                        var clientBehaviorCriteria = new EndpointClientCriteria();
                        dsClientBehaviors.Criteria = clientBehaviorCriteria.AddSortBy(clientBehaviorCriteria.ClientEndpointBehavior_id, false)
                            .And(clientBehaviorCriteria.Endpoint_id == int.Parse(parameters[1]));
                        hidSelectedEndpoint.Value = parameters[1];
                        gvClientBehaviors.DataBind();
                        panelClientBehaviors.Visible = true;
                    }
                }
            }
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

        protected void btnShowSubAddNewPanel_Click(object sender, EventArgs e)
        {
            gvClientBehaviors.SelectedIndex = -1;
            panelAddClientBehavior.Visible = true;
        }

        protected void gvClientBehaviors_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            gvClientBehaviors.SelectedIndex = -1;
            panelAddClientBehavior.Visible = false;
        }

        protected void dvAddClientBehavior_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            gvEndpoints.DataBind();
        }

        protected void dvAddClientBehavior_ItemCommand(object sender, DetailsViewCommandEventArgs e)
        {
            if (e.CommandName == "Cancel")
            {
                gvClientBehaviors.SelectedIndex = -1;
                panelAddClientBehavior.Visible = false;
            }
        }

        protected void dvAddClientBehavior_DataBound(object sender, EventArgs e)
        {
            var ddlEndpoints = dvAddClientBehavior.FindControl("ddlEndpoints") as DropDownList;
            for (var i = 0; i < ddlEndpoints.Items.Count; ++i)
            {
                if (ddlEndpoints.Items[i].Value == hidSelectedEndpoint.Value)
                {
                    ddlEndpoints.SelectedIndex = i;
                    break;
                }
            }
        }
    }
}
