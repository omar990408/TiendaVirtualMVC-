using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TiendaVirtualMVC.Models
{
    public class ProveedorCE
    {
        [Required]
        [Display(Name = "Ingrese Nombre del Proveedor:")]
        public string nombre_proveedor { get; set; }
        [Required]
        [Display(Name = "Ingrese la Ciudad:")]
        public string ciudad { get; set; }
        [Required]
        [Display(Name = "Ingrese el estado:")]
        public bool estado { get; set; }
        [Required]
        [Display(Name = "Ingrese el mail:")]
        public string Email { get; set; }
    }
    [MetadataType(typeof(ProveedorCE))]
    public partial class Proveedor
    {

    }
}