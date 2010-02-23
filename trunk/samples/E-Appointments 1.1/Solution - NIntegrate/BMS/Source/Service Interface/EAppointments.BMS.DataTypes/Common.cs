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
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace EAppointments.BMS.DataTypes
{

    [DataContract]
    public class Patient
    {
        private Guid _id;
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
        private string _contactNumber;
        private string _email;
        private Referrer _referrer;

        [DataMemberAttribute(IsRequired = true, Name = "Id", Order = 1)]
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [DataMemberAttribute(IsRequired = true, Name = "PatientNo", Order = 2)]
        public string PatientNo
        {
            get { return _patientNo; }
            set { _patientNo = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "Title", Order = 3)]
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "FirstName", Order = 4)]
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "LastName", Order = 5)]
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "Gender", Order = 6)]
        public char Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "DateOfBirth", Order = 7)]
        public Nullable<DateTime> DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "AddressLine1", Order = 8)]
        public string AddressLine1
        {
            get { return _addressLine1; }
            set { _addressLine1 = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "AddressLine2", Order = 9)]
        public string AddressLine2
        {
            get { return _addressLine2; }
            set { _addressLine2 = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "City", Order = 10)]
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "State", Order = 11)]
        public string State
        {
            get { return _state; }
            set { _state = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "Country", Order = 12)]
        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "ZipCode", Order = 13)]
        public string ZipCode
        {
            get { return _zipCode; }
            set { _zipCode = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "ContactNumber", Order = 14)]
        public string ContactNumber
        {
            get { return _contactNumber; }
            set { _contactNumber = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "Email", Order = 15)]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        [DataMemberAttribute(IsRequired = true, Name = "Referrer", Order = 16)]
        public Referrer Referrer
        {
            get { return _referrer; }
            set { _referrer = value; }
        }
    }

    [DataContract]   
    public class PatientSearchCriteria
    {
        private Guid? _id;
        private Guid? _referrerId;
        private String _firstName;
        private String _lastName;
        
        [DataMemberAttribute(IsRequired = false, Name = "Id", Order = 1)]
        public Nullable<Guid> Id
        {
            get { return _id; }
            set { _id = value; }
        }
        
        [DataMemberAttribute(IsRequired = false, Name = "ReferrerId", Order = 2)]
        public Nullable<Guid> ReferrerId
        {
            get { return _referrerId; }
            set { _referrerId = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "FirstName", Order = 3)]
        public String FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "LastName", Order = 4)]
        public String LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }      
    }

    [CollectionDataContract]
    public class PatientCollection : List<Patient>
    {
        public PatientCollection() { }

        public PatientCollection(IEnumerable<Patient> collection) : base(collection) { }
    }

    [DataContract]
    public class Referrer
    {
        private Guid _id;
        private string _firstName;
        private string _lastName;
        private string _clinicName;
        private string _email;

        [DataMemberAttribute(IsRequired = true, Name = "Id", Order = 1)]
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [DataMemberAttribute(IsRequired = true, Name = "FirstName", Order = 2)]
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        [DataMemberAttribute(IsRequired = true, Name = "LastName", Order = 3)]
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "ClinicName", Order = 4)]
        public string ClinicName
        {
            get { return _clinicName; }
            set { _clinicName = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "Email", Order = 5)]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
    }

    [DataContract]
    public class ReferrerSearchCriteria
    {
        private Guid? _id;
        private String _firstName;
        private String _lastName;

        [DataMemberAttribute(IsRequired = false, Name = "Id", Order = 1)]
        public Nullable<Guid> Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "FirstName", Order = 2)]
        public String FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "LastName", Order = 3)]
        public String LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
    }

    [CollectionDataContract]
    public class ReferrerCollection : List<Referrer>
    {
        public ReferrerCollection() { }

        public ReferrerCollection(IEnumerable<Referrer> collection) : base(collection) { }
    }

    [DataContract]
    public class Specialty
    {
        private Guid _id;
        private String _name;

        [DataMemberAttribute(IsRequired = true, Name = "Id", Order = 1)]
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [DataMemberAttribute(IsRequired = true, Name = "Name", Order = 2)]
        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }

    [CollectionDataContract]
    public class SpecialtyCollection : List<Specialty>
    {
        public SpecialtyCollection() { }

        public SpecialtyCollection(IEnumerable<Specialty> collection) : base(collection) { }
    }

    [DataContract] 
    public class ClinicType
    {
        private Guid _id;
        private Specialty _specialty;
        private String _name;

        [DataMemberAttribute(IsRequired = true, Name = "Id", Order = 1)]
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [DataMemberAttribute(IsRequired = true, Name = "Specialty", Order = 2)]
        public Specialty Specialty
        {
            get { return _specialty; }
            set { _specialty = value; }
        }

        [DataMemberAttribute(IsRequired = true, Name = "Name", Order = 3)]
        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

    }

    [CollectionDataContract]
    public class ClinicTypeCollection : List<ClinicType>
    {
        public ClinicTypeCollection() { }

        public ClinicTypeCollection(IEnumerable<ClinicType> collection) : base(collection) { }
    }
}
