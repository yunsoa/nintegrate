SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Teddy Ma
-- Create date: 2009-4-6
-- Description:	Get all custom behavior types.
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetAllCustomBehaviorTypes]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM CustomBehaviorType_lkp
END

GO


