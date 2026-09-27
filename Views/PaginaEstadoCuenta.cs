using ElectricSolution.Data;

namespace ElectricSolution.Views;

public class PaginaEstadoCuenta : PaginaBase
{
    public PaginaEstadoCuenta() : base("Estado de cuenta", "Tu saldo y los movimientos de tu cuenta")
    {
        var cliente = DatosDemo.Cliente;
        var cuenta = DatosDemo.Cuenta;

        var resumen = new Label
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            BackColor = Color.White,
            ForeColor = Tema.TextoOscuro,
            Padding = new Padding(16, 12, 16, 12),
            Text = $"Titular: {cliente.NombreCliente}\n" +
                   $"NISS: {cliente.NumeroNiss}      Cuenta: {cuenta.NumeroCuenta}\n" +
                   $"Saldo pendiente: {Tema.Moneda(DatosDemo.SaldoPendiente())}\n" +
                   $"Fecha de vencimiento: {cuenta.FechaVencimiento:dd/MM/yyyy}"
        };

        // Facturas (cargos) y pagos (abonos) en una sola lista, del más reciente al más antiguo.
        var movimientos = DatosDemo.Facturas
            .Select(f => new { Fecha = f.FechaEmision, Tipo = "Factura", Detalle = $"Consumo {f.Periodo}", Monto = f.Monto })
            .Concat(DatosDemo.Pagos
                .Select(p => new { Fecha = p.FechaPago, Tipo = "Pago", Detalle = $"Referencia {p.Referencia}", Monto = -p.Monto }))
            .OrderByDescending(m => m.Fecha)
            .ToList();

        var grid = Tema.Grid();
        grid.Columns.AddRange(
            Tema.Col("Fecha", "Fecha", "dd/MM/yyyy"),
            Tema.Col("Tipo", "Tipo", null, 60),
            Tema.Col("Detalle", "Detalle", null, 160),
            Tema.Col("Monto (C$)", "Monto", "N2", 80));
        grid.DataSource = movimientos;

        Contenido.Controls.Add(grid);
        Contenido.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 14 });
        Contenido.Controls.Add(resumen);
    }
}
