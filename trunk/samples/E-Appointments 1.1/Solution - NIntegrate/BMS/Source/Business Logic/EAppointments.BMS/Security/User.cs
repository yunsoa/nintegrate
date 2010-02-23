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
using EAppointments.BMS;
using EAppointments.BMS.DataAccess;
using System.Transactions;
using System.Security.Principal;


namespace EAppointments.BMS.Security
{
    public enum RoleType
    {
        Patient,
        Referrer,
        ProviderClinician,       
        BmsAdmin
    }
    
    
    public class User : IIdentity
	{
        #region Private Members

        private Guid _id;
        private Session _parent;
        private string _name;
        private string _firstName;
        private string _lastName;
        private string _password;
        private string _email;
        private RoleType _role;
        private Guid? _refUserId;
        private bool _isAuthenticated;
        private string _authenticationType;

        #endregion

        #region Properties
        
        public Guid Id
        {
            get { return _id; }
        }

        public Session Parent
        {
            get { return _parent; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public RoleType Role
        {
            get { return _role; }
            set { _role = value; }
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

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(_password);
                _password = value;
            }
        }

        public Guid? RefUserId
        {
            get { return _refUserId; }
            set { _refUserId = value; }
        }

        public bool IsAuthenticated
        {
            get { return _isAuthenticated; }
        }

        public string AuthenticationType
        {
            get { return _authenticationType; }
        }
 
        #endregion

        #region Constructors

        internal User(Session parent)
        {
            _parent = parent;
            _id = Guid.NewGuid();
        }

        internal User(Session parent, DataRow dataRow)
        {
            _parent = parent;
            _isAuthenticated = true;
            _authenticationType = "DATABASE";
            Load(dataRow);
        }
       
        #endregion

        #region Public Methods
     
        public void Save()
        {
            if (_id == Guid.Empty || String.IsNullOrEmpty(_name) || String.IsNullOrEmpty(_password))
                throw new ArgumentNullException();

            if (_parent == null)
                throw new ArgumentException("Parent Cannot be Null ");

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    _parent.Application.DbLib.UserSave(_id, _name, _password, _firstName, _lastName, _email, (int)_role, _refUserId);
                    scope.Complete();
                }
                catch (DBLibException ex)
                {
                    Application.HandleException(ex);
                }
            }
        }
             
        #endregion

        #region Private Methods

        private void Load(DataRow dataRow)
        {
            this._id = (Guid)dataRow["Id"];
            this._name = (string)dataRow["UserName"];
            this._password = (string)dataRow["Password"];
            this._firstName = (string)dataRow["FirstName"];
            this._lastName = (string)dataRow["LastName"];
            this._email = (string)dataRow["Email"];
            this._role = (RoleType)dataRow["Role"];
            if(!String.IsNullOrEmpty(dataRow["RefUserId"].ToString()))
                this._refUserId = (Guid?)dataRow["RefUserId"];
        }

        #endregion
    }

    public class UserSearchCriteria
    {
        #region Private Members
        
        private Guid? _id;

        #endregion

        #region Public Properties

        public Guid? Id
        {
            get { return _id; }
            set { _id = value; }
        }

        #endregion

        public UserSearchCriteria(Guid? id)
        {
            this.Id = id;
        }
    }
}
