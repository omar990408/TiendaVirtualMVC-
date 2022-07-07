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

    }

    [MetadataType(typeof(PedidoCE))]
    public partial class Pedido
    {

    }
}