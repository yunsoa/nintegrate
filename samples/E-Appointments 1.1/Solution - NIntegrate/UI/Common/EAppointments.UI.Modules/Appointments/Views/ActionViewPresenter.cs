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
using Microsoft.Practices.CompositeWeb;
using Microsoft.Practices.ObjectBuilder;
using EAppointments.UI.ServiceAgents.AppointmentService;
using EAppointments.UI.Modules.Services;
using EAppointments.UI.Modules.Constants;

namespace EAppointments.UI.Modules.Views
{
    public class ActionViewPresenter : Presenter<IActionView>
    {
        private AppointmentController _controller;
        private IStateProvider _state;
        private INavigationService _navigator;

        public ActionViewPresenter(
            [CreateNew] AppointmentController controller,
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
            Appointment appointment = (Appointment)_state[StateKeys.CurrentAppointment];

            if (appointment == null)
                _navigator.Navigate(NavigationKeys.Home);

            if (appointment != null)
                View.CurrentAppointment = appointment;
        }


        public void OnApprove()
        {
            Appointment appointment = (Appointment)_state[StateKeys.CurrentAppointment];
            _controller.Approve(appointment);

            _navigator.Navigate(NavigationKeys.Home);
        }


        public void OnReject()
        {
            Appointment appointment = (Appointment)_state[StateKeys.CurrentAppointment];
            _controller.Reject(appointment);

            _navigator.Navigate(NavigationKeys.Home);
        }

        public void OnCancel()
        {
            Appointment appointment = (Appointment)_state[StateKeys.CurrentAppointment];
            _controller.Cancel(appointment, View.Reason);

            _navigator.Navigate(NavigationKeys.Home);
        }

        public void OnDelete()
        {
            Appointment appointment = (Appointment)_state[StateKeys.CurrentAppointment];
            _controller.Delete(appointment);

            _navigator.Navigate(NavigationKeys.Home);
        }
        
        public void OnBack()
        {
            _navigator.Navigate(NavigationKeys.Home);
        }       
    }
}
