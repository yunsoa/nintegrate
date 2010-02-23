using System;
using System.Web.UI;
using Microsoft.Practices.CompositeWeb;
using Microsoft.Practices.CompositeWeb.Interfaces;
using Microsoft.Practices.CompositeWeb.Web;
using System.Security.Principal;

namespace EAppointments
{
    /// <summary>
    /// Custom WebClientApplication that injects dependencies into
    /// User Controls and Master Pages.
    /// </summary>
    public class WebClientApplication : Microsoft.Practices.CompositeWeb.WebClientApplication
    {
        protected override void PrePageExecute(Page page)
        {
            base.PrePageExecute(page);
            page.InitComplete += new EventHandler(OnPageInitComplete);
            page.PreInit += new EventHandler(OnPagePreInit);
        }

        private void OnPageInitComplete(object sender, EventArgs e)
        {
            Page page = (Page)sender;
            page.InitComplete -= new EventHandler(OnPageInitComplete);
            ICompositionContainer moduleContainer = GetModuleContainer(new HttpContext(System.Web.HttpContext.Current));
            BuildControls(moduleContainer, page.Controls);
        }

        private void OnPagePreInit(object sender, EventArgs e)
        {
            Page page = (Page)sender;
            page.PreInit -= new EventHandler(OnPagePreInit);
            page.Theme = GetPageTheme();            
        }

        private string GetPageTheme()
        {
            IPrincipal currentPrincipal = System.Threading.Thread.CurrentPrincipal;
            
            if (!currentPrincipal.Identity.IsAuthenticated)
                return "Default";

            if (currentPrincipal.IsInRole("Patient"))
                return "Purple";
            if (currentPrincipal.IsInRole("BmsAdmin"))
                return "Green";
            if (currentPrincipal.IsInRole("Provider"))
                return "Orange";

            return "Default";
        }

        private void BuildControls(ICompositionContainer moduleContainer, ControlCollection controls)
        {
            foreach (Control currentControl in controls)
            {
                if (currentControl is UserControl)
                {
                    CompositionContainer.BuildItem(PageBuilder, moduleContainer.Locator, currentControl);
                    BuildControls(moduleContainer, currentControl.Controls);
                }
                else
                {
                    BuildControls(moduleContainer, currentControl.Controls);
                }
            }
        }
    }
}
