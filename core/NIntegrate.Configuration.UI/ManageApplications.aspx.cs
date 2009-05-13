using System;
using System.Web.UI.WebControls;
using NIntegrate.Configuration.UI.Code.Criterias;

namespace NIntegrate.Configuration.UI
{
    public partial class ManageApplications : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var appCriteria = new AppCriteria();
                dsApps.Criteria = appCriteria.AddSortBy(appCriteria.AppCode, false);
                var envCriteria = new EnvironmentCriteria();
                dsEnvironments.Criteria = envCriteria.AddSortBy(envCriteria.Environment_id, false);
            }
        }

        protected void btnShowAddNewPanel_Click(object sender, EventArgs e)
        {
            gvApps.SelectedIndex = -1;
            panelBottom.Controls.Hide();
            dvAddApp.Visible = true;
        }

        protected void gvApps_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            gvApps.SelectedIndex = -1;
            gvAppVariables.EditIndex = -1;
            panelBottom.Controls.Hide();
            panelAddAppVariable.Visible = false;

            if (e.CommandName == "Select")
            {
                panelBottom.Style["display"] = "block";

                var parameters = e.CommandArgument.ToString().Split('|');

                if (parameters.Length == 2)
                {
                    panelBottom.Controls.Hide();
                    if (parameters[0] == "ViewAppVariables")
                    {
                        var appVariableCriteria = new AppVariableCriteria();
                        dsAppVariables.Criteria = appVariableCriteria.AddSortBy(appVariableCriteria.AppVariableName, false)
                            .And(appVariableCriteria.AppCode == parameters[1]);
                        hidSelectedAppCode.Value = parameters[1];
                        gvAppVariables.DataBind();
                        panelAppVariables.Visible = true;
                    }
                }
            }
        }

        protected void dvAddApp_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            gvApps.DataBind();
        }

        protected void dvAddApp_ItemCommand(object sender, DetailsViewCommandEventArgs e)
        {
            if (e.CommandName == "Cancel")
            {
                gvApps.SelectedIndex = -1;
                panelBottom.Controls.Hide();
            }
        }

        protected void btnShowSubAddNewPanel_Click(object sender, EventArgs e)
        {
            gvAppVariables.SelectedIndex = -1;
            panelAddAppVariable.Visible = true;
        }

        protected void gvAppVariables_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            gvAppVariables.SelectedIndex = -1;
            panelAddAppVariable.Visible = false;
        }

        protected void dvAddAppVariable_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            gvApps.DataBind();
        }

        protected void dvAddAppVariable_ItemCommand(object sender, DetailsViewCommandEventArgs e)
        {
            if (e.CommandName == "Cancel")
            {
                gvAppVariables.SelectedIndex = -1;
                panelAddAppVariable.Visible = false;
            }
        }

        protected void dvAddAppVariable_DataBound(object sender, EventArgs e)
        {
            var ddlApps = dvAddAppVariable.FindControl("ddlApps") as DropDownList;
            for (var i = 0; i < ddlApps.Items.Count; ++i)
            {
                if (ddlApps.Items[i].Value == hidSelectedAppCode.Value)
                {
                    ddlApps.SelectedIndex = i;
                    break;
                }
            }
        }
    }
}
