using System;
using System.Web.UI.WebControls;
using NIntegrate.Configuration.UI.Code.Criterias;

namespace NIntegrate.Configuration.UI
{
    public partial class ManageFarms : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var farmCriteria = new FarmCriteria();
                dsFarms.Criteria = farmCriteria.AddSortBy(farmCriteria.Farm_id, false);
                var envCriteria = new EnvironmentCriteria();
                dsEnvironments.Criteria = envCriteria.AddSortBy(envCriteria.Environment_id, false);
            }
        }

        protected void btnShowAddNewPanel_Click(object sender, EventArgs e)
        {
            gvFarms.SelectedIndex = -1;
            panelBottom.Controls.Hide();
            dvAddFarm.Visible = true;
        }

        protected void gvFarms_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            gvFarms.SelectedIndex = -1;
            gvServers.EditIndex = -1;
            panelBottom.Controls.Hide();
            panelAddServer.Visible = false;

            if (e.CommandName == "Select")
            {
                panelBottom.Style["display"] = "block";

                var parameters = e.CommandArgument.ToString().Split('|');

                if (parameters.Length == 2)
                {
                    panelBottom.Controls.Hide();
                    if (parameters[0] == "ViewServers")
                    {
                        var serverCriteria = new ServerCriteria();
                        dsServers.Criteria = serverCriteria.AddSortBy(serverCriteria.ServerName, false)
                            .And(serverCriteria.Farm_id == int.Parse(parameters[1]));
                        hidSelectedFarm_id.Value = parameters[1];
                        gvServers.DataBind();
                        panelServers.Visible = true;
                    }
                }
            }
        }

        protected void dvAddFarm_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            gvFarms.DataBind();
        }

        protected void dvAddFarm_ItemCommand(object sender, DetailsViewCommandEventArgs e)
        {
            if (e.CommandName == "Cancel")
            {
                gvFarms.SelectedIndex = -1;
                panelBottom.Controls.Hide();
            }
        }

        protected void btnShowSubAddNewPanel_Click(object sender, EventArgs e)
        {
            gvServers.SelectedIndex = -1;
            panelAddServer.Visible = true;
        }

        protected void gvServers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            gvServers.SelectedIndex = -1;
            panelAddServer.Visible = false;
        }

        protected void dvAddServer_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            gvServers.DataBind();
        }

        protected void dvAddServer_ItemCommand(object sender, DetailsViewCommandEventArgs e)
        {
            if (e.CommandName == "Cancel")
            {
                gvServers.SelectedIndex = -1;
                panelAddServer.Visible = false;
            }
        }

        protected void dvAddServer_DataBound(object sender, EventArgs e)
        {
            var ddlFarms = dvAddServer.FindControl("ddlFarms") as DropDownList;
            for (var i = 0; i < ddlFarms.Items.Count; ++i)
            {
                if (ddlFarms.Items[i].Value == hidSelectedFarm_id.Value)
                {
                    ddlFarms.SelectedIndex = i;
                    break;
                }
            }
        }
    }
}
