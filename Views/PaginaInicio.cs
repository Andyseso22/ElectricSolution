using ElectricSolution.Data;

namespace ElectricSolution.Views;

public class PaginaInicio : PaginaBase
{
    public PaginaInicio()
        : base($"Bienvenido, {DatosDemo.Cliente.NombreCliente}", "Resumen de tu servicio eléctrico")
    {
        var saldo = DatosDemo.SaldoPendiente();
        var porPagar = DatosDemo.Facturas.Count(f => f.Estado != "Pagada");
        var proximoCorte = DatosDemo.Cortes
            .Where(c => c.Fecha >= DateTime.Today)
            .OrderBy(c => c.Fecha)
            .FirstOrDefault();

        var aviso = new Label
        {
            Dock = DockStyle.Top,
            Height = 64,
            AutoSize = false,
            BackColor = ColorTranslator.FromHtml("#FFF7DB"),
            Padding = new Padding(14, 0, 14, 0),
            TextAlign = ContentAlignment.MiddleLeft,
            Text = proximoCorte is null
                ? "No hay cortes programados próximamente."
                : $"Corte programado: {proximoCorte.Fecha:dd/MM/yyyy}, de {proximoCorte.HoraInicio:hh\\:mm} a {proximoCorte.HoraFin:hh\\:mm} en {proximoCorte.Zona}."
        };

        var tarjetas = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink
        };
        tarjetas.Controls.Add(Tema.Tarjeta("Saldo pendiente", Tema.Moneda(saldo), saldo > 0 ? Tema.Rojo : Tema.Verde));
        tarjetas.Controls.Add(Tema.Tarjeta("Próximo vencimiento", DatosDemo.Cuenta.FechaVencimiento.ToString("dd/MM/yyyy"), Tema.Amarillo));
        tarjetas.Controls.Add(Tema.Tarjeta("Facturas por pagar", porPagar.ToString(), Tema.AzulActivo));

        Contenido.Controls.Add(aviso);
        Contenido.Controls.Add(tarjetas);
    }
}
