﻿IF( OBJECT_ID('SP_AGD_ConsultarContatoPorID_V1') IS NOT NULL)
BEGIN
	DROP PROCEDURE SP_AGD_ConsultarContatoPorID_V1;
END
GO

CREATE PROCEDURE SP_AGD_ConsultarContatoPorID_V1(
	@pIdContato BIGINT
)
AS
BEGIN	
	SELECT 
		ID,
		NOME,
		TELEFONE,
		EMAIL,
		ATIVO
	FROM AGENDA_CONTATO
	WHERE ATIVO = 'S'
	AND ID = @pIdContato
END;