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
using EAppointments.BMS.Security;
using System.Runtime.Remoting.Messaging;

namespace EAppointments.BMS.Workflow.Interfaces
{
    public class AppointmentWorkflowService : IAppointmentWorkflowService
    {
        public event EventHandler<AppointmentCreatedEventArgs> AppointmentCreated;

        public event EventHandler<AppointmentSavedEventArgs> AppointmentSaved;

        public event EventHandler<AppointmentBookedEventArgs> AppointmentBooked;

        public event EventHandler<AppointmentCancelledEventArgs> AppointmentCancelled;

        public event EventHandler<AppointmentReBookedEventArgs> AppointmentReBooked;

        public event EventHandler<AppointmentApprovedEventArgs> AppointmentApproved;

        public event EventHandler<AppointmentRejectedEventArgs> AppointmentRejected;

        public event EventHandler<AppointmentDeletedEventArgs> AppointmentDeleted;        


        /// <summary>
        /// Raises an event out of the service to indicate that an appointment needs to be created
        /// </summary>
        public void RaiseAppointmentCreateEvent(Guid workflowInstanceId, Guid patientId, Guid providerId, Guid clinicTypeId)
        {
            AppointmentCreatedEventArgs e = new AppointmentCreatedEventArgs(workflowInstanceId);
            e.PatientId = patientId;
            e.ProviderId = providerId;
            e.ClinicTypeId = clinicTypeId;

            if (AppointmentCreated != null)
            {
                AppointmentCreated(null, e);
            }
        }

        /// <summary>
        /// Raises an event out of the service to indicate that an appointment needs to be saved
        /// </summary>
        public void RaiseAppointmentSaveEvent(Guid workflowInstanceId, int ubrn, String comments, DateTime? reminderDateTime)
        {
            AppointmentSavedEventArgs e = new AppointmentSavedEventArgs(workflowInstanceId);
            e.Ubrn = ubrn;
            e.Comments = comments;
            e.ReminderDate = reminderDateTime;

            if (AppointmentSaved != null)
            {
                AppointmentSaved(null, e);
            }
        }

        /// <summary>
        /// Raises an event out of the service to indicate that an appointment has been booked
        /// </summary>
        public void RaiseAppointmentBookEvent(Guid workflowInstanceId, int ubrn, Guid slotId)
        {
            AppointmentBookedEventArgs e = new AppointmentBookedEventArgs(workflowInstanceId);
            e.Ubrn = ubrn;
            e.SlotId = slotId;

            if (AppointmentBooked != null)
            {
                AppointmentBooked(null, e);
            }
        }

        /// <summary>
        /// Raises an event out of the service to indicate that an appointment has been cancelled
        /// </summary>
        public void RaiseAppointmentCancelEvent(Guid workflowInstanctId, int ubrn, String cancellationReason)
        {
            AppointmentCancelledEventArgs e = new AppointmentCancelledEventArgs(workflowInstanctId);
            e.Ubrn = ubrn;
            e.Reason = cancellationReason;

            if (AppointmentCancelled != null)
            {
                AppointmentCancelled(null, e);
            }
        }

        /// <summary>
        /// Raises an event out of the service to indicate that an appointment has been Re-booked
        /// </summary>
        public void RaiseAppointmentReBookEvent(Guid workflowInstanceId, int ubrn, Guid newSlotId)
        {
            AppointmentReBookedEventArgs e = new AppointmentReBookedEventArgs(workflowInstanceId);
            e.Ubrn = ubrn;
            e.NewSlotId = newSlotId;

            if (AppointmentReBooked != null)
            {
                AppointmentReBooked(null, e);
            }
        }

        /// <summary>
        /// Raises an event out of the service to indicate that an appointment has been Approved
        /// </summary>
        public void RaiseAppointmentApproveEvent(Guid workflowInstanceId, int ubrn)
        {
            AppointmentApprovedEventArgs e = new AppointmentApprovedEventArgs(workflowInstanceId);
            e.Ubrn = ubrn;

            if (AppointmentApproved != null)
            {
                AppointmentApproved(null, e);
            }
        }

        /// <summary>
        /// Raises an event out of the service to indicate that an appointment has been rejected
        /// </summary>
        public void RaiseAppointmentRejectEvent(Guid workflowInstanctId, int ubrn)
        {
            AppointmentRejectedEventArgs e = new AppointmentRejectedEventArgs(workflowInstanctId);
            e.Ubrn = ubrn;

            if (AppointmentRejected != null)
            {
                AppointmentRejected(null, e);
            }
        }

        /// <summary>
        /// Raises an event out of the service to indicate that an appointment has been Approved
        /// </summary>
        public void RaiseAppointmentDeleteEvent(Guid workflowInstanceId, int ubrn)
        {
            AppointmentDeletedEventArgs e = new AppointmentDeletedEventArgs(workflowInstanceId);
            e.Ubrn = ubrn;

            if (AppointmentDeleted != null)
            {
                AppointmentDeleted(null, e);
            }
        }


        #region IAppointmentWorkflowService Members

        public void Create(Guid patientId, Guid providerId, Guid clinicTypeId, Guid workflowInstanceId)
        {
            try
            {
                Application application = GetApplication();

                Patient patient = application.FindPatientById(patientId);
                Referrer referrer = patient.ReferringClinician;
                Provider provider = application.FindProviderById(providerId);
                ClinicType clinicType = (BMS.ClinicType)application.FindClinicTypeById(clinicTypeId);

                Appointment appointment = application.NewAppointment(patient, referrer, provider, clinicType);
                appointment.WorkflowId = workflowInstanceId;
                appointment.Save();
            }
            catch (Exception ex)
            {
                CallContext.SetData("Exception", ex);
            }
        }

        public void Save(int ubrn, String comments, DateTime? reminderDate)
        {
            try
            {
                Appointment appointment = GetApplication().FindAppointmentById(ubrn);
                appointment.Comments = comments;
                appointment.ReminderDate = reminderDate;
                appointment.Save();
            }
            catch (Exception ex)
            {
                CallContext.SetData("Exception", ex);
            }
        }

        public void Book(int ubrn, Guid slotId)
        {
            try
            {
                Application application = GetApplication();

                Appointment appointment = application.FindAppointmentById(ubrn);
                Slot slot = application.FindSlotById(slotId);
                appointment.Book(slot);
            }
            catch (Exception ex)
            {
                CallContext.SetData("Exception", ex);
            }
        }

        public void Cancel(int ubrn, string reason)
        {
            try
            {
                Appointment appointment = GetApplication().FindAppointmentById(ubrn);
                appointment.Cancel(reason);
            }
            catch (Exception ex)
            {
                CallContext.SetData("Exception", ex);
            }
        }

        public void ReBook(int ubrn, Guid newSlotId)
        {
            try
            {
                Application application = GetApplication();

                Appointment appointment = application.FindAppointmentById(ubrn);
                Slot newSlot = application.FindSlotById(newSlotId);
                appointment.Rebook(newSlot);
            }
            catch (Exception ex)
            {
                CallContext.SetData("Exception", ex);
            }
        }

        public void Approve(int ubrn)
        {
            try
            {
                Appointment appointment = GetApplication().FindAppointmentById(ubrn);
                appointment.Approve();
            }
            catch (Exception ex)
            {
                CallContext.SetData("Exception", ex);
            }
        }

        public void Reject(int ubrn)
        {
            try
            {
                Appointment appointment = GetApplication().FindAppointmentById(ubrn);
                appointment.Reject();
            }
            catch (Exception ex)
            {
                CallContext.SetData("Exception", ex);
            }
        }

        public void Delete(int ubrn)
        {
            try
            {
                Appointment appointment = GetApplication().FindAppointmentById(ubrn);
                appointment.Delete();
            }
            catch (Exception ex)
            {
               CallContext.SetData("Exception", ex);
            }
        }

        #endregion

        public static Application GetApplication()
        {
            User currentUser = (User)System.Threading.Thread.CurrentPrincipal.Identity;
            return currentUser.Parent.Application;
        }

    }
}

