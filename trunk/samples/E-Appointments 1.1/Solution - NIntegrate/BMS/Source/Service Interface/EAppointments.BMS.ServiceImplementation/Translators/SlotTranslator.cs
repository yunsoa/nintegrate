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
    public static class SlotTranslator
    {
        internal static DataTypes.Slot TranslateBusinessToService(BMS.Slot from)
        {
            DataTypes.Slot to = new DataTypes.Slot();

            to.Id = from.Id;
            to.Provider = ProviderTranslator.TranslateBusinessToService(from.Provider);
            to.ClinicType = DirectoryTranslator.TranslateBusinessToService(from.ClinicType);
            to.StartDateTime = from.StartDateTime;
            to.EndDateTime = from.EndDateTime;
            to.Ubrn = from.Ubrn;
            to.Status = (EAppointments.BMS.DataTypes.SlotStatus)from.Status;

            return to;
        }

        internal static BMS.SlotSearchCriteria TranslateSearchCriteria(DataTypes.SlotSearchCriteria from)
        {
            BMS.SlotSearchCriteria to = new BMS.SlotSearchCriteria(from.Id);

            to.ProviderId = from.ProviderId;
            to.SpecialtyId = from.SpecialtyId;
            to.ClinicTypeId = from.ClinicTypeId;
            to.StartDateTime = from.StartDateTime;
            to.EndDateTime = from.EndDateTime;            
            to.WeekDays = from.WeekDays;
            if (from.Status.HasValue)
                to.Status = (BMS.SlotStatus)from.Status;

            return to;
        }
    }
}
