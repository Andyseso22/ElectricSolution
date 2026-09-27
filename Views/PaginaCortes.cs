using ElectricSolution.Data;
using ElectricSolution.Models;

namespace ElectricSolution.Views;

public class PaginaCortes : PaginaBase
{
    public PaginaCortes() : base("Cortes programados", "Interrupciones planificadas por mantenimiento de la red")
    {
        var grid = Tema.Grid();
        grid.Columns.AddRange(
            Tema.Col("Fecha", nameof(CorteProgramado.Fecha), "dd/MM/yyyy", 80),
            Tema.Col("Inicio", nameof(CorteProgramado.HoraInicio), "hh\\:mm", 60),
            Tema.Col("Fin", nameof(CorteProgramado.HoraFin), "hh\\:mm", 60),
            Tema.Col("Zona afectada", nameof(CorteProgramado.Zona), null, 160),
            Tema.Col("Motivo", nameof(CorteProgramado.Descripcion), null, 140));
        grid.DataSource = DatosDemo.Cortes.OrderBy(c => c.Fecha).ToList();

        Contenido.Controls.Add(grid);
    }
}
