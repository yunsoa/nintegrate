SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Teddy Ma
-- Create date: 2009-4-6
-- Description:	Get all service host types.
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetAllServiceHostTypes]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM ServiceHostType_lkp
END

GO


