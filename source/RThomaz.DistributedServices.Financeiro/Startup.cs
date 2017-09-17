using RThomaz.DistributedServices.Financeiro.App_Start;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using RThomaz.Infra.CrossCutting.Swagger;

[assembly: OwinStartup(typeof(RThomaz.DistributedServices.Financeiro.Startup))]
namespace RThomaz.DistributedServices.Financeiro
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            ConfigureOAuth(app);

            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

            AutoMapperConfig.RegisterMappings();

            SwaggerConfig.Register(config, "v1", "RThomaz.DistributedServices.Financeiro", "RThomaz.DistributedServices.Financeiro.XML");

            SimpleInjectorInitializer.Initialize(config);
        }
    }
}