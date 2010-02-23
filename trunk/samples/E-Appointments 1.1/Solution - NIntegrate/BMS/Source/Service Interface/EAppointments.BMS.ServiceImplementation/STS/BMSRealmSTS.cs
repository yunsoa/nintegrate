using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens;
using System.ServiceModel;
using System.IdentityModel.Claims;
using Microsoft.ServiceModel.Samples.Federation;

namespace EAppointments.BMS.Services.STS
{
    public class BMSRealmSTS : SecurityTokenService
    {
        public BMSRealmSTS()
            :
            base(ServiceConstants.StsName,
                 FederationUtilities.GetX509TokenFromCert(ServiceConstants.CertStoreName, ServiceConstants.CertStoreLocation, ServiceConstants.CertDistinguishedName),
                 FederationUtilities.GetX509TokenFromCert(ServiceConstants.CertStoreName, ServiceConstants.CertStoreLocation, ServiceConstants.TargetDistinguishedName))
        {
        }

        /// <summary>
        /// Overrides the GetIssuedClaims from the SecurityTokenService Base Class
        /// to return a valid user claim for the user
        /// </summary>
        protected override Collection<SamlAttribute> GetIssuedClaims(RequestSecurityToken requestSecurityToken)
        {
            string caller = ServiceSecurityContext.Current.PrimaryIdentity.Name;

            // Create Name claim
            Collection<SamlAttribute> samlAttributes = new Collection<SamlAttribute>();
            samlAttributes.Add(new SamlAttribute(Claim.CreateNameClaim(caller)));
            return samlAttributes;
        }
    }
}
