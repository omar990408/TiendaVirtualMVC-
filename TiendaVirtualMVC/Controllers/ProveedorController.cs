using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaVirtualMVC.Models;

namespace TiendaVirtualMVC.Controllers
{
    [Authorize]
    public class ProveedorController : Controller
    {
        // GET: Proveedor
        public ActionResult Index()
        {
            try
            {
                using (var db = new TiendaContext())
                {
                    List<Proveedor> lista = db.Proveedors.ToList();
                    return View(lista);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(Proveedor proveedor)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                using (var db = new TiendaContext())
                {
                    db.Proveedors.Add(proveedor);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Error al ingresar proveedor - " + e.Message);
                return View();
            }

        }

        public ActionResult Editar(int id)
        {
            try
            {
                using (var db = new TiendaContext())
                {
                    Proveedor proveedor = db.Proveedors.Find(id);
                    return View(proveedor);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Proveedor proveedor)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                using (var db = new TiendaContext())
                {
                    Proveedor provdb = db.Proveedors.Find(proveedor.codigo_proveedor);
                    provdb.nombre_proveedor = proveedor.nombre_proveedor;
                    provdb.ciudad = proveedor.ciudad;
                    provdb.estado = proveedor.estado;
                    provdb.Email = proveedor.Email;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Detalles(int id)
        {
            using (var db = new TiendaContext())
            {
                Proveedor proveedor = db.Proveedors.Find(id);
                return View(proveedor);
            }
        }

        public ActionResult Eliminar(int id)
        {
            using (var db = new TiendaContext())
            {
                Proveedor proveedor = db.Proveedors.Find(id);
                db.Proveedors.Remove(proveedor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}