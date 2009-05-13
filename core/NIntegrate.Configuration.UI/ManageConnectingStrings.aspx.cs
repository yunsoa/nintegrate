using System;
using System.Web.UI.WebControls;
using NIntegrate.Configuration.UI.Code.Criterias;

namespace NIntegrate.Configuration.UI
{
    public partial class ManageConnectingStrings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var envCriteria = new EnvironmentCriteria();
                dsEnvironments.Criteria = envCriteria.AddSortBy(envCriteria.Environment_id, false);
                var connStrCriteria = new ConnectionStringCriteria();
                dsConnectionStrings.Criteria = connStrCriteria.AddSortBy(connStrCriteria.Name, false).AddSortBy(connStrCriteria.Environment_id, false);
            }
        }

        protected void btnShowAddNewPanel_Click(object sender, EventArgs e)
        {
            gvConnectionStrings.SelectedIndex = -1;
            panelBottom.Controls.Hide();
            dvAddConnectionString.Visible = true;
        }

        protected void gvConnectionStrings_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            gvConnectionStrings.SelectedIndex = -1;
            panelBottom.Controls.Hide();
        }

        protected void dvAddConnectionString_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            gvConnectionStrings.DataBind();
        }

        protected void dvAddConnectionString_ItemCommand(object sender, DetailsViewCommandEventArgs e)
        {
            if (e.CommandName == "Cancel")
            {
                gvConnectionStrings.SelectedIndex = -1;
                panelBottom.Controls.Hide();
            }
        }
    }
}
