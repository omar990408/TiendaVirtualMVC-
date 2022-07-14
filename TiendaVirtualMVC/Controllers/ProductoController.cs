using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using TiendaVirtualMVC.Models;

namespace TiendaVirtualMVC.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            try
            {
                using (var db = new TiendaContext())
                {
                    List<Producto> lista = db.Productos.ToList();
                    return View(lista);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryTokenAttribute]
        public ActionResult Agregar(Producto producto)
        {
            try
            {
                string filename = Path.GetFileNameWithoutExtension(producto.ImageFile.FileName);
                string extension = Path.GetExtension(producto.ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                producto.Imagen = "~/img/" + filename;
                filename = Path.Combine(Server.MapPath("~/img/"), filename);
                producto.ImageFile.SaveAs(filename);
                if (!ModelState.IsValid)
                    return View();
                using (var db = new TiendaContext())
                {
                    db.Productos.Add(producto);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Error al ingresar producto - " + e.Message);
                return View();
            }
        }

        public ActionResult Editar(int id)
        {
            try
            {
                using (var db = new TiendaContext())
                {
                    Producto producto = db.Productos.Find(id);
                    return View(producto);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Producto producto)
        {
            string filename = Path.GetFileNameWithoutExtension(producto.ImageFile.FileName);
            string extension = Path.GetExtension(producto.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            producto.Imagen = "~/img/" + filename;
            filename = Path.Combine(Server.MapPath("~/img/"), filename);
            producto.ImageFile.SaveAs(filename);
            try
            {
                if (!ModelState.IsValid)
                    return View();

                using (var db = new TiendaContext())
                {
                    Producto productodb = db.Productos.Find(producto.ProductoID);
                    productodb.NombreProducto = producto.NombreProducto;
                    productodb.Descripcion = producto.Descripcion;
                    productodb.Precio = producto.Precio;
                    productodb.Imagen = producto.Imagen;
                    productodb.Detalles = producto.Detalles;
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
                Producto producto = db.Productos.Find(id);
                return View(producto);
            }
        }

        public ActionResult Eliminar(int id)
        {
            using (var db = new TiendaContext())
            {
                Producto producto = db.Productos.Find(id);
                db.Productos.Remove(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}