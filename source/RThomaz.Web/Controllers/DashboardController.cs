using RThomaz.Web.Helpers;
using System.Web.Mvc;

namespace RThomaz.Web.Controllers
{
    public class DashboardController : AuthenticatedController
    {
        public ActionResult Index()
        {   
            return View();
        }
    }    
}