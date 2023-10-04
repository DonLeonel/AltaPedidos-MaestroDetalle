using ParcialApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Acceso_a_datos
{
    internal class DAO : IDao
    {
        public List<Producto> GetProductos()
        {
            var lst = new List<Producto>();
            lst.Clear();
            var dt = HelperSing.Instance.GetProductos("SP_CONSULTAR_PRODUCTOS");
            foreach (DataRow dr in dt.Rows)
            {
                Producto p = new Producto();
                p.Id = (int)dr[0];
                p.Name = (string)dr[1];
                p.Precio = Convert.ToDouble(dr[2]);
                p.Activo = (string)dr[3];
                lst.Add(p);
            }
            return lst;
        }

        public bool Save(Factura oFactura)
        {
            return HelperSing.Instance.Save("SP_INSERTAR_FACTURA", "SP_INSERTAR_DETALLES", oFactura);
        }
    }
}
