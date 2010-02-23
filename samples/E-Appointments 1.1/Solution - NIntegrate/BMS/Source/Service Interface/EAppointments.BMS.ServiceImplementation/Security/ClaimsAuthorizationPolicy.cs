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
using System.IdentityModel.Policy;
using System.IdentityModel.Claims;
using EAppointments.BMS.Security;
using System.Security;
using System.Security.Principal;

namespace EAppointments.BMS.ServiceImplementation.Security
{
    public class Resources
    {
        public const string Appointment = "http://EAppointments.BMS.ServiceContracts/2007/08/Security/resource/appointment";

        public const string Provider = "http://EAppointments.BMS.ServiceContracts/2007/08/Security/resource/provider";

        // A single resource for Patient, Referrer, Specialty and Clinic Types
        public const string Directory = "http://EAppointments.BMS.ServiceContracts/2007/08/Security/resource/directory";
    }

    public class GenericClaimTypes
    {
        public const string Create = "http://EAppointments.BMS.ServiceContracts/2007/08/Security/claims/create";
        public const string Read = "http://EAppointments.BMS.ServiceContracts/2007/08/Security/claims/read";
        public const string Update = "http://EAppointments.BMS.ServiceContracts/2007/08/Security/claims/update";
        public const string Delete = "http://EAppointments.BMS.ServiceContracts/2007/08/Security/claims/delete";
    }

    public class AppointmentClaimTypes : GenericClaimTypes
    {
        public const string Book = "http://EAppointments.BMS.ServiceContracts/2007/08/Security/appointment/claims/book";
        public const string Cancel = "http://EAppointments.BMS.ServiceContracts/2007/08/Security/appointment/claims/cancel";
        public const string Rebook = "http://EAppointments.BMS.ServiceContracts/2007/08/Security/appointment/claims/rebook";
        public const string Approve = "http://EAppointments.BMS.ServiceContracts/2007/08/Security/appointment/claims/approve";
        public const string Reject = "http://EAppointments.BMS.ServiceContracts/2007/08/Security/appointment/claims/reject";
    }

    public partial class ClaimsAuthorizationPolicy : IAuthorizationPolicy
    {
        public const string IssuerName = "http://EAppointments.BMS.ServiceContracts/2007/08/Security/issuer";

        private Guid _id;
        private ClaimSet _issuer;

        public ClaimsAuthorizationPolicy()
        {
            _id = Guid.NewGuid();
            _issuer = new DefaultClaimSet(Claim.CreateNameClaim(ClaimsAuthorizationPolicy.IssuerName));
        }

        #region IAuthorizationPolicy Members

        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            if (!evaluationContext.Properties.ContainsKey("Identities"))
                return false;

            List<IIdentity> identities = evaluationContext.Properties["Identities"] as List<IIdentity>;
            IIdentity identity = identities[0];
            IPrincipal principal = Session.GetCustomPrincipal(identity);
            
            evaluationContext.Properties["Principal"] = principal;
            
            ClaimSet claims = MapClaims(identity, principal);

            if (claims != null)
                evaluationContext.AddClaimSet(this, claims);

            return true;
        }


        protected virtual ClaimSet MapClaims(IIdentity identity, IPrincipal principal)
        {
            List<Claim> listClaims = new List<Claim>();

            if (!identity.IsAuthenticated)
                throw new SecurityException("User not authenticated.");

            if (principal.IsInRole(RoleType.BmsAdmin.ToString()))
            {
                listClaims.Add(new Claim(AppointmentClaimTypes.Create, Resources.Appointment, Rights.PossessProperty));
                listClaims.Add(new Claim(AppointmentClaimTypes.Update, Resources.Appointment, Rights.PossessProperty));
                listClaims.Add(new Claim(AppointmentClaimTypes.Read, Resources.Appointment, Rights.PossessProperty));
                listClaims.Add(new Claim(AppointmentClaimTypes.Delete, Resources.Appointment, Rights.PossessProperty));
                listClaims.Add(new Claim(AppointmentClaimTypes.Book, Resources.Appointment, Rights.PossessProperty));
                listClaims.Add(new Claim(AppointmentClaimTypes.Rebook, Resources.Appointment, Rights.PossessProperty));
                listClaims.Add(new Claim(AppointmentClaimTypes.Cancel, Resources.Appointment, Rights.PossessProperty));

                listClaims.Add(new Claim(GenericClaimTypes.Read, Resources.Directory, Rights.PossessProperty));
                listClaims.Add(new Claim(GenericClaimTypes.Create, Resources.Directory, Rights.PossessProperty));
                listClaims.Add(new Claim(GenericClaimTypes.Update, Resources.Directory, Rights.PossessProperty));
                listClaims.Add(new Claim(GenericClaimTypes.Delete, Resources.Directory, Rights.PossessProperty));

                listClaims.Add(new Claim(GenericClaimTypes.Read, Resources.Provider, Rights.PossessProperty));
                listClaims.Add(new Claim(GenericClaimTypes.Create, Resources.Provider, Rights.PossessProperty));
                listClaims.Add(new Claim(GenericClaimTypes.Update, Resources.Provider, Rights.PossessProperty));
                listClaims.Add(new Claim(GenericClaimTypes.Delete, Resources.Provider, Rights.PossessProperty));
            }
            else if (principal.IsInRole(RoleType.Patient.ToString()))
            {
                listClaims.Add(new Claim(AppointmentClaimTypes.Read, Resources.Appointment, Rights.PossessProperty));
                listClaims.Add(new Claim(AppointmentClaimTypes.Book, Resources.Appointment, Rights.PossessProperty));
                listClaims.Add(new Claim(AppointmentClaimTypes.Rebook, Resources.Appointment, Rights.PossessProperty));
                listClaims.Add(new Claim(AppointmentClaimTypes.Cancel, Resources.Appointment, Rights.PossessProperty));

                listClaims.Add(new Claim(GenericClaimTypes.Read, Resources.Directory, Rights.PossessProperty));
                listClaims.Add(new Claim(GenericClaimTypes.Read, Resources.Provider, Rights.PossessProperty));
            }
            else if (principal.IsInRole(RoleType.Referrer.ToString()))
            {
                listClaims.Add(new Claim(AppointmentClaimTypes.Create, Resources.Appointment, Rights.PossessProperty));
                listClaims.Add(new Claim(AppointmentClaimTypes.Update, Resources.Appointment, Rights.PossessProperty));
                listClaims.Add(new Claim(AppointmentClaimTypes.Read, Resources.Appointment, Rights.PossessProperty));
                listClaims.Add(new Claim(AppointmentClaimTypes.Delete, Resources.Appointment, Rights.PossessProperty));
                listClaims.Add(new Claim(AppointmentClaimTypes.Book, Resources.Appointment, Rights.PossessProperty));
                listClaims.Add(new Claim(AppointmentClaimTypes.Rebook, Resources.Appointment, Rights.PossessProperty));
                listClaims.Add(new Claim(AppointmentClaimTypes.Cancel, Resources.Appointment, Rights.PossessProperty));

                listClaims.Add(new Claim(GenericClaimTypes.Read, Resources.Directory, Rights.PossessProperty));
                listClaims.Add(new Claim(GenericClaimTypes.Read, Resources.Provider, Rights.PossessProperty));
            }
            else if (principal.IsInRole(RoleType.ProviderClinician.ToString()))
            {
                listClaims.Add(new Claim(AppointmentClaimTypes.Read, Resources.Appointment, Rights.PossessProperty));
                listClaims.Add(new Claim(AppointmentClaimTypes.Approve, Resources.Appointment, Rights.PossessProperty));
                listClaims.Add(new Claim(AppointmentClaimTypes.Reject, Resources.Appointment, Rights.PossessProperty));

                listClaims.Add(new Claim(GenericClaimTypes.Read, Resources.Directory, Rights.PossessProperty));
                listClaims.Add(new Claim(GenericClaimTypes.Read, Resources.Provider, Rights.PossessProperty));
            }


            return new DefaultClaimSet(this._issuer, listClaims);
        }

        public ClaimSet Issuer
        {
            get { return _issuer; }
        }

        #endregion

        #region IAuthorizationComponent Members

        public string Id
        {
            get
            {
                return _id.ToString();
            }
        }

        #endregion
    }

    public partial class ClaimsAuthorizationPolicy
    {

        protected static List<string> _supportedClaimTypes = new List<string>();
        protected static List<string> _supportedResources = new List<string>();

        static ClaimsAuthorizationPolicy()
        {
            _supportedClaimTypes.Add(GenericClaimTypes.Create);
            _supportedClaimTypes.Add(GenericClaimTypes.Read);
            _supportedClaimTypes.Add(GenericClaimTypes.Update);
            _supportedClaimTypes.Add(GenericClaimTypes.Delete);

            _supportedClaimTypes.Add(AppointmentClaimTypes.Book);
            _supportedClaimTypes.Add(AppointmentClaimTypes.Cancel);
            _supportedClaimTypes.Add(AppointmentClaimTypes.Rebook);
            _supportedClaimTypes.Add(AppointmentClaimTypes.Approve);
            _supportedClaimTypes.Add(AppointmentClaimTypes.Reject);

            _supportedResources.Add(Resources.Appointment);
            _supportedResources.Add(Resources.Directory);
            _supportedResources.Add(Resources.Provider);
        }

        private static bool IsValidClaimType(string s)
        {
            return _supportedClaimTypes.Contains(s);
        }

        private static bool IsValidResource(string s)
        {
            return _supportedResources.Contains(s);
        }

        public static ClaimSet CreateIssuerClaimSet()
        {
            return new DefaultClaimSet(Claim.CreateUriClaim(new Uri(ClaimsAuthorizationPolicy.IssuerName)), Claim.CreateDnsClaim(ClaimsAuthorizationPolicy.IssuerName), Claim.CreateNameClaim(ClaimsAuthorizationPolicy.IssuerName));
        }

        public static ClaimSet CreateClaimSet(string resource, string claimType)
        {
            List<Claim> claims = new List<Claim>();

            if (!IsValidResource(resource))
                throw new SecurityException(string.Format("Resource not supported by ClaimsAuthorizationPolicy: {0}", resource));

            if (!IsValidClaimType(claimType))
                throw new SecurityException(string.Format("Claim type not supported by ClaimsAuthorizationPolicy: {0}", claimType));

            claims.Add(new Claim(claimType, resource, Rights.PossessProperty));

            return new DefaultClaimSet(ClaimsAuthorizationPolicy.CreateIssuerClaimSet(), claims);
        }

    }
}
