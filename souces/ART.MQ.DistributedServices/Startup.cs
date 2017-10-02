using ART.MQ.DistributedServices.App_Start;
using ART.MQ.DistributedServices.Controllers;
using ART.MQ.DistributedServices.IProducers;
using ART.MQ.DistributedServices.Producers;
using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Microsoft.Owin.BuilderProperties;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using RabbitMQ.Client;
using System.Configuration;
using System.Threading;
using System.Web.Http;

[assembly: OwinStartup(typeof(ART.MQ.DistributedServices.Startup))]
namespace ART.MQ.DistributedServices
{
    public class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            ConfigureOAuth(app);

            WebApiConfig.Register(config);

            AutoMapperConfig.RegisterMappings();

            // Make the autofac container
            var builder = new ContainerBuilder();

            // Register your Bus
            builder.Register(c =>
            {
                var hostName = ConfigurationManager.AppSettings["RabbitMQHostName"];
                var virtualHost = ConfigurationManager.AppSettings["RabbitMQVirtualHost"];
                var username = ConfigurationManager.AppSettings["RabbitMQUsername"];
                var password = ConfigurationManager.AppSettings["RabbitMQPassword"];

                var factory = new ConnectionFactory();

                factory.UserName = username;
                factory.Password = password;
                factory.VirtualHost = virtualHost;
                factory.HostName = hostName;

                IConnection conn = factory.CreateConnection();

                return conn;
            })
                .As<IConnection>()
                .SingleInstance();

            builder.RegisterType<DSFamilyTempSensorProducer>().As<IDSFamilyTempSensorProducer>();

            // Register anything else you might need...
            //builder.RegisterApiControllers();
            builder.RegisterApiControllers(typeof(DSFamilyTempSensorController).Assembly);

            // Build the container
            var container = builder.Build();

            // OWIN WEB API SETUP:

            // set the DependencyResolver
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            // Register the Autofac middleware FIRST, then the Autofac Web API middleware,
            // and finally the standard Web API middleware.

            app.UseAutofacMiddleware(container);
            app.UseCors(CorsOptions.AllowAll);
            app.UseAutofacWebApi(config);           
            app.UseWebApi(config);

            var connection = container.Resolve<IConnection>();
            
            var properties = new AppProperties(app.Properties);

            if (properties.OnAppDisposing != CancellationToken.None)
            {
                properties.OnAppDisposing.Register(() =>
                {
                    connection.Close(30);
                });
            }
        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
            //Token Consumption
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);
        }
    }
}