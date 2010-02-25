using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Diagnostics;

namespace NIntegrate.WebTest
{
    public class Global : System.Web.HttpApplication
    {
        private void NIntegrateLoggingBehavior_LogError(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        private void NIntegrateLoggingBehavior_LogMessage(System.ServiceModel.Channels.Message request, System.ServiceModel.Channels.Message reply)
        {
            Console.WriteLine(request.Headers.Action);
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            NIntegrate.ServiceModel.Description.NIntegrateLoggingBehavior.LogError += new NIntegrate.ServiceModel.Description.NIntegrateLoggingBehavior.LogErrorHandler(NIntegrateLoggingBehavior_LogError);
            NIntegrate.ServiceModel.Description.NIntegrateLoggingBehavior.LogMessage += new NIntegrate.ServiceModel.Description.NIntegrateLoggingBehavior.LogMessageHandler(NIntegrateLoggingBehavior_LogMessage);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}