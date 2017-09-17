using RThomaz.Web.Helpers;
using System.Web.Mvc;

namespace RThomaz.Web.Controllers
{
    public class TransferenciaController : AuthenticatedController
    {
        public ActionResult Detail(long? id)
        {
            return View();
        }
    }
}