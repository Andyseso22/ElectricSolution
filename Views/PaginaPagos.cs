using ElectricSolution.Data;
using ElectricSolution.Models;

namespace ElectricSolution.Views;

public class PaginaPagos : PaginaBase
{
    private readonly ComboBox cboFactura = new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 340 };
    private readonly ComboBox cboMetodo = new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 340 };
    private readonly Label lblMonto = new() { AutoSize = true, ForeColor = Tema.Azul, Font = new Font("Segoe UI Semibold", 20f) };
    private readonly Button btnPagar = Tema.Boton("Pagar ahora", 200);

    public PaginaPagos() : base("Sistema de pago", "Paga tus facturas de forma rápida y segura")
    {
        cboMetodo.Items.AddRange(new object[] { "Tarjeta de crédito", "Tarjeta de débito", "Billetera digital", "Banca en línea" });
        cboMetodo.SelectedIndex = 0;
        cboFactura.SelectedIndexChanged += (s, e) => ActualizarMonto();
        btnPagar.Margin = new Padding(0, 16, 0, 0);
        btnPagar.Click += (s, e) => Pagar();

        var izquierda = Tema.Columna();
        izquierda.Controls.Add(Tema.Etiqueta("Factura a pagar"));
        izquierda.Controls.Add(cboFactura);
        izquierda.Controls.Add(Tema.Etiqueta("Método de pago"));
        izquierda.Controls.Add(cboMetodo);

        var derecha = Tema.Columna();
        derecha.Controls.Add(Tema.Etiqueta("Monto a pagar"));
        derecha.Controls.Add(lblMonto);
        derecha.Controls.Add(btnPagar);

        var formulario = Tema.Formulario();
        formulario.Controls.Add(izquierda);
        formulario.Controls.Add(derecha);

        var lblHistorial = new Label
        {
            Text = "Historial de pagos",
            Dock = DockStyle.Top,
            Height = 46,
            AutoSize = false,
            TextAlign = ContentAlignment.BottomLeft,
            Font = new Font("Segoe UI Semibold", 12f),
            ForeColor = Tema.Azul
        };

        var grid = Tema.Grid();
        grid.Columns.AddRange(
            Tema.Col("Fecha", nameof(Pago.FechaPago), "dd/MM/yyyy HH:mm"),
            Tema.Col("Monto (C$)", nameof(Pago.Monto), "N2", 70),
            Tema.Col("Método", nameof(Pago.MetodoPago)),
            Tema.Col("Referencia", nameof(Pago.Referencia), null, 130));
        grid.DataSource = DatosDemo.Pagos;

        Contenido.Controls.Add(grid);
        Contenido.Controls.Add(lblHistorial);
        Contenido.Controls.Add(formulario);

        CargarPendientes();
    }

    private void CargarPendientes()
    {
        var pendientes = DatosDemo.Facturas.Where(f => f.Estado != "Pagada").ToList();
        cboFactura.DisplayMember = nameof(Factura.Descripcion);
        cboFactura.DataSource = pendientes;
        btnPagar.Enabled = pendientes.Count > 0;
        ActualizarMonto();
    }

    private void ActualizarMonto() =>
        lblMonto.Text = Tema.Moneda(cboFactura.SelectedItem is Factura f ? f.Monto : 0m);

    private void Pagar()
    {
        if (cboFactura.SelectedItem is not Factura factura)
        {
            MessageBox.Show("No tienes facturas pendientes de pago.", "Sistema de pago",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var metodo = cboMetodo.SelectedItem?.ToString() ?? "";
        var confirmar = MessageBox.Show(
            $"¿Confirmas el pago de {Tema.Moneda(factura.Monto)} con {metodo}?",
            "Confirmar pago", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (confirmar != DialogResult.Yes) return;

        // Simulación: aquí iría la pasarela de pago y el INSERT en SQL Server.
        var pago = new Pago
        {
            IdPago = DatosDemo.Pagos.Count + 1,
            FechaPago = DateTime.Now,
            Monto = factura.Monto,
            MetodoPago = metodo,
            Referencia = $"REF-{DateTime.Now:yyyyMMddHHmmss}",
            IdFactura = factura.IdFactura
        };
        DatosDemo.Pagos.Add(pago);
        factura.Estado = "Pagada";

        MessageBox.Show($"Pago realizado con éxito.\nReferencia: {pago.Referencia}", "Sistema de pago",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
        CargarPendientes();
    }
}
