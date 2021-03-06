USE [dbHuogCMS]
GO
/****** Object:  Table [dbo].[tblHuogPitanje]    Script Date: 03/28/2013 16:38:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblHuogPitanje](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GrupaId] [int] NOT NULL,
	[Oznaka] [varchar](3) NULL,
	[Naziv] [nvarchar](500) NOT NULL,
	[Opis] [nvarchar](max) NULL,
	[Faktor] [decimal](10, 2) NOT NULL,
	[Aktivno] [bit] NOT NULL,
 CONSTRAINT [PK_tblHuogPitanje] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblHuogRezultat]    Script Date: 03/28/2013 16:38:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblHuogRezultat](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GrupaId] [int] NOT NULL,
	[UpitnikId] [int] NULL,
	[Vrijednost] [decimal](10, 2) NULL,
	[Tip] [varchar](2) NULL,
	[Vrsta] [varchar](2) NULL,
 CONSTRAINT [PK_tblHuogRezultat] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'IZ - izvodjac, KO - konzultant, IN - investiror' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblHuogRezultat', @level2type=N'COLUMN',@level2name=N'Tip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'BP - best practice, AV - average' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblHuogRezultat', @level2type=N'COLUMN',@level2name=N'Vrsta'
GO
/****** Object:  Table [dbo].[tblHuogUpitnik]    Script Date: 03/28/2013 16:38:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblHuogUpitnik](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Naziv] [nvarchar](250) NULL,
	[Opis] [nvarchar](max) NULL,
	[Datum] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_tblHuogUpitnik] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblHuogGrupa]    Script Date: 03/28/2013 16:38:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblHuogGrupa](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RedniBroj] [int] NULL,
	[Naziv] [nvarchar](150) NOT NULL,
	[Opis] [nvarchar](max) NULL,
	[FaktorMedju] [decimal](10, 2) NOT NULL,
	[FaktorKrajnji] [decimal](10, 2) NOT NULL,
	[Aktivno] [bit] NOT NULL,
 CONSTRAINT [PK_tblHuogGrupa] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'djeljenje koje se odvija u medjukoraku, napravit ce se kao faktor za umnozak' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblHuogGrupa', @level2type=N'COLUMN',@level2name=N'FaktorMedju'
GO
/****** Object:  Table [dbo].[tblHuogUpitnikVrijednost]    Script Date: 03/28/2013 16:38:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblHuogUpitnikVrijednost](
	[PitanjeId] [int] NOT NULL,
	[UpitnikId] [int] NOT NULL,
	[Vrijednost1] [decimal](10, 2) NULL,
	[Vrijednost2] [decimal](10, 2) NULL,
	[Vrijednost3] [decimal](10, 2) NULL,
	[Vrijednost4] [decimal](10, 2) NULL,
	[Vrijednost5] [decimal](10, 2) NULL,
	[Vrijednost6] [decimal](10, 2) NULL,
	[Vrijednost7] [decimal](10, 2) NULL,
	[Vrijednost8] [decimal](10, 2) NULL,
	[Vrijednost9] [decimal](10, 2) NULL,
	[Vrijednost10] [decimal](10, 2) NULL,
 CONSTRAINT [PK_tblHuogUpitnikVrijednost] PRIMARY KEY CLUSTERED 
(
	[PitanjeId] ASC,
	[UpitnikId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblHuogUser]    Script Date: 03/28/2013 16:38:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblHuogUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](20) NOT NULL,
	[CompanyName] [nvarchar](255) NULL,
	[City] [nvarchar](255) NULL,
	[Type] [varchar](2) NOT NULL,
	[Employees] [nvarchar](50) NULL,
	[Income] [nvarchar](50) NULL,
	[Newsletter] [bit] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_tblHuogUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[tblHuogUser_update]    Script Date: 03/28/2013 16:38:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[tblHuogUser_update]
			@Id int
			, @Name nvarchar(50) = NULL
			, @Email nvarchar(50)
			, @Password nvarchar(20)
			, @CompanyName nvarchar(255) = NULL
			, @City nvarchar(255) = NULL
			, @Type varchar(2)
			, @Employees nvarchar(50) = NULL
			, @Income nvarchar(50) = NULL
			, @Newsletter bit
    
		as

		SET NOCOUNT ON

		DECLARE @ERROR INT
		BEGIN TRAN

		UPDATE tblHuogUser SET 
		Name = @Name
			, Email = @Email
			, Password = @Password
			, CompanyName = @CompanyName
			, City = @City
			, Type = @Type
			, Employees = @Employees
			, Income = @Income
			, Newsletter = @Newsletter
WHERE
		Id = @Id
 

		SET @ERROR = @@ERROR
		IF @ERROR<>0
			ROLLBACK TRAN
		ELSE
			COMMIT TRAN

		SET NOCOUNT OFF
GO
/****** Object:  StoredProcedure [dbo].[tblHuogUser_login]    Script Date: 03/28/2013 16:38:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[tblHuogUser_login]
	@Email nvarchar(50)
	, @Password nvarchar(20)
as

SET NOCOUNT ON

SELECT * FROM tblHuogUser
WHERE
Email = @Email
and [Password] = @Password

SET NOCOUNT OFF
GO
/****** Object:  StoredProcedure [dbo].[tblHuogUser_insert]    Script Date: 03/28/2013 16:38:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[tblHuogUser_insert]
			@Id int output
			, @Name nvarchar(50) = NULL
			, @Email nvarchar(50)
			, @Password nvarchar(20)
			, @CompanyName nvarchar(255) = NULL
			, @City nvarchar(255) = NULL
			, @Type varchar(2)
			, @Employees nvarchar(50) = NULL
			, @Income nvarchar(50) = NULL
			, @Newsletter bit
    
		as

		SET NOCOUNT ON

		DECLARE @ERROR INT
		BEGIN TRAN

		INSERT INTO tblHuogUser ( 
		[Name], [Email], [Password], [CompanyName], [City], [Type], [Employees], [Income], [Newsletter]) VALUES (@Name, @Email, @Password, @CompanyName, @City, @Type, @Employees, @Income, @Newsletter) 

		SELECT @Id = SCOPE_IDENTITY(), @ERROR = @@ERROR

		IF @ERROR<>0
			ROLLBACK TRAN
		ELSE
			COMMIT TRAN

		SET NOCOUNT OFF
GO
/****** Object:  StoredProcedure [dbo].[tblHuogUser_Activate]    Script Date: 03/28/2013 16:38:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[tblHuogUser_Activate]
	@Id int
	, @Type nvarchar(20)
as

declare @Status int

select @Status = [Status] from tblHuogUser where Id = @Id

SET NOCOUNT ON

if(@Type='USER' and @Status = 0)
	update tblHuogUser set [Status] = 1 where Id = @Id
else if(@Type='SUPERUSER')
	update tblHuogUser set [Status] = 2 where Id = @Id

SELECT * FROM tblHuogUser
WHERE
Id = @Id

SET NOCOUNT OFF
GO
/****** Object:  StoredProcedure [dbo].[tblHuogUpitnikVrijednosti_List]    Script Date: 03/28/2013 16:38:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[tblHuogUpitnikVrijednosti_List]
	@PitanjeId int = null
	, @UpitnikId int=null
as

SET NOCOUNT ON

SELECT tblHuogPitanje.GrupaId, tblHuogPitanje.Oznaka, tblHuogUpitnikVrijednost.*  
FROM tblHuogUpitnikVrijednost
inner join tblHuogPitanje on tblHuogPitanje.Id = PitanjeId
WHERE
	(@PitanjeId is null or PitanjeId = @PitanjeId)
	and (@UpitnikId is null or UpitnikId = @UpitnikId)

SET NOCOUNT OFF
GO
/****** Object:  StoredProcedure [dbo].[tblHuogUpitnikVrijednost_update]    Script Date: 03/28/2013 16:38:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[tblHuogUpitnikVrijednost_update]
			@PitanjeId int
			, @UpitnikId int
			, @Vrijednost1 decimal(10,2) = NULL
			, @Vrijednost2 decimal(10,2) = NULL
			, @Vrijednost3 decimal(10,2) = NULL
			, @Vrijednost4 decimal(10,2) = NULL
			, @Vrijednost5 decimal(10,2) = NULL
			, @Vrijednost6 decimal(10,2) = NULL
			, @Vrijednost7 decimal(10,2) = NULL
			, @Vrijednost8 decimal(10,2) = NULL
			, @Vrijednost9 decimal(10,2) = NULL
			, @Vrijednost10 decimal(10,2) = NULL
    
		as

		SET NOCOUNT ON

		DECLARE @ERROR INT
		BEGIN TRAN

		UPDATE tblHuogUpitnikVrijednost SET 
			PitanjeId = @PitanjeId
			, UpitnikId = @UpitnikId
			, Vrijednost1 = @Vrijednost1
			, Vrijednost2 = @Vrijednost2
			, Vrijednost3 = @Vrijednost3
			, Vrijednost4 = @Vrijednost4
			, Vrijednost5 = @Vrijednost5
			, Vrijednost6 = @Vrijednost6
			, Vrijednost7 = @Vrijednost7
			, Vrijednost8 = @Vrijednost8
			, Vrijednost9 = @Vrijednost9
			, Vrijednost10 = @Vrijednost10
WHERE
		PitanjeId = @PitanjeId
			 AND UpitnikId = @UpitnikId
 

		SET @ERROR = @@ERROR
		IF @ERROR<>0
			ROLLBACK TRAN
		ELSE
			COMMIT TRAN

		SET NOCOUNT OFF
GO
/****** Object:  StoredProcedure [dbo].[tblHuogUpitnikVrijednost_insert]    Script Date: 03/28/2013 16:38:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[tblHuogUpitnikVrijednost_insert]
			@PitanjeId int
			, @UpitnikId int
			, @Vrijednost1 decimal(10,2) = NULL
			, @Vrijednost2 decimal(10,2) = NULL
			, @Vrijednost3 decimal(10,2) = NULL
			, @Vrijednost4 decimal(10,2) = NULL
			, @Vrijednost5 decimal(10,2) = NULL
			, @Vrijednost6 decimal(10,2) = NULL
			, @Vrijednost7 decimal(10,2) = NULL
			, @Vrijednost8 decimal(10,2) = NULL
			, @Vrijednost9 decimal(10,2) = NULL
			, @Vrijednost10 decimal(10,2) = NULL
    
		as

		SET NOCOUNT ON

		DECLARE @ERROR INT
		BEGIN TRAN

		INSERT INTO tblHuogUpitnikVrijednost ( 
		
		PitanjeId, UpitnikId, [Vrijednost1], [Vrijednost2], [Vrijednost3], [Vrijednost4], [Vrijednost5], [Vrijednost6], [Vrijednost7], [Vrijednost8], [Vrijednost9], [Vrijednost10]) VALUES (@PitanjeId, @UpitnikId, @Vrijednost1, @Vrijednost2, @Vrijednost3, @Vrijednost4, @Vrijednost5, @Vrijednost6, @Vrijednost7, @Vrijednost8, @Vrijednost9, @Vrijednost10) 

		SELECT @ERROR = @@ERROR

		IF @ERROR<>0
			ROLLBACK TRAN
		ELSE
			COMMIT TRAN

		SET NOCOUNT OFF
GO
/****** Object:  StoredProcedure [dbo].[tblHuogUpitnik_update]    Script Date: 03/28/2013 16:38:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[tblHuogUpitnik_update]
			@Id int
			, @UserId int
			, @Naziv nvarchar(250) = NULL
			, @Opis nvarchar(max) = NULL
			, @Datum smalldatetime
    
		as

		SET NOCOUNT ON

		DECLARE @ERROR INT
		BEGIN TRAN

		UPDATE tblHuogUpitnik SET 
		UserId = @UserId
			, Naziv = @Naziv
			, Opis = @Opis
			, Datum = @Datum
WHERE
		Id = @Id
 

		SET @ERROR = @@ERROR
		IF @ERROR<>0
			ROLLBACK TRAN
		ELSE
			COMMIT TRAN

		SET NOCOUNT OFF
GO
/****** Object:  StoredProcedure [dbo].[tblHuogUpitnik_List]    Script Date: 03/28/2013 16:38:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[tblHuogUpitnik_List]
	@Id int = null
	, @UserId int=null
as

SET NOCOUNT ON

SELECT * FROM tblHuogUpitnik
WHERE
	(@Id is null or Id = @Id)
	and (@UserId is null or UserId = @UserId)

SET NOCOUNT OFF
GO
/****** Object:  StoredProcedure [dbo].[tblHuogUpitnik_insert]    Script Date: 03/28/2013 16:38:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[tblHuogUpitnik_insert]
			@Id int output
			, @UserId int
			, @Naziv nvarchar(250) = NULL
			, @Opis nvarchar(max) = NULL
			, @Datum smalldatetime
    
		as

		SET NOCOUNT ON

		DECLARE @ERROR INT
		BEGIN TRAN

		INSERT INTO tblHuogUpitnik ( 
		[UserId], [Naziv], [Opis], [Datum]) VALUES (@UserId, @Naziv, @Opis, @Datum) 

		SELECT @Id = SCOPE_IDENTITY(), @ERROR = @@ERROR

		IF @ERROR<>0
			ROLLBACK TRAN
		ELSE
			COMMIT TRAN

		SET NOCOUNT OFF
GO
/****** Object:  StoredProcedure [dbo].[tblHuogRezultat_update]    Script Date: 03/28/2013 16:38:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[tblHuogRezultat_update]
			@Id int
			, @GrupaId int
			, @UpitnikId int = NULL
			, @Vrijednost decimal(10,2) = NULL
			, @Tip varchar(2) = NULL
			, @Vrsta varchar(2) = NULL
    
		as

		SET NOCOUNT ON

		DECLARE @ERROR INT
		BEGIN TRAN

		UPDATE tblHuogRezultat SET 
		GrupaId = @GrupaId
			, UpitnikId = @UpitnikId
			, Vrijednost = @Vrijednost
			, Tip = @Tip
			, Vrsta = @Vrsta
WHERE
		Id = @Id
 

		SET @ERROR = @@ERROR
		IF @ERROR<>0
			ROLLBACK TRAN
		ELSE
			COMMIT TRAN

		SET NOCOUNT OFF
GO
/****** Object:  StoredProcedure [dbo].[tblHuogRezultat_Save]    Script Date: 03/28/2013 16:38:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[tblHuogRezultat_Save]
	@GrupaId int
	, @UpitnikId int = NULL
	, @Vrijednost decimal(10,2) = NULL
	, @Tip varchar(2) = NULL
	, @Vrsta varchar(2) = NULL

as

SET NOCOUNT ON

DECLARE @ERROR INT
BEGIN TRAN

declare @exists int

select @exists=count(*) from tblHuogRezultat where UpitnikId = @UpitnikId and GrupaId = @GrupaId

if(@exists = 0)

	INSERT INTO tblHuogRezultat ( 
	[GrupaId], [UpitnikId], [Vrijednost], [Tip], [Vrsta]) VALUES (@GrupaId, @UpitnikId, @Vrijednost, @Tip, @Vrsta) 
else
	update tblHuogRezultat set
		[Vrijednost] = @Vrijednost
		, Tip = @Tip
		, Vrsta = @Vrsta
	where UpitnikId = @UpitnikId and GrupaId = @GrupaId

SELECT  @ERROR = @@ERROR

IF @ERROR<>0
	ROLLBACK TRAN
ELSE
	COMMIT TRAN

SET NOCOUNT OFF
GO
/****** Object:  StoredProcedure [dbo].[tblHuogRezultat_insert]    Script Date: 03/28/2013 16:38:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[tblHuogRezultat_insert]
			@Id int output
			, @GrupaId int
			, @UpitnikId int = NULL
			, @Vrijednost decimal(10,2) = NULL
			, @Tip varchar(2) = NULL
			, @Vrsta varchar(2) = NULL
    
		as

		SET NOCOUNT ON

		DECLARE @ERROR INT
		BEGIN TRAN

		INSERT INTO tblHuogRezultat ( 
		[GrupaId], [UpitnikId], [Vrijednost], [Tip], [Vrsta]) VALUES (@GrupaId, @UpitnikId, @Vrijednost, @Tip, @Vrsta) 

		SELECT @Id = SCOPE_IDENTITY(), @ERROR = @@ERROR

		IF @ERROR<>0
			ROLLBACK TRAN
		ELSE
			COMMIT TRAN

		SET NOCOUNT OFF
GO
/****** Object:  StoredProcedure [dbo].[tblHuogResult_Rezultat]    Script Date: 03/28/2013 16:38:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[tblHuogResult_Rezultat]
	@UpitnikId int = null
	, @Tip varchar(2)
as

SET NOCOUNT ON

SELECT * FROM tblHuogUpitnik
WHERE Id = @UpitnikId

select * from TblHuogRezultat where [Tip] =@Tip and Vrsta = 'BP' order by GrupaId

select * from TblHuogRezultat where [Tip] =@Tip and Vrsta = 'AV' order by GrupaId

select * from TblHuogRezultat where [Tip] =@Tip and UpitnikID = @UpitnikId order by GrupaId

SET NOCOUNT OFF
GO
/****** Object:  StoredProcedure [dbo].[tblHuogPitanje_update]    Script Date: 03/28/2013 16:38:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[tblHuogPitanje_update]
			@Id int
			, @GrupaId int
			, @Oznaka varchar(3) = NULL
			, @Naziv nvarchar(500)
			, @Opis nvarchar(max) = NULL
			, @Faktor decimal(10,2)
			, @Aktivno bit
    
		as

		SET NOCOUNT ON

		DECLARE @ERROR INT
		BEGIN TRAN

		UPDATE tblHuogPitanje SET 
		GrupaId = @GrupaId
			, Oznaka = @Oznaka
			, Naziv = @Naziv
			, Opis = @Opis
			, Faktor = @Faktor
			, Aktivno = @Aktivno
WHERE
		Id = @Id
 

		SET @ERROR = @@ERROR
		IF @ERROR<>0
			ROLLBACK TRAN
		ELSE
			COMMIT TRAN

		SET NOCOUNT OFF
GO
/****** Object:  StoredProcedure [dbo].[tblHuogPitanje_List]    Script Date: 03/28/2013 16:38:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[tblHuogPitanje_List]

as

SET NOCOUNT ON

Select * from tblHuogPitanje

SET NOCOUNT OFF
GO
/****** Object:  StoredProcedure [dbo].[tblHuogPitanje_insert]    Script Date: 03/28/2013 16:38:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[tblHuogPitanje_insert]
			@Id int output
			, @GrupaId int
			, @Oznaka varchar(3) = NULL
			, @Naziv nvarchar(500)
			, @Opis nvarchar(max) = NULL
			, @Faktor decimal(10,2)
			, @Aktivno bit
    
		as

		SET NOCOUNT ON

		DECLARE @ERROR INT
		BEGIN TRAN

		INSERT INTO tblHuogPitanje ( 
		[GrupaId], [Oznaka], [Naziv], [Opis], [Faktor], [Aktivno]) VALUES (@GrupaId, @Oznaka, @Naziv, @Opis, @Faktor, @Aktivno) 

		SELECT @Id = SCOPE_IDENTITY(), @ERROR = @@ERROR

		IF @ERROR<>0
			ROLLBACK TRAN
		ELSE
			COMMIT TRAN

		SET NOCOUNT OFF
GO
/****** Object:  StoredProcedure [dbo].[tblHuogGrupa_update]    Script Date: 03/28/2013 16:38:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[tblHuogGrupa_update]
			@Id int
			, @RedniBroj int = NULL
			, @Naziv nvarchar(150)
			, @Opis nvarchar(max) = NULL
			, @FaktorMedju decimal(10,2)
			, @FaktorKrajnji decimal(10,2)
			, @Aktivno bit
    
		as

		SET NOCOUNT ON

		DECLARE @ERROR INT
		BEGIN TRAN

		UPDATE tblHuogGrupa SET 
		RedniBroj = @RedniBroj
			, Naziv = @Naziv
			, Opis = @Opis
			, FaktorMedju = @FaktorMedju
			, FaktorKrajnji = @FaktorKrajnji
			, Aktivno = @Aktivno
WHERE
		Id = @Id
 

		SET @ERROR = @@ERROR
		IF @ERROR<>0
			ROLLBACK TRAN
		ELSE
			COMMIT TRAN

		SET NOCOUNT OFF
GO
/****** Object:  StoredProcedure [dbo].[tblHuogGrupa_List]    Script Date: 03/28/2013 16:38:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[tblHuogGrupa_List]

as

SET NOCOUNT ON

Select * from tblHuogGrupa

SET NOCOUNT OFF
GO
/****** Object:  StoredProcedure [dbo].[tblHuogGrupa_insert]    Script Date: 03/28/2013 16:38:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[tblHuogGrupa_insert]
			@Id int output
			, @RedniBroj int = NULL
			, @Naziv nvarchar(150)
			, @Opis nvarchar(max) = NULL
			, @FaktorMedju decimal(10,2)
			, @FaktorKrajnji decimal(10,2)
			, @Aktivno bit
    
		as

		SET NOCOUNT ON

		DECLARE @ERROR INT
		BEGIN TRAN

		INSERT INTO tblHuogGrupa ( 
		[RedniBroj], [Naziv], [Opis], [FaktorMedju], [FaktorKrajnji], [Aktivno]) VALUES (@RedniBroj, @Naziv, @Opis, @FaktorMedju, @FaktorKrajnji, @Aktivno) 

		SELECT @Id = SCOPE_IDENTITY(), @ERROR = @@ERROR

		IF @ERROR<>0
			ROLLBACK TRAN
		ELSE
			COMMIT TRAN

		SET NOCOUNT OFF
GO
/****** Object:  Default [DF_tblHuogGrupa_Aktivno]    Script Date: 03/28/2013 16:38:05 ******/
ALTER TABLE [dbo].[tblHuogGrupa] ADD  CONSTRAINT [DF_tblHuogGrupa_Aktivno]  DEFAULT ((1)) FOR [Aktivno]
GO
/****** Object:  Default [DF_tblHuogPitanje_Aktivno]    Script Date: 03/28/2013 16:38:05 ******/
ALTER TABLE [dbo].[tblHuogPitanje] ADD  CONSTRAINT [DF_tblHuogPitanje_Aktivno]  DEFAULT ((1)) FOR [Aktivno]
GO
/****** Object:  Default [DF_tblHuogUser_Status]    Script Date: 03/28/2013 16:38:05 ******/
ALTER TABLE [dbo].[tblHuogUser] ADD  CONSTRAINT [DF_tblHuogUser_Status]  DEFAULT ((0)) FOR [Status]
GO
