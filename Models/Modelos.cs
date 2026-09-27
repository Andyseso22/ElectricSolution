namespace ElectricSolution.Models;

// Las clases siguen las tablas del diagrama de base de datos del informe.

public class Cliente
{
    public int IdCliente { get; set; }
    public string NombreCliente { get; set; } = "";
    public string Cedula { get; set; } = "";
    public string NumeroNiss { get; set; } = "";
    public string Correo { get; set; } = "";
    public string Telefono { get; set; } = "";
    public string Direccion { get; set; } = "";
}

public class Cuenta
{
    public int IdCuenta { get; set; }
    public string NumeroCuenta { get; set; } = "";
    public DateTime FechaVencimiento { get; set; }
    public int IdCliente { get; set; }
}

public class Factura
{
    public int IdFactura { get; set; }
    public string Periodo { get; set; } = "";
    public decimal Monto { get; set; }
    public string Estado { get; set; } = "Pendiente";   // Pendiente | Vencida | Pagada
    public DateTime FechaEmision { get; set; }
    public int IdCuenta { get; set; }

    public string Descripcion => $"{Periodo} - C$ {Monto:N2} ({Estado})";
}

public class Pago
{
    public int IdPago { get; set; }
    public DateTime FechaPago { get; set; }
    public decimal Monto { get; set; }
    public string MetodoPago { get; set; } = "";
    public string Referencia { get; set; } = "";
    public int IdFactura { get; set; }
}

public class Reporte
{
    public int IdReporte { get; set; }
    public string TipoReporte { get; set; } = "";
    public string Descripcion { get; set; } = "";
    public DateTime FechaReporte { get; set; }
    public string Estado { get; set; } = "Recibido";    // Recibido | En asignación | Cuadrilla en camino | Resuelto
    public int IdCliente { get; set; }

    public string NumeroReporte => $"AV-{IdReporte:D4}";
}

public class CorteProgramado
{
    public int IdCorte { get; set; }
    public DateTime Fecha { get; set; }
    public TimeSpan HoraInicio { get; set; }
    public TimeSpan HoraFin { get; set; }
    public string Zona { get; set; } = "";
    public string Descripcion { get; set; } = "";
}
