using ElectricSolution.Data;
using ElectricSolution.Models;

namespace ElectricSolution.Views;

public class PaginaAverias : PaginaBase
{
    private readonly ComboBox cboTipo = new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 260 };
    private readonly TextBox txtDescripcion = new() { Multiline = true, Width = 460, Height = 90, ScrollBars = ScrollBars.Vertical };

    public PaginaAverias() : base("Reportar avería", "Informa fallas en el suministro y da seguimiento a tus reportes")
    {
        cboTipo.Items.AddRange(new object[]
        {
            "Sin suministro de energía", "Bajo voltaje", "Poste o cable dañado", "Medidor con falla", "Otro"
        });
        cboTipo.SelectedIndex = 0;

        var btnEnviar = Tema.Boton("Enviar reporte", 260);
        btnEnviar.Margin = new Padding(0, 16, 0, 0);
        btnEnviar.Click += (s, e) => Enviar();

        var izquierda = Tema.Columna();
        izquierda.Controls.Add(Tema.Etiqueta("Tipo de avería"));
        izquierda.Controls.Add(cboTipo);
        izquierda.Controls.Add(btnEnviar);

        var derecha = Tema.Columna();
        derecha.Controls.Add(Tema.Etiqueta("Describe lo que ocurre"));
        derecha.Controls.Add(txtDescripcion);

        var formulario = Tema.Formulario();
        formulario.Controls.Add(izquierda);
        formulario.Controls.Add(derecha);

        var lblSeguimiento = new Label
        {
            Text = "Seguimiento de reportes",
            Dock = DockStyle.Top,
            Height = 46,
            AutoSize = false,
            TextAlign = ContentAlignment.BottomLeft,
            Font = new Font("Segoe UI Semibold", 12f),
            ForeColor = Tema.Azul
        };

        var grid = Tema.Grid();
        grid.Columns.AddRange(
            Tema.Col("N.º", nameof(Reporte.NumeroReporte), null, 60),
            Tema.Col("Fecha", nameof(Reporte.FechaReporte), "dd/MM/yyyy HH:mm", 90),
            Tema.Col("Tipo", nameof(Reporte.TipoReporte), null, 120),
            Tema.Col("Estado", nameof(Reporte.Estado), null, 90),
            Tema.Col("Descripción", nameof(Reporte.Descripcion), null, 200));
        grid.DataSource = DatosDemo.Reportes;

        Contenido.Controls.Add(grid);
        Contenido.Controls.Add(lblSeguimiento);
        Contenido.Controls.Add(formulario);
    }

    private void Enviar()
    {
        var descripcion = txtDescripcion.Text.Trim();
        if (descripcion.Length < 10)
        {
            MessageBox.Show("Por favor, describe la avería con al menos 10 caracteres para que la cuadrilla pueda ubicarla.",
                "Reporte de avería", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtDescripcion.Focus();
            return;
        }

        var reporte = new Reporte
        {
            IdReporte = DatosDemo.Reportes.Count + 1,
            TipoReporte = cboTipo.SelectedItem?.ToString() ?? "Otro",
            Descripcion = descripcion,
            FechaReporte = DateTime.Now,
            Estado = "Recibido",
            IdCliente = DatosDemo.Cliente.IdCliente
        };
        DatosDemo.Reportes.Add(reporte);

        MessageBox.Show($"Tu reporte fue registrado con el número {reporte.NumeroReporte}.",
            "Reporte de avería", MessageBoxButtons.OK, MessageBoxIcon.Information);
        txtDescripcion.Clear();
    }
}
