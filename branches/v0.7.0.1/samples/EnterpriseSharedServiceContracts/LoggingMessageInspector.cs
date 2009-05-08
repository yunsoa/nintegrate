using System.ServiceModel.Dispatcher;
using NIntegrate;

namespace EnterpriseSharedServiceContracts
{
    public class LoggingMessageInspector : IDispatchMessageInspector
    {
        #region IDispatchMessageInspector Members

        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel, System.ServiceModel.InstanceContext instanceContext)
        {
            if (!request.Headers.Action.EndsWith("/ILoggingService/WriteLog"))
            {
                using (var locator = new WcfServiceLocator())
                {
                    var logging = locator.GetService<ILoggingService>();
                    logging.WriteLog(string.Format("Action: {0}, To: {1}", request.Headers.Action, request.Headers.To));
                }
            }

            return null;
        }

        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
        }

        #endregion
    }
}
