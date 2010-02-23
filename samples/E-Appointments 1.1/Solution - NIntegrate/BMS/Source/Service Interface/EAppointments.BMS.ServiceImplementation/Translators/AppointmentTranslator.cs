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
using EAppointments.BMS.DataTypes;
using EAppointments.BMS;
using EAppointments.BMS.Security;

namespace EAppointments.BMS.ServiceImplementation
{
    public static class AppointmentTranslator
    {
        internal static DataTypes.Appointment TranslateBusinessToService(BMS.Appointment from)
        {
            DataTypes.Appointment to = new DataTypes.Appointment();
            to.Ubrn = from.Ubrn;
            to.StartDateTime = from.StartDateTime;
            to.EndDateTime = from.EndDateTime;
            to.CreatedDateTime = from.CreatedDateTime;
            to.UpdatedDateTime = from.UpdatedDateTime;
            to.CancelledDateTime = from.CancelledDateTime;
            to.CancellationReason = from.CancellationReason;
            to.Referrer = DirectoryTranslator.TranslateBusinessToService(from.Referrer);
            to.Patient = DirectoryTranslator.TranslateBusinessToService(from.Patient);
            to.Provider = ProviderTranslator.TranslateBusinessToService(from.Provider);
            to.ClinicType = DirectoryTranslator.TranslateBusinessToService(from.ClinicType);
            
            to.Status = (DataTypes.AppointmentStatus)from.Status;
            to.Comments = from.Comments;
            to.ReminderDate = from.ReminderDate;
            to.WorkflowId = from.WorkflowId;
            if (from.CancelledBy != null)
                to.CancelledById = from.CancelledBy.Id;            
            if (from.Slot != null)
                to.Slot = SlotTranslator.TranslateBusinessToService(from.Slot);
            
            return to;
        }

        internal static BMS.Appointment TranslateServiceToBusiness(DataTypes.Appointment from, BMS.Appointment to)
        {
            to.Comments = from.Comments;
            to.ReminderDate = from.ReminderDate;
            to.WorkflowId = from.WorkflowId;

            return to;
        }

        internal static BMS.Appointment TranslateServiceToBusiness(DataTypes.Appointment from)
        {
            Application application = Helper.GetApplication();

            BMS.Patient patient = (BMS.Patient)application.FindPatientById(from.Patient.Id);


            BMS.Referrer referrer = null;
            if (from.Referrer == null)
            {
                referrer = patient.ReferringClinician;
            }
            else
            {
                referrer = (BMS.Referrer)application.FindReferrerById(from.Referrer.Id);
            }

            BMS.Provider provider = (BMS.Provider)application.FindProviderById(from.Provider.Id);
            BMS.ClinicType clinicType = (BMS.ClinicType)application.FindClinicTypeById(from.ClinicType.Id);

            EAppointments.BMS.Appointment to = application.NewAppointment(
                   patient, referrer, provider, clinicType
                );

            return to;
        }

        internal static BMS.AppointmentSearchCriteria TranslateSearchCriteria(DataTypes.AppointmentSearchCriteria from)
        {
            BMS.AppointmentSearchCriteria to = new BMS.AppointmentSearchCriteria(from.Ubrn);
            to.PatientId = from.PatientId;
            to.ProviderId = from.ProviderId;
            to.ReferrerId = from.ReferrerId;
            to.StartDate = from.StartDateTime;
            to.EndDate = from.EndDateTime;
            to.CreatedDate = from.CreatedDateTime;
            to.Status = from.Status;
            to.WorkflowId = from.WorkflowId;

            return to;
        }
     }
}

