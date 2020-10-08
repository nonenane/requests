USE [dbase1]
GO
/****** Object:  StoredProcedure [requests].[getTablePromotionalTovar]    Script Date: 08.10.2020 16:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin
-- Create date: 2020-10-08
-- Description:	Таблица для просмотра акционного товара
CREATE PROCEDURE [requests].[getCatalogPromotionalTovars] 

AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	select
		c.id_tovar,
		t.ean,
		ltrim(isnull(tt.cName+' ','')+ ltrim(rtrim(n.cname))) as cName,
		t.id_otdel,
		ltrim(rtrim(d.name)) as nameDep,
		isnull(c.Price,0.0) as Price,
		isnull(c.SalePrice,0.0) as SalePrice,
		ltrim(rtrim(isnull(l.FIO,''))) as FIO,
		c.DateEdit
	from 
		requests.s_CatalogPromotionalTovars c
			left join dbo.s_tovar t on t.id = c.id_tovar
			left join dbo.s_ntovar n on n.id_tovar = c.id_tovar and n.isActual = 1
			left join dbo.s_TypeTovar tt on tt.id = n.ntypetovar
			left join dbo.departments d on d.id = t.id_otdel
			left join dbo.ListUsers l on l.id = c.id_Editor

END