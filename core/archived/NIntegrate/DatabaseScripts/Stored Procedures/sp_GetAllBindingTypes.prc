SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Teddy Ma
-- Create date: 2009-4-6
-- Description:	Get all binding types.
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetAllBindingTypes]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM BindingType_lkp
END

GO


