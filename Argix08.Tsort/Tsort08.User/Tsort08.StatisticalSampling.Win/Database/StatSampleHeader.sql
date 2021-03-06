USE [Tsort]
GO
/****** Object:  Table [dbo].[StatSampleHeader]    Script Date: 04/14/2009 13:55:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StatSampleHeader](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[VendorCartonNumber] [char](25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[VendorNumber] [char](5) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SampleDate] [datetime] NOT NULL CONSTRAINT [DF_StatSampleHeader_SampleDate]  DEFAULT (getdate()),
	[TransactionDate] [datetime] NULL,
 CONSTRAINT [PK_StatSampleHeader] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IXU_StatSampleHeaderVendorCarton] UNIQUE NONCLUSTERED 
(
	[VendorNumber] ASC,
	[VendorCartonNumber] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF