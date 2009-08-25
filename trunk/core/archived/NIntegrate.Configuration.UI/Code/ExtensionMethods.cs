using System.Web.UI;

namespace NIntegrate.Configuration.UI
{
    public static class ExtensionMethods
    {
        public static void Hide(this ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                control.Visible = false;
            }
        }
    }
}
