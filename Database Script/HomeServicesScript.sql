USE [master]
GO
/****** Object:  Database [HomeServices]    Script Date: 26-07-2024 11.45.31 AM ******/
CREATE DATABASE [HomeServices]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HomeServices', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\HomeServices.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HomeServices_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\HomeServices_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [HomeServices] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HomeServices].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HomeServices] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HomeServices] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HomeServices] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HomeServices] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HomeServices] SET ARITHABORT OFF 
GO
ALTER DATABASE [HomeServices] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HomeServices] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HomeServices] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HomeServices] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HomeServices] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HomeServices] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HomeServices] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HomeServices] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HomeServices] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HomeServices] SET  DISABLE_BROKER 
GO
ALTER DATABASE [HomeServices] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HomeServices] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HomeServices] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HomeServices] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HomeServices] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HomeServices] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HomeServices] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HomeServices] SET RECOVERY FULL 
GO
ALTER DATABASE [HomeServices] SET  MULTI_USER 
GO
ALTER DATABASE [HomeServices] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HomeServices] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HomeServices] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HomeServices] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [HomeServices] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [HomeServices] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'HomeServices', N'ON'
GO
ALTER DATABASE [HomeServices] SET QUERY_STORE = ON
GO
ALTER DATABASE [HomeServices] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [HomeServices]
GO
/****** Object:  User [sa2]    Script Date: 26-07-2024 11.45.31 AM ******/
CREATE USER [sa2] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [IIS APPPOOL\HomeServicesWeb]    Script Date: 26-07-2024 11.45.31 AM ******/
CREATE USER [IIS APPPOOL\HomeServicesWeb] FOR LOGIN [IIS APPPOOL\HomeServicesWeb] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [IIS APPPOOL\HomeServicesWeb]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [IIS APPPOOL\HomeServicesWeb]
GO
ALTER ROLE [db_datareader] ADD MEMBER [IIS APPPOOL\HomeServicesWeb]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [IIS APPPOOL\HomeServicesWeb]
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 26-07-2024 11.45.31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[BookingId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NULL,
	[ServiceProviderId] [int] NULL,
	[DateTime] [datetime] NOT NULL,
	[Description] [nvarchar](255) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[StatusId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[BookingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[City]    Script Date: 26-07-2024 11.45.31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[CityId] [int] IDENTITY(1,1) NOT NULL,
	[CityName] [nvarchar](255) NOT NULL,
	[CountryId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 26-07-2024 11.45.31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[CountryId] [int] IDENTITY(1,1) NOT NULL,
	[CountryName] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rating]    Script Date: 26-07-2024 11.45.31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rating](
	[RatingId] [int] IDENTITY(1,1) NOT NULL,
	[Ratings] [decimal](3, 2) NOT NULL,
	[BookingId] [int] NULL,
	[Reviews] [nvarchar](1000) NULL,
	[ServiceProviderId] [int] NULL,
	[ServiceCategoryId] [int] NULL,
	[CreatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[RatingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceCategory]    Script Date: 26-07-2024 11.45.31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceCategory](
	[ServiceCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[ServiceName] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[DurationTimeSlot] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[CategoryImage] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ServiceCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceProvider]    Script Date: 26-07-2024 11.45.31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceProvider](
	[ServiceProviderId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[Bio] [nvarchar](1000) NULL,
	[ServiceCategoryId] [int] NULL,
	[Rating] [decimal](3, 2) NULL,
	[Reviews] [int] NULL,
	[ExperienceYear] [int] NULL,
	[Price] [decimal](10, 2) NOT NULL,
	[AvailabilityTimeSlot] [nvarchar](255) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ServiceProviderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 26-07-2024 11.45.31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[StatusId] [int] NOT NULL,
	[StatusName] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 26-07-2024 11.45.31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](255) NOT NULL,
	[LastName] [nvarchar](255) NOT NULL,
	[PhoneNumber] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[CityId] [int] NULL,
	[CountryId] [int] NULL,
	[Address] [nvarchar](500) NULL,
	[ZipCode] [nvarchar](20) NULL,
	[UserTypeId] [int] NULL,
	[Password] [nvarchar](255) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[ProfilePicture] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserType]    Script Date: 26-07-2024 11.45.31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserType](
	[UserTypeId] [int] IDENTITY(1,1) NOT NULL,
	[UserRole] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Booking] ON 

INSERT [dbo].[Booking] ([BookingId], [CustomerId], [ServiceProviderId], [DateTime], [Description], [CreatedDate], [ModifiedDate], [StatusId]) VALUES (24, 24, 28, CAST(N'2024-07-26T12:00:00.000' AS DateTime), N'need', CAST(N'2024-07-25T10:30:45.670' AS DateTime), CAST(N'2024-07-25T11:37:38.240' AS DateTime), 102)
SET IDENTITY_INSERT [dbo].[Booking] OFF
GO
SET IDENTITY_INSERT [dbo].[City] ON 

INSERT [dbo].[City] ([CityId], [CityName], [CountryId]) VALUES (1, N'Toronto', 1)
INSERT [dbo].[City] ([CityId], [CityName], [CountryId]) VALUES (2, N'Vancouver', 1)
INSERT [dbo].[City] ([CityId], [CityName], [CountryId]) VALUES (3, N'Montreal', 1)
INSERT [dbo].[City] ([CityId], [CityName], [CountryId]) VALUES (4, N'Calgary', 1)
INSERT [dbo].[City] ([CityId], [CityName], [CountryId]) VALUES (5, N'New York', 2)
SET IDENTITY_INSERT [dbo].[City] OFF
GO
SET IDENTITY_INSERT [dbo].[Country] ON 

INSERT [dbo].[Country] ([CountryId], [CountryName]) VALUES (1, N'Canada')
INSERT [dbo].[Country] ([CountryId], [CountryName]) VALUES (2, N'USA')
SET IDENTITY_INSERT [dbo].[Country] OFF
GO
SET IDENTITY_INSERT [dbo].[ServiceCategory] ON 

INSERT [dbo].[ServiceCategory] ([ServiceCategoryId], [ServiceName], [Description], [DurationTimeSlot], [CreatedDate], [ModifiedDate], [CategoryImage]) VALUES (1, N'Plumbing', N'Plumbing services', 60, CAST(N'2024-07-11T13:22:23.497' AS DateTime), CAST(N'2024-07-11T13:22:23.497' AS DateTime), N'Plumbing.png')
INSERT [dbo].[ServiceCategory] ([ServiceCategoryId], [ServiceName], [Description], [DurationTimeSlot], [CreatedDate], [ModifiedDate], [CategoryImage]) VALUES (2, N'Cleaner', N'Cleaning services', 120, CAST(N'2024-07-11T13:22:23.497' AS DateTime), CAST(N'2024-07-11T13:22:23.497' AS DateTime), N'Cleaner.png')
INSERT [dbo].[ServiceCategory] ([ServiceCategoryId], [ServiceName], [Description], [DurationTimeSlot], [CreatedDate], [ModifiedDate], [CategoryImage]) VALUES (3, N'Electrician', N'Electrical services', 60, CAST(N'2024-07-11T13:22:23.497' AS DateTime), CAST(N'2024-07-11T13:22:23.497' AS DateTime), N'Electrician.png')
SET IDENTITY_INSERT [dbo].[ServiceCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[ServiceProvider] ON 

INSERT [dbo].[ServiceProvider] ([ServiceProviderId], [UserId], [Bio], [ServiceCategoryId], [Rating], [Reviews], [ExperienceYear], [Price], [AvailabilityTimeSlot], [CreatedDate], [ModifiedDate]) VALUES (26, 42, N'dfdf', 3, CAST(0.00 AS Decimal(3, 2)), 0, 10, CAST(456.00 AS Decimal(10, 2)), N'7', CAST(N'2024-07-25T09:07:19.637' AS DateTime), CAST(N'2024-07-25T10:07:20.543' AS DateTime))
INSERT [dbo].[ServiceProvider] ([ServiceProviderId], [UserId], [Bio], [ServiceCategoryId], [Rating], [Reviews], [ExperienceYear], [Price], [AvailabilityTimeSlot], [CreatedDate], [ModifiedDate]) VALUES (28, 46, N'pro', 3, CAST(0.00 AS Decimal(3, 2)), 0, 2, CAST(125.00 AS Decimal(10, 2)), N'10:00 AM to 07:00 PM', CAST(N'2024-07-25T10:16:29.370' AS DateTime), CAST(N'2024-07-25T10:52:08.577' AS DateTime))
SET IDENTITY_INSERT [dbo].[ServiceProvider] OFF
GO
INSERT [dbo].[Status] ([StatusId], [StatusName]) VALUES (101, N'Pending')
INSERT [dbo].[Status] ([StatusId], [StatusName]) VALUES (102, N'Completed')
INSERT [dbo].[Status] ([StatusId], [StatusName]) VALUES (103, N'Rejected')
INSERT [dbo].[Status] ([StatusId], [StatusName]) VALUES (104, N'Cancelled ')
INSERT [dbo].[Status] ([StatusId], [StatusName]) VALUES (105, N'Scheduled')
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [PhoneNumber], [Email], [CityId], [CountryId], [Address], [ZipCode], [UserTypeId], [Password], [CreatedDate], [ModifiedDate], [ProfilePicture]) VALUES (24, N'Mark', N'Brown', N'703-710-8261', N'mark@gmail.com', 1, 1, N'137 Yonge St, ON M5C 1W6', N'123456', 1, N'$2a$11$o3yJEKDIWRjSXYYcVkyE6upjhChCzWvdT2HflsAUwY8A9kDzEHw3K', CAST(N'2024-07-24T16:55:28.813' AS DateTime), CAST(N'2024-07-25T10:31:29.660' AS DateTime), N'/images/Profile_Image/ProfilePicture.jpg')
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [PhoneNumber], [Email], [CityId], [CountryId], [Address], [ZipCode], [UserTypeId], [Password], [CreatedDate], [ModifiedDate], [ProfilePicture]) VALUES (25, N'Amanda', N'Miller', N'703-701-9964', N'amanda@gmai.com', 1, 1, N'1245 Dupont St Unit 1, ON M6H 2A6', N'', 2, N'$2a$11$DVwsjOwg.gIGop6lBN28e.Sl8kr2BRmUf01eHpz7cjUE.mRLnhDDK', CAST(N'2024-07-24T17:01:15.617' AS DateTime), CAST(N'2024-07-24T17:01:15.617' AS DateTime), NULL)
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [PhoneNumber], [Email], [CityId], [CountryId], [Address], [ZipCode], [UserTypeId], [Password], [CreatedDate], [ModifiedDate], [ProfilePicture]) VALUES (26, N'Amanda', N'Miller', N'306-368-5363', N'amanda@gmail.com', 1, 1, N'1245 Dupont St Unit 1, ON M6H 2A6', N'', 2, N'$2a$11$JXp8QA3s3zUoOpfdcxtDQesCQZx3g4ePiThQZ/qADx4mw8Ea4GM9y', CAST(N'2024-07-24T17:05:55.837' AS DateTime), CAST(N'2024-07-24T17:05:55.837' AS DateTime), NULL)
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [PhoneNumber], [Email], [CityId], [CountryId], [Address], [ZipCode], [UserTypeId], [Password], [CreatedDate], [ModifiedDate], [ProfilePicture]) VALUES (41, N'Dev', N'Bhagat', N'262151533', N'Dev@gmail.com', 1, 1, N'Ind', N'54584', 1, N'$2a$11$/m7vblLP9HL90TCOx7gY5e9.FdoFe7CqR6siCdQBV7Cyn9VeCppL6', CAST(N'2024-07-25T10:20:18.403' AS DateTime), CAST(N'2024-07-25T10:05:09.377' AS DateTime), N'/images/Profile_Image/NoImage.jpg')
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [PhoneNumber], [Email], [CityId], [CountryId], [Address], [ZipCode], [UserTypeId], [Password], [CreatedDate], [ModifiedDate], [ProfilePicture]) VALUES (42, N'D', N'B', N'262151533', N'D@gmail.com', 1, 1, N'Ind', N'2654', 2, N'$2a$11$9IrfCbxfkLB3GFU5qnmttuqiuaj5koKVn3swMpmpsTEI4d6Uh/vqK', CAST(N'2024-07-25T10:21:31.623' AS DateTime), CAST(N'2024-07-25T10:21:31.623' AS DateTime), N'/images/Profile_Image/Profile2.jpg')
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [PhoneNumber], [Email], [CityId], [CountryId], [Address], [ZipCode], [UserTypeId], [Password], [CreatedDate], [ModifiedDate], [ProfilePicture]) VALUES (46, N'Amanda', N'Miller', N'2589636541', N'amanda1@gmail.com', 1, 1, N'abcd', N'124565', 2, N'$2a$11$hDvjx19k97nOzf556Kc6/.bhqZeC0sy81paa7knA0fPZhWm/yIq2y', CAST(N'2024-07-25T15:40:39.623' AS DateTime), CAST(N'2024-07-25T15:40:39.623' AS DateTime), N'/images/Profile_Image/ProfilePicture.jpg')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[UserType] ON 

INSERT [dbo].[UserType] ([UserTypeId], [UserRole], [CreatedDate], [ModifiedDate]) VALUES (1, N'Customer', CAST(N'2024-07-11T13:11:54.467' AS DateTime), CAST(N'2024-07-11T13:11:54.467' AS DateTime))
INSERT [dbo].[UserType] ([UserTypeId], [UserRole], [CreatedDate], [ModifiedDate]) VALUES (2, N'Service Provider', CAST(N'2024-07-11T13:11:54.470' AS DateTime), CAST(N'2024-07-11T13:11:54.470' AS DateTime))
SET IDENTITY_INSERT [dbo].[UserType] OFF
GO
ALTER TABLE [dbo].[Booking] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Booking] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Rating] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[ServiceCategory] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[ServiceCategory] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[ServiceProvider] ADD  DEFAULT ((0)) FOR [Rating]
GO
ALTER TABLE [dbo].[ServiceProvider] ADD  DEFAULT ((0)) FOR [Reviews]
GO
ALTER TABLE [dbo].[ServiceProvider] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[ServiceProvider] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[UserType] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[UserType] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD FOREIGN KEY([ServiceProviderId])
REFERENCES [dbo].[ServiceProvider] ([ServiceProviderId])
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_StatusId] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Status] ([StatusId])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_StatusId]
GO
ALTER TABLE [dbo].[City]  WITH CHECK ADD FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([CountryId])
GO
ALTER TABLE [dbo].[Rating]  WITH CHECK ADD FOREIGN KEY([BookingId])
REFERENCES [dbo].[Booking] ([BookingId])
GO
ALTER TABLE [dbo].[Rating]  WITH CHECK ADD FOREIGN KEY([ServiceCategoryId])
REFERENCES [dbo].[ServiceCategory] ([ServiceCategoryId])
GO
ALTER TABLE [dbo].[Rating]  WITH CHECK ADD FOREIGN KEY([ServiceProviderId])
REFERENCES [dbo].[ServiceProvider] ([ServiceProviderId])
GO
ALTER TABLE [dbo].[ServiceProvider]  WITH CHECK ADD FOREIGN KEY([ServiceCategoryId])
REFERENCES [dbo].[ServiceCategory] ([ServiceCategoryId])
GO
ALTER TABLE [dbo].[ServiceProvider]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([CityId])
REFERENCES [dbo].[City] ([CityId])
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([CountryId])
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([UserTypeId])
REFERENCES [dbo].[UserType] ([UserTypeId])
GO
USE [master]
GO
ALTER DATABASE [HomeServices] SET  READ_WRITE 
GO
