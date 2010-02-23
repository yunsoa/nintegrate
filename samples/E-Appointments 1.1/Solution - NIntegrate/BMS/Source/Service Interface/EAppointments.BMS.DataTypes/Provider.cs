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
    public class Provider
    {
        private Guid _id;
        private string _name; 
        private string _location;
        private string _organization;
        private string _email;
        private Double _latitude;
        private Double _longitude;
        private string _conditionsTreated;
        private string _proceduresPerformed;
        private string _exclusions;
        private string _alternativeServices;
        private double _proximity;

        [DataMemberAttribute(IsRequired = true, Name = "Id", Order = 1)]
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [DataMemberAttribute(IsRequired = true, Name = "Name", Order = 2)]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [DataMemberAttribute(IsRequired = true, Name = "Location", Order = 3)]
        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }

        [DataMemberAttribute(IsRequired = true, Name = "Organization", Order = 4)]
        public string Organization
        {
            get { return _organization; }
            set { _organization = value; }
        }

        [DataMemberAttribute(IsRequired = true, Name = "Email", Order = 5)]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        [DataMemberAttribute(IsRequired = true, Name = "Latitude", Order = 6)]
        public Double Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }

        [DataMemberAttribute(IsRequired = true, Name = "Longitude", Order = 7)]
        public Double Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }

        [DataMemberAttribute(IsRequired = true, Name = "ConditionsTreated", Order = 8)]
        public string ConditionsTreated
        {
            get { return _conditionsTreated; }
            set { _conditionsTreated = value; }
        }

        [DataMemberAttribute(IsRequired = true, Name = "ProceduresPerformed", Order = 9)]
        public string ProceduresPerformed
        {
            get { return _proceduresPerformed; }
            set { _proceduresPerformed = value; }
        }

        [DataMemberAttribute(IsRequired = true, Name = "Exclusions", Order = 10)]
        public string Exclusions
        {
            get { return _exclusions; }
            set { _exclusions = value; }
        }

        [DataMemberAttribute(IsRequired = true, Name = "AlternativeServices", Order = 11)]
        public string AlternativeServices
        {
            get { return _alternativeServices; }
            set { _alternativeServices = value; }
        }

        [DataMemberAttribute(IsRequired = true, Name = "Proximity", Order = 12)]
        public double Proximity
        {
            get { return _proximity; }
            set { _proximity = value; }
        }
    }

    [DataContract]
    public class ProviderSearchCriteria
    {  
        private Guid _id;
        private Guid _specialtyId;
        private Guid _clinicTypeId;
        private double _withinMiles;
        private String _zipCode;
        private string _keywords;

        [DataMemberAttribute(IsRequired = false, Name = "Id", Order = 1)]
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "SpecialtyId", Order = 2)]
        public Guid SpecialtyId
        {
            get { return _specialtyId; }
            set { _specialtyId = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "ClinicTypesId", Order = 3)]
        public Guid ClinicTypeId
        {
            get { return _clinicTypeId; }
            set { _clinicTypeId = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "WithinMiles", Order = 4)]
        public double WithinMiles
        {
            get { return _withinMiles; }
            set { _withinMiles = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "ZipCode", Order = 5)]
        public string ZipCode
        {
            get { return _zipCode; }
            set { _zipCode = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "KeyWords", Order = 6)]
        public string KeyWords
        {
            get { return _keywords; }
            set { _keywords = value; }
        }
    }

    [CollectionDataContract]
    public class ProviderCollection : List<Provider>
    {
        public ProviderCollection() { }

        public ProviderCollection(IEnumerable<Provider> collection) : base(collection) { }
    }

    [DataContract]
    public class Slot
    {
        private Guid _id;
        private Provider _provider;
        private ClinicType _clinicType;
        private DateTime _startDateTime;
        private DateTime _endDateTime;
        private Int32? _ubrn;
        private SlotStatus _status;

        [DataMemberAttribute(IsRequired = true, Name = "Id", Order = 1)]
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "Provider", Order = 2)]
        public Provider Provider
        {
            get { return _provider; }
            set { _provider = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "ClinicType", Order = 3)]
        public ClinicType ClinicType
        {
            get { return _clinicType; }
            set { _clinicType = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "StartDateTime", Order = 4)]
        public DateTime StartDateTime
        {
            get { return _startDateTime; }
            set { _startDateTime = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "EndDateTime", Order = 5)]
        public DateTime EndDateTime
        {
            get { return _endDateTime; }
            set { _endDateTime = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "UBRN", Order = 6)]
        public Nullable<Int32> Ubrn
        {
            get { return _ubrn; }
            set { _ubrn = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "Status", Order = 7)]
        public SlotStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }
    }

    [DataContract]
    public enum SlotStatus
    {
        [EnumMember]
        All = 0,

        [EnumMember]
        Available = 1,

        [EnumMember]
        Booked = 2

    }

    [DataContract]
    public class SlotSearchCriteria
    {
        private Guid? _id;
        private Guid? _providerId;
        private Guid? _specialtyId;
        private Guid? _clinicTypeId;
        private DateTime? _startDateTime;
        private DateTime? _endDateTime;
        private Int32? _weekDays;
        private Int32? _status;
        
        [DataMemberAttribute(IsRequired = false, Name = "Id", Order = 1)]
        public Nullable<Guid> Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "ProviderId", Order = 2)]
        public Nullable<Guid> ProviderId
        {
            get { return _providerId; }
            set { _providerId = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "SpecialtyId", Order = 3)]
        public Nullable<Guid> SpecialtyId
        {
            get { return _specialtyId; }
            set { _specialtyId = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "ClinicTypeId", Order = 4)]
        public Nullable<Guid> ClinicTypeId
        {
            get { return _clinicTypeId; }
            set { _clinicTypeId = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "StartDateTime", Order = 5)]
        public Nullable<DateTime> StartDateTime
        {
            get { return _startDateTime; }
            set { _startDateTime = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "EndDateTime", Order = 6)]
        public Nullable<DateTime> EndDateTime
        {
            get { return _endDateTime; }
            set { _endDateTime = value; }
        }

        [DataMemberAttribute(IsRequired = false, Name = "WeekDays", Order = 7)]
        public Nullable<Int32> WeekDays
        {
            get { return _weekDays; }
            set { _weekDays = value; }
        }
        [DataMemberAttribute(IsRequired = false, Name = "Status", Order = 8)]
        public Int32? Status
        {
            get { return _status; }
            set { _status = value; }
        }
    }

    [CollectionDataContract]
    public class SlotCollection : List<Slot>
    {
        public SlotCollection() { }

        public SlotCollection(IEnumerable<Slot> collection) : base(collection) { }
    }

}
