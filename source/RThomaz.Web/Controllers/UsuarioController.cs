using RThomaz.Web.Helpers;
using System.Web.Mvc;

namespace RThomaz.Web.Controllers
{
    public class UsuarioController : AuthenticatedController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(long? id)
        {
            return View();
        }
    }
}