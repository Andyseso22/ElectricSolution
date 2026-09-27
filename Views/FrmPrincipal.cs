namespace ElectricSolution.Views;

/// <summary>
/// Pantalla principal: menú lateral (Inicio, Estado de Cuenta, Facturas, Sistema de Pago, ...)
/// y un panel donde se carga cada pantalla.
/// </summary>
public class FrmPrincipal : Form
{
    private readonly Panel panelMenu = new() { Dock = DockStyle.Left, Width = 250, BackColor = Tema.Azul };
    private readonly Panel panelContenido = new() { Dock = DockStyle.Fill, BackColor = Tema.Fondo };
    private readonly FlowLayoutPanel listaBotones = new()
    {
        Dock = DockStyle.Fill,
        FlowDirection = FlowDirection.TopDown,
        WrapContents = false,
        BackColor = Tema.Azul
    };
    private readonly List<Button> botones = new();
    private Button? botonActivo;

    public FrmPrincipal()
    {
        Text = "Electric Solution";
        ClientSize = new Size(1080, 660);
        MinimumSize = new Size(940, 620);
        StartPosition = FormStartPosition.CenterScreen;
        Font = new Font("Segoe UI", 10f);
        BackColor = Tema.Fondo;

        // Opciones del menú (las 4 primeras son las del prototipo del informe)
        Agregar("Inicio", () => new PaginaInicio());
        Agregar("Estado de Cuenta", () => new PaginaEstadoCuenta());
        Agregar("Facturas", () => new PaginaFacturas());
        Agregar("Sistema de Pago", () => new PaginaPagos());
        Agregar("Reportar Avería", () => new PaginaAverias());
        Agregar("Cortes Programados", () => new PaginaCortes());
        Agregar("Mi Perfil", () => new PaginaPerfil());

        panelMenu.Controls.Add(listaBotones);
        panelMenu.Controls.Add(CrearBotonCerrarSesion());
        panelMenu.Controls.Add(CrearEncabezado());

        Controls.Add(panelContenido);
        Controls.Add(panelMenu);

        Load += (s, e) => Mostrar(botones[0], new PaginaInicio());
    }

    private void Agregar(string texto, Func<UserControl> crearPagina)
    {
        var boton = CrearBoton(texto);
        boton.Click += (s, e) => Mostrar(boton, crearPagina());
        botones.Add(boton);
        listaBotones.Controls.Add(boton);
    }

    private void Mostrar(Button boton, UserControl pagina)
    {
        if (botonActivo != null) botonActivo.BackColor = Tema.Azul;
        botonActivo = boton;
        boton.BackColor = Tema.AzulActivo;

        // Liberar la pantalla anterior (RNF-11)
        var anteriores = panelContenido.Controls.Cast<Control>().ToList();
        panelContenido.Controls.Clear();
        anteriores.ForEach(c => c.Dispose());

        pagina.Dock = DockStyle.Fill;
        panelContenido.Controls.Add(pagina);
    }

    private static Button CrearBoton(string texto)
    {
        var boton = new Button
        {
            Text = texto,
            Width = 250,
            Height = 52,
            FlatStyle = FlatStyle.Flat,
            BackColor = Tema.Azul,
            ForeColor = Color.White,
            TextAlign = ContentAlignment.MiddleLeft,
            Padding = new Padding(22, 0, 0, 0),
            Margin = new Padding(0),
            Cursor = Cursors.Hand,
            Font = new Font("Segoe UI", 10.5f)
        };
        boton.FlatAppearance.BorderSize = 0;
        boton.FlatAppearance.MouseOverBackColor = Tema.AzulActivo;
        return boton;
    }

    private Control CrearBotonCerrarSesion()
    {
        var boton = CrearBoton("Cerrar Sesión");
        boton.Dock = DockStyle.Bottom;
        boton.BackColor = ColorTranslator.FromHtml("#8E1B1B");
        boton.FlatAppearance.MouseOverBackColor = Tema.Rojo;
        boton.Click += (s, e) =>
        {
            var respuesta = MessageBox.Show("¿Deseas cerrar sesión?", "Cerrar sesión",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // Cuando exista FrmLogin: abrirlo aquí en lugar de cerrar la aplicación.
            if (respuesta == DialogResult.Yes) Close();
        };
        return boton;
    }

    // Logo en texto. Para usar el logo real: agregar un PictureBox con Image = ...
    private static Control CrearEncabezado()
    {
        var panel = new Panel { Dock = DockStyle.Top, Height = 120, BackColor = Tema.Azul };
        var lblSolution = new Label
        {
            Text = "S O L U T I O N",
            Dock = DockStyle.Bottom,
            Height = 36,
            AutoSize = false,
            ForeColor = Tema.Verde,
            Font = new Font("Segoe UI", 10f, FontStyle.Bold),
            TextAlign = ContentAlignment.TopCenter
        };
        var lblElectric = new Label
        {
            Text = "ELECTRIC",
            Dock = DockStyle.Fill,
            AutoSize = false,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 22f, FontStyle.Bold),
            TextAlign = ContentAlignment.BottomCenter
        };
        panel.Controls.Add(lblElectric);
        panel.Controls.Add(lblSolution);
        return panel;
    }
}
