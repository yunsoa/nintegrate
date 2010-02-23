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
    public class Provider
    {
        #region Private Members
        private DbLib _dbLib;

        private Guid _id;
        private Application _parent;
        ProviderClinicType[] _providerClinicTypes;
        private string _name; 
        private string _location;
        private string _organization;
        private string _email;
        private Double _latitude;
        private Double _longitude;
        private string _conditionsTreated;
        private string _proceduresPerformed;
        private string _exclusions;
        private string _alternativeServices;
        private double _proximity;
        #endregion

        #region Properties
        public Guid Id
        {
            get { return _id; }
        }

        public Application Parent
        {
            get { return _parent; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }

        public string Organization
        {
            get { return _organization; }
            set { _organization = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public Double Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }

        public Double Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }

        public string ConditionsTreated
        {
            get { return _conditionsTreated; }
            set { _conditionsTreated = value; }
        }

        public string ProceduresPerformed
        {
            get { return _proceduresPerformed; }
            set { _proceduresPerformed = value; }
        }

        public string Exclusions
        {
            get { return _exclusions; }
            set { _exclusions = value; }
        }

        public string AlternativeServices
        {
            get { return _alternativeServices; }
            set { _alternativeServices = value; }
        }

        public double Proximity
        {
            get { return _proximity; }
        }
        #endregion

        #region Constructors

        internal Provider(Application parent)
        {
            _id = Guid.NewGuid();
            _parent = parent;
            _dbLib = _parent.DbLib;
            _parent = parent;
        }

        internal Provider(Application parent, DataRow dataRow)
        {
            _parent = parent;
            _dbLib = _parent.DbLib;
            this.Load(dataRow);
        }

        #endregion  

        #region Public Methods

        public ProviderClinicType NewProviderClinicType()
        {
            return new ProviderClinicType(this);
        }

        public ProviderClinicType[] GetProviderClinicTypes()
        {
            return _providerClinicTypes;
        }
        
        public void AddProviderClinicType(ProviderClinicType pct)
        {
            if (pct != null)
            {
                List<ProviderClinicType> providerClinicTypesList = new List<ProviderClinicType>();
                if (_providerClinicTypes != null)
                {
                    foreach (ProviderClinicType provClinicType in _providerClinicTypes)
                    {
                        providerClinicTypesList.Add(provClinicType);
                    }
                }

                providerClinicTypesList.Add(pct);
                _providerClinicTypes = providerClinicTypesList.ToArray();
            }
        }

        public void DeleteProviderClinicType(Guid providerClinicTypeId)
        {
            List<ProviderClinicType> providerClinicTypesList = new List<ProviderClinicType>();
            if (_providerClinicTypes != null)
            {
                foreach (ProviderClinicType pct in _providerClinicTypes)
                {
                    if (pct.Id == providerClinicTypeId)
                    {
                        // First delete all slots for that ProviderClinicType
                        DataTable dataTable = _dbLib.SlotGet(null, pct.Parent.Id, null, pct.ClinicType.Id, null, null, null, null, null);
                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            _dbLib.SlotDelete((Guid)dataRow["Id"]);
                        }

                        _dbLib.ProviderClinicTypeDelete(pct.Parent.Id, pct.ClinicType.Id);
                    }
                    else
                    {
                        providerClinicTypesList.Add(pct);
                    }
                }
                _providerClinicTypes = providerClinicTypesList.ToArray();
            }
        }
        
        public void Save()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    
                    _dbLib.ProviderSave(_id, _name, _location, _organization, _email, _latitude, _longitude, _conditionsTreated,
                                        _proceduresPerformed, _exclusions, _alternativeServices);
                    
                    foreach (ProviderClinicType pct in _providerClinicTypes)
                    {
                        _dbLib.ProviderClinicTypeSave(pct.Id, pct.Parent.Id, pct.ClinicType.Id, pct.SlotsAvailable, pct.SlotDuration, pct.DayStartTime, pct.DayEndTime, pct.Weekdays);
                    }
                    
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
                foreach (ProviderClinicType pct in _providerClinicTypes)
                {
                    _dbLib.ProviderClinicTypeDelete(pct.Parent.Id, pct.ClinicType.Id);
                }
                
                _dbLib.ProviderDelete(_id);
                scope.Complete();
            }
        }

        #endregion	

        #region Private Methods

        private ProviderClinicType[] FindProviderClinicTypes(Guid providerId)
        {
            DataTable dataTable = _dbLib.ProviderClinicTypeGet(null, providerId, null);
            List<ProviderClinicType> providerClinicTypeList = new List<ProviderClinicType>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                ProviderClinicType providerClinicType = new ProviderClinicType(this, dataRow);
                providerClinicTypeList.Add(providerClinicType);
            }

            return providerClinicTypeList.ToArray();
        }
        
        private void Load(DataRow dataRow)
        {
            _id = (Guid)dataRow["Id"];
            _providerClinicTypes = FindProviderClinicTypes(_id);
            _name = (string)dataRow["Name"];
            _location = (string)dataRow["Location"];
            _organization = (string)dataRow["Organization"];
            _email = (string)dataRow["Email"];
            _latitude = (Double)dataRow["Latitude"];
            _longitude = (Double)dataRow["Longitude"];
            _conditionsTreated = (string)dataRow["ConditionsTreated"];
            _proceduresPerformed = (string)dataRow["ProceduresPerformed"];
            _exclusions = (string)dataRow["Exclusions"];
            _alternativeServices = (string)dataRow["AlternativeServices"];
            if (!String.IsNullOrEmpty(dataRow["Proximity"].ToString()))
            {
                _proximity = (double)dataRow["Proximity"];
            }
        }

        #endregion
    }

    public class ProviderClinicType
    {
        #region Private Members
        private Provider _parent;

        private Guid _id;
        private ClinicType _clinicType;
        private Specialty _specialty;
        private int _slotsAvailable;
        private int _slotDuration;
        private DateTime _dayStartTime;
        private DateTime _dayEndTime;
        private int _weekDays;

        #endregion

        #region Public Members

        public Provider Parent
        {
            get { return _parent; }
        }
        public Guid Id
        {
            get { return _id; }
        }
        public ClinicType ClinicType
        {
            get { return _clinicType; }
            set { _clinicType = value; }
        }
        public Specialty Specialty
        {
            get { return _specialty; }
            set { _specialty = value; }
        }
        public int SlotsAvailable
        {
            get { return _slotsAvailable; }
            set { _slotsAvailable = value; }
        }
        public int SlotDuration
        {
            get { return _slotDuration; }
            set { _slotDuration = value; }
        }
        public DateTime DayStartTime
        {
            get { return _dayStartTime; }
            set { _dayStartTime = value; }
        }
        public DateTime DayEndTime
        {
            get { return _dayEndTime; }
            set { _dayEndTime = value; }
        }
        public int Weekdays
        {
            get { return _weekDays; }
            set { _weekDays = value; }
        }

        #endregion

        #region Constructor
        
        internal ProviderClinicType(Provider parent)
        {
            _id = Guid.NewGuid();
            _parent = parent;
        }
        
        internal ProviderClinicType(Provider parent, DataRow dataRow)
        {
            _id = Guid.NewGuid();
            _parent = parent;
            Load(dataRow);
        }
        #endregion

        #region Private Methods

        private void Load(DataRow dataRow)
        {
            _id = (Guid)dataRow["Id"];

            Guid clinicTypeId = (Guid)dataRow["ClinicTypeId"];
            _clinicType = _parent.Parent.FindClinicTypeById((Guid)clinicTypeId);
            _specialty = _clinicType.Parent;

            _slotsAvailable = (int)dataRow["SlotsAvailable"];
            _slotDuration = (int)dataRow["SlotDuration"];
            _dayStartTime = (DateTime)dataRow["DayStartTime"];
            _dayEndTime = (DateTime)dataRow["DayEndTime"];
            _weekDays = (int)dataRow["WeekDays"];

        }
        #endregion

        #region Public Methods
        public void GenerateSlots(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                throw new ArgumentException(Properties.Resource.Generate_Slots_Failed_Message_1);
            }
            if (startDate.Date < DateTime.Now.Date && endDate.Date < DateTime.Now.Date)
            {
                throw new ArgumentException(Properties.Resource.Generate_Slots_Failed_Message_2);
            }
            
            if (startDate.Date < DateTime.Now.Date)
                startDate = startDate.AddDays((DateTime.Now.Date - startDate.Date).Days);
            
            for (DateTime currDate = startDate; currDate <= endDate; currDate = currDate.AddDays(1))
            {
                
                if ((_weekDays&1) > 0 && currDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    SetAndSaveSlotTimes(currDate);
                }
                if ((_weekDays&2) > 0 && currDate.DayOfWeek == DayOfWeek.Friday)
                {
                    SetAndSaveSlotTimes(currDate);
                }
                if ((_weekDays&4) > 0 && currDate.DayOfWeek == DayOfWeek.Thursday)
                {
                    SetAndSaveSlotTimes(currDate);
                }
                if ((_weekDays&8) > 0 && currDate.DayOfWeek == DayOfWeek.Wednesday)
                {
                    SetAndSaveSlotTimes(currDate);
                }
                if ((_weekDays&16) > 0 && currDate.DayOfWeek == DayOfWeek.Tuesday)
                {
                    SetAndSaveSlotTimes(currDate);
                }
                if ((_weekDays&32) > 0 && currDate.DayOfWeek == DayOfWeek.Monday)
                {
                    SetAndSaveSlotTimes(currDate);
                }
                if ((_weekDays&64) > 0 && currDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    SetAndSaveSlotTimes(currDate);
                }
            }
        }

        private void SetAndSaveSlotTimes(DateTime slotDateTime)
        {
            Slot slot = _parent.Parent.NewSlot();
            slot.ClinicType = _clinicType;
            slot.Provider = _parent;

            slot.StartDateTime = new DateTime(slotDateTime.Year, slotDateTime.Month, slotDateTime.Day, _dayStartTime.Hour,
                _dayStartTime.Minute, _dayStartTime.Second);

            slot.EndDateTime = new DateTime(slotDateTime.Year, slotDateTime.Month, slotDateTime.Day, _dayEndTime.Hour,
                _dayEndTime.Minute, _dayEndTime.Second);

            slot.Save();

        }
        #endregion

    }

    public class ProviderSearchCriteria
    {
        #region Private Members

        private string _zipCode;
        private Guid? _id;
        private Guid? _specialtyId;
        private Guid? _clinicTypeId;
        private double? _withinMiles;
        private string _keywords;

        #endregion

        #region Public Properties

        public string ZipCode
        {
            get { return _zipCode; }
            set { _zipCode = String.IsNullOrEmpty(value) ? null : value; }
        }
        
        public Guid? Id
        {
            get { return _id; }
            set { _id = value; }
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
        
        public double? WithinMiles 
        { 
            get { return _withinMiles; } 
            set { _withinMiles = value; } 
        }
        
        public string Keywords
        {
            get { return _keywords; }
            set { _keywords = String.IsNullOrEmpty(value) ? null : value; }
        }

        #endregion

        public ProviderSearchCriteria(Guid id)
        {
            this.Id = id;
        }
        public ProviderSearchCriteria(Guid? id, Guid? specialtyId)
        {
            this.Id = id;
            this.SpecialtyId = specialtyId;
        }

    }

}
