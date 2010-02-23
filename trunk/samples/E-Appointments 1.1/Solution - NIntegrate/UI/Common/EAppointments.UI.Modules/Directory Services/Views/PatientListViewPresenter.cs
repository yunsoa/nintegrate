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
using Microsoft.Practices.CompositeWeb;
using EAppointments.UI.ServiceAgents.DirectoryService;

namespace EAppointments.UI.Modules.Views
{
    public class PatientListViewPresenter : Presenter<IPatientListView>
    {
        private DirectoryServicesController _controller;

        public PatientListViewPresenter([CreateNew] DirectoryServicesController controller)
        {
            _controller = controller;
        }

        public override void OnViewLoaded()
        {
            PatientSearchCriteria criteria = new PatientSearchCriteria();
            criteria.FirstName = View.FirstName;
            criteria.LastName = View.LastName;
            View.Patients = _controller.Find(criteria);
        }
    }
}
