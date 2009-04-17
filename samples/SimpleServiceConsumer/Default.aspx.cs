using System;
using SimpleServiceContracts;

namespace SimpleServiceConsumer
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var criteria1 = new BindingCriteria();
                QueryDataSource1.Criteria = criteria1.AddSortBy(criteria1.BindingName, false);

                var criteria2 = new BindingTypeCriteria();
                QueryDataSource2.Criteria = criteria2.AddSortBy(criteria2.BindingTypeFriendlyName, false);

                var criteria3 = new EndpointCriteria();
                QueryDataSource3.Criteria = criteria3;
            }
        }
    }
}
