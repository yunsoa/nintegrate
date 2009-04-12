﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3074
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NIntegrate.Configuration
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[System.Data.Linq.Mapping.DatabaseAttribute(Name="NIntegrateConfig")]
	public partial class StoredProceduresDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public StoredProceduresDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public StoredProceduresDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public StoredProceduresDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[Function(Name="dbo.sp_GetAllBindingTypes")]
		public ISingleResult<sp_GetAllBindingTypesResult> sp_GetAllBindingTypes()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<sp_GetAllBindingTypesResult>)(result.ReturnValue));
		}
		
		[Function(Name="dbo.sp_GetAllCustomBehaviorTypes")]
		public ISingleResult<sp_GetAllCustomBehaviorTypesResult> sp_GetAllCustomBehaviorTypes()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<sp_GetAllCustomBehaviorTypesResult>)(result.ReturnValue));
		}
		
		[Function(Name="dbo.sp_GetAllServiceHostTypes")]
		public ISingleResult<sp_GetAllServiceHostTypesResult> sp_GetAllServiceHostTypes()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<sp_GetAllServiceHostTypesResult>)(result.ReturnValue));
		}
		
		[Function(Name="dbo.sp_GetAppVariable")]
		public ISingleResult<sp_GetAppVariableResult> sp_GetAppVariable([Parameter(Name="AppVariableName", DbType="VarChar(50)")] string appVariableName, [Parameter(Name="AppCode", DbType="VarChar(10)")] string appCode, [Parameter(Name="ServerName", DbType="VarChar(50)")] string serverName)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), appVariableName, appCode, serverName);
			return ((ISingleResult<sp_GetAppVariableResult>)(result.ReturnValue));
		}
		
		[Function(Name="dbo.sp_GetClientConfiguration")]
		public ISingleResult<sp_GetClientConfigurationResult> sp_GetClientConfiguration([Parameter(Name="ServiceContract", DbType="VarChar(255)")] string serviceContract, [Parameter(Name="ServerName", DbType="VarChar(50)")] string serverName, [Parameter(Name="AppCode", DbType="VarChar(10)")] string appCode)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), serviceContract, serverName, appCode);
			return ((ISingleResult<sp_GetClientConfigurationResult>)(result.ReturnValue));
		}
		
		[Function(Name="dbo.sp_GetConnectionString")]
		public ISingleResult<sp_GetConnectionStringResult> sp_GetConnectionString([Parameter(Name="ConnectionStringName", DbType="VarChar(100)")] string connectionStringName, [Parameter(Name="ServerName", DbType="VarChar(50)")] string serverName)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), connectionStringName, serverName);
			return ((ISingleResult<sp_GetConnectionStringResult>)(result.ReturnValue));
		}
	}
	
	public partial class sp_GetAllBindingTypesResult
	{
		
		private int _BindingType_id;
		
		private string _BindingTypeFriendlyName;
		
		private string _BindingTypeClassName;
		
		private string _BindingConfigurationElementTypeClassName;
		
		private int _ChannelType_id;
		
		public sp_GetAllBindingTypesResult()
		{
		}
		
		[Column(Storage="_BindingType_id", DbType="Int NOT NULL")]
		public int BindingType_id
		{
			get
			{
				return this._BindingType_id;
			}
			set
			{
				if ((this._BindingType_id != value))
				{
					this._BindingType_id = value;
				}
			}
		}
		
		[Column(Storage="_BindingTypeFriendlyName", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string BindingTypeFriendlyName
		{
			get
			{
				return this._BindingTypeFriendlyName;
			}
			set
			{
				if ((this._BindingTypeFriendlyName != value))
				{
					this._BindingTypeFriendlyName = value;
				}
			}
		}
		
		[Column(Storage="_BindingTypeClassName", DbType="VarChar(255) NOT NULL", CanBeNull=false)]
		public string BindingTypeClassName
		{
			get
			{
				return this._BindingTypeClassName;
			}
			set
			{
				if ((this._BindingTypeClassName != value))
				{
					this._BindingTypeClassName = value;
				}
			}
		}
		
		[Column(Storage="_BindingConfigurationElementTypeClassName", DbType="VarChar(255) NOT NULL", CanBeNull=false)]
		public string BindingConfigurationElementTypeClassName
		{
			get
			{
				return this._BindingConfigurationElementTypeClassName;
			}
			set
			{
				if ((this._BindingConfigurationElementTypeClassName != value))
				{
					this._BindingConfigurationElementTypeClassName = value;
				}
			}
		}
		
		[Column(Storage="_ChannelType_id", DbType="Int NOT NULL")]
		public int ChannelType_id
		{
			get
			{
				return this._ChannelType_id;
			}
			set
			{
				if ((this._ChannelType_id != value))
				{
					this._ChannelType_id = value;
				}
			}
		}
	}
	
	public partial class sp_GetAllCustomBehaviorTypesResult
	{
		
		private int _BehaviorType_id;
		
		private string _BehaviorTypeExtensionName;
		
		private string _BehaviorTypeFriendlyName;
		
		private string _BehaviorTypeClassName;
		
		private string _BehaviorConfigurationElementTypeClassName;
		
		private int _BehaviorCategory_id;
		
		public sp_GetAllCustomBehaviorTypesResult()
		{
		}
		
		[Column(Storage="_BehaviorType_id", DbType="Int NOT NULL")]
		public int BehaviorType_id
		{
			get
			{
				return this._BehaviorType_id;
			}
			set
			{
				if ((this._BehaviorType_id != value))
				{
					this._BehaviorType_id = value;
				}
			}
		}
		
		[Column(Storage="_BehaviorTypeExtensionName", DbType="VarChar(100)")]
		public string BehaviorTypeExtensionName
		{
			get
			{
				return this._BehaviorTypeExtensionName;
			}
			set
			{
				if ((this._BehaviorTypeExtensionName != value))
				{
					this._BehaviorTypeExtensionName = value;
				}
			}
		}
		
		[Column(Storage="_BehaviorTypeFriendlyName", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string BehaviorTypeFriendlyName
		{
			get
			{
				return this._BehaviorTypeFriendlyName;
			}
			set
			{
				if ((this._BehaviorTypeFriendlyName != value))
				{
					this._BehaviorTypeFriendlyName = value;
				}
			}
		}
		
		[Column(Storage="_BehaviorTypeClassName", DbType="VarChar(255) NOT NULL", CanBeNull=false)]
		public string BehaviorTypeClassName
		{
			get
			{
				return this._BehaviorTypeClassName;
			}
			set
			{
				if ((this._BehaviorTypeClassName != value))
				{
					this._BehaviorTypeClassName = value;
				}
			}
		}
		
		[Column(Storage="_BehaviorConfigurationElementTypeClassName", DbType="VarChar(255) NOT NULL", CanBeNull=false)]
		public string BehaviorConfigurationElementTypeClassName
		{
			get
			{
				return this._BehaviorConfigurationElementTypeClassName;
			}
			set
			{
				if ((this._BehaviorConfigurationElementTypeClassName != value))
				{
					this._BehaviorConfigurationElementTypeClassName = value;
				}
			}
		}
		
		[Column(Storage="_BehaviorCategory_id", DbType="Int NOT NULL")]
		public int BehaviorCategory_id
		{
			get
			{
				return this._BehaviorCategory_id;
			}
			set
			{
				if ((this._BehaviorCategory_id != value))
				{
					this._BehaviorCategory_id = value;
				}
			}
		}
	}
	
	public partial class sp_GetAllServiceHostTypesResult
	{
		
		private int _ServiceHostType_id;
		
		private string _ServiceHostTypeFriendlyName;
		
		private string _ServiceHostTypeClassName;
		
		public sp_GetAllServiceHostTypesResult()
		{
		}
		
		[Column(Storage="_ServiceHostType_id", DbType="Int NOT NULL")]
		public int ServiceHostType_id
		{
			get
			{
				return this._ServiceHostType_id;
			}
			set
			{
				if ((this._ServiceHostType_id != value))
				{
					this._ServiceHostType_id = value;
				}
			}
		}
		
		[Column(Storage="_ServiceHostTypeFriendlyName", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string ServiceHostTypeFriendlyName
		{
			get
			{
				return this._ServiceHostTypeFriendlyName;
			}
			set
			{
				if ((this._ServiceHostTypeFriendlyName != value))
				{
					this._ServiceHostTypeFriendlyName = value;
				}
			}
		}
		
		[Column(Storage="_ServiceHostTypeClassName", DbType="VarChar(255) NOT NULL", CanBeNull=false)]
		public string ServiceHostTypeClassName
		{
			get
			{
				return this._ServiceHostTypeClassName;
			}
			set
			{
				if ((this._ServiceHostTypeClassName != value))
				{
					this._ServiceHostTypeClassName = value;
				}
			}
		}
	}
	
	public partial class sp_GetAppVariableResult
	{
		
		private string _Value;
		
		public sp_GetAppVariableResult()
		{
		}
		
		[Column(Storage="_Value", DbType="NVarChar(MAX)")]
		public string Value
		{
			get
			{
				return this._Value;
			}
			set
			{
				if ((this._Value != value))
				{
					this._Value = value;
				}
			}
		}
	}
	
	public partial class sp_GetClientConfigurationResult
	{
		
		private string _HostXML;
		
		private string _EndpointAddress;
		
		private string _BindingNamespace;
		
		private string _EndpointBehaviorXML;
		
		private string _IdentityXML;
		
		private int _BindingType_id;
		
		private string _BindingXML;
		
		public sp_GetClientConfigurationResult()
		{
		}
		
		[Column(Storage="_HostXML", DbType="VarChar(MAX)")]
		public string HostXML
		{
			get
			{
				return this._HostXML;
			}
			set
			{
				if ((this._HostXML != value))
				{
					this._HostXML = value;
				}
			}
		}
		
		[Column(Storage="_EndpointAddress", DbType="VarChar(255) NOT NULL", CanBeNull=false)]
		public string EndpointAddress
		{
			get
			{
				return this._EndpointAddress;
			}
			set
			{
				if ((this._EndpointAddress != value))
				{
					this._EndpointAddress = value;
				}
			}
		}
		
		[Column(Storage="_BindingNamespace", DbType="VarChar(255)")]
		public string BindingNamespace
		{
			get
			{
				return this._BindingNamespace;
			}
			set
			{
				if ((this._BindingNamespace != value))
				{
					this._BindingNamespace = value;
				}
			}
		}
		
		[Column(Storage="_EndpointBehaviorXML", DbType="VarChar(MAX)")]
		public string EndpointBehaviorXML
		{
			get
			{
				return this._EndpointBehaviorXML;
			}
			set
			{
				if ((this._EndpointBehaviorXML != value))
				{
					this._EndpointBehaviorXML = value;
				}
			}
		}
		
		[Column(Storage="_IdentityXML", DbType="VarChar(MAX)")]
		public string IdentityXML
		{
			get
			{
				return this._IdentityXML;
			}
			set
			{
				if ((this._IdentityXML != value))
				{
					this._IdentityXML = value;
				}
			}
		}
		
		[Column(Storage="_BindingType_id", DbType="Int NOT NULL")]
		public int BindingType_id
		{
			get
			{
				return this._BindingType_id;
			}
			set
			{
				if ((this._BindingType_id != value))
				{
					this._BindingType_id = value;
				}
			}
		}
		
		[Column(Storage="_BindingXML", DbType="VarChar(MAX)")]
		public string BindingXML
		{
			get
			{
				return this._BindingXML;
			}
			set
			{
				if ((this._BindingXML != value))
				{
					this._BindingXML = value;
				}
			}
		}
	}
	
	public partial class sp_GetConnectionStringResult
	{
		
		private string _Value;
		
		private string _ProviderName;
		
		public sp_GetConnectionStringResult()
		{
		}
		
		[Column(Storage="_Value", DbType="VarChar(200) NOT NULL", CanBeNull=false)]
		public string Value
		{
			get
			{
				return this._Value;
			}
			set
			{
				if ((this._Value != value))
				{
					this._Value = value;
				}
			}
		}
		
		[Column(Storage="_ProviderName", DbType="VarChar(200)")]
		public string ProviderName
		{
			get
			{
				return this._ProviderName;
			}
			set
			{
				if ((this._ProviderName != value))
				{
					this._ProviderName = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
