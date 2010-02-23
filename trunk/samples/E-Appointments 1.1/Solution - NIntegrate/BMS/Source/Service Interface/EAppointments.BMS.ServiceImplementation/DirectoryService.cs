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
using System.Security.Permissions;
using EAppointments.BMS;
using EAppointments.BMS.DataTypes;
using EAppointments.BMS.ServiceContracts;
using EAppointments.BMS.ServiceImplementation.Security;

namespace EAppointments.BMS.ServiceImplementation
{
    public class DirectoryService : IDirectoryService
    {
        [ClaimsPrincipalPermissionAttribute(SecurityAction.Demand, RequiredClaimType = GenericClaimTypes.Read, Resource = Resources.Directory)]
        public PatientCollection FindPatient(DataTypes.PatientSearchCriteria criteria)
        {
            if (criteria == null)
                throw new ArgumentNullException("criteria");
            
            BMS.PatientSearchCriteria businessCriteria = DirectoryTranslator.TranslateSearchCriteria(criteria);

            List<BMS.Patient> businessPatients = new List<BMS.Patient>(Helper.GetApplication().Find(businessCriteria));

            return new PatientCollection(businessPatients.ConvertAll<DataTypes.Patient>(
                new Converter<BMS.Patient, DataTypes.Patient>(DirectoryTranslator.TranslateBusinessToService)
               ));
        }

        [ClaimsPrincipalPermissionAttribute(SecurityAction.Demand, RequiredClaimType = GenericClaimTypes.Read, Resource = Resources.Directory)]
        public ReferrerCollection FindReferrer(DataTypes.ReferrerSearchCriteria criteria)
        {
            if (criteria == null)
                throw new ArgumentNullException("criteria");
            
            BMS.ReferrerSearchCriteria businessCriteria = DirectoryTranslator.TranslateSearchCriteria(criteria);

            List<BMS.Referrer> businessReferrers = new List<BMS.Referrer>(Helper.GetApplication().Find(businessCriteria));

            return new ReferrerCollection(businessReferrers.ConvertAll<DataTypes.Referrer>(
                new Converter<BMS.Referrer, DataTypes.Referrer>(DirectoryTranslator.TranslateBusinessToService)
            ));
        }

        [ClaimsPrincipalPermissionAttribute(SecurityAction.Demand, RequiredClaimType = GenericClaimTypes.Read, Resource = Resources.Directory)]
        public SpecialtyCollection FindSpecialty()
        {
            List<BMS.Specialty> businessSpecialties = new List<BMS.Specialty>(Helper.GetApplication().FindSpecialties());

            return new SpecialtyCollection(businessSpecialties.ConvertAll<DataTypes.Specialty>(
                 new Converter<BMS.Specialty, DataTypes.Specialty>(DirectoryTranslator.TranslateBusinessToService)
            ));
        }

        [ClaimsPrincipalPermissionAttribute(SecurityAction.Demand, RequiredClaimType = GenericClaimTypes.Read, Resource = Resources.Directory)]
        public ClinicTypeCollection FindClinicType(Guid specialtyId)
        {
            List<BMS.ClinicType> businessClinicTypes = new List<ClinicType>(Helper.GetApplication().Find(new BMS.ClinicTypeSearchCriteria(null, specialtyId)));

            return new ClinicTypeCollection(businessClinicTypes.ConvertAll<DataTypes.ClinicType>(
                new Converter<BMS.ClinicType, DataTypes.ClinicType>(DirectoryTranslator.TranslateBusinessToService)
            ));
        }
    }
}
