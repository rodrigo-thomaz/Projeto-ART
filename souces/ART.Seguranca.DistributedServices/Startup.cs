[assembly: Microsoft.Owin.OwinStartup(typeof(ART.Seguranca.DistributedServices.Startup))]

namespace ART.Seguranca.DistributedServices
{
    using System;
    using System.Threading;
    using System.Web.Http;

    using ART.Seguranca.DistributedServices.Controllers;
    using ART.Seguranca.DistributedServices.Providers;
    using ART.Seguranca.Domain;
    using ART.Seguranca.Repository;

    using Autofac;
    using Autofac.Integration.WebApi;

    using Microsoft.Owin;
    using Microsoft.Owin.BuilderProperties;
    using Microsoft.Owin.Security.Facebook;
    using Microsoft.Owin.Security.Google;
    using Microsoft.Owin.Security.OAuth;

    using Owin;

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

            ConfigureOAuth(app);

            WebApiConfig.Register(config);

            AutoMapperConfig.RegisterMappings();

            // Make the autofac container
            var builder = new ContainerBuilder();

            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<DomainModule>();

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
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);

            var properties = new AppProperties(app.Properties);

            if (properties.OnAppDisposing != CancellationToken.None)
            {
                properties.OnAppDisposing.Register(() =>
                {
                    //connection.Close(30);
                });
            }
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            //use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions() {

                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new SimpleAuthorizationServerProvider(),
                RefreshTokenProvider = new SimpleRefreshTokenProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);

            //Configure Google External Login
            googleAuthOptions = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "xxxxxx",
                ClientSecret = "xxxxxx",
                Provider = new GoogleAuthProvider()
            };
            app.UseGoogleAuthentication(googleAuthOptions);

            //Configure Facebook External Login
            facebookAuthOptions = new FacebookAuthenticationOptions()
            {
                AppId = "xxxxxx",
                AppSecret = "xxxxxx",
                Provider = new FacebookAuthProvider()
            };
            app.UseFacebookAuthentication(facebookAuthOptions);
        }

        #endregion Methods
    }
}