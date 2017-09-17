using RThomaz.Web.Helpers;
using System.Web.Mvc;

namespace RThomaz.Web.Controllers
{
    public class PerfilController : AuthenticatedController
    {
        public ActionResult Index()
        {
            ViewBag.Id = UsuarioId;
            ViewBag.AvatarStorageObject = AvatarStorageObject;
            return View();
        }

        public ActionResult Account()
        {
            ViewBag.Id = UsuarioId;
            ViewBag.AvatarStorageObject = AvatarStorageObject;
            return View();
        }

        public ActionResult Help()
        {
            ViewBag.Id = UsuarioId;
            ViewBag.AvatarStorageObject = AvatarStorageObject;
            return View();
        }
    }
}