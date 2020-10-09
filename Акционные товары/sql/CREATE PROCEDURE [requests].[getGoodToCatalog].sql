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
-- Description:	Получение товара для каталога акций
ALTER PROCEDURE [requests].[getGoodToCatalog] 
		@ean varchar(13),
		@isValidate bit= 1
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	


DECLARE		
	@id_tovar int,@cName varchar(1024),@ntypetovar int,@id_deps int,@nds int,@id_grp1 int

	select 
		@id_tovar  = t.id,		
		@cName = ltrim(isnull(tt.cName+' ','')+ ltrim(rtrim(n.cname))),
		@ntypetovar = n.ntypetovar,
		@id_deps = t.id_otdel,
		@nds = cast(nd.nds as int),
		@id_grp1 = t.id_grp1
	from 
		dbo.s_tovar t
			inner join dbo.s_ntovar n on n.id_tovar = t.id
			left join dbo.s_TypeTovar tt on tt.id = n.ntypetovar
			left join dbo.s_nds nd on nd.id = t.id_nds

	where 
		ltrim(rtrim(t.ean))=@ean

		if(@id_tovar is null)
			BEGIN
				select -1 as id,'Товар не найден'as msg
				return;
			END

		IF @ntypetovar !=0 
			BEGIN
				select -1 as id,'Товар находиться в резерке.\nВвод данных невозможен.'as msg
				return;
			END

		IF exists (select top(1) id from requests.s_CatalogPromotionalTovars where id_tovar = @id_tovar) and @isValidate = 1
			BEGIN
				select -1 as id,'Для данного товара уже существует акция.\nВвод данных невозможен.'as msg
				return;
			END






select 
	0 as id, 
	@id_tovar as id_tovar,
	@cName as cName,
	@id_deps as id_deps,
	@nds as nds,
	[requests].[fGetNtypeOrgGoods](@id_tovar,GETDATE()) as ntypeorg,
	@id_grp1 as id_grp1

END