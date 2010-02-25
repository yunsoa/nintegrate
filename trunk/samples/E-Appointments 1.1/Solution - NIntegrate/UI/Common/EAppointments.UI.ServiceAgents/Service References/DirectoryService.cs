﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.832
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EAppointments.UI.ServiceAgents.DirectoryService
{
    using System.Runtime.Serialization;
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.datacontract.org/2004/07/EAppointments.BMS.DataTypes")]
    [System.SerializableAttribute()]
    public partial class PatientSearchCriteria : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.Guid> IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.Guid> ReferrerIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FirstNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LastNameField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.Guid> Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.Guid> ReferrerId
        {
            get
            {
                return this.ReferrerIdField;
            }
            set
            {
                this.ReferrerIdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public string FirstName
        {
            get
            {
                return this.FirstNameField;
            }
            set
            {
                this.FirstNameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public string LastName
        {
            get
            {
                return this.LastNameField;
            }
            set
            {
                this.LastNameField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Namespace="http://schemas.datacontract.org/2004/07/EAppointments.BMS.DataTypes", ItemName="Patient")]
    [System.SerializableAttribute()]
    public class PatientCollection : System.Collections.Generic.List<EAppointments.UI.ServiceAgents.DirectoryService.Patient>
    {
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.datacontract.org/2004/07/EAppointments.BMS.DataTypes")]
    [System.SerializableAttribute()]
    public partial class Patient : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private System.Guid IdField;
        
        private string PatientNoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TitleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FirstNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LastNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private char GenderField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> DateOfBirthField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AddressLine1Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AddressLine2Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CountryField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ZipCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ContactNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmailField;
        
        private EAppointments.UI.ServiceAgents.DirectoryService.Referrer ReferrerField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public System.Guid Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string PatientNo
        {
            get
            {
                return this.PatientNoField;
            }
            set
            {
                this.PatientNoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Title
        {
            get
            {
                return this.TitleField;
            }
            set
            {
                this.TitleField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public string FirstName
        {
            get
            {
                return this.FirstNameField;
            }
            set
            {
                this.FirstNameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public string LastName
        {
            get
            {
                return this.LastNameField;
            }
            set
            {
                this.LastNameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public char Gender
        {
            get
            {
                return this.GenderField;
            }
            set
            {
                this.GenderField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=6)]
        public System.Nullable<System.DateTime> DateOfBirth
        {
            get
            {
                return this.DateOfBirthField;
            }
            set
            {
                this.DateOfBirthField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=7)]
        public string AddressLine1
        {
            get
            {
                return this.AddressLine1Field;
            }
            set
            {
                this.AddressLine1Field = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=8)]
        public string AddressLine2
        {
            get
            {
                return this.AddressLine2Field;
            }
            set
            {
                this.AddressLine2Field = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=9)]
        public string City
        {
            get
            {
                return this.CityField;
            }
            set
            {
                this.CityField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=10)]
        public string State
        {
            get
            {
                return this.StateField;
            }
            set
            {
                this.StateField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=11)]
        public string Country
        {
            get
            {
                return this.CountryField;
            }
            set
            {
                this.CountryField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=12)]
        public string ZipCode
        {
            get
            {
                return this.ZipCodeField;
            }
            set
            {
                this.ZipCodeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=13)]
        public string ContactNumber
        {
            get
            {
                return this.ContactNumberField;
            }
            set
            {
                this.ContactNumberField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=14)]
        public string Email
        {
            get
            {
                return this.EmailField;
            }
            set
            {
                this.EmailField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=15)]
        public EAppointments.UI.ServiceAgents.DirectoryService.Referrer Referrer
        {
            get
            {
                return this.ReferrerField;
            }
            set
            {
                this.ReferrerField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.datacontract.org/2004/07/EAppointments.BMS.DataTypes")]
    [System.SerializableAttribute()]
    public partial class Referrer : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private System.Guid IdField;
        
        private string FirstNameField;
        
        private string LastNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ClinicNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmailField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public System.Guid Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=1)]
        public string FirstName
        {
            get
            {
                return this.FirstNameField;
            }
            set
            {
                this.FirstNameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=2)]
        public string LastName
        {
            get
            {
                return this.LastNameField;
            }
            set
            {
                this.LastNameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public string ClinicName
        {
            get
            {
                return this.ClinicNameField;
            }
            set
            {
                this.ClinicNameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public string Email
        {
            get
            {
                return this.EmailField;
            }
            set
            {
                this.EmailField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.datacontract.org/2004/07/EAppointments.BMS.DataTypes")]
    [System.SerializableAttribute()]
    public partial class ReferrerSearchCriteria : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.Guid> IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FirstNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LastNameField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.Guid> Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public string FirstName
        {
            get
            {
                return this.FirstNameField;
            }
            set
            {
                this.FirstNameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public string LastName
        {
            get
            {
                return this.LastNameField;
            }
            set
            {
                this.LastNameField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Namespace="http://schemas.datacontract.org/2004/07/EAppointments.BMS.DataTypes", ItemName="Referrer")]
    [System.SerializableAttribute()]
    public class ReferrerCollection : System.Collections.Generic.List<EAppointments.UI.ServiceAgents.DirectoryService.Referrer>
    {
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Namespace="http://schemas.datacontract.org/2004/07/EAppointments.BMS.DataTypes", ItemName="Specialty")]
    [System.SerializableAttribute()]
    public class SpecialtyCollection : System.Collections.Generic.List<EAppointments.UI.ServiceAgents.DirectoryService.Specialty>
    {
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.datacontract.org/2004/07/EAppointments.BMS.DataTypes")]
    [System.SerializableAttribute()]
    public partial class Specialty : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private System.Guid IdField;
        
        private string NameField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public System.Guid Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Namespace="http://schemas.datacontract.org/2004/07/EAppointments.BMS.DataTypes", ItemName="ClinicType")]
    [System.SerializableAttribute()]
    public class ClinicTypeCollection : System.Collections.Generic.List<EAppointments.UI.ServiceAgents.DirectoryService.ClinicType>
    {
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.datacontract.org/2004/07/EAppointments.BMS.DataTypes")]
    [System.SerializableAttribute()]
    public partial class ClinicType : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private System.Guid IdField;
        
        private EAppointments.UI.ServiceAgents.DirectoryService.Specialty SpecialtyField;
        
        private string NameField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public System.Guid Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public EAppointments.UI.ServiceAgents.DirectoryService.Specialty Specialty
        {
            get
            {
                return this.SpecialtyField;
            }
            set
            {
                this.SpecialtyField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=2)]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.datacontract.org/2004/07/EAppointments.BMS.ServiceContracts")]
    [System.SerializableAttribute()]
    public partial class SystemFault : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid IdField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message
        {
            get
            {
                return this.MessageField;
            }
            set
            {
                this.MessageField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public System.Guid Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.datacontract.org/2004/07/EAppointments.BMS.ServiceContracts")]
    [System.SerializableAttribute()]
    public partial class DirectoryServiceFault : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid IdField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message
        {
            get
            {
                return this.MessageField;
            }
            set
            {
                this.MessageField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public System.Guid Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://EAppointments.BMS.ServiceContracts/Directory/2007/08", ConfigurationName="EAppointments.UI.ServiceAgents.DirectoryService.IDirectoryService")]
    public interface IDirectoryService
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="FindPatient", ReplyAction="http://EAppointments.BMS.ServiceContracts/Directory/2007/08/IDirectoryService/Fin" +
            "dPatientResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(EAppointments.UI.ServiceAgents.DirectoryService.SystemFault), Action="http://EAppointments.BMS.ServiceContracts/Directory/2007/08/IDirectoryService/Fin" +
            "dPatientSystemFaultFault", Name="SystemFault", Namespace="http://schemas.datacontract.org/2004/07/EAppointments.BMS.ServiceContracts")]
        [System.ServiceModel.FaultContractAttribute(typeof(EAppointments.UI.ServiceAgents.DirectoryService.DirectoryServiceFault), Action="http://EAppointments.BMS.ServiceContracts/Directory/2007/08/IDirectoryService/Fin" +
            "dPatientDirectoryServiceFaultFault", Name="DirectoryServiceFault", Namespace="http://schemas.datacontract.org/2004/07/EAppointments.BMS.ServiceContracts")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ServiceModel.Security.SecurityAccessDeniedException), Action="http://EAppointments.BMS.ServiceContracts/Directory/2007/08/IDirectoryService/Fin" +
            "dPatientSecurityAccessDeniedExceptionFault", Name="SecurityAccessDeniedException", Namespace="http://schemas.datacontract.org/2004/07/System.ServiceModel.Security")]
        EAppointments.UI.ServiceAgents.DirectoryService.PatientCollection FindPatient(EAppointments.UI.ServiceAgents.DirectoryService.PatientSearchCriteria criteria);
        
        [System.ServiceModel.OperationContractAttribute(Action="FindReferrer", ReplyAction="http://EAppointments.BMS.ServiceContracts/Directory/2007/08/IDirectoryService/Fin" +
            "dReferrerResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(EAppointments.UI.ServiceAgents.DirectoryService.SystemFault), Action="http://EAppointments.BMS.ServiceContracts/Directory/2007/08/IDirectoryService/Fin" +
            "dReferrerSystemFaultFault", Name="SystemFault", Namespace="http://schemas.datacontract.org/2004/07/EAppointments.BMS.ServiceContracts")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ServiceModel.Security.SecurityAccessDeniedException), Action="http://EAppointments.BMS.ServiceContracts/Directory/2007/08/IDirectoryService/Fin" +
            "dReferrerSecurityAccessDeniedExceptionFault", Name="SecurityAccessDeniedException", Namespace="http://schemas.datacontract.org/2004/07/System.ServiceModel.Security")]
        [System.ServiceModel.FaultContractAttribute(typeof(EAppointments.UI.ServiceAgents.DirectoryService.DirectoryServiceFault), Action="http://EAppointments.BMS.ServiceContracts/Directory/2007/08/IDirectoryService/Fin" +
            "dReferrerDirectoryServiceFaultFault", Name="DirectoryServiceFault", Namespace="http://schemas.datacontract.org/2004/07/EAppointments.BMS.ServiceContracts")]
        EAppointments.UI.ServiceAgents.DirectoryService.ReferrerCollection FindReferrer(EAppointments.UI.ServiceAgents.DirectoryService.ReferrerSearchCriteria criteria);
        
        [System.ServiceModel.OperationContractAttribute(Action="FindSpecialty", ReplyAction="http://EAppointments.BMS.ServiceContracts/Directory/2007/08/IDirectoryService/Fin" +
            "dSpecialtyResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(EAppointments.UI.ServiceAgents.DirectoryService.SystemFault), Action="http://EAppointments.BMS.ServiceContracts/Directory/2007/08/IDirectoryService/Fin" +
            "dSpecialtySystemFaultFault", Name="SystemFault", Namespace="http://schemas.datacontract.org/2004/07/EAppointments.BMS.ServiceContracts")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ServiceModel.Security.SecurityAccessDeniedException), Action="http://EAppointments.BMS.ServiceContracts/Directory/2007/08/IDirectoryService/Fin" +
            "dSpecialtySecurityAccessDeniedExceptionFault", Name="SecurityAccessDeniedException", Namespace="http://schemas.datacontract.org/2004/07/System.ServiceModel.Security")]
        [System.ServiceModel.FaultContractAttribute(typeof(EAppointments.UI.ServiceAgents.DirectoryService.DirectoryServiceFault), Action="http://EAppointments.BMS.ServiceContracts/Directory/2007/08/IDirectoryService/Fin" +
            "dSpecialtyDirectoryServiceFaultFault", Name="DirectoryServiceFault", Namespace="http://schemas.datacontract.org/2004/07/EAppointments.BMS.ServiceContracts")]
        EAppointments.UI.ServiceAgents.DirectoryService.SpecialtyCollection FindSpecialty();
        
        [System.ServiceModel.OperationContractAttribute(Action="FindClinicType", ReplyAction="http://EAppointments.BMS.ServiceContracts/Directory/2007/08/IDirectoryService/Fin" +
            "dClinicTypeResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ServiceModel.Security.SecurityAccessDeniedException), Action="http://EAppointments.BMS.ServiceContracts/Directory/2007/08/IDirectoryService/Fin" +
            "dClinicTypeSecurityAccessDeniedExceptionFault", Name="SecurityAccessDeniedException", Namespace="http://schemas.datacontract.org/2004/07/System.ServiceModel.Security")]
        [System.ServiceModel.FaultContractAttribute(typeof(EAppointments.UI.ServiceAgents.DirectoryService.SystemFault), Action="http://EAppointments.BMS.ServiceContracts/Directory/2007/08/IDirectoryService/Fin" +
            "dClinicTypeSystemFaultFault", Name="SystemFault", Namespace="http://schemas.datacontract.org/2004/07/EAppointments.BMS.ServiceContracts")]
        [System.ServiceModel.FaultContractAttribute(typeof(EAppointments.UI.ServiceAgents.DirectoryService.DirectoryServiceFault), Action="http://EAppointments.BMS.ServiceContracts/Directory/2007/08/IDirectoryService/Fin" +
            "dClinicTypeDirectoryServiceFaultFault", Name="DirectoryServiceFault", Namespace="http://schemas.datacontract.org/2004/07/EAppointments.BMS.ServiceContracts")]
        EAppointments.UI.ServiceAgents.DirectoryService.ClinicTypeCollection FindClinicType(System.Guid specialtyId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface IDirectoryServiceChannel : EAppointments.UI.ServiceAgents.DirectoryService.IDirectoryService, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class DirectoryServiceClient : System.ServiceModel.ClientBase<EAppointments.UI.ServiceAgents.DirectoryService.IDirectoryService>, EAppointments.UI.ServiceAgents.DirectoryService.IDirectoryService
    {
        
        public DirectoryServiceClient()
        {
        }
        
        public DirectoryServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName)
        {
        }
        
        public DirectoryServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress)
        {
        }
        
        public DirectoryServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress)
        {
        }
        
        public DirectoryServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public EAppointments.UI.ServiceAgents.DirectoryService.PatientCollection FindPatient(EAppointments.UI.ServiceAgents.DirectoryService.PatientSearchCriteria criteria)
        {
            return base.Channel.FindPatient(criteria);
        }
        
        public EAppointments.UI.ServiceAgents.DirectoryService.ReferrerCollection FindReferrer(EAppointments.UI.ServiceAgents.DirectoryService.ReferrerSearchCriteria criteria)
        {
            return base.Channel.FindReferrer(criteria);
        }
        
        public EAppointments.UI.ServiceAgents.DirectoryService.SpecialtyCollection FindSpecialty()
        {
            return base.Channel.FindSpecialty();
        }
        
        public EAppointments.UI.ServiceAgents.DirectoryService.ClinicTypeCollection FindClinicType(System.Guid specialtyId)
        {
            return base.Channel.FindClinicType(specialtyId);
        }
    }
}