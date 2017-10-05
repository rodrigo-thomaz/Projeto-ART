[assembly: Microsoft.Owin.OwinStartup(typeof(ART.Corporativo.WebApi.Startup))]

namespace ART.Corporativo.WebApi
{
    using System.Web.Http;

    using ART.Corporativo.WebApi.App_Start;

    using Microsoft.Owin.Security.OAuth;

    using Owin;

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
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
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