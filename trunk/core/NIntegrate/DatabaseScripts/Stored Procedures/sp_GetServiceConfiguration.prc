SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Teddy Ma
-- Create date: 2009-4-8
-- Description:	Get service configuration 
--              by service contract and server name
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetServiceConfiguration]
	@ServiceName as varchar(255)
	,@ServerName as varchar(50)
	,@AppCode as varchar(10)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Service_id as int
	DECLARE @ServiceBehaviorXML as varchar(MAX)
	DECLARE @ServiceHostType_id as int
	DECLARE @HostXML as varchar(MAX)

	SELECT TOP 1 @Service_id = s.Service_id, 
		@ServiceBehaviorXML = sb.BehaviorXML, 
		@ServiceHostType_id = s.ServiceHostType_id, 
		@HostXML = s.HostXML
	FROM [Service] s
		left join Behavior sb on sb.Behavior_id = s.ServiceBehavior_id 
	WHERE s.AppCode = @AppCode and s.ServiceName = @ServiceName 
		and s.Service_id in
			(
				SELECT se.Service_id FROM ServiceEndpoint_lnk se
					inner join [Endpoint] e on se.Active = 1
						and se.Endpoint_id = e.Endpoint_id
				WHERE se.Farm_id in
						(SELECT Farm_id FROM [Server] WHERE ServerName = @ServerName)
			)
			
	SELECT @ServiceBehaviorXML as ServiceBehaviorXML, 
		@ServiceHostType_id as ServiceHostType_id, 
		@HostXML as HostXML
			
	SELECT e.EndpointAddress, e.ServiceContract,
		eb.BehaviorXML as EndpointBehaviorXML,
		e.BindingNamespace, e.IdentityXML, 
		bd.BindingType_id, bd.BindingXML, 
		bd.MexBindingEnabled, bd.AddMexBindingOnly,
		e.ListenUri, e.ListenUriMode_id
	FROM [Endpoint] e
		inner join ServiceEndpoint_lnk se on se.Active = 1
			and se.Endpoint_id = e.Endpoint_id
		inner join [Binding] bd on bd.Binding_id = e.Binding_id 
		left join Behavior eb on e.EndpointBehavior_id = eb.Behavior_id
	WHERE se.Service_id = @Service_id
END
