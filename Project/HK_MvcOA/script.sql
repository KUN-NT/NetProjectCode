USE [master]
GO
/****** Object:  Database [OAData]    Script Date: 06/14/2018 08:05:31 ******/
CREATE DATABASE [OAData] ON  PRIMARY 
( NAME = N'OAData', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\OAData.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'OAData_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\OAData_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [OAData] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OAData].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OAData] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [OAData] SET ANSI_NULLS OFF
GO
ALTER DATABASE [OAData] SET ANSI_PADDING OFF
GO
ALTER DATABASE [OAData] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [OAData] SET ARITHABORT OFF
GO
ALTER DATABASE [OAData] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [OAData] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [OAData] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [OAData] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [OAData] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [OAData] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [OAData] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [OAData] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [OAData] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [OAData] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [OAData] SET  DISABLE_BROKER
GO
ALTER DATABASE [OAData] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [OAData] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [OAData] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [OAData] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [OAData] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [OAData] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [OAData] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [OAData] SET  READ_WRITE
GO
ALTER DATABASE [OAData] SET RECOVERY FULL
GO
ALTER DATABASE [OAData] SET  MULTI_USER
GO
ALTER DATABASE [OAData] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [OAData] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'OAData', N'ON'
GO
USE [OAData]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 06/14/2018 08:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DepName] [nvarchar](32) NOT NULL,
	[ParentId] [int] NOT NULL,
	[TreePath] [nvarchar](128) NOT NULL,
	[Level] [int] NOT NULL,
	[IsLeaf] [bit] NOT NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 06/14/2018 08:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Author] [nvarchar](200) NOT NULL,
	[PublisherId] [int] NOT NULL,
	[PublishDate] [datetime] NOT NULL,
	[ISBN] [nvarchar](50) NOT NULL,
	[WordsCount] [int] NOT NULL,
	[UnitPrice] [decimal](19, 4) NOT NULL,
	[ContentDescription] [nvarchar](max) NULL,
	[AurhorDescription] [nvarchar](max) NULL,
	[EditorComment] [nvarchar](max) NULL,
	[TOC] [nvarchar](max) NULL,
	[CategoryId] [int] NULL,
	[Clicks] [int] NOT NULL,
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActionInfo]    Script Date: 06/14/2018 08:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ActionInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SubTime] [datetime] NOT NULL,
	[DelFlag] [smallint] NOT NULL,
	[ModifiedOn] [nvarchar](max) NOT NULL,
	[Remark] [nvarchar](256) NULL,
	[Url] [nvarchar](512) NOT NULL,
	[HttpMethod] [nvarchar](32) NOT NULL,
	[ActionMethodName] [nvarchar](32) NULL,
	[ControllerName] [nvarchar](128) NULL,
	[ActionInfoName] [nvarchar](32) NOT NULL,
	[Sort] [nvarchar](max) NULL,
	[ActionTypeEnum] [smallint] NOT NULL,
	[MenuIcon] [varchar](512) NULL,
	[IconWidth] [int] NOT NULL,
	[IconHeight] [int] NOT NULL,
 CONSTRAINT [PK_ActionInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[ActionInfo] ON
INSERT [dbo].[ActionInfo] ([ID], [SubTime], [DelFlag], [ModifiedOn], [Remark], [Url], [HttpMethod], [ActionMethodName], [ControllerName], [ActionInfoName], [Sort], [ActionTypeEnum], [MenuIcon], [IconWidth], [IconHeight]) VALUES (6, CAST(0x0000A8F800000000 AS DateTime), 0, N'2018-06-06', N'权限', N'/Home/Index', N'GET', NULL, NULL, N'主界面', N'1', 1, N'/Content/lib/images/3DSMAX.png', 200, 100)
INSERT [dbo].[ActionInfo] ([ID], [SubTime], [DelFlag], [ModifiedOn], [Remark], [Url], [HttpMethod], [ActionMethodName], [ControllerName], [ActionInfoName], [Sort], [ActionTypeEnum], [MenuIcon], [IconWidth], [IconHeight]) VALUES (7, CAST(0x0000A8F800000000 AS DateTime), 0, N'2018-06-06', N'权限', N'/UserInfo/Index', N'GET', NULL, NULL, N'用户管理', N'1', 1, N'/Content/lib/images/3DSMAX.png', 200, 100)
INSERT [dbo].[ActionInfo] ([ID], [SubTime], [DelFlag], [ModifiedOn], [Remark], [Url], [HttpMethod], [ActionMethodName], [ControllerName], [ActionInfoName], [Sort], [ActionTypeEnum], [MenuIcon], [IconWidth], [IconHeight]) VALUES (14, CAST(0x0000A8F700000000 AS DateTime), 0, N'2018-06-06', N'role', N'/RoleInfo/Index', N'GET', NULL, NULL, N'角色管理', NULL, 1, N'/Content/lib/images/3DSMAX.png', 200, 100)
SET IDENTITY_INSERT [dbo].[ActionInfo] OFF
/****** Object:  Table [dbo].[RoleInfo]    Script Date: 06/14/2018 08:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](32) NOT NULL,
	[DelFlag] [smallint] NOT NULL,
	[SubTime] [datetime] NOT NULL,
	[Remark] [nvarchar](256) NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[Sort] [nvarchar](max) NULL,
 CONSTRAINT [PK_RoleInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[RoleInfo] ON
INSERT [dbo].[RoleInfo] ([ID], [RoleName], [DelFlag], [SubTime], [Remark], [ModifiedOn], [Sort]) VALUES (2, N'管理员', 0, CAST(0x0000A8F700000000 AS DateTime), N'admin', CAST(0x0000A8F700000000 AS DateTime), N'1')
INSERT [dbo].[RoleInfo] ([ID], [RoleName], [DelFlag], [SubTime], [Remark], [ModifiedOn], [Sort]) VALUES (3, N'角色管理', 0, CAST(0x0000A8F70111FB0C AS DateTime), N'admin', CAST(0x0000A8F70111FB0C AS DateTime), N'1')
INSERT [dbo].[RoleInfo] ([ID], [RoleName], [DelFlag], [SubTime], [Remark], [ModifiedOn], [Sort]) VALUES (4, N'用户管理', 0, CAST(0x0000A8F700000000 AS DateTime), N'admin', CAST(0x0000A8F800000000 AS DateTime), N'1')
SET IDENTITY_INSERT [dbo].[RoleInfo] OFF
/****** Object:  Table [dbo].[UserInfo]    Script Date: 06/14/2018 08:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UName] [nvarchar](32) NULL,
	[UPwd] [nvarchar](16) NOT NULL,
	[SubTime] [datetime] NOT NULL,
	[DelFlag] [smallint] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[Remark] [nvarchar](256) NULL,
	[Sort] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[UserInfo] ON
INSERT [dbo].[UserInfo] ([ID], [UName], [UPwd], [SubTime], [DelFlag], [ModifiedOn], [Remark], [Sort]) VALUES (2, N'admin', N'admin', CAST(0x0000A8F400000000 AS DateTime), 0, CAST(0x0000A8F700E5A20B AS DateTime), N'1234', N'1')
INSERT [dbo].[UserInfo] ([ID], [UName], [UPwd], [SubTime], [DelFlag], [ModifiedOn], [Remark], [Sort]) VALUES (4, N'user', N'123', CAST(0x0000A8F700000000 AS DateTime), 0, CAST(0x0000A8F700E5E245 AS DateTime), N'123', N'1')
INSERT [dbo].[UserInfo] ([ID], [UName], [UPwd], [SubTime], [DelFlag], [ModifiedOn], [Remark], [Sort]) VALUES (5, N'123', N'12312', CAST(0x0000A8F700000000 AS DateTime), 0, CAST(0x0000A8F701028EC7 AS DateTime), N'123', N'1')
SET IDENTITY_INSERT [dbo].[UserInfo] OFF
/****** Object:  Table [dbo].[SearchDetails]    Script Date: 06/14/2018 08:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SearchDetails](
	[Id] [uniqueidentifier] NOT NULL,
	[KeyWords] [nvarchar](255) NULL,
	[SearchDateTime] [datetime] NULL,
 CONSTRAINT [PK_SearchDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KeyWordsRank]    Script Date: 06/14/2018 08:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KeyWordsRank](
	[Id] [uniqueidentifier] NOT NULL,
	[KeyWords] [nvarchar](255) NULL,
	[SearchCount] [int] NULL,
 CONSTRAINT [PK_KeyWordsRank] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserInfoRoleInfo]    Script Date: 06/14/2018 08:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfoRoleInfo](
	[RoleInfo_ID] [int] NOT NULL,
	[UserInfo_ID] [int] NOT NULL,
 CONSTRAINT [PK_UserInfoRoleInfo] PRIMARY KEY NONCLUSTERED 
(
	[RoleInfo_ID] ASC,
	[UserInfo_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_UserInfoRoleInfo_UserInfo] ON [dbo].[UserInfoRoleInfo] 
(
	[UserInfo_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
INSERT [dbo].[UserInfoRoleInfo] ([RoleInfo_ID], [UserInfo_ID]) VALUES (3, 2)
INSERT [dbo].[UserInfoRoleInfo] ([RoleInfo_ID], [UserInfo_ID]) VALUES (3, 4)
INSERT [dbo].[UserInfoRoleInfo] ([RoleInfo_ID], [UserInfo_ID]) VALUES (4, 2)
INSERT [dbo].[UserInfoRoleInfo] ([RoleInfo_ID], [UserInfo_ID]) VALUES (4, 4)
/****** Object:  Table [dbo].[UserInfoDepartment]    Script Date: 06/14/2018 08:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfoDepartment](
	[Department_ID] [int] NOT NULL,
	[UserInfo_ID] [int] NOT NULL,
 CONSTRAINT [PK_UserInfoDepartment] PRIMARY KEY NONCLUSTERED 
(
	[Department_ID] ASC,
	[UserInfo_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_UserInfoDepartment_UserInfo] ON [dbo].[UserInfoDepartment] 
(
	[UserInfo_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DepartmentActionInfo]    Script Date: 06/14/2018 08:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DepartmentActionInfo](
	[ActionInfo_ID] [int] NOT NULL,
	[Department_ID] [int] NOT NULL,
 CONSTRAINT [PK_DepartmentActionInfo] PRIMARY KEY NONCLUSTERED 
(
	[ActionInfo_ID] ASC,
	[Department_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_DepartmentActionInfo_Department] ON [dbo].[DepartmentActionInfo] 
(
	[Department_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleInfoActionInfo]    Script Date: 06/14/2018 08:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleInfoActionInfo](
	[ActionInfo_ID] [int] NOT NULL,
	[RoleInfo_ID] [int] NOT NULL,
 CONSTRAINT [PK_RoleInfoActionInfo] PRIMARY KEY NONCLUSTERED 
(
	[ActionInfo_ID] ASC,
	[RoleInfo_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_RoleInfoActionInfo_RoleInfo] ON [dbo].[RoleInfoActionInfo] 
(
	[RoleInfo_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
INSERT [dbo].[RoleInfoActionInfo] ([ActionInfo_ID], [RoleInfo_ID]) VALUES (7, 4)
INSERT [dbo].[RoleInfoActionInfo] ([ActionInfo_ID], [RoleInfo_ID]) VALUES (14, 3)
/****** Object:  Table [dbo].[R_UserInfo_ActionInfo]    Script Date: 06/14/2018 08:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[R_UserInfo_ActionInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserInfoID] [int] NOT NULL,
	[ActionInfoID] [int] NOT NULL,
	[IsPass] [bit] NOT NULL,
 CONSTRAINT [PK_R_UserInfo_ActionInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_ActionInfoR_UserInfo_ActionInfo] ON [dbo].[R_UserInfo_ActionInfo] 
(
	[ActionInfoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_UserInfoR_UserInfo_ActionInfo] ON [dbo].[R_UserInfo_ActionInfo] 
(
	[UserInfoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[R_UserInfo_ActionInfo] ON
INSERT [dbo].[R_UserInfo_ActionInfo] ([ID], [UserInfoID], [ActionInfoID], [IsPass]) VALUES (1, 5, 7, 1)
INSERT [dbo].[R_UserInfo_ActionInfo] ([ID], [UserInfoID], [ActionInfoID], [IsPass]) VALUES (4, 2, 7, 1)
SET IDENTITY_INSERT [dbo].[R_UserInfo_ActionInfo] OFF
/****** Object:  ForeignKey [FK_UserInfoRoleInfo_RoleInfo]    Script Date: 06/14/2018 08:05:31 ******/
ALTER TABLE [dbo].[UserInfoRoleInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfoRoleInfo_RoleInfo] FOREIGN KEY([RoleInfo_ID])
REFERENCES [dbo].[RoleInfo] ([ID])
GO
ALTER TABLE [dbo].[UserInfoRoleInfo] CHECK CONSTRAINT [FK_UserInfoRoleInfo_RoleInfo]
GO
/****** Object:  ForeignKey [FK_UserInfoRoleInfo_UserInfo]    Script Date: 06/14/2018 08:05:31 ******/
ALTER TABLE [dbo].[UserInfoRoleInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfoRoleInfo_UserInfo] FOREIGN KEY([UserInfo_ID])
REFERENCES [dbo].[UserInfo] ([ID])
GO
ALTER TABLE [dbo].[UserInfoRoleInfo] CHECK CONSTRAINT [FK_UserInfoRoleInfo_UserInfo]
GO
/****** Object:  ForeignKey [FK_UserInfoDepartment_Department]    Script Date: 06/14/2018 08:05:31 ******/
ALTER TABLE [dbo].[UserInfoDepartment]  WITH CHECK ADD  CONSTRAINT [FK_UserInfoDepartment_Department] FOREIGN KEY([Department_ID])
REFERENCES [dbo].[Department] ([ID])
GO
ALTER TABLE [dbo].[UserInfoDepartment] CHECK CONSTRAINT [FK_UserInfoDepartment_Department]
GO
/****** Object:  ForeignKey [FK_UserInfoDepartment_UserInfo]    Script Date: 06/14/2018 08:05:31 ******/
ALTER TABLE [dbo].[UserInfoDepartment]  WITH CHECK ADD  CONSTRAINT [FK_UserInfoDepartment_UserInfo] FOREIGN KEY([UserInfo_ID])
REFERENCES [dbo].[UserInfo] ([ID])
GO
ALTER TABLE [dbo].[UserInfoDepartment] CHECK CONSTRAINT [FK_UserInfoDepartment_UserInfo]
GO
/****** Object:  ForeignKey [FK_DepartmentActionInfo_ActionInfo]    Script Date: 06/14/2018 08:05:31 ******/
ALTER TABLE [dbo].[DepartmentActionInfo]  WITH CHECK ADD  CONSTRAINT [FK_DepartmentActionInfo_ActionInfo] FOREIGN KEY([ActionInfo_ID])
REFERENCES [dbo].[ActionInfo] ([ID])
GO
ALTER TABLE [dbo].[DepartmentActionInfo] CHECK CONSTRAINT [FK_DepartmentActionInfo_ActionInfo]
GO
/****** Object:  ForeignKey [FK_DepartmentActionInfo_Department]    Script Date: 06/14/2018 08:05:31 ******/
ALTER TABLE [dbo].[DepartmentActionInfo]  WITH CHECK ADD  CONSTRAINT [FK_DepartmentActionInfo_Department] FOREIGN KEY([Department_ID])
REFERENCES [dbo].[Department] ([ID])
GO
ALTER TABLE [dbo].[DepartmentActionInfo] CHECK CONSTRAINT [FK_DepartmentActionInfo_Department]
GO
/****** Object:  ForeignKey [FK_RoleInfoActionInfo_ActionInfo]    Script Date: 06/14/2018 08:05:31 ******/
ALTER TABLE [dbo].[RoleInfoActionInfo]  WITH CHECK ADD  CONSTRAINT [FK_RoleInfoActionInfo_ActionInfo] FOREIGN KEY([ActionInfo_ID])
REFERENCES [dbo].[ActionInfo] ([ID])
GO
ALTER TABLE [dbo].[RoleInfoActionInfo] CHECK CONSTRAINT [FK_RoleInfoActionInfo_ActionInfo]
GO
/****** Object:  ForeignKey [FK_RoleInfoActionInfo_RoleInfo]    Script Date: 06/14/2018 08:05:31 ******/
ALTER TABLE [dbo].[RoleInfoActionInfo]  WITH CHECK ADD  CONSTRAINT [FK_RoleInfoActionInfo_RoleInfo] FOREIGN KEY([RoleInfo_ID])
REFERENCES [dbo].[RoleInfo] ([ID])
GO
ALTER TABLE [dbo].[RoleInfoActionInfo] CHECK CONSTRAINT [FK_RoleInfoActionInfo_RoleInfo]
GO
/****** Object:  ForeignKey [FK_ActionInfoR_UserInfo_ActionInfo]    Script Date: 06/14/2018 08:05:31 ******/
ALTER TABLE [dbo].[R_UserInfo_ActionInfo]  WITH CHECK ADD  CONSTRAINT [FK_ActionInfoR_UserInfo_ActionInfo] FOREIGN KEY([ActionInfoID])
REFERENCES [dbo].[ActionInfo] ([ID])
GO
ALTER TABLE [dbo].[R_UserInfo_ActionInfo] CHECK CONSTRAINT [FK_ActionInfoR_UserInfo_ActionInfo]
GO
/****** Object:  ForeignKey [FK_UserInfoR_UserInfo_ActionInfo]    Script Date: 06/14/2018 08:05:31 ******/
ALTER TABLE [dbo].[R_UserInfo_ActionInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfoR_UserInfo_ActionInfo] FOREIGN KEY([UserInfoID])
REFERENCES [dbo].[UserInfo] ([ID])
GO
ALTER TABLE [dbo].[R_UserInfo_ActionInfo] CHECK CONSTRAINT [FK_UserInfoR_UserInfo_ActionInfo]
GO
