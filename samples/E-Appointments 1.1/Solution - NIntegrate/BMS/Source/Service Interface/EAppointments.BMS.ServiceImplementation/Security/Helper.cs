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
using System.ServiceModel;
using System.Security.Principal;
using System.IdentityModel.Selectors;
using System.IdentityModel.Policy;
using System.IdentityModel.Claims;
using System.Security;
using EAppointments.BMS;
using EAppointments.BMS.Security;
using EAppointments.BMS.ServiceImplementation.Security;


namespace EAppointments.BMS.ServiceImplementation
{
    public class Helper
    {
        public static Application GetApplication()
        {
            User currentUser = (User)System.Threading.Thread.CurrentPrincipal.Identity;
            return currentUser.Parent.Application;
        }

        public static void Logout()
        {
            User currentUser = (User)System.Threading.Thread.CurrentPrincipal.Identity;
            currentUser.Parent.Logout();
        }
    }
}
