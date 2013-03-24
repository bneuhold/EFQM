
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


-----------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------------------------------
