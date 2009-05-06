SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Teddy Ma
-- Create date: 2009-3-24
-- Description:	Get App Variable from 
-- AppVariable table by AppCode and Server Name
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetAppVariable]
	@AppVariableName as varchar(255)
	,@AppCode as varchar(10)
	,@ServerName as varchar(50)
AS
BEGIN
	SET NOCOUNT ON;

    select [Value] from AppVariable 
    where AppVariableName = @AppVariableName AND AppCode = @AppCode
		and Environment_id = (
			select Environment_id from Farm where Farm_id = (
				select Farm_id from [Server] where ServerName = @ServerName
			)
		)
END
