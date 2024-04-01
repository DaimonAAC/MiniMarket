using System;
using System.Data;
using System.Data.SqlClient;
using MiniMarket.Models;

namespace MiniMarket.DataAccess
{
    public class ProveedorDataAccess
    {
        private string connectionString = "Data Source=DAIMON-AQUINO\\MSSQLSERVER01;Initial Catalog=MiniMarket;Integrated Security=True";

        public string ConnectionString
        {
            get { return connectionString; }
        }

        public void AgregarProveedor(Proveedor proveedor)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Proveedores (ProveedorID, Nombre, Direccion, Telefono, CorreoElectronico) VALUES (@ProveedorID, @Nombre, @Direccion, @Telefono, @CorreoElectronico)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProveedorID", proveedor.ProveedorID);
                        command.Parameters.AddWithValue("@Nombre", proveedor.Nombre);
                        command.Parameters.AddWithValue("@Direccion", proveedor.Direccion);
                        command.Parameters.AddWithValue("@Telefono", proveedor.Telefono);
                        command.Parameters.AddWithValue("@CorreoElectronico", proveedor.CorreoElectronico);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar proveedor: " + ex.Message);
            }
        }

        public void EliminarProveedor(int idProveedor)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Proveedores WHERE ProveedorID = @ProveedorID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProveedorID", idProveedor);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar proveedor: " + ex.Message);
            }
        }

        public void EditarProveedor(Proveedor proveedor)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Proveedores SET Nombre = @Nombre, Direccion = @Direccion, Telefono = @Telefono, CorreoElectronico = @CorreoElectronico WHERE ProveedorID = @ProveedorID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Nombre", proveedor.Nombre);
                        command.Parameters.AddWithValue("@Direccion", proveedor.Direccion);
                        command.Parameters.AddWithValue("@Telefono", proveedor.Telefono);
                        command.Parameters.AddWithValue("@CorreoElectronico", proveedor.CorreoElectronico);
                        command.Parameters.AddWithValue("@ProveedorID", proveedor.ProveedorID);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar proveedor: " + ex.Message);
            }
        }
    }
}
