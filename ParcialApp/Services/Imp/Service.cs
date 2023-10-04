using ParcialApp.Acceso_a_datos;
using ParcialApp.Models;
using ParcialApp.Services.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Services.Imp
{
    internal class Service : IService
    {
        private IDao oDAO;

        public Service()
        {
            oDAO = new DAO();
        }

        public List<Producto> GetProductos()
        {
            return oDAO.GetProductos();
        }

        public bool Save(Factura oFactura)
        {
            return oDAO.Save(oFactura);
        }
    }
}
