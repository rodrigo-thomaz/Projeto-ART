using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace RThomaz.Web.Helpers
{
    public class ThrowingAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (!((HttpContext.Current.User).Identity).IsAuthenticated)
            {
                var message = string.Format("Access denied in the controller '{0}', action '{1}'", actionContext.ActionDescriptor.ControllerDescriptor.ControllerName, actionContext.ActionDescriptor.ActionName);
                throw new System.UnauthorizedAccessException(message);
            }
            else
            {
                base.HandleUnauthorizedRequest(actionContext);
            }
        }
    }
}