USE [master]
GO
/****** Object:  Database [CarDB]    Script Date: 20/07/2021 12:40:31 PM ******/
CREATE DATABASE [CarDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CarDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\CarDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CarDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\CarDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [CarDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CarDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CarDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CarDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CarDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CarDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CarDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [CarDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CarDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CarDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CarDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CarDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CarDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CarDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CarDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CarDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CarDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [CarDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CarDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CarDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CarDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CarDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CarDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CarDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CarDB] SET RECOVERY FULL 
GO
ALTER DATABASE [CarDB] SET  MULTI_USER 
GO
ALTER DATABASE [CarDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CarDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CarDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CarDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CarDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'CarDB', N'ON'
GO
ALTER DATABASE [CarDB] SET QUERY_STORE = OFF
GO
USE [CarDB]
GO
/****** Object:  Table [dbo].[Car]    Script Date: 20/07/2021 12:40:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Car](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TypeID] [int] NOT NULL,
	[Price] [numeric](8, 2) NOT NULL,
	[BRAND] [nvarchar](200) NULL,
	[Photo] [nvarchar](250) NULL,
	[CarName] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_Car] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 20/07/2021 12:40:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CARID] [int] NOT NULL,
	[STARTDATE] [datetime] NOT NULL,
	[ENDDATE] [datetime] NOT NULL,
	[PICK_LOCATION] [varchar](250) NOT NULL,
	[DROP_LOCATION] [varchar](250) NOT NULL,
	[CONTACT NO] [varchar](15) NOT NULL,
	[CONTACT_PERSON] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Type]    Script Date: 20/07/2021 12:40:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Type](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Type] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Car]  WITH NOCHECK ADD  CONSTRAINT [FK_TypeId] FOREIGN KEY([TypeID])
REFERENCES [dbo].[Type] ([ID])
GO
ALTER TABLE [dbo].[Car] CHECK CONSTRAINT [FK_TypeId]
GO
ALTER TABLE [dbo].[Order]  WITH NOCHECK ADD  CONSTRAINT [FK_CarId] FOREIGN KEY([CARID])
REFERENCES [dbo].[Car] ([ID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_CarId]
GO
USE [master]
GO
ALTER DATABASE [CarDB] SET  READ_WRITE 
GO
