using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using System.ServiceModel.Configuration;

namespace NIntegrate.Test.DatabaseScripts.TestClasses
{
    public class TestCustomServiceBehavior : IServiceBehavior
    {
        #region IServiceBehavior Members

        public void AddBindingParameters(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
            throw new NotImplementedException();
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            throw new NotImplementedException();
        }

        public void Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class TestCustomServiceBehaviorElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(TestCustomServiceBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new TestCustomServiceBehavior();
        }
    }
}
