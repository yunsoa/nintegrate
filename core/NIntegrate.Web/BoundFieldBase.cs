using System;
using System.Reflection;
using System.Web.UI.WebControls;

namespace NIntegrate.Web
{
    internal class BoundFieldBase : BoundField
    {
        public Unit FieldWidth { get; set; }
        public Unit FieldHeight { get; set; }

        protected override object GetValue(System.Web.UI.Control controlContainer)
        {
            if (controlContainer != null)
            {
                var dv = controlContainer as DetailsView;
                var bindingObj = dv.DataItem;
                if (bindingObj != null && !string.IsNullOrEmpty(DataField))
                {
                    object result = null;
                    var propertyChain = DataField.Split('.');
                    if (propertyChain != null)
                    {
                        foreach (var property in propertyChain)
                        {
                            var pi = GetPropertyInfo(bindingObj.GetType(), property);
                            if (pi != null)
                            {
                                result = pi.GetValue(bindingObj, null);

                                if (result == null)
                                    break;

                                bindingObj = result;
                            }
                        }

                        return result;
                    }
                }
            }

            return null;
        }

        private PropertyInfo GetPropertyInfo(Type type, string property)
        {
            PropertyInfo result;

            while (true)
            {
                result = type.GetProperty(property);
                if (result != null)
                    break;
                
                if (type.BaseType != null && type.BaseType != typeof(object)
                    && type.BaseType != typeof(ValueType))
                {
                    type = type.BaseType;
                }
            }

            if (result == null)
                throw new InvalidOperationException(string.Format("Could not find a public property named {0} on {1}", property, type));

            return result;
        }
    }
}
