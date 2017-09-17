namespace RThomaz.Infra.Data.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Aplicacao",
                c => new
                    {
                        AplicacaoId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.AplicacaoId);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        UsuarioId = c.Long(nullable: false, identity: true),
                        AplicacaoId = c.Guid(nullable: false),
                        ApplicationUserId = c.Guid(nullable: false),
                        Email = c.String(nullable: false, maxLength: 250),
                        Senha = c.String(nullable: false, maxLength: 50),
                        Ativo = c.Boolean(nullable: false),
                        Descricao = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.UsuarioId)
                .ForeignKey("dbo.Aplicacao", t => t.AplicacaoId)
                .Index(t => t.AplicacaoId);
            
            CreateTable(
                "dbo.Perfil",
                c => new
                    {
                        UsuarioId = c.Long(nullable: false),
                        AplicacaoId = c.Guid(nullable: false),
                        NomeExibicao = c.String(nullable: false, maxLength: 250),
                        AvatarStorageObject = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.UsuarioId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.CentroCusto",
                c => new
                    {
                        CentroCustoId = c.Long(nullable: false, identity: true),
                        AplicacaoId = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 250),
                        Ativo = c.Boolean(nullable: false),
                        Descricao = c.String(maxLength: 4000),
                        ResponsavelId = c.Long(),
                        ParentId = c.Long(),
                    })
                .PrimaryKey(t => t.CentroCustoId)
                .ForeignKey("dbo.Aplicacao", t => t.AplicacaoId)
                .ForeignKey("dbo.CentroCusto", t => t.ParentId)
                .ForeignKey("dbo.Usuario", t => t.ResponsavelId)
                .Index(t => t.AplicacaoId)
                .Index(t => t.ResponsavelId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.LancamentoRateio",
                c => new
                    {
                        LancamentoId = c.Guid(nullable: false),
                        TipoTransacao = c.Byte(nullable: false),
                        PlanoContaId = c.Long(nullable: false),
                        CentroCustoId = c.Long(nullable: false),
                        AplicacaoId = c.Guid(nullable: false),
                        Observacao = c.String(maxLength: 4000),
                        Porcentagem = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.LancamentoId, t.TipoTransacao, t.PlanoContaId, t.CentroCustoId })
                .ForeignKey("dbo.CentroCusto", t => t.CentroCustoId)
                .ForeignKey("dbo.Lancamento", t => new { t.LancamentoId, t.TipoTransacao })
                .ForeignKey("dbo.PlanoConta", t => new { t.PlanoContaId, t.TipoTransacao })
                .Index(t => new { t.LancamentoId, t.TipoTransacao })
                .Index(t => new { t.PlanoContaId, t.TipoTransacao })
                .Index(t => t.CentroCustoId);
            
            CreateTable(
                "dbo.Lancamento",
                c => new
                    {
                        LancamentoId = c.Guid(nullable: false, identity: true),
                        TipoTransacao = c.Byte(nullable: false),
                        AplicacaoId = c.Guid(nullable: false),
                        Historico = c.String(nullable: false, maxLength: 500),
                        Numero = c.String(maxLength: 50),
                        DataVencimento = c.DateTime(nullable: false),
                        ValorVencimento = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Observacao = c.String(maxLength: 4000),
                        PessoaId = c.Long(),
                        TipoPessoa = c.Byte(),
                        ContaId = c.Long(nullable: false),
                        TipoConta = c.Byte(nullable: false),
                        TransferenciaId = c.Long(),
                        TransferenciaProgramadaId = c.Long(),
                        ProgramacaoId = c.Long(),
                        TipoProgramacao = c.Byte(),
                    })
                .PrimaryKey(t => new { t.LancamentoId, t.TipoTransacao })
                .ForeignKey("dbo.Aplicacao", t => t.AplicacaoId)
                .ForeignKey("dbo.Conta", t => new { t.ContaId, t.TipoConta })
                .ForeignKey("dbo.Pessoa", t => new { t.PessoaId, t.TipoPessoa })
                .ForeignKey("dbo.Programacao", t => new { t.ProgramacaoId, t.TipoProgramacao, t.TipoTransacao })
                .ForeignKey("dbo.Transferencia", t => t.TransferenciaId)
                .ForeignKey("dbo.TransferenciaProgramada", t => t.TransferenciaProgramadaId)
                .Index(t => new { t.ProgramacaoId, t.TipoProgramacao, t.TipoTransacao })
                .Index(t => t.AplicacaoId)
                .Index(t => new { t.PessoaId, t.TipoPessoa })
                .Index(t => new { t.ContaId, t.TipoConta })
                .Index(t => t.TransferenciaId)
                .Index(t => t.TransferenciaProgramadaId);
            
            CreateTable(
                "dbo.Conta",
                c => new
                    {
                        ContaId = c.Long(nullable: false, identity: true),
                        TipoConta = c.Byte(nullable: false),
                        AplicacaoId = c.Guid(nullable: false),
                        Descricao = c.String(maxLength: 4000),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.ContaId, t.TipoConta })
                .ForeignKey("dbo.Aplicacao", t => t.AplicacaoId)
                .Index(t => t.AplicacaoId);
            
            CreateTable(
                "dbo.Movimento",
                c => new
                    {
                        MovimentoId = c.Long(nullable: false, identity: true),
                        TipoTransacao = c.Byte(nullable: false),
                        AplicacaoId = c.Guid(nullable: false),
                        ContaId = c.Long(nullable: false),
                        TipoConta = c.Byte(nullable: false),
                        DataMovimento = c.DateTime(nullable: false),
                        ValorMovimento = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Historico = c.String(nullable: false, maxLength: 500),
                        MovimentoImportacaoId = c.Long(),
                        Observacao = c.String(maxLength: 4000),
                        EstaConciliado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.MovimentoId, t.TipoTransacao })
                .ForeignKey("dbo.Aplicacao", t => t.AplicacaoId)
                .ForeignKey("dbo.Conta", t => new { t.ContaId, t.TipoConta })
                .ForeignKey("dbo.MovimentoImportacao", t => t.MovimentoImportacaoId)
                .Index(t => t.AplicacaoId)
                .Index(t => new { t.ContaId, t.TipoConta })
                .Index(t => t.MovimentoImportacaoId);
            
            CreateTable(
                "dbo.Conciliacao",
                c => new
                    {
                        MovimentoId = c.Long(nullable: false),
                        LancamentoId = c.Guid(nullable: false),
                        TipoTransacao = c.Byte(nullable: false),
                        AplicacaoId = c.Guid(nullable: false),
                        ValorConciliado = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.MovimentoId, t.LancamentoId, t.TipoTransacao })
                .ForeignKey("dbo.Movimento", t => new { t.MovimentoId, t.TipoTransacao })
                .ForeignKey("dbo.Pagamento", t => new { t.LancamentoId, t.TipoTransacao })
                .Index(t => new { t.MovimentoId, t.TipoTransacao })
                .Index(t => new { t.LancamentoId, t.TipoTransacao });
            
            CreateTable(
                "dbo.Pagamento",
                c => new
                    {
                        LancamentoId = c.Guid(nullable: false),
                        TipoTransacao = c.Byte(nullable: false),
                        AplicacaoId = c.Guid(nullable: false),
                        DataPagamento = c.DateTime(nullable: false),
                        ValorPagamento = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.LancamentoId, t.TipoTransacao })
                .ForeignKey("dbo.Lancamento", t => new { t.LancamentoId, t.TipoTransacao })
                .Index(t => new { t.LancamentoId, t.TipoTransacao });
            
            CreateTable(
                "dbo.MovimentoImportacao",
                c => new
                    {
                        MovimentoImportacaoId = c.Long(nullable: false, identity: true),
                        AplicacaoId = c.Guid(nullable: false),
                        ImportadoEm = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MovimentoImportacaoId)
                .ForeignKey("dbo.Aplicacao", t => t.AplicacaoId)
                .Index(t => t.AplicacaoId);
            
            CreateTable(
                "dbo.Programacao",
                c => new
                    {
                        ProgramacaoId = c.Long(nullable: false, identity: true),
                        TipoProgramacao = c.Byte(nullable: false),
                        TipoTransacao = c.Byte(nullable: false),
                        AplicacaoId = c.Guid(nullable: false),
                        PessoaId = c.Long(),
                        TipoPessoa = c.Byte(),
                        ContaId = c.Long(nullable: false),
                        TipoConta = c.Byte(nullable: false),
                        DataInicial = c.DateTime(nullable: false),
                        DataFinal = c.DateTime(nullable: false),
                        Frequencia = c.Byte(nullable: false),
                        Dia = c.Byte(),
                        HasDomingo = c.Boolean(),
                        HasSegunda = c.Boolean(),
                        HasTerca = c.Boolean(),
                        HasQuarta = c.Boolean(),
                        HasQuinta = c.Boolean(),
                        HasSexta = c.Boolean(),
                        HasSabado = c.Boolean(),
                        Historico = c.String(nullable: false, maxLength: 500),
                        ValorVencimento = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Observacao = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => new { t.ProgramacaoId, t.TipoProgramacao, t.TipoTransacao })
                .ForeignKey("dbo.Aplicacao", t => t.AplicacaoId)
                .ForeignKey("dbo.Conta", t => new { t.ContaId, t.TipoConta })
                .ForeignKey("dbo.Pessoa", t => new { t.PessoaId, t.TipoPessoa })
                .Index(t => t.AplicacaoId)
                .Index(t => new { t.PessoaId, t.TipoPessoa })
                .Index(t => new { t.ContaId, t.TipoConta });
            
            CreateTable(
                "dbo.Pessoa",
                c => new
                    {
                        PessoaId = c.Long(nullable: false, identity: true),
                        TipoPessoa = c.Byte(nullable: false),
                        AplicacaoId = c.Guid(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        Descricao = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => new { t.PessoaId, t.TipoPessoa })
                .ForeignKey("dbo.Aplicacao", t => t.AplicacaoId)
                .Index(t => t.AplicacaoId);
            
            CreateTable(
                "dbo.PessoaEmail",
                c => new
                    {
                        PessoaEmailId = c.Long(nullable: false, identity: true),
                        PessoaId = c.Long(nullable: false),
                        TipoPessoa = c.Byte(nullable: false),
                        AplicacaoId = c.Guid(nullable: false),
                        TipoEmailId = c.Guid(nullable: false),
                        Email = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => new { t.PessoaEmailId, t.PessoaId, t.TipoPessoa })
                .ForeignKey("dbo.Pessoa", t => new { t.PessoaId, t.TipoPessoa })
                .ForeignKey("dbo.TipoEmail", t => new { t.TipoEmailId, t.TipoPessoa })
                .Index(t => new { t.PessoaId, t.TipoPessoa })
                .Index(t => new { t.TipoEmailId, t.TipoPessoa });
            
            CreateTable(
                "dbo.TipoEmail",
                c => new
                    {
                        TipoEmailId = c.Guid(nullable: false, identity: true),
                        TipoPessoa = c.Byte(nullable: false),
                        AplicacaoId = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 255),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.TipoEmailId, t.TipoPessoa })
                .ForeignKey("dbo.Aplicacao", t => t.AplicacaoId)
                .Index(t => new { t.AplicacaoId, t.TipoPessoa, t.Nome }, unique: true, name: "IX_Unique_Nome");
            
            CreateTable(
                "dbo.PessoaEndereco",
                c => new
                    {
                        PessoaEnderecoId = c.Long(nullable: false, identity: true),
                        PessoaId = c.Long(nullable: false),
                        TipoPessoa = c.Byte(nullable: false),
                        AplicacaoId = c.Guid(nullable: false),
                        TipoEnderecoId = c.Guid(nullable: false),
                        Cep = c.String(maxLength: 20),
                        Logradouro = c.String(maxLength: 255),
                        Numero = c.String(maxLength: 10),
                        Complemento = c.String(maxLength: 255),
                        BairroId = c.Long(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.PessoaEnderecoId, t.PessoaId, t.TipoPessoa })
                .ForeignKey("dbo.Bairro", t => t.BairroId)
                .ForeignKey("dbo.Pessoa", t => new { t.PessoaId, t.TipoPessoa })
                .ForeignKey("dbo.TipoEndereco", t => new { t.TipoEnderecoId, t.TipoPessoa })
                .Index(t => new { t.PessoaId, t.TipoPessoa })
                .Index(t => new { t.TipoEnderecoId, t.TipoPessoa })
                .Index(t => t.BairroId);
            
            CreateTable(
                "dbo.Bairro",
                c => new
                    {
                        BairroId = c.Long(nullable: false, identity: true),
                        CidadeId = c.Long(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 255),
                        NomeAbreviado = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.BairroId)
                .ForeignKey("dbo.Cidade", t => t.CidadeId)
                .Index(t => t.CidadeId);
            
            CreateTable(
                "dbo.Cidade",
                c => new
                    {
                        CidadeId = c.Long(nullable: false, identity: true),
                        EstadoId = c.Long(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 255),
                        NomeAbreviado = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.CidadeId)
                .ForeignKey("dbo.Estado", t => t.EstadoId)
                .Index(t => t.EstadoId);
            
            CreateTable(
                "dbo.Estado",
                c => new
                    {
                        EstadoId = c.Long(nullable: false, identity: true),
                        PaisId = c.Long(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 255),
                        Sigla = c.String(nullable: false, maxLength: 255),
                        TipoOrigemDado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EstadoId)
                .ForeignKey("dbo.Pais", t => t.PaisId)
                .Index(t => t.PaisId);
            
            CreateTable(
                "dbo.Pais",
                c => new
                    {
                        PaisId = c.Long(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100),
                        ISO2 = c.String(nullable: false, maxLength: 2, fixedLength: true),
                        ISO3 = c.String(maxLength: 3, fixedLength: true),
                        NumCode = c.String(maxLength: 4),
                        DDI = c.String(maxLength: 10),
                        ccTLD = c.String(maxLength: 5),
                        BandeiraStorageObject = c.String(maxLength: 255),
                        TipoOrigemDado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PaisId);
            
            CreateTable(
                "dbo.TipoEndereco",
                c => new
                    {
                        TipoEnderecoId = c.Guid(nullable: false, identity: true),
                        TipoPessoa = c.Byte(nullable: false),
                        AplicacaoId = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 255),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.TipoEnderecoId, t.TipoPessoa })
                .ForeignKey("dbo.Aplicacao", t => t.AplicacaoId)
                .Index(t => new { t.AplicacaoId, t.TipoPessoa, t.Nome }, unique: true, name: "IX_Unique_Nome");
            
            CreateTable(
                "dbo.PessoaHomePage",
                c => new
                    {
                        PessoaHomePageId = c.Long(nullable: false, identity: true),
                        PessoaId = c.Long(nullable: false),
                        TipoPessoa = c.Byte(nullable: false),
                        AplicacaoId = c.Guid(nullable: false),
                        TipoHomePageId = c.Guid(nullable: false),
                        HomePage = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => new { t.PessoaHomePageId, t.PessoaId, t.TipoPessoa })
                .ForeignKey("dbo.Pessoa", t => new { t.PessoaId, t.TipoPessoa })
                .ForeignKey("dbo.TipoHomePage", t => new { t.TipoHomePageId, t.TipoPessoa })
                .Index(t => new { t.PessoaId, t.TipoPessoa })
                .Index(t => new { t.TipoHomePageId, t.TipoPessoa });
            
            CreateTable(
                "dbo.TipoHomePage",
                c => new
                    {
                        TipoHomePageId = c.Guid(nullable: false, identity: true),
                        TipoPessoa = c.Byte(nullable: false),
                        AplicacaoId = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 255),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.TipoHomePageId, t.TipoPessoa })
                .ForeignKey("dbo.Aplicacao", t => t.AplicacaoId)
                .Index(t => new { t.AplicacaoId, t.TipoPessoa, t.Nome }, unique: true, name: "IX_Unique_Nome");
            
            CreateTable(
                "dbo.PessoaTelefone",
                c => new
                    {
                        PessoaTelefoneId = c.Long(nullable: false, identity: true),
                        PessoaId = c.Long(nullable: false),
                        TipoPessoa = c.Byte(nullable: false),
                        AplicacaoId = c.Guid(nullable: false),
                        TipoTelefoneId = c.Guid(nullable: false),
                        Telefone = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => new { t.PessoaTelefoneId, t.PessoaId, t.TipoPessoa })
                .ForeignKey("dbo.Pessoa", t => new { t.PessoaId, t.TipoPessoa })
                .ForeignKey("dbo.TipoTelefone", t => new { t.TipoTelefoneId, t.TipoPessoa })
                .Index(t => new { t.PessoaId, t.TipoPessoa })
                .Index(t => new { t.TipoTelefoneId, t.TipoPessoa });
            
            CreateTable(
                "dbo.TipoTelefone",
                c => new
                    {
                        TipoTelefoneId = c.Guid(nullable: false, identity: true),
                        TipoPessoa = c.Byte(nullable: false),
                        AplicacaoId = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 255),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.TipoTelefoneId, t.TipoPessoa })
                .ForeignKey("dbo.Aplicacao", t => t.AplicacaoId)
                .Index(t => new { t.AplicacaoId, t.TipoPessoa, t.Nome }, unique: true, name: "IX_Unique_Nome");
            
            CreateTable(
                "dbo.CBOOcupacao",
                c => new
                    {
                        CBOOcupacaoId = c.Int(nullable: false),
                        CBOFamiliaId = c.Short(nullable: false),
                        Codigo = c.String(nullable: false, maxLength: 6, fixedLength: true, unicode: false),
                        Titulo = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.CBOOcupacaoId)
                .ForeignKey("dbo.CBOFamilia", t => t.CBOFamiliaId)
                .Index(t => t.CBOFamiliaId);
            
            CreateTable(
                "dbo.CBOFamilia",
                c => new
                    {
                        CBOFamiliaId = c.Short(nullable: false),
                        CBOSubGrupoId = c.Short(nullable: false),
                        Codigo = c.String(nullable: false, maxLength: 4, fixedLength: true, unicode: false),
                        Titulo = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.CBOFamiliaId)
                .ForeignKey("dbo.CBOSubGrupo", t => t.CBOSubGrupoId)
                .Index(t => t.CBOSubGrupoId);
            
            CreateTable(
                "dbo.CBOSubGrupo",
                c => new
                    {
                        CBOSubGrupoId = c.Short(nullable: false),
                        CBOSubGrupoPrincipalId = c.Short(nullable: false),
                        Codigo = c.String(nullable: false, maxLength: 3, fixedLength: true, unicode: false),
                        Titulo = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.CBOSubGrupoId)
                .ForeignKey("dbo.CBOSubGrupoPrincipal", t => t.CBOSubGrupoPrincipalId)
                .Index(t => t.CBOSubGrupoPrincipalId);
            
            CreateTable(
                "dbo.CBOSubGrupoPrincipal",
                c => new
                    {
                        CBOSubGrupoPrincipalId = c.Short(nullable: false),
                        CBOGrandeGrupoId = c.Short(nullable: false),
                        Codigo = c.String(nullable: false, maxLength: 2, fixedLength: true, unicode: false),
                        Titulo = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.CBOSubGrupoPrincipalId)
                .ForeignKey("dbo.CBOGrandeGrupo", t => t.CBOGrandeGrupoId)
                .Index(t => t.CBOGrandeGrupoId);
            
            CreateTable(
                "dbo.CBOGrandeGrupo",
                c => new
                    {
                        CBOGrandeGrupoId = c.Short(nullable: false),
                        Codigo = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        Titulo = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.CBOGrandeGrupoId);
            
            CreateTable(
                "dbo.CBOSinonimo",
                c => new
                    {
                        CBOSinonimoId = c.Int(nullable: false, identity: true),
                        CBOOcupacaoId = c.Int(nullable: false),
                        Titulo = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => new { t.CBOSinonimoId, t.CBOOcupacaoId })
                .ForeignKey("dbo.CBOOcupacao", t => t.CBOOcupacaoId)
                .Index(t => t.CBOOcupacaoId);
            
            CreateTable(
                "dbo.ProgramacaoRateio",
                c => new
                    {
                        ProgramacaoId = c.Long(nullable: false),
                        TipoProgramacao = c.Byte(nullable: false),
                        TipoTransacao = c.Byte(nullable: false),
                        PlanoContaId = c.Long(nullable: false),
                        CentroCustoId = c.Long(nullable: false),
                        AplicacaoId = c.Guid(nullable: false),
                        Observacao = c.String(maxLength: 4000),
                        Porcentagem = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.ProgramacaoId, t.TipoProgramacao, t.TipoTransacao, t.PlanoContaId, t.CentroCustoId })
                .ForeignKey("dbo.CentroCusto", t => t.CentroCustoId)
                .ForeignKey("dbo.PlanoConta", t => new { t.PlanoContaId, t.TipoTransacao })
                .ForeignKey("dbo.Programacao", t => new { t.ProgramacaoId, t.TipoProgramacao, t.TipoTransacao })
                .Index(t => new { t.ProgramacaoId, t.TipoProgramacao, t.TipoTransacao })
                .Index(t => new { t.PlanoContaId, t.TipoTransacao })
                .Index(t => t.CentroCustoId);
            
            CreateTable(
                "dbo.PlanoConta",
                c => new
                    {
                        PlanoContaId = c.Long(nullable: false, identity: true),
                        TipoTransacao = c.Byte(nullable: false),
                        AplicacaoId = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 250),
                        Ativo = c.Boolean(nullable: false),
                        Descricao = c.String(maxLength: 4000),
                        ParentId = c.Long(),
                    })
                .PrimaryKey(t => new { t.PlanoContaId, t.TipoTransacao })
                .ForeignKey("dbo.Aplicacao", t => t.AplicacaoId)
                .ForeignKey("dbo.PlanoConta", t => new { t.ParentId, t.TipoTransacao })
                .Index(t => new { t.ParentId, t.TipoTransacao })
                .Index(t => t.AplicacaoId);
            
            CreateTable(
                "dbo.TransferenciaProgramada",
                c => new
                    {
                        TransferenciaProgramadaId = c.Long(nullable: false, identity: true),
                        AplicacaoId = c.Guid(nullable: false),
                        DataInicial = c.DateTime(nullable: false),
                        DataFinal = c.DateTime(nullable: false),
                        Frequencia = c.Byte(nullable: false),
                        Dia = c.Byte(),
                        HasDomingo = c.Boolean(),
                        HasSegunda = c.Boolean(),
                        HasTerca = c.Boolean(),
                        HasQuarta = c.Boolean(),
                        HasQuinta = c.Boolean(),
                        HasSexta = c.Boolean(),
                        HasSabado = c.Boolean(),
                        Historico = c.String(nullable: false, maxLength: 500),
                        ValorVencimento = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Observacao = c.String(maxLength: 4000),
                        ContaOrigem_ContaId = c.Long(nullable: false),
                        ContaOrigem_TipoConta = c.Byte(nullable: false),
                        ContaDestino_ContaId = c.Long(nullable: false),
                        ContaDestino_TipoConta = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.TransferenciaProgramadaId)
                .ForeignKey("dbo.Aplicacao", t => t.AplicacaoId, cascadeDelete: true)
                .ForeignKey("dbo.Conta", t => new { t.ContaDestino_ContaId, t.ContaDestino_TipoConta })
                .ForeignKey("dbo.Conta", t => new { t.ContaOrigem_ContaId, t.ContaOrigem_TipoConta })
                .Index(t => t.AplicacaoId)
                .Index(t => new { t.ContaOrigem_ContaId, t.ContaOrigem_TipoConta })
                .Index(t => new { t.ContaDestino_ContaId, t.ContaDestino_TipoConta });
            
            CreateTable(
                "dbo.Transferencia",
                c => new
                    {
                        TransferenciaId = c.Long(nullable: false, identity: true),
                        AplicacaoId = c.Guid(nullable: false),
                        TransferenciaProgramadaId = c.Long(),
                        Historico = c.String(nullable: false, maxLength: 500),
                        Numero = c.String(maxLength: 50),
                        DataVencimento = c.DateTime(nullable: false),
                        ValorVencimento = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Observacao = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.TransferenciaId)
                .ForeignKey("dbo.Aplicacao", t => t.AplicacaoId)
                .ForeignKey("dbo.TransferenciaProgramada", t => t.TransferenciaProgramadaId)
                .Index(t => t.AplicacaoId)
                .Index(t => t.TransferenciaProgramadaId);
            
            CreateTable(
                "dbo.BandeiraCartao",
                c => new
                    {
                        BandeiraCartaoId = c.Long(nullable: false, identity: true),
                        AplicacaoId = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 250),
                        LogoStorageObject = c.String(maxLength: 255),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BandeiraCartaoId)
                .ForeignKey("dbo.Aplicacao", t => t.AplicacaoId, cascadeDelete: true)
                .Index(t => new { t.AplicacaoId, t.Nome }, unique: true, name: "IX_Unique_Nome");
            
            CreateTable(
                "dbo.Banco",
                c => new
                    {
                        BancoId = c.Long(nullable: false, identity: true),
                        AplicacaoId = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 255),
                        NomeAbreviado = c.String(nullable: false, maxLength: 255),
                        Numero = c.String(nullable: false, maxLength: 15),
                        MascaraNumeroAgencia = c.String(maxLength: 20),
                        MascaraNumeroContaCorrente = c.String(maxLength: 20),
                        MascaraNumeroContaPoupanca = c.String(maxLength: 20),
                        CodigoImportacaoOfx = c.String(maxLength: 15),
                        Site = c.String(maxLength: 500),
                        LogoStorageObject = c.String(maxLength: 255),
                        Descricao = c.String(maxLength: 4000),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BancoId)
                .ForeignKey("dbo.Aplicacao", t => t.AplicacaoId, cascadeDelete: true)
                .Index(t => new { t.AplicacaoId, t.Nome }, unique: true, name: "IX_Unique_Banco_Nome")
                .Index(t => new { t.AplicacaoId, t.NomeAbreviado }, unique: true, name: "IX_Unique_Banco_NomeAbreviado")
                .Index(t => new { t.AplicacaoId, t.Numero }, unique: true, name: "IX_Unique_Banco_Numero");
            
            CreateTable(
                "dbo.LancamentoParcelado",
                c => new
                    {
                        ProgramacaoId = c.Long(nullable: false),
                        TipoProgramacao = c.Byte(nullable: false),
                        TipoTransacao = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProgramacaoId, t.TipoProgramacao, t.TipoTransacao })
                .ForeignKey("dbo.Programacao", t => new { t.ProgramacaoId, t.TipoProgramacao, t.TipoTransacao })
                .Index(t => new { t.ProgramacaoId, t.TipoProgramacao, t.TipoTransacao });
            
            CreateTable(
                "dbo.LancamentoProgramado",
                c => new
                    {
                        ProgramacaoId = c.Long(nullable: false),
                        TipoProgramacao = c.Byte(nullable: false),
                        TipoTransacao = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProgramacaoId, t.TipoProgramacao, t.TipoTransacao })
                .ForeignKey("dbo.Programacao", t => new { t.ProgramacaoId, t.TipoProgramacao, t.TipoTransacao })
                .Index(t => new { t.ProgramacaoId, t.TipoProgramacao, t.TipoTransacao });
            
            CreateTable(
                "dbo.ContaEspecie",
                c => new
                    {
                        ContaId = c.Long(nullable: false),
                        TipoConta = c.Byte(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 100),
                        SaldoInicialData = c.DateTime(nullable: false),
                        SaldoInicialValor = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.ContaId, t.TipoConta })
                .ForeignKey("dbo.Conta", t => new { t.ContaId, t.TipoConta })
                .Index(t => new { t.ContaId, t.TipoConta });
            
            CreateTable(
                "dbo.ContaCorrente",
                c => new
                    {
                        ContaId = c.Long(nullable: false),
                        TipoConta = c.Byte(nullable: false),
                        BancoId = c.Long(nullable: false),
                        NumeroAgencia = c.String(nullable: false, maxLength: 20),
                        NumeroConta = c.String(nullable: false, maxLength: 20),
                        SaldoInicialData = c.DateTime(nullable: false),
                        SaldoInicialValor = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.ContaId, t.TipoConta })
                .ForeignKey("dbo.Conta", t => new { t.ContaId, t.TipoConta })
                .ForeignKey("dbo.Banco", t => t.BancoId)
                .Index(t => new { t.ContaId, t.TipoConta })
                .Index(t => t.BancoId);
            
            CreateTable(
                "dbo.ContaPoupanca",
                c => new
                    {
                        ContaId = c.Long(nullable: false),
                        TipoConta = c.Byte(nullable: false),
                        BancoId = c.Long(nullable: false),
                        NumeroAgencia = c.String(nullable: false, maxLength: 20),
                        NumeroConta = c.String(nullable: false, maxLength: 20),
                        SaldoInicialData = c.DateTime(nullable: false),
                        SaldoInicialValor = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.ContaId, t.TipoConta })
                .ForeignKey("dbo.Conta", t => new { t.ContaId, t.TipoConta })
                .ForeignKey("dbo.Banco", t => t.BancoId)
                .Index(t => new { t.ContaId, t.TipoConta })
                .Index(t => t.BancoId);
            
            CreateTable(
                "dbo.ContaCartaoCredito",
                c => new
                    {
                        ContaId = c.Long(nullable: false),
                        TipoConta = c.Byte(nullable: false),
                        BandeiraCartaoId = c.Long(nullable: false),
                        ContaCorrente_ContaCorrenteId = c.Long(),
                        ContaCorrente_TipoConta = c.Byte(),
                        Nome = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => new { t.ContaId, t.TipoConta })
                .ForeignKey("dbo.Conta", t => new { t.ContaId, t.TipoConta })
                .ForeignKey("dbo.BandeiraCartao", t => t.BandeiraCartaoId)
                .ForeignKey("dbo.ContaCorrente", t => new { t.ContaCorrente_ContaCorrenteId, t.ContaCorrente_TipoConta })
                .Index(t => new { t.ContaId, t.TipoConta })
                .Index(t => t.BandeiraCartaoId)
                .Index(t => new { t.ContaCorrente_ContaCorrenteId, t.ContaCorrente_TipoConta });
            
            CreateTable(
                "dbo.PessoaFisica",
                c => new
                    {
                        PessoaId = c.Long(nullable: false),
                        TipoPessoa = c.Byte(nullable: false),
                        PrimeiroNome = c.String(nullable: false, maxLength: 250),
                        NomeDoMeio = c.String(maxLength: 250),
                        Sobrenome = c.String(maxLength: 250),
                        Sexo = c.Byte(nullable: false),
                        EstadoCivil = c.Byte(),
                        CPF = c.String(maxLength: 11),
                        RG = c.String(maxLength: 9),
                        OrgaoEmissor = c.String(maxLength: 100),
                        DataNascimento = c.DateTime(),
                        Naturalidade = c.String(maxLength: 100),
                        Nacionalidade = c.String(maxLength: 100),
                        CBOOcupacaoId = c.Int(),
                        CBOSinonimoId = c.Int(),
                    })
                .PrimaryKey(t => new { t.PessoaId, t.TipoPessoa })
                .ForeignKey("dbo.Pessoa", t => new { t.PessoaId, t.TipoPessoa })
                .ForeignKey("dbo.CBOOcupacao", t => t.CBOOcupacaoId)
                .ForeignKey("dbo.CBOSinonimo", t => new { t.CBOSinonimoId, t.CBOOcupacaoId })
                .Index(t => new { t.PessoaId, t.TipoPessoa })
                .Index(t => t.CBOOcupacaoId)
                .Index(t => new { t.CBOSinonimoId, t.CBOOcupacaoId });
            
            CreateTable(
                "dbo.PessoaJuridica",
                c => new
                    {
                        PessoaId = c.Long(nullable: false),
                        TipoPessoa = c.Byte(nullable: false),
                        NomeFantasia = c.String(nullable: false, maxLength: 250),
                        RazaoSocial = c.String(nullable: false, maxLength: 250),
                        CNPJ = c.String(maxLength: 14),
                        InscricaoEstadual = c.String(maxLength: 12),
                        InscricaoMunicipal = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => new { t.PessoaId, t.TipoPessoa })
                .ForeignKey("dbo.Pessoa", t => new { t.PessoaId, t.TipoPessoa })
                .Index(t => new { t.PessoaId, t.TipoPessoa });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PessoaJuridica", new[] { "PessoaId", "TipoPessoa" }, "dbo.Pessoa");
            DropForeignKey("dbo.PessoaFisica", new[] { "CBOSinonimoId", "CBOOcupacaoId" }, "dbo.CBOSinonimo");
            DropForeignKey("dbo.PessoaFisica", "CBOOcupacaoId", "dbo.CBOOcupacao");
            DropForeignKey("dbo.PessoaFisica", new[] { "PessoaId", "TipoPessoa" }, "dbo.Pessoa");
            DropForeignKey("dbo.ContaCartaoCredito", new[] { "ContaCorrente_ContaCorrenteId", "ContaCorrente_TipoConta" }, "dbo.ContaCorrente");
            DropForeignKey("dbo.ContaCartaoCredito", "BandeiraCartaoId", "dbo.BandeiraCartao");
            DropForeignKey("dbo.ContaCartaoCredito", new[] { "ContaId", "TipoConta" }, "dbo.Conta");
            DropForeignKey("dbo.ContaPoupanca", "BancoId", "dbo.Banco");
            DropForeignKey("dbo.ContaPoupanca", new[] { "ContaId", "TipoConta" }, "dbo.Conta");
            DropForeignKey("dbo.ContaCorrente", "BancoId", "dbo.Banco");
            DropForeignKey("dbo.ContaCorrente", new[] { "ContaId", "TipoConta" }, "dbo.Conta");
            DropForeignKey("dbo.ContaEspecie", new[] { "ContaId", "TipoConta" }, "dbo.Conta");
            DropForeignKey("dbo.LancamentoProgramado", new[] { "ProgramacaoId", "TipoProgramacao", "TipoTransacao" }, "dbo.Programacao");
            DropForeignKey("dbo.LancamentoParcelado", new[] { "ProgramacaoId", "TipoProgramacao", "TipoTransacao" }, "dbo.Programacao");
            DropForeignKey("dbo.CentroCusto", "ResponsavelId", "dbo.Usuario");
            DropForeignKey("dbo.CentroCusto", "ParentId", "dbo.CentroCusto");
            DropForeignKey("dbo.LancamentoRateio", new[] { "PlanoContaId", "TipoTransacao" }, "dbo.PlanoConta");
            DropForeignKey("dbo.LancamentoRateio", new[] { "LancamentoId", "TipoTransacao" }, "dbo.Lancamento");
            DropForeignKey("dbo.Lancamento", "TransferenciaProgramadaId", "dbo.TransferenciaProgramada");
            DropForeignKey("dbo.Lancamento", "TransferenciaId", "dbo.Transferencia");
            DropForeignKey("dbo.Lancamento", new[] { "ProgramacaoId", "TipoProgramacao", "TipoTransacao" }, "dbo.Programacao");
            DropForeignKey("dbo.Lancamento", new[] { "PessoaId", "TipoPessoa" }, "dbo.Pessoa");
            DropForeignKey("dbo.Lancamento", new[] { "ContaId", "TipoConta" }, "dbo.Conta");
            DropForeignKey("dbo.Banco", "AplicacaoId", "dbo.Aplicacao");
            DropForeignKey("dbo.BandeiraCartao", "AplicacaoId", "dbo.Aplicacao");
            DropForeignKey("dbo.Transferencia", "TransferenciaProgramadaId", "dbo.TransferenciaProgramada");
            DropForeignKey("dbo.Transferencia", "AplicacaoId", "dbo.Aplicacao");
            DropForeignKey("dbo.TransferenciaProgramada", new[] { "ContaOrigem_ContaId", "ContaOrigem_TipoConta" }, "dbo.Conta");
            DropForeignKey("dbo.TransferenciaProgramada", new[] { "ContaDestino_ContaId", "ContaDestino_TipoConta" }, "dbo.Conta");
            DropForeignKey("dbo.TransferenciaProgramada", "AplicacaoId", "dbo.Aplicacao");
            DropForeignKey("dbo.ProgramacaoRateio", new[] { "ProgramacaoId", "TipoProgramacao", "TipoTransacao" }, "dbo.Programacao");
            DropForeignKey("dbo.ProgramacaoRateio", new[] { "PlanoContaId", "TipoTransacao" }, "dbo.PlanoConta");
            DropForeignKey("dbo.PlanoConta", new[] { "ParentId", "TipoTransacao" }, "dbo.PlanoConta");
            DropForeignKey("dbo.PlanoConta", "AplicacaoId", "dbo.Aplicacao");
            DropForeignKey("dbo.ProgramacaoRateio", "CentroCustoId", "dbo.CentroCusto");
            DropForeignKey("dbo.Programacao", new[] { "PessoaId", "TipoPessoa" }, "dbo.Pessoa");
            DropForeignKey("dbo.CBOSinonimo", "CBOOcupacaoId", "dbo.CBOOcupacao");
            DropForeignKey("dbo.CBOOcupacao", "CBOFamiliaId", "dbo.CBOFamilia");
            DropForeignKey("dbo.CBOFamilia", "CBOSubGrupoId", "dbo.CBOSubGrupo");
            DropForeignKey("dbo.CBOSubGrupo", "CBOSubGrupoPrincipalId", "dbo.CBOSubGrupoPrincipal");
            DropForeignKey("dbo.CBOSubGrupoPrincipal", "CBOGrandeGrupoId", "dbo.CBOGrandeGrupo");
            DropForeignKey("dbo.PessoaTelefone", new[] { "TipoTelefoneId", "TipoPessoa" }, "dbo.TipoTelefone");
            DropForeignKey("dbo.TipoTelefone", "AplicacaoId", "dbo.Aplicacao");
            DropForeignKey("dbo.PessoaTelefone", new[] { "PessoaId", "TipoPessoa" }, "dbo.Pessoa");
            DropForeignKey("dbo.PessoaHomePage", new[] { "TipoHomePageId", "TipoPessoa" }, "dbo.TipoHomePage");
            DropForeignKey("dbo.TipoHomePage", "AplicacaoId", "dbo.Aplicacao");
            DropForeignKey("dbo.PessoaHomePage", new[] { "PessoaId", "TipoPessoa" }, "dbo.Pessoa");
            DropForeignKey("dbo.PessoaEndereco", new[] { "TipoEnderecoId", "TipoPessoa" }, "dbo.TipoEndereco");
            DropForeignKey("dbo.TipoEndereco", "AplicacaoId", "dbo.Aplicacao");
            DropForeignKey("dbo.PessoaEndereco", new[] { "PessoaId", "TipoPessoa" }, "dbo.Pessoa");
            DropForeignKey("dbo.PessoaEndereco", "BairroId", "dbo.Bairro");
            DropForeignKey("dbo.Bairro", "CidadeId", "dbo.Cidade");
            DropForeignKey("dbo.Cidade", "EstadoId", "dbo.Estado");
            DropForeignKey("dbo.Estado", "PaisId", "dbo.Pais");
            DropForeignKey("dbo.PessoaEmail", new[] { "TipoEmailId", "TipoPessoa" }, "dbo.TipoEmail");
            DropForeignKey("dbo.TipoEmail", "AplicacaoId", "dbo.Aplicacao");
            DropForeignKey("dbo.PessoaEmail", new[] { "PessoaId", "TipoPessoa" }, "dbo.Pessoa");
            DropForeignKey("dbo.Pessoa", "AplicacaoId", "dbo.Aplicacao");
            DropForeignKey("dbo.Programacao", new[] { "ContaId", "TipoConta" }, "dbo.Conta");
            DropForeignKey("dbo.Programacao", "AplicacaoId", "dbo.Aplicacao");
            DropForeignKey("dbo.Movimento", "MovimentoImportacaoId", "dbo.MovimentoImportacao");
            DropForeignKey("dbo.MovimentoImportacao", "AplicacaoId", "dbo.Aplicacao");
            DropForeignKey("dbo.Movimento", new[] { "ContaId", "TipoConta" }, "dbo.Conta");
            DropForeignKey("dbo.Conciliacao", new[] { "LancamentoId", "TipoTransacao" }, "dbo.Pagamento");
            DropForeignKey("dbo.Pagamento", new[] { "LancamentoId", "TipoTransacao" }, "dbo.Lancamento");
            DropForeignKey("dbo.Conciliacao", new[] { "MovimentoId", "TipoTransacao" }, "dbo.Movimento");
            DropForeignKey("dbo.Movimento", "AplicacaoId", "dbo.Aplicacao");
            DropForeignKey("dbo.Conta", "AplicacaoId", "dbo.Aplicacao");
            DropForeignKey("dbo.Lancamento", "AplicacaoId", "dbo.Aplicacao");
            DropForeignKey("dbo.LancamentoRateio", "CentroCustoId", "dbo.CentroCusto");
            DropForeignKey("dbo.CentroCusto", "AplicacaoId", "dbo.Aplicacao");
            DropForeignKey("dbo.Perfil", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.Usuario", "AplicacaoId", "dbo.Aplicacao");
            DropIndex("dbo.PessoaJuridica", new[] { "PessoaId", "TipoPessoa" });
            DropIndex("dbo.PessoaFisica", new[] { "CBOSinonimoId", "CBOOcupacaoId" });
            DropIndex("dbo.PessoaFisica", new[] { "CBOOcupacaoId" });
            DropIndex("dbo.PessoaFisica", new[] { "PessoaId", "TipoPessoa" });
            DropIndex("dbo.ContaCartaoCredito", new[] { "ContaCorrente_ContaCorrenteId", "ContaCorrente_TipoConta" });
            DropIndex("dbo.ContaCartaoCredito", new[] { "BandeiraCartaoId" });
            DropIndex("dbo.ContaCartaoCredito", new[] { "ContaId", "TipoConta" });
            DropIndex("dbo.ContaPoupanca", new[] { "BancoId" });
            DropIndex("dbo.ContaPoupanca", new[] { "ContaId", "TipoConta" });
            DropIndex("dbo.ContaCorrente", new[] { "BancoId" });
            DropIndex("dbo.ContaCorrente", new[] { "ContaId", "TipoConta" });
            DropIndex("dbo.ContaEspecie", new[] { "ContaId", "TipoConta" });
            DropIndex("dbo.LancamentoProgramado", new[] { "ProgramacaoId", "TipoProgramacao", "TipoTransacao" });
            DropIndex("dbo.LancamentoParcelado", new[] { "ProgramacaoId", "TipoProgramacao", "TipoTransacao" });
            DropIndex("dbo.Banco", "IX_Unique_Banco_Numero");
            DropIndex("dbo.Banco", "IX_Unique_Banco_NomeAbreviado");
            DropIndex("dbo.Banco", "IX_Unique_Banco_Nome");
            DropIndex("dbo.BandeiraCartao", "IX_Unique_Nome");
            DropIndex("dbo.Transferencia", new[] { "TransferenciaProgramadaId" });
            DropIndex("dbo.Transferencia", new[] { "AplicacaoId" });
            DropIndex("dbo.TransferenciaProgramada", new[] { "ContaDestino_ContaId", "ContaDestino_TipoConta" });
            DropIndex("dbo.TransferenciaProgramada", new[] { "ContaOrigem_ContaId", "ContaOrigem_TipoConta" });
            DropIndex("dbo.TransferenciaProgramada", new[] { "AplicacaoId" });
            DropIndex("dbo.PlanoConta", new[] { "AplicacaoId" });
            DropIndex("dbo.PlanoConta", new[] { "ParentId", "TipoTransacao" });
            DropIndex("dbo.ProgramacaoRateio", new[] { "CentroCustoId" });
            DropIndex("dbo.ProgramacaoRateio", new[] { "PlanoContaId", "TipoTransacao" });
            DropIndex("dbo.ProgramacaoRateio", new[] { "ProgramacaoId", "TipoProgramacao", "TipoTransacao" });
            DropIndex("dbo.CBOSinonimo", new[] { "CBOOcupacaoId" });
            DropIndex("dbo.CBOSubGrupoPrincipal", new[] { "CBOGrandeGrupoId" });
            DropIndex("dbo.CBOSubGrupo", new[] { "CBOSubGrupoPrincipalId" });
            DropIndex("dbo.CBOFamilia", new[] { "CBOSubGrupoId" });
            DropIndex("dbo.CBOOcupacao", new[] { "CBOFamiliaId" });
            DropIndex("dbo.TipoTelefone", "IX_Unique_Nome");
            DropIndex("dbo.PessoaTelefone", new[] { "TipoTelefoneId", "TipoPessoa" });
            DropIndex("dbo.PessoaTelefone", new[] { "PessoaId", "TipoPessoa" });
            DropIndex("dbo.TipoHomePage", "IX_Unique_Nome");
            DropIndex("dbo.PessoaHomePage", new[] { "TipoHomePageId", "TipoPessoa" });
            DropIndex("dbo.PessoaHomePage", new[] { "PessoaId", "TipoPessoa" });
            DropIndex("dbo.TipoEndereco", "IX_Unique_Nome");
            DropIndex("dbo.Estado", new[] { "PaisId" });
            DropIndex("dbo.Cidade", new[] { "EstadoId" });
            DropIndex("dbo.Bairro", new[] { "CidadeId" });
            DropIndex("dbo.PessoaEndereco", new[] { "BairroId" });
            DropIndex("dbo.PessoaEndereco", new[] { "TipoEnderecoId", "TipoPessoa" });
            DropIndex("dbo.PessoaEndereco", new[] { "PessoaId", "TipoPessoa" });
            DropIndex("dbo.TipoEmail", "IX_Unique_Nome");
            DropIndex("dbo.PessoaEmail", new[] { "TipoEmailId", "TipoPessoa" });
            DropIndex("dbo.PessoaEmail", new[] { "PessoaId", "TipoPessoa" });
            DropIndex("dbo.Pessoa", new[] { "AplicacaoId" });
            DropIndex("dbo.Programacao", new[] { "ContaId", "TipoConta" });
            DropIndex("dbo.Programacao", new[] { "PessoaId", "TipoPessoa" });
            DropIndex("dbo.Programacao", new[] { "AplicacaoId" });
            DropIndex("dbo.MovimentoImportacao", new[] { "AplicacaoId" });
            DropIndex("dbo.Pagamento", new[] { "LancamentoId", "TipoTransacao" });
            DropIndex("dbo.Conciliacao", new[] { "LancamentoId", "TipoTransacao" });
            DropIndex("dbo.Conciliacao", new[] { "MovimentoId", "TipoTransacao" });
            DropIndex("dbo.Movimento", new[] { "MovimentoImportacaoId" });
            DropIndex("dbo.Movimento", new[] { "ContaId", "TipoConta" });
            DropIndex("dbo.Movimento", new[] { "AplicacaoId" });
            DropIndex("dbo.Conta", new[] { "AplicacaoId" });
            DropIndex("dbo.Lancamento", new[] { "TransferenciaProgramadaId" });
            DropIndex("dbo.Lancamento", new[] { "TransferenciaId" });
            DropIndex("dbo.Lancamento", new[] { "ContaId", "TipoConta" });
            DropIndex("dbo.Lancamento", new[] { "PessoaId", "TipoPessoa" });
            DropIndex("dbo.Lancamento", new[] { "AplicacaoId" });
            DropIndex("dbo.Lancamento", new[] { "ProgramacaoId", "TipoProgramacao", "TipoTransacao" });
            DropIndex("dbo.LancamentoRateio", new[] { "CentroCustoId" });
            DropIndex("dbo.LancamentoRateio", new[] { "PlanoContaId", "TipoTransacao" });
            DropIndex("dbo.LancamentoRateio", new[] { "LancamentoId", "TipoTransacao" });
            DropIndex("dbo.CentroCusto", new[] { "ParentId" });
            DropIndex("dbo.CentroCusto", new[] { "ResponsavelId" });
            DropIndex("dbo.CentroCusto", new[] { "AplicacaoId" });
            DropIndex("dbo.Perfil", new[] { "UsuarioId" });
            DropIndex("dbo.Usuario", new[] { "AplicacaoId" });
            DropTable("dbo.PessoaJuridica");
            DropTable("dbo.PessoaFisica");
            DropTable("dbo.ContaCartaoCredito");
            DropTable("dbo.ContaPoupanca");
            DropTable("dbo.ContaCorrente");
            DropTable("dbo.ContaEspecie");
            DropTable("dbo.LancamentoProgramado");
            DropTable("dbo.LancamentoParcelado");
            DropTable("dbo.Banco");
            DropTable("dbo.BandeiraCartao");
            DropTable("dbo.Transferencia");
            DropTable("dbo.TransferenciaProgramada");
            DropTable("dbo.PlanoConta");
            DropTable("dbo.ProgramacaoRateio");
            DropTable("dbo.CBOSinonimo");
            DropTable("dbo.CBOGrandeGrupo");
            DropTable("dbo.CBOSubGrupoPrincipal");
            DropTable("dbo.CBOSubGrupo");
            DropTable("dbo.CBOFamilia");
            DropTable("dbo.CBOOcupacao");
            DropTable("dbo.TipoTelefone");
            DropTable("dbo.PessoaTelefone");
            DropTable("dbo.TipoHomePage");
            DropTable("dbo.PessoaHomePage");
            DropTable("dbo.TipoEndereco");
            DropTable("dbo.Pais");
            DropTable("dbo.Estado");
            DropTable("dbo.Cidade");
            DropTable("dbo.Bairro");
            DropTable("dbo.PessoaEndereco");
            DropTable("dbo.TipoEmail");
            DropTable("dbo.PessoaEmail");
            DropTable("dbo.Pessoa");
            DropTable("dbo.Programacao");
            DropTable("dbo.MovimentoImportacao");
            DropTable("dbo.Pagamento");
            DropTable("dbo.Conciliacao");
            DropTable("dbo.Movimento");
            DropTable("dbo.Conta");
            DropTable("dbo.Lancamento");
            DropTable("dbo.LancamentoRateio");
            DropTable("dbo.CentroCusto");
            DropTable("dbo.Perfil");
            DropTable("dbo.Usuario");
            DropTable("dbo.Aplicacao");
        }
    }
}
