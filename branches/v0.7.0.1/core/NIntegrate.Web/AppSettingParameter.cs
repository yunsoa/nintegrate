using System;
using System.ComponentModel;
using System.Web;
using System.Security.Permissions;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Configuration;

namespace NIntegrate.Web
{
    /// <summary>
    /// The AppSettingParameter.
    /// </summary>
    [DefaultProperty("AppSettingKey"), AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class AppSettingParameter : Parameter
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettingParameter"/> class.
        /// </summary>
        public AppSettingParameter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettingParameter"/> class.
        /// </summary>
        /// <param name="original">The original.</param>
        protected AppSettingParameter(AppSettingParameter original)
            : base(original)
        {
            AppSettingKey = original.AppSettingKey;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettingParameter"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="appSettingKey">The app setting key.</param>
        public AppSettingParameter(string name, string appSettingKey)
            : base(name)
        {
            AppSettingKey = appSettingKey;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettingParameter"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <param name="appSettingKey">The app setting key.</param>
        public AppSettingParameter(string name, TypeCode type, string appSettingKey)
            : base(name, type)
        {
            AppSettingKey = appSettingKey;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the app setting key.
        /// </summary>
        /// <value>The app setting key.</value>
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

        /// <summary>
        /// Returns a duplicate of the current <see cref="T:System.Web.UI.WebControls.Parameter"/> instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Web.UI.WebControls.Parameter"/> that is an exact duplicate of the current one.
        /// </returns>
        protected override Parameter Clone()
        {
            return new AppSettingParameter(this);
        }

        /// <summary>
        /// Updates and returns the value of the <see cref="T:System.Web.UI.WebControls.Parameter"/> object.
        /// </summary>
        /// <param name="context">The current <see cref="T:System.Web.HttpContext"/> of the request.</param>
        /// <param name="control">The <see cref="T:System.Web.UI.Control"/> the parameter is bound to. If the parameter is not bound to a control, the <paramref name="control"/> parameter is ignored.</param>
        /// <returns>
        /// An object that represents the updated and current value of the parameter.
        /// </returns>
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
