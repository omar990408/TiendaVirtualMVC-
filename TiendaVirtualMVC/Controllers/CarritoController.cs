using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaVirtualMVC.Models;
using TiendaVirtualMVC.Controllers;
using MiTiendaMVC5Model.ViewModel;

namespace WebAppECartDemo.Controllers
{
    public class ShoppingController : Controller
    {
        private TiendaContext objECartDbEntities;
        private List<ShoppingCartModel> listOfShoppingCartModels;
        public ShoppingController()
        {
            objECartDbEntities = new TiendaContext();
            listOfShoppingCartModels = new List<ShoppingCartModel>();
        }
        // GET: Shopping
        public ActionResult Index()
        {
            IEnumerable<ShoppingViewModel> listOfShoppingViewModels = (from objItem in objECartDbEntities.Productos
                    select new ShoppingViewModel()
                    {
                        Imagen = objItem.Imagen,
                        NombreProducto = objItem.NombreProducto,
                        Descripcion = objItem.Descripcion,
                        Precio = objItem.Precio,
                        ProductoID = objItem.ProductoID,
                    }

                ).ToList();
            return View(listOfShoppingViewModels);
        }

        [HttpPost]
        public JsonResult Index(string ItemId)
        {
            ShoppingCartModel objShoppingCartModel = new ShoppingCartModel();
            Producto objItem = objECartDbEntities.Productos.Single(model => model.ProductoID.ToString() == ItemId);
            if (Session["CartCounter"] != null)
            {
                listOfShoppingCartModels = Session["CartItem"] as List<ShoppingCartModel>;
            }
            if (listOfShoppingCartModels.Any(model => model.ProductoID.ToString() == ItemId))
            {
                objShoppingCartModel = listOfShoppingCartModels.Single(model => model.ProductoID.ToString() == ItemId);
                objShoppingCartModel.Quantity = objShoppingCartModel.Quantity + 1;
                objShoppingCartModel.Total = objShoppingCartModel.Quantity * objShoppingCartModel.UnitPrice;
            }
            else
            {

                objShoppingCartModel.ProductoID = (ItemId).ToString();
                objShoppingCartModel.Imagen = objItem.Imagen;
                objShoppingCartModel.NombreProducto = objItem.NombreProducto;
                objShoppingCartModel.Quantity = 1;
                objShoppingCartModel.Total = objItem.ItemPrice;
                objShoppingCartModel.UnitPrice = objItem.ItemPrice;
                listOfShoppingCartModels.Add(objShoppingCartModel);
            }

            Session["CartCounter"] = listOfShoppingCartModels.Count;
            Session["CartItem"] = listOfShoppingCartModels;
            return Json(new {Success = true, Counter = listOfShoppingCartModels .Count}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShoppingCart()
        {
            listOfShoppingCartModels = Session["CartItem"] as List<ShoppingCartModel>;
            return View(listOfShoppingCartModels);
        }

        [HttpPost]
        public ActionResult AddOrder()
        {
            int OrderId = 0;
            listOfShoppingCartModels = Session["CartItem"] as List<ShoppingCartModel>;
            Pedido orderObj = new Pedido()
            {
                FechaPedido = DateTime.Now,
            };
            objECartDbEntities.Pedidos.Add(orderObj);
            objECartDbEntities.SaveChanges();
            OrderId = orderObj.PedidoID;

            foreach (var item in listOfShoppingCartModels)
            {
                PedidosItem objOrderDetail = new PedidosItem();
                objOrderDetail.ProductoID = item.ProductoID;
                objOrderDetail.

                objECartDbEntities.Pedidos.Add(objOrderDetail);
                objECartDbEntities.SaveChanges();

            }

            Session["CartItem"] = null;
            Session["CartCounter"] = null;
            return RedirectToAction("Index");
        }
    }
}
