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
using EAppointments.UI.ServiceAgents.ProviderService;
using EAppointments.UI.ServiceAgents.Interfaces;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using Microsoft.Practices.CompositeWeb.Utility;
using EAppointments.UI.ServiceAgents;

namespace EAppointments.UI.Modules
{
    public class ProviderController
    {
       private IProviderServiceAgent _providerServiceAgent;

        [InjectionConstructor]
        public ProviderController
            (
            [ServiceDependency] IProviderServiceAgent providerServiceAgent
            )
        {
            _providerServiceAgent = providerServiceAgent;
        }		

        internal Provider[] Find(ProviderSearchCriteria criteria)
        {
            List<Provider> providers = new List<Provider>(_providerServiceAgent.Find(criteria));            
            return OutputValidationUtility.Encode<Provider>(providers).ToArray();
        }

        internal Slot[] Find(SlotSearchCriteria criteria)
        {
            List<Slot> slots = new List<Slot>(_providerServiceAgent.Find(criteria));
            return OutputValidationUtility.Encode<Slot>(slots).ToArray();
        }


        internal Provider FindProviderById(Guid providerId)
        {
            ProviderSearchCriteria criteria = new ProviderSearchCriteria();
            criteria.Id = providerId;
            List<Provider> providers = new List<Provider>(_providerServiceAgent.Find(criteria));
            return OutputValidationUtility.Encode<Provider>(providers[0]);
        }

        internal Slot FindSlotById(Guid slotId)
        {
            SlotSearchCriteria criteria = new SlotSearchCriteria();
            criteria.Id = slotId;
            List<Slot> slots = new List<Slot>(_providerServiceAgent.Find(criteria));
            return OutputValidationUtility.Encode<Slot>(slots[0]);
        }

        public static Provider[] FindProvider(Guid clinicTypeId, string keywords, double? withinMiles, string zipCode)
        {
            ProviderSearchCriteria criteria = new ProviderSearchCriteria();
            criteria.ClinicTypesId = clinicTypeId;
            criteria.KeyWords = keywords;
            criteria.WithinMiles = (double)withinMiles;
            criteria.ZipCode = zipCode;            
            List<Provider> providers = new List<Provider>(new ProviderServiceAgent().Find(criteria));
            return OutputValidationUtility.Encode<Provider>(providers).ToArray();
        }
    }
}
