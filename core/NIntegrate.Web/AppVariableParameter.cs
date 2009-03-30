using System;
using System.ComponentModel;
using System.Web;
using System.Security.Permissions;
using System.Web.UI.WebControls;
using System.Web.UI;
using NIntegrate.Configuration;

namespace NIntegrate.Web
{
    [DefaultProperty("AppVariableName"), AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class AppVariableParameter : Parameter
    {
        #region Constructors

        public AppVariableParameter()
        {
        }

        protected AppVariableParameter(AppVariableParameter original)
            : base(original)
        {
            AppVariableName = original.AppVariableName;
        }

        public AppVariableParameter(string name, string appVariableName)
            : base(name)
        {
            AppVariableName = appVariableName;
        }

        public AppVariableParameter(string name, TypeCode type, string appVariableName)
            : base(name, type)
        {
            AppVariableName = appVariableName;
        }

        #endregion

        #region Public Properties

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

        protected override Parameter Clone()
        {
            return new AppVariableParameter(this);
        }

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
