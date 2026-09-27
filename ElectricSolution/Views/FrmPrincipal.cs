using System;
using System.Windows.Forms;

namespace ElectricSolution.Views
{
    public partial class FrmPrincipal : Form
    {
        private Panel panelMenu;
        private Panel panelContenido;
        private Label lblBienvenida;
        private Button btnInicio;
        private Button btnEstadoCuenta;
        private Button btnFacturas;
        private Button btnReportes;
        private Button btnClientesAdmin;
        private Button btnCortes;
        private Button btnCerrarSesion;

        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.panelMenu = new Panel();
            this.panelContenido = new Panel();
            this.lblBienvenida = new Label();
            this.btnInicio = new Button();
            this.btnEstadoCuenta = new Button();
            this.btnFacturas = new Button();
            this.btnReportes = new Button();
            this.btnClientesAdmin = new Button();
            this.btnCortes = new Button();
            this.btnCerrarSesion = new Button();
            this.SuspendLayout();

            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(30, 60, 120);
            this.panelMenu.Dock = DockStyle.Left;
            this.panelMenu.Width = 200;

            Label lblTituloMenu = new Label();
            lblTituloMenu.Text = "ELECTRIC SOLUTION";
            lblTituloMenu.ForeColor = System.Drawing.Color.White;
            lblTituloMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            lblTituloMenu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblTituloMenu.Dock = DockStyle.Top;
            lblTituloMenu.Height = 70;
            this.panelMenu.Controls.Add(lblTituloMenu);

            ConfigurarBoton(this.btnInicio, "Inicio", 80);
            ConfigurarBoton(this.btnEstadoCuenta, "Estado de Cuenta", 130);
            ConfigurarBoton(this.btnFacturas, "Facturas y Pagos", 180);
            ConfigurarBoton(this.btnReportes, "Reportar Avería", 230);
            ConfigurarBoton(this.btnCortes, "Cortes Programados", 280);
            ConfigurarBoton(this.btnClientesAdmin, "Administrar Clientes", 330);
            ConfigurarBoton(this.btnCerrarSesion, "Cerrar Sesión", 460);
            this.btnCerrarSesion.BackColor = System.Drawing.Color.FromArgb(180, 40, 40);

            this.btnInicio.Click += new EventHandler(this.btnInicio_Click);
            this.btnEstadoCuenta.Click += new EventHandler(this.btnEstadoCuenta_Click);
            this.btnFacturas.Click += new EventHandler(this.btnFacturas_Click);
            this.btnReportes.Click += new EventHandler(this.btnReportes_Click);
            this.btnCortes.Click += new EventHandler(this.btnCortes_Click);
            this.btnClientesAdmin.Click += new EventHandler(this.btnClientesAdmin_Click);
            this.btnCerrarSesion.Click += new EventHandler(this.btnCerrarSesion_Click);

            this.panelContenido.Dock = DockStyle.Fill;
            this.panelContenido.BackColor = System.Drawing.Color.White;

            this.lblBienvenida.AutoSize = true;
            this.lblBienvenida.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lblBienvenida.Location = new System.Drawing.Point(40, 40);
            this.lblBienvenida.Text = "Bienvenido al sistema";
            this.panelContenido.Controls.Add(this.lblBienvenida);

            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.panelMenu);
            this.Name = "FrmPrincipal";
            this.Text = "Electric Solution - Pantalla Principal";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // Metodo de ayuda para no repetir el mismo codigo al crear cada boton del menu
        private void ConfigurarBoton(Button b, string texto, int y)
        {
            b.Text = texto;
            b.ForeColor = System.Drawing.Color.White;
            b.BackColor = System.Drawing.Color.FromArgb(30, 60, 120);
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            b.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            b.Padding = new Padding(15, 0, 0, 0);
            b.Location = new System.Drawing.Point(0, y);
            b.Size = new System.Drawing.Size(200, 45);
            this.panelMenu.Controls.Add(b);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (FrmLogin.clienteActual != null)
            {
                lblBienvenida.Text = "Bienvenido, " + FrmLogin.clienteActual.NombreCliente;
            }
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            lblBienvenida.Text = "Bienvenido, " + FrmLogin.clienteActual.NombreCliente;
        }

        private void btnEstadoCuenta_Click(object sender, EventArgs e)
        {
            FrmEstadoCuenta frm = new FrmEstadoCuenta();
            frm.ShowDialog();
        }

        private void btnFacturas_Click(object sender, EventArgs e)
        {
            FrmFacturas frm = new FrmFacturas();
            frm.ShowDialog();
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            FrmReportes frm = new FrmReportes();
            frm.ShowDialog();
        }

        private void btnCortes_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Módulo Cortes Programados (por implementar en un proximo sprint)");
        }

        private void btnClientesAdmin_Click(object sender, EventArgs e)
        {
            FrmClientes frm = new FrmClientes();
            frm.ShowDialog();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("¿Desea cerrar sesión?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                FrmLogin.clienteActual = null;
                FrmLogin login = new FrmLogin();
                login.Show();
                this.Close();
            }
        }
    }
}
