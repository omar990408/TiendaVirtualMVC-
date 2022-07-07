using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaVirtualMVC.Models;

namespace TiendaVirtualMVC.Controllers
{
    public class PedidoController : Controller
    {
        // GET: Pedido
        public ActionResult Index()
        {
            try
            {
                TiendaContext db = new TiendaContext();
                List<Pedido> lista = db.Pedidos.ToList();
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
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(Pedido pedido)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();
                using (var db = new TiendaContext())
                {
                    pedido.FechaPedido = DateTime.Now;
                    db.Pedidos.Add(pedido);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {

                ModelState.AddModelError("", "Error al agregar pedido - " + e.Message);
                return View();
            }
        }

        public ActionResult Editar(int id)
        {
            try
            {
                using (var db = new TiendaContext())
                {
                    Pedido pedido = db.Pedidos.Find(id);
                    return View(pedido);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar (Pedido pedido)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                using(var db = new TiendaContext())
                {
                    Pedido pedidoDb = db.Pedidos.Find(pedido.PedidoID);
                    pedidoDb.ClienteID = pedido.ClienteID;
                    pedidoDb.FechaPedido = pedido.FechaPedido;
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
            try
            {
                using(var db = new TiendaContext())
                {
                    Pedido pedido = db.Pedidos.Find(id);
                    return View(pedido);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Eliminar(int id)
        {
            using(var db = new TiendaContext())
            {
                Pedido pedido = db.Pedidos.Find(id);
                db.Pedidos.Remove(pedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult ListarClientes()
        {
            try
            {
                using(var db = new TiendaContext())
                {
                    return PartialView(db.Clientes.ToList());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string NombreCliente(int? clienteID)
        {
            using (var db = new TiendaContext())
            {
                return db.Clientes.Find(clienteID).RazonSocial;
            }
        }
    }
}
