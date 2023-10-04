﻿using ParcialApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Acceso_a_datos
{
    interface IDao
    {

        List<Producto> GetProductos();
        bool Save(Factura oFactura);

    }
}
