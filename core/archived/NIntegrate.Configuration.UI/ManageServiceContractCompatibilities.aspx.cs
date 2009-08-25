using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using NIntegrate.Configuration.UI.Code.Criterias;

namespace NIntegrate.Configuration.UI
{
    public partial class ManageServiceContractCompatibilities : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var criteria = new ServiceContractCompatibilityCriteria();
                dsServiceContractCompatibilities.Criteria = criteria.AddSortBy(criteria.ServiceContract, false);
            }
        }

        protected void btnShowAddNewPanel_Click(object sender, EventArgs e)
        {
            gvServiceContractCompatibilities.SelectedIndex = -1;
            panelBottom.Controls.Hide();
            dvAddServiceContractCompatibility.Visible = true;
        }

        protected void gvServiceContractCompatibilities_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            gvServiceContractCompatibilities.SelectedIndex = -1;
            panelBottom.Controls.Hide();
        }

        protected void dvAddServiceContractCompatibility_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            gvServiceContractCompatibilities.DataBind();
        }

        protected void dvAddServiceContractCompatibility_ItemCommand(object sender, DetailsViewCommandEventArgs e)
        {
            if (e.CommandName == "Cancel")
            {
                gvServiceContractCompatibilities.SelectedIndex = -1;
                panelBottom.Controls.Hide();
            }
        }
    }
}
