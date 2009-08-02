using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using NIntegrate.ServiceModel.Configuration;
using NIntegrate.Web;

namespace NIntegrate.WebTest
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            QueryDataSource1.Endpoint = AppConfigLoader.Default.LoadClientEndpoint(typeof(IQueryService));
        }

        protected void DetailsView1_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            GridView1.DataBind();
        }
    }
}
