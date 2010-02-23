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
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EAppointments.BMS.DataAccess
{
    public class DBLibException : ApplicationException
    {
        private DBLibExceptionType _exType;
        public DBLibExceptionType ExType
        {
            get
            {
                return _exType;
            }
        }

        private NameValueCollection _additionalInfo = new NameValueCollection();

        public NameValueCollection AdditionalInfo
        {
            get
            {
                return _additionalInfo;
            }
        }

        public DBLibException()
            : base()
        {
        }

        public DBLibException(string message)
            : base(message)
        {
        }

        public DBLibException(string message, Exception ex)
            : base(message, ex)
        {           
            if (!(ex is SqlException)) return;

            if (ex is SqlException)
            {
                SqlException sqlEx = (SqlException)this.InnerException;
                if (sqlEx != null)
                {
                    _exType = (DBLibExceptionType)sqlEx.Number;
                    _additionalInfo = ParseMessageSQL(sqlEx);
                }
            }

        }

        private NameValueCollection ParseMessageSQL(SqlException ex)
        {
            if (ex == null)
                return _additionalInfo;
            switch (_exType)
            {
                case DBLibExceptionType.LoginFailed:
                    _additionalInfo.Add("Server", ex.Server);

                    break;

                case DBLibExceptionType.InvalidStoredProcedure:
                    _additionalInfo.Add("Server", ex.Server);
                    _additionalInfo.Add("Procedure", ex.Procedure);
                    break;

                case DBLibExceptionType.ExecPermissionDenied:
                    _additionalInfo.Add("Server", ex.Server);
                    _additionalInfo.Add("Procedure", ex.Procedure);
                    break;

                case DBLibExceptionType.UniqueKeyViolation:
                    _additionalInfo.Add("Server", ex.Server);
                    ParseUniqueKeyViolationMessage(ex.Message);
                    break;

                case DBLibExceptionType.ForeignKeyViolation:
                    // TODO:
                    _additionalInfo.Add("Server", ex.Server);
                    break;

                default:
                    break;

            }

            return _additionalInfo;
        }


        void ParseUniqueKeyViolationMessage(string message)
        {
            // TODO:
        }
    }





    public enum DBLibExceptionType
    {
        LoginFailed = 18456,
        InvalidStoredProcedure = 2812,
        ExecPermissionDenied = 229,
        UniqueKeyViolation = 2627,
        ForeignKeyViolation = 547
    }
}
