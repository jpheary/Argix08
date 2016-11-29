USE [Tsort]
GO
/****** Object:  StoredProcedure [dbo].[uspStatSampleUpdate]    Script Date: 04/14/2009 13:57:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspStatSampleUpdate]
	@ID				BIGINT

AS	  
	UPDATE	StatSampleHeader 
	SET		TransactionDate=GETDATE()
	WHERE	ID = @ID
