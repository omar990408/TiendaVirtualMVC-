using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaVirtualMVC.Models;

namespace TiendaVirtualMVC.Controllers
{
    [Authorize]
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            try
            {
                using(var db = new TiendaContext())
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

        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar (Producto producto)
        {
            try
            {
                //if (!ModelState.IsValid)
                //    return View();

                string fileName = Path.GetFileNameWithoutExtension(producto.ImageFile.FileName);
                string extension = Path.GetExtension(producto.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                producto.Imagen = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                producto.ImageFile.SaveAs(fileName);

                using(var db = new TiendaContext())
                {
                    db.Productos.Add(producto);
                    db.SaveChanges();
                    ModelState.Clear();
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
                using(var db = new TiendaContext())
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
        public ActionResult Editar (Producto producto)
        {

            try
            {

                string fileName = Path.GetFileNameWithoutExtension(producto.ImageFile.FileName);
                string extension = Path.GetExtension(producto.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                producto.Imagen = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                producto.ImageFile.SaveAs(fileName);

                using (var db = new TiendaContext())
                {
                    Producto productodb = db.Productos.Find(producto.ProductoID);
                    productodb.NombreProducto = producto.NombreProducto;
                    productodb.Descripcion = producto.Descripcion;
                    productodb.Precio = producto.Precio;
                    productodb.Imagen = producto.Imagen;
                    productodb.Detalles = producto.Detalles;
                    productodb.codigo_proveedor = producto.codigo_proveedor;
                    productodb.stock = producto.stock;
                    ModelState.Clear();
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
            using(var db = new TiendaContext())
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

        public ActionResult ListarProveedor()
        {
            try
            {
                using (var db = new TiendaContext())
                {
                    return PartialView(db.Proveedors.ToList());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string NombreProveedor(int? proveedorID)
        {
            using (var db = new TiendaContext())
            {
                return db.Proveedors.Find(proveedorID).nombre_proveedor;
            }
        }



    }
}