USE [ART.Log]
GO

/****** Object:  Table [dbo].[Log]    Script Date: 10/10/2017 11:05:52 ******/
DROP TABLE [dbo].[Log]
GO

/****** Object:  Table [dbo].[Log]    Script Date: 10/10/2017 11:05:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Log](
	[Id] [UNIQUEIDENTIFIER] NOT NULL DEFAULT NEWID(),
	[UtcDateTime] [datetime] NOT NULL,
	[Thread] [varchar](255) NOT NULL,
	[Level] [varchar](50) NOT NULL,
	[AppDomain] [varchar](500) NOT NULL,
	[Module] [varchar](500) NULL,
	[Namespace] [varchar](500) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[Method] [varchar](500) NOT NULL,
	[Line] INT NULL,		
	[Message] [varchar](4000) NULL,
	[Exception] [varchar](2000) NULL,
	[Identity] [varchar](256) NULL,
	[StackTrace] [VARCHAR](8000) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


