using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NIntegrate.Web
{
    [ToolboxData("<{0}:WcfClientEndpointView runat=\"server\"></{0}:WcfClientEndpointView>")]
    [SupportsEventValidation]
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public sealed class WcfClientEndpointView : WcfEndpointView
    {
        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

            Fields.Add(new BoundXmlField
                           {
                               HeaderText = "MetadataXml",
                               DataField = "MetadataXml.Xml",
                               FieldWidth = FieldWidth,
                               FieldHeight = new Unit(50),
                           });
        }
    }
}
