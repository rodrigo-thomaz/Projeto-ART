using Newtonsoft.Json.Serialization;
using RThomaz.DistributedServices.Core.RouteConstraints;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Routing;

namespace RThomaz.DistributedServices.Identity
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var constraintResolver = new DefaultInlineConstraintResolver();

            constraintResolver.ConstraintMap.Add("custom_min", typeof(CustomMinRouteConstraint));
            constraintResolver.ConstraintMap.Add("custom_max", typeof(CustomMaxRouteConstraint));
            constraintResolver.ConstraintMap.Add("custom_range", typeof(CustomRangeRouteConstraint));
            constraintResolver.ConstraintMap.Add("custom_length", typeof(CustomLengthRouteConstraint));

            config.MapHttpAttributeRoutes(constraintResolver);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
