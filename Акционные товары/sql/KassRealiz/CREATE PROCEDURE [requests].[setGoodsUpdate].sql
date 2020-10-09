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
CREATE PROCEDURE [requests].[setGoodsUpdate]	
	@id_departments int,
	@sender varchar(10),
	@tax smallint,
	@grp int,
	@dpt int,
	@price int,
	@name varchar(40),
	@ean varchar(13)
AS
BEGIN
	SET NOCOUNT ON;


	UPDATE dbo.goods_updates  SET ActualRow = 0 WHERE ltrim(rtrim(ean)) = ltrim(rtrim(@ean)) and ActualRow = 1
		
	INSERT INTO dbo.goods_updates (ean,name,price,t1,t2,dpt,grp,tax,r_time,s_time,sender,s,id_departments,ActualRow)
	VALUES (@ean,@name,@price,'00:00:00','00:00:00',@dpt,@grp,@tax,GETDATE(),GETDATE(),@sender,'F',@id_departments,1)



END
