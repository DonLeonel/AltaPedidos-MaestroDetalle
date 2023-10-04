using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Models
{
    internal class DtlleFactura
    {
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }

        public DtlleFactura(Producto producto, int cant)
        {           
            this.Producto = producto;
            this.Cantidad = cant;
        }

        public double CalcularSubTotal()
        {
            return Cantidad * Producto.Precio;
        }
    }
}
