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
using System.ServiceModel;
using EAppointments.BMS.DataTypes;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.WCF;
using System.ServiceModel.Security;
using System.Security;
using NIntegrate.Data;

namespace EAppointments.BMS.ServiceContracts
{
    [ServiceContract(Namespace = "http://EAppointments.BMS.ServiceContracts/Appointment/2007/08")]
    [ExceptionShielding("AppointmentService")]    
    public interface IAppointmentService
    {
        [OperationContract(Action = "Query")]
        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(AppointmentServiceFault))]
        [FaultContract(typeof(SecurityAccessDeniedException))]  
        AppointmentCollection Query(QueryCriteria criteria);

        [OperationContract(Action = "Find")]
        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(AppointmentServiceFault))]
        [FaultContract(typeof(SecurityAccessDeniedException))]  
        AppointmentCollection Find(AppointmentSearchCriteria criteria);

        [OperationContract(Action = "Save")]
        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(AppointmentServiceFault))]
        [FaultContract(typeof(SecurityAccessDeniedException))]      
        Appointment Save(Appointment appointment);

        [OperationContract(Action = "Book")]
        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(AppointmentServiceFault))]
        [FaultContract(typeof(SecurityAccessDeniedException))]      
        void Book(int ubrn, Guid slotId);

        [OperationContract(Action = "Cancel")]
        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(AppointmentServiceFault))]
        [FaultContract(typeof(SecurityAccessDeniedException))]      
        void Cancel(int ubrn, String cancellationReason);

        [OperationContract(Action = "Rebook")]
        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(AppointmentServiceFault))]
        [FaultContract(typeof(SecurityAccessDeniedException))]      
        void Rebook(int ubrn, Guid slotId);

        [OperationContract(Action = "Approve")]
        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(AppointmentServiceFault))]
        [FaultContract(typeof(SecurityAccessDeniedException))]      
        void Approve(int ubrn);

        [OperationContract(Action = "Reject")]
        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(AppointmentServiceFault))]
        [FaultContract(typeof(SecurityAccessDeniedException))]      
        void Reject(int ubrn);

        [OperationContract(Action = "Delete")]
        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(AppointmentServiceFault))]
        [FaultContract(typeof(SecurityAccessDeniedException))]
        void Delete(int ubrn);
    }
}
