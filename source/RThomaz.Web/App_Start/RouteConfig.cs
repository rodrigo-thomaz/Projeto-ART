using System.Web.Mvc;
using System.Web.Routing;

namespace RThomaz.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "DefaultMvc",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "DefaultTipo",
                url: "{controller}/{action}/{id}/{tipo}",
                defaults: new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional, tipo = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "DefaultPlanoConta",
               url: "{controller}/{action}/{id}/{tipo}/{parentId}",
               defaults: new { controller = "PlanoConta", action = "Index", id = UrlParameter.Optional, tipo = UrlParameter.Optional, parentId = UrlParameter.Optional }
           );
        }
    }
}
