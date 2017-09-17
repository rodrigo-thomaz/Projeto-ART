using RThomaz.Domain.Financeiro.Enums;
using RThomaz.Web.Helpers;
using System.Web.Mvc;

namespace RThomaz.Web.Controllers
{
    public class LancamentoProgramadoController : AuthenticatedController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(long id, TipoTransacao tipo)
        {
            return View();
        }        
    }
}