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
    public class ProviderService : IProviderService
    {
        [ClaimsPrincipalPermissionAttribute(SecurityAction.Demand, RequiredClaimType = GenericClaimTypes.Read, Resource = Resources.Provider)]
        public ProviderCollection FindProvider(DataTypes.ProviderSearchCriteria criteria)
        {
            if (criteria == null)
                throw new ArgumentNullException("criteria");
            
            BMS.ProviderSearchCriteria businessCriteria = ProviderTranslator.TranslateSearchCriteria(criteria);

            List<BMS.Provider> businessProviders = new List<BMS.Provider>(Helper.GetApplication().Find(businessCriteria));

            return new ProviderCollection(businessProviders.ConvertAll<DataTypes.Provider>(
                new Converter<BMS.Provider, DataTypes.Provider>(ProviderTranslator.TranslateBusinessToService)
                ));
        }

        [ClaimsPrincipalPermissionAttribute(SecurityAction.Demand, RequiredClaimType = GenericClaimTypes.Read, Resource = Resources.Provider)]
        public SlotCollection FindSlot(DataTypes.SlotSearchCriteria criteria)
        {
            if (criteria == null)
                throw new ArgumentNullException("criteria");

            BMS.SlotSearchCriteria businessCriteria = SlotTranslator.TranslateSearchCriteria(criteria);

            List<BMS.Slot> businessSlots = new List<BMS.Slot>(Helper.GetApplication().Find(businessCriteria));

            return new SlotCollection(businessSlots.ConvertAll<DataTypes.Slot>(
                new Converter<BMS.Slot, DataTypes.Slot>(SlotTranslator.TranslateBusinessToService)
                ));
        }

        [ClaimsPrincipalPermissionAttribute(SecurityAction.Demand, RequiredClaimType = GenericClaimTypes.Create, Resource = Resources.Provider)]
        public EAppointments.BMS.DataTypes.Provider Create(DataTypes.Provider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            BMS.Provider businessProvider = Helper.GetApplication().NewProvider();

            businessProvider = ProviderTranslator.TranslateServiceToBusiness(provider, businessProvider);

            businessProvider.Save();

            return ProviderTranslator.TranslateBusinessToService(businessProvider);
        }

        [ClaimsPrincipalPermissionAttribute(SecurityAction.Demand, RequiredClaimType = GenericClaimTypes.Update, Resource = Resources.Provider)]
        public EAppointments.BMS.DataTypes.Provider Update(DataTypes.Provider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            BMS.Provider businessProvider = Helper.GetApplication().FindProviderById(provider.Id);
            
            businessProvider = ProviderTranslator.TranslateServiceToBusiness(provider, businessProvider);

            businessProvider.Save();

            return ProviderTranslator.TranslateBusinessToService(businessProvider);
        }

        [ClaimsPrincipalPermissionAttribute(SecurityAction.Demand, RequiredClaimType = GenericClaimTypes.Delete, Resource = Resources.Provider)]
        public void Delete(DataTypes.Provider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            BMS.Provider businessProvider = Helper.GetApplication().FindProviderById(provider.Id);

            businessProvider.Delete();

        }
    }
}
