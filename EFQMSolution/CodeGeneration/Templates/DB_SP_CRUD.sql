
-- Next line is only needed on SQL 2008


		IF  EXISTS (SELECT * FROM sys.objects 
			WHERE object_id = OBJECT_ID(N'tblHuogGrupa_insert') AND type in (N'P', N'PC'))
		DROP PROCEDURE  tblHuogGrupa_insert
		GO
		
		CREATE procedure tblHuogGrupa_insert
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




	
		IF  EXISTS (SELECT * FROM sys.objects 
			WHERE object_id = OBJECT_ID(N'tblHuogGrupa_update') AND type in (N'P', N'PC'))
		DROP PROCEDURE  tblHuogGrupa_update
		GO
		
		CREATE procedure tblHuogGrupa_update
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


-----------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------------------------------
		IF  EXISTS (SELECT * FROM sys.objects 
			WHERE object_id = OBJECT_ID(N'tblHuogPitanje_insert') AND type in (N'P', N'PC'))
		DROP PROCEDURE  tblHuogPitanje_insert
		GO
		
		CREATE procedure tblHuogPitanje_insert
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




	
		IF  EXISTS (SELECT * FROM sys.objects 
			WHERE object_id = OBJECT_ID(N'tblHuogPitanje_update') AND type in (N'P', N'PC'))
		DROP PROCEDURE  tblHuogPitanje_update
		GO
		
		CREATE procedure tblHuogPitanje_update
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


-----------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------------------------------
		IF  EXISTS (SELECT * FROM sys.objects 
			WHERE object_id = OBJECT_ID(N'tblHuogRezultat_insert') AND type in (N'P', N'PC'))
		DROP PROCEDURE  tblHuogRezultat_insert
		GO
		
		CREATE procedure tblHuogRezultat_insert
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




	
		IF  EXISTS (SELECT * FROM sys.objects 
			WHERE object_id = OBJECT_ID(N'tblHuogRezultat_update') AND type in (N'P', N'PC'))
		DROP PROCEDURE  tblHuogRezultat_update
		GO
		
		CREATE procedure tblHuogRezultat_update
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


-----------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------------------------------
		IF  EXISTS (SELECT * FROM sys.objects 
			WHERE object_id = OBJECT_ID(N'tblHuogTecaj_insert') AND type in (N'P', N'PC'))
		DROP PROCEDURE  tblHuogTecaj_insert
		GO
		
		CREATE procedure tblHuogTecaj_insert
			@Id int output
			, @Title nvarchar(250)
			, @DateFrom smalldatetime = NULL
			, @DateTo smalldatetime = NULL
			, @Active bit
			, @Description nvarchar(max) = NULL
    
		as

		SET NOCOUNT ON

		DECLARE @ERROR INT
		BEGIN TRAN

		INSERT INTO tblHuogTecaj ( 
		[Title], [DateFrom], [DateTo], [Active], [Description]) VALUES (@Title, @DateFrom, @DateTo, @Active, @Description) 

		SELECT @Id = SCOPE_IDENTITY(), @ERROR = @@ERROR

		IF @ERROR<>0
			ROLLBACK TRAN
		ELSE
			COMMIT TRAN

		SET NOCOUNT OFF
        GO




	
		IF  EXISTS (SELECT * FROM sys.objects 
			WHERE object_id = OBJECT_ID(N'tblHuogTecaj_update') AND type in (N'P', N'PC'))
		DROP PROCEDURE  tblHuogTecaj_update
		GO
		
		CREATE procedure tblHuogTecaj_update
			@Id int
			, @Title nvarchar(250)
			, @DateFrom smalldatetime = NULL
			, @DateTo smalldatetime = NULL
			, @Active bit
			, @Description nvarchar(max) = NULL
    
		as

		SET NOCOUNT ON

		DECLARE @ERROR INT
		BEGIN TRAN

		UPDATE tblHuogTecaj SET 
		Title = @Title
			, DateFrom = @DateFrom
			, DateTo = @DateTo
			, Active = @Active
			, Description = @Description
WHERE
		Id = @Id
 

		SET @ERROR = @@ERROR
		IF @ERROR<>0
			ROLLBACK TRAN
		ELSE
			COMMIT TRAN

		SET NOCOUNT OFF
        GO


-----------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------------------------------
		IF  EXISTS (SELECT * FROM sys.objects 
			WHERE object_id = OBJECT_ID(N'tblHuogTecajPolaznik_insert') AND type in (N'P', N'PC'))
		DROP PROCEDURE  tblHuogTecajPolaznik_insert
		GO
		
		CREATE procedure tblHuogTecajPolaznik_insert
			@UserId int output
			@TecajId int output
			, @DateCreated smalldatetime
    
		as

		SET NOCOUNT ON

		DECLARE @ERROR INT
		BEGIN TRAN

		INSERT INTO tblHuogTecajPolaznik ( 
		[DateCreated]) VALUES (@DateCreated) 

		SELECT @TecajId = SCOPE_IDENTITY(), @ERROR = @@ERROR

		IF @ERROR<>0
			ROLLBACK TRAN
		ELSE
			COMMIT TRAN

		SET NOCOUNT OFF
        GO




	
		IF  EXISTS (SELECT * FROM sys.objects 
			WHERE object_id = OBJECT_ID(N'tblHuogTecajPolaznik_update') AND type in (N'P', N'PC'))
		DROP PROCEDURE  tblHuogTecajPolaznik_update
		GO
		
		CREATE procedure tblHuogTecajPolaznik_update
			@UserId int
			, @TecajId int
			, @DateCreated smalldatetime
    
		as

		SET NOCOUNT ON

		DECLARE @ERROR INT
		BEGIN TRAN

		UPDATE tblHuogTecajPolaznik SET 
		DateCreated = @DateCreated
WHERE
		UserId = @UserId
			 AND TecajId = @TecajId
 

		SET @ERROR = @@ERROR
		IF @ERROR<>0
			ROLLBACK TRAN
		ELSE
			COMMIT TRAN

		SET NOCOUNT OFF
        GO


-----------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------------------------------
		IF  EXISTS (SELECT * FROM sys.objects 
			WHERE object_id = OBJECT_ID(N'tblHuogUpitnik_insert') AND type in (N'P', N'PC'))
		DROP PROCEDURE  tblHuogUpitnik_insert
		GO
		
		CREATE procedure tblHuogUpitnik_insert
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




	
		IF  EXISTS (SELECT * FROM sys.objects 
			WHERE object_id = OBJECT_ID(N'tblHuogUpitnik_update') AND type in (N'P', N'PC'))
		DROP PROCEDURE  tblHuogUpitnik_update
		GO
		
		CREATE procedure tblHuogUpitnik_update
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


-----------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------------------------------
		IF  EXISTS (SELECT * FROM sys.objects 
			WHERE object_id = OBJECT_ID(N'tblHuogUpitnikVrijednost_insert') AND type in (N'P', N'PC'))
		DROP PROCEDURE  tblHuogUpitnikVrijednost_insert
		GO
		
		CREATE procedure tblHuogUpitnikVrijednost_insert
			@PitanjeId int output
			@UpitnikId int output
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
		[Vrijednost1], [Vrijednost2], [Vrijednost3], [Vrijednost4], [Vrijednost5], [Vrijednost6], [Vrijednost7], [Vrijednost8], [Vrijednost9], [Vrijednost10]) VALUES (@Vrijednost1, @Vrijednost2, @Vrijednost3, @Vrijednost4, @Vrijednost5, @Vrijednost6, @Vrijednost7, @Vrijednost8, @Vrijednost9, @Vrijednost10) 

		SELECT @UpitnikId = SCOPE_IDENTITY(), @ERROR = @@ERROR

		IF @ERROR<>0
			ROLLBACK TRAN
		ELSE
			COMMIT TRAN

		SET NOCOUNT OFF
        GO




	
		IF  EXISTS (SELECT * FROM sys.objects 
			WHERE object_id = OBJECT_ID(N'tblHuogUpitnikVrijednost_update') AND type in (N'P', N'PC'))
		DROP PROCEDURE  tblHuogUpitnikVrijednost_update
		GO
		
		CREATE procedure tblHuogUpitnikVrijednost_update
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
		Vrijednost1 = @Vrijednost1
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


-----------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------------------------------
		IF  EXISTS (SELECT * FROM sys.objects 
			WHERE object_id = OBJECT_ID(N'tblHuogUser_insert') AND type in (N'P', N'PC'))
		DROP PROCEDURE  tblHuogUser_insert
		GO
		
		CREATE procedure tblHuogUser_insert
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
			, @Status int
    
		as

		SET NOCOUNT ON

		DECLARE @ERROR INT
		BEGIN TRAN

		INSERT INTO tblHuogUser ( 
		[Name], [Email], [Password], [CompanyName], [City], [Type], [Employees], [Income], [Newsletter], [Status]) VALUES (@Name, @Email, @Password, @CompanyName, @City, @Type, @Employees, @Income, @Newsletter, @Status) 

		SELECT @Id = SCOPE_IDENTITY(), @ERROR = @@ERROR

		IF @ERROR<>0
			ROLLBACK TRAN
		ELSE
			COMMIT TRAN

		SET NOCOUNT OFF
        GO




	
		IF  EXISTS (SELECT * FROM sys.objects 
			WHERE object_id = OBJECT_ID(N'tblHuogUser_update') AND type in (N'P', N'PC'))
		DROP PROCEDURE  tblHuogUser_update
		GO
		
		CREATE procedure tblHuogUser_update
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
			, @Status int
    
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
			, Status = @Status
WHERE
		Id = @Id
 

		SET @ERROR = @@ERROR
		IF @ERROR<>0
			ROLLBACK TRAN
		ELSE
			COMMIT TRAN

		SET NOCOUNT OFF
        GO


-----------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------------------------------
		IF  EXISTS (SELECT * FROM sys.objects 
			WHERE object_id = OBJECT_ID(N'tblSeminarRegistration_insert') AND type in (N'P', N'PC'))
		DROP PROCEDURE  tblSeminarRegistration_insert
		GO
		
		CREATE procedure tblSeminarRegistration_insert
			@seminarRegistrationID int output
			, @firstName nvarchar(50)
			, @lastName nvarchar(50)
			, @userOIB varchar(11)
			, @academicTitle nvarchar(250) = NULL
			, @academicDegree nvarchar(50) = NULL
			, @dateOfBirth smalldatetime = NULL
			, @placeOfBirth nvarchar(250) = NULL
			, @address nvarchar(200) = NULL
			, @apartment nvarchar(50) = NULL
			, @city nvarchar(200) = NULL
			, @ZIP nvarchar(10) = NULL
			, @email nvarchar(50) = NULL
			, @mobile nvarchar(50) = NULL
			, @telephone nvarchar(50) = NULL
			, @companyName nvarchar(250) = NULL
			, @companyPhone nvarchar(20) = NULL
			, @companyMob nvarchar(20) = NULL
			, @companyFax nvarchar(20) = NULL
			, @companyAddress nvarchar(250) = NULL
			, @companyCity nvarchar(250) = NULL
			, @companyZIP nvarchar(250) = NULL
			, @companyOIB varchar(11) = NULL
			, @companyWorkPosition nvarchar(250) = NULL
			, @userType varchar(2) = NULL
    
		as

		SET NOCOUNT ON

		DECLARE @ERROR INT
		BEGIN TRAN

		INSERT INTO tblSeminarRegistration ( 
		[firstName], [lastName], [userOIB], [academicTitle], [academicDegree], [dateOfBirth], [placeOfBirth], [address], [apartment], [city], [ZIP], [email], [mobile], [telephone], [companyName], [companyPhone], [companyMob], [companyFax], [companyAddress], [companyCity], [companyZIP], [companyOIB], [companyWorkPosition], [userType]) VALUES (@firstName, @lastName, @userOIB, @academicTitle, @academicDegree, @dateOfBirth, @placeOfBirth, @address, @apartment, @city, @ZIP, @email, @mobile, @telephone, @companyName, @companyPhone, @companyMob, @companyFax, @companyAddress, @companyCity, @companyZIP, @companyOIB, @companyWorkPosition, @userType) 

		SELECT @seminarRegistrationID = SCOPE_IDENTITY(), @ERROR = @@ERROR

		IF @ERROR<>0
			ROLLBACK TRAN
		ELSE
			COMMIT TRAN

		SET NOCOUNT OFF
        GO




	
		IF  EXISTS (SELECT * FROM sys.objects 
			WHERE object_id = OBJECT_ID(N'tblSeminarRegistration_update') AND type in (N'P', N'PC'))
		DROP PROCEDURE  tblSeminarRegistration_update
		GO
		
		CREATE procedure tblSeminarRegistration_update
			@seminarRegistrationID int
			, @firstName nvarchar(50)
			, @lastName nvarchar(50)
			, @userOIB varchar(11)
			, @academicTitle nvarchar(250) = NULL
			, @academicDegree nvarchar(50) = NULL
			, @dateOfBirth smalldatetime = NULL
			, @placeOfBirth nvarchar(250) = NULL
			, @address nvarchar(200) = NULL
			, @apartment nvarchar(50) = NULL
			, @city nvarchar(200) = NULL
			, @ZIP nvarchar(10) = NULL
			, @email nvarchar(50) = NULL
			, @mobile nvarchar(50) = NULL
			, @telephone nvarchar(50) = NULL
			, @companyName nvarchar(250) = NULL
			, @companyPhone nvarchar(20) = NULL
			, @companyMob nvarchar(20) = NULL
			, @companyFax nvarchar(20) = NULL
			, @companyAddress nvarchar(250) = NULL
			, @companyCity nvarchar(250) = NULL
			, @companyZIP nvarchar(250) = NULL
			, @companyOIB varchar(11) = NULL
			, @companyWorkPosition nvarchar(250) = NULL
			, @userType varchar(2) = NULL
    
		as

		SET NOCOUNT ON

		DECLARE @ERROR INT
		BEGIN TRAN

		UPDATE tblSeminarRegistration SET 
		firstName = @firstName
			, lastName = @lastName
			, userOIB = @userOIB
			, academicTitle = @academicTitle
			, academicDegree = @academicDegree
			, dateOfBirth = @dateOfBirth
			, placeOfBirth = @placeOfBirth
			, address = @address
			, apartment = @apartment
			, city = @city
			, ZIP = @ZIP
			, email = @email
			, mobile = @mobile
			, telephone = @telephone
			, companyName = @companyName
			, companyPhone = @companyPhone
			, companyMob = @companyMob
			, companyFax = @companyFax
			, companyAddress = @companyAddress
			, companyCity = @companyCity
			, companyZIP = @companyZIP
			, companyOIB = @companyOIB
			, companyWorkPosition = @companyWorkPosition
			, userType = @userType
WHERE
		seminarRegistrationID = @seminarRegistrationID
 

		SET @ERROR = @@ERROR
		IF @ERROR<>0
			ROLLBACK TRAN
		ELSE
			COMMIT TRAN

		SET NOCOUNT OFF
        GO


-----------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------------------------------
