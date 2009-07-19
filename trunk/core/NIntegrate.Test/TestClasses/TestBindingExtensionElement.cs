using System;
using System.ServiceModel.Configuration;

namespace NIntegrate.Test.TestClasses
{
    public class TestBindingExtensionElement : BindingElementExtensionElement
    {
        public override Type BindingElementType
        {
            get { return typeof (TestBindingElement); }
        }

        protected override System.ServiceModel.Channels.BindingElement CreateBindingElement()
        {
            return new TestBindingElement();
        }
    }
}
