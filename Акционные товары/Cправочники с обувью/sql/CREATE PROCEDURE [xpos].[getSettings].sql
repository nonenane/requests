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
-- Description:	Получение настроек
-- =============================================
CREATE PROCEDURE [xpos].[getSettings]	
	@id_prog int,
	@id_value varchar(4)
AS
BEGIN
	SET NOCOUNT ON;

	select value from dbo.prog_config where id_prog = @id_prog and id_value= @id_value
	
END
