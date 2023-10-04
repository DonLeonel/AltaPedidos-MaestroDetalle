using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Models
{
    internal class Factura
    {
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public int Forma_Pago { get; set; }
        public DateTime Fecha_Baja { get; set; }
        public double Total { get; set; }

        public List<DtlleFactura> lDetalle { get; set; }

        public Factura()
        {
            lDetalle = new List<DtlleFactura>();
        }

        public void AgregarDetalle(DtlleFactura d)
        {
            lDetalle.Add(d);
        }

        public void QuitarDetalle(int n)
        {
            lDetalle.RemoveAt(n);
        }

        public double CalcularTotal()
        {
            double total = 0;
            foreach (DtlleFactura dtll in lDetalle)
            {
                total += dtll.CalcularSubTotal();
            }
            return total;
        }
    }
}
