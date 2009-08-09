using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.ServiceModel.Description;

namespace NIntegrate.Web
{
    [ToolboxData("<{0}:WcfServiceEndpointView runat=\"server\"></{0}:WcfServiceEndpointView>")]
    [SupportsEventValidation]
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public sealed class WcfServiceEndpointView : WcfEndpointView
    {
        #region Non-Public Methods

        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

            Fields.Add(new BoundTextField
                           {
                               HeaderText = "ListenUri",
                               DataField = "ListenUri",
                               FieldWidth = FieldWidth,
                           });
            Fields.Add(new BoundDropDownListField
                           {
                               HeaderText = "ListenUriMode",
                               DataField = "ListenUriMode",
                               FieldWidth = FieldWidth,
                               EnumType = typeof(ListenUriMode).AssemblyQualifiedName,
                               IsNullable = true
                           });
        }

        #endregion

    }
}
