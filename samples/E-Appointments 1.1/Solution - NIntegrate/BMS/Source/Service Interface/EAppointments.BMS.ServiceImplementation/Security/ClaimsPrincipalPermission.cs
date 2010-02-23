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
using System.Security;
using System.Threading;
using System.IdentityModel.Claims;
using System.Security.Permissions;
using System.Security.Principal;
using System.IdentityModel.Policy;
using System.ServiceModel;
using System.ServiceModel.Security;

namespace EAppointments.BMS.ServiceImplementation.Security
{
    public class ClaimsPrincipalPermission : IPermission, IUnrestrictedPermission
    {
        private ClaimSet _requiredClaims;
        private bool _isAuthenticated;
        private bool _isUnrestricted;

        public bool IsAuthenticated
        {
            get { return _isAuthenticated; }
        }

        public ClaimSet Issuer
        {
            get { return ((this._requiredClaims == null) ? null : this._requiredClaims.Issuer); }
        }

        public ClaimSet RequiredClaims
        {
            get { return this._requiredClaims; }
        }

        public ClaimsPrincipalPermission(PermissionState state)
        {
            this._isUnrestricted = (state == PermissionState.Unrestricted);
        }

        public ClaimsPrincipalPermission(bool isAuthenticated, ClaimSet requiredClaims)
        {
            this._isAuthenticated = isAuthenticated;
            this._requiredClaims = requiredClaims;
        }

        public override bool Equals(object obj)
        {
            IPermission p = obj as IPermission;

            if (obj != null && p == null)
            {
                return false;
            }
            if (!this.IsSubsetOf(p))
            {
                return false;
            }
            if (p != null && !p.IsSubsetOf(this))
            {
                return false;
            }
            return true;

        }

        public override int GetHashCode()
        {
            int hashCode = this._isAuthenticated.GetHashCode();
            hashCode += this._requiredClaims.GetHashCode();
            hashCode += this._isUnrestricted.GetHashCode();

            return hashCode;
        }

        #region IPermission Members

        /// <summary>
        /// Make a copy of this permission and return it.
        /// </summary>
        /// <returns></returns>
        public IPermission Copy()
        {
            if (this._isUnrestricted)
                return new ClaimsPrincipalPermission(PermissionState.Unrestricted);
            else
                return new ClaimsPrincipalPermission(this._isAuthenticated, this._requiredClaims);
        }

        /// <summary>
        /// If IsAuthenticated was set on the permission, 
        ///   check the thread's principal is authenticated
        /// If Claims WERE NOT provided, don't check them
        /// If Claims WERE provided, check the AuthorizationContext
        ///   check for an issuer match, 
        ///   check that required claims are present
        /// </summary>        
        public void CheckClaims()
        {
            if (this._requiredClaims == null)
                return;
            
            AuthorizationContext authContext = ServiceSecurityContext.Current.AuthorizationContext;

            ClaimSet issuerClaimSet = null;
            foreach (ClaimSet claimSet in authContext.ClaimSets)
            {
                Claim issuerClaim = Claim.CreateNameClaim(ClaimsAuthorizationPolicy.IssuerName);

                if (claimSet.Issuer.ContainsClaim(issuerClaim))
                    issuerClaimSet = claimSet;
            }

            if (issuerClaimSet == null)
                throw new SecurityAccessDeniedException("Access is denied. No claims were provided from the expected issuer.");

            bool hasClaims = true;

            foreach (Claim claim in _requiredClaims)
            {
                if (!issuerClaimSet.ContainsClaim(claim))
                {
                    hasClaims = false;
                    break;
                }
            }

            if (!hasClaims)
                throw new SecurityAccessDeniedException("Access is denied. Security principal does not satisfy required claims.");
        }


        /// <summary>
        /// Thrown a SecurityException if the permissions required are
        /// not satisfied by the current thread's principal.
        /// </summary>
        public void Demand()
        {
            //this.CheckClaims();
        }

        /// <summary>
        /// Return a new permission with the intersection of claims.
        /// Issuer must be an exact match.
        /// Intersecting claims are added to a new ClaimSet by the same issuer.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public IPermission Intersect(IPermission target)
        {
            if (target == null)
                return null;

            ClaimsPrincipalPermission perm = target as ClaimsPrincipalPermission;
            if (perm == null)
                return null;

            if (this.IsUnrestricted())
                return target.Copy();

            if (perm.IsUnrestricted())
                return this.Copy();

            if (this._isAuthenticated != perm.IsAuthenticated)
                return null;

            if (!IsExactIssuerMatch(perm.Issuer)) return null;

            List<Claim> claims = new List<Claim>();
            foreach (Claim c in this._requiredClaims)
            {
                if (perm.RequiredClaims.ContainsClaim(c))
                {
                    claims.Add(c);
                }

            }

            // it is assumed that the issuers are identical from the call
            // to IsExactIssuerMatch() above
            ClaimsPrincipalPermission newPerm = new ClaimsPrincipalPermission(this._isAuthenticated, new DefaultClaimSet(this._requiredClaims.Issuer, claims));
            return newPerm;

        }

        /// <summary>
        /// It is important that when performing a check that two permissions
        /// are a subset, or to perform union or intersection of two permissions
        /// that the issuers provided are an exact match by claimset 
        /// to reduce confusion.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        protected bool IsExactIssuerMatch(ClaimSet target)
        {
            bool issuerMatch = true;
            foreach (Claim c in target)
            {
                if (!this._requiredClaims.Issuer.ContainsClaim(c))
                {
                    issuerMatch = false;
                    break;
                }
            }

            return issuerMatch;
        }

        /// <summary>
        /// Is the permission provided a subset of this permission?
        /// Issuer must be an exact match.
        /// Claims in this permission must all be contained in target.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool IsSubsetOf(IPermission target)
        {

            if (target == null)
                return false;

            ClaimsPrincipalPermission perm = target as ClaimsPrincipalPermission;
            if (perm == null)
                return false;

            if (perm.IsUnrestricted())
                return true;

            if (this.IsUnrestricted())
                return false;

            if (this._isAuthenticated != perm.IsAuthenticated)
                return false;

            if (!IsExactIssuerMatch(perm.Issuer)) return false;

            bool isSubsetOf = false;
            foreach (Claim c in this._requiredClaims)
            {
                if (!perm.RequiredClaims.ContainsClaim(c))
                {
                    isSubsetOf = false;
                    break;
                }

            }

            return isSubsetOf;

        }

        /// <summary>
        /// Return a new permission with the union of this and the permission 
        /// provided.
        /// IsAuthenticated must match.
        /// Issuer must be an exact match.
        /// All claims added to a new ClaimSet with the same Issuer.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public IPermission Union(IPermission target)
        {
            if (target == null)
                return null;

            ClaimsPrincipalPermission perm = target as ClaimsPrincipalPermission;
            if (perm == null)
                return null;

            if (perm.IsUnrestricted() || this.IsUnrestricted())
                return new ClaimsPrincipalPermission(PermissionState.Unrestricted);

            if (this._isAuthenticated != perm.IsAuthenticated)
                return null;

            if (!IsExactIssuerMatch(perm.Issuer)) return null;

            List<Claim> claims = new List<Claim>();
            foreach (Claim c in this._requiredClaims)
                claims.Add(c);

            foreach (Claim c in perm.RequiredClaims)
            {
                if (!this._requiredClaims.ContainsClaim(c))
                {
                    claims.Add(c);
                }
            }

            // it is assumed that the issuers are identical from the call
            // to IsExactIssuerMatch() above
            ClaimsPrincipalPermission newPerm = new ClaimsPrincipalPermission(this._isAuthenticated, new DefaultClaimSet(this._requiredClaims.Issuer, claims));
            return newPerm;

        }

        #endregion

        #region ISecurityEncodable Members

        public void FromXml(SecurityElement e)
        {
            throw new NotSupportedException("ClaimsPrincipalPermission cannot be loaded from XML.");
        }

        public SecurityElement ToXml()
        {
            throw new NotSupportedException("ClaimsPrincipalPermission cannot be saved to XML.");
        }

        #endregion

        #region IUnrestrictedPermission Members

        public bool IsUnrestricted()
        {
            return this._isUnrestricted;
        }

        #endregion
    }
}
