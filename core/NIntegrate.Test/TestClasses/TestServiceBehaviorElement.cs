using System;
using System.ServiceModel.Configuration;

namespace NIntegrate.Test.TestClasses
{
    public class TestServiceBehaviorElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(TestServiceBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new TestServiceBehavior();
        }
    }
}
