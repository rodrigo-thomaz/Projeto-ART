using RThomaz.Web.Helpers;
using System.Web.Mvc;

namespace RThomaz.Web.Controllers
{
    public class ContaController : AuthenticatedController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Summary()
        {
            return View();
        }

        public ActionResult ContaEspecieDetail(long? id)
        {
            return View();
        }

        public ActionResult ContaCorrenteDetail(long? id)
        {
            return View();
        }

        public ActionResult ContaPoupancaDetail(long? id)
        {
            return View();
        }

        public ActionResult ContaCartaoCreditoDetail(long? id)
        {
            return View();
        }
    }
}