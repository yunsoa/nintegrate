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
    public sealed class NIntegrateLoggingBehavior : IServiceBehavior, IErrorHandler, IDispatchMessageInspector
    {
        public static event LogErrorHandler LogError;

        public static event LogMessageHandler LogMessage;

        #region IServiceBehavior Members

        public void AddBindingParameters(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

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

        public void Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
        }

        #endregion

        #region IErrorHandler Members

        public bool HandleError(Exception error)
        {
            if (!(error is CommunicationException))
            {
                OnLogError(error);
            }

            return false;
        }

        public void ProvideFault(Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault)
        {
        }

        #endregion

        #region IDispatchMessageInspector Members

        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            if (string.IsNullOrEmpty(request.Headers.Action))
            {
                return null;
            }

            return request;
        }

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
        
        public delegate void LogErrorHandler(Exception ex);

        public delegate void LogMessageHandler(Message request, Message reply);

        #endregion
    }
}
