using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaVirtualMVC.Models;

namespace TiendaVirtualMVC.Controllers
{
    [Authorize]
    public class PedidosItemController : Controller
    {
        // GET: PedidosItem
        public ActionResult Index()
        {
            try
            {
                using(var db = new TiendaContext())
                {
                    if (Session["PedidoId"] == null)
                    {
                        List<PedidosItem> var = db.PedidosItems.Where(a => a.PedidoItemID == -1).ToList();
                        return View(var);
                    }
                    else
                    {
                        var pedidoID = (int)Session["PedidoId"];
                        List<PedidosItem> lista = db.PedidosItems.Where(a => a.PedidoID == pedidoID).ToList();
                        return View(lista);
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult VerAllPedidos()
        {
            using (var db = new TiendaContext())
            {
                List<PedidosItem> lista = db.PedidosItems.ToList();
                return View(lista);
            }
        }

        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(PedidosItem pedidosItem)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                using (var db = new TiendaContext())
                {
                    db.PedidosItems.Add(pedidosItem);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Error al ingresar pedido - item - " + e.Message);
                return View();
            }

        }

        public ActionResult Editar(int id)
        {
            try
            {
                using (var db = new TiendaContext())
                {
                    PedidosItem pedidosItem = db.PedidosItems.Find(id);
                    return View(pedidosItem);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(PedidosItem pedidosItem)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                using (var db = new TiendaContext())
                {
                    PedidosItem pedidosItemdb = db.PedidosItems.Find(pedidosItem.PedidoItemID);
                    pedidosItemdb.PedidoID = pedidosItem.PedidoID;
                    pedidosItemdb.ProductoID = pedidosItem.ProductoID;
                    pedidosItemdb.Cantidad = pedidosItem.Cantidad;
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
                PedidosItem pedidosItem = db.PedidosItems.Find(id);
                return View(pedidosItem);
            }
        }

        public ActionResult Eliminar(int id)
        {
            using (var db = new TiendaContext())
            {
                PedidosItem pedidosItem = db.PedidosItems.Find(id);
                db.PedidosItems.Remove(pedidosItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult ListarProductos()
        {
            try
            {
                using (var db = new TiendaContext())
                {
                    return PartialView(db.Productos.ToList());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult ListarPedidos()
        {
            try
            {
                using (var db = new TiendaContext())
                {
                    return PartialView(db.Pedidos.ToList());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string NombreProducto(int productoID)
        {
            using (var db = new TiendaContext())
            {
                return db.Productos.Find(productoID).NombreProducto;
            }
        }

        public static string ImagenProducto(int productoID)
        {
            using (var db = new TiendaContext())
            {
                return db.Productos.Find(productoID).Imagen;
            }
        }

        public static int NombreRazonSocial(int pedidoID)
        {
            using(var db = new TiendaContext())
            {
                return db.Pedidos.Find(pedidoID).ClienteID.Value;
            }
        }

        public static decimal PrecioProducto(int pedidoID)
        {
            using (var db = new TiendaContext())
            {
                return db.Productos.Find(pedidoID).Precio;
            }
        }

        public ActionResult DetalleTotal()
        {
            using (var db = new TiendaContext())
            {

                Session["PedidoId"] = null;
                return View();

            }
        }

        public ActionResult Cancelar()
        {
            using (var db = new TiendaContext())
            {

                Pedido pedido= db.Pedidos.Find(Session["PedidoId"]);
                db.Pedidos.Remove(pedido);
                db.SaveChanges();
                Session["PedidoId"] = null;
                return RedirectToAction("Index","Home");

            }
        }


    }
}