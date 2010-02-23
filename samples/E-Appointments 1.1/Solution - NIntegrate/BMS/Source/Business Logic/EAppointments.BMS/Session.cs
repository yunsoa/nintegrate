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
using System.Security;
using System.Security.Principal;
using System.Threading;
using EAppointments.BMS.DataAccess;
using EAppointments.BMS.Security;
using System.Transactions;
using System.Globalization;

namespace EAppointments.BMS
{
    public class Session
    {
        #region Private Members

        private DbLib _dbLib = new DbLib(); 

        private Guid _id;
        private Application _application;
        private bool _isValid;
        private DateTime _loginTime;
        private DateTime _lastAccessedTime;
        private const int DEFAULT_EXPIRE_MINUTES = 60;
        private EAPrincipal _principal;
        #endregion

        #region Properties

        /// <summary>
        /// Makes the Extended IPrinicpal object public so that its function can be accesed
        /// </summary>
        public EAPrincipal Principal
        {
            get
            {
                return _principal;
            }
        }

        /// <summary>
        /// Unique id of the session
        /// </summary>
        public Guid Id
        {
            get { return _id; }
        }

        public bool IsValid
        {
            get
            {
                _isValid = DateTime.Now < _lastAccessedTime.AddMinutes(DEFAULT_EXPIRE_MINUTES) ? _isValid : false;

                if (_isValid)
                {
                    //Extend expire time
                    _lastAccessedTime = DateTime.Now;
                    Save();
                }

                return _isValid;
            } 
        }

        public Application Application
        {
            get
            {
                if (!IsValid)
                {
                    throw new SecurityException();
                }

                if (_application == null)
                {
                    _application = Application.GetApplication(this);
                }

                return _application;
            }
        }


        #endregion

        #region Constructors

        public Session()
        {
            _id = Guid.NewGuid();
            _lastAccessedTime = DateTime.Now;
        }

        public Session(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("id");
            
            _id = id;
            _loginTime = DateTime.Now;
            _lastAccessedTime = DateTime.Now;
            Load();
        }

        #endregion

        #region Internal Methods
        internal void Load(DataRow row)
        {
            _lastAccessedTime = Convert.ToDateTime(row["LastAccessedTime"], CultureInfo.CurrentCulture);
        }

        internal void Save()
        {
            if (!_isValid)
                throw new InvalidOperationException(Properties.Resource.Invalid_Session_Message);

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    TimeSpan length = DateTime.Now - _loginTime;

                    _dbLib.SessionSave(_id, ((User)_principal.Identity).Id, length.Seconds, _lastAccessedTime, _loginTime);
                    scope.Complete();
                }
                catch
                {
                    // TODO: Handle Exception
                    throw;
                }
            }
        }

        internal void Load()
        {
            DbLib dbLib = new DbLib();

            DataTable dataTable = dbLib.SessionGet(_id);

            Load(dataTable.Rows[0]);
        }

        #endregion
    
        #region Public Methods

        public void Login(string userName, string password)
        {
            if (userName == null || password == null)
                throw new ArgumentNullException();

            //Get the User
            DataTable userTable = _dbLib.UserGet(null, userName);

            if (userTable == null || userTable.Rows.Count != 1)
                throw new SecurityException("Invalid UserName or Password");

            DataRow dataRow = userTable.Rows[0];
            if (((string)dataRow["UserName"]).ToLower(CultureInfo.CurrentCulture) != userName.ToLower(CultureInfo.CurrentCulture) || (string)dataRow["Password"] != password)
                throw new SecurityException("Invalid UserName or Password");


            _isValid = true;          
            _loginTime = _lastAccessedTime = DateTime.Now;

            // Set the Custom Principal object
            User user = new User(this, dataRow);
            _principal = new EAPrincipal(new List<string>(new string[] { user.Role.ToString() } ), user);
            
            Thread.CurrentPrincipal = _principal;
        }

        internal void Login(string userName)
        {
            User[] users = this.Find(new UserSearchCriteria(null));
            User currentUser = null;
            foreach (User user in users)
            {
                if (user.Name == userName)
                {
                    currentUser = user;
                    break;
                }
            }            
            
            _isValid = true;
            _loginTime = _lastAccessedTime = DateTime.Now;

            //initialize the Custom Principal object
            _principal = new EAPrincipal(new List<string>(new string[] { currentUser.Role.ToString() }), currentUser);

            Thread.CurrentPrincipal = _principal;
        }
     
        public void Logout()
        {
            _isValid = false;
            _application = null;
            _principal = null;

            using (TransactionScope scope = new TransactionScope())
            {
                _dbLib.SessionDelete(_id, DateTime.Now);
                scope.Complete();
            }
        }

        public static EAPrincipal GetCustomPrincipal(IIdentity identity)
        {
            if (identity == null)
                return null;
            
            if (!identity.IsAuthenticated)
                return null;
            
            // Create a dummy session
            Session session = new Session();
            session.Login(identity.Name);

            return (EAPrincipal)Thread.CurrentPrincipal;
        }

        public User[] Find(UserSearchCriteria criteria)
        {
            DataTable dataTable = _dbLib.UserGet(criteria.Id, null);

            List<User> userList = new List<User>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                User user = new User(this, dataRow);
                userList.Add(user);
            }

            return userList.ToArray();
        }

        public User FindUserById(Guid id)
        {
            User[] users = this.Find(new UserSearchCriteria(id));
            if (users.Length == 0)
                throw new IdNotFoundException("User", id);
            return users[0];
        }

        public User NewUser()
        {
            return new User(this);
        }

        #endregion
    }
}
