using System;
using System.Windows.Forms;
using ElectricSolution.Controllers;
using ElectricSolution.Models;

namespace ElectricSolution.Views
{
    public partial class FrmFacturas : Form
    {
        private Label lblTitulo;
        private DataGridView dgvFacturas;
        private Button btnPagar;
        private Button btnCerrar;

       
        private Cuenta cuentaCliente;

        public FrmFacturas()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.dgvFacturas = new System.Windows.Forms.DataGridView();
            this.btnPagar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturas)).BeginInit();
            this.SuspendLayout();
           
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(136, 26);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Mis Facturas";

            this.dgvFacturas.AllowUserToAddRows = false;
            this.dgvFacturas.AllowUserToDeleteRows = false;
            this.dgvFacturas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFacturas.ColumnHeadersHeight = 29;
            this.dgvFacturas.Location = new System.Drawing.Point(20, 55);
            this.dgvFacturas.MultiSelect = false;
            this.dgvFacturas.Name = "dgvFacturas";
            this.dgvFacturas.ReadOnly = true;
            this.dgvFacturas.RowHeadersWidth = 51;
            this.dgvFacturas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFacturas.Size = new System.Drawing.Size(540, 250);
            this.dgvFacturas.TabIndex = 1;
            this.dgvFacturas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFacturas_CellContentClick);
            
            this.btnPagar.Location = new System.Drawing.Point(20, 320);
            this.btnPagar.Name = "btnPagar";
            this.btnPagar.Size = new System.Drawing.Size(150, 35);
            this.btnPagar.TabIndex = 2;
            this.btnPagar.Text = "Pagar factura seleccionada";
            this.btnPagar.UseVisualStyleBackColor = true;
            this.btnPagar.Click += new System.EventHandler(this.btnPagar_Click);
           
            this.btnCerrar.Location = new System.Drawing.Point(430, 320);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(130, 35);
            this.btnCerrar.TabIndex = 3;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            
            this.ClientSize = new System.Drawing.Size(580, 380);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.dgvFacturas);
            this.Controls.Add(this.btnPagar);
            this.Controls.Add(this.btnCerrar);
            this.Name = "FrmFacturas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Facturas y Pagos";
            this.Load += new System.EventHandler(this.FrmFacturas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        
        private void FrmFacturas_Load(object sender, EventArgs e)
        {
            CargarFacturas();
        }

        private void CargarFacturas()
        {
            try
            {
                CuentaController cuentaController = new CuentaController();
                cuentaCliente = cuentaController.ObtenerPorCliente(FrmLogin.clienteActual.idCliente);

                if (cuentaCliente == null)
                {
                    MessageBox.Show("Este cliente no tiene cuenta registrada.");
                    return;
                }

                FacturaController facturaController = new FacturaController();
                var facturas = facturaController.ObtenerPorCuenta(cuentaCliente.idCuenta);

                dgvFacturas.DataSource = null;
                dgvFacturas.DataSource = facturas;

                if (dgvFacturas.Columns.Contains("idCuenta"))
                {
                    dgvFacturas.Columns["idCuenta"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las facturas: " + ex.Message);
            }
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            if (dgvFacturas.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una factura de la lista.");
                return;
            }

            Factura facturaSeleccionada = (Factura)dgvFacturas.CurrentRow.DataBoundItem;

            if (facturaSeleccionada.Estado == "Pagada")
            {
                MessageBox.Show("Esa factura ya esta pagada.");
                return;
            }

            DialogResult r = MessageBox.Show(
                "¿Confirma el pago de C$ " + facturaSeleccionada.Monto.ToString("N2") + "?",
                "Confirmar pago", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (r != DialogResult.Yes) return;

            try
            {
                FacturaController facturaController = new FacturaController();
                bool ok = facturaController.PagarFactura(facturaSeleccionada.idFactura, "Tarjeta");

                if (ok)
                {
                    MessageBox.Show("Pago registrado correctamente.");
                    CargarFacturas();
                }
                else
                {
                    MessageBox.Show("No se pudo procesar el pago.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvFacturas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
