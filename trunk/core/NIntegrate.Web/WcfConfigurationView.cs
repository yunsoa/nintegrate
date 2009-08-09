using System.Web.UI.WebControls;
using NIntegrate.ServiceModel.Configuration;

namespace NIntegrate.Web
{
    public abstract class WcfConfigurationView : DetailsView
    {
        #region Constructors

        internal WcfConfigurationView()
        {
        }

        #endregion

        #region Sealed Override Members

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
    }
}
