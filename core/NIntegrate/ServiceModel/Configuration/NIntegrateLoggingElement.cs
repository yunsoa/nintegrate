using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Configuration;
using NIntegrate.ServiceModel.Description;

namespace NIntegrate.ServiceModel.Configuration
{
    public sealed class NIntegrateLoggingElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(NIntegrateLoggingBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new NIntegrateLoggingBehavior();
        }
    }
}
