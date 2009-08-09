using System.Web.UI.WebControls;

namespace NIntegrate.Web
{
    internal sealed class WcfClientEndpointView : WcfEndpointView
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
