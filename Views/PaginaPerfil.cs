using ElectricSolution.Data;

namespace ElectricSolution.Views;

public class PaginaPerfil : PaginaBase
{
    private readonly TextBox txtCorreo;
    private readonly TextBox txtTelefono;
    private readonly TextBox txtDireccion;

    public PaginaPerfil() : base("Mi perfil", "Mantén tus datos de contacto actualizados")
    {
        var cliente = DatosDemo.Cliente;
        txtCorreo = Tema.CajaTexto(cliente.Correo);
        txtTelefono = Tema.CajaTexto(cliente.Telefono);
        txtTelefono.MaxLength = 8;
        txtDireccion = Tema.CajaTexto(cliente.Direccion);

        var btnGuardar = Tema.Boton("Guardar cambios", 200);
        btnGuardar.Margin = new Padding(0, 16, 0, 0);
        btnGuardar.Click += (s, e) => Guardar();

        var izquierda = Tema.Columna();
        izquierda.Controls.Add(Tema.Etiqueta("Nombre completo"));
        izquierda.Controls.Add(Tema.CajaTexto(cliente.NombreCliente, soloLectura: true));
        izquierda.Controls.Add(Tema.Etiqueta("Cédula de identidad"));
        izquierda.Controls.Add(Tema.CajaTexto(cliente.Cedula, soloLectura: true));
        izquierda.Controls.Add(Tema.Etiqueta("Número NISS"));
        izquierda.Controls.Add(Tema.CajaTexto(cliente.NumeroNiss, soloLectura: true));

        var derecha = Tema.Columna();
        derecha.Controls.Add(Tema.Etiqueta("Correo electrónico"));
        derecha.Controls.Add(txtCorreo);
        derecha.Controls.Add(Tema.Etiqueta("Teléfono"));
        derecha.Controls.Add(txtTelefono);
        derecha.Controls.Add(Tema.Etiqueta("Dirección"));
        derecha.Controls.Add(txtDireccion);
        derecha.Controls.Add(btnGuardar);

        var formulario = Tema.Formulario();
        formulario.Controls.Add(izquierda);
        formulario.Controls.Add(derecha);

        var nota = new Label
        {
            Text = "Para cambiar el nombre o la cédula debes presentar documentación en una oficina de atención.",
            Dock = DockStyle.Top,
            AutoSize = true,
            ForeColor = Tema.TextoSuave,
            Padding = new Padding(0, 12, 0, 0)
        };

        Contenido.Controls.Add(nota);
        Contenido.Controls.Add(formulario);
    }

    private void Guardar()
    {
        var correo = txtCorreo.Text.Trim();
        var telefono = txtTelefono.Text.Trim();
        var direccion = txtDireccion.Text.Trim();

        if (!CorreoValido(correo))
        {
            Aviso("Por favor, ingresa un correo electrónico válido (ejemplo: nombre@correo.com).", txtCorreo);
            return;
        }
        if (telefono.Length != 8 || !telefono.All(char.IsDigit))
        {
            Aviso("Por favor, ingresa un número telefónico válido de 8 dígitos.", txtTelefono);
            return;
        }
        if (direccion.Length == 0)
        {
            Aviso("Por favor, ingresa tu dirección.", txtDireccion);
            return;
        }

        // Aquí iría el UPDATE en SQL Server.
        var cliente = DatosDemo.Cliente;
        cliente.Correo = correo;
        cliente.Telefono = telefono;
        cliente.Direccion = direccion;

        MessageBox.Show("Tus datos fueron actualizados correctamente.", "Mi perfil",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private static void Aviso(string mensaje, Control campo)
    {
        MessageBox.Show(mensaje, "Mi perfil", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        campo.Focus();
    }

    private static bool CorreoValido(string correo)
    {
        if (string.IsNullOrWhiteSpace(correo)) return false;
        try { return new System.Net.Mail.MailAddress(correo).Address == correo; }
        catch (FormatException) { return false; }
    }
}
