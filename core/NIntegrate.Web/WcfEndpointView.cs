using System.Web.UI.WebControls;

namespace NIntegrate.Web
{
    public abstract class WcfEndpointView : WcfConfigurationView
    {
        private Unit _fieldWidth;

        #region Constructors

        internal WcfEndpointView()
        {
        }

        #endregion

        #region Properties

        public Unit FieldWidth
        {
            get { return _fieldWidth == default(Unit) ? new Unit("350") : _fieldWidth; }
            set { _fieldWidth = value; }
        }

        #endregion

        #region Non-Public Methods

        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

            Fields.Add(new BoundTextField
                           {
                               HeaderText = "ServiceContract",
                               DataField = "ServiceContractType",
                               FieldWidth = FieldWidth,
                           });
            Fields.Add(new BoundTextField
                           {
                               HeaderText = "BindingTypeCode",
                               DataField = "BindingXml.BindingTypeCode",
                               FieldWidth = FieldWidth,
                           });
            Fields.Add(new BoundXmlField
                           {
                               HeaderText = "BindingXml",
                               DataField = "BindingXml.Xml",
                               FieldWidth = FieldWidth,
                               FieldHeight = new Unit(100),
                           });
            Fields.Add(new BoundTextField
                           {
                               HeaderText = "Address",
                               DataField = "Address",
                               FieldWidth = FieldWidth,
                           });
            Fields.Add(new BoundXmlField
                           {
                               HeaderText = "IdentityXml",
                               DataField = "IdentityXml.Xml",
                               FieldWidth = FieldWidth,
                               FieldHeight = new Unit(50),
                           });
            Fields.Add(new BoundXmlField
                           {
                               HeaderText = "HeadersXml",
                               DataField = "HeadersXml.Xml",
                               FieldWidth = FieldWidth,
                               FieldHeight = new Unit(50),
                           });
            Fields.Add(new BoundXmlField
                           {
                               HeaderText = "EndpointBehaviorXml",
                               DataField = "EndpointBehaviorXml.Xml",
                               FieldWidth = FieldWidth,
                               FieldHeight = new Unit(50),
                           });
        }

        #endregion
    }
}
