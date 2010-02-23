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
using System.Globalization;

namespace EAppointments.BMS
{
    public class Patient
    {
        #region Private Members
        private DbLib _dbLib; 
        
        private Guid _id;
        private Application _parent;
        private string _patientNo;
        private string _title;
        private string _firstName;
        private string _lastName;
        private char _gender;
        private DateTime? _dateOfBirth;
        private string _addressLine1;
        private string _addressLine2;
        private string _city;
        private string _state;
        private string _country;
        private string _zipCode;
        private bool _consentToCallBack;
        private string _contactNumber;
        private string _email;
        private Referrer _referrer;
        #endregion

        #region Properties
        public Guid Id
        {
            get { return _id; }
        }

        public string PatientNo
        {
            get { return _patientNo; }            
            set { _patientNo = value; }
        }
       
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
              
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
       
        public char Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        public DateTime? DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; }
        }

        public string AddressLine1
        {
            get { return _addressLine1; }
            set { _addressLine1 = value; }
        }
        
        public string AddressLine2
        {
            get { return _addressLine2; }
            set { _addressLine2 = value; }
        }
        
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        public string State
        {
            get { return _state; }
            set { _state = value; }
        }
      
        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }
       
        public string ZipCode
        {
            get { return _zipCode; }
            set { _zipCode = value; }
        }
       
        public bool ConsentToCallBack
        {
            get { return _consentToCallBack; }
            set { _consentToCallBack = value; }
        }
     
        public string ContactNumber
        {
            get { return _contactNumber; }
            set { _contactNumber = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public Referrer ReferringClinician
        {
            get { return _referrer; }
            set { _referrer = value; }
        }
        #endregion 

        #region Constructors
        internal Patient(Application parent)
        {
            _id = Guid.NewGuid();
            _parent = parent;
            _dbLib = _parent.DbLib;
            
        }

        internal Patient(Application parent, DataRow dataRow)
        {
            _parent = parent;
            _dbLib = _parent.DbLib;
            Load(dataRow);
        }   
        #endregion

        #region Public Methods
        
        public void Save()
        {
            if (_id == Guid.Empty)
                throw new ArgumentNullException();

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    _dbLib.PatientSave(_id, _patientNo, _title, _firstName, _lastName, _gender, _dateOfBirth, _addressLine1, _addressLine2, _city, _state, _country, _zipCode, _consentToCallBack, _contactNumber, _email, _referrer.Id);
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
                _dbLib.PatientDelete(this._id);
                scope.Complete();
            }
        }

        #endregion	

        #region Private Methods

        private void Load(DataRow dataRow)
        {
            this._id = (Guid)dataRow["Id"];
            this._patientNo = (string)dataRow["PatientNo"];
            this._title = (string)dataRow["Title"];
            this._firstName = (string)dataRow["FirstName"];
            this._lastName = (string)dataRow["LastName"];
            this._gender = Convert.ToChar(dataRow["Gender"].ToString(), CultureInfo.CurrentCulture);
            this._dateOfBirth = dataRow["DOB"] != DBNull.Value ? (DateTime?)dataRow["DOB"] : null;
            this._addressLine1 = (string)dataRow["AddressLine1"];
            this._addressLine2 = (string)dataRow["AddressLine2"];
            this._city = (string)dataRow["City"];
            this._state = (string)dataRow["State"];
            this._country = (string)dataRow["Country"];
            this._zipCode = (string)dataRow["ZipCode"];
            this._consentToCallBack = (bool)dataRow["ConsentToCallBack"];
            this._contactNumber = (string)dataRow["ContactNumber"];
            this._email = (string)dataRow["Email"];

            Guid referrerId = (Guid)dataRow["ReferrerId"];            
            this._referrer = _parent.FindReferrerById(referrerId);
        }
        #endregion
    }

    public class PatientSearchCriteria
    {
        #region Private Members

        private Guid? _id;
        private Guid? _referrerId;
        private String _firstName;
        private String _lastName;

        #endregion

        #region Public Properties

        public Guid? Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Guid? ReferrerId
        {
            get { return _referrerId; }
            set { _referrerId = value; }
        }

        public String FirstName
        {
            get { return _firstName; }
            set { _firstName = String.IsNullOrEmpty(value) ? null : value; }
        }

        public String LastName
        {
            get { return _lastName; }
            set { _lastName = String.IsNullOrEmpty(value) ? null : value; }
        }

        #endregion

        public PatientSearchCriteria(Guid id)
        {
            this.Id = id;
        }
        
        public PatientSearchCriteria(Guid? id, Guid? referrerId)
        {
            this.Id = id;
            this.ReferrerId = referrerId;
        }

        public PatientSearchCriteria(String firstName, String lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }
    }

}
