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

namespace EAppointments.BMS.ServiceImplementation
{
    public static class ProviderTranslator
    {
        internal static DataTypes.Provider TranslateBusinessToService(BMS.Provider from)
        {
            DataTypes.Provider to = new DataTypes.Provider();
            to.Id = from.Id;
            to.Name = from.Name;

            to.Latitude = from.Latitude;
            to.Longitude = from.Longitude;
            to.Organization = from.Organization;
            to.Email = from.Email;

            to.AlternativeServices = from.AlternativeServices;
            to.ConditionsTreated = from.ConditionsTreated;
            to.Exclusions = from.Exclusions;
            to.ProceduresPerformed = from.ProceduresPerformed;
            to.Proximity = from.Proximity;

            return to;
        }

        internal static BMS.Provider TranslateServiceToBusiness(DataTypes.Provider from, BMS.Provider to)
        {
            to.Name = from.Name;

            to.Latitude = from.Latitude;
            to.Longitude = from.Longitude;
            to.Organization = from.Organization;
            to.Email = from.Email;

            to.AlternativeServices = from.AlternativeServices;
            to.ConditionsTreated = from.ConditionsTreated;
            to.Exclusions = from.Exclusions;
            to.ProceduresPerformed = from.ProceduresPerformed;

            return to;
        }

        internal static BMS.ProviderSearchCriteria TranslateSearchCriteria(DataTypes.ProviderSearchCriteria from)
        {
            BMS.ProviderSearchCriteria to = new BMS.ProviderSearchCriteria(from.Id);

            to.ClinicTypeId = from.ClinicTypeId;
            to.SpecialtyId = from.SpecialtyId;
            to.WithinMiles = from.WithinMiles;
            to.ZipCode = from.ZipCode;
            to.Keywords = from.KeyWords;
          
            return to;
        }
    }
}
