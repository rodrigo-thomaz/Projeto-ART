using ART.DistributedServices.App_Start;
using ART.DistributedServices.Controllers;
using ART.Infra.CrossCutting.WebApi;
using Autofac;
using Autofac.Integration.WebApi;
using MassTransit;
using MassTransit.Util;
using Microsoft.Owin;
using Microsoft.Owin.BuilderProperties;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Configuration;
using System.Threading;
using System.Web.Http;

[assembly: OwinStartup(typeof(ART.DistributedServices.Startup))]
namespace ART.DistributedServices
{
    public class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            ConfigureOAuth(app);

            WebApiConfig.Register(config);

            // Make the autofac container
            var builder = new ContainerBuilder();

            // Register your Bus
            builder.Register(c =>
            {
                return Bus.Factory.CreateUsingRabbitMq(rabbit =>
                    rabbit.Host("file-server", "/", settings =>
                    {
                        var username = ConfigurationManager.AppSettings["RabbitMQUsername"];
                        var password = ConfigurationManager.AppSettings["RabbitMQPassword"];

                        //settings.Heartbeat(2000);
                        settings.Username(username);
                        settings.Password(password);
                    })
                );
            })
                .As<IBusControl>()
                .As<IPublishEndpoint>()
                .SingleInstance();

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

            // Starts Mass Transit Service bus, and registers stopping of bus on app dispose
            var bus = container.Resolve<IBusControl>();
            var busHandle = TaskUtil.Await(() => bus.StartAsync());

            var properties = new AppProperties(app.Properties);

            if (properties.OnAppDisposing != CancellationToken.None)
            {
                properties.OnAppDisposing.Register(() => busHandle.Stop(TimeSpan.FromSeconds(30)));
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