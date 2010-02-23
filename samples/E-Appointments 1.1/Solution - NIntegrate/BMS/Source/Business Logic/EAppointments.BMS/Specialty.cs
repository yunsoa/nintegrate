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

namespace EAppointments.BMS
{
    public class Specialty
    {

        #region Private Members
        
        private Guid _id;
        private string _name;
        private Application _parent;

        #endregion

        #region Public Properties
        
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public Application Parent
        {
            get { return _parent; }
        }
        
        #endregion

        #region Constructor

        internal Specialty(Application parent, DataRow dataRow)
        {
            _parent = parent;
            Load(dataRow);
        }
        #endregion

        #region Private Members
        private void Load(DataRow dataRow)
        {
            _id = (Guid)dataRow["Id"];
            _name = (string)dataRow["Name"];

        }

        #endregion

    }
    
}
