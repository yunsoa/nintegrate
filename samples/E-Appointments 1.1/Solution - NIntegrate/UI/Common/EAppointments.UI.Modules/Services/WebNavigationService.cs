//===============================================================================
// Microsoft Aspiring Software Architects Program
// E-Appointments - Case Study Implementation
//===============================================================================
// Copyright  Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===============================================================================

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using EAppointments.UI.Modules.Services;

namespace EAppointments.UI.Modules.Services
{
    public class WebNavigationService : INavigationService
    {
        public void Navigate(string location)
        {
            String url = GetUrl(location);
            HttpContext.Current.Response.Redirect(url, true);
        }

        String GetUrl(String location)
        {
            switch (location)
            {
                case "Home": return "Default.aspx";
                case "CreateAppointment": return "SelectPatient.aspx";  
                case "BookAppointment" : return "BookSlot.aspx";
                case "CancelAppointment" : return "Action.aspx?command=cancel";
                case "ApproveAppointment": return "Action.aspx?command=approve";
                case "RejectAppointment": return "Action.aspx?command=reject";
                case "DeleteAppointment": return "Action.aspx?command=delete";
                case "EditAppointment": return "Summary.aspx";
                case "SelectProvider": return "SelectProvider.aspx";
                case "SelectSlot": return "BookSlot.aspx";
                case "Summary": return "Summary.aspx";
            }
            return "Default.aspx";
        }
    }
}