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
    public class AppointmentDashboardViewPresenter : Presenter<IAppointmentDashboardView>
    {
        private AppointmentController _controller;
        private IStateProvider _state;
        private INavigationService _navigator;

        public AppointmentDashboardViewPresenter(
            [CreateNew] AppointmentController controller,
            [ServiceDependency] IStateProvider state,
            [ServiceDependency] INavigationService nagivator
            )
        {
            _controller = controller;
            _state = state;
            _navigator = nagivator;
        }

        public void OnAppointmentRowSelected(int ubrn)
        {
            View.SelectedAppointment = _controller.FindByUbrn(ubrn);
        }

        public void OnCreate()
        {
            Appointment appointment = new Appointment();
            appointment.UBRN = -1;
            _state[StateKeys.CurrentAppointment] = appointment;
            _navigator.Navigate(NavigationKeys.CreateAppointment);
        }

        public void OnBook(int ubrn)
        {
            _state[StateKeys.CurrentAppointment] = _controller.FindByUbrn(ubrn);
            _navigator.Navigate(NavigationKeys.BookAppointment);
        }

        public void OnCancel(int ubrn)
        {
            _state[StateKeys.CurrentAppointment] = _controller.FindByUbrn(ubrn);
            _navigator.Navigate(NavigationKeys.CancelAppointment);
        }

        public void OnApprove(int ubrn)
        {
            _state[StateKeys.CurrentAppointment] = _controller.FindByUbrn(ubrn);
            _navigator.Navigate(NavigationKeys.ApproveAppointment);
        }

        public void OnReject(int ubrn)
        {
            _state[StateKeys.CurrentAppointment] = _controller.FindByUbrn(ubrn);
            _navigator.Navigate(NavigationKeys.RejectAppointment);
        }

        public void OnDelete(int ubrn)
        {
            _state[StateKeys.CurrentAppointment] = _controller.FindByUbrn(ubrn);
            _navigator.Navigate(NavigationKeys.DeleteAppointment);
        }

        public void OnEdit(int ubrn)
        {
            _state[StateKeys.CurrentAppointment] = _controller.FindByUbrn(ubrn);
            _navigator.Navigate(NavigationKeys.EditAppointment);
        }

        public void OnReBook(int ubrn)
        {
            this.OnBook(ubrn);
        }
    }
}