using System;
using System.ComponentModel;
using System.Web;
using System.Security.Permissions;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Configuration;

namespace NIntegrate.Web
{
    [DefaultProperty("AppSettingKey"), AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class AppSettingParameter : Parameter
    {
        #region Constructors

        public AppSettingParameter()
        {
        }

        protected AppSettingParameter(AppSettingParameter original)
            : base(original)
        {
            AppSettingKey = original.AppSettingKey;
        }

        public AppSettingParameter(string name, string appSettingKey)
            : base(name)
        {
            AppSettingKey = appSettingKey;
        }

        public AppSettingParameter(string name, TypeCode type, string appSettingKey)
            : base(name, type)
        {
            AppSettingKey = appSettingKey;
        }

        #endregion

        #region Public Properties

        [Description("AppSettingParameter AppSettingKey"), Category("Parameter"), DefaultValue("")]
        public string AppSettingKey
        {
            get
            {
                return (ViewState["AppSettingKey"] == null ? string.Empty : (string)ViewState["AppSettingKey"]);
            }
            set
            {
                if (AppSettingKey != value)
                {
                    ViewState["AppSettingKey"] = value;
                    OnParameterChanged();
                }
            }
        }

        #endregion

        #region Non-Public Methods

        protected override Parameter Clone()
        {
            return new AppSettingParameter(this);
        }

        protected override object Evaluate(HttpContext context, Control control)
        {
            if ((context != null) && (context.Request != null))
            {
                return ConfigurationManager.AppSettings[AppSettingKey];
            }
            return null;
        }

        #endregion
    }
}
