using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Security.Permissions;
using NIntegrate.ServiceModel.Activation;

namespace NIntegrate.Web
{
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level=AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.LinkDemand, Level=AspNetHostingPermissionLevel.Minimal)]
    public sealed class BoundDropDownListField : BoundFieldBase
    {
        protected override DataControlField CreateField()
        {
            return new BoundDropDownListField();
        }

        public override void ExtractValuesFromCell(IOrderedDictionary dictionary, DataControlFieldCell cell, DataControlRowState rowState, bool includeReadOnly)
        {
            var key = DataField;
            object value = null;
            if (cell.Controls.Count > 0)
            {
                var control = cell.Controls[0];
                var list = control as DropDownList;
                if ((list != null) && list.Items.Count > 0 && (includeReadOnly || list.Enabled))
                {
                    value = list.SelectedValue;
                }
            }
            if (value != null)
            {
                if (dictionary.Contains(key))
                {
                    dictionary[key] = value;
                }
                else
                {
                    dictionary.Add(key, value);
                }
            }
        }

        protected override object GetDesignTimeValue()
        {
            return 0;
        }

        protected override void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
        {
            DropDownList child = null;
            DropDownList list = null;
            if ((((rowState & DataControlRowState.Edit) != DataControlRowState.Normal) && !ReadOnly) || ((rowState & DataControlRowState.Insert) != DataControlRowState.Normal))
            {
                var ddl = new DropDownList { Height = FieldHeight, Width = FieldWidth };
                InitializeDropDownListItems(ddl);

                ddl.ToolTip = HeaderText;
                child = ddl;
                if ((DataField.Length != 0) && ((rowState & DataControlRowState.Edit) != DataControlRowState.Normal))
                {
                    list = ddl;
                }
            }
            else if (DataField.Length != 0)
            {
                var ddl = new DropDownList {Height = FieldHeight, Width = FieldWidth};
                InitializeDropDownListItems(ddl);

                ddl.Enabled = false;
                child = ddl;
                list = ddl;
            }
            if (child != null)
            {
                cell.Controls.Add(child);
            }
            if ((list != null) && Visible)
            {
                list.DataBinding += OnDataBindField;
            }
        }

        private void InitializeDropDownListItems(DropDownList listControl)
        {
            listControl.Items.Clear();
            if (IsNullable)
                listControl.Items.Add(new ListItem(string.Empty));
            var typeOfEnum = WcfServiceHostFactory.GetType(EnumType, true);
            var descs = GetDescriptions(typeOfEnum);
            var en = descs.GetEnumerator();
            while (en.MoveNext())
            {
                listControl.Items.Add(new ListItem(en.Current.Value, (en.Current.Key).ToString()));
            }
        }

        private static Dictionary<int, string> GetDescriptions(Type enumType)
        {
            var fields = enumType.GetFields();
            var descs = new Dictionary<int, string>();
            for (var i = 1; i < fields.Length; ++i)
            {
                var fieldValue = Enum.Parse(enumType, fields[i].Name);
                descs.Add((int)fieldValue, fieldValue.ToString());
            }

            return descs;
        }

        protected override void OnDataBindField(object sender, System.EventArgs e)
        {
            var control = (Control) sender;
            var controlContainer = control.NamingContainer;
            var value = GetValue(controlContainer);
            if (!(control is DropDownList))
            {
                throw new HttpException("DropDownListField_WrongControlType");
            }
            if (value == null)
            {
                ((DropDownList) control).SelectedIndex = 0;
            }
            else if (value.GetType().IsEnum)
            {
                var listControl = ((DropDownList) control);
                for (var i = 0; i < listControl.Items.Count; ++i)
                {
                    if (((int)value).ToString() == listControl.Items[i].Value)
                    {
                        listControl.SelectedIndex = i;
                        break;
                    }
                }
            }
            else
            {
                throw new HttpException("DropDownListField_ValueMustBeEnum");
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected override bool SupportsHtmlEncode
        {
            get
            {
                return false;
            }
        }

		[Category("Binding"), DefaultValue(""), Description("The full name of the enum type bound to this field.")]
		public string EnumType
        {
            get { return GetViewState<string>("EnumType"); }
            set { ViewState["EnumType"] = value; }
		}

        public bool IsNullable { get; set; }

        private T GetViewState<T>(string key)
        {
            return ViewState[key] == null ? default(T) : (T)ViewState[key];
        }
    }
}
