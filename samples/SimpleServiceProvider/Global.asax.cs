﻿using System;
using System.Web;
using System.Web.Hosting;
using NIntegrate.Web;

namespace SimpleServiceProvider
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            //when the line below is uncommented, you could delete the .svc files in this app
            //HostingEnvironment.RegisterVirtualPathProvider(new WcfVirtualPathProvider());
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