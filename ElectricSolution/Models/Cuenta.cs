using System;

namespace ElectricSolution.Models
{
    public class Cuenta
    {
        public int idCuenta { get; set; }
        public string NumeroCuenta { get; set; }
        public decimal Saldo { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int idCliente { get; set; }
    }
}
