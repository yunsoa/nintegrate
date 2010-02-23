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
using EAppointments.UI.ServiceAgents.AppointmentService;
using NIntegrate.Data;

namespace EAppointments.UI.ServiceAgents.Interfaces
{
    public interface IAppointmentServiceAgent
    {
        Appointment[] Query(QueryCriteria criteria);
        Appointment[] Find(AppointmentSearchCriteria criteria);
        Appointment Save(Appointment appointment);
        void Book(int ubrn, Guid slotId);
        void Cancel(int ubrn, String cancellationReason);
        void Rebook(int ubrn, Guid slotId);
        void Approve(int ubrn);
        void Reject(int ubrn);
        void Delete(int ubrn);
    }
}
