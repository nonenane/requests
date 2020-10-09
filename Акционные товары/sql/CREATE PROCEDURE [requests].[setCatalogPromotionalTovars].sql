/****** Object:  StoredProcedure [Goods_Card_New].[spg_getGrp3]    Script Date: 29.09.2020 21:31:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-09-29
-- Description:	Добавление или удаление товара по переоценки
-- =============================================
ALTER PROCEDURE [requests].[setCatalogPromotionalTovars]	
	@id_tovar int,
	@rcena numeric(15,2),
	@rcenaDiscount numeric(15,2),
	@id_user int,
	@isDel bit
AS
BEGIN
	SET NOCOUNT ON;

IF @isDel = 0
	BEGIN

		UPDATE dbo.s_rcena SET isActual = 0  where id_tovar = @id_tovar and isActual = 1

		INSERT INTO dbo.s_rcena (id_tovar,rcena,tdate_k,tdate_n,isActual,id_Creator,DateCreate)
			VALUES (@id_tovar,@rcenaDiscount,null,GETDATE(),1,@id_user,GETDATE())



		IF not exists(select top(1) id from requests.s_CatalogPromotionalTovars where id_tovar = @id_tovar)
			BEGIN
				INSERT INTO requests.s_CatalogPromotionalTovars (id_tovar,Price,SalePrice,id_Creator,id_Editor,DateCreate,DateEdit)
					values (@id_tovar,@rcena,@rcenaDiscount,@id_user,@id_user,GETDATE(),GETDATE())
			END
		ELSE
			BEGIN
				UPDATE 
					requests.s_CatalogPromotionalTovars
				SET 
					Price = @rcena,
					SalePrice = @rcenaDiscount,
					id_Editor=@id_user,
					DateEdit = GETDATE()
				where 
					id_tovar  = @id_tovar

			END


		INSERT INTO requests.j_CatalogPromotionalTovars (id_tovar,Price,SalePrice,id_Creator,DateCreate)
			values (@id_tovar,@rcena,@rcenaDiscount,@id_user,GETDATE())

		INSERT INTO [dbo].[j_EditGoodsCard](id_tovar,flag,id_Editor,DateEdit)
			VALUES (@id_tovar,1,@id_user,GETDATE())
		
	END
ELSE
	BEGIN
		INSERT INTO requests.j_CatalogPromotionalTovars (id_tovar,Price,SalePrice,id_Creator,DateCreate)
			values (@id_tovar,@rcena,@rcenaDiscount,@id_user,GETDATE())

		UPDATE dbo.s_rcena SET isActual = 0  where id_tovar = @id_tovar and isActual = 1

		INSERT INTO dbo.s_rcena (id_tovar,rcena,tdate_k,tdate_n,isActual,id_Creator,DateCreate)
			VALUES (@id_tovar,@rcenaDiscount,null,GETDATE(),1,@id_user,GETDATE())
			
		INSERT INTO [dbo].[j_EditGoodsCard](id_tovar,flag,id_Editor,DateEdit)
			VALUES (@id_tovar,1,@id_user,GETDATE())

		DELETE FROM requests.s_CatalogPromotionalTovars where id_tovar = @id_tovar 

	END

END
