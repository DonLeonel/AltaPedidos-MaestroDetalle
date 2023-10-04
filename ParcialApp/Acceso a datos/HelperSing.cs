using ParcialApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Acceso_a_datos
{
    internal class HelperSing
    {
        private SqlConnection cnn;
        private static HelperSing instance = null;

        private HelperSing()
        {
            string Cadena = @"Data Source=DESKTOP-0L0DMBQ\SQLEXPRESS;Initial Catalog=db_facturas;Integrated Security=True";
            cnn = new SqlConnection(Cadena);
        }

        public static HelperSing Instance
        {
            get
            {
                if (instance == null)
                    return new HelperSing();
                return instance;
            }
        }

        public DataTable GetProductos(string SP)
        {
            cnn.Open();
            SqlCommand cmd = new SqlCommand(SP, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            var dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            cnn.Close();
            return dt;
        }

        public bool Save(string SpM, string SpD, Factura Factura)
        {
            SqlTransaction t = null;

            try
            {
                cnn.Open();
                t = cnn.BeginTransaction();
                SqlCommand cmdM = new SqlCommand(SpM, cnn, t);
                cmdM.CommandType = CommandType.StoredProcedure;
                cmdM.Parameters.Clear();
                cmdM.Parameters.AddWithValue("@fecha", Factura.Fecha);
                cmdM.Parameters.AddWithValue("@cliente", Factura.Cliente);
                cmdM.Parameters.AddWithValue("@forma_pago", Factura.Forma_Pago);
                cmdM.Parameters.AddWithValue("@total", Factura.CalcularTotal());

                SqlParameter p = new SqlParameter("@nro", DbType.Int32);
                p.Direction = ParameterDirection.Output;
                cmdM.Parameters.Add(p);
                cmdM.ExecuteNonQuery();
                int NroFac = (int)p.Value;

                int DetalleNro = 1;
                SqlCommand CmdD= new SqlCommand(SpD, cnn, t);
                CmdD.CommandType = CommandType.StoredProcedure;
                foreach (DtlleFactura dt in Factura.lDetalle)
                {
                    CmdD.Parameters.AddWithValue("@nro", NroFac);
                    CmdD.Parameters.AddWithValue("@detalle", DetalleNro);
                    CmdD.Parameters.AddWithValue("@id_producto", dt.Producto.Id);
                    CmdD.Parameters.AddWithValue("@cantidad", dt.Cantidad);
                    CmdD.ExecuteNonQuery();
                    DetalleNro++;
                }
                t.Commit();
                return true;
            }
            catch 
            {
                t.Rollback();
                return false;
            }
            finally
            {
                if (cnn.State.Equals(ConnectionState.Open))
                {
                    cnn.Close();
                }
            }
        }
    }
}
