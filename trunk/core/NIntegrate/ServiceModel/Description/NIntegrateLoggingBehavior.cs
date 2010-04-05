using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace NIntegrate.ServiceModel.Description
{
    /// <summary>
    /// The behavior to log WCF host &amp; client errors and nessages
    /// </summary>
    public sealed class NIntegrateLoggingBehavior : IServiceBehavior, IErrorHandler, IDispatchMessageInspector
    {
        /// <summary>
        /// Occurs when logging error.
        /// </summary>
        public static event LogErrorHandler LogError;

        /// <summary>
        /// Occurs when logging message.
        /// </summary>
        public static event LogMessageHandler LogMessage;

        #region IServiceBehavior Members

        /// <summary>
        /// Provides the ability to pass custom data to binding elements to support the contract implementation.
        /// </summary>
        /// <param name="serviceDescription">The service description of the service.</param>
        /// <param name="serviceHostBase">The host of the service.</param>
        /// <param name="endpoints">The service endpoints.</param>
        /// <param name="bindingParameters">Custom objects to which binding elements have access.</param>
        public void AddBindingParameters(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        /// <summary>
        /// Provides the ability to change run-time property values or insert custom extension objects such as error handlers, message or parameter interceptors, security extensions, and other custom extension objects.
        /// </summary>
        /// <param name="serviceDescription">The service description.</param>
        /// <param name="serviceHostBase">The host that is currently being built.</param>
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher chanDisp in serviceHostBase.ChannelDispatchers)
            {
                chanDisp.ErrorHandlers.Add(this);

                foreach (EndpointDispatcher epDisp in chanDisp.Endpoints)
                {
                    epDisp.DispatchRuntime.MessageInspectors.Add(this);
                }
            }
        }

        /// <summary>
        /// Provides the ability to inspect the service host and the service description to confirm that the service can run successfully.
        /// </summary>
        /// <param name="serviceDescription">The service description.</param>
        /// <param name="serviceHostBase">The service host that is currently being constructed.</param>
        public void Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
        }

        #endregion

        #region IErrorHandler Members

        /// <summary>
        /// Enables error-related processing and returns a value that indicates whether subsequent HandleError implementations are called.
        /// </summary>
        /// <param name="error">The exception thrown during processing.</param>
        /// <returns>
        /// true if subsequent <see cref="T:System.ServiceModel.Dispatcher.IErrorHandler"/> implementations must not be called; otherwise, false. The default is false.
        /// </returns>
        public bool HandleError(Exception error)
        {
            if (!(error is CommunicationException))
            {
                OnLogError(error);
            }

            return false;
        }

        /// <summary>
        /// Enables the creation of a custom <see cref="T:System.ServiceModel.FaultException`1"/> that is returned from an exception in the course of a service method.
        /// </summary>
        /// <param name="error">The <see cref="T:System.Exception"/> object thrown in the course of the service operation.</param>
        /// <param name="version">The SOAP version of the message.</param>
        /// <param name="fault">The <see cref="T:System.ServiceModel.Channels.Message"/> object that is returned to the client, or service, in the duplex case.</param>
        public void ProvideFault(Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault)
        {
        }

        #endregion

        #region IDispatchMessageInspector Members

        /// <summary>
        /// Called after an inbound message has been received but before the message is dispatched to the intended operation.
        /// </summary>
        /// <param name="request">The request message.</param>
        /// <param name="channel">The incoming channel.</param>
        /// <param name="instanceContext">The current service instance.</param>
        /// <returns>
        /// The object used to correlate state. This object is passed back in the <see cref="M:System.ServiceModel.Dispatcher.IDispatchMessageInspector.BeforeSendReply(System.ServiceModel.Channels.Message@,System.Object)"/> method.
        /// </returns>
        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            if (string.IsNullOrEmpty(request.Headers.Action))
            {
                return null;
            }

            return request;
        }

        /// <summary>
        /// Called after the operation has returned but before the reply message is sent.
        /// </summary>
        /// <param name="reply">The reply message. This value is null if the operation is one way.</param>
        /// <param name="correlationState">The correlation object returned from the <see cref="M:System.ServiceModel.Dispatcher.IDispatchMessageInspector.AfterReceiveRequest(System.ServiceModel.Channels.Message@,System.ServiceModel.IClientChannel,System.ServiceModel.InstanceContext)"/> method.</param>
        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            if (correlationState == null)
            {
                return;
            }

            Message request = (Message)correlationState;
            OnLogMessage(request, reply);
        }

        #endregion

        #region Non-Public Methods

        private static void OnLogError(Exception ex)
        {
            if (LogError != null)
                LogError(ex);
        }

        private static void OnLogMessage(Message request, Message reply)
        {
            if (LogMessage != null)
                LogMessage(request, reply);
        }

        #endregion

        #region Nested Classes

        /// <summary>
        /// 
        /// </summary>
        public delegate void LogErrorHandler(Exception ex);

        /// <summary>
        /// 
        /// </summary>
        public delegate void LogMessageHandler(Message request, Message reply);

        #endregion
    }
}
