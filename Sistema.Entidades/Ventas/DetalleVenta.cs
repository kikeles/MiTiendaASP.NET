using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;
using Sistema.Entidades.Almacen;

namespace Sistema.Entidades.Ventas
{
    public class DetalleVenta
    {
        public int iddetalle_venta { get; set; }
        [Required]
        public int idventa { get; set; }
        [Required]
        public int idarticulo { get; set; }
        [Required]
        public int cantidad { get; set; }
        [Required]
        public decimal precio { get; set; }
        [Required]
        public decimal descuento { get; set; }

        //Relacion
        public Venta venta { get; set; }
        public Articulo articulo { get; set; }
    }
}
