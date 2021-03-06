USE [Tsort]
GO
/****** Object:  StoredProcedure [dbo].[uspStatSampleVendorNew]    Script Date: 04/14/2009 13:57:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspStatSampleVendorNew]
(
	@NUMBER 			char(5),
	@NAME				char(30),
	@ADDRESS_LINE1		char(30),
	@ADDRESS_LINE2		char(30),
	@CITY				char(30),
	@STATE				char(2),
	@ZIP				char(5),
	@ZIP4				char(4)
)
AS
	INSERT INTO dbo.[VENDOR] (NUMBER, NAME, STATUS, ADDRESS_LINE1, ADDRESS_LINE2, CITY, STATE, ZIP, ZIP4)
	VALUES(@NUMBER, @NAME, 'A', @ADDRESS_LINE1, @ADDRESS_LINE2, @CITY, @STATE, @ZIP, @ZIP4)
