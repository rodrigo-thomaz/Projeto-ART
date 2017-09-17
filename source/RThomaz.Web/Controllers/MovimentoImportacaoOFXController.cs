using RThomaz.Web.Helpers;
using System.Web.Mvc;

namespace RThomaz.Web.Controllers
{
    public class MovimentoImportacaoOFXController : AuthenticatedController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}