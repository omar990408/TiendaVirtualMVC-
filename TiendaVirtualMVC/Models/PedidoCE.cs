using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TiendaVirtualMVC.Models
{
    public class PedidoCE
    {
        [Required]
        [Display(Name = "Ingrese el Cliente:")]
        public Nullable<int> ClienteID { get; set; }

        [Required]
        [Display(Name = "Ingrese los detalles:")]
        public string Detalles { get; set; }
        [Required]
        [Display(Name = "Ingrese el proveedor:")]
        public int codigo_proveedor { get; set; }
        [Required]
        [Display(Name = "Ingrese el stock:")]
        public int stock { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
    }
    [MetadataType(typeof(PedidoCE))]
    public partial class Pedido
    {

    }
}