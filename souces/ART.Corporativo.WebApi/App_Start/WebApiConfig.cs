namespace ART.Corporativo.WebApi.App_Start
{
    using System.Linq;
    using System.Net.Http.Formatting;
    using System.Web.Http;

    using Newtonsoft.Json.Serialization;

    public static class WebApiConfig
    {
        #region Methods

        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        #endregion Methods
    }
}