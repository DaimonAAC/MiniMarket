using MiniMarket.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MiniMarket.DataAccess
{
    public class ClienteDataAccess
    {
        private string connectionString = "Data Source=DAIMON-AQUINO\\MSSQLSERVER01;Initial Catalog=MiniMarket;Integrated Security=True";

        // Propiedad para acceder al connectionString
        public string ConnectionString
        {
            get { return connectionString; }
        }

        public Cliente BuscarClientePorID(int clienteID)
        {
            Cliente cliente = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Clientes WHERE ClienteID = @ClienteID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ClienteID", clienteID);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cliente = new Cliente
                                {
                                    ClienteID = Convert.ToInt32(reader["ClienteID"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    Direccion = reader["Direccion"].ToString(),
                                    Telefono = reader["Telefono"].ToString(),
                                    CorreoElectronico = reader["CorreoElectronico"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar cliente por ID: " + ex.Message);
            }
            return cliente;
        }

        // Método para agregar un nuevo cliente
        public void AgregarCliente(Cliente cliente)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Clientes (ClienteID, Nombre, Direccion, Telefono, CorreoElectronico) VALUES (@ClienteID, @Nombre, @Direccion, @Telefono, @CorreoElectronico)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ClienteID", cliente.ClienteID);
                        command.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                        command.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                        command.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                        command.Parameters.AddWithValue("@CorreoElectronico", cliente.CorreoElectronico);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar cliente: " + ex.Message);
            }
        }

        // Método para eliminar un cliente
        public void EliminarCliente(int idCliente)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Clientes WHERE ClienteID = @ClienteID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ClienteID", idCliente);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar cliente: " + ex.Message);
            }
        }

        // Método para editar un cliente
        public void EditarCliente(Cliente cliente)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Clientes SET Nombre = @Nombre, Direccion = @Direccion, Telefono = @Telefono, CorreoElectronico = @CorreoElectronico WHERE ClienteID = @ClienteID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                        command.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                        command.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                        command.Parameters.AddWithValue("@CorreoElectronico", cliente.CorreoElectronico);
                        command.Parameters.AddWithValue("@ClienteID", cliente.ClienteID);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar cliente: " + ex.Message);
            }
        }
    }
}
