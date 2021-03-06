USE [Tsort]
GO
/****** Object:  StoredProcedure [dbo].[uspStatSampleGet]    Script Date: 04/14/2009 13:56:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspStatSampleGet]
	@FromDate		DATETIME,
	@ToDate			DATETIME

AS	  
	SELECT	S.ID, VendorCartonNumber, VendorNumber, V.NAME AS VendorName, D.ItemNumber, D.ItemCount, D.DamageCount, SampleDate, TransactionDate
	FROM	StatSampleHeader AS S
	INNER JOIN StatSampleDetail AS D
			ON D.ID = S.ID
	INNER JOIN VENDOR AS V
			ON V.NUMBER = S.VendorNumber
	WHERE	SampleDate >= @FromDate  AND SampleDate <= @ToDate AND TransactionDate IS NULL

