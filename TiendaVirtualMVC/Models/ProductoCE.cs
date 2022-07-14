﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TiendaVirtualMVC.Models
{
    public class ProductoCE
    {
        [Required]
        [Display(Name = "Ingrese el nombre del producto:")]
        public string NombreProducto { get; set; }
        [Required]
        [Display(Name = "Ingrese la descripción:")]
        public string Descripcion { get; set; }
        [Required]
        [Display(Name = "Ingrese el precio:")]
        public decimal Precio { get; set; }
        [Required]
        [Display(Name = "Ingrese la imagen:")]
        public string Imagen { get; set; }
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
    [MetadataType(typeof(ProductoCE))]
    public partial class Producto
    {

    }
}