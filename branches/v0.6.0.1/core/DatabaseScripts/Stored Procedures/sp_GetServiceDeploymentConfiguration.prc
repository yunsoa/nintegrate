SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Teddy Ma
-- Create date: 2009-4-21
-- Description:	Get Service Deployment
-- Configuration by ServerName & AppCode
-- =============================================
CREATE PROCEDURE sp_GetServiceDeploymentConfiguration
	@ServerName as varchar(50)
	,@AppCode as varchar(10)
AS
BEGIN
	SELECT s.ServiceName, s.HostXML, e.EndpointAddress, e.ListenUri FROM [Service] s
	inner join dbo.ServiceEndpoint_lnk se on s.Service_id = se.Service_id 
		and se.Active = 1 and se.Farm_id in 
		(SELECT Farm_id FROM [Server] WHERE ServerName = @ServerName)
	inner join [Endpoint] e on se.Endpoint_id = e.Endpoint_id
	WHERE s.AppCode = @AppCode
END
GO
