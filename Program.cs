namespace ElectricSolution;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        // RNF-11: capturador global de errores. Mensaje amigable al usuario;
        // el detalle técnico debería guardarse en un log.
        Application.ThreadException += (s, e) =>
        {
            System.Diagnostics.Debug.WriteLine(e.Exception);
            MessageBox.Show("Ocurrió un problema inesperado. Por favor, intenta de nuevo.",
                "Electric Solution", MessageBoxButtons.OK, MessageBoxIcon.Error);
        };

        Application.Run(new Views.FrmPrincipal());
    }
}
