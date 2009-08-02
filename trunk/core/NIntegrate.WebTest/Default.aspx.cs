using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NIntegrate.ServiceModel.Configuration;
using NIntegrate.Data;

namespace NIntegrate.WebTest
{
    public partial class _Default : System.Web.UI.Page
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
