using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaVirtualMVC.Models;

namespace TiendaVirtualMVC.Controllers
{
    public class TiendaController : Controller
    {
        private TiendaContext db = new TiendaContext();

        // GET: Tienda
        public ActionResult Index()
        {
            return View();
        }

      //  private ActionResult Index()
      //  {
      //      var UserList = (from a in db.AspNetUsers
      //                      where a.user == GlobalVariables.Usuario
      //                      select a).Firsto
      //  }
    }
}