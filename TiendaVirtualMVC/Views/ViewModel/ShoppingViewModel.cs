using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiTiendaMVC5Model.ViewModel
{
    public class ShoppingViewModel
    {
        public int ProductoID { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Imagen { get; set; }
        public string Detalles { get; set; }
        public int codigo_proveedor { get; set; }
        public int stock { get; set; }
        
    }
}