IF( OBJECT_ID('SP_AGD_AtualizarContato_V1') IS NOT NULL)
BEGIN
	DROP PROCEDURE SP_AGD_AtualizarContato_V1;
END
GO

CREATE PROCEDURE SP_AGD_AtualizarContato_V1(
    @pNome VARCHAR(200),
    @pTelefone VARCHAR(14),
    @pEmail VARCHAR(250),
	@pIdContato BIGINT
)
AS
BEGIN	
	UPDATE AGENDA_CONTATO
	SET NOME = @pNome,
	TELEFONE = @pTelefone,
	EMAIL = @pEmail
	WHERE ID = @pIdContato
END;
