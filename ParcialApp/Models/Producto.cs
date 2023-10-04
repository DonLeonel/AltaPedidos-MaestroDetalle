using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Models
{
    internal class Producto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Precio { get; set; }
        public string Activo { get; set; }

        public Producto(int id, string name, double precio)
        {
            Id = id;
            Name = name;
            Precio = precio;
        }
        public Producto()
        {

        }
    }
}
