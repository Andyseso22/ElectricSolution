using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ElectricSolution.Models;

namespace ElectricSolution.Controllers
{
    public class ReporteController
    {
       
        public bool Registrar(Reporte r)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    con.Open();
                    string sql = "INSERT INTO Reporte (TipoReporte, Descripcion, Estado, idCliente) " +
                                 "VALUES (@tipo, @descripcion, 'Recibido', @idCliente)";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@tipo", r.TipoReporte);
                    cmd.Parameters.AddWithValue("@descripcion", r.Descripcion);
                    cmd.Parameters.AddWithValue("@idCliente", r.idCliente);

                    int filas = cmd.ExecuteNonQuery();
                    if (filas > 0) resultado = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar el reporte: " + ex.Message);
            }
            return resultado;
        }

        // Trae los reportes hechos por un cliente (RF-08: Seguimiento de Reportes)
        public List<Reporte> ObtenerPorCliente(int idCliente)
        {
            List<Reporte> lista = new List<Reporte>();
            try
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    con.Open();
                    string sql = "SELECT * FROM Reporte WHERE idCliente = @idCliente ORDER BY FechaReporte DESC";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Reporte r = new Reporte();
                        r.idReporte = (int)reader["idReporte"];
                        r.TipoReporte = reader["TipoReporte"].ToString();
                        r.Descripcion = reader["Descripcion"].ToString();
                        r.FechaReporte = Convert.ToDateTime(reader["FechaReporte"]);
                        r.Estado = reader["Estado"].ToString();
                        r.idCliente = (int)reader["idCliente"];
                        lista.Add(r);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los reportes: " + ex.Message);
            }
            return lista;
        }
    }
}
