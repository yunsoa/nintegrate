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
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace EAppointments.BMS.DataAccess
{
    public class DbLib
    {
        Database db = DatabaseFactory.CreateDatabase();

        #region Helper Methods for Get, Save, Delete & Transaction

        private DataTable ExecuteQuery(string spName, params object[] parameterValues)
        {
            try
            {
                IDataReader dataReader = db.ExecuteReader(spName, parameterValues);
                DataTable dt = LoadDataTable(dataReader);

                if (dataReader != null || !dataReader.IsClosed)
                    dataReader.Close();

                return dt;
            }
            catch (SqlException ex)
            {
                ExceptionPolicy.HandleException(ex, "Data Access Policy");
                throw;
            }
        }

        private int ExecuteNonQuery(string spName, params object[] parameterValues)
        {
            try
            {
                return db.ExecuteNonQuery(spName, parameterValues);
            }
            catch (SqlException ex)
            {
                ExceptionPolicy.HandleException(ex, "Data Access Policy");
                throw;
            }
        }

        private DataTable LoadDataTable(IDataReader dataReader)
        {
            DataTable dataTable = new DataTable();

            // Populate the columns (name, type)
            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                dataTable.Columns.Add(dataReader.GetName(i), dataReader.GetFieldType(i));
            }

            while (dataReader.Read())
            {
                // Populate the values 
                object[] values = new object[dataReader.FieldCount];
                dataReader.GetValues(values);
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        #endregion

        #region Session

        public int SessionSave(Guid Id, Guid UserId, int length, DateTime lastAccessTime, DateTime loginTime)
        {
            return this.ExecuteNonQuery("dbo.SessionSave", Id, UserId, length, lastAccessTime, loginTime);
        }

        public DataTable SessionGet(Guid? id)
        {
            return this.ExecuteQuery("dbo.SessionGet", id);
        }

        public int SessionDelete(Guid id, DateTime logOutTime)
        {
            return this.ExecuteNonQuery("dbo.SessionDelete", id, logOutTime);
        }

        #endregion

        #region User CRUD

        public DataTable UserGet(Guid? id, string userName)
        {
            return this.ExecuteQuery("dbo.UserGet", id, userName);
        }

        public int UserSave(Guid id, string userName, string password, string firstName, string lastName, string email, int role, Guid? refUserId)
        {
            return this.ExecuteNonQuery("dbo.UserSave", id, userName, password, firstName, lastName, email, role, refUserId);
        }

        public int UserDelete(Guid id)
        {
            return this.ExecuteNonQuery("dbo.UserDelete", id);
        }

        #endregion

        #region Patient CRUD

        public int PatientSave(Guid patientId, String patientNo, String title, String firstName, String lastName, Char gender, DateTime? dob,
                                String addressLine1, String addressLine2, String city, String state, String country, String zipCode, bool consentToCallback, string contactNumber, string email, Guid? referrerId)
        {

            return this.ExecuteNonQuery("dbo.PatientSave", patientId, patientNo, title, firstName, lastName, gender, dob,
                        addressLine1, addressLine2, city, state, country, zipCode, consentToCallback, contactNumber, email, referrerId);
        }

        public DataTable PatientGet(Guid? patientId, Guid? referrerId, String firstname, String lastName)
        {
            return this.ExecuteQuery("dbo.PatientGet", patientId, referrerId, firstname, lastName);
        }

        public int PatientDelete(Guid? patientId)
        {
            return this.ExecuteNonQuery("dbo.PatientDelete", patientId);
        }

        #endregion

        #region Referrer CRUD

        public int ReferrerSave(Guid referrerId, String firstName, String clinicName, String lastName, String email)
        {
            return this.ExecuteNonQuery("dbo.ReferrerSave", referrerId, firstName, clinicName, lastName, email);
        }

        public DataTable ReferrerGet(Guid? referrerId, String firstName, String lastName)
        {
            return this.ExecuteQuery("dbo.ReferrerGet", referrerId, firstName, lastName);
        }

        public int ReferrerDelete(Guid? referrerId)
        {
            return this.ExecuteNonQuery("dbo.ReferrerDelete", referrerId);
        }

        #endregion

        #region Appointment CRUD

        public int AppointmentSave(int ubrnNo, Guid patientId, Guid referrerId, Guid providerId, Guid clinicTypeId, Guid? slotId, DateTime createdDateTime, DateTime? startDateTime, DateTime? endDateTime, DateTime updatedDateTime, Guid? cancelledBy, DateTime? cancelledDate, string cancellationReason, int? status, string comments, DateTime? reminderDate, Guid? workflowId)
        {
            return this.ExecuteNonQuery("dbo.AppointmentSave", ubrnNo, patientId, referrerId, providerId, clinicTypeId, slotId, createdDateTime, startDateTime, endDateTime, updatedDateTime, cancelledBy, cancelledDate, cancellationReason, status, comments, reminderDate, workflowId);
        }

        public DataTable AppointmentGet(int? ubrnNo, Guid? patientId, Guid? referrerId, Guid? providerId, int? status, DateTime? startDateTime, DateTime? endDateTime, Guid? workflowId)
        {
            return this.ExecuteQuery("dbo.AppointmentGet", ubrnNo, patientId, referrerId, providerId, status, startDateTime, endDateTime, workflowId);
        }

        public int AppointmentDelete(int ubrnNo)
        {
            return this.ExecuteNonQuery("dbo.AppointmentDelete", ubrnNo);
        }

        public int GetMaxUBRN()
        {
            return (int)db.ExecuteScalar(CommandType.StoredProcedure, "dbo.GetMaxUBRN");
        }

        #endregion

        #region Provider CRUD

        public int ProviderSave(Guid providerId, string name, string location, string organization, string email, double latitude, double longitude, string conditionsTreated, string proceduresPerformed, string exclusions, string alternativeServices)
        {
            return this.ExecuteNonQuery("dbo.ProviderSave", providerId, name, location, organization, email, latitude, longitude, conditionsTreated, proceduresPerformed, exclusions, alternativeServices);
        }

        public DataTable ProviderGet(Guid? providerId, Guid? specialityId, Guid? clinicTypeId, string zipCode, double? withinMiles, string keywords)
        {
            return this.ExecuteQuery("dbo.ProviderGet", providerId == Guid.Empty ? null : providerId
                , specialityId == Guid.Empty ? null : specialityId
                , clinicTypeId == Guid.Empty ? null : clinicTypeId
                , zipCode, (withinMiles.HasValue && withinMiles != (double)0) ? withinMiles : null, keywords);
        }

        public int ProviderDelete(Guid providerId)
        {
            return this.ExecuteNonQuery("dbo.ProviderDelete", providerId);
        }

        #endregion

        #region Provider ClinicType CRUD

        public int ProviderClinicTypeSave(Guid providerClinicTypeId, Guid providerId, Guid clinicTypeId, int slotsAvailable, int slotDuration, DateTime dayStartTime, DateTime dayEndTime, int weekDays)
        {
            return this.ExecuteNonQuery("dbo.ProviderClinicTypeSave", providerClinicTypeId, providerId, clinicTypeId, slotsAvailable, slotDuration, dayStartTime, dayEndTime, weekDays);
        }

        public DataTable ProviderClinicTypeGet(Guid? id, Guid? providerId, Guid? clinicTypeId)
        {
            return this.ExecuteQuery("dbo.ProviderClinicTypeGet", id, providerId, clinicTypeId);
        }

        public int ProviderClinicTypeDelete(Guid providerId, Guid? clinicTypeId)
        {
            return this.ExecuteNonQuery("dbo.ProviderClinicTypeDelete", providerId, clinicTypeId);
        }

        #endregion

        #region Speciality & Clinictype Get

        public DataTable SpecialtyGet()
        {
            return this.ExecuteQuery("dbo.SpecialtyGet");
        }

        public DataTable ClinicTypeGet(Guid? id, Guid? specialityId)
        {
            return this.ExecuteQuery("dbo.ClinicTypeGet", id, specialityId);
        }

        #endregion

        #region Slot CRUD

        public int SlotSave(Guid slotId, Guid providerId, Guid specialtyId, Guid clinicTypeId, DateTime startDate, DateTime endDate, int? ubrnNo, int status)
        {
            return this.ExecuteNonQuery("dbo.SlotSave", slotId, providerId, specialtyId, clinicTypeId, startDate, endDate, ubrnNo, status);
        }

        public DataTable SlotGet(Guid? slotId, Guid? providerId, Guid? specialtyId, Guid? clinicTypeId, DateTime? startDateTime, DateTime? endDateTime, int? weekDays, int? ubrnNo, int? status)
        {
            return this.ExecuteQuery("dbo.SlotGet", slotId, providerId, specialtyId, clinicTypeId,
                startDateTime != null ? (startDateTime == DateTime.MinValue ? (DateTime?)null : startDateTime) : null,
                endDateTime != null ? (endDateTime == DateTime.MinValue ? (DateTime?)null : endDateTime) : null
                , weekDays, ubrnNo, status);
        }

        public int SlotDelete(Guid slotId)
        {
            return this.ExecuteNonQuery("dbo.SlotDelete", slotId);
        }

        #endregion
    }
}
