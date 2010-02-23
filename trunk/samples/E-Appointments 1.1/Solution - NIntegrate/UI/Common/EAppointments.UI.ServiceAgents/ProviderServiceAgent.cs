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
using EAppointments.UI.ServiceAgents.Interfaces;
using EAppointments.UI.ServiceAgents.ProviderService;
using NIntegrate.ServiceModel;
using NIntegrateExtensions;

namespace EAppointments.UI.ServiceAgents
{
    public class ProviderServiceAgent : IProviderServiceAgent
    {
        private WcfChannelWrapper<IProviderService> GetProxy()
        {
            return EAWcfClientChannelFactory.CreateWcfChannel<IProviderService>();
        }

        #region IProviderServiceAgent Members

        public Provider[] Find(ProviderSearchCriteria criteria)
        {
            using (var proxy = GetProxy())
            {
                return proxy.Channel.FindProvider(criteria).ToArray();
            }
        }

        public Slot[] Find(SlotSearchCriteria criteria)
        {
            using (var proxy = GetProxy())
            {
                return proxy.Channel.FindSlot(criteria).ToArray();
            }
        }


        public Slot[] Slot(SlotSearchCriteria criteria)
        {
            using (var proxy = GetProxy())
            {
                return proxy.Channel.FindSlot(criteria).ToArray();
            }
        }

        public Provider Create(Provider provider)
        {
            using (var proxy = GetProxy())
            {
                return proxy.Channel.Create(provider);
            }
        }

        public void Delete(Provider provider)
        {
            using (var proxy = GetProxy())
            {
                proxy.Channel.Delete(provider);
            }
        }

        #endregion
    }
}
