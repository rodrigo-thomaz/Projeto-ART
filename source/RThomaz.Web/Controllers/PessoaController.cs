using RThomaz.Web.Helpers;
using System.Web.Mvc;

namespace RThomaz.Web.Controllers
{
    public class PessoaController : AuthenticatedController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PessoaFisicaDetail(long id)
        {
            return View();
        }

        public ActionResult PessoaJuridicaDetail(long id)
        {
            return View();
        }
    }
}