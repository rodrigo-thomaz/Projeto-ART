using Microsoft.Owin;
using Owin;
using System.Data.Entity;
using System.Web.Http;
using RThomaz.Infra.CrossCutting.Identity.Context;
using RThomaz.Infra.CrossCutting.Swagger;

[assembly: OwinStartup(typeof(RThomaz.DistributedServices.Identity.Startup))]

namespace RThomaz.DistributedServices.Identity
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

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Migrations.Configuration>());

            AutoMapperConfig.RegisterMappings();

            SwaggerConfig.Register(config, "v1", "RThomaz.DistributedServices.Identity", "RThomaz.DistributedServices.Identity.XML");

            SimpleInjectorInitializer.Initialize(config);
        }
    }

}