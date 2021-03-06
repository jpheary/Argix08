USE [Tsort]
GO
/****** Object:  Table [dbo].[StatSampleDetail]    Script Date: 04/14/2009 13:55:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StatSampleDetail](
	[ID] [bigint] NOT NULL,
	[ItemNumber] [char](25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ItemCount] [smallint] NOT NULL,
	[DamageCount] [smallint] NOT NULL,
 CONSTRAINT [PK_StatSampleDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[ItemNumber] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF