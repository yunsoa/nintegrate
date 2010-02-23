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
using System.Transactions;

namespace EAppointments.BMS
{
    public enum SlotStatus
    {
        All,
        Available,
        Booked
    }
    
    public class Slot
    {
        #region Private Members

        private DbLib _dbLib;

        private Guid _id;
        private Provider _provider;
        private Application _parent;
        private ClinicType _clinicType;
        private DateTime _startDateTime;
        private DateTime _endDateTime;
        private int? _ubrn;
        private SlotStatus _status;

        #endregion

        #region Public Properties
        public Guid Id
        {
            get { return _id; }
        }

        public Provider Provider
        {
            get { return _provider; }
            set { _provider = value; }
        }

        public ClinicType ClinicType
        {
            get { return _clinicType; }
            set { _clinicType = value; }
        }

        public DateTime StartDateTime
        {
            get { return _startDateTime; }
            set { _startDateTime = value; }
        }

        public DateTime EndDateTime
        {
            get { return _endDateTime; }
            set { _endDateTime = value; }
        }

        public int? Ubrn
        {
            get { return _ubrn; }
        }

        public SlotStatus Status
        {
            get { return _status; }
        }

        #endregion

        #region Internal Methods
        internal Slot(Application parent)
        {
            _id = Guid.NewGuid();
            _parent = parent;
            _dbLib = _parent.DbLib;
        }

        internal Slot(Application parent, DataRow dataRow)
        {
            _parent = parent;
            _dbLib = _parent.DbLib;
            Load(dataRow);
        }

        public bool Book(int ubrn)
        {
            if (_status == SlotStatus.Booked)
                return false;
            
            _status = SlotStatus.Booked;
            _ubrn = ubrn;
            this.Save();
            return true;
        }

        public bool Cancel()
        {
            if (_status == SlotStatus.Available)
                return false;

            _status = SlotStatus.Available;
            _ubrn = null;
            this.Save();
            return true;
        }

        #endregion

        #region Public Methods

        public void Save()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    _dbLib.SlotSave(_id, _provider.Id, _clinicType.Parent.Id, _clinicType.Id, _startDateTime, _endDateTime, _ubrn, (int)_status);
                    scope.Complete();
                }
                catch (DBLibException ex)
                {
                    Application.HandleException(ex);
                }
            }
        }

        public void Delete()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                _dbLib.SlotDelete(this._id);
                scope.Complete();
            }
        }

        #endregion

        #region Private Methods

        private void Load(DataRow dataRow)
        {
            this._id = (Guid)dataRow["Id"];

            Guid providerId = new Guid(dataRow["ProviderId"].ToString());            
            this._provider = _parent.FindProviderById(providerId);

            Guid _clinicTypeId = new Guid(dataRow["ClinicTypeId"].ToString());
            this._clinicType = _parent.FindClinicTypeById(_clinicTypeId);

            this._startDateTime = (DateTime)dataRow["StartDateTime"];
            this._endDateTime = (DateTime)dataRow["EndDateTime"];
            
            if (!String.IsNullOrEmpty(dataRow["UBRN"].ToString()))
            {
                this._ubrn = (int)dataRow["UBRN"];
            }

            this._status = (SlotStatus)Enum.Parse(typeof(SlotStatus), dataRow["Status"].ToString());

        }
        #endregion
    }

  
    public class SlotSearchCriteria
    {
        #region Private Members

        private Guid? _id;
        private Guid? _providerId;
        private Guid? _specialtyId;
        private Guid? _clinicTypeId;
        private DateTime? _startDateTime;
        private DateTime? _endDateTime;
        private int? _weekDays;
        private int? _ubrn;
        private SlotStatus _status = SlotStatus.All;

        #endregion

        #region Public Properties

        public Guid? Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Guid? ProviderId
        {
            get { return _providerId; }
            set { _providerId = value; }
        }

        public Guid? SpecialtyId
        {
            get { return _specialtyId; }
            set { _specialtyId = value; }
        }

        public Guid? ClinicTypeId
        {
            get { return _clinicTypeId; }
            set { _clinicTypeId = value; }
        }

        public DateTime? StartDateTime
        {
            get { return _startDateTime; }
            set { _startDateTime = value; }
        }

        public DateTime? EndDateTime
        {
            get { return _endDateTime; }
            set { _endDateTime = value; }
        }

        public int? WeekDays
        {
            get { return _weekDays; }
            set { _weekDays = value; }
        }

        public int? Ubrn
        {
            get { return _ubrn; }
            set { _ubrn = value; }
        }

        public SlotStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        #endregion

        public SlotSearchCriteria(Guid? id)
        {
            this.Id = id; 
        }

        public SlotSearchCriteria(Guid? id, Guid providerId)
        {
            this.Id = id;
            this.ProviderId = providerId;
        }
    }
}
