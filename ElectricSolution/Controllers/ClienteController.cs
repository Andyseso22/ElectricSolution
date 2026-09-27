using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ElectricSolution.Models;

namespace ElectricSolution.Controllers
{
    public class ClienteController
    {
       
        public bool Registrar(Cliente c)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    con.Open();
                    string sql = "INSERT INTO Cliente (NombreCliente, Cedula, NumeroNiss, Correo, Telefono, Direccion, Contrasena, Estado) " +
                                 "VALUES (@nombre, @cedula, @niss, @correo, @telefono, @direccion, @contrasena, 'Activo')";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@nombre", c.NombreCliente);
                    cmd.Parameters.AddWithValue("@cedula", c.Cedula);
                    cmd.Parameters.AddWithValue("@niss", c.NumeroNiss);
                    cmd.Parameters.AddWithValue("@correo", c.Correo);
                    cmd.Parameters.AddWithValue("@telefono", c.Telefono);
                    cmd.Parameters.AddWithValue("@direccion", c.Direccion);
                    cmd.Parameters.AddWithValue("@contrasena", c.Contrasena);

                    int filas = cmd.ExecuteNonQuery();
                    if (filas > 0) resultado = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar cliente: " + ex.Message);
            }
            return resultado;
        }

        
        public Cliente Login(string correo, string contrasena)
        {
            Cliente cliente = null;
            try
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    con.Open();
                    string sql = "SELECT * FROM Cliente WHERE Correo = @correo AND Contrasena = @contrasena AND Estado = 'Activo'";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.Parameters.AddWithValue("@contrasena", contrasena);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        cliente = new Cliente();
                        cliente.idCliente = (int)reader["idCliente"];
                        cliente.NombreCliente = reader["NombreCliente"].ToString();
                        cliente.Cedula = reader["Cedula"].ToString();
                        cliente.NumeroNiss = reader["NumeroNiss"].ToString();
                        cliente.Correo = reader["Correo"].ToString();
                        cliente.Telefono = reader["Telefono"].ToString();
                        cliente.Direccion = reader["Direccion"].ToString();
                        cliente.Contrasena = reader["Contrasena"].ToString();
                        cliente.Estado = reader["Estado"].ToString();
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al iniciar sesión: " + ex.Message);
            }
            return cliente;
        }

        public bool ExisteCorreo(string correo)
        {
            bool existe = false;
            try
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    con.Open();
                    string sql = "SELECT COUNT(*) FROM Cliente WHERE Correo = @correo";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@correo", correo);

                    int cantidad = (int)cmd.ExecuteScalar();
                    if (cantidad > 0) existe = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar correo: " + ex.Message);
            }
            return existe;
        }

        
        public List<Cliente> ObtenerTodos()
        {
            List<Cliente> lista = new List<Cliente>();
            try
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    con.Open();
                    string sql = "SELECT * FROM Cliente";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Cliente c = new Cliente();
                        c.idCliente = (int)reader["idCliente"];
                        c.NombreCliente = reader["NombreCliente"].ToString();
                        c.Cedula = reader["Cedula"].ToString();
                        c.NumeroNiss = reader["NumeroNiss"].ToString();
                        c.Correo = reader["Correo"].ToString();
                        c.Telefono = reader["Telefono"].ToString();
                        c.Direccion = reader["Direccion"].ToString();
                        c.Estado = reader["Estado"].ToString();
                        lista.Add(c);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener clientes: " + ex.Message);
            }
            return lista;
        }

        
        public bool Actualizar(Cliente c)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    con.Open();
                    string sql = "UPDATE Cliente SET NombreCliente=@nombre, Telefono=@telefono, Direccion=@direccion " +
                                 "WHERE idCliente=@id";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@nombre", c.NombreCliente);
                    cmd.Parameters.AddWithValue("@telefono", c.Telefono);
                    cmd.Parameters.AddWithValue("@direccion", c.Direccion);
                    cmd.Parameters.AddWithValue("@id", c.idCliente);

                    int filas = cmd.ExecuteNonQuery();
                    if (filas > 0) resultado = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar cliente: " + ex.Message);
            }
            return resultado;
        }

       
        public bool Eliminar(int idCliente)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    con.Open();
                    string sql = "DELETE FROM Cliente WHERE idCliente = @id";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@id", idCliente);

                    int filas = cmd.ExecuteNonQuery();
                    if (filas > 0) resultado = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar cliente: " + ex.Message);
            }
            return resultado;
        }
    }
}
