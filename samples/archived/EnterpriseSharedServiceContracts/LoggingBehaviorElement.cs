using System;
using System.ServiceModel.Configuration;

namespace EnterpriseSharedServiceContracts
{
    public class LoggingBehaviorElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(LoggingBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new LoggingBehavior();
        }
    }
}
