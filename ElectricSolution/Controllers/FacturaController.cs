using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ElectricSolution.Models;

namespace ElectricSolution.Controllers
{
    public class FacturaController
    {
       
        public List<Factura> ObtenerPorCuenta(int idCuenta)
        {
            List<Factura> lista = new List<Factura>();
            try
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    con.Open();
                    string sql = "SELECT * FROM Factura WHERE idCuenta = @idCuenta ORDER BY FechaEmision DESC";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@idCuenta", idCuenta);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Factura f = new Factura();
                        f.idFactura = (int)reader["idFactura"];
                        f.Periodo = reader["Periodo"].ToString();
                        f.Monto = Convert.ToDecimal(reader["Monto"]);
                        f.Estado = reader["Estado"].ToString();
                        f.FechaEmision = Convert.ToDateTime(reader["FechaEmision"]);
                        f.idCuenta = (int)reader["idCuenta"];
                        lista.Add(f);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las facturas: " + ex.Message);
            }
            return lista;
        }

        // Marca una factura como pagada y registra el pago (RF-06: Realizacion de Pagos)
        public bool PagarFactura(int idFactura, string metodoPago)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    con.Open();

                    // 1. Traemos el monto de la factura
                    string sqlMonto = "SELECT Monto FROM Factura WHERE idFactura = @id";
                    SqlCommand cmdMonto = new SqlCommand(sqlMonto, con);
                    cmdMonto.Parameters.AddWithValue("@id", idFactura);
                    decimal monto = Convert.ToDecimal(cmdMonto.ExecuteScalar());

                    // 2. Insertamos el pago
                    string sqlPago = "INSERT INTO Pago (Monto, MetodoPago, Referencia, idFactura) " +
                                      "VALUES (@monto, @metodo, @referencia, @idFactura)";
                    SqlCommand cmdPago = new SqlCommand(sqlPago, con);
                    cmdPago.Parameters.AddWithValue("@monto", monto);
                    cmdPago.Parameters.AddWithValue("@metodo", metodoPago);
                    cmdPago.Parameters.AddWithValue("@referencia", "REF-" + DateTime.Now.Ticks);
                    cmdPago.Parameters.AddWithValue("@idFactura", idFactura);
                    cmdPago.ExecuteNonQuery();

                    // 3. Actualizamos el estado de la factura a "Pagada"
                    string sqlUpdate = "UPDATE Factura SET Estado = 'Pagada' WHERE idFactura = @id";
                    SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, con);
                    cmdUpdate.Parameters.AddWithValue("@id", idFactura);
                    int filas = cmdUpdate.ExecuteNonQuery();

                    if (filas > 0) resultado = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar el pago: " + ex.Message);
            }
            return resultado;
        }
    }
}
