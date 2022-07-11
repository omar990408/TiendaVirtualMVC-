using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaVirtualMVC.Controllers;
using TiendaVirtualMVC.Models;

namespace TiendaVirtualMVC.Filters
{

    public class Filtro_VERIFICA_SESSION : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var objUser = (ApplicationUser)HttpContext.Current.Session["User"];
            if (objUser == null)
            {
                if (filterContext.Controller is AccountController == false)
                {
                    filterContext.HttpContext.Response.Redirect("~/Account/Login", false);
                }
            }
            else
            {
                if (filterContext.Controller is AccountController == true)
                {
                    filterContext.HttpContext.Response.Redirect("~/Home/Index");
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}   
