using RThomaz.Infra.Data.Persistence.Configurations;
using RThomaz.Domain.Financeiro.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace RThomaz.Infra.Data.Persistence.Contexts
{
    public class RThomazDbContext : DbContext
    {
        public RThomazDbContext()
            : base("DefaultConnection")
        {
            Initialize();
        }

        private void Initialize()
        {
            Configuration.ValidateOnSaveEnabled = true;
        }

        public static RThomazDbContext Create()
        {
            return new RThomazDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Conventions

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //Configurations

            modelBuilder.Configurations.Add(new AplicacaoConfiguration());
            modelBuilder.Configurations.Add(new TipoEmailConfiguration());
            modelBuilder.Configurations.Add(new TipoEnderecoConfiguration());
            modelBuilder.Configurations.Add(new TipoHomePageConfiguration());
            modelBuilder.Configurations.Add(new TipoTelefoneConfiguration());            
            modelBuilder.Configurations.Add(new UsuarioConfiguration());            
            modelBuilder.Configurations.Add(new PerfilConfiguration());
            modelBuilder.Configurations.Add(new PaisConfiguration());
            modelBuilder.Configurations.Add(new EstadoConfiguration());
            modelBuilder.Configurations.Add(new CidadeConfiguration());
            modelBuilder.Configurations.Add(new BairroConfiguration());
            modelBuilder.Configurations.Add(new DadoBancarioConfiguration());
            modelBuilder.Configurations.Add(new SaldoInicialConfiguration());
            modelBuilder.Configurations.Add(new ContaConfiguration());
            modelBuilder.Configurations.Add(new ContaEspecieConfiguration());
            modelBuilder.Configurations.Add(new ContaCorrenteConfiguration());
            modelBuilder.Configurations.Add(new ContaPoupancaConfiguration());
            modelBuilder.Configurations.Add(new ContaCartaoCreditoConfiguration());
            modelBuilder.Configurations.Add(new PessoaConfiguration());
            modelBuilder.Configurations.Add(new PessoaFisicaConfiguration());
            modelBuilder.Configurations.Add(new PessoaJuridicaConfiguration());
            modelBuilder.Configurations.Add(new PessoaEmailConfiguration());
            modelBuilder.Configurations.Add(new PessoaEnderecoConfiguration());
            modelBuilder.Configurations.Add(new PessoaHomePageConfiguration());
            modelBuilder.Configurations.Add(new PessoaTelefoneConfiguration());
            modelBuilder.Configurations.Add(new PlanoContaConfiguration());
            modelBuilder.Configurations.Add(new CentroCustoConfiguration());
            modelBuilder.Configurations.Add(new BandeiraCartaoConfiguration());
            modelBuilder.Configurations.Add(new BancoConfiguration());
            modelBuilder.Configurations.Add(new MovimentoImportacaoConfiguration());
            modelBuilder.Configurations.Add(new MovimentoConfiguration());            
            modelBuilder.Configurations.Add(new ProgramacaoConfiguration());
            modelBuilder.Configurations.Add(new LancamentoProgramadoConfiguration());
            modelBuilder.Configurations.Add(new TransferenciaProgramadaConfiguration());
            modelBuilder.Configurations.Add(new LancamentoRateioConfiguration());
            modelBuilder.Configurations.Add(new ProgramacaoRateioConfiguration());                        
            modelBuilder.Configurations.Add(new TransferenciaConfiguration());
            modelBuilder.Configurations.Add(new LancamentoConfiguration());
            modelBuilder.Configurations.Add(new LancamentoParceladoConfiguration());
            modelBuilder.Configurations.Add(new PagamentoConfiguration());
            modelBuilder.Configurations.Add(new ConciliacaoConfiguration());
            modelBuilder.Configurations.Add(new CBOGrandeGrupoConfiguration());
            modelBuilder.Configurations.Add(new CBOSubGrupoPrincipalConfiguration());
            modelBuilder.Configurations.Add(new CBOSubGrupoConfiguration());
            modelBuilder.Configurations.Add(new CBOFamiliaConfiguration());
            modelBuilder.Configurations.Add(new CBOOcupacaoConfiguration());
            modelBuilder.Configurations.Add(new CBOSinonimoConfiguration());
            modelBuilder.Configurations.Add(new ProgramadorConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Aplicacao> Aplicacao { get; set; }
        public DbSet<TipoEmail> TipoEmail { get; set; }
        public DbSet<TipoTelefone> TipoTelefone { get; set; }
        public DbSet<TipoEndereco> TipoEndereco { get; set; }
        public DbSet<TipoHomePage> TipoHomePage { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Pais> Pais { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<Bairro> Bairro { get; set; }
        public DbSet<Conta> Conta { get; set; }
        public DbSet<Lancamento> Lancamento { get; set; }
        public DbSet<LancamentoProgramado> LancamentoProgramado { get; set; }
        public DbSet<TransferenciaProgramada> TransferenciaProgramada { get; set; }
        public DbSet<Transferencia> Transferencia { get; set; }        
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<PessoaEmail> PessoaEmail { get; set; }
        public DbSet<PessoaEndereco> PessoaEndereco { get; set; }
        public DbSet<PessoaHomePage> PessoaHomePage { get; set; }
        public DbSet<PessoaTelefone> PessoaTelefone { get; set; }
        public DbSet<PlanoConta> PlanoConta { get; set; }        
        public DbSet<CentroCusto> CentroCusto { get; set; }
        public DbSet<BandeiraCartao> BandeiraCartao { get; set; }
        public DbSet<Banco> Banco { get; set; }
        public DbSet<MovimentoImportacao> MovimentoImportacao { get; set; }
        public DbSet<Movimento> Movimento { get; set; }
        public DbSet<LancamentoRateio> LancamentoRateio { get; set; }
        public DbSet<ProgramacaoRateio> ProgramacaoRateio { get; set; }
        public DbSet<Programacao> Programacao { get; set; }
        public DbSet<LancamentoParcelado> LancamentoParcelado { get; set; }
        public DbSet<Pagamento> Pagamento { get; set; }
        public DbSet<Conciliacao> Conciliacao { get; set; }
        public DbSet<CBOGrandeGrupo> CBOGrandeGrupo { get; set; }
        public DbSet<CBOSubGrupoPrincipal> CBOSubGrupoPrincipal { get; set; }
        public DbSet<CBOSubGrupo> CBOSubGrupo { get; set; }
        public DbSet<CBOFamilia> CBOFamilia { get; set; }
        public DbSet<CBOOcupacao> CBOOcupacao { get; set; }
        public DbSet<CBOSinonimo> CBOSinonimo { get; set; }
    }
}
