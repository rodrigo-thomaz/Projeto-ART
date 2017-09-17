using RThomaz.Web.Helpers;
using System.Web.Mvc;

namespace RThomaz.Web.Controllers
{
    public class SegurancaController : AuthenticatedController
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            return View();
        }
    }
}