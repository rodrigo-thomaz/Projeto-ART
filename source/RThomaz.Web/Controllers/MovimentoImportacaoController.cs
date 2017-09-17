using RThomaz.Web.Helpers;
using System.Web.Mvc;

namespace RThomaz.Web.Controllers
{
    public class MovimentoImportacaoController : AuthenticatedController
    {
        public ActionResult Index(long? id)
        {
            return View();
        }
    }
}