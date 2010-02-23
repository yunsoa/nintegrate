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
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections.ObjectModel;
using System.Threading;
using EAppointments.BMS.Security;
using EAppointments.BMS.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;


namespace EAppointments.BMS
{
    public class Application
    {
        private static Application _application;
        private DbLib _dbLib;
        private int? _maxUbrn;
        private Session _session;

        private Application()
        {
            _dbLib = new DbLib();
        }

        private Application(Session session)
        {
            _dbLib = new DbLib();
            _session = session;
        }

        internal DbLib DbLib
        {
            get { return _dbLib; }
        }

        public Session Session
        {
            get { return _session; }
        }

        internal int GetMaxUBRN()
        {
            if (!_maxUbrn.HasValue)
            {
                _maxUbrn = _dbLib.GetMaxUBRN();
            }
            _maxUbrn = _maxUbrn + 1;
            return (int)_maxUbrn;
        }

        internal static Application GetApplication(Session session)
        {
            // TODO : Make it thread safe
            if (_application == null)
            {
                _application = new Application(session);
            }
            return _application;
        }

   
        public Appointment NewAppointment(Patient patient, Referrer referrer, Provider provider, ClinicType clinicType)
        {
            return new Appointment(this, patient, referrer, provider, clinicType);
        }

        public Appointment[] Find(AppointmentSearchCriteria criteria)
        {
            // Apply User security on criteria
            User currentUser = (User)Thread.CurrentPrincipal.Identity;

            switch (currentUser.Role)
            {
                case RoleType.Patient :
                    // Has Access to only own records
                    criteria.PatientId = currentUser.RefUserId;
                    break;

                case RoleType.ProviderClinician :
                    // Has Access to records belong to own Provider
                    criteria.ProviderId = currentUser.RefUserId;
                    break;

                case RoleType.Referrer :
                    // Has Access to records only to referring patients
                    criteria.ReferrerId = currentUser.RefUserId;
                    break;
            }
            
            DataTable dataTable = _dbLib.AppointmentGet(criteria.Ubrn, criteria.PatientId, criteria.ReferrerId, 
                                    criteria.ProviderId, (int?)criteria.Status, criteria.StartDate, criteria.EndDate, 
                                    criteria.WorkflowId);

            List<Appointment> appointmentList = new List<Appointment>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                Appointment appointment = new Appointment(this, dataRow);
                appointmentList.Add(appointment);
            }

            return appointmentList.ToArray();
        }

        public Appointment FindAppointmentById(int ubrn)
        {
            Appointment[] appointments = this.Find(new AppointmentSearchCriteria(ubrn));
            if (appointments.Length == 0)
                throw new IdNotFoundException("Appointment", ubrn);
            return appointments[0];
        }

        public Specialty FindSpecialtyById(Guid id)
        {
            Specialty[] specialties = FindSpecialties();

            foreach (Specialty specialty in specialties)
            {
                if (specialty.Id == id)
                    return specialty;
            }
            
            throw new IdNotFoundException("Specialty", id);
        }

        public Specialty[] FindSpecialties()
        {
            DataTable dataTable = _dbLib.SpecialtyGet();

            List<Specialty> specialties = new List<Specialty>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                Specialty specialty = new Specialty(this, dataRow);
                specialties.Add(specialty);
            }

            specialties.Sort(
                delegate(Specialty a, Specialty b)
            {
                return a.Name.CompareTo(b.Name);
            });

            return specialties.ToArray();
        }

        public ClinicType FindClinicTypeById(Guid id)
        {
            ClinicType[] clinicTypeList = this.Find(new ClinicTypeSearchCriteria(id));
            if (clinicTypeList.Length == 0)
                throw new IdNotFoundException("Specialty", id);
            return clinicTypeList[0];
        }

        public ClinicType[] Find(ClinicTypeSearchCriteria criteria)
        {
            DataTable dataTable = _dbLib.ClinicTypeGet((Guid?)criteria.Id, (Guid?)criteria.SpecialtyId);
            List<ClinicType> clinicTypeList = new List<ClinicType>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                Guid specialtyId = (Guid)dataRow["SpecialtyId"];
                Specialty specialty = this.FindSpecialtyById((Guid)specialtyId);

                ClinicType clinicType = new ClinicType(specialty, dataRow);
                clinicTypeList.Add(clinicType);
            }

            clinicTypeList.Sort(delegate(ClinicType a, ClinicType b)
            {
                return a.Name.CompareTo(b.Name);
            });

            return clinicTypeList.ToArray();
        }

        
        public Patient NewPatient()
        {
            return new Patient(this);
        }

        public Patient[] Find(PatientSearchCriteria criteria)
        {
            // Apply User security on criteria
            User currentUser = (User)Thread.CurrentPrincipal.Identity;

            switch (currentUser.Role)
            {
               case RoleType.Patient:
                    // Has Access to only own records
                    criteria.Id = currentUser.RefUserId;
                    break;

                case RoleType.Referrer:
                    // Has Access to records only to referring patients
                    criteria.ReferrerId = currentUser.RefUserId;
                    break;
            }
            
            DataTable dataTable = _dbLib.PatientGet(criteria.Id, criteria.ReferrerId, criteria.FirstName, criteria.LastName);

            List<Patient> patientList = new List<Patient>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                Patient patient = new Patient(this, dataRow);
                patientList.Add(patient);
            }

            return patientList.ToArray();
        }

        public Patient FindPatientById(Guid id)
        {
            Patient[] patients = this.Find(new PatientSearchCriteria(id));
            if (patients.Length == 0)
                throw new IdNotFoundException("Patient", id);
            return patients[0];
        }

        public Referrer NewReferrer()
        {
            return new Referrer(this);
        }

        public Referrer[] Find(ReferrerSearchCriteria criteria)
        {
            // Apply User security on criteria
            User currentUser = (User)Thread.CurrentPrincipal.Identity;

            switch (currentUser.Role)
            {
                case RoleType.Referrer:
                    // Has Access to records only to referring patients
                    criteria.Id = currentUser.RefUserId;
                    break;                
            }
            
            DataTable dataTable = _dbLib.ReferrerGet(criteria.Id, criteria.FirstName, criteria.LastName);
            
            List<Referrer> referrerList = new List<Referrer>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                Referrer referrer = new Referrer(this, dataRow);
                referrerList.Add(referrer);
            }

            return referrerList.ToArray();
        }

        public Referrer FindReferrerById(Guid id)
        {
            Referrer[] referrers = this.Find(new ReferrerSearchCriteria(id));
            if (referrers.Length == 0)
                throw new IdNotFoundException("Referrer", id);
            return referrers[0];
        }

        public Provider NewProvider()
        {
            return new Provider(this);
        }

        public Provider[] Find(ProviderSearchCriteria criteria) 
        {
            DataTable dataTable = _dbLib.ProviderGet(criteria.Id, criteria.SpecialtyId, criteria.ClinicTypeId, criteria.ZipCode, criteria.WithinMiles, criteria.Keywords);

            List<Provider> providerList = new List<Provider>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                Provider provider = new Provider(this, dataRow);
                providerList.Add(provider);
            }

            return providerList.ToArray();
        }

        public Provider FindProviderById(Guid id)
        {
            Provider[] providers = this.Find(new ProviderSearchCriteria(id));
            if (providers.Length == 0)
                throw new IdNotFoundException("Provider", id);
            return providers[0];
        }

        public Slot NewSlot()
        {
            return new Slot(this);
        }

        public Slot[] Find(SlotSearchCriteria slotSearchCriteria)
        {
            DataTable dataTable = _dbLib.SlotGet(slotSearchCriteria.Id, slotSearchCriteria.ProviderId, slotSearchCriteria.SpecialtyId, slotSearchCriteria.ClinicTypeId, slotSearchCriteria.StartDateTime, slotSearchCriteria.EndDateTime, slotSearchCriteria.WeekDays, slotSearchCriteria.Ubrn, (int)slotSearchCriteria.Status);

            List<Slot> slotList = new List<Slot>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                Slot slot = new Slot(this, dataRow);
                slotList.Add(slot);
            }

            return slotList.ToArray();
        }

        public Slot FindSlotById(Guid id)
        {
            Slot[] slots = this.Find(new SlotSearchCriteria(id));
            if (slots.Length == 0)
                throw new IdNotFoundException("Slot", id);
            return slots[0];
        }

        public static void HandleException(Exception ex)
        {
            if (ex == null)
                return;
            
           if (ExceptionPolicy.HandleException(ex, "Business Layer Policy"))
               throw ex;        
        }

    }    
}
