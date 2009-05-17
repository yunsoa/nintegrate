using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using NIntegrate.Configuration.UI.Code.Criterias;

namespace NIntegrate.Configuration.UI
{
    public partial class ManageCustomBehaviorTypes : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var criteria = new CustomBehaviorTypeCriteria();
                dsCustomBehaviorTypes.Criteria = criteria.AddSortBy(criteria.BehaviorType_id, false);
            }
        }

        protected void btnShowAddNewPanel_Click(object sender, EventArgs e)
        {
            gvBehaviorTypes.SelectedIndex = -1;
            panelBottom.Controls.Hide();
            dvAddBehaviorType.Visible = true;
        }

        protected void gvBehaviorTypes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            gvBehaviorTypes.SelectedIndex = -1;
            panelBottom.Controls.Hide();
        }

        protected void dvAddBehaviorType_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            gvBehaviorTypes.DataBind();
        }

        protected void dvAddBehaviorType_ItemCommand(object sender, DetailsViewCommandEventArgs e)
        {
            if (e.CommandName == "Cancel")
            {
                gvBehaviorTypes.SelectedIndex = -1;
                panelBottom.Controls.Hide();
            }
        }
    }
}
