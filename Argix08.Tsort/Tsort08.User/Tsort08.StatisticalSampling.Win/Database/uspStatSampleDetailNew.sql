USE [Tsort]
GO
/****** Object:  StoredProcedure [dbo].[uspStatSampleDetailNew]    Script Date: 04/14/2009 13:56:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspStatSampleDetailNew]
	@ID				BIGINT,
	@ISBNNumber		CHAR(25),
	@ISBNCount		SMALLINT,
	@DamageCount	SMALLINT

AS	  
	DECLARE  @Error INT, @RowCount INT	

	INSERT INTO StatSampleDetail (ID, ItemNumber, ItemCount, DamageCount)
	VALUES (@ID, @ISBNNumber, @ISBNCount, @DamageCount)
	
	SELECT @Error = @@ERROR, @RowCount = @@ROWCOUNT
	IF  @Error <> 0  GOTO ErrorHandler
	IF  @RowCount = 0
	    BEGIN	
     	    RAISERROR ('Unable to create Stat Sample Detail' , 16, 1) WITH SETERROR
	        SET @Error = @@ERROR
	        GOTO ErrorHandler
	    END	
	RETURN @@ERROR
ErrorHandler:
	RETURN @Error
	