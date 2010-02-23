using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;

namespace WFExtensions
{
    public class WFServiceHost : ServiceHost
    {
        public WFServiceHost(Type t, params Uri[] baseAddresses) : base(t, baseAddresses) { }

        public WFServiceHost(object singletonInstance, params Uri[] baseAddresses)
            : base(singletonInstance, baseAddresses) { }

        public WFServiceHost() : base() { }

        protected override void OnOpening()
        {
            WfWcfExtension wfWcfExtension = new WfWcfExtension("WorkflowRuntimeConfig");
            Extensions.Add(wfWcfExtension);
            base.OnOpening();
        }
    }
}
