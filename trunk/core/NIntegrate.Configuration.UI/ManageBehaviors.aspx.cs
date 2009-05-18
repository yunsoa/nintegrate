using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using NIntegrate.Configuration.UI.Code.Criterias;

namespace NIntegrate.Configuration.UI
{
    public partial class ManageBehaviors : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var criteria = new BehaviorCriteria();
                dsBehaviors.Criteria = criteria.AddSortBy(criteria.Behavior_id, false);
            }
        }

        protected void btnShowAddNewPanel_Click(object sender, EventArgs e)
        {
            gvBehaviors.SelectedIndex = -1;
            panelBottom.Controls.Hide();
            dvAddBehavior.Visible = true;
        }

        protected void gvBehaviors_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            gvBehaviors.SelectedIndex = -1;
            panelBottom.Controls.Hide();
        }

        protected void dvAddBehavior_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            gvBehaviors.DataBind();
        }

        protected void dvAddBehavior_ItemCommand(object sender, DetailsViewCommandEventArgs e)
        {
            if (e.CommandName == "Cancel")
            {
                gvBehaviors.SelectedIndex = -1;
                panelBottom.Controls.Hide();
            }
        }
    }
}
