SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Teddy Ma
-- Create date: 2009-3-2
-- Description:	Get Connection String from 
-- ConnectionString table by Server Name
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetConnectionString]
	@ConnectionStringName as varchar(100)
	,@ServerName as varchar(50)
AS
BEGIN
	SET NOCOUNT ON;

    select [Value], [ProviderName] from ConnectionString
    where Name = @ConnectionStringName
		and Environment_id = (
			select Environment_id from Farm where Farm_id = (
				select Farm_id from [Server] where ServerName = @ServerName
			)
		)
END

GO


