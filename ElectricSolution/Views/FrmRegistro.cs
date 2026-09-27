using System;
using System.Windows.Forms;
using ElectricSolution.Controllers;
using ElectricSolution.Models;

namespace ElectricSolution.Views
{
    public partial class FrmRegistro : Form
    {
        private Label lblTitulo;
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblCedula;
        private TextBox txtCedula;
        private Label lblNiss;
        private TextBox txtNiss;
        private Label lblCorreo;
        private TextBox txtCorreo;
        private Label lblTelefono;
        private TextBox txtTelefono;
        private Label lblDireccion;
        private TextBox txtDireccion;
        private Label lblContrasena;
        private TextBox txtContrasena;
        private Button btnGuardar;
        private Button btnCancelar;

        public FrmRegistro()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblCedula = new System.Windows.Forms.Label();
            this.txtCedula = new System.Windows.Forms.TextBox();
            this.lblNiss = new System.Windows.Forms.Label();
            this.txtNiss = new System.Windows.Forms.TextBox();
            this.lblCorreo = new System.Windows.Forms.Label();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.lblDireccion = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.lblContrasena = new System.Windows.Forms.Label();
            this.txtContrasena = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.lblTitulo.Location = new System.Drawing.Point(100, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(298, 29);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Registro - Electric Solution";
           
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(40, 70);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(118, 16);
            this.lblNombre.TabIndex = 1;
            this.lblNombre.Text = "Nombre completo:";
           
            this.txtNombre.Location = new System.Drawing.Point(180, 67);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(220, 22);
            this.txtNombre.TabIndex = 2;
            
            this.lblCedula.AutoSize = true;
            this.lblCedula.Location = new System.Drawing.Point(40, 105);
            this.lblCedula.Name = "lblCedula";
            this.lblCedula.Size = new System.Drawing.Size(53, 16);
            this.lblCedula.TabIndex = 3;
            this.lblCedula.Text = "Cédula:";
           
            this.txtCedula.Location = new System.Drawing.Point(180, 102);
            this.txtCedula.Name = "txtCedula";
            this.txtCedula.Size = new System.Drawing.Size(220, 22);
            this.txtCedula.TabIndex = 4;
            
            this.lblNiss.AutoSize = true;
            this.lblNiss.Location = new System.Drawing.Point(40, 140);
            this.lblNiss.Name = "lblNiss";
            this.lblNiss.Size = new System.Drawing.Size(92, 16);
            this.lblNiss.TabIndex = 5;
            this.lblNiss.Text = "Número NISS:";
             
            this.txtNiss.Location = new System.Drawing.Point(180, 137);
            this.txtNiss.Name = "txtNiss";
            this.txtNiss.Size = new System.Drawing.Size(220, 22);
            this.txtNiss.TabIndex = 6;
            
            this.lblCorreo.AutoSize = true;
            this.lblCorreo.Location = new System.Drawing.Point(40, 175);
            this.lblCorreo.Name = "lblCorreo";
            this.lblCorreo.Size = new System.Drawing.Size(51, 16);
            this.lblCorreo.TabIndex = 7;
            this.lblCorreo.Text = "Correo:";
            
            this.txtCorreo.Location = new System.Drawing.Point(180, 172);
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.Size = new System.Drawing.Size(220, 22);
            this.txtCorreo.TabIndex = 8;
            
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Location = new System.Drawing.Point(40, 210);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(64, 16);
            this.lblTelefono.TabIndex = 9;
            this.lblTelefono.Text = "Teléfono:";
            
            this.txtTelefono.Location = new System.Drawing.Point(180, 207);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(220, 22);
            this.txtTelefono.TabIndex = 10;
            
            this.lblDireccion.AutoSize = true;
            this.lblDireccion.Location = new System.Drawing.Point(40, 245);
            this.lblDireccion.Name = "lblDireccion";
            this.lblDireccion.Size = new System.Drawing.Size(67, 16);
            this.lblDireccion.TabIndex = 11;
            this.lblDireccion.Text = "Dirección:";
            
            this.txtDireccion.Location = new System.Drawing.Point(180, 242);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(220, 22);
            this.txtDireccion.TabIndex = 12;
           
            this.lblContrasena.AutoSize = true;
            this.lblContrasena.Location = new System.Drawing.Point(40, 280);
            this.lblContrasena.Name = "lblContrasena";
            this.lblContrasena.Size = new System.Drawing.Size(79, 16);
            this.lblContrasena.TabIndex = 13;
            this.lblContrasena.Text = "Contraseña:";
             
            this.txtContrasena.Location = new System.Drawing.Point(180, 277);
            this.txtContrasena.Name = "txtContrasena";
            this.txtContrasena.PasswordChar = '*';
            this.txtContrasena.Size = new System.Drawing.Size(220, 22);
            this.txtContrasena.TabIndex = 14;
            
            this.btnGuardar.Location = new System.Drawing.Point(180, 320);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(120, 35);
            this.btnGuardar.TabIndex = 15;
            this.btnGuardar.Text = "Crear Cuenta";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            
            this.btnCancelar.Location = new System.Drawing.Point(310, 320);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(90, 35);
            this.btnCancelar.TabIndex = 16;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            
            this.ClientSize = new System.Drawing.Size(450, 380);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblCedula);
            this.Controls.Add(this.txtCedula);
            this.Controls.Add(this.lblNiss);
            this.Controls.Add(this.txtNiss);
            this.Controls.Add(this.lblCorreo);
            this.Controls.Add(this.txtCorreo);
            this.Controls.Add(this.lblTelefono);
            this.Controls.Add(this.txtTelefono);
            this.Controls.Add(this.lblDireccion);
            this.Controls.Add(this.txtDireccion);
            this.Controls.Add(this.lblContrasena);
            this.Controls.Add(this.txtContrasena);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);
            this.Name = "FrmRegistro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registro";
            this.Load += new System.EventHandler(this.FrmRegistro_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text == "" || txtCedula.Text == "" ||
                txtNiss.Text == "" || txtCorreo.Text == "" ||
                txtContrasena.Text == "")
            {
                MessageBox.Show("Complete todos los campos obligatorios.");
                return;
            }

            try
            {
                ClienteController controller = new ClienteController();

                if (controller.ExisteCorreo(txtCorreo.Text))
                {
                    MessageBox.Show("Ese correo ya está registrado.");
                    return;
                }

                Cliente c = new Cliente();
                c.NombreCliente = txtNombre.Text;
                c.Cedula = txtCedula.Text;
                c.NumeroNiss = txtNiss.Text;
                c.Correo = txtCorreo.Text;
                c.Telefono = txtTelefono.Text;
                c.Direccion = txtDireccion.Text;
                c.Contrasena = txtContrasena.Text;

                bool ok = controller.Registrar(c);

                if (ok)
                {
                    MessageBox.Show("¡Cuenta creada exitosamente!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo crear la cuenta.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmRegistro_Load(object sender, EventArgs e)
        {

        }
    }
}
