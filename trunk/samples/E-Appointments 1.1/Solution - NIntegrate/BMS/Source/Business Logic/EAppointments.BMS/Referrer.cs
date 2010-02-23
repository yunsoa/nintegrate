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
    public class Referrer
    {
        #region Private Members

        private DbLib _dbLib;

        private Guid _id;
        private Application _parent;
        private string _firstName;
        private string _clinicName;
        private string _lastName;
        private string _email;

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

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string ClinicName
        {
            get { return _clinicName; }
            set { _clinicName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
       
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        #endregion 

        #region Internal Methods
        internal Referrer(Application parent)
        {
            _id = Guid.NewGuid();
            _parent = parent;
            _dbLib = _parent.DbLib;
        }

        internal Referrer(Application parent, DataRow dataRow)
        {
            _parent = parent;
            _dbLib = _parent.DbLib;
            Load(dataRow);
        }

        #endregion

        #region Public Methods

        public void Save()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    _dbLib.ReferrerSave(_id, _firstName, _clinicName, _lastName, _email);
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
                _dbLib.ReferrerDelete(this._id);
                scope.Complete();
            }
        }

        #endregion	

        #region Private Methods

        private void Load(DataRow dataRow)
        {
            this._id = (Guid)dataRow["Id"];
            this._firstName = (string)dataRow["FirstName"];
            this._clinicName = (string)dataRow["ClinicName"];
            this._lastName = (string)dataRow["LastName"];
            this._email = (string)dataRow["Email"];
        }
        #endregion
    }

    public class ReferrerSearchCriteria
    {
        #region Private Members

        private Guid? _id;
        private String _firstName;
        private String _lastName;

        #endregion

        #region Public Properties

        public Guid? Id
        {
            get { return _id; }
            set { _id = value; }
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

        public ReferrerSearchCriteria(Guid? id)
        {
            this.Id = id;
        }

        public ReferrerSearchCriteria(String firstName, String lastName)
        {
            this.FirstName = String.IsNullOrEmpty(firstName) ? null : firstName;
            this.LastName = String.IsNullOrEmpty(lastName) ? null : lastName;
        }
    }
}
