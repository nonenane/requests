USE [dbase1]
GO
/****** Object:  StoredProcedure [Vacation].[getSettings]    Script Date: 12.10.2020 12:36:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		SPG
-- Create date: 2019-11-28
-- Description:	Получение списка акционных товаров
-- =============================================
CREATE PROCEDURE [xpos].[getCatalogPromotionalTovars]	
AS
BEGIN
	SET NOCOUNT ON;

	select 
		id_tovar,
		Price,
		SalePrice 
	from 
		requests.s_CatalogPromotionalTovars
	where 
		Price is not null and SalePrice is not null
	
END
