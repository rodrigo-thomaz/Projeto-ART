namespace ART.Domotica.WebApi.App_Start
{
    using System.Linq;
    using System.Net.Http.Formatting;
    using System.Web.Http;
    using System.Web.Http.ExceptionHandling;
    using System.Web.Http.Routing;

    using ART.Infra.CrossCutting.WebApi;
    using ART.Infra.CrossCutting.WebApi.RouteConstraints;

    using Newtonsoft.Json.Serialization;

    public static class WebApiConfig
    {
        #region Methods

        public static void Register(HttpConfiguration config)
        {
            var constraintResolver = new DefaultInlineConstraintResolver();

            constraintResolver.ConstraintMap.Add("custom_min", typeof(CustomMinRouteConstraint));
            constraintResolver.ConstraintMap.Add("custom_max", typeof(CustomMaxRouteConstraint));
            constraintResolver.ConstraintMap.Add("custom_range", typeof(CustomRangeRouteConstraint));
            constraintResolver.ConstraintMap.Add("custom_length", typeof(CustomLengthRouteConstraint));

            config.MapHttpAttributeRoutes(constraintResolver);

            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new JavaScriptDateTimeConverter());
        }

        #endregion Methods
    }
}