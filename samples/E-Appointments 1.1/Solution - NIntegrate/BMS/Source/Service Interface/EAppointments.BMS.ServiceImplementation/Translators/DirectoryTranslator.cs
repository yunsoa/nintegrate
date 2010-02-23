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
using EAppointments.BMS.DataTypes;
using EAppointments.BMS;

namespace EAppointments.BMS.ServiceImplementation
{
    public static class DirectoryTranslator
    {
        internal static DataTypes.Patient TranslateBusinessToService(BMS.Patient from)
        {
            DataTypes.Patient to = new DataTypes.Patient();

            to.Id = from.Id;
            to.PatientNo = from.PatientNo;
            to.Referrer = TranslateBusinessToService(from.ReferringClinician);
            to.Title = from.Title;
            to.FirstName = from.FirstName;
            to.LastName = from.LastName;
            to.AddressLine1 = from.AddressLine1;
            to.AddressLine2 = from.AddressLine2;
            to.City = from.City;
            to.State = from.State;
            to.ZipCode = from.ZipCode;
            to.Country = from.Country;
            to.ContactNumber = from.ContactNumber;
            to.DateOfBirth = from.DateOfBirth;
            to.Gender = from.Gender;
            to.Email = from.Email;

            return to;
        }

        internal static DataTypes.Referrer TranslateBusinessToService(BMS.Referrer from)
        {
            DataTypes.Referrer to = new DataTypes.Referrer();

            to.Id = from.Id;
            to.FirstName = from.FirstName;
            to.LastName = from.LastName;
            to.ClinicName = from.ClinicName;
            to.Email = from.Email;

            return to;
        }

        internal static DataTypes.ClinicType TranslateBusinessToService(BMS.ClinicType from)
        {
            DataTypes.ClinicType to = new EAppointments.BMS.DataTypes.ClinicType();

            to.Id = from.Id;
            to.Name = from.Name;
            to.Specialty = TranslateBusinessToService(from.Parent);

            return to;

        }

        internal static DataTypes.Specialty TranslateBusinessToService(BMS.Specialty from)
        {
            DataTypes.Specialty to = new EAppointments.BMS.DataTypes.Specialty();
            to.Id = from.Id;
            to.Name = from.Name;

            return to;
        }

        internal static BMS.ReferrerSearchCriteria TranslateSearchCriteria(DataTypes.ReferrerSearchCriteria from)
        {
            BMS.ReferrerSearchCriteria to = new BMS.ReferrerSearchCriteria(from.Id);

            to.FirstName = from.FirstName;
            to.LastName = from.LastName;

            return to;
        }

        internal static BMS.PatientSearchCriteria TranslateSearchCriteria(DataTypes.PatientSearchCriteria from)
        {
            BMS.PatientSearchCriteria to = new BMS.PatientSearchCriteria(from.Id, from.ReferrerId);

            to.FirstName = from.FirstName;
            to.LastName = from.LastName;

            return to;
        }
    }
}
