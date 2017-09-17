using RThomaz.Web.Helpers;
using System.Web.Mvc;

namespace RThomaz.Web.Controllers
{    
    public class CentroCustoController : AuthenticatedController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(long? id, long? tipo)
        {
            return View();
        }
    }
}