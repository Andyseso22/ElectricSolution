using System;

namespace ElectricSolution.Models
{
    public class Factura
    {
        public int idFactura { get; set; }
        public string Periodo { get; set; }
        public decimal Monto { get; set; }
        public string Estado { get; set; }
        public DateTime FechaEmision { get; set; }
        public int idCuenta { get; set; }
    }
}
