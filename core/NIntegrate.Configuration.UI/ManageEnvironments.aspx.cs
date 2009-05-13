using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using NIntegrate.Configuration.UI.Code.Criterias;

namespace NIntegrate.Configuration.UI
{
    public partial class ManageEnvironments : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var envCriteria = new EnvironmentCriteria();
                dsEnvironments.Criteria = envCriteria.AddSortBy(envCriteria.Environment_id, false);
            }
        }

        protected void btnShowAddNewPanel_Click(object sender, EventArgs e)
        {
            gvEnvironments.SelectedIndex = -1;
            panelBottom.Controls.Hide();
            dvAddEnvironment.Visible = true;
        }

        protected void gvEnvironments_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            gvEnvironments.SelectedIndex = -1;
            panelBottom.Controls.Hide();

            if (e.CommandName == "Select")
            {
                panelBottom.Style["display"] = "block";

                var parameters = e.CommandArgument.ToString().Split('|');

                if (parameters.Length == 2)
                {
                    panelBottom.Controls.Hide();
                    if (parameters[0] == "ViewFarms")
                    {
                        var farmCriteria = new FarmCriteria();
                        dsFarms.Criteria = farmCriteria.AddSortBy(farmCriteria.Farm_id, false)
                            .And(farmCriteria.Environment_id == int.Parse(parameters[1]));
                        gvFarms.DataBind();
                        gvFarms.Visible = true;
                    }
                    else if (parameters[0] == "ViewConnectionStrings")
                    {
                        var connStrCriteria = new ConnectionStringCriteria();
                        dsConnectionStrings.Criteria = connStrCriteria.AddSortBy(connStrCriteria.Name, false)
                            .And(connStrCriteria.Environment_id == int.Parse(parameters[1]));
                        dsConnectionStrings.DataBind();
                        gvConnectionStrings.Visible = true;
                    }
                }
            }
        }

        protected void dvAddEnvironment_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            gvEnvironments.DataBind();
        }

        protected void dvAddEnvironment_ItemCommand(object sender, DetailsViewCommandEventArgs e)
        {
            if (e.CommandName == "Cancel")
            {
                gvEnvironments.SelectedIndex = -1;
                panelBottom.Controls.Hide();
            }
        }
    }
}
