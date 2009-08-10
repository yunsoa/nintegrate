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

            WcfServiceView1.DataSource = new[] { AppConfigLoader.Default.LoadService(typeof(QueryService)) };
            WcfServiceView1.DataBind();
        }

        protected void WcfServiceEndpointView1_ModeChanging(object sender, DetailsViewModeEventArgs e)
        {
            //e.NewMode = DetailsViewMode.Edit;
        }

        protected void DetailsView1_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            GridView1.DataBind();
        }
    }
}
