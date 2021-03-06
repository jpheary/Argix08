USE [Tsort]
GO
/****** Object:  StoredProcedure [dbo].[uspStatSampleHeaderNew]    Script Date: 04/14/2009 13:57:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspStatSampleHeaderNew]

	@ID					BIGINT OUTPUT,
	@VendorCartonNumber	CHAR(25),
	@VendorNumber		CHAR(5)

AS	  
	DECLARE  @Error INT, @RowCount INT	

	INSERT INTO StatSampleHeader ( VendorCartonNumber, VendorNumber)
	VALUES (@VendorCartonNumber, @VendorNumber)
	SET @ID = @@Identity
	
	SELECT @Error = @@ERROR, @RowCount = @@ROWCOUNT
	IF  @Error <> 0  GOTO ErrorHandler
	IF  @RowCount = 0
	    BEGIN	
     	    RAISERROR ('Unable to create Stat Sample Header' , 16, 1) WITH SETERROR
	        SET @Error = @@ERROR
	        GOTO ErrorHandler
	    END	
	RETURN @@ERROR
ErrorHandler:
	RETURN @Error