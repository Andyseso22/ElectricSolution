using System;
using System.Data.SqlClient;
using ElectricSolution.Models;

namespace ElectricSolution.Controllers
{
    public class CuentaController
    {
       
        public Cuenta ObtenerPorCliente(int idCliente)
        {
            Cuenta cuenta = null;
            try
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    con.Open();
                    string sql = "SELECT * FROM Cuenta WHERE idCliente = @idCliente";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        cuenta = new Cuenta();
                        cuenta.idCuenta = (int)reader["idCuenta"];
                        cuenta.NumeroCuenta = reader["NumeroCuenta"].ToString();
                        cuenta.Saldo = Convert.ToDecimal(reader["Saldo"]);
                        cuenta.FechaVencimiento = Convert.ToDateTime(reader["FechaVencimiento"]);
                        cuenta.idCliente = (int)reader["idCliente"];
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar la cuenta: " + ex.Message);
            }
            return cuenta;
        }
    }
}
