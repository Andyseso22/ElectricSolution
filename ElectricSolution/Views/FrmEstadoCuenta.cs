using System;
using System.Windows.Forms;
using ElectricSolution.Controllers;
using ElectricSolution.Models;

namespace ElectricSolution.Views
{
    public partial class FrmEstadoCuenta : Form
    {
        private Label lblTitulo;
        private Label lblNumeroCuenta;
        private Label lblSaldo;
        private Label lblVencimiento;
        private Button btnCerrar;

        public FrmEstadoCuenta()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblTitulo = new Label();
            this.lblNumeroCuenta = new Label();
            this.lblSaldo = new Label();
            this.lblVencimiento = new Label();
            this.btnCerrar = new Button();
            this.SuspendLayout();

            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.lblTitulo.Location = new System.Drawing.Point(30, 20);
            this.lblTitulo.Text = "Estado de Cuenta";

            this.lblNumeroCuenta.AutoSize = true;
            this.lblNumeroCuenta.Location = new System.Drawing.Point(30, 70);
            this.lblNumeroCuenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblNumeroCuenta.Text = "Numero de cuenta: -";

            this.lblSaldo.AutoSize = true;
            this.lblSaldo.Location = new System.Drawing.Point(30, 105);
            this.lblSaldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblSaldo.Text = "Saldo pendiente: -";

            this.lblVencimiento.AutoSize = true;
            this.lblVencimiento.Location = new System.Drawing.Point(30, 145);
            this.lblVencimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblVencimiento.Text = "Fecha de vencimiento: -";

            this.btnCerrar.Location = new System.Drawing.Point(30, 200);
            this.btnCerrar.Size = new System.Drawing.Size(100, 32);
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new EventHandler(this.btnCerrar_Click);

            this.ClientSize = new System.Drawing.Size(400, 260);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblNumeroCuenta);
            this.Controls.Add(this.lblSaldo);
            this.Controls.Add(this.lblVencimiento);
            this.Controls.Add(this.btnCerrar);
            this.Name = "FrmEstadoCuenta";
            this.Text = "Estado de Cuenta";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Load += new EventHandler(this.FrmEstadoCuenta_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

       
        private void FrmEstadoCuenta_Load(object sender, EventArgs e)
        {
            try
            {
                CuentaController controller = new CuentaController();
                Cuenta cuenta = controller.ObtenerPorCliente(FrmLogin.clienteActual.idCliente);

                if (cuenta != null)
                {
                    lblNumeroCuenta.Text = "Numero de cuenta: " + cuenta.NumeroCuenta;
                    lblSaldo.Text = "Saldo pendiente: C$ " + cuenta.Saldo.ToString("N2");
                    lblVencimiento.Text = "Fecha de vencimiento: " + cuenta.FechaVencimiento.ToString("dd/MM/yyyy");

                    // Cambiamos el color segun si tiene o no saldo pendiente
                    if (cuenta.Saldo > 0)
                    {
                        lblSaldo.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        lblSaldo.ForeColor = System.Drawing.Color.Green;
                    }
                }
                else
                {
                    lblNumeroCuenta.Text = "Este cliente no tiene una cuenta registrada.";
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
    }
}
