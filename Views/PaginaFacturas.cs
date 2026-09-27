using ElectricSolution.Data;
using ElectricSolution.Models;

namespace ElectricSolution.Views;

public class PaginaFacturas : PaginaBase
{
    public PaginaFacturas() : base("Facturas", "Consulta tus facturas, su estado y su vencimiento")
    {
        var detalle = new Label
        {
            Text = "Selecciona una factura para ver su detalle.",
            Dock = DockStyle.Bottom,
            Height = 52,
            AutoSize = false,
            BackColor = Color.White,
            ForeColor = Tema.TextoOscuro,
            Padding = new Padding(14, 0, 0, 0),
            TextAlign = ContentAlignment.MiddleLeft
        };

        var grid = Tema.Grid();
        grid.Columns.AddRange(
            Tema.Col("Período", nameof(Factura.Periodo), null, 120),
            Tema.Col("Fecha de emisión", nameof(Factura.FechaEmision), "dd/MM/yyyy"),
            Tema.Col("Monto (C$)", nameof(Factura.Monto), "N2"),
            Tema.Col("Estado", nameof(Factura.Estado)));

        grid.CellFormatting += (s, e) =>
        {
            if (grid.Columns[e.ColumnIndex].Name == nameof(Factura.Estado) && e.Value is string estado)
            {
                e.CellStyle!.ForeColor = estado switch
                {
                    "Pagada" => Tema.Verde,
                    "Vencida" => Tema.Rojo,
                    _ => Tema.TextoOscuro
                };
            }
        };

        grid.SelectionChanged += (s, e) =>
        {
            if (grid.CurrentRow?.DataBoundItem is Factura f)
                detalle.Text = $"Factura {f.Periodo}  |  emitida el {f.FechaEmision:dd/MM/yyyy}  |  {Tema.Moneda(f.Monto)}  |  {f.Estado}";
        };

        grid.DataSource = DatosDemo.Facturas;

        Contenido.Controls.Add(grid);
        Contenido.Controls.Add(detalle);
    }
}
