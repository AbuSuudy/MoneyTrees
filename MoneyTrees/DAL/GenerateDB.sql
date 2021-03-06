USE [master]
GO

CREATE DATABASE [Monzo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Monzo', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Monzo.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Monzo_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Monzo_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO


USE [Monzo]
GO
/****** Object:  Table [dbo].[Tokens]    Script Date: 02/12/2021 20:21:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tokens](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Token] [nvarchar](255) NOT NULL,
	[TokenType] [varchar](255) NULL,
	[CreationDate] [datetime] NOT NULL,
	[RefreshToken] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 02/12/2021 20:21:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[Created] [datetime] NULL,
	[Name] [nvarchar](255) NULL,
	[Address] [nvarchar](255) NULL,
	[Country] [nvarchar](255) NULL,
	[City] [nvarchar](255) NULL,
	[Longitude] [decimal](12, 9) NULL,
	[Latitude] [decimal](12, 9) NULL,
	[Amount] [float] NOT NULL,
	[Currency] [nvarchar](255) NULL,
	[Category] [nvarchar](255) NULL,
	[Logo] [nvarchar](255) NULL,
	[Emoji] [nvarchar](255) NULL,
	[InsertedDate] [datetime] NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Transactions] ADD  CONSTRAINT [DF_Transactions_InsertedDate]  DEFAULT (getdate()) FOR [InsertedDate]
GO
/****** Object:  StoredProcedure [dbo].[SumOfEachCategory]    Script Date: 02/12/2021 20:21:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Abubakar Suudy
-- Create date: 17/08/2021
-- Description:	Sum ammount of each transaction category 
-- =============================================
CREATE PROCEDURE [dbo].[SumOfEachCategory]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT
     ROUND(sum([Amount]) * 100/ (SELECT sum([Amount]) FROM [Monzo].[dbo].[Transactions]where Amount <0 and Category not in ('general','mondo')),1)  as 'Amount',
      [Category]
	  FROM [Monzo].[dbo].[Transactions]
	 where amount <0 and Category not in ('general','mondo')

	GROUP BY [Category]
  order  by ROUND(sum([Amount]) * 100/ (SELECT sum([Amount]) FROM [Monzo].[dbo].[Transactions]where Amount <0  and Category not in ('general','mondo')),1)


END
GO
/****** Object:  StoredProcedure [dbo].[SumOfEachDay]    Script Date: 02/12/2021 20:21:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Abubakar Suudy
-- Create date: 25/08/2021
-- Description:	Sum ammount of transaction for each day 
-- =============================================
CREATE PROCEDURE [dbo].[SumOfEachDay]
	
AS
BEGIN

	SET NOCOUNT ON;

	SELECT 
		   CONVERT(DATE, [Created]) AS 'Date'
		  ,round(SUM([Amount]),2) * -1 as 'Amount'
	  FROM [Monzo].[dbo].[Transactions]
	  where [Amount] < 0
	  GROUP  BY  CONVERT(DATE, [Created])

END
GO
