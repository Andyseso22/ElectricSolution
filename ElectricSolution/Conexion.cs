using System.Data.SqlClient;

namespace ElectricSolution
{
    
    public class Conexion
    {
        
        private static string cadena = "Server=.\\SQLEXPRESS;Database=ElectricSolutionDB;Integrated Security=True;";

        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadena);
        }
    }
}
