using System;
using System.Windows.Forms;
using ElectricSolution.Controllers;
using ElectricSolution.Models;

namespace ElectricSolution.Views
{
    public partial class FrmReportes : Form
    {
        private Label lblTitulo;
        private Label lblTipo;
        private ComboBox cmbTipo;
        private Label lblDescripcion;
        private TextBox txtDescripcion;
        private Button btnEnviar;
        private Label lblHistorial;
        private DataGridView dgvReportes;
        private Button btnCerrar;

        public FrmReportes()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblTitulo = new Label();
            this.lblTipo = new Label();
            this.cmbTipo = new ComboBox();
            this.lblDescripcion = new Label();
            this.txtDescripcion = new TextBox();
            this.btnEnviar = new Button();
            this.lblHistorial = new Label();
            this.dgvReportes = new DataGridView();
            this.btnCerrar = new Button();
            this.SuspendLayout();

            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Text = "Reportar Avería";

            this.lblTipo.AutoSize = true;
            this.lblTipo.Location = new System.Drawing.Point(20, 60);
            this.lblTipo.Text = "Tipo de reporte:";

            this.cmbTipo.Location = new System.Drawing.Point(150, 57);
            this.cmbTipo.Size = new System.Drawing.Size(200, 21);
            this.cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbTipo.Items.AddRange(new object[] { "Avería", "Fluctuación", "Poste dañado", "Otro" });

            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new System.Drawing.Point(20, 95);
            this.lblDescripcion.Text = "Descripción:";

            this.txtDescripcion.Location = new System.Drawing.Point(150, 92);
            this.txtDescripcion.Size = new System.Drawing.Size(400, 60);
            this.txtDescripcion.Multiline = true;

            this.btnEnviar.Location = new System.Drawing.Point(150, 165);
            this.btnEnviar.Size = new System.Drawing.Size(150, 32);
            this.btnEnviar.Text = "Enviar Reporte";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new EventHandler(this.btnEnviar_Click);

            this.lblHistorial.AutoSize = true;
            this.lblHistorial.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblHistorial.Location = new System.Drawing.Point(20, 215);
            this.lblHistorial.Text = "Mis reportes:";

            this.dgvReportes.Location = new System.Drawing.Point(20, 240);
            this.dgvReportes.Size = new System.Drawing.Size(540, 180);
            this.dgvReportes.ReadOnly = true;
            this.dgvReportes.AllowUserToAddRows = false;
            this.dgvReportes.AllowUserToDeleteRows = false;
            this.dgvReportes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.btnCerrar.Location = new System.Drawing.Point(430, 430);
            this.btnCerrar.Size = new System.Drawing.Size(130, 32);
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new EventHandler(this.btnCerrar_Click);

            this.ClientSize = new System.Drawing.Size(580, 480);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblTipo);
            this.Controls.Add(this.cmbTipo);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.lblHistorial);
            this.Controls.Add(this.dgvReportes);
            this.Controls.Add(this.btnCerrar);
            this.Name = "FrmReportes";
            this.Text = "Reportar Avería";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Load += new EventHandler(this.FrmReportes_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void FrmReportes_Load(object sender, EventArgs e)
        {
            CargarHistorial();
        }

        private void CargarHistorial()
        {
            try
            {
                ReporteController controller = new ReporteController();
                var reportes = controller.ObtenerPorCliente(FrmLogin.clienteActual.idCliente);

                dgvReportes.DataSource = null;
                dgvReportes.DataSource = reportes;

                if (dgvReportes.Columns.Contains("idCliente"))
                {
                    dgvReportes.Columns["idCliente"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el historial: " + ex.Message);
            }
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            if (cmbTipo.SelectedItem == null || txtDescripcion.Text.Trim() == "")
            {
                MessageBox.Show("Seleccione el tipo de reporte y escriba una descripción.");
                return;
            }

            try
            {
                Reporte r = new Reporte();
                r.TipoReporte = cmbTipo.SelectedItem.ToString();
                r.Descripcion = txtDescripcion.Text.Trim();
                r.idCliente = FrmLogin.clienteActual.idCliente;

                ReporteController controller = new ReporteController();
                bool ok = controller.Registrar(r);

                if (ok)
                {
                    MessageBox.Show("Reporte enviado correctamente. Le daremos seguimiento.");
                    txtDescripcion.Clear();
                    cmbTipo.SelectedIndex = -1;
                    CargarHistorial();
                }
                else
                {
                    MessageBox.Show("No se pudo enviar el reporte.");
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
