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
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using Microsoft.Practices.CompositeWeb.Utility;
using EAppointments.UI.ServiceAgents;

namespace EAppointments.UI.Modules
{
    public class DirectoryServicesController
    {
        private IDirectoryServiceAgent _directoryServiceAgent;

        [InjectionConstructor]
        public DirectoryServicesController
            (
            [ServiceDependency] IDirectoryServiceAgent directoryServiceAgent
            )
        {
            _directoryServiceAgent = directoryServiceAgent;
        }		

        public Patient[] Find(PatientSearchCriteria criteria)
        {
            List<Patient> patients = new List<Patient>(_directoryServiceAgent.Find(criteria));
            return OutputValidationUtility.Encode<Patient>(patients).ToArray();
        }

        public Patient FindPatientById(Guid patientId)
        {
            PatientSearchCriteria criteria = new PatientSearchCriteria();
            criteria.Id = patientId;

            List<Patient> patients = new List<Patient>(_directoryServiceAgent.Find(criteria));

            return OutputValidationUtility.Encode<Patient>(patients[0]);
        }

        public Referrer[] Find(ReferrerSearchCriteria criteria)
        {
            List<Referrer> referrers = new List<Referrer>(_directoryServiceAgent.Find(criteria));
            return OutputValidationUtility.Encode<Referrer>(referrers).ToArray();
        }

        public static Specialty[] FindSpecialties()
        {
            List<Specialty> specialties = new List<Specialty>( new DirectoryServiceAgent().FindSpecialty());
            return OutputValidationUtility.Encode<Specialty>(specialties).ToArray();
        }

        public static ClinicType[] FindClinicTypes(Guid specialtyId)
        {
            List<ClinicType> clinicTypes = new List<ClinicType>(new DirectoryServiceAgent().FindClinicType(specialtyId));
            return OutputValidationUtility.Encode<ClinicType>(clinicTypes).ToArray();
        }
    }
}
