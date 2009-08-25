using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using NIntegrate.Configuration.UI.Code.Criterias;

namespace NIntegrate.Configuration.UI
{
    public partial class ManageServiceHostTypes : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var criteria = new ServiceHostTypeCriteria();
                dsServiceHostTypes.Criteria = criteria.AddSortBy(criteria.ServiceHostType_id, false);
            }
        }

        protected void btnShowAddNewPanel_Click(object sender, EventArgs e)
        {
            gvServiceHostTypes.SelectedIndex = -1;
            panelBottom.Controls.Hide();
            dvAddServiceHostType.Visible = true;
        }

        protected void gvServiceHostTypes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            gvServiceHostTypes.SelectedIndex = -1;
            panelBottom.Controls.Hide();
        }

        protected void dvAddServiceHostType_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            gvServiceHostTypes.DataBind();
        }

        protected void dvAddServiceHostType_ItemCommand(object sender, DetailsViewCommandEventArgs e)
        {
            if (e.CommandName == "Cancel")
            {
                gvServiceHostTypes.SelectedIndex = -1;
                panelBottom.Controls.Hide();
            }
        }
    }
}
