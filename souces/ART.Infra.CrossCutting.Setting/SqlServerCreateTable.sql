USE [ART.Setting]
GO

/****** Object:  Table [dbo].[Setting]    Script Date: 13/10/2017 12:51:36 ******/
DROP TABLE [dbo].[Setting]
GO

/****** Object:  Table [dbo].[Setting]    Script Date: 13/10/2017 12:51:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Setting](
	[Key] [varchar](500) NOT NULL,
	[Value] [varchar](8000) NULL,
 CONSTRAINT [PK_Setting] PRIMARY KEY CLUSTERED 
(
	[Key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


