[assembly: Microsoft.Owin.OwinStartup(typeof(ART.Domotica.WebApi.Startup))]
//[assembly: log4net.Config.XmlConfigurator(ConfigFileExtension ="config",ConfigFile = "log4net.webapi.config", Watch = true)]
[assembly: log4net.Config.XmlConfigurator()]
namespace ART.Domotica.WebApi
{
    using System.Threading;
    using System.Web.Http;

    using ART.Domotica.Producer;
    using ART.Domotica.WebApi.App_Start;
    using ART.Domotica.WebApi.Modules;
    using ART.Infra.CrossCutting.MQ;

    using Autofac;
    using Autofac.Integration.WebApi;

    using Microsoft.Owin.BuilderProperties;
    using Microsoft.Owin.Cors;
    using Microsoft.Owin.Security.OAuth;

    using Owin;

    using RabbitMQ.Client;
    using ART.Infra.CrossCutting.Logging;
    using System.IO;
    using System.Configuration;
    using System;
    using System.Web;

    public class Startup
    {
        #region Properties

        public static OAuthBearerAuthenticationOptions OAuthBearerOptions
        {
            get; private set;
        }

        #endregion Properties

        #region Methods

        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            ConfigureOAuth(app);

            WebApiConfig.Register(config);
            
            // work arround rodrigo (não estava funcionando)
            var log4netConfig = ConfigurationManager.AppSettings["log4net.Config"];
            var log4netConfigWatch = Convert.ToBoolean(ConfigurationManager.AppSettings["log4net.Config.Watch"]);
            var log4netConfigFullName = Path.Combine(HttpRuntime.BinDirectory, log4netConfig);
            var log4netConfigFileInfo = new FileInfo(log4netConfigFullName);
            if (log4netConfigWatch)
            {                
                log4net.Config.XmlConfigurator.ConfigureAndWatch(log4netConfigFileInfo);
            }
            else
                log4net.Config.XmlConfigurator.Configure(log4netConfigFileInfo);
            // work arround rodrigo


            // Make the autofac container
            var builder = new ContainerBuilder();

            builder.RegisterModule<LoggingModule>();
            builder.RegisterModule<MQModule>();
            builder.RegisterModule<ProducerModule>();
            builder.RegisterModule<ControllerModule>();

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
                    log4net.LogManager.Shutdown();
                });
            }
        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
            //Token Consumption
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);
        }

        #endregion Methods
    }
}