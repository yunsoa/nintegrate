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
using System.Workflow.Activities;

namespace EAppointments.BMS.Workflow.Interfaces
{
    [ExternalDataExchange()]
    public interface IAppointmentWorkflowService
    {
        void Create(Guid patientId, Guid providerId, Guid clinicTypeId, Guid workflowInstanceId);

        void Save(int ubrn, String comments, DateTime? reminderDate);

        void Book(int ubrn, Guid slotId);

        void Cancel(int ubrn, String reason);

        void ReBook(int ubrn, Guid slotId); 

        void Approve(int ubrn);

        void Reject(int ubrn);

        void Delete(int ubrn);
        
        event EventHandler<AppointmentCreatedEventArgs> AppointmentCreated;

        event EventHandler<AppointmentSavedEventArgs> AppointmentSaved;

        event EventHandler<AppointmentBookedEventArgs> AppointmentBooked;

        event EventHandler<AppointmentCancelledEventArgs> AppointmentCancelled;

        event EventHandler<AppointmentReBookedEventArgs> AppointmentReBooked;

        event EventHandler<AppointmentApprovedEventArgs> AppointmentApproved;

        event EventHandler<AppointmentRejectedEventArgs> AppointmentRejected;

        event EventHandler<AppointmentDeletedEventArgs> AppointmentDeleted;        
    }

    [Serializable]
    public class AppointmentCreatedEventArgs : ExternalDataEventArgs
    {
        public AppointmentCreatedEventArgs(Guid InstanceId)
            : base(InstanceId){}

        private Guid _patientId;

        public Guid PatientId
        {
            get { return _patientId; }
            set { _patientId = value; }
        }

        private Guid _providerId;

        public Guid ProviderId
        {
            get { return _providerId; }
            set { _providerId = value; }
        }

        private Guid _clinicTypeId;

        public Guid ClinicTypeId
        {
            get { return _clinicTypeId; }
            set { _clinicTypeId = value; }
        }
    }

    [Serializable]
    public class AppointmentSavedEventArgs : ExternalDataEventArgs
    {
        public AppointmentSavedEventArgs(Guid InstanceId)
            : base(InstanceId){}

        private int _ubrn;

        public int Ubrn
        {
            get { return _ubrn; }
            set { _ubrn = value; }
        }

        private string _comments;
        
        public String Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }

        private DateTime? _reminderDate;

        public DateTime? ReminderDate
        {
            get { return _reminderDate; }
            set { _reminderDate = value; }
        }
    }

    [Serializable]
    public class AppointmentBookedEventArgs : ExternalDataEventArgs
    {
        public AppointmentBookedEventArgs(Guid InstanceId)
            : base(InstanceId) { }

        private int _ubrn;
        private Guid _slotId;

        public int Ubrn
        {
            get { return _ubrn; }
            set { _ubrn = value; }
        }
       
        public Guid SlotId
        {
            get { return _slotId; }
            set { _slotId = value; }
        }
    }
    
    [Serializable]
    public class AppointmentReBookedEventArgs : ExternalDataEventArgs
    {
        public AppointmentReBookedEventArgs(Guid InstanceId)
            : base(InstanceId) { }

        private int _ubrn;
        private Guid _slotId;

        public int Ubrn
        {
            get { return _ubrn; }
            set { _ubrn = value; }
        }

        public Guid NewSlotId
        {
            get { return _slotId; }
            set { _slotId = value; }
        }
    }
    
    [Serializable]
    public class AppointmentCancelledEventArgs : ExternalDataEventArgs
    {
        public AppointmentCancelledEventArgs(Guid InstanceId)
            : base(InstanceId) { }

        private int _ubrn;
        private String _reason;

        public int Ubrn
        {
            get { return _ubrn; }
            set { _ubrn = value; }
        }

        public String Reason
        {
            get { return _reason; }
            set { _reason = value; }
        }
    }

    [Serializable]
    public class AppointmentApprovedEventArgs : ExternalDataEventArgs
    {
        public AppointmentApprovedEventArgs(Guid InstanceId)
            : base(InstanceId) { }

        private int _ubrn;

        public int Ubrn
        {
            get { return _ubrn; }
            set { _ubrn = value; }
        }
    }

    [Serializable]    
    public class AppointmentRejectedEventArgs : ExternalDataEventArgs
    {
        public AppointmentRejectedEventArgs(Guid InstanceId)
            : base(InstanceId) { }

        private int _ubrn;

        public int Ubrn
        {
            get { return _ubrn; }
            set { _ubrn = value; }
        }
    }

    [Serializable]
    public class AppointmentDeletedEventArgs : ExternalDataEventArgs
    {
        public AppointmentDeletedEventArgs(Guid InstanceId)
            : base(InstanceId) { }

        private int _ubrn;

        public int Ubrn
        {
            get { return _ubrn; }
            set { _ubrn = value; }
        }
    }

    
}
