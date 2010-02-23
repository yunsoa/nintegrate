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
using System.Data;
using EAppointments.BMS.DataAccess;
using EAppointments.BMS.Security;
using System.Transactions;
using System.Threading;
using System.Security.Permissions;

namespace EAppointments.BMS
{
    [Flags]
    public enum AppointmentStatus
    {
        Pending = 1,
        Booked = 2,
        Approved = 4,
        Cancelled = 8,
        Rejected = 16,
        Elapsed = 32
    }

    // TODO: Move operations like Book, Cancel, Approve etc. to Workflow Activities.
    public class Appointment
    {
        #region Private Members
        private DbLib _dbLib;

        private int _ubrn;
        private Application _parent;
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
        private User _cancelledBy;
        private string _cancellationReason;
        private AppointmentStatus _status;
        private string _comments;
        private DateTime? _reminderDate;
        private Guid? _workflowId;

        #endregion

        #region Properties
        public int Ubrn
        {
            get { return _ubrn; }
            set { _ubrn = value; }
        }

        public Patient Patient
        {
            get { return _patient; }
        }

        public Referrer Referrer
        {
            get { return _referrer; }
        }

        public Provider Provider
        {
            get { return _provider; }
        }

        public ClinicType ClinicType
        {
            get { return _clinicType; }
        }

        public Slot Slot
        {
            get { return _slot; }
        }

        public DateTime? StartDateTime
        {
            get { return _startDateTime; }
        }

        public DateTime? EndDateTime
        {
            get { return _endDateTime; }
        }

        public DateTime CreatedDateTime
        {
            get { return _createdDateTime; }
        }

        public DateTime UpdatedDateTime
        {
            get { return _updatedDateTime; }
        }

        public DateTime? CancelledDateTime
        {
            get { return _cancelledDateTime; }
        }

        public User CancelledBy
        {
            get { return _cancelledBy; }
        }

        public string CancellationReason
        {
            get { return _cancellationReason; }
        }

        public AppointmentStatus Status
        {
            get { return _status; }
        }

        public string Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }

        public DateTime? ReminderDate
        {
            get { return _reminderDate; }
            set { _reminderDate = value; }
        }

        public Guid? WorkflowId
        {
            get { return _workflowId; }
            set { _workflowId = value; }
        }

        #endregion

        #region Constructors
        internal Appointment(Application parent, Patient patient, Referrer referrer, Provider provider, ClinicType clinicType)
        {
            _parent = parent;
            _dbLib = _parent.DbLib;
            _ubrn = _parent.GetMaxUBRN();
            _patient = patient;
            _referrer = referrer;
            _provider = provider;
            _clinicType = clinicType;    
            _createdDateTime = DateTime.Now;
            _status = AppointmentStatus.Pending;
        }

        internal Appointment(Application parent, DataRow dataRow)
        {
            _parent = parent;
            _dbLib = _parent.DbLib;
            Load(dataRow);
        }
        #endregion

        #region Public Methods

        public void Save()
        {
            if (_patient == null)
            {
                throw new ArgumentNullException(Properties.Resource.Appointment_Save_Failed_Message_1);
            }
            if (_referrer == null)
            {
                throw new ArgumentNullException(Properties.Resource.Appointment_Save_Failed_Message_2);
            }
            if (_provider == null)
            {
                throw new ArgumentNullException(Properties.Resource.Appointment_Save_Failed_Message_3);
            }
            if (_clinicType == null)
            {
                throw new ArgumentNullException(Properties.Resource.Appointment_Save_Failed_Message_4);
            }
            
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    _updatedDateTime = DateTime.Now;
                    _dbLib.AppointmentSave(_ubrn, _patient.Id, _referrer.Id, _provider.Id, _clinicType.Id, _slot != null ? _slot.Id : (Guid?)null, _createdDateTime, _startDateTime, _endDateTime,_updatedDateTime, _cancelledBy != null ? _cancelledBy.Id : (Guid?)null, _cancelledDateTime, _cancellationReason, (int)_status, _comments, (DateTime?)_reminderDate, (Guid?)_workflowId);

                    scope.Complete();
                }
                catch (DBLibException ex)
                {
                    Application.HandleException(ex);
                }
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Referrer")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Patient")]
        [PrincipalPermission(SecurityAction.Demand, Role = "BmsAdmin")]
        public void Book(Slot slot)
        {
            if (slot == null)
                throw new ArgumentNullException(Properties.Resource.Invalid_Slot_Message);
            
            using (TransactionScope scope = new TransactionScope())
            {            
                if (!slot.Book(_ubrn))
                    throw new BookingFailedException(Properties.Resource.Appointment_Book_Failed_Message_2);

                _status = AppointmentStatus.Booked;
                _slot = slot;
                _startDateTime = slot.StartDateTime;
                _endDateTime = slot.EndDateTime;

                this.Save();

                scope.Complete();

                #region Uncomment this Code if Custom Performance Counters are required

                //PerformanceCounter perfCounter = new PerformanceCounter("EAppointments Counters", "Book Appointment", false);
                //perfCounter.Increment();

                #endregion
            }            
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Referrer")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Patient")]
        [PrincipalPermission(SecurityAction.Demand, Role = "BmsAdmin")]
        public void Cancel(string cancellationReason)
        {

                Slot slot = _parent.FindSlotById(_slot.Id);

                using (TransactionScope scope = new TransactionScope())
                {
                    try
                    {

                        if (!slot.Cancel())
                            throw new CancellationFailedException(Properties.Resource.Appointment_Cancel_Failed_Message_2);

                        if (String.IsNullOrEmpty(cancellationReason))
                            throw new CancellationFailedException(Properties.Resource.Appointment_Cancel_Failed_Message_3);

                        _cancelledBy = (User)Thread.CurrentPrincipal.Identity; ;
                        _cancelledDateTime = DateTime.Now;
                        _cancellationReason = cancellationReason;

                        _status = AppointmentStatus.Cancelled;
                        _slot = null;

                        this.Save();

                        scope.Complete();

                        #region Uncomment this Code if Custom Performance Counters are required

                        //PerformanceCounter perfCounter = new PerformanceCounter("EAppointments Counters", "Cancel Appointment", false);
                        //perfCounter.Increment();

                        #endregion
                    }

                    catch (CancellationFailedException ex)
                    {
                        Application.HandleException(ex);
                    }
                }
            
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Referrer")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Patient")]
        [PrincipalPermission(SecurityAction.Demand, Role = "BmsAdmin")]
        public void Rebook(Slot newSlot)
        {
            if (newSlot == null)
                throw new ArgumentNullException(Properties.Resource.Invalid_Slot_Message);
            
            Slot oldSlot = _slot;

            using (TransactionScope scope = new TransactionScope())
            {
                if (!oldSlot.Cancel())
                    throw new CancellationFailedException(Properties.Resource.Appointment_Rebook_Failed_Message_2);

                if (!newSlot.Book(_ubrn))
                    throw new BookingFailedException(Properties.Resource.Appointment_Rebook_Failed_Message_3);

                _status = AppointmentStatus.Booked;
                _slot = newSlot;
                _startDateTime = newSlot.StartDateTime;
                _endDateTime = newSlot.EndDateTime;

                this.Save();

                scope.Complete();

                #region Uncomment this Code if Custom Performance Counters are required

                //PerformanceCounter perfCounter = new PerformanceCounter("EAppointments Counters", "ReBook Appointment", false);
                //perfCounter.Increment();

                #endregion
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "ProviderClinician")]
        public void Approve()
        {
            _status = AppointmentStatus.Approved;

            this.Save();

            #region Uncomment this Code if Custom Performance Counters are required

            //PerformanceCounter perfCounter = new PerformanceCounter("EAppointments Counters", "Approve Appointment", false);
            //perfCounter.Increment();

            #endregion
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "ProviderClinician")]
        public void Reject()
        {
            if (!_slot.Cancel())
                throw new CancellationFailedException(Properties.Resource.Appointment_Reject_Failed_Message_1);
            
            _status = AppointmentStatus.Rejected;

            this.Save();

            #region Uncomment this Code if Custom Performance Counters are required

            //PerformanceCounter perfCounter = new PerformanceCounter("EAppointments Counters", "Reject Appointment", false);
            //perfCounter.Increment();

            #endregion
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Referrer")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Patient")]
        [PrincipalPermission(SecurityAction.Demand, Role = "BmsAdmin")]
        public void Delete()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                _dbLib.AppointmentDelete(_ubrn);
                scope.Complete();
            }

            #region Uncomment this Code if Custom Performance Counters are required

            //PerformanceCounter perfCounter = new PerformanceCounter("EAppointments Counters", "Delete Appointment", false);
            //perfCounter.Increment();

            #endregion
        }

        #endregion

        #region Private Methods

        private void Load(DataRow dataRow)
        {
            _ubrn = (int)dataRow["UBRN"];            
            _patient = _parent.FindPatientById((Guid)dataRow["PatientId"]);
            _referrer = _parent.FindReferrerById((Guid)dataRow["ReferrerId"]);
            _provider = _parent.FindProviderById((Guid)dataRow["ProviderId"]);
            _clinicType = _parent.FindClinicTypeById((Guid)dataRow["ClinicTypeId"]);

            if (!String.IsNullOrEmpty(dataRow["SlotId"].ToString()))
            {
                Guid? slotId = (Guid?)dataRow["SlotId"];
                _slot = _parent.FindSlotById((Guid)slotId);
                _startDateTime = (DateTime?)dataRow["StartDateTime"];
                _endDateTime = (DateTime?)dataRow["EndDateTime"];
            }

            _createdDateTime = (DateTime)dataRow["CreatedDate"];
            _updatedDateTime = (DateTime)dataRow["UpdatedDate"];

            if (!String.IsNullOrEmpty(dataRow["CancelledBy"].ToString()))
            {
                Guid cancelledByUserId = new Guid(dataRow["CancelledBy"].ToString());
                _cancelledBy = _parent.Session.FindUserById((Guid)cancelledByUserId);
                _cancellationReason = (string)dataRow["CancellationReason"];
                _cancelledDateTime = (DateTime?)dataRow["CancelledDate"];
            }

            _status = (AppointmentStatus)Enum.Parse(typeof(AppointmentStatus), dataRow["Status"].ToString());

            if (!String.IsNullOrEmpty(dataRow["Comments"].ToString()))
            {
                _comments = (string)dataRow["Comments"];
            }
            
            if (!String.IsNullOrEmpty(dataRow["ReminderDate"].ToString()))
            {
                _reminderDate = (DateTime?)dataRow["ReminderDate"];
            }

            if (!String.IsNullOrEmpty(dataRow["WorkflowId"].ToString()))
            {
                _workflowId = (Guid?)dataRow["WorkflowId"];
            }
        
        }

        
        #endregion
    }

    public class AppointmentSearchCriteria
    {
        #region Private Members
        
        private int? _ubrn;
        private int? _status;
        private Guid? _patientId;
        private Guid? _referrerId;
        private Guid? _providerId;
        private DateTime? _startDate;
        private DateTime? _endDate;
        private DateTime? _createdDate;
        private Guid? _workflowId;
        
        #endregion

        #region Public Properties

        public int? Ubrn
        {
            get { return _ubrn; }
            set { _ubrn = value; }
        }

        public int? Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public Guid? PatientId
        {
            get { return _patientId; }
            set { _patientId = value; }
        }

        public Guid? ReferrerId
        {
            get { return _referrerId; }
            set { _referrerId = value; }
        }

        public Guid? ProviderId
        {
            get { return _providerId; }
            set { _providerId = value; }
        }

        public DateTime? StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public DateTime? EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        public DateTime? CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }

        public Guid? WorkflowId
        {
            get { return _workflowId; }
            set { _workflowId = value; }
        }

        #endregion
        
        public AppointmentSearchCriteria(int? ubrn)
        {
            this.Ubrn = ubrn;
        }

        public AppointmentSearchCriteria(DateTime? startDate, DateTime? endDate)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
        }
    }
}
