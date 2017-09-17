DECLARE @emailUserAdmin VARCHAR(50);
DECLARE @nomeUserAdmin VARCHAR(50);
DECLARE @passwordUserAdmin VARCHAR(5);
DECLARE @idUserAdmin BIGINT;

SET @emailUserAdmin = 'rodrigothomaz@msn.com';
SET @passwordUserAdmin = '12345';
SET @nomeUserAdmin = 'Rodrigo';

--Para as queries CI = Case incencitive e AI = Accent incencitive
--ALTER DATABASE RThomazDbDev
--COLLATE SQL_Latin1_General_CI_AI;

-- +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
-- Removendo todos os dados ------------------------------------------------------------------------------------
-- +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
DELETE FROM LancamentoRateio;
DELETE FROM ProgramacaoRateio;
DELETE FROM Lancamento;
DELETE FROM LancamentoProgramado;
DELETE FROM TransferenciaProgramada;
DELETE FROM LancamentoParcelado;
DELETE FROM Programacao;
DELETE FROM Transferencia;
DELETE FROM PlanoConta;
DELETE FROM CentroCusto;
DELETE FROM Estado;
DELETE FROM Pais;
DELETE FROM Conta;
DELETE FROM Pessoa;
DELETE FROM Perfil;
DELETE FROM Banco;
DELETE FROM Arquivo;
DELETE FROM Usuario;
-- +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
-- Carregando tabela Usuario -----------------------------------------------------------------------------------
-- +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

INSERT INTO Usuario (
	AplicacaoId,
	Email,
	Senha,
	Ativo
) VALUES (
	1,
	@emailUserAdmin,
	@passwordUserAdmin,
	1
);

SET @idUserAdmin = IDENT_CURRENT ('Usuario');

INSERT INTO Perfil (
	AplicacaoId,
	UsuarioId,
	NomeExibicao
) VALUES (
	1,
	@idUserAdmin,
	@nomeUserAdmin
);

SELECT @idUserAdmin;