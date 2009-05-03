Insert   [App]   (AppCode,Name,Description)      Values   ('SP_SSP','Sample - SimpleServiceProvider',NULL)

Insert   [AppVariable]   (AppVariableName,AppCode,Environment_id,Value,Description)      Values   ('NIntegrate.Query.IQueryService, NIntegrate.Query, Version=0.6.0.1, Culture=neutral, PublicKeyToken=e2b9e2165dbdd5e6','SP_SSP',1,'NIntegrate.Query.Command.QueryService, NIntegrate.Query.Command, Version=0.6.0.1, Culture=neutral, PublicKeyToken=e2b9e2165dbdd5e6',NULL)
Insert   [AppVariable]   (AppVariableName,AppCode,Environment_id,Value,Description)      Values   ('SimpleServiceContracts.ISimpleServiceDemo, SimpleServiceContracts','SP_SSP',1,'SimpleServiceImpls.SimpleServiceDemoImpl, SimpleServiceImpls',NULL)

Insert   [Binding]   (BindingType_id,BindingName,BindingXML,MexBindingEnabled)      Values   (11,'Sample - Shared Binding',NULL,1)
Insert   [Binding]   (BindingType_id,BindingName,BindingXML,MexBindingEnabled)      Values   (6,'SimpleServiceDemo Binding 2',NULL,1)

Insert   [ConnectionString]   (Name,Environment_id,Value,ProviderName)      Values   ('Sample - SimpleService',1,'Data Source=vista;Initial Catalog=NIntegrateDemo;Persist Security Info=True;User ID=nbear;Password=nbear','System.Data.SqlClient')

Insert   [ServiceCategory]   (ServiceCategoryName)      Values   ('Sample')

Insert   [Service]   (ServiceName,AppCode,ServiceCategory_id,ServiceBehavior_id,ServiceHostType_id,HostXML)      Values   ('NIntegrate.Query.IQueryService, NIntegrate.Query, Version=0.6.0.1, Culture=neutral, PublicKeyToken=e2b9e2165dbdd5e6','SP_SSP',1,NULL,1,NULL)
Insert   [Service]   (ServiceName,AppCode,ServiceCategory_id,ServiceBehavior_id,ServiceHostType_id,HostXML)      Values   ('SimpleServiceContracts.ISimpleServiceDemo, SimpleServiceContracts','SP_SSP',1,NULL,1,'<host><baseAddresses><add baseAddress="http://{0}/SimpleServiceProvider/SimpleService.svc" /><add baseAddress="net.tcp://{0}/SimpleServiceProvider/SimpleService.svc" /></baseAddresses></host>')

Insert   [Endpoint]   (EndpointName,EndpointAddress,Binding_id,ServiceContract,EndpointBehavior_id,BindingNamespace,IdentityXML,ListenUri,ListenUriMode_id)      Values   ('QueryService','http://{0}/SimpleServiceProvider/QueryService.svc',1,'NIntegrate.Query.IQueryService, NIntegrate.Query, Version=0.6.0.1, Culture=neutral, PublicKeyToken=e2b9e2165dbdd5e6',NULL,NULL,NULL,NULL,NULL)
Insert   [Endpoint]   (EndpointName,EndpointAddress,Binding_id,ServiceContract,EndpointBehavior_id,BindingNamespace,IdentityXML,ListenUri,ListenUriMode_id)      Values   ('SimpleServiceDemo','',1,'SimpleServiceContracts.ISimpleServiceDemo, SimpleServiceContracts',NULL,NULL,NULL,NULL,NULL)
Insert   [Endpoint]   (EndpointName,EndpointAddress,Binding_id,ServiceContract,EndpointBehavior_id,BindingNamespace,IdentityXML,ListenUri,ListenUriMode_id)      Values   ('SimpleServiceDemo Binding 2','',2,'SimpleServiceContracts.ISimpleServiceDemo, SimpleServiceContracts',NULL,NULL,NULL,NULL,NULL)

Insert   [ServiceEndpoint_lnk]   (Service_id,Endpoint_id,Farm_id,Active)      Values   (   1,1,1,1)
Insert   [ServiceEndpoint_lnk]   (Service_id,Endpoint_id,Farm_id,Active)      Values   (   2,2,1,1)
Insert   [ServiceEndpoint_lnk]   (Service_id,Endpoint_id,Farm_id,Active)      Values   (   2,3,1,0)

