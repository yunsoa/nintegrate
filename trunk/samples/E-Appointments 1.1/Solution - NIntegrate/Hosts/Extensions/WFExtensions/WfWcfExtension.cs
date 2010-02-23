using System;
using System.ServiceModel;
using System.Workflow.Activities;
using System.Workflow.Runtime;

namespace WFExtensions
{
    /// <summary>
    /// This is an extension applied to ServiceHostBase which will instantiate an
    /// instance of our Workflow container and start the runtime. This provides
    /// a single instance of the Workflow runtime per service instance.
    /// </summary>
    public class WfWcfExtension : IExtension<ServiceHostBase>, IDisposable
    {
        private WorkflowRuntime workflowRuntime;
        private string workflowServicesConfig;

        public WfWcfExtension(string WorkflowServicesConfig)
        {
            workflowServicesConfig = WorkflowServicesConfig;
        }

        void IExtension<ServiceHostBase>.Attach(ServiceHostBase owner)
        {
            // When this Extension is attached within the Service Host, create a
            // new instance of the WorkflowServiceContainer
            workflowRuntime = new WorkflowRuntime(workflowServicesConfig);
            workflowRuntime.ServicesExceptionNotHandled +=
                new EventHandler<ServicesExceptionNotHandledEventArgs>(
                    workflowRuntime_ServicesExceptionNotHandled);

            ExternalDataExchangeService exSvc = new ExternalDataExchangeService();
            workflowRuntime.AddService(exSvc);

            // Start the services associated with the container
            workflowRuntime.StartRuntime();
        }

        private void workflowRuntime_ServicesExceptionNotHandled(object sender, ServicesExceptionNotHandledEventArgs e)
        {
            Console.WriteLine("ServicesExceptionNotHandled");
        }

        void IExtension<ServiceHostBase>.Detach(ServiceHostBase owner)
        {
            // When this WCF Extension is detached, then just stop the Workflow Container
            workflowRuntime.StopRuntime();
        }

        public WorkflowRuntime WorkflowRuntime
        {
            get { return workflowRuntime; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            workflowRuntime.Dispose();
        }

        #endregion
    }
}
