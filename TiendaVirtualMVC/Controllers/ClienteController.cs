using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaVirtualMVC.Models;

namespace TiendaVirtualMVC.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {

            try
            {
                using (var db = new TiendaContext())
                {
                    List<Cliente> lista = db.Clientes.ToList();
                    return View(lista);
                }
            }
            catch (Exception)
            {

                throw;
            }
            //TiendaContext db = new TiendaContext();
            //db.Clientes.Where(item => item.Ciudad == "Quito").ToList();
            //db.Clientes.ToList();
            // Linq para hacer una consulta a la bdd en base a un criterio
            //List<Cliente> lista = db.Clientes.Where(item => item.Ciudad == "Quito").ToList();
            //List<Cliente> lista = db.Clientes.ToList();
            //return View(lista);
        }

        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(Cliente cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                using (var db = new TiendaContext())
                {
                    db.Clientes.Add(cliente);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Error al ingresar cliente - " + e.Message);
                return View();
            }

        }

        public ActionResult Editar(int id)
        {
            try
            {
                using (var db = new TiendaContext())
                {
                    Cliente cliente = db.Clientes.Find(id);
                    return View(cliente);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Cliente cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                using (var db = new TiendaContext())
                {
                    Cliente clidb = db.Clientes.Find(cliente.ClienteID);
                    clidb.RazonSocial = cliente.RazonSocial;
                    clidb.Direccion = cliente.Direccion;
                    clidb.Ciudad = cliente.Ciudad;
                    clidb.Estado = cliente.Estado;
                    clidb.CodigoPostal = cliente.CodigoPostal;
                    clidb.Rif = cliente.Rif;
                    clidb.Pais = cliente.Pais;
                    clidb.Telefonos = cliente.Telefonos;
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
                Cliente cliente = db.Clientes.Find(id);
                return View(cliente);
            }
        }

        public ActionResult Eliminar(int id)
        {
            using (var db = new TiendaContext())
            {
                Cliente cliente = db.Clientes.Find(id);
                db.Clientes.Remove(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}