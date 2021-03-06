USE [dbase1]
GO
/****** Object:  StoredProcedure [Requests].[GetGoods]    Script Date: 12.10.2020 13:25:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Houston
-- Create date: 2014-02-19
-- Description: Получение списка товаров
-- =============================================
-- exec [Requests].[GetGoods] 1, 256, 96, 1, 0,0,0,0,0,0
ALTER PROCEDURE [Requests].[GetGoods]
	@id_status int,-- 0 - МН, 1 - КД, КНТ
	@id_prog int,
	@id_user int,
	@id_dep int,
	@id_grp1 int,
	@id_grp2 int,
	@id_grp3 int,
	@id_post int,
	@type_post int = 0,
	@id_oper1 int = 0
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @dateInv DateTime --Последняя(или предпоследняя) дата основной инвентаризации
	DECLARE @otsechBegin DateTime = CAST(DATEADD(dd,-7,GETDATE()) AS DATE)
	DECLARE @otsechEnd DateTime = CAST(DATEADD(dd,-1,GETDATE()) AS DATE)
	DECLARE @isPreDate bit = 0
	DECLARE @showZeroRows bit = 0
	
	IF EXISTS(SELECT * FROM [ISI_SERVER].dbase1.[dbo].[property_list]
				WHERE [id_val]= 'show zeroStrings'
				AND	[id_prog]= @id_prog
				AND [id_user]= @id_user
				AND LTRIM(RTRIM(val)) = '1')
		SET @showZeroRows = 1
	
	-------\\---
	declare @ltype int 
	
	if(@type_post=1)
	set @id_post = @id_post+90000
	select @ltype = ltype from s_post where id = @id_post

	print @id_post
	print @ltype
	
	declare @postDays int 
SELECT @postDays = CAST(val as int)
	FROM property_list
	WHERE id_val = 'post period'
	AND id_prog = @id_prog
	AND id_user = @id_user
	set @postDays = ISNULL (@postDays,99)
	
	
	DECLARE @DatePostStart DateTime = DATEADD(MM,-@postDays, GETDATE())
	
	create table #tov (id int primary key clustered)
	if @type_post=0 and @ltype in (1,3)
	begin
	insert into #tov(id)
    SELECT DISTINCT id_tovar
											FROM j_prihod a with (nolock)
											INNER JOIN j_allprihod b with (nolock)
											ON a.id_allprihod = b.id
											WHERE b.id_post = @id_post
											AND b.dprihod BETWEEN @DatePostStart AND GETDATE() 
											AND b.id_operand in (1,3)
	end										
	else
	begin										
	insert into #tov(id)
	SELECT DISTINCT id_tovar
											FROM j_request a with (nolock)
											INNER JOIN j_trequest b with (nolock)
											ON a.id_trequest = b.id
											WHERE b.id_post = @id_post
											AND b.data_out BETWEEN @DatePostStart AND GETDATE() 
											AND b.id_operand in (1,3)
											and a.nprimech <> 1
											and b.isActive = 1
	UNION
	SELECT DISTINCT st.id
											FROM j_newtovar nt  with (nolock) INNER JOIN j_request r with (nolock) ON nt.id=r.id_tovar 
															   INNER JOIN j_trequest tr with (nolock) ON r.id_trequest=tr.id 
															   INNER JOIN s_tovar st with (nolock) ON ltrim(rtrim(st.ean))=ltrim(rtrim(nt.ean))
											WHERE tr.id_post = @id_post
											AND tr.data_out BETWEEN @DatePostStart AND GETDATE() 
											AND tr.id_operand in (1,3)
											and r.nprimech = 1
											and tr.isActive = 1
	end
	--------\\---
	--Фильтрация по группе
	create table #tovar (id int primary key clustered,ean char(13)/*,id_otdel int*/)
	
	if (@id_grp1<>0 and not(@id_grp1 is null))
	or (@id_grp2<>0 and not(@id_grp2 is null))
	or (@id_grp3<>0 and not(@id_grp3 is null))
	OR (@id_oper1<>0 AND @id_oper1 IS NOT NULL)
	BEGIN
		
		CREATE TABLE #mnGoods(ean char(13), cname char(45), id_tovar int)
		
		DECLARE @mnSettingsExists bit = 0
		
		IF  @id_oper1 != 0 
			AND
			 EXISTS(SELECT * 
						FROM property_list 
						WHERE id_val = 'mngood' 
							AND id_prog = @id_prog 
							AND id_user = @id_user
							AND ISNUMERIC(val) =1
							AND CAST(val as INT) = @id_dep)
			SET @mnSettingsExists = 1
			
		IF(@mnSettingsExists = 1)
			INSERT INTO #mnGoods(ean, cname, id_tovar)
			EXEC [Requests].[GetGoodsForManager] @id_oper1, @id_dep, 0, 0, 1, 0, @id_user
		
		
		
		insert into #tovar(id,ean/*,id_otdel*/)
		select id,ean/*,id_otdel*/ from dbo.s_tovar tov
		where (
			   (
				(@id_grp1 = 0 AND (@id_status != 0 OR tov.id_grp1 IN (SELECT id_grp1
																	  FROM users_vs_grp1
																	  WHERE id_users = @id_user
																	 )
								   )
				 )
				 OR (@id_grp1 > 0 AND tov.id_grp1 = @id_grp1)
				 OR (@id_grp1 = -1 AND tov.id_grp1 IN (SELECT value
														FROM Requests.FilterSettings
														WHERE id_value = 'tugr'
														AND id_user = @id_user)
					 )
				)
				AND
				(@id_grp2 = 0 
					OR (@id_grp2 > 0 AND tov.id_grp2 = @id_grp2)
					OR (@id_grp2 = -1 AND tov.id_grp2 IN (SELECT value
															FROM Requests.FilterSettings
															WHERE id_value = 'invg'
															AND id_user = @id_user))
				)
				AND
				(@id_grp3 = 0 
					OR (@id_grp3 > 0 AND tov.id_grp3 = @id_grp3)
					OR (@id_grp3 = -1 AND tov.id_grp3 IN (SELECT value
															FROM Requests.FilterSettings
															WHERE id_value = 'subg'
															AND id_user = @id_user))
				)
				AND
				((@id_dep = 0 AND (@id_status != 1 OR (tov.id_otdel IN (SELECT CAST(val AS INT)
																	FROM dbo.property_list 
																	WHERE id_prog = @id_prog
																	AND id_user = @id_user
																	AND LTRIM(RTRIM(id_val)) = 'idusr'
																	AND ISNUMERIC(val) = 1 and val<>6
																	)
													
													  ) 
								  )
				  )OR tov.id_otdel = @id_dep
				  ) 
				  AND
				 (@id_post = 0 OR (tov.id in (select id from #tov)))
				 AND 
				 (@id_oper1 = 0 OR (
									(@mnSettingsExists = 0 AND 
									tov.id IN (SELECT req.id_tovar
												FROM j_trequest trq
														INNER JOIN j_request req ON trq.id = req.id_trequest
												WHERE (
														((@id_oper1 = -1 AND 											
																	trq.id_oper1 IN (SELECT value
																					FROM Requests.FilterSettings
																					WHERE id_value = 'mngr'
																					AND id_user = @id_user)
														  )
														  OR trq.id_oper1 = @id_oper1 
														 )																											
														 AND trq.drequest BETWEEN DATEADD(YEAR,-1,GETDATE()) AND GETDATE()
														 and trq.isActive = 1
													  )
											)
									 )
									 OR
									 (@mnSettingsExists = 1
										AND tov.id in (SELECT id_tovar
														FROM #mnGoods)
									 )
									)
				 )
			)
						
			DROP TABLE #mnGoods
	END
	else
	insert into #tovar(id,ean/*,id_otdel*/)
	select id,ean/*,id_otdel*/ from dbo.s_tovar tov
	where (@id_grp1 = 0 AND (@id_status != 0 OR tov.id_grp1 IN (SELECT id_grp1
																  FROM users_vs_grp1
																  WHERE id_users = @id_user
																 )
							   )
					 OR tov.id_grp1 = @id_grp1)
			AND
			(@id_grp2 = 0 OR tov.id_grp2 = @id_grp2)
			AND
			(@id_grp3 = 0 OR tov.id_grp3 = @id_grp3)
			AND
			((@id_dep = 0 AND (@id_status != 1 OR (tov.id_otdel IN (SELECT CAST(val AS INT)
																FROM dbo.property_list 
																WHERE id_prog = @id_prog
																AND id_user = @id_user
																AND LTRIM(RTRIM(id_val)) = 'idusr'
																AND ISNUMERIC(val) = 1 and val<>6
																)
												
												  ) 
							  )
			  )OR tov.id_otdel = @id_dep
			  ) 
			  AND
			 (@id_post = 0 OR (tov.id in (select id from #tov)))	
			 		 
	create unique nonclustered index ean on #tovar(ean)
	
	--SELECT * FROM #tovar
	
	SELECT @otsechBegin = CAST(val as DateTime)
	FROM dbo.property_list
	WHERE id_prog = @id_prog
			AND id_user = @id_user
			AND id_val = 'curPeriodBegin'
	SET @otsechBegin = ISNULL(@otsechBegin,CAST(DATEADD(dd,-7,GETDATE()) AS DATE))
				
	SELECT @otsechEnd = CAST(val as DateTime)
	FROM dbo.property_list
	WHERE id_prog = @id_prog
			AND id_user = @id_user
			AND id_val = 'curPeriodEnd'
			
	SET @otsechEnd = ISNULL(@otsechEnd,CAST(DATEADD(dd,-1,GETDATE()) AS DATE))		
			
	SELECT @isPreDate = ISNULL(CAST(val as bit),0)
	FROM dbo.property_list
	WHERE id_prog = @id_prog
			AND id_user = @id_user
			AND id_val = 'rest calculation'
	
	SET @isPreDate = ISNULL(@isPreDate, 0)
	
	--declare @DateNow date = CAST(GETDATE() AS DATE)	 
	--declare @DateNowFrom	datetime = CAST(CONVERT(varchar(10),GETDATE(),102)  + ' 00:00:00.000' AS DATETIME)
	--declare @DateNowTo		datetime = CAST(CONVERT(varchar(10),GETDATE(),102)  + ' 23:59:59.999' AS DATETIME)
	
	CREATE TABLE #unconfVozvr(id_tovar int, vozvrat numeric(11,3))
	
	INSERT INTO #unconfVozvr
	SELECT b.id_tovar, SUM(netto) as vozvrat
    FROM j_trequest a with (nolock) inner join
		 j_request b with (nolock) on a.id = b.id_trequest
	WHERE a.nstatus NOT IN (1) 
		  AND a.data_out >= CAST(GETDATE() as DATE)
		  AND a.id_operand = 2
		  and a.isActive = 1
          AND ISNULL(LTRIM(RTRIM(a.cinclude)),'') = ''
	GROUP BY b.id_tovar
/*	
	--Максимальные id из приходов
	CREATE TABLE #prihId(id_tovar int, id_prihod int)
	
	INSERT INTO #prihId
	SELECT pr.id_tovar, MAX(pr.id)
	FROM #tovar tov INNER JOIN j_prihod pr on pr.id_tovar = tov.id
					INNER JOIN j_allprihod ap ON ap.id = pr.id_allprihod
	WHERE ap.dprihod <= CAST(GETDATE() AS DATE)
		AND ap.SubTypeOperand = 1 --in (1,3)
	GROUP BY pr.id_tovar
	
	--Получение цены закупки
	CREATE TABLE #goodsZcena (id_tovar int, zcena numeric(11,4))
	
	INSERT INTO #goodsZcena
	SELECT DISTINCT pr.id_tovar, pr.zcena 
	FROM 
		j_allprihod apr INNER JOIN j_prihod pr ON apr.id = pr.id_allprihod
		INNER JOIN(
	SELECT DISTINCT p.id_tovar, MAX(dprihod) as dprihod --over(partition by id_tovar) 
	FROM #tovar tov INNER JOIN j_prihod p ON tov.id = p.id_tovar
					INNER JOIN j_allprihod ap ON ap.id = p.id_allprihod
	WHERE ap.dprihod <= CAST(GETDATE() AS DATE)
		  AND ap.SubTypeOperand = 1 --and id_post<>6619
	GROUP BY p.id_tovar) x ON apr.dprihod = x.dprihod AND pr.id_tovar = x.id_tovar AND apr.SubTypeOperand = 1 --in (1,3)
					INNER JOIN #prihId id on pr.id = id.id_prihod AND pr.id_tovar = id.id_tovar
*/

------------------------------------------------------------------------------------------------------
CREATE TABLE #prihId(id_tovar int, id_prihod int, isAlpha bit)
	
	--получение макс. дат
	CREATE TABLE #maxDates(id_tovar int, dprihod DateTime, isAlpha bit)
	INSERT INTO #maxDates
	SELECT x.id_tovar, max(x.dprihod), x.isAlpha FROM (
	SELECT DISTINCT p.id_tovar, dprihod, (CASE ap.SubTypeOperand WHEN 11 THEN 1 ELSE 0 END) AS isAlpha
	FROM  #tovar tov INNER JOIN j_prihod p with (nolock) ON tov.id = p.id_tovar
			INNER JOIN j_allprihod ap with (nolock) ON ap.id = p.id_allprihod
	WHERE ap.dprihod <= CAST(GETDATE() AS DATE) AND ap.SubTypeOperand IN (1, 8, 11, 14, 18, 17, 35))x
	GROUP BY x.id_tovar, x.isAlpha
	
	INSERT INTO #prihId
	SELECT x.id_tovar, max(x.id), x.isAlpha FROM (
	SELECT pr.id_tovar, pr.id, (CASE ap.SubTypeOperand WHEN 11 THEN 1 ELSE 0 END) AS isAlpha, ap.dprihod
	FROM  #tovar tov INNER JOIN j_prihod pr with (nolock) ON tov.id = pr.id_tovar
			INNER JOIN j_allprihod ap with (nolock) ON ap.id = pr.id_allprihod
	WHERE  ap.SubTypeOperand IN (1, 8, 11, 14, 18, 17, 35)) x
	WHERE x.dprihod IN (SELECT dprihod FROM #maxDates d WHERE d.id_tovar = x.id_tovar AND d.isAlpha = x.isAlpha)
	GROUP BY x.id_tovar, x.isAlpha
	
	--Объединение макс. id и дат
	CREATE TABLE #prihodVsDates(id_tovar int, id_prihod int, id_allprihod int, dprihod DateTime, isAlpha bit)
	INSERT INTO #prihodVsDates
	SELECT pri.id_tovar, pri.id_prihod, p.id_allprihod, md.dprihod, md.isAlpha 
	FROM #prihId pri 
	INNER JOIN #maxDates md ON pri.id_tovar = md.id_tovar AND pri.isAlpha = md.isAlpha
	LEFT JOIN j_prihod p ON pri.id_prihod = p.id
	
	--Последние приходы без учета альфа/не альфа
	CREATE TABLE #maxPrihod	(id_tovar int, id_prihod int, id_allprihod int, dprihod DateTime)
	INSERT INTO #maxPrihod
	SELECT pvd.id_tovar, pvd.id_prihod, pvd.id_allprihod, pvd.dprihod
	FROM 
		#prihodVsDates pvd INNER JOIN
		(
		SELECT id_tovar, Max(dprihod) AS dprihod FROM #prihodVsDates
		GROUP BY id_tovar) x 
	ON pvd.dprihod = x.dprihod AND x.id_tovar = pvd.id_tovar
	ORDER BY pvd.id_tovar ASC, pvd.isAlpha ASC

	--Определение цены закупки для товаров с TransferAmountWard = 1 и DistributedGoods = 1
	CREATE TABLE #prihodVSRules(id_tovar int, id_prihod int, id_allprihod int, dprihod DateTime, id_rule int)
	--Находим товары  с TransferAmountWard = 1 и DistributedGoods = 1
	INSERT INTO #prihodVSRules
	SELECT y.id_tovar, y.id_prihod, y.id_allprihod, y.dprihod, y.id_rule FROM
	(SELECT mp.id_tovar, mp.id_prihod, mp.id_allprihod, mp.dprihod, MIN(tg.id_rule) AS id_rule 
		FROM #maxPrihod mp 
				INNER JOIN disparity.s_TFGoods tg with (nolock) ON mp.id_tovar = tg.id_tovar
		GROUP BY mp.id_tovar, mp.id_prihod, mp.id_allprihod, mp.dprihod) y
	LEFT JOIN disparity.s_TFGoods tfg with (nolock) ON y.id_tovar = tfg.id_tovar AND y.id_rule = tfg.id_rule
	LEFT JOIN disparity.s_TFRules tfr with (nolock) ON tfg.id_rule = tfr.id
	WHERE tfg.DistributedGoods = 1 AND tfr.TransferAmountWard = 1
	
	--Находим товары по группе в последней накладной, находим по ним среднюю цену зак.
	CREATE TABLE #grpZcena(id_tovar int, zcena numeric(11,4))
	INSERT INTO #grpZcena
	SELECT pvr.id_tovar, AVG(y.zcena)--SUM(y.zcena)/COUNT(y.zcena)
	FROM
		#prihodVSRules pvr 
		LEFT JOIN
		j_prihod y with (nolock) ON pvr.id_tovar = y.id_tovar /*AND pvr.id_rule = y.id_rule*/ AND pvr.id_allprihod = y.id_allprihod
	GROUP BY pvr.id_tovar 
	--INSERT INTO #grpZcena
	--SELECT pvr.id_tovar, SUM(y.zcena)/COUNT(y.zcena)
	--FROM
	--	#prihodVSRules pvr 
		--LEFT JOIN
		--(
		--SELECT pvr.id_tovar, p.zcena, p.id_allprihod, MIN(tg.id_rule) AS id_rule
		--FROM j_prihod p
		--	INNER JOIN #prihodVSRules pvr ON pvr.id_allprihod = p.id_allprihod
		--	INNER JOIN disparity.s_TFGoods tg ON p.id_tovar = tg.id_tovar
		--GROUP BY  pvr.id_tovar, p.zcena, p.id_allprihod
		--) y ON pvr.id_tovar = y.id_tovar AND pvr.id_rule = y.id_rule AND pvr.id_allprihod = y.id_allprihod
	--GROUP BY pvr.id_tovar 
		
	--Получение цены закупки
	CREATE TABLE #goodsZcena (id_tovar int, zcena numeric(11,4), zcenaAlpha numeric(11,4))
	
	IF(@id_dep = (SELECT value FROM dbo.prog_config WHERE id_value = 'zdep' AND id_prog = @id_prog))
		INSERT INTO #goodsZcena
		SELECT pr.id_tovar, MAX(ISNULL(grpZ.zcena,pr.zcena)) AS zcena, MAX(ISNULL(grpZ.zcena,pr.zcena)) as zcenaAlpha 
		FROM 
		j_allprihod apr with (nolock) INNER JOIN j_prihod pr with (nolock) ON apr.id = pr.id_allprihod
		INNER JOIN #maxPrihod prih ON apr.dprihod = prih.dprihod 
											AND pr.id_tovar = prih.id_tovar 
											AND pr.id = prih.id_prihod 
		--Для товара с TransferAmountWard = 1 и DistributedGoods = 1 цена з - средняя цена по группе товара в последней накладной
		LEFT JOIN #grpZcena grpZ ON prih.id_tovar = grpZ.id_tovar
		GROUP BY pr.id_tovar
	ELSE
		INSERT INTO #goodsZcena
		SELECT y.id_tovar, MAX(ISNULL(gZc.zcena,y.zcena)) AS zcena, MAX(ISNULL(gZc.zcena,y.zcenaAlpha)) as zcenaAlpha FROM(
		SELECT DISTINCT pr.id_tovar, pr.zcena, 0 as zcenaAlpha
		FROM 
		j_allprihod apr with (nolock) INNER JOIN j_prihod pr with (nolock) ON apr.id = pr.id_allprihod
		INNER JOIN #prihodVsDates prih ON apr.dprihod = prih.dprihod 
											AND pr.id_tovar = prih.id_tovar 
											AND pr.id = prih.id_prihod 
											AND prih.isAlpha = 0
		--Цены закупки по альфе
		UNION
		SELECT DISTINCT pr.id_tovar, 0 as zcena, pr.zcena as zcenaAlpha
		FROM 
		j_allprihod apr INNER JOIN j_prihod pr ON apr.id = pr.id_allprihod							
		INNER JOIN #prihodVsDates prihA ON apr.dprihod = prihA.dprihod 
											AND pr.id_tovar = prihA.id_tovar 
											AND pr.id = prihA.id_prihod 
											AND prihA.isAlpha = 1
											)y
		--Для товара с TransferAmountWard = 1 и DistributedGoods = 1 цена з - средняя цена по группе товара в последней
		LEFT JOIN #grpZcena gZc on y.id_tovar = gZc.id_tovar
		GROUP BY y.id_tovar
	--DROP TABLE #prihId, #goodsZcena, #maxDates, #prihodVsDates
------------------------------------------------------------------------------------------------------	
    SET @dateInv = [Requests].[GetDateInv](@isPreDate)

--------
    SELECT *
			, ostOnline - vozvrat AS OOVozvr
			, (CASE WHEN isCatGood = 1 AND 
								 ost = 0 AND
								 prihod = 0 AND
								 rash = 0 AND
								 ostOnDate  = 0 AND
								 prihodOnline  = 0 AND
								 ostOnline = 0 AND
								 --avgReal = 0 AND
								 vozvrat  = 0 AND
								 zakaz  = 0 AND
								 zakazUnconf  = 0 AND
							     zapas = 0
							     THEN 1
							     ELSE 0
							     END						     
							    ) AS CatFullZero
    FROM
    (
	SELECT CAST(0 as bit) as choose,
		   ean,
		   (select FullName from [dbo].[funcFullNameTovar](id_tovar)) as cName, 
		   --LTRIM(RTRIM(cName)) as cName,
		   nachOst AS ost,
		   --+ postup + vkass - (otgruz + vozvr + spis + realiz)) ,
		   (postup-vkass -otgruz - vozvr - spis) as prihod,
		   (realiz /*+ vkass*/) as rash,
		   ( nachOst + postup/*-vkass*/ -otgruz - vozvr - spis - realiz - vkass) as ostOnDate,--ost + prihod - rash
		   prihodOnline,
		   ( ( nachOst + postup/*-vkass*/ -otgruz - vozvr - spis - realiz - vkass) +  prihodOnline
		   +postupDay - otgruzDay - vozvrDay - spisDay - vkassDay ) as ostOnline,
		   ISNULL(avgReal,0) as avgReal,
		   rcena,
		   morningNacen,
		   --0 as morningNacen,
		   vozvrat,
		   --CAST(0 as numeric(13,3)) as zakaz,
		   zakaz,
		   zakazUnconf,	
		   CAST((CASE WHEN ISNULL(avgReal,0) = 0 THEN (nachOst + postup + vkass - (otgruz + vozvr + spis + realiz))
				 ELSE (nachOst + postup + vkass - (otgruz + vozvr + spis + realiz))/avgReal
			END	
		   ) AS INT) as zapas,
		   ShelfSpace,
		   isCatGood,
		   isProh,
		   id_tovar,
		   avgRealWeek,
		   uVozvr
		   ,zcena
		   ,CASE WHEN rcena=0 THEN 0 ELSE CAST((rcena - zcena)*100/rcena AS NUMERIC(11,2)) END AS rn
		   ,id_grp1
		   ,id_grp2		   
		   ,idMaxrixGood

    FROM
    (
    SELECT tov.id as id_tovar,
		   tov.ean,
		   isnull(nac.morningNacen,0) as morningNacen,
		   ISNULL(tt.cName,'') + ' ' + ISNULL(vtov.cname,'') as cName,
		   --поступления
		    (SELECT ISNULL(SUM(netto),0)
				FROM j_allprihod a with (nolock)
						INNER JOIN j_prihod p with (nolock) on a.id = p.id_allprihod
				WHERE p.id_tovar = tov.id
						AND a.dprihod < cast(GETDATE() as date)
						AND a.dprihod > @dateInv  AND a.InventSpis = 0
			)  as postup, 
			-- возврат с касс
			(SELECT ISNULL(SUM(vk.netto),0)
				FROM j_allprihod a with (nolock)
						INNER JOIN j_vozvkass vk with (nolock) ON a.id = vk.id_allprihod
				WHERE vk.id_tovar = tov.id
						AND a.dprihod < cast(GETDATE() as date)
						AND a.dprihod > @dateInv AND a.InventSpis = 0
			) as vkass,
			-- отгрузка
			(SELECT ISNULL(SUM(o.netto),0)
				FROM j_allprihod a with (nolock)
						INNER JOIN j_otgruz o with (nolock) ON a.id = o.id_allprihod
				WHERE o.id_tovar = tov.id
						AND a.dprihod < cast(GETDATE() as date)
						AND a.dprihod > @dateInv AND a.InventSpis = 0
			) as otgruz,
			-- возврат поставщику
			(SELECT ISNULL(SUM(v.netto),0)
				FROM j_allprihod a with (nolock)
						LEFT JOIN j_vozvr v with (nolock) ON a.id = v.id_allprihod
				WHERE v.id_tovar = tov.id
						AND a.dprihod < cast(GETDATE() as date)
						AND a.dprihod > @dateInv AND a.InventSpis = 0
			) as vozvr,
			-- списание
			(SELECT ISNULL(SUM(s.netto),0)
				FROM j_allprihod a with (nolock)
						LEFT JOIN j_spis s with (nolock) ON a.id = s.id_allprihod AND a.InventSpis = 0
				WHERE s.id_tovar = tov.id
						AND a.dprihod < cast(GETDATE() as date)
						AND a.dprihod > @dateInv				
			) AS  spis,
			--поступления на день
		    (SELECT ISNULL(SUM(netto),0)
				FROM j_allprihod a with (nolock)
						INNER JOIN j_prihod p with (nolock) on a.id = p.id_allprihod
				WHERE p.id_tovar = tov.id
						AND a.dprihod =cast(GETDATE() as date) AND a.InventSpis = 0
			)  as postupDay, 
			-- возврат с касс
			(SELECT ISNULL(SUM(vk.netto),0)
				FROM j_allprihod a with (nolock)
						INNER JOIN j_vozvkass vk with (nolock) ON a.id = vk.id_allprihod
				WHERE vk.id_tovar = tov.id
						AND a.dprihod =cast(GETDATE() as date) AND a.InventSpis = 0
			) as vkassDay,
			-- отгрузка
			(SELECT ISNULL(SUM(o.netto),0)
				FROM j_allprihod a with (nolock)
						INNER JOIN j_otgruz o with (nolock) ON a.id = o.id_allprihod
				WHERE o.id_tovar = tov.id
						AND a.dprihod =cast(GETDATE() as date) AND a.InventSpis = 0
			) as otgruzDay,
			-- возврат поставщику
			(SELECT ISNULL(SUM(v.netto),0)
				FROM j_allprihod a with (nolock)
						LEFT JOIN j_vozvr v with (nolock) ON a.id = v.id_allprihod
				WHERE v.id_tovar = tov.id
						AND a.dprihod =cast(GETDATE() as date) AND a.InventSpis = 0
			) as vozvrDay,
			-- списание
			(SELECT ISNULL(SUM(s.netto),0)
				FROM j_allprihod a with (nolock)
						LEFT JOIN j_spis s with (nolock) ON a.id = s.id_allprihod AND a.InventSpis = 0
				WHERE s.id_tovar = tov.id
						AND a.dprihod =cast(GETDATE() as date)
			) AS  spisDay,
			--Начальный остаток
			(
			SELECT ISNULL(SUM(jo.netto),0)
				FROM j_ost jo with (nolock)
						LEFT JOIN j_tost jt with (nolock) on jo.id_tost = jt.id
						LEFT JOIN j_ttost jtt with (nolock) on jt.id_ttost = jtt.id
				WHERE jt.dtost = @dateInv 
						AND jo.id_tovar = tov.id
			) as nachOst,		
			-- Реализация	
		    (SELECT ISNULL(SUM(netto),0)
				FROM j_realiz with (nolock)
				WHERE id_tovar = tov.id
						AND drealiz < cast(GETDATE() as date)
						AND drealiz > @dateInv
			) as realiz,
			-- приход онлайн
		   (SELECT ISNULL(SUM(nt.Netto),0)
				FROM j_NaklOform nak with (nolock) LEFT JOIN j_NaklTovars nt with (nolock) on nak.id = nt.id_Nakl
				WHERE nak.DateNakl > @dateInv
						AND nak.id_Status IN (0, 1)
						AND nak.id_Operand IN (1,3)
						AND isActiv=1 AND isActive=1
						AND LTRIM(RTRIM(nt.EAN)) = LTRIM(RTRIM(tov.ean)) COLLATE Cyrillic_General_CS_AS
		   ) AS prihodOnline,
		   -- ср. расход
		   (SELECT (CASE WHEN DATEDIFF(dd,@otsechBegin,@otsechEnd) = 0
							THEN ISNULL(SUM(netto),0)
						 ELSE
							ISNULL(SUM(netto),0) / (DATEDIFF(dd,@otsechBegin,@otsechEnd)+1)
						END)
				FROM j_realiz with (nolock)
				WHERE id_tovar = tov.id
						AND drealiz BETWEEN @otsechBegin AND @otsechEnd
		   ) as avgReal,
		   --ср. расход за неделю
		   ( SELECT isnull(sum(netto)/7.0,0) 
				FROM j_realiz with (nolock)
				WHERE id_tovar = tov.id
						AND drealiz BETWEEN DATEADD(dd,-7,CAST(GETDATE() AS DATE))
						 AND DATEADD(dd,-1,CAST(GETDATE() AS DATE))) as avgRealWeek,
		   -- цена продажи
		   (SELECT TOP 1 rcena
				FROM s_rcena with (nolock)
				WHERE id_tovar = tov.id
						AND tdate_n <= GETDATE() 
				ORDER BY tdate_n DESC				
		   ) as rcena,
		   (SELECT ISNULL(SUM(req.netto),0)
				FROM j_trequest trq with (nolock) LEFT JOIN j_request req with (nolock) ON trq.id = req.id_trequest
				WHERE nstatus = 1
						AND trq.data_out >= cast(GETDATE() as date)
						AND trq.id_operand = 2
						and trq.isActive = 1
						--AND trq.data_factout IS NULL
						--AND trq.id_priem IS NULL
						AND trq.sum_ttn = 0
						AND LTRIM(RTRIM(trq.ttn)) = ''
						AND req.id_tovar = tov.id
		   ) as vozvrat,
		   		   (SELECT ISNULL(SUM(req.netto),0)
				FROM j_trequest trq with (nolock) LEFT JOIN j_request req with (nolock) ON trq.id = req.id_trequest
				WHERE trq.id_operand in (1)
						AND nstatus IN (1)
						AND trq.data_out >= cast(GETDATE() as date)
						AND req.id_tovar = tov.id
						and trq.isActive = 1
						--AND trq.data_factout IS NULL
						--AND trq.id_priem IS NULL
						AND trq.sum_ttn = 0
						AND LTRIM(RTRIM(trq.ttn)) = ''
						AND ISNULL(LTRIM(RTRIM(trq.cinclude)),'') = '' -- Добавлено 2015-03-13
		   ) as zakaz, --- добавила СС
		   (SELECT ISNULL(SUM(req.netto),0)
				FROM j_trequest trq with (nolock) LEFT JOIN j_request req with (nolock) ON trq.id = req.id_trequest
				WHERE trq.id_operand = 1
						AND nstatus NOT IN (0,1,3,6)
						and trq.isActive = 1
						AND trq.data_out >= cast(GETDATE() as date)
						AND req.id_tovar = tov.id
		   ) as zakazUnconf,
		   ISNULL(ss.ShelfSpace,0) as ShelfSpace,
		   CAST((CASE WHEN ct.id IS NULL THEN 0
				 ELSE 1 
			END
		   ) AS bit) AS isCatGood,
		   CAST((CASE WHEN pg.id IS NULL THEN 0
				 ELSE 1 
			END
		   ) AS bit) AS isProh,
		   ISNULL(ucv.vozvrat, 0) AS uVozvr
		   ,ISNULL(CASE gz.zcena WHEN 0 THEN gz.zcenaAlpha ELSE gz.zcena END ,0) AS zcena
		   ,id_grp1
		   ,id_grp2
		   ,tx.id  AS idMaxrixGood
    FROM #tovar tov
			LEFT JOIN v_tovar vtov with (nolock) on tov.id = vtov.id_tovar
			LEFT JOIN s_TypeTovar tt with (nolock) on vtov.ntypetovar = tt.id
			LEFT JOIN requests.s_ShelfSpace ss with (nolock) ON tov.id = ss.id_tovar
			LEFT JOIN requests.s_CatalogTovar ct with (nolock) ON tov.id = ct.id_tovar
			LEFT JOIN requests.s_ProhibitedGoods pg with (nolock) ON pg.id_tovar = tov.id 
															--AND pg.isRequests = 1
			LEFT JOIN Requests.j_GetMorningNacen as nac with (nolock)
			on(tov.id=nac.id_tovar)
			LEFT JOIN #unconfVozvr ucv ON tov.id = ucv.id_tovar
			LEFT JOIN #goodsZcena gz ON tov.id = gz.id_tovar
			LEFT JOIN requests.s_TovarMatrix tx on tx.id_tovar = tov.id

    ) x
    ) y 

    WHERE (@showZeroRows = 1 OR ( ost != 0 OR
								 prihod != 0 OR
								 rash != 0 OR
								 ostOnDate  != 0 OR
								 prihodOnline  != 0 OR
								 ostOnline != 0 OR
								 --avgReal != 0 OR
								 vozvrat  != 0 OR
								 zakaz  != 0 OR
								 zakazUnconf  != 0 OR
							     zapas != 0 OR
							     isCatGood = 1
							    )
		   )
			AND LEN(cName) > 0 
	ORDER BY ean ASC
	option(recompile)
		drop table #tov,#tovar,#unconfVozvr,#goodsZcena, #prihId, 
				#maxDates, #prihodVsDates, #prihodVSRules, #grpZcena, #maxPrihod
	
END
