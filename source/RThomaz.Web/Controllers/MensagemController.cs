using RThomaz.Web.Helpers;
using System.Web.Mvc;

namespace RThomaz.Web.Controllers
{
    public class MensagemController : AuthenticatedController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Inbox()
        {
            return View();
        }

        public ActionResult Compose()
        {
            return View();
        }

        public ActionResult Reply(long? id)
        {
            return View();
        }

        public ActionResult ViewMessage()
        {
            return View();
        }
    }
}