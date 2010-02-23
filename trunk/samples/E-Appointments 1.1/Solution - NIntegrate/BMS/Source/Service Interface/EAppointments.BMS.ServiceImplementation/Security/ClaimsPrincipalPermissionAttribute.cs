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
using System.Security.Permissions;
using System.IdentityModel.Claims;

namespace EAppointments.BMS.ServiceImplementation.Security
{

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class ClaimsPrincipalPermissionAttribute : CodeAccessSecurityAttribute
    {

        private bool _isAuthenticated;
        private string _requiredClaimType;
        private string _resource;

        public ClaimsPrincipalPermissionAttribute(SecurityAction action)
            : base(action)
        {
            this._isAuthenticated = true;
        }

        public string RequiredClaimType
        {
            get
            {
                return this._requiredClaimType;
            }
            set
            {
                this._requiredClaimType = value;
            }
        }


        public string Resource
        {
            get
            {
                return this._resource;
            }
            set
            {
                this._resource = value;
            }
        }

        public bool Authenticated
        {
            get { return _isAuthenticated; }
            set { _isAuthenticated = value; }
        }

        public override System.Security.IPermission CreatePermission()
        {
            if (this.Unrestricted)
                return new ClaimsPrincipalPermission(PermissionState.Unrestricted);

            ClaimSet cs = ClaimsAuthorizationPolicy.CreateClaimSet(this._resource, this._requiredClaimType);
            return new ClaimsPrincipalPermission(this._isAuthenticated, cs);
        }

    }
}
