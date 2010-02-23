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
using EAppointments.UI.Modules.Services;
using EAppointments.UI.Modules.Constants;
using EAppointments.UI.ServiceAgents.AppointmentService;

namespace EAppointments.UI.Modules.Views
{
    public class SelectPatientViewPresenter : Presenter<ISelectPatientView>
    {
        private DirectoryServicesController _controller;
        private IStateProvider _state;
        private INavigationService _navigator;

        public SelectPatientViewPresenter(           
            [CreateNew] DirectoryServicesController controller,
            [ServiceDependency] IStateProvider state,
            [ServiceDependency] INavigationService nagivator
            )
        {
            _controller = controller;
            _state = state;
            _navigator = nagivator;
        }

        public override void OnViewLoaded()
        {
            View.IsNew = isNew();
            Appointment appointment = (Appointment)_state[StateKeys.CurrentAppointment];

            if (appointment == null)
                _navigator.Navigate(NavigationKeys.Home);

            if (appointment != null && appointment.Patient != null)
                View.PatientId = appointment.Patient.Id;
        }

        public void OnNext()
        {
            if (isNew())
            {
                Appointment appointment = (Appointment)_state[StateKeys.CurrentAppointment];
                appointment.Patient = new Patient();
                appointment.Patient.Id = (Guid)View.PatientId;
                _state[StateKeys.CurrentAppointment] = appointment;
            }

            _navigator.Navigate(NavigationKeys.SelectProvider);
        }

        public void OnBack()
        {
            _state[StateKeys.CurrentAppointment] = null;
            _navigator.Navigate(NavigationKeys.Home);
        }

        private bool isNew()
        {
            Appointment appointment = (Appointment)_state[StateKeys.CurrentAppointment];
            return appointment != null && appointment.UBRN == -1;
        }

        public void OnEdit()
        {
            Appointment appointment = (Appointment)_state[StateKeys.CurrentAppointment];
            appointment.Patient = null;
            View.PatientId = null;
            _state[StateKeys.CurrentAppointment] = appointment;
            _navigator.Navigate(NavigationKeys.CreateAppointment);
        }
    }
}