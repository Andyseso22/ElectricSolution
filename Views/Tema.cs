namespace ElectricSolution.Views;

/// <summary>Colores y controles reutilizables (estilo de Electric Solution).</summary>
public static class Tema
{
    public static readonly Color Azul = ColorTranslator.FromHtml("#0B2A5B");
    public static readonly Color AzulActivo = ColorTranslator.FromHtml("#1E4FA3");
    public static readonly Color Verde = ColorTranslator.FromHtml("#3FA34D");
    public static readonly Color Amarillo = ColorTranslator.FromHtml("#F5B700");
    public static readonly Color Rojo = ColorTranslator.FromHtml("#C62828");
    public static readonly Color Fondo = ColorTranslator.FromHtml("#F3F6FB");
    public static readonly Color TextoOscuro = ColorTranslator.FromHtml("#1F2937");
    public static readonly Color TextoSuave = ColorTranslator.FromHtml("#6B7280");

    public static string Moneda(decimal monto) => $"C$ {monto:N2}";

    public static Label Etiqueta(string texto) => new()
    {
        Text = texto,
        AutoSize = true,
        ForeColor = TextoSuave,
        Margin = new Padding(0, 10, 0, 2)
    };

    public static TextBox CajaTexto(string valor = "", bool soloLectura = false, int ancho = 340)
    {
        var caja = new TextBox { Text = valor, Width = ancho, ReadOnly = soloLectura };
        if (soloLectura) caja.BackColor = Color.FromArgb(235, 238, 244);
        return caja;
    }

    public static Button Boton(string texto, int ancho = 180)
    {
        var boton = new Button
        {
            Text = texto,
            Width = ancho,
            Height = 40,
            FlatStyle = FlatStyle.Flat,
            BackColor = AzulActivo,
            ForeColor = Color.White,
            Cursor = Cursors.Hand,
            Font = new Font("Segoe UI Semibold", 10f)
        };
        boton.FlatAppearance.BorderSize = 0;
        return boton;
    }

    /// <summary>Columna vertical que crece con su contenido (para formularios).</summary>
    public static FlowLayoutPanel Columna() => new()
    {
        FlowDirection = FlowDirection.TopDown,
        WrapContents = false,
        AutoSize = true,
        AutoSizeMode = AutoSizeMode.GrowAndShrink,
        Margin = new Padding(0, 0, 40, 0)
    };

    /// <summary>Tarjeta blanca que agrupa columnas de un formulario.</summary>
    public static FlowLayoutPanel Formulario() => new()
    {
        Dock = DockStyle.Top,
        AutoSize = true,
        AutoSizeMode = AutoSizeMode.GrowAndShrink,
        BackColor = Color.White,
        Padding = new Padding(20, 10, 20, 20),
        FlowDirection = FlowDirection.LeftToRight,
        WrapContents = false
    };

    public static Panel Tarjeta(string titulo, string valor, Color acento)
    {
        var tarjeta = new Panel
        {
            Width = 240,
            Height = 96,
            BackColor = Color.White,
            Margin = new Padding(0, 0, 16, 16)
        };
        var barra = new Panel { Dock = DockStyle.Left, Width = 6, BackColor = acento };
        var lblTitulo = new Label
        {
            Text = titulo,
            Dock = DockStyle.Top,
            Height = 32,
            AutoSize = false,
            ForeColor = TextoSuave,
            Padding = new Padding(12, 8, 0, 0)
        };
        var lblValor = new Label
        {
            Text = valor,
            Dock = DockStyle.Fill,
            AutoSize = false,
            ForeColor = TextoOscuro,
            Font = new Font("Segoe UI Semibold", 16f),
            Padding = new Padding(12, 0, 0, 0),
            TextAlign = ContentAlignment.MiddleLeft
        };
        tarjeta.Controls.Add(lblValor);
        tarjeta.Controls.Add(lblTitulo);
        tarjeta.Controls.Add(barra);
        return tarjeta;
    }

    public static DataGridView Grid()
    {
        var grid = new DataGridView
        {
            Dock = DockStyle.Fill,
            AutoGenerateColumns = false,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AllowUserToResizeRows = false,
            ReadOnly = true,
            MultiSelect = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            BackgroundColor = Color.White,
            BorderStyle = BorderStyle.None,
            RowHeadersVisible = false,
            EnableHeadersVisualStyles = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
            ColumnHeadersHeight = 38,
            GridColor = ColorTranslator.FromHtml("#E5E7EB"),
            CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        };
        grid.RowTemplate.Height = 34;
        grid.ColumnHeadersDefaultCellStyle.BackColor = Azul;
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Azul;
        grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        grid.DefaultCellStyle.ForeColor = TextoOscuro;
        grid.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#DBE7FB");
        grid.DefaultCellStyle.SelectionForeColor = TextoOscuro;
        grid.DefaultCellStyle.Padding = new Padding(6, 0, 6, 0);
        return grid;
    }

    public static DataGridViewTextBoxColumn Col(string encabezado, string propiedad, string? formato = null, float peso = 100)
    {
        var columna = new DataGridViewTextBoxColumn
        {
            Name = propiedad,
            HeaderText = encabezado,
            DataPropertyName = propiedad,
            FillWeight = peso
        };
        if (formato != null) columna.DefaultCellStyle.Format = formato;
        return columna;
    }
}
