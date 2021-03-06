USE [Tsort]
GO
/****** Object:  Table [dbo].[VENDOR]    Script Date: 04/14/2009 13:56:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[VENDOR](
	[NUMBER] [char](5) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[NAME] [char](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[STATUS] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ADDRESS_LINE1] [char](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ADDRESS_LINE2] [char](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CITY] [char](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[STATE] [char](2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ZIP] [char](5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ZIP4] [char](4) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[USERDATA] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsStore] [tinyint] NULL CONSTRAINT [DF_VENDOR_IsStore]  DEFAULT ((0)),
	[ParentNumber] [char](5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_VENDOR_ParentNumber]  DEFAULT (''),
	[ParentClient] [char](3) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_VENDOR_ParentClient]  DEFAULT (''),
	[ParentDivision] [char](2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_VENDOR_ParentDivision]  DEFAULT (''),
 CONSTRAINT [PK_VENDOR] PRIMARY KEY CLUSTERED 
(
	[NUMBER] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF