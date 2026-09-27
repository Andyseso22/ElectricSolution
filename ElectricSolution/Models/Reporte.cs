using System;

namespace ElectricSolution.Models
{
    public class Reporte
    {
        public int idReporte { get; set; }
        public string TipoReporte { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaReporte { get; set; }
        public string Estado { get; set; }
        public int idCliente { get; set; }
    }
}
