using System.Web.UI.WebControls;
using System.Web;

namespace NIntegrate.Web
{
    public abstract class WcfConfigurationView : DetailsView
    {
        private Unit _fieldWidth;

        #region Constructors

        internal WcfConfigurationView()
        {
        }

        #endregion

        #region Properties

        public Unit FieldWidth
        {
            get { return _fieldWidth == default(Unit) ? new Unit("300px") : _fieldWidth; }
            set { _fieldWidth = value; }
        }

        public sealed override bool AllowPaging
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        public sealed override bool AutoGenerateRows
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        #endregion

        #region Non-Public Methods

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            if (HttpContext.Current == null)
            {
                writer.Write(GetType().Name);
            }
        }

        #endregion
    }
}
