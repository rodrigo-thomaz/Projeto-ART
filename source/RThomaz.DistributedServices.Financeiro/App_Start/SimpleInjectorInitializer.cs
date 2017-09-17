using SimpleInjector;
using RThomaz.Infra.CrossCutting.IoC;
using System.Web.Http;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

namespace RThomaz.DistributedServices.Financeiro
{
    public class SimpleInjectorInitializer
    {
        public static void Initialize(HttpConfiguration config)
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            // Chamada dos módulos do Simple Injector
            InitializeContainer(container);           

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        private static void InitializeContainer(Container container)
        {
            BootStrapper.RegisterRepositoriesServices(container);
            BootStrapper.RegisterDomainServices(container);
            BootStrapper.RegisterApplicationServices(container);
        }
    }
}