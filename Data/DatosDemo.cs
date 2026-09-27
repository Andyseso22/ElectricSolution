using System.ComponentModel;
using ElectricSolution.Models;

namespace ElectricSolution.Data;

/// <summary>
/// Datos de ejemplo en memoria para poder probar el menú sin base de datos.
/// Cuando conecten SQL Server (ADO.NET), reemplacen esta clase por sus Controllers.
/// </summary>
public static class DatosDemo
{
    public static Cliente Cliente { get; } = new()
    {
        IdCliente = 1,
        NombreCliente = "Cliente de Prueba",
        Cedula = "001-010190-0001A",
        NumeroNiss = "1234567",
        Correo = "cliente@correo.com",
        Telefono = "88881234",
        Direccion = "Managua, Nicaragua"
    };

    public static Cuenta Cuenta { get; } = new()
    {
        IdCuenta = 1,
        NumeroCuenta = "CTA-000123",
        FechaVencimiento = DateTime.Today.AddDays(5),
        IdCliente = 1
    };

    public static BindingList<Factura> Facturas { get; } = new()
    {
        new Factura { IdFactura = 1, Periodo = "Julio 2026", Monto = 980.40m, Estado = "Pagada", FechaEmision = new DateTime(2026, 7, 5), IdCuenta = 1 },
        new Factura { IdFactura = 2, Periodo = "Agosto 2026", Monto = 1120.50m, Estado = "Vencida", FechaEmision = new DateTime(2026, 8, 5), IdCuenta = 1 },
        new Factura { IdFactura = 3, Periodo = "Septiembre 2026", Monto = 1245.80m, Estado = "Pendiente", FechaEmision = new DateTime(2026, 9, 5), IdCuenta = 1 }
    };

    public static BindingList<Pago> Pagos { get; } = new()
    {
        new Pago { IdPago = 1, FechaPago = new DateTime(2026, 7, 18, 10, 30, 0), Monto = 980.40m, MetodoPago = "Tarjeta de débito", Referencia = "REF-20260718103000", IdFactura = 1 }
    };

    public static BindingList<Reporte> Reportes { get; } = new()
    {
        new Reporte { IdReporte = 1, TipoReporte = "Sin suministro de energía", Descripcion = "Sin energía desde la madrugada en mi cuadra.", FechaReporte = DateTime.Today.AddDays(-5), Estado = "Resuelto", IdCliente = 1 }
    };

    public static List<CorteProgramado> Cortes { get; } = new()
    {
        new CorteProgramado { IdCorte = 1, Fecha = DateTime.Today.AddDays(3), HoraInicio = new TimeSpan(8, 0, 0), HoraFin = new TimeSpan(12, 0, 0), Zona = "Barrio Altagracia, Managua", Descripcion = "Mantenimiento de red" },
        new CorteProgramado { IdCorte = 2, Fecha = DateTime.Today.AddDays(9), HoraInicio = new TimeSpan(13, 0, 0), HoraFin = new TimeSpan(16, 0, 0), Zona = "Ciudad Sandino", Descripcion = "Cambio de transformador" }
    };

    public static decimal SaldoPendiente() =>
        Facturas.Where(f => f.Estado != "Pagada").Sum(f => f.Monto);
}
