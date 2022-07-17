using System.Web.Mvc;

namespace TiendaVirtualMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User?.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Welcome");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Welcome()
        {
            return View();
        }
    }
}