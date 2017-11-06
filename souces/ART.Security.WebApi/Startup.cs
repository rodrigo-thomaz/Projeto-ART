[assembly: Microsoft.Owin.OwinStartup(typeof(ART.Security.WebApi.Startup))]

namespace ART.Security.WebApi
{
    using System;
    using System.Threading;
    using System.Web.Http;

    using ART.Infra.CrossCutting.MQ;
    using ART.Infra.CrossCutting.Setting;
    using ART.Security.Domain;
    using ART.Security.Producer;
    using ART.Security.Repository;
    using ART.Security.WebApi.Controllers;
    using ART.Security.WebApi.Providers;

    using Autofac;
    using Autofac.Integration.WebApi;

    using Microsoft.Owin;
    using Microsoft.Owin.BuilderProperties;
    using Microsoft.Owin.Security.Facebook;
    using Microsoft.Owin.Security.Google;
    using Microsoft.Owin.Security.OAuth;

    using Owin;

    using RabbitMQ.Client;

    public class Startup
    {
        #region Properties

        public static FacebookAuthenticationOptions facebookAuthOptions
        {
            get; private set;
        }

        public static GoogleOAuth2AuthenticationOptions googleAuthOptions
        {
            get; private set;
        }

        public static OAuthBearerAuthenticationOptions OAuthBearerOptions
        {
            get; private set;
        }

        #endregion Properties

        #region Methods

        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            //ConfigureOAuth(app);

            WebApiConfig.Register(config);

            AutoMapperConfig.RegisterMappings();

            // Make the autofac container
            var builder = new ContainerBuilder();

            builder.RegisterType<AuthContext>().InstancePerDependency();

            builder.RegisterModule<SettingModule>();
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<DomainModule>();
            builder.RegisterModule<MQModule>();
            builder.RegisterModule<ProducerModule>();

            // Register Providers
            builder.RegisterType<FacebookAuthProvider>();
            builder.RegisterType<GoogleAuthProvider>();
            builder.RegisterType<SimpleAuthorizationServerProvider>();
            builder.RegisterType<SimpleRefreshTokenProvider>();

            // Register anything else you might need...
            //builder.RegisterApiControllers();
            builder.RegisterApiControllers(typeof(AccountController).Assembly);
            builder.RegisterApiControllers(typeof(OrdersController).Assembly);
            builder.RegisterApiControllers(typeof(RefreshTokensController).Assembly);

            // Build the container
            var container = builder.Build();

            // OWIN WEB API SETUP:

            // set the DependencyResolver
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            // Register the Autofac middleware FIRST, then the Autofac Web API middleware,
            // and finally the standard Web API middleware.

            app.UseAutofacMiddleware(container);
            ConfigureOAuth(app, container);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
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

        public void ConfigureOAuth(IAppBuilder app, IContainer container)
        {
            //use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            var facebookAuthProvider = container.Resolve<FacebookAuthProvider>();
            var googleAuthProvider = container.Resolve<GoogleAuthProvider>();
            var simpleAuthorizationServerProvider = container.Resolve<SimpleAuthorizationServerProvider>();
            var simpleRefreshTokenProvider = container.Resolve<SimpleRefreshTokenProvider>();

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions() {

                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = simpleAuthorizationServerProvider,
                RefreshTokenProvider = simpleRefreshTokenProvider
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);

            //Configure Google External Login
            googleAuthOptions = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "xxxxxx",
                ClientSecret = "xxxxxx",
                Provider = googleAuthProvider
            };
            app.UseGoogleAuthentication(googleAuthOptions);

            //Configure Facebook External Login
            facebookAuthOptions = new FacebookAuthenticationOptions()
            {
                AppId = "xxxxxx",
                AppSecret = "xxxxxx",
                Provider = facebookAuthProvider
            };
            app.UseFacebookAuthentication(facebookAuthOptions);
        }

        #endregion Methods
    }
}