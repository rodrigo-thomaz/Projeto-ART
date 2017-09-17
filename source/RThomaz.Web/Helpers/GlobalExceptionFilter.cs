using RollbarSharp;
using System.Web.Mvc;

namespace RThomaz.Web.Helpers
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly RollbarClient _rollbarClient;

        public GlobalExceptionFilter()
        {
            _rollbarClient = new RollbarClient();
        }

        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                return;

            _rollbarClient.SendException(filterContext.Exception);
        }
    }
}