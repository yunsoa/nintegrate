using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using NIntegrate.Configuration.UI.Code.Criterias;

namespace NIntegrate.Configuration.UI
{
    public partial class ManageBindings : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var bindingCriteria = new BindingCriteria();
                dsBindings.Criteria = bindingCriteria.AddSortBy(bindingCriteria.Binding_id, false);
                var bindingTypeCriteria = new BindingTypeCriteria();
                dsBindingTypes.Criteria = bindingTypeCriteria.AddSortBy(bindingTypeCriteria.BindingType_id, false);
            }
        }

        protected void btnShowAddNewPanel_Click(object sender, EventArgs e)
        {
            gvBindings.SelectedIndex = -1;
            panelBottom.Controls.Hide();
            dvAddBinding.Visible = true;
        }

        protected void gvBindings_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            gvBindings.SelectedIndex = -1;
            panelBottom.Controls.Hide();
        }

        protected void dvAddBinding_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            gvBindings.DataBind();
        }

        protected void dvAddBinding_ItemCommand(object sender, DetailsViewCommandEventArgs e)
        {
            if (e.CommandName == "Cancel")
            {
                gvBindings.SelectedIndex = -1;
                panelBottom.Controls.Hide();
            }
        }
    }
}
