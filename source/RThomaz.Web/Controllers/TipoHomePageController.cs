using RThomaz.Domain.Financeiro.Enums;
using RThomaz.Web.Helpers;
using System;
using System.Web.Mvc;

namespace RThomaz.Web.Controllers
{
    public class TipoHomePageController : AuthenticatedController
    {
        public ActionResult Index(TipoPessoa tipoPessoa)
        {
            return View();
        }

        public ActionResult Detail(Guid? id, TipoPessoa tipo)
        {
            return View();
        }
    }
}