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
    public class ClinicType
    {
        #region Private Members

        private Specialty _parent;
        private Guid _id;
        private string _name;

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

        public Specialty Parent
        {
            get { return _parent; }
        }

        #endregion

        #region Constructor

        internal ClinicType(Specialty parent, DataRow dataRow)
        {
            _parent = parent;
            Load(dataRow);
        }
        
        #endregion

        #region Private Methods

        private void Load(DataRow dataRow)
        {
            _id = (Guid)dataRow["Id"];
            _name = (string)dataRow["Name"];
        }

        #endregion

        
    }

    public class ClinicTypeSearchCriteria
    {
        #region Private Members

        Guid? _id;
        Guid? _specialtyId;

        #endregion

        #region Public Properties

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

        #endregion

        public ClinicTypeSearchCriteria(Guid? id)
        {
            _id = id;
        }
        public ClinicTypeSearchCriteria(Guid? id, Guid specialtyId)
        {
            _id = id;
            _specialtyId = specialtyId;
        }
    }
}
