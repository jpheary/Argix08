USE [Tsort]
GO
/****** Object:  StoredProcedure [dbo].[uspStatSampleVendorUpdate]    Script Date: 05/26/2009 11:15:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspStatSampleVendorUpdate]
(
	@NUMBER 			char(5),
	@STATUS				char(1),
	@NAME				char(30),
	@ADDRESS_LINE1		char(30),
	@ADDRESS_LINE2		char(30),
	@CITY				char(30),
	@STATE				char(2),
	@ZIP				char(5),
	@ZIP4				char(4)
)
AS
	UPDATE	dbo.[VENDOR] 
	SET		NAME=@NAME, STATUS=@STATUS, ADDRESS_LINE1=@ADDRESS_LINE1, ADDRESS_LINE2=@ADDRESS_LINE2, CITY=@CITY, STATE=@STATE, ZIP=@ZIP, ZIP4=@ZIP4
	WHERE	NUMBER = @NUMBER
