USE [OA_14]
GO

/****** Object:  Table [dbo].[Books]    Script Date: 2016/2/14 20:43:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Books](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Author] [nvarchar](50) NOT NULL,
	[PublisherId] [int] NOT NULL,
	[PublishDate] [datetime] NOT NULL,
	[ISBN] [nvarchar](50) NOT NULL,
	[WordsCount] [int] NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[ContentDescription] [nvarchar](max) NOT NULL,
	[AurhorDescription] [nvarchar](max) NOT NULL,
	[EditorComment] [nvarchar](max) NOT NULL,
	[TOC] [nvarchar](max) NOT NULL,
	[CategoryId] [int] NULL,
	[Clicks] [int] NOT NULL,
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'±‡’ﬂµ„∆¿' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Books', @level2type=N'COLUMN',@level2name=N'EditorComment'
GO


