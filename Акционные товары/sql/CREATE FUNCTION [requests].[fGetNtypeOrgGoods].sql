USE [dbase1]
GO
/****** Object:  UserDefinedFunction [Arenda2].[ConvertDateToString]    Script Date: 09.10.2020 12:09:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G Y
-- Create date: 2020-10-09
-- Description:	Получение ЮЛ для товара на дату
-- =============================================
ALTER FUNCTION [requests].[fGetNtypeOrgGoods]
(
	@id_tovar int,
	@date date
)
RETURNS int
AS
BEGIN
	

DECLARE @id_otdel int = (select top(1) id_otdel from dbo.s_tovar where id=@id_tovar)
DECLARE @id_grp1  int = (select top(1) id_grp1 from dbo.s_tovar where id=@id_tovar)
DECLARE @ntypeorg int, @Abbriviation varchar(5)
DECLARE @tmp1 table (id_tovar int,[date] date,ntypeorg int ) 
DECLARE @tmp_FvD_ntypeorg  table (ntypeorg int,id_departments int ) 
DECLARE @TMP_2  table (nTypeOrg int,Abbriviation varchar(10) ) 
DECLARE @tmp_tovar_GvF  table (id_tovar int,ntypeorg int,Abbriviation varchar(10),id_grp1 int) 
DECLARE @tmp_tovar_Grp table (id int,ntypeorg int,Abbriviation varchar(10)) 


insert into @tmp1 (id_tovar,[date],ntypeorg)
select distinct a.id_tovar,a.date,g.ntypeorg   from (
select g.id_tovar,max(g.date) as date from [dbo].[goods_vs_firms] g  where g.date<=@date and @id_tovar = @id_tovar --and send = 1
GROUP BY g.id_tovar)  a inner join  [dbo].[goods_vs_firms] g  on g.date = a.date and g.id_tovar = a.id_tovar --and g.send = 1


INSERT INTO @tmp_FvD_ntypeorg (ntypeorg,id_departments)
select ntypeorg,f.id_departments 
from dbo.firms_vs_departments f
where f.DateStart<= @date AND @date <= f.DateEnd and f.id_departments = @id_otdel

INSERT INTO @TMP_2 (nTypeOrg,Abbriviation)
select nTypeOrg,Abbriviation 
from [dbo].[s_MainOrg] 
where isSeler = 1 and DateStart<=@date and @date<=DateEnd

INSERT INTO @tmp_tovar_GvF(id_tovar,ntypeorg,Abbriviation,id_grp1)
select distinct t.id_tovar,cast(smo.nTypeOrg as int) as ntypeorg,t2.Abbriviation,tov.id_grp1

from @tmp1 t
	LEFT JOIN dbo.s_tovar tov on tov.id = t.id_tovar
	LEFT JOIN @tmp_FvD_ntypeorg n on n.ntypeorg= t.ntypeorg
	LEFT JOIN @TMP_2 t2 on t2.nTypeOrg = n.ntypeorg
	LEFT JOIN dbo.s_SelectedMainOrg smo on smo.nTypeOrg = t2.nTypeOrg
where t.id_tovar = @id_tovar
	   	
		
INSERT INTO @tmp_tovar_Grp (id,ntypeorg,Abbriviation)
select distinct g.id,cast(smo.nTypeOrg as int) as ntypeorg,t2.Abbriviation 
from dbo.s_grp1 g
	LEFT JOIN @tmp_FvD_ntypeorg n on n.ntypeorg= g.ntypeorg
	LEFT JOIN @TMP_2 t2 on t2.nTypeOrg = n.ntypeorg
	LEFT JOIN dbo.s_SelectedMainOrg smo on smo.nTypeOrg = t2.nTypeOrg
where 
	g.id_otdel = @id_otdel and 
	smo.nTypeOrg is not null and g.id = @id_grp1



select @ntypeorg= t.nTypeOrg, @Abbriviation = t.Abbriviation 
from dbo.firms_vs_departments f inner join @TMP_2 t on t.nTypeOrg = f.ntypeorg
where 
	f.id_departments = @id_otdel and 
	f.[default] = 1 and f.DateStart<=@date and @date<=f.DateEnd


	DECLARE @sendNtypeOrg int

if exists(select * from @tmp_tovar_GvF where ntypeorg is not null)
	begin
		select top(1) @sendNtypeOrg =  t.ntypeorg from @tmp_tovar_GvF t	 where ntypeorg is not null	
	end
else
if exists(select * from @tmp_tovar_Grp where ntypeorg is not null)
	begin
		select top(1) @sendNtypeOrg = t.ntypeorg from @tmp_tovar_Grp t	 where ntypeorg is not null	
	end
else
	BEGIN
		SET @sendNtypeOrg =  @ntypeorg
	END

RETURN @sendNtypeOrg

END
