using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using NIntegrate.Configuration.UI.Code.Criterias;

namespace NIntegrate.Configuration.UI
{
    public partial class ManageServiceCategories : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var criteria = new ServiceCategoryCriteria();
                dsServiceCategories.Criteria = criteria.AddSortBy(criteria.ServiceCategory_id, false);
            }
        }

        protected void btnShowAddNewPanel_Click(object sender, EventArgs e)
        {
            gvServiceCategories.SelectedIndex = -1;
            panelBottom.Controls.Hide();
            dvAddServiceCategory.Visible = true;
        }

        protected void gvServiceCategories_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            gvServiceCategories.SelectedIndex = -1;
            panelBottom.Controls.Hide();

            if (e.CommandName == "Select")
            {
                panelBottom.Style["display"] = "block";

                var parameters = e.CommandArgument.ToString().Split('|');

                if (parameters.Length == 2)
                {
                    panelBottom.Controls.Hide();
                    if (parameters[0] == "ViewServices")
                    {
                        var serviceCriteria = new ServiceCriteria();
                        dsServices.Criteria = serviceCriteria.AddSortBy(serviceCriteria.Service_id, false)
                            .And(serviceCriteria.ServiceCategory_id == int.Parse(parameters[1]));
                        gvServices.DataBind();
                        gvServices.Visible = true;
                    }
                }
            }
        }

        protected void dvAddServiceCategory_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            gvServiceCategories.DataBind();
        }

        protected void dvAddServiceCategory_ItemCommand(object sender, DetailsViewCommandEventArgs e)
        {
            if (e.CommandName == "Cancel")
            {
                gvServiceCategories.SelectedIndex = -1;
                panelBottom.Controls.Hide();
            }
        }

    }
}
