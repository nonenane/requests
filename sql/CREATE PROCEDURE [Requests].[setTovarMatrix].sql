USE [dbase1]
GO
/****** Object:  StoredProcedure [Requests].[GetGoods]    Script Date: 08.10.2020 9:44:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G Y
-- Create date: 2020-10-08
-- Description: Запись и удаление товара их матрицы
-- =============================================
CREATE PROCEDURE [Requests].[setTovarMatrix]
	@id_tovar int,
	@id_user int,
	@isDel bit
--with recompile 
AS
BEGIN
	
	SET NOCOUNT ON;

	IF @isDel = 0
		BEGIN
			INSERT INTO requests.s_TovarMatrix (id_tovar,id_Creator,DateCreate)
				VALUES (@id_tovar,@id_user,GETDATE())

			select cast(SCOPE_IDENTITY() as int ) as id
		END
	ELSE
		BEGIN

			DELETE FROM requests.s_TovarMatrix where id_tovar = @id_tovar
			
			Select 0 as id

		END

END