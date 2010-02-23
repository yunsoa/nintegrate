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
using System.ServiceModel.Security;
using EAppointments.BMS.DataTypes;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.WCF;


namespace EAppointments.BMS.ServiceContracts
{
    [ServiceContract(Namespace = "http://EAppointments.BMS.ServiceContracts/Directory/2007/08")]
    [ExceptionShielding("DirectoryService")]
    public interface IDirectoryService
    {
        [OperationContract(Action = "FindPatient")]
        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(DirectoryServiceFault))]
        [FaultContract(typeof(SecurityAccessDeniedException))] 
        PatientCollection FindPatient(PatientSearchCriteria criteria);

        [OperationContract(Action = "FindReferrer")]
        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(DirectoryServiceFault))]
        [FaultContract(typeof(SecurityAccessDeniedException))] 
        ReferrerCollection FindReferrer(ReferrerSearchCriteria criteria);

        [OperationContract(Action = "FindSpecialty")]
        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(DirectoryServiceFault))]
        [FaultContract(typeof(SecurityAccessDeniedException))] 
        SpecialtyCollection FindSpecialty();

        [OperationContract(Action = "FindClinicType")]
        [FaultContract(typeof(SystemFault))]
        [FaultContract(typeof(DirectoryServiceFault))]
        [FaultContract(typeof(SecurityAccessDeniedException))] 
        ClinicTypeCollection FindClinicType(Guid specialtyId);
    }
}
