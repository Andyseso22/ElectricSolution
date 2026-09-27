namespace ElectricSolution.Views;

/// <summary>Base de todas las pantallas del menú: título, subtítulo y área de contenido.</summary>
public abstract class PaginaBase : UserControl
{
    protected Panel Contenido { get; } = new()
    {
        Dock = DockStyle.Fill,
        Padding = new Padding(0, 8, 0, 0)
    };

    protected PaginaBase(string titulo, string subtitulo)
    {
        Dock = DockStyle.Fill;
        BackColor = Tema.Fondo;
        Padding = new Padding(28, 20, 28, 20);

        var lblTitulo = new Label
        {
            Text = titulo,
            Dock = DockStyle.Top,
            Height = 44,
            AutoSize = false,
            Font = new Font("Segoe UI Semibold", 18f),
            ForeColor = Tema.Azul
        };
        var lblSubtitulo = new Label
        {
            Text = subtitulo,
            Dock = DockStyle.Top,
            Height = 30,
            AutoSize = false,
            ForeColor = Tema.TextoSuave
        };

        Controls.Add(Contenido);
        Controls.Add(lblSubtitulo);
        Controls.Add(lblTitulo);
    }
}
