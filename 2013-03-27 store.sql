SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
		
ALTER procedure [dbo].[tblHuogResult_Rezultat]
	@UpitnikId int = null
	, @Uuid uniqueidentifier = null
	, @Tip varchar(2)
as

SET NOCOUNT ON

if (@UpitnikId is null)
begin
	select @UpitnikId = Id from tblHuogUpitnik where uuid = @Uuid
end

SELECT * FROM tblHuogUpitnik
WHERE Id = @UpitnikId

select * from TblHuogRezultat where [Tip] =@Tip and Vrsta = 'BP' order by GrupaId

select * from TblHuogRezultat where [Tip] =@Tip and Vrsta = 'AV' order by GrupaId

select * from TblHuogRezultat where [Tip] =@Tip and UpitnikID = @UpitnikId order by GrupaId

SET NOCOUNT OFF