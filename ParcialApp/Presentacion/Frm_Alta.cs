
using ParcialApp.Models;
using ParcialApp.Services;
using ParcialApp.Services.Interfaz;
using System;
using System.Windows.Forms;

namespace ParcialApp.Presentacion
{
    public partial class Frm_Alta : Form
    {
        private FactoryServiceABS Factory;
        private IService Service;
        private Factura factura;
        
        public Frm_Alta()
        {
            InitializeComponent();
            Factory = new FactoryServiceIMP();
            Service = Factory.GetService();
            factura = new Factura();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

          //completar...
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea cancelar?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Dispose();

            }
            else
            {
                return;
            }
        }

        private void Frm_Alta_Presupuesto_Load(object sender, EventArgs e)
        {
            CargarCombo();
        }

        private void CargarCombo()
        {
            cboProducto.DataSource = Service.GetProductos();
            cboProducto.ValueMember = "id";
            cboProducto.DisplayMember = "name";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (Valido())
            {
                Producto p = new Producto();
                p = (Producto)cboProducto.SelectedItem;

                int cant = int.Parse(nudCantidad.Value.ToString());
                DtlleFactura dtll = new DtlleFactura(p,cant);

                if (!ExisteProductoEnGrilla(cboProducto.Text))
                {
                    factura.AgregarDetalle(dtll);

                    dgvDetalles.Rows.Add(new object[]
                    {
                        p.Id, p.Name, p.Precio, dtll.Cantidad, dtll.CalcularSubTotal()
                    });
                    CargarTotal();
                } 
                else
                {
                    MessageBox.Show("El producto ya se encuentra como detalle","CONTROL",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
            }
        }

        private void CargarTotal()

        {
            if (cboForma.Text == "1-Contado")
            {
                lblTotal.Text = "Total: $" + factura.CalcularTotal();
            }
            else
            {
                lblTotal.Text = "Total: $" + (factura.CalcularTotal() * 1.10);
            }
        }

        private bool Valido()
        {
            bool ok = true;
            
            if (string.IsNullOrEmpty(txtCliente.Text)) { ok = false; MessageBox.Show("Debe completar el campo Cliente", "CONTROL", MessageBoxButtons.OK); }
            if (cboForma.SelectedIndex == -1) { ok = false; MessageBox.Show("Debe elegir una forma de Pago", "CONTROL", MessageBoxButtons.OK); }
            if (cboProducto.SelectedIndex == -1) { ok = false; MessageBox.Show("Debe seleciona un Producto", "CONTROL", MessageBoxButtons.OK); }
            
            return ok;
        }

        private bool ExisteProductoEnGrilla(string text)
        {
            foreach (DataGridViewRow fila in dgvDetalles.Rows)
            {
                if (fila.Cells["producto"].Value.Equals(text))
                    return true;
            }
            return false;
        }
              

        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalles.CurrentCell.ColumnIndex == 5)
            {
                factura.QuitarDetalle(dgvDetalles.CurrentRow.Index);
                dgvDetalles.Rows.Remove(dgvDetalles.CurrentRow);
                CargarTotal();
            }
        }

        private void cboForma_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
