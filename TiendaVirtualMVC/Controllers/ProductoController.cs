using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaVirtualMVC.Models;

namespace TiendaVirtualMVC.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        private TiendaContext db = new TiendaContext();
        public ActionResult Index()
        {
            try
            {
                    List<Producto> lista = db.Productos.ToList();
                    return View(lista);
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
        [ValidateAntiForgeryTokenAttribute]
        public ActionResult Agregar (Producto producto)
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
                {
                    return View();
                }
                else
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

        public ActionResult Editar (int id)
        {
            try
            {
                    Producto producto = db.Productos.Find(id);
                    return View(producto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar (Producto producto)
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

                    Producto productodb = db.Productos.Find(producto.ProductoID);
                    productodb.NombreProducto = producto.NombreProducto;
                    productodb.Descripcion = producto.Descripcion;
                    productodb.Precio = producto.Precio;
                    productodb.Imagen = producto.Imagen;
                    productodb.Detalles = producto.Detalles;
                    db.SaveChanges();
                    return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Detalles(int id)
        {
                Producto producto = db.Productos.Find(id);
                return View(producto);
        }

        public ActionResult Eliminar(int id)
        {
                Producto producto = db.Productos.Find(id);
                db.Productos.Remove(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
        }



    }
}