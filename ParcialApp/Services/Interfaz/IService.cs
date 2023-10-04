using ParcialApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Services.Interfaz
{
    internal interface IService
    {
        List<Producto> GetProductos();
        bool Save(Factura oFactura);
    }
}
