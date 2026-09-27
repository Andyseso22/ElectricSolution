using System;
using System.Windows.Forms;
using ElectricSolution.Controllers;
using ElectricSolution.Models;

namespace ElectricSolution.Views
{
    public partial class FrmLogin : Form
    {
        private Label lblTitulo;
        private Label lblCorreo;
        private TextBox txtCorreo;
        private Label lblContrasena;
        private TextBox txtContrasena;
        private Button btnIngresar;
        private Button btnRegistrar;
        private Button btnSalir;

        
        public static Cliente clienteActual = null;

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblCorreo = new System.Windows.Forms.Label();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.lblContrasena = new System.Windows.Forms.Label();
            this.txtContrasena = new System.Windows.Forms.TextBox();
            this.btnIngresar = new System.Windows.Forms.Button();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.SuspendLayout();
          
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.lblTitulo.Location = new System.Drawing.Point(70, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(267, 29);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Login - Electric Solution";
            
            this.lblCorreo.AutoSize = true;
            this.lblCorreo.Location = new System.Drawing.Point(40, 80);
            this.lblCorreo.Name = "lblCorreo";
            this.lblCorreo.Size = new System.Drawing.Size(51, 16);
            this.lblCorreo.TabIndex = 1;
            this.lblCorreo.Text = "Correo:";
           
            this.txtCorreo.Location = new System.Drawing.Point(120, 77);
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.Size = new System.Drawing.Size(200, 22);
            this.txtCorreo.TabIndex = 2;
             
            this.lblContrasena.AutoSize = true;
            this.lblContrasena.Location = new System.Drawing.Point(40, 120);
            this.lblContrasena.Name = "lblContrasena";
            this.lblContrasena.Size = new System.Drawing.Size(79, 16);
            this.lblContrasena.TabIndex = 3;
            this.lblContrasena.Text = "Contraseña:";
           
            this.txtContrasena.Location = new System.Drawing.Point(120, 117);
            this.txtContrasena.Name = "txtContrasena";
            this.txtContrasena.PasswordChar = '*';
            this.txtContrasena.Size = new System.Drawing.Size(200, 22);
            this.txtContrasena.TabIndex = 4;
           
            this.btnIngresar.Location = new System.Drawing.Point(120, 160);
            this.btnIngresar.Name = "btnIngresar";
            this.btnIngresar.Size = new System.Drawing.Size(90, 30);
            this.btnIngresar.TabIndex = 5;
            this.btnIngresar.Text = "Ingresar";
            this.btnIngresar.UseVisualStyleBackColor = true;
            this.btnIngresar.Click += new System.EventHandler(this.btnIngresar_Click);
            
            this.btnRegistrar.Location = new System.Drawing.Point(230, 160);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(90, 30);
            this.btnRegistrar.TabIndex = 6;
            this.btnRegistrar.Text = "Registrarse";
            this.btnRegistrar.UseVisualStyleBackColor = true;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            
            this.btnSalir.Location = new System.Drawing.Point(120, 200);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(200, 30);
            this.btnSalir.TabIndex = 7;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            
            this.ClientSize = new System.Drawing.Size(380, 260);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblCorreo);
            this.Controls.Add(this.txtCorreo);
            this.Controls.Add(this.lblContrasena);
            this.Controls.Add(this.txtContrasena);
            this.Controls.Add(this.btnIngresar);
            this.Controls.Add(this.btnRegistrar);
            this.Controls.Add(this.btnSalir);
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

       
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (txtCorreo.Text == "" || txtContrasena.Text == "")
            {
                MessageBox.Show("Complete todos los campos.");
                return;
            }

            try
            {
                ClienteController controller = new ClienteController();
                Cliente c = controller.Login(txtCorreo.Text, txtContrasena.Text);

                if (c != null)
                {
                    clienteActual = c;
                    MessageBox.Show("¡Bienvenido, " + c.NombreCliente + "!");
                    FrmPrincipal principal = new FrmPrincipal();
                    principal.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Correo o contraseña incorrectos.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            FrmRegistro registro = new FrmRegistro();
            registro.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
