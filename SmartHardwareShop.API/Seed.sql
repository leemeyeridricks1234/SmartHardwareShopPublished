USE [master]
GO
/****** Object:  Database [SmartHardwareShop]    Script Date: 4/8/2021 9:49:48 AM ******/
CREATE DATABASE [SmartHardwareShop]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SmartHardwareShop', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\SmartHardwareShop.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SmartHardwareShop_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\SmartHardwareShop_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [SmartHardwareShop] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SmartHardwareShop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SmartHardwareShop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SmartHardwareShop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SmartHardwareShop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SmartHardwareShop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SmartHardwareShop] SET ARITHABORT OFF 
GO
ALTER DATABASE [SmartHardwareShop] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SmartHardwareShop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SmartHardwareShop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SmartHardwareShop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SmartHardwareShop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SmartHardwareShop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SmartHardwareShop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SmartHardwareShop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SmartHardwareShop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SmartHardwareShop] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SmartHardwareShop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SmartHardwareShop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SmartHardwareShop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SmartHardwareShop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SmartHardwareShop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SmartHardwareShop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SmartHardwareShop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SmartHardwareShop] SET RECOVERY FULL 
GO
ALTER DATABASE [SmartHardwareShop] SET  MULTI_USER 
GO
ALTER DATABASE [SmartHardwareShop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SmartHardwareShop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SmartHardwareShop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SmartHardwareShop] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SmartHardwareShop] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'SmartHardwareShop', N'ON'
GO
ALTER DATABASE [SmartHardwareShop] SET QUERY_STORE = OFF
GO
USE [SmartHardwareShop]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [SmartHardwareShop]
GO
/****** Object:  Table [dbo].[CartItem]    Script Date: 4/8/2021 9:49:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[DateAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_CartItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 4/8/2021 9:49:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[OrderTotal] [decimal](18, 2) NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[DateUpdated] [datetime] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItem]    Script Date: 4/8/2021 9:49:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Discount] [decimal](18, 2) NULL,
	[DateAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_OrderItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 4/8/2021 9:49:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Description] [text] NOT NULL,
	[Price] [decimal](7, 2) NOT NULL,
	[RRP] [decimal](7, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Image] [text] NOT NULL,
	[DateAdded] [datetime] NOT NULL,
	[Category] [varchar](200) NULL,
	[UserId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 4/8/2021 9:49:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](100) NULL,
	[Password] [nvarchar](100) NULL,
	[Role] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT ('0.00') FOR [RRP]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[CartItem]  WITH CHECK ADD  CONSTRAINT [FK_CartItem_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[CartItem] CHECK CONSTRAINT [FK_CartItem_Product]
GO
ALTER TABLE [dbo].[CartItem]  WITH CHECK ADD  CONSTRAINT [FK_CartItem_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[CartItem] CHECK CONSTRAINT [FK_CartItem_User]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_User]
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderItem_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([Id])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_Order]
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderItem_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_Product]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_User]
GO
USE [master]
GO
ALTER DATABASE [SmartHardwareShop] SET  READ_WRITE 
GO


Insert into [User] values ('admin','admin', 'admin')
Insert into [User] values ('customer1','customer1', 'customer')
Insert into [User] values ('customer2','customer2', 'customer')
GO

INSERT INTO Product ([Name], [Description], [Price], [RRP], [Quantity], [Image], [DateAdded], Category, UserId) VALUES
('Smart Watch', '<p>Unique watch made with stainless steel, ideal for those that prefer interative watches.</p>\r\n<h3>Features</h3>\r\n<ul>\r\n<li>Powered by Android with built-in apps.</li>\r\n<li>Adjustable to fit most.</li>\r\n<li>Long battery life, continuous wear for up to 2 days.</li>\r\n<li>Lightweight design, comfort on your wrist.</li>\r\n</ul>', '29.99', '0.00', 10, 'watch.jpg', '2019-03-13 17:55:22', 'watches', 1),
('Wallet', '', '14.99', '19.99', 34, 'wallet.jpg', '2019-03-13 18:52:49', 'watches', 1),
('Headphones', '', '19.99', '0.00', 23, 'headphones.jpg', '2019-03-13 18:47:56', 'watches', 1),
('Digital Camera', '', '69.99', '0.00', 7, 'camera.jpg', '2019-03-13 17:42:04', 'watches', 1);
GO