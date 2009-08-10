using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NIntegrate.Web
{
    [ToolboxData("<{0}:WcfServiceView runat=\"server\"></{0}:WcfServiceView>")]
    [SupportsEventValidation]
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public sealed class WcfServiceView : WcfConfigurationView
    {
        #region Non-Public Methods

        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

            Fields.Add(new BoundTextField
            {
                HeaderText = "ServiceType",
                DataField = "ServiceType",
                FieldWidth = FieldWidth,
            });
            Fields.Add(new BoundTextField
            {
                HeaderText = "CustomServiceHostType",
                DataField = "CustomServiceHostType",
                FieldWidth = FieldWidth,
            });
            Fields.Add(new BoundXmlField
            {
                HeaderText = "ServiceBehaviorXml",
                DataField = "ServiceBehaviorXml.Xml",
                FieldWidth = FieldWidth,
                FieldHeight = new Unit(100),
            });
            Fields.Add(new BoundXmlField
            {
                HeaderText = "HostXml",
                DataField = "HostXml.Xml",
                FieldWidth = FieldWidth,
                FieldHeight = new Unit(100),
            });
        }

        #endregion
    }
}
