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
using EAppointments.UI.ServiceAgents.DirectoryService;
using EAppointments.UI.ServiceAgents.Interfaces;
using NIntegrateExtensions;
using NIntegrate.ServiceModel;

namespace EAppointments.UI.ServiceAgents
{
    public class DirectoryServiceAgent : IDirectoryServiceAgent
    {
        private WcfChannelWrapper<IDirectoryService> GetProxy()
        {
            return EAWcfClientChannelFactory.CreateWcfChannel<IDirectoryService>();
        }

        #region IDirectoryServiceAgent Members

        public Patient[] Find(PatientSearchCriteria criteria)
        {
            using (var proxy = GetProxy())
            {
                return proxy.Channel.FindPatient(criteria).ToArray();
            }
        }

        public Referrer[] Find(ReferrerSearchCriteria criteria)
        {
            using (var proxy = GetProxy())
            {
                return proxy.Channel.FindReferrer(criteria).ToArray();
            }
        }

        public Specialty[] FindSpecialty()
        {
            using (var proxy = GetProxy())
            {
                return proxy.Channel.FindSpecialty().ToArray();
            }
        }

        public ClinicType[] FindClinicType(Guid specialtyId)
        {
            using (var proxy = GetProxy())
            {
                return proxy.Channel.FindClinicType(specialtyId).ToArray();
            }
        }

        #endregion
    }
}
