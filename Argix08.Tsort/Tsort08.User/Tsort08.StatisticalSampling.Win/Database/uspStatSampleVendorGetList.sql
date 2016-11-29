USE [Tsort]
GO
/****** Object:  StoredProcedure [dbo].[uspStatSampleVendorGetList]    Script Date: 04/14/2009 13:57:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspStatSampleVendorGetList]
	
AS
	SET NOCOUNT ON
	SELECT NUMBER, NAME, NUMBER + ' ' + NAME AS DESCRIPTION, STATUS, ADDRESS_LINE1 AS ADDRESS, CITY, STATE, ZIP 
    FROM VENDOR 



