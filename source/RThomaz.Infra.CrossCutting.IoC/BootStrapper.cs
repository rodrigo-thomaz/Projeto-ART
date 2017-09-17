using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RThomaz.Application.Financeiro.AppServices;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.Domain.Financeiro.Interfaces.Repositories;
using RThomaz.Infra.CrossCutting.Identity.Managers;
using RThomaz.Infra.CrossCutting.Identity.Context;
using RThomaz.Infra.Data.Financeiro.Repositories;
using SimpleInjector;
using System.Security.Claims;
using System.Web;
using System.Linq;
using RThomaz.Domain.Financeiro.Entities;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using RThomaz.Domain.Financeiro.Services;
using RThomaz.Infra.CrossCutting.Identity.Entities;
using RThomaz.Infra.Data.Core;
using RThomaz.Domain.Core;
using System;

namespace RThomaz.Infra.CrossCutting.IoC
{
    public class BootStrapper
    {
        public static void RegisterIdentityServices(Container container)
        {
            var context = new ApplicationDbContext();

            container.Register<ApplicationDbContext>(Lifestyle.Scoped);
            container.Register<IUserStore<ApplicationUser, Guid>>(() => new UserStore<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(context), Lifestyle.Scoped);
            container.Register<IRoleStore<ApplicationRole, Guid>>(() => new RoleStore<ApplicationRole, Guid, ApplicationUserRole>(context), Lifestyle.Scoped);
            container.Register<ApplicationRoleManager>(Lifestyle.Scoped);
            container.Register<ApplicationUserManager>(Lifestyle.Scoped);
            container.Register<ApplicationSignInManager>(Lifestyle.Scoped);
        }

        public static void RegisterRepositoriesServices(Container container)
        {
            container.Register<IBancoRepository, BancoRepository>(Lifestyle.Scoped);
            container.Register<IBandeiraCartaoRepository, BandeiraCartaoRepository>(Lifestyle.Scoped);
            container.Register<ICBOOcupacaoRepository, CBOOcupacaoRepository>(Lifestyle.Scoped);
            container.Register<ICentroCustoRepository, CentroCustoRepository>(Lifestyle.Scoped);
            container.Register<IConciliacaoRepository, ConciliacaoRepository>(Lifestyle.Scoped);
            container.Register<IContaRepository, ContaRepository>(Lifestyle.Scoped);
            container.Register<ILancamentoProgramadoRepository, LancamentoProgramadoRepository>(Lifestyle.Scoped);
            container.Register<ILancamentoRepository, LancamentoRepository>(Lifestyle.Scoped);
            container.Register<IMovimentoImportacaoRepository, MovimentoImportacaoRepository>(Lifestyle.Scoped);
            container.Register<IMovimentoRepository, MovimentoRepository>(Lifestyle.Scoped);
            container.Register<IPerfilRepository, PerfilRepository>(Lifestyle.Scoped);
            container.Register<IPessoaRepository, PessoaRepository>(Lifestyle.Scoped);
            container.Register<IPlanoContaRepository, PlanoContaRepository>(Lifestyle.Scoped);
            container.Register<ITipoEmailRepository, TipoEmailRepository>(Lifestyle.Scoped);
            container.Register<ITipoEnderecoRepository, TipoEnderecoRepository>(Lifestyle.Scoped);
            container.Register<ITipoHomePageRepository, TipoHomePageRepository>(Lifestyle.Scoped);
            container.Register<ITipoTelefoneRepository, TipoTelefoneRepository>(Lifestyle.Scoped);
            container.Register<ITransferenciaProgramadaRepository, TransferenciaProgramadaRepository>(Lifestyle.Scoped);
            container.Register<ITransferenciaRepository, TransferenciaRepository>(Lifestyle.Scoped);
            container.Register<IUsuarioRepository, UsuarioRepository>(Lifestyle.Scoped);

            container.RegisterInitializer<IBancoRepository>(x => SetPropertiesRepositoriesServices<IBancoRepository, Banco>(x));
            container.RegisterInitializer<IBandeiraCartaoRepository>(x => SetPropertiesRepositoriesServices<IBandeiraCartaoRepository, BandeiraCartao>(x));
            container.RegisterInitializer<ICBOOcupacaoRepository>(x => SetPropertiesRepositoriesServices<ICBOOcupacaoRepository, CBOOcupacao>(x));
            container.RegisterInitializer<ICentroCustoRepository>(x => SetPropertiesRepositoriesServices<ICentroCustoRepository, CentroCusto>(x));
            container.RegisterInitializer<IConciliacaoRepository>(x => SetPropertiesRepositoriesServices<IConciliacaoRepository, Conciliacao>(x));
            container.RegisterInitializer<IContaRepository>(x => SetPropertiesRepositoriesServices<IContaRepository, Conta>(x));
            container.RegisterInitializer<ILancamentoProgramadoRepository>(x => SetPropertiesRepositoriesServices<ILancamentoProgramadoRepository, LancamentoProgramado>(x));
            container.RegisterInitializer<ILancamentoRepository>(x => SetPropertiesRepositoriesServices<ILancamentoRepository, Lancamento>(x));
            container.RegisterInitializer<IMovimentoImportacaoRepository>(x => SetPropertiesRepositoriesServices<IMovimentoImportacaoRepository, MovimentoImportacao>(x));
            container.RegisterInitializer<IMovimentoRepository>(x => SetPropertiesRepositoriesServices<IMovimentoRepository, Movimento>(x));
            container.RegisterInitializer<IPerfilRepository>(x => SetPropertiesRepositoriesServices<IPerfilRepository, Perfil>(x));
            container.RegisterInitializer<IPessoaRepository>(x => SetPropertiesRepositoriesServices<IPessoaRepository, Pessoa>(x));
            container.RegisterInitializer<IPlanoContaRepository>(x => SetPropertiesRepositoriesServices<IPlanoContaRepository, PlanoConta>(x));
            container.RegisterInitializer<ITipoEmailRepository>(x => SetPropertiesRepositoriesServices<ITipoEmailRepository, TipoEmail>(x));
            container.RegisterInitializer<ITipoEnderecoRepository>(x => SetPropertiesRepositoriesServices<ITipoEnderecoRepository, TipoEndereco>(x));
            container.RegisterInitializer<ITipoHomePageRepository>(x => SetPropertiesRepositoriesServices<ITipoHomePageRepository, TipoHomePage>(x));
            container.RegisterInitializer<ITipoTelefoneRepository>(x => SetPropertiesRepositoriesServices<ITipoTelefoneRepository, TipoTelefone>(x));
            container.RegisterInitializer<ITransferenciaProgramadaRepository>(x => SetPropertiesRepositoriesServices<ITransferenciaProgramadaRepository, TransferenciaProgramada>(x));
            container.RegisterInitializer<ITransferenciaRepository>(x => SetPropertiesRepositoriesServices<ITransferenciaRepository, Transferencia>(x));
            container.RegisterInitializer<IUsuarioRepository>(x => SetPropertiesRepositoriesServices<IUsuarioRepository, Usuario>(x));
        }

        public static void RegisterDomainServices(Container container)
        {
            container.Register<IAccountService, AccountService>(Lifestyle.Scoped);
            container.Register<IBancoService, BancoService>(Lifestyle.Scoped);
            container.Register<IBandeiraCartaoService, BandeiraCartaoService>(Lifestyle.Scoped);
            container.Register<ICBOOcupacaoService, CBOOcupacaoService>(Lifestyle.Scoped);
            container.Register<ICentroCustoService, CentroCustoService>(Lifestyle.Scoped);
            container.Register<IConciliacaoService, ConciliacaoService>(Lifestyle.Scoped);
            container.Register<IContaService, ContaService>(Lifestyle.Scoped);
            container.Register<ILancamentoProgramadoService, LancamentoProgramadoService>(Lifestyle.Scoped);
            container.Register<ILancamentoPagarReceberService, LancamentoPagarReceberService>(Lifestyle.Scoped);
            container.Register<ILancamentoPagoRecebidoService, LancamentoPagoRecebidoService>(Lifestyle.Scoped);
            container.Register<ILocalidadeService, LocalidadeService>(Lifestyle.Scoped);
            container.Register<IMovimentoImportacaoService, MovimentoImportacaoService>(Lifestyle.Scoped);
            container.Register<IMovimentoImportacaoOFXService, MovimentoImportacaoOFXService>(Lifestyle.Scoped);
            container.Register<IMovimentoService, MovimentoService>(Lifestyle.Scoped);
            container.Register<IPerfilService, PerfilService>(Lifestyle.Scoped);
            container.Register<IPessoaService, PessoaService>(Lifestyle.Scoped);
            container.Register<IPlanoContaService, PlanoContaService>(Lifestyle.Scoped);
            container.Register<ITipoEmailService, TipoEmailService>(Lifestyle.Scoped);
            container.Register<ITipoEnderecoService, TipoEnderecoService>(Lifestyle.Scoped);
            container.Register<ITipoHomePageService, TipoHomePageService>(Lifestyle.Scoped);
            container.Register<ITipoTelefoneService, TipoTelefoneService>(Lifestyle.Scoped);
            container.Register<ITransferenciaProgramadaService, TransferenciaProgramadaService>(Lifestyle.Scoped);
            container.Register<ITransferenciaService, TransferenciaService>(Lifestyle.Scoped);
            container.Register<IUsuarioService, UsuarioService>(Lifestyle.Scoped);

            container.RegisterInitializer<IBancoService>(x => SetPropertiesDomainServices(x));
            container.RegisterInitializer<IBandeiraCartaoService>(x => SetPropertiesDomainServices(x));
            container.RegisterInitializer<ICentroCustoService>(x => SetPropertiesDomainServices(x));
            container.RegisterInitializer<IConciliacaoService>(x => SetPropertiesDomainServices(x));
            container.RegisterInitializer<IContaService>(x => SetPropertiesDomainServices(x));
            container.RegisterInitializer<ILancamentoProgramadoService>(x => SetPropertiesDomainServices(x));
            container.RegisterInitializer<ILancamentoPagarReceberService>(x => SetPropertiesDomainServices(x));
            container.RegisterInitializer<ILancamentoPagoRecebidoService>(x => SetPropertiesDomainServices(x));
            container.RegisterInitializer<ILocalidadeService>(x => SetPropertiesDomainServices(x));
            container.RegisterInitializer<IMovimentoImportacaoService>(x => SetPropertiesDomainServices(x));
            container.RegisterInitializer<IMovimentoImportacaoOFXService>(x => SetPropertiesDomainServices(x));
            container.RegisterInitializer<IMovimentoService>(x => SetPropertiesDomainServices(x));
            container.RegisterInitializer<IPerfilService>(x => SetPropertiesDomainServices(x));
            container.RegisterInitializer<IPessoaService>(x => SetPropertiesDomainServices(x));
            container.RegisterInitializer<IPlanoContaService>(x => SetPropertiesDomainServices(x));
            container.RegisterInitializer<ITipoEmailService>(x => SetPropertiesDomainServices(x));
            container.RegisterInitializer<ITipoEnderecoService>(x => SetPropertiesDomainServices(x));
            container.RegisterInitializer<ITipoHomePageService>(x => SetPropertiesDomainServices(x));
            container.RegisterInitializer<ITipoTelefoneService>(x => SetPropertiesDomainServices(x));
            container.RegisterInitializer<ITransferenciaProgramadaService>(x => SetPropertiesDomainServices(x));
            container.RegisterInitializer<ITransferenciaService>(x => SetPropertiesDomainServices(x));
            container.RegisterInitializer<IUsuarioService>(x => SetPropertiesDomainServices(x));
        }

        public static void RegisterApplicationServices(Container container)
        {
            container.Register<IAccountAppService, AccountAppService>(Lifestyle.Scoped);
            container.Register<IBancoAppService, BancoAppService>(Lifestyle.Scoped);
            container.Register<IBandeiraCartaoAppService, BandeiraCartaoAppService>(Lifestyle.Scoped);
            container.Register<ICBOOcupacaoAppService, CBOOcupacaoAppService>(Lifestyle.Scoped);
            container.Register<ICentroCustoAppService, CentroCustoAppService>(Lifestyle.Scoped);
            container.Register<IConciliacaoAppService, ConciliacaoAppService>(Lifestyle.Scoped);
            container.Register<IContaAppService, ContaAppService>(Lifestyle.Scoped);
            container.Register<ILancamentoProgramadoAppService, LancamentoProgramadoAppService>(Lifestyle.Scoped);
            container.Register<ILancamentoPagarReceberAppService, LancamentoPagarReceberAppService>(Lifestyle.Scoped);
            container.Register<ILancamentoPagoRecebidoAppService, LancamentoPagoRecebidoAppService>(Lifestyle.Scoped);
            container.Register<ILocalidadeAppService, LocalidadeAppService>(Lifestyle.Scoped);
            container.Register<IMovimentoImportacaoAppService, MovimentoImportacaoAppService>(Lifestyle.Scoped);
            container.Register<IMovimentoImportacaoOFXAppService, MovimentoImportacaoOFXAppService>(Lifestyle.Scoped);
            container.Register<IMovimentoAppService, MovimentoAppService>(Lifestyle.Scoped);
            container.Register<IPerfilAppService, PerfilAppService>(Lifestyle.Scoped);
            container.Register<IPessoaAppService, PessoaAppService>(Lifestyle.Scoped);
            container.Register<IPlanoContaAppService, PlanoContaAppService>(Lifestyle.Scoped);
            container.Register<ITipoEmailAppService, TipoEmailAppService>(Lifestyle.Scoped);
            container.Register<ITipoEnderecoAppService, TipoEnderecoAppService>(Lifestyle.Scoped);
            container.Register<ITipoHomePageAppService, TipoHomePageAppService>(Lifestyle.Scoped);
            container.Register<ITipoTelefoneAppService, TipoTelefoneAppService>(Lifestyle.Scoped);
            container.Register<ITransferenciaProgramadaAppService, TransferenciaProgramadaAppService>(Lifestyle.Scoped);
            container.Register<ITransferenciaAppService, TransferenciaAppService>(Lifestyle.Scoped);
            container.Register<IUsuarioAppService, UsuarioAppService>(Lifestyle.Scoped);

            container.RegisterInitializer<IBancoAppService>(x => SetPropertiesApplicationServices(x));
            container.RegisterInitializer<IBandeiraCartaoAppService>(x => SetPropertiesApplicationServices(x));
            container.RegisterInitializer<ICentroCustoAppService>(x => SetPropertiesApplicationServices(x));
            container.RegisterInitializer<IConciliacaoAppService>(x => SetPropertiesApplicationServices(x));
            container.RegisterInitializer<IContaAppService>(x => SetPropertiesApplicationServices(x));
            container.RegisterInitializer<ILancamentoProgramadoAppService>(x => SetPropertiesApplicationServices(x));
            container.RegisterInitializer<ILancamentoPagarReceberAppService>(x => SetPropertiesApplicationServices(x));
            container.RegisterInitializer<ILancamentoPagoRecebidoAppService>(x => SetPropertiesApplicationServices(x));
            container.RegisterInitializer<ILocalidadeAppService>(x => SetPropertiesApplicationServices(x));
            container.RegisterInitializer<IMovimentoImportacaoAppService>(x => SetPropertiesApplicationServices(x));
            container.RegisterInitializer<IMovimentoImportacaoOFXAppService>(x => SetPropertiesApplicationServices(x));
            container.RegisterInitializer<IMovimentoAppService>(x => SetPropertiesApplicationServices(x));
            container.RegisterInitializer<IPerfilAppService>(x => SetPropertiesApplicationServices(x));
            container.RegisterInitializer<IPessoaAppService>(x => SetPropertiesApplicationServices(x));
            container.RegisterInitializer<IPlanoContaAppService>(x => SetPropertiesApplicationServices(x));
            container.RegisterInitializer<ITipoEmailAppService>(x => SetPropertiesApplicationServices(x));
            container.RegisterInitializer<ITipoEnderecoAppService>(x => SetPropertiesApplicationServices(x));
            container.RegisterInitializer<ITipoHomePageAppService>(x => SetPropertiesApplicationServices(x));
            container.RegisterInitializer<ITipoTelefoneAppService>(x => SetPropertiesApplicationServices(x));
            container.RegisterInitializer<ITransferenciaProgramadaAppService>(x => SetPropertiesApplicationServices(x));
            container.RegisterInitializer<ITransferenciaAppService>(x => SetPropertiesApplicationServices(x));
            container.RegisterInitializer<IUsuarioAppService>(x => SetPropertiesApplicationServices(x));
        }

        private static void SetPropertiesRepositoriesServices<TService, TEntity>(TService service)
            where TService : IRepositoryBase<TEntity>
            where TEntity : class
        {
            service.AplicacaoId = GetAplicacaoId();
        }

        private static void SetPropertiesDomainServices<TService>(TService service)
            where TService : IServiceBase
        {
            service.AplicacaoId = GetAplicacaoId();
            service.StorageBucketName = GetStorageBucketName();
        }

        private static void SetPropertiesApplicationServices<TAppService>(TAppService service)
            where TAppService : IAppServiceBase
        {
            service.AplicacaoId = GetAplicacaoId();
            service.StorageBucketName = GetStorageBucketName();
        }

        private static Guid GetAplicacaoId()
        {
            var user = HttpContext.Current.User;
            if(user == null)
            {
                return default(Guid);
            }
            var claim = ((ClaimsIdentity)user.Identity).Claims.FirstOrDefault(x => x.Type.Equals("aplicacaoId"));
            if(claim == null)
            {
                return default(Guid);
            }
            return Guid.Parse(claim.Value);
        }

        private static string GetStorageBucketName()
        {
            var user = HttpContext.Current.User;
            if (user == null)
            {
                return default(string);
            }
            var claim = ((ClaimsIdentity)user.Identity).Claims.FirstOrDefault(x => x.Type.Equals("storageBucketName"));
            if (claim == null)
            {
                return default(string);
            }
            return claim.Value;
        }
    }
}
