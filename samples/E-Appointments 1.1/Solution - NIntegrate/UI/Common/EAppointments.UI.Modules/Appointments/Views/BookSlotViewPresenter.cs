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
    public class BookSlotViewPresenter : Presenter<IBookSlotView>
    {
        private ProviderController _controller;
        private AppointmentController _appointmentController;
        private IStateProvider _state;
        private INavigationService _navigator;

        public BookSlotViewPresenter(
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

            View.ProviderId = appointment.Provider.Id;
            View.ClinicTypeId = appointment.ClinicType.Id;

            if (appointment.Slot != null)
            {
                View.SlotId = appointment.Slot.Id == Guid.Empty ? (Guid?)null : appointment.Slot.Id;                
            }
        }

        public void OnNext()
        {
            Appointment appointment = (Appointment)_state[StateKeys.CurrentAppointment];

            if(isNew())
            {
                // If its a new appointment save the appointment first
                appointment = _appointmentController.Save(appointment);
                
                appointment.Slot = new Slot();
                appointment.Slot.Id = (Guid)View.SlotId;

                _appointmentController.Book(appointment);
            }
            else // This is a book on pending appointment or a re-book attempt
            {
                bool isRebook = appointment.Slot != null;
                if (View.SlotId.HasValue)
                {
                    appointment.Slot = new Slot();
                    appointment.Slot.Id = (Guid)View.SlotId;

                    if (isRebook)
                        _appointmentController.ReBook(appointment);
                    else
                        _appointmentController.Book(appointment);
                }
            }

            _state[StateKeys.CurrentAppointment] = appointment;
            _navigator.Navigate(NavigationKeys.Summary);
        }

        public void OnBack()
        {
            _navigator.Navigate(NavigationKeys.SelectProvider);
        }

        private bool isNew()
        {
            Appointment appointment = (Appointment)_state[StateKeys.CurrentAppointment];
            return appointment != null && appointment.UBRN == -1;
        }

        public void OnSkip()
        {
            _navigator.Navigate(NavigationKeys.Summary);
        }

        public void OnEdit()
        {
            Appointment appointment = (Appointment)_state[StateKeys.CurrentAppointment];
            appointment.Slot.Id = Guid.Empty;
            View.SlotId = null;
            _state[StateKeys.CurrentAppointment] = appointment;
            _navigator.Navigate(NavigationKeys.SelectSlot);
        }
    }
}