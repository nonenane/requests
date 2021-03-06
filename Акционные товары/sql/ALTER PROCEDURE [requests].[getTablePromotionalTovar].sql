USE [dbase1]
GO
/****** Object:  StoredProcedure [requests].[getTablePromotionalTovar]    Script Date: 08.10.2020 14:25:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		SAA
-- Create date: 2019-10-17
-- Description:	Таблица для просмотра реализации акционного товара
ALTER PROCEDURE [requests].[getTablePromotionalTovar] 
	@dateStart datetime,
	@dateEnd datetime
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;
	DECLARE @SQL nvarchar(Max) =''
	DECLARE @SQL2 nvarchar(Max) =''
	DECLARE @journal nvarchar(MAX) = 'journal'
	Create table #table (time datetime, ean nvarchar(13), count505 numeric(14,3), price int, summa numeric(14,3))
	Create table #table2  (time datetime, ean nvarchar(13), count507 numeric(14,3), price int, summa numeric(14,3))

	SET @journal = @journal+'_'+cast(year(@dateStart) as nvarchar(4))+'_'+case when MONTH(@dateStart)<10 then '0' else '' end +cast( MONTH(@dateStart)  as nvarchar(2))
	set @dateStart = DATEADD (hour, 6, @dateStart)
	set @dateEnd = DATEADD (hour, 3, @dateEnd)

	SET @SQL = 'select case when CAST(time AS Time) < ''03:00'' then cast(dateADD(hour, -3, time) as DATE) else  CAST(time AS DATE) end as time, ltrim(rtrim(ean)), cast(SUM(j.count) as numeric(14,3))/1000 as count505, price, cast(sum(cash_val) as numeric(14,3))/100 as summa
					from [KassRealiz].[dbo].['+@journal+'] j
					 LEFT JOIN (select terminal, doc_id 
					 from [KassRealiz].[dbo].['+@journal+']
					 where op_code = 512  ) j2 on j2.doc_id = j.doc_id and j2.terminal = j.terminal 
			where j2.doc_id is null and
			j2.terminal is null and
			j.op_code = 505  
			and '''+convert(nvarchar(max),@dateStart,120)+'''<=time and time<='''+convert(nvarchar(max),@dateEnd,120)+'''' 
			+ 'group by j.ean, case when CAST(time AS Time) < ''3:00'' then cast(dateADD(hour, -3, time) as DATE) else  CAST(time AS DATE) end, price'

	SET @SQL2 = 'select case when CAST(time AS Time) < ''03:00'' then cast(dateADD(hour, -3, time) as DATE) else  CAST(time AS DATE) end as time, ltrim(rtrim(ean)), cast(SUM(j.count) as numeric(14,3))/1000 as count507, price, cast(sum(cash_val) as numeric(14,3))/100 as summa
					from [KassRealiz].[dbo].['+@journal+'] j
					 LEFT JOIN (select terminal, doc_id 
					 from [KassRealiz].[dbo].['+@journal+']
					 where op_code = 512  ) j2 on j2.doc_id = j.doc_id and j2.terminal = j.terminal 
			where j2.doc_id is null and
			j2.terminal is null and
			j.op_code = 507  
			and '''+convert(nvarchar(max),@dateStart,120)+'''<=time and time<='''+convert(nvarchar(max),@dateEnd,120)+'''' 
			+ 'group by j.ean, case when CAST(time AS Time) < ''03:00'' then cast(dateADD(hour, -3, time) as DATE) else  CAST(time AS DATE) end, price, op_code'
	
	INSERT INTO #table
	EXEC(@SQL)

	INSERT INTO #table2
	EXEC(@SQL2)

	SELECT 
		s_ntovar.id_tovar,
		t1.ean as code, 
		s_ntovar.cname as name,
		t1.time as date, 
		(isnull(count505,0) - isnull(count507,0)) as count,
		cast(t1.price as numeric(14,2))/100 as price,
		isnull(t1.summa,0) - isnull(t2.summa,0) as summa,
		s_tovar.id_otdel,
		ltrim(rtrim(dep.name)) as nameDep
	FROM #table t1
		 LEFT JOIN #table2 t2 on t1.ean like t2.ean and cast(t1.time as date) = cast(t2.time as date) and t1.price = t2.price
		 JOIN dbo.s_tovar s_tovar on replace(s_tovar.ean, ' ', '') like replace(t1.ean, ' ', '') 
		 JOIN [requests].[s_CatalogPromotionalTovars] scpt on scpt.id_tovar = s_tovar.id
		 JOIN (select s_ntovar.id_tovar, s_ntovar.cname  from dbo.s_ntovar  s_ntovar		 
		 where s_ntovar.tdate_n = (select top 1 s_ntovar2.tdate_n 
									from dbo.s_ntovar s_ntovar2
									where s_ntovar.id_tovar = s_ntovar2.id_tovar
									order by s_ntovar2.tdate_n desc) ) s_ntovar on s_ntovar.id_tovar = s_tovar.id 
									left join dbo.departments dep on dep.id = s_tovar.id_otdel
		order by t1.time , s_ntovar.cname  asc
END




