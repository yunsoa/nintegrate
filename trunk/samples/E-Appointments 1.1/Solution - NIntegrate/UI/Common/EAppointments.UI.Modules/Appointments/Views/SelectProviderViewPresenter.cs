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
using EAppointments.UI.ServiceAgents.AppointmentService;
using EAppointments.UI.Modules.Services;
using EAppointments.UI.Modules.Constants;

namespace EAppointments.UI.Modules.Views
{
    public class SelectProviderViewPresenter : Presenter<ISelectProviderView>
    {
        private ProviderController _controller;
        private AppointmentController _appointmentController;
        private IStateProvider _state;
        private INavigationService _navigator;

        public SelectProviderViewPresenter(
            [CreateNew] ProviderController controller,
           [CreateNew] AppointmentController appointmentController,
           [ServiceDependency] IStateProvider state,
           [ServiceDependency] INavigationService nagivator
           )
        {
            _controller = controller;
            _appointmentController = appointmentController;
            _state = state;
            _navigator = nagivator;
        }

        public override void OnViewLoaded()
        {
            View.IsNew = isNew();
            Appointment appointment = (Appointment)_state[StateKeys.CurrentAppointment];

            if (appointment == null)
                _navigator.Navigate(NavigationKeys.Home);

            if (appointment != null && appointment.Provider != null)
                View.ProviderId = appointment.Provider.Id;
        }

        public void OnNext()
        {
            if (isNew())
            {
                Appointment appointment = (Appointment)_state[StateKeys.CurrentAppointment];

                if (appointment.Provider == null)
                {
                    appointment.Provider = new Provider();
                    appointment.Provider.Id = (Guid)View.ProviderId;
                    appointment.ClinicType = new ClinicType();
                    appointment.ClinicType.Id = (Guid)View.ClinicTypeId;
                    _state[StateKeys.CurrentAppointment] = appointment;
                }               
            }            
            _navigator.Navigate(NavigationKeys.SelectSlot);
        }

        public void OnBack()
        {
            _navigator.Navigate(NavigationKeys.CreateAppointment);
        }

        private bool isNew()
        {
            Appointment appointment = (Appointment)_state[StateKeys.CurrentAppointment];
            return appointment != null && appointment.UBRN == -1;
        }

        public void OnEdit()
        {
            Appointment appointment = (Appointment)_state[StateKeys.CurrentAppointment];
            appointment.Provider = null;
            View.ProviderId = null;
            _state[StateKeys.CurrentAppointment] = appointment;
            _navigator.Navigate(NavigationKeys.SelectProvider);
        }

        public void OnSave()
        {
            if (isNew())
            {
                Appointment appointment = (Appointment)_state[StateKeys.CurrentAppointment];

                if (appointment.Provider == null)
                {
                    appointment.Provider = new Provider();
                    appointment.Provider.Id = (Guid)View.ProviderId;
                    appointment.ClinicType = new ClinicType();
                    appointment.ClinicType.Id = (Guid)View.ClinicTypeId;
                }
                // Save the appointment now...
                appointment = _appointmentController.Save(appointment);

                _state[StateKeys.CurrentAppointment] = appointment;
            }

            _navigator.Navigate(NavigationKeys.Summary);
        }       
    }
}