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
using Microsoft.Practices.CompositeWeb.Utility;
using Microsoft.Practices.CompositeWeb.Web;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.PageFlow;
using EAppointments.UI.ServiceAgents.AppointmentService;
using EAppointments.UI.ServiceAgents.Interfaces;
using NIntegrate.Mapping;
using System.Data;

namespace EAppointments.UI.Modules
{
    public class AppointmentController
    {
        private IAppointmentServiceAgent _appointmentServiceAgent;
        private IProviderServiceAgent _providerServiceAgent;
        private IDirectoryServiceAgent _directoryServiceAgent;

        [InjectionConstructor]
        public AppointmentController
			(
            [ServiceDependency] IAppointmentServiceAgent appointmentServiceAgent,            
            [ServiceDependency] IProviderServiceAgent providerServiceAgent,
            [ServiceDependency] IDirectoryServiceAgent directoryServiceAgent
			)
		{
            _appointmentServiceAgent = appointmentServiceAgent;
            _providerServiceAgent = providerServiceAgent;
            _directoryServiceAgent = directoryServiceAgent;
		}		

        internal Appointment[] Find(Guid? patientId, DateTime? startDateTime, DateTime? endDateTime, Int32 status)
        {
            var query = new Query.Appointment();
            var criteria = query.Select();
            if (startDateTime.HasValue)
                criteria.And(query.StartDateTime >= startDateTime.Value);
            if (endDateTime.HasValue)
                criteria.And(query.EndDateTime <= endDateTime.Value);
            criteria.And(query.Status.BitwiseAnd(status) > 0);
            if (patientId.HasValue)
                criteria.And((query.PatientId == patientId.Value));
            var appointments = new List<Appointment>(_appointmentServiceAgent.Query(criteria));

            return OutputValidationUtility.Encode<Appointment>(appointments).ToArray();
        }

        internal Appointment FindByUbrn(int ubrn)
        {
            AppointmentSearchCriteria criteria = new AppointmentSearchCriteria();
            criteria.Ubrn = ubrn;

            List<Appointment> appointments = new List<Appointment>(_appointmentServiceAgent.Find(criteria));

            return OutputValidationUtility.Encode<Appointment>(appointments[0]);
        }

        internal Appointment Save(Appointment appointment)
        {
            return OutputValidationUtility.Encode < Appointment >(_appointmentServiceAgent.Save(appointment));
        }

        internal void Book(Appointment appointment)
        {
            _appointmentServiceAgent.Book(appointment.UBRN, appointment.Slot.Id);
        }

        internal void ReBook(Appointment appointment)
        {
            _appointmentServiceAgent.Rebook(appointment.UBRN, appointment.Slot.Id);
        }

        internal void Delete(Appointment appointment)
        {
            _appointmentServiceAgent.Delete(appointment.UBRN);
        }

        internal void Approve(Appointment appointment)
        {
            _appointmentServiceAgent.Approve(appointment.UBRN);
        }

        internal void Reject(Appointment appointment)
        {
            _appointmentServiceAgent.Reject(appointment.UBRN);
        }

        internal void Cancel(Appointment appointment, String cancellationReason)
        {
            _appointmentServiceAgent.Cancel(appointment.UBRN, cancellationReason);
        }
    }
}
