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
using Microsoft.Practices.ObjectBuilder;
using EAppointments.UI.ServiceAgents.ProviderService;
using Microsoft.Practices.CompositeWeb;

namespace EAppointments.UI.Modules.Views
{
    public class ProviderListViewPresenter: Presenter<IProviderListView>
    {
        private ProviderController _controller;

        public ProviderListViewPresenter([CreateNew] ProviderController controller)
        {
            _controller = controller;
        }

        public override void OnViewLoaded()
        {
            ProviderSearchCriteria criteria = new ProviderSearchCriteria();
            criteria.ClinicTypesId = View.ClinicTypeId;
            criteria.KeyWords = View.Keywords;
            if (View.WithinMiles.HasValue)
            {
                criteria.WithinMiles = (double)View.WithinMiles;
                criteria.ZipCode = View.ZipCode;
            }
            View.Providers = _controller.Find(criteria);
        }
    }
}

