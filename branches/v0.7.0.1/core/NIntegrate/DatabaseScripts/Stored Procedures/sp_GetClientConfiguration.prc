SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Teddy Ma
-- Create date: 2009-4-8
-- Description:	Get client configuration 
--              by service contract, server name
--				and appCode
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetClientConfiguration]
	@ServiceContract as varchar(255)
	,@ServerName as varchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT TOP 1 s.HostXML, f.FarmAddress, e.EndpointAddress, e.ListenUri, 
		e.ListenUriMode_id, e.BindingNamespace,
		(select BehaviorXML from Behavior where Behavior_id in
			(select ClientEndpointBehavior_id from EndpointClient where Endpoint_id = e.Endpoint_id and ClientFarm_id in 
					(select Farm_id from [Server] where ServerName = @ServerName)
			)
		) as EndpointBehaviorXML,
		e.IdentityXML, bd.BindingType_id, bd.BindingXML 
	FROM [Service] s
		inner join ServiceEndpoint_lnk se on se.Active = 1
			and s.Service_id = se.Service_id
		inner join Farm f on f.Farm_id = se.Farm_id
		inner join [Endpoint] e on se.Endpoint_id = e.Endpoint_id
		inner join [Binding] bd on bd.Binding_id = e.Binding_id and bd.AddMexBindingOnly = 0
		inner join BindingType_lkp bdt on bdt.BindingType_id = bd.BindingType_id		
	WHERE (e.ServiceContract = @ServiceContract
			or e.ServiceContract in
				(select ServiceContract
					from ServiceContractCompatibility
					where CompatibleServiceContract = @ServiceContract
				)
		)
		and se.Farm_id in
			(select fa.ServerFarm_id 
				from FarmAccessibility fa 
				where fa.ChannelType_id = bdt.ChannelType_id
					and fa.ClientFarm_id in 
					(
						select Farm_id 
						from [Server] 
						where ServerName = @ServerName
					)
			)
	ORDER BY bdt.ChannelType_id DESC, s.Service_id DESC
END

