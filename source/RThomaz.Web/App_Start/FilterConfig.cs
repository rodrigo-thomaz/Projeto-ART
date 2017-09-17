using System.Web.Mvc;

namespace RThomaz.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
#if (!DEBUG)            
            filters.Add(new Helpers.GlobalExceptionFilter());
#endif
        }
    }
}