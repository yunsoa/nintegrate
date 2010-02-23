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
using System.Security.Principal;

namespace EAppointments.BMS.Security
{
    public class EAPrincipal : IPrincipal
    {
        private List<string> _roles;
        internal User _user;

        public EAPrincipal(ICollection<string> roles, User user)
        {
            _user = user;
            _roles = new List<string>(roles);
        }

        public bool IsInRole(string role)
        {
            return _roles.Contains(role);
        }

        public IIdentity Identity
        {
            get { return _user; }
        }
    }
}
