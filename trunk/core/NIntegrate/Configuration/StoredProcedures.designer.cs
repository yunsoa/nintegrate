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
	
	
	[System.Data.Linq.Mapping.DatabaseAttribute(Name="ServiceConfig")]
	public partial class StoredProceduresDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public StoredProceduresDataContext() : 
				base(global::NIntegrate.Properties.Settings.Default.ServiceConfigConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public StoredProceduresDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
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
		
		[Function(Name="dbo.sp_GetClientEndpoints")]
		public ISingleResult<sp_GetClientEndpointsResult> sp_GetClientEndpoints([Parameter(Name="ServiceContract", DbType="VarChar(255)")] string serviceContract, [Parameter(Name="Version", DbType="VarChar(50)")] string version, [Parameter(Name="ServerName", DbType="VarChar(50)")] string serverName)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), serviceContract, version, serverName);
			return ((ISingleResult<sp_GetClientEndpointsResult>)(result.ReturnValue));
		}
		
		[Function(Name="dbo.sp_GetServerEndpoints")]
		public ISingleResult<sp_GetServerEndpointsResult> sp_GetServerEndpoints([Parameter(Name="ServiceContract", DbType="VarChar(255)")] string serviceContract, [Parameter(Name="Version", DbType="VarChar(50)")] string version, [Parameter(Name="ServerName", DbType="VarChar(50)")] string serverName)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), serviceContract, version, serverName);
			return ((ISingleResult<sp_GetServerEndpointsResult>)(result.ReturnValue));
		}
		
		[Function(Name="dbo.sp_GetConnectionString")]
		public ISingleResult<sp_GetConnectionStringResult> sp_GetConnectionString([Parameter(Name="ConnectionStringName", DbType="VarChar(100)")] string connectionStringName, [Parameter(Name="ServerName", DbType="VarChar(50)")] string serverName)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), connectionStringName, serverName);
			return ((ISingleResult<sp_GetConnectionStringResult>)(result.ReturnValue));
		}
	}
	
	public partial class sp_GetClientEndpointsResult
	{
		
		private string _Address;
		
		private string _FarmAddress;
		
		private string _ChannelType;
		
		private int _OpenTimeout;
		
		private int _CloseTimeout;
		
		private int _ReceiveTimeout;
		
		private int _SendTimeout;
		
		private string _TransferMode;
		
		private int _ListenBacklog;
		
		private int _MaxBufferPoolSize;
		
		private int _MaxBufferSize;
		
		private int _MaxConnections;
		
		private int _MaxReceivedMessageSize;
		
		private bool _PortSharingEnabled;
		
		private string _SecurityMode;
		
		private string _ClientCredentialTypeName;
		
		private bool _IncludeExceptionDetailInFaults;
		
		private int _MaxConcurrentCalls;
		
		private int _MaxConcurrentSessions;
		
		private int _MaxConcurrentInstances;
		
		private bool _TransactionFlow;
		
		private int _TransactionTimeout;
		
		private bool _ReliableSessionEnabled;
		
		private int _ReliableSessionInactivityTimeout;
		
		private bool _ReliableSessionOrdered;
		
		public sp_GetClientEndpointsResult()
		{
		}
		
		[Column(Storage="_Address", DbType="VarChar(255) NOT NULL", CanBeNull=false)]
		public string Address
		{
			get
			{
				return this._Address;
			}
			set
			{
				if ((this._Address != value))
				{
					this._Address = value;
				}
			}
		}
		
		[Column(Storage="_FarmAddress", DbType="VarChar(255) NOT NULL", CanBeNull=false)]
		public string FarmAddress
		{
			get
			{
				return this._FarmAddress;
			}
			set
			{
				if ((this._FarmAddress != value))
				{
					this._FarmAddress = value;
				}
			}
		}
		
		[Column(Storage="_ChannelType", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string ChannelType
		{
			get
			{
				return this._ChannelType;
			}
			set
			{
				if ((this._ChannelType != value))
				{
					this._ChannelType = value;
				}
			}
		}
		
		[Column(Storage="_OpenTimeout", DbType="Int NOT NULL")]
		public int OpenTimeout
		{
			get
			{
				return this._OpenTimeout;
			}
			set
			{
				if ((this._OpenTimeout != value))
				{
					this._OpenTimeout = value;
				}
			}
		}
		
		[Column(Storage="_CloseTimeout", DbType="Int NOT NULL")]
		public int CloseTimeout
		{
			get
			{
				return this._CloseTimeout;
			}
			set
			{
				if ((this._CloseTimeout != value))
				{
					this._CloseTimeout = value;
				}
			}
		}
		
		[Column(Storage="_ReceiveTimeout", DbType="Int NOT NULL")]
		public int ReceiveTimeout
		{
			get
			{
				return this._ReceiveTimeout;
			}
			set
			{
				if ((this._ReceiveTimeout != value))
				{
					this._ReceiveTimeout = value;
				}
			}
		}
		
		[Column(Storage="_SendTimeout", DbType="Int NOT NULL")]
		public int SendTimeout
		{
			get
			{
				return this._SendTimeout;
			}
			set
			{
				if ((this._SendTimeout != value))
				{
					this._SendTimeout = value;
				}
			}
		}
		
		[Column(Storage="_TransferMode", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string TransferMode
		{
			get
			{
				return this._TransferMode;
			}
			set
			{
				if ((this._TransferMode != value))
				{
					this._TransferMode = value;
				}
			}
		}
		
		[Column(Storage="_ListenBacklog", DbType="Int NOT NULL")]
		public int ListenBacklog
		{
			get
			{
				return this._ListenBacklog;
			}
			set
			{
				if ((this._ListenBacklog != value))
				{
					this._ListenBacklog = value;
				}
			}
		}
		
		[Column(Storage="_MaxBufferPoolSize", DbType="Int NOT NULL")]
		public int MaxBufferPoolSize
		{
			get
			{
				return this._MaxBufferPoolSize;
			}
			set
			{
				if ((this._MaxBufferPoolSize != value))
				{
					this._MaxBufferPoolSize = value;
				}
			}
		}
		
		[Column(Storage="_MaxBufferSize", DbType="Int NOT NULL")]
		public int MaxBufferSize
		{
			get
			{
				return this._MaxBufferSize;
			}
			set
			{
				if ((this._MaxBufferSize != value))
				{
					this._MaxBufferSize = value;
				}
			}
		}
		
		[Column(Storage="_MaxConnections", DbType="Int NOT NULL")]
		public int MaxConnections
		{
			get
			{
				return this._MaxConnections;
			}
			set
			{
				if ((this._MaxConnections != value))
				{
					this._MaxConnections = value;
				}
			}
		}
		
		[Column(Storage="_MaxReceivedMessageSize", DbType="Int NOT NULL")]
		public int MaxReceivedMessageSize
		{
			get
			{
				return this._MaxReceivedMessageSize;
			}
			set
			{
				if ((this._MaxReceivedMessageSize != value))
				{
					this._MaxReceivedMessageSize = value;
				}
			}
		}
		
		[Column(Storage="_PortSharingEnabled", DbType="Bit NOT NULL")]
		public bool PortSharingEnabled
		{
			get
			{
				return this._PortSharingEnabled;
			}
			set
			{
				if ((this._PortSharingEnabled != value))
				{
					this._PortSharingEnabled = value;
				}
			}
		}
		
		[Column(Storage="_SecurityMode", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string SecurityMode
		{
			get
			{
				return this._SecurityMode;
			}
			set
			{
				if ((this._SecurityMode != value))
				{
					this._SecurityMode = value;
				}
			}
		}
		
		[Column(Storage="_ClientCredentialTypeName", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string ClientCredentialTypeName
		{
			get
			{
				return this._ClientCredentialTypeName;
			}
			set
			{
				if ((this._ClientCredentialTypeName != value))
				{
					this._ClientCredentialTypeName = value;
				}
			}
		}
		
		[Column(Storage="_IncludeExceptionDetailInFaults", DbType="Bit NOT NULL")]
		public bool IncludeExceptionDetailInFaults
		{
			get
			{
				return this._IncludeExceptionDetailInFaults;
			}
			set
			{
				if ((this._IncludeExceptionDetailInFaults != value))
				{
					this._IncludeExceptionDetailInFaults = value;
				}
			}
		}
		
		[Column(Storage="_MaxConcurrentCalls", DbType="Int NOT NULL")]
		public int MaxConcurrentCalls
		{
			get
			{
				return this._MaxConcurrentCalls;
			}
			set
			{
				if ((this._MaxConcurrentCalls != value))
				{
					this._MaxConcurrentCalls = value;
				}
			}
		}
		
		[Column(Storage="_MaxConcurrentSessions", DbType="Int NOT NULL")]
		public int MaxConcurrentSessions
		{
			get
			{
				return this._MaxConcurrentSessions;
			}
			set
			{
				if ((this._MaxConcurrentSessions != value))
				{
					this._MaxConcurrentSessions = value;
				}
			}
		}
		
		[Column(Storage="_MaxConcurrentInstances", DbType="Int NOT NULL")]
		public int MaxConcurrentInstances
		{
			get
			{
				return this._MaxConcurrentInstances;
			}
			set
			{
				if ((this._MaxConcurrentInstances != value))
				{
					this._MaxConcurrentInstances = value;
				}
			}
		}
		
		[Column(Storage="_TransactionFlow", DbType="Bit NOT NULL")]
		public bool TransactionFlow
		{
			get
			{
				return this._TransactionFlow;
			}
			set
			{
				if ((this._TransactionFlow != value))
				{
					this._TransactionFlow = value;
				}
			}
		}
		
		[Column(Storage="_TransactionTimeout", DbType="Int NOT NULL")]
		public int TransactionTimeout
		{
			get
			{
				return this._TransactionTimeout;
			}
			set
			{
				if ((this._TransactionTimeout != value))
				{
					this._TransactionTimeout = value;
				}
			}
		}
		
		[Column(Storage="_ReliableSessionEnabled", DbType="Bit NOT NULL")]
		public bool ReliableSessionEnabled
		{
			get
			{
				return this._ReliableSessionEnabled;
			}
			set
			{
				if ((this._ReliableSessionEnabled != value))
				{
					this._ReliableSessionEnabled = value;
				}
			}
		}
		
		[Column(Storage="_ReliableSessionInactivityTimeout", DbType="Int NOT NULL")]
		public int ReliableSessionInactivityTimeout
		{
			get
			{
				return this._ReliableSessionInactivityTimeout;
			}
			set
			{
				if ((this._ReliableSessionInactivityTimeout != value))
				{
					this._ReliableSessionInactivityTimeout = value;
				}
			}
		}
		
		[Column(Storage="_ReliableSessionOrdered", DbType="Bit NOT NULL")]
		public bool ReliableSessionOrdered
		{
			get
			{
				return this._ReliableSessionOrdered;
			}
			set
			{
				if ((this._ReliableSessionOrdered != value))
				{
					this._ReliableSessionOrdered = value;
				}
			}
		}
	}
	
	public partial class sp_GetServerEndpointsResult
	{
		
		private string _Address;
		
		private string _FarmAddress;
		
		private string _ChannelType;
		
		private int _OpenTimeout;
		
		private int _CloseTimeout;
		
		private int _ReceiveTimeout;
		
		private int _SendTimeout;
		
		private string _TransferMode;
		
		private int _ListenBacklog;
		
		private int _MaxBufferPoolSize;
		
		private int _MaxBufferSize;
		
		private int _MaxConnections;
		
		private int _MaxReceivedMessageSize;
		
		private bool _PortSharingEnabled;
		
		private string _SecurityMode;
		
		private string _ClientCredentialTypeName;
		
		private bool _IncludeExceptionDetailInFaults;
		
		private int _MaxConcurrentCalls;
		
		private int _MaxConcurrentSessions;
		
		private int _MaxConcurrentInstances;
		
		private bool _TransactionFlow;
		
		private int _TransactionTimeout;
		
		private bool _ReliableSessionEnabled;
		
		private int _ReliableSessionInactivityTimeout;
		
		private bool _ReliableSessionOrdered;
		
		public sp_GetServerEndpointsResult()
		{
		}
		
		[Column(Storage="_Address", DbType="VarChar(255) NOT NULL", CanBeNull=false)]
		public string Address
		{
			get
			{
				return this._Address;
			}
			set
			{
				if ((this._Address != value))
				{
					this._Address = value;
				}
			}
		}
		
		[Column(Storage="_FarmAddress", DbType="VarChar(255) NOT NULL", CanBeNull=false)]
		public string FarmAddress
		{
			get
			{
				return this._FarmAddress;
			}
			set
			{
				if ((this._FarmAddress != value))
				{
					this._FarmAddress = value;
				}
			}
		}
		
		[Column(Storage="_ChannelType", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string ChannelType
		{
			get
			{
				return this._ChannelType;
			}
			set
			{
				if ((this._ChannelType != value))
				{
					this._ChannelType = value;
				}
			}
		}
		
		[Column(Storage="_OpenTimeout", DbType="Int NOT NULL")]
		public int OpenTimeout
		{
			get
			{
				return this._OpenTimeout;
			}
			set
			{
				if ((this._OpenTimeout != value))
				{
					this._OpenTimeout = value;
				}
			}
		}
		
		[Column(Storage="_CloseTimeout", DbType="Int NOT NULL")]
		public int CloseTimeout
		{
			get
			{
				return this._CloseTimeout;
			}
			set
			{
				if ((this._CloseTimeout != value))
				{
					this._CloseTimeout = value;
				}
			}
		}
		
		[Column(Storage="_ReceiveTimeout", DbType="Int NOT NULL")]
		public int ReceiveTimeout
		{
			get
			{
				return this._ReceiveTimeout;
			}
			set
			{
				if ((this._ReceiveTimeout != value))
				{
					this._ReceiveTimeout = value;
				}
			}
		}
		
		[Column(Storage="_SendTimeout", DbType="Int NOT NULL")]
		public int SendTimeout
		{
			get
			{
				return this._SendTimeout;
			}
			set
			{
				if ((this._SendTimeout != value))
				{
					this._SendTimeout = value;
				}
			}
		}
		
		[Column(Storage="_TransferMode", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string TransferMode
		{
			get
			{
				return this._TransferMode;
			}
			set
			{
				if ((this._TransferMode != value))
				{
					this._TransferMode = value;
				}
			}
		}
		
		[Column(Storage="_ListenBacklog", DbType="Int NOT NULL")]
		public int ListenBacklog
		{
			get
			{
				return this._ListenBacklog;
			}
			set
			{
				if ((this._ListenBacklog != value))
				{
					this._ListenBacklog = value;
				}
			}
		}
		
		[Column(Storage="_MaxBufferPoolSize", DbType="Int NOT NULL")]
		public int MaxBufferPoolSize
		{
			get
			{
				return this._MaxBufferPoolSize;
			}
			set
			{
				if ((this._MaxBufferPoolSize != value))
				{
					this._MaxBufferPoolSize = value;
				}
			}
		}
		
		[Column(Storage="_MaxBufferSize", DbType="Int NOT NULL")]
		public int MaxBufferSize
		{
			get
			{
				return this._MaxBufferSize;
			}
			set
			{
				if ((this._MaxBufferSize != value))
				{
					this._MaxBufferSize = value;
				}
			}
		}
		
		[Column(Storage="_MaxConnections", DbType="Int NOT NULL")]
		public int MaxConnections
		{
			get
			{
				return this._MaxConnections;
			}
			set
			{
				if ((this._MaxConnections != value))
				{
					this._MaxConnections = value;
				}
			}
		}
		
		[Column(Storage="_MaxReceivedMessageSize", DbType="Int NOT NULL")]
		public int MaxReceivedMessageSize
		{
			get
			{
				return this._MaxReceivedMessageSize;
			}
			set
			{
				if ((this._MaxReceivedMessageSize != value))
				{
					this._MaxReceivedMessageSize = value;
				}
			}
		}
		
		[Column(Storage="_PortSharingEnabled", DbType="Bit NOT NULL")]
		public bool PortSharingEnabled
		{
			get
			{
				return this._PortSharingEnabled;
			}
			set
			{
				if ((this._PortSharingEnabled != value))
				{
					this._PortSharingEnabled = value;
				}
			}
		}
		
		[Column(Storage="_SecurityMode", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string SecurityMode
		{
			get
			{
				return this._SecurityMode;
			}
			set
			{
				if ((this._SecurityMode != value))
				{
					this._SecurityMode = value;
				}
			}
		}
		
		[Column(Storage="_ClientCredentialTypeName", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string ClientCredentialTypeName
		{
			get
			{
				return this._ClientCredentialTypeName;
			}
			set
			{
				if ((this._ClientCredentialTypeName != value))
				{
					this._ClientCredentialTypeName = value;
				}
			}
		}
		
		[Column(Storage="_IncludeExceptionDetailInFaults", DbType="Bit NOT NULL")]
		public bool IncludeExceptionDetailInFaults
		{
			get
			{
				return this._IncludeExceptionDetailInFaults;
			}
			set
			{
				if ((this._IncludeExceptionDetailInFaults != value))
				{
					this._IncludeExceptionDetailInFaults = value;
				}
			}
		}
		
		[Column(Storage="_MaxConcurrentCalls", DbType="Int NOT NULL")]
		public int MaxConcurrentCalls
		{
			get
			{
				return this._MaxConcurrentCalls;
			}
			set
			{
				if ((this._MaxConcurrentCalls != value))
				{
					this._MaxConcurrentCalls = value;
				}
			}
		}
		
		[Column(Storage="_MaxConcurrentSessions", DbType="Int NOT NULL")]
		public int MaxConcurrentSessions
		{
			get
			{
				return this._MaxConcurrentSessions;
			}
			set
			{
				if ((this._MaxConcurrentSessions != value))
				{
					this._MaxConcurrentSessions = value;
				}
			}
		}
		
		[Column(Storage="_MaxConcurrentInstances", DbType="Int NOT NULL")]
		public int MaxConcurrentInstances
		{
			get
			{
				return this._MaxConcurrentInstances;
			}
			set
			{
				if ((this._MaxConcurrentInstances != value))
				{
					this._MaxConcurrentInstances = value;
				}
			}
		}
		
		[Column(Storage="_TransactionFlow", DbType="Bit NOT NULL")]
		public bool TransactionFlow
		{
			get
			{
				return this._TransactionFlow;
			}
			set
			{
				if ((this._TransactionFlow != value))
				{
					this._TransactionFlow = value;
				}
			}
		}
		
		[Column(Storage="_TransactionTimeout", DbType="Int NOT NULL")]
		public int TransactionTimeout
		{
			get
			{
				return this._TransactionTimeout;
			}
			set
			{
				if ((this._TransactionTimeout != value))
				{
					this._TransactionTimeout = value;
				}
			}
		}
		
		[Column(Storage="_ReliableSessionEnabled", DbType="Bit NOT NULL")]
		public bool ReliableSessionEnabled
		{
			get
			{
				return this._ReliableSessionEnabled;
			}
			set
			{
				if ((this._ReliableSessionEnabled != value))
				{
					this._ReliableSessionEnabled = value;
				}
			}
		}
		
		[Column(Storage="_ReliableSessionInactivityTimeout", DbType="Int NOT NULL")]
		public int ReliableSessionInactivityTimeout
		{
			get
			{
				return this._ReliableSessionInactivityTimeout;
			}
			set
			{
				if ((this._ReliableSessionInactivityTimeout != value))
				{
					this._ReliableSessionInactivityTimeout = value;
				}
			}
		}
		
		[Column(Storage="_ReliableSessionOrdered", DbType="Bit NOT NULL")]
		public bool ReliableSessionOrdered
		{
			get
			{
				return this._ReliableSessionOrdered;
			}
			set
			{
				if ((this._ReliableSessionOrdered != value))
				{
					this._ReliableSessionOrdered = value;
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
