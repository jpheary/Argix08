USE [Tsort]
GO
/****** Object:  StoredProcedure [dbo].[uspStatSampleHeaderGet]    Script Date: 04/14/2009 13:57:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspStatSampleHeaderGet]
	@VendorCartonNumber	CHAR(25),
	@VendorNumber	CHAR(5)

AS	  
	SELECT VendorCartonNumber, VendorNumber, SampleDate, TransactionDate
	FROM StatSampleHeader
	WHERE  VendorCartonNumber=@VendorCartonNumber  AND VendorNumber = @VendorNumber

