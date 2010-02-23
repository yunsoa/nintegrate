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
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace EAppointments.BMS.DataTypes
{

    [DataContract]
    public class Appointment
    {
        private Int32 _ubrn;       
        private Patient _patient;
        private Referrer _referrer;
        private Provider _provider;
        private ClinicType _clinicType;
        private Slot _slot;
        private DateTime? _startDateTime;
        private DateTime? _endDateTime;
        private DateTime _createdDateTime;
        private DateTime _updatedDateTime;
        private DateTime? _cancelledDateTime;
        private Guid? _cancelledById;
        private string _cancellationReason;
        private AppointmentStatus _status;
        private DateTime? _reminderDate;
        private string _comments;
        private Guid? _workflowId;

        [DataMemberAttribute(IsRequired = false, Name = "UBRN", Order = 1)]
        public Int32 Ubrn
        {
            get { return _ubrn; }
            set { _ubrn = value; }
        }

        [DataMemberAttribute(IsRequired = true, Name = "Patient", Order = 2)]
        public Patient Patient
        {
            get { return _patient; }
            set { _patient = value; }
        }

        [DataMemberAttribute(IsRequired = true, Name = "Referrer", Order = 3)]
        public Referrer Referrer
        {
            get { return _referrer; }
            set { _referrer = value; }
        }

        [DataMemberAttribute(IsRequired = true, Name = "Provider", Order = 4)]
        public Provider Provider
        {
            get { return _provider; }
            set { _provider = value; }
        }

        [DataMemberAttribute(IsRequired = true, Name = "ClinicType", Order = 5)]
        public ClinicType ClinicType
        {
            get { return _clinicType; }
            set { _clinicType = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "Slot", Order = 6)]
        public Slot Slot
        {
            get { return _slot; }
            set { _slot = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "StartDateTime", Order = 7)]
        public Nullable<DateTime> StartDateTime
        {
            get { return _startDateTime; }
            set { _startDateTime = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "EndDateTime", Order = 8)]
        public Nullable<DateTime> EndDateTime
        {
            get { return _endDateTime; }
            set { _endDateTime = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "CreatedDateTime", Order = 9)]
        public DateTime CreatedDateTime
        {
            get { return _createdDateTime; }
            set { _createdDateTime = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "UpdatedDateTime", Order = 10)]
        public DateTime UpdatedDateTime
        {
            get { return _updatedDateTime; }
            set { _updatedDateTime = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "CancellationDateTime", Order = 11)]
        public Nullable<DateTime> CancelledDateTime
        {
            get { return _cancelledDateTime; }
            set { _cancelledDateTime = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "CancelledBy", Order = 12)]
        public Nullable<Guid> CancelledById
        {
            get { return _cancelledById; }
            set { _cancelledById = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "CancellationReason", Order = 13)]
        public string CancellationReason
        {
            get { return _cancellationReason; }
            set { _cancellationReason = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "Status", Order = 14)]
        public AppointmentStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "Comments", Order = 15)]
        public string Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "ReminderDate", Order = 16)]
        public DateTime? ReminderDate
        {
            get { return _reminderDate; }
            set { _reminderDate = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "WorkflowId", Order = 17)]
        public Guid? WorkflowId
        {
            get { return _workflowId; }
            set { _workflowId = value; }
        }

    }

    [DataContract]
    public enum AppointmentStatus
    {
        [EnumMember]
        None = 0,
        
        [EnumMember]
        Pending = 1,
        
        [EnumMember]
        Booked = 2,
        
        [EnumMember]
        Approved = 4,
        
        [EnumMember]
        Cancelled = 8,
        
        [EnumMember]
        Rejected = 16,
        
        [EnumMember]
        Elapsed = 32
    }

    [DataContract]   
    public class AppointmentSearchCriteria
    {
        private Int32? _ubrn;
        private Int32? _status;
        private Guid? _patientId;
        private Guid? _referrerId;
        private Guid? _providerId;
        private DateTime? _startDateTime;
        private DateTime? _endDateTime;       
        private DateTime? _createdDateTime;
        private Guid? _workflowId;

        [DataMemberAttribute(IsRequired = false, Name = "Ubrn", Order = 1)]
        public Nullable<Int32> Ubrn
        {
            get { return _ubrn; }
            set { _ubrn = value;}
        }
        
        [DataMemberAttribute(IsRequired = false, Name = "Status", Order = 2)]
        public Int32? Status
        {
            get { return _status; }
            set { _status = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "PatientId", Order = 3)]
        public Nullable<Guid> PatientId
        {
            get { return _patientId; }
            set { _patientId = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "ReferrerId", Order = 4)]
        public Nullable<Guid> ReferrerId
        {
            get { return _referrerId; }
            set { _referrerId = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "ProviderId", Order = 5)]
        public Nullable<Guid> ProviderId
        {
            get { return _providerId; }
            set { _providerId = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "StartDateTime", Order = 6)]
        public Nullable<DateTime> StartDateTime
        {
            get { return _startDateTime; }
            set { _startDateTime = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "EndDateTime", Order = 7)]
        public Nullable<DateTime> EndDateTime
        {
            get { return _endDateTime; }
            set { _endDateTime = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "CreatedDateTime", Order = 8)]
        public Nullable<DateTime> CreatedDateTime
        {
            get { return _createdDateTime; }
            set { _createdDateTime = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "WorkflowId", Order = 9)]
        public Nullable<Guid> WorkflowId
        {
            get { return _workflowId; }
            set { _workflowId = value; }
        }
    }

    [CollectionDataContract]
    public class AppointmentCollection : List<Appointment>
    {
        public AppointmentCollection() { }

        public AppointmentCollection(IEnumerable<Appointment> collection) : base(collection) {}
    }
}

