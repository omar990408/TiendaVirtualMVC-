using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TiendaVirtualMVC.Models
{
    public class ClienteCE
    {
        [Required]
        [Display(Name = "Ingrese Razon Social:")]
        public string RazonSocial { get; set; }
        [Required]
        [Display(Name = "Ingrese la Direccion:")]
        public string Direccion { get; set; }
        [Required]
        [Display(Name = "Ingrese la Ciudad:")]
        public string Ciudad { get; set; }
        [Required]
        [Display(Name = "Ingrese la provincia:")]
        public string Estado { get; set; }
        [Required]
        [Display(Name = "Ingrese Codigo Postal:")]
        public string CodigoPostal { get; set; }
        [Required]
        [Display(Name = "Ingrese Rif:")]
        public string Rif { get; set; }
        [Required]
        [Display(Name = "Ingrese el Pais:")]
        public string Pais { get; set; }
        [Required]
        [Display(Name = "Ingrese el telefono:")]
        public string Telefonos { get; set; }
    }

    [MetadataType(typeof(ClienteCE))]
    public partial class Cliente
    {
        public string Pais_CodPostal { get { return this.Convertir(); } }

        public string Estado_CodPostal { get { return Estado + " " + CodigoPostal; } }

        //Enunciado
        // Pais: Ecuador
        // Codigo Postal: 171503
        // RESULTADO = EC171503
        public string Convertir()
        {
            var pais = this.Pais.Substring(0, 2);
            pais = pais.ToUpper();
            var codigoPostal = this.CodigoPostal;
            return (pais + "" + codigoPostal);
        }
    }

}