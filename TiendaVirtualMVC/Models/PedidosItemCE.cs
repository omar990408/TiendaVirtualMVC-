using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TiendaVirtualMVC.Models
{
    public class PedidosItemCE
    {
        [Required]
        [Display(Name = "Ingrese el Pedido")]
        public int PedidoID { get; set; }
        [Required]
        [Display(Name = "Ingrese el Producto")]
        public int ProductoID { get; set; }
        [Required]
        [Display(Name = "Ingrese la Cantidad")]
        public int Cantidad { get; set; }

        public virtual Pedido Pedido { get; set; }
        public virtual Producto Producto { get; set; }

    }

    [MetadataType(typeof(PedidosItemCE))]
    public partial class PedidosItem
    {
        
    }
}