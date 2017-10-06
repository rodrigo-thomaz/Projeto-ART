[assembly: Microsoft.Owin.OwinStartup(typeof(ART.Domotica.WebApi.Startup))]

namespace ART.Domotica.WebApi
{
    using System.Threading;
    using System.Web.Http;

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

            // Make the autofac container
            var builder = new ContainerBuilder();

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