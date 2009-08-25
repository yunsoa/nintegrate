using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnterpriseAspNetAppServiceContracts;
using NIntegrate;

namespace EnterpriseAspNetAppLegacyConsumer
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var locator = new WcfServiceLocator())
            {
                var service = locator.GetService<IBackCompatibleService>();
                Response.Write(service.GetCompatibleResult().Value);
            }
        }
    }
}
