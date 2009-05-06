using System;
using System.ComponentModel;
using System.Web;
using System.Security.Permissions;
using System.Web.UI.WebControls;
using System.Web.UI;
using NIntegrate.Configuration;

namespace NIntegrate.Web
{
    /// <summary>
    /// The AppVariableParameter.
    /// </summary>
    [DefaultProperty("AppVariableName"), AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class AppVariableParameter : Parameter
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AppVariableParameter"/> class.
        /// </summary>
        public AppVariableParameter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppVariableParameter"/> class.
        /// </summary>
        /// <param name="original">The original.</param>
        protected AppVariableParameter(AppVariableParameter original)
            : base(original)
        {
            AppVariableName = original.AppVariableName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppVariableParameter"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="appVariableName">Name of the app variable.</param>
        public AppVariableParameter(string name, string appVariableName)
            : base(name)
        {
            AppVariableName = appVariableName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppVariableParameter"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <param name="appVariableName">Name of the app variable.</param>
        public AppVariableParameter(string name, TypeCode type, string appVariableName)
            : base(name, type)
        {
            AppVariableName = appVariableName;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the name of the app variable.
        /// </summary>
        /// <value>The name of the app variable.</value>
        [Description("AppVariableParameter AppVariableName"), Category("Parameter"), DefaultValue("")]
        public string AppVariableName
        {
            get
            {
                return (ViewState["AppVariableName"] == null ? string.Empty : (string)ViewState["AppVariableName"]);
            }
            set
            {
                if (AppVariableName != value)
                {
                    ViewState["AppVariableName"] = value;
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
            return new AppVariableParameter(this);
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
                return AppVariableStore.GetAppVariable(AppVariableName);
            }
            return null;
        }

        #endregion
    }
}
