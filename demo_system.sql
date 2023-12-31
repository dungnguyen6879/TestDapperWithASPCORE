USE [demo_system]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 2023/12/29 16:34:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](300) NULL,
	[FirstName] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NULL,
	[Email] [nvarchar](300) NULL,
	[WebsiteUrl] [nvarchar](500) NULL,
	[PhoneNumber] [nvarchar](30) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[UpdatedOn] [datetime2](7) NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Contact] ([Id], [Title], [FirstName], [LastName], [Email], [WebsiteUrl], [PhoneNumber], [IsDeleted], [CreatedOn], [UpdatedOn], [CreatedBy], [UpdatedBy]) VALUES (N'00000000-0000-0000-0000-000000000000', N'Title', N'FirstName', N'LastName', N'Email', N'WebsiteUrl', N'PhoneNumber', 1, CAST(N'2023-12-29T04:17:20.9800000' AS DateTime2), CAST(N'2023-12-29T06:43:45.5100000' AS DateTime2), N'00000000-0000-0000-0000-000000000000', N'00000000-0000-0000-0000-000000000000')
INSERT [dbo].[Contact] ([Id], [Title], [FirstName], [LastName], [Email], [WebsiteUrl], [PhoneNumber], [IsDeleted], [CreatedOn], [UpdatedOn], [CreatedBy], [UpdatedBy]) VALUES (N'86f17520-1329-43fd-6991-08dc05f24447', NULL, N'Admin', N'Contact', N'admin@website.com', NULL, NULL, 0, CAST(N'2023-12-26T09:08:45.9836256' AS DateTime2), CAST(N'2023-12-26T09:08:45.9835400' AS DateTime2), N'00000000-0000-0000-0000-000000000000', NULL)
GO
/****** Object:  StoredProcedure [dbo].[Add_Contact]    Script Date: 2023/12/29 16:35:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Add_Contact](
	@Title nvarchar(300),
	@FirstName nvarchar(100),
	@LastName nvarchar(100),
	@Email nvarchar(300),
	@WebsiteUrl nvarchar(500),
	@PhoneNumber nvarchar(30),
	@IsDeleted bit,
	@Status INT OUT
)
AS
BEGIN
IF NOT EXISTS(SELECT Title  FROM Contact WHERE Title= @Title)
	BEGIN
	DECLARE @Id uniqueidentifier ;
	SET @Id = NEWID(); 
	INSERT INTO Contact(Id,Title,FirstName,LastName,Email,WebsiteUrl,PhoneNumber,IsDeleted,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy) VALUES (@Id ,@Title ,@FirstName ,@LastName ,@Email ,@WebsiteUrl ,@PhoneNumber ,@IsDeleted ,GETDATE() ,null ,@Id ,null)
	SET @Status=1
END
ELSE
	BEGIN
	SET @Status=2
	END
END
GO
/****** Object:  StoredProcedure [dbo].[Remove_Contact]    Script Date: 2023/12/29 16:35:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Remove_Contact](
	@id uniqueidentifier,
	@Status INT = 0
)
AS
BEGIN
	IF EXISTS(SELECT Id  FROM Contact WHERE Id= @id)
		BEGIN
			DELETE FROM Contact WHERE Id = @id
			SET @Status=1
			RETURN @Status
		END
	ELSE
		BEGIN
			SET @Status=2
			RETURN @Status
		END
END
GO
