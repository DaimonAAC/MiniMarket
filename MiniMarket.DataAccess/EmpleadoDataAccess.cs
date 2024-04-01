using MiniMarket.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MiniMarket.DataAccess
{
    public class EmpleadoDataAccess
    {
        private string connectionString = "Data Source=DAIMON-AQUINO\\MSSQLSERVER01;Initial Catalog=MiniMarket;Integrated Security=True";

        public string ConnectionString
        {
            get { return connectionString; }
        }

        public Empleado BuscarEmpleadoPorID(int empleadoID)
        {
            Empleado empleado = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Empleados WHERE EmpleadoID = @EmpleadoID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EmpleadoID", empleadoID);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                empleado = new Empleado
                                {
                                    EmpleadoID = Convert.ToInt32(reader["EmpleadoID"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    Direccion = reader["Direccion"].ToString(),
                                    Telefono = reader["Telefono"].ToString(),
                                    CorreoElectronico = reader["CorreoElectronico"].ToString(),
                                    Cargo = reader["Cargo"].ToString(),
                                    Salario = Convert.ToDecimal(reader["Salario"]),
                                    FechaContratacion = Convert.ToDateTime(reader["FechaContratacion"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar empleado por ID: " + ex.Message);
            }
            return empleado;
        }

        public void AgregarEmpleado(Empleado empleado)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Empleados (EmpleadoID, Nombre, Direccion, Telefono, CorreoElectronico, Cargo, Salario, FechaContratacion) " +
                                   "VALUES (@EmpleadoID, @Nombre, @Direccion, @Telefono, @CorreoElectronico, @Cargo, @Salario, @FechaContratacion)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EmpleadoID", empleado.EmpleadoID);
                        command.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                        command.Parameters.AddWithValue("@Direccion", empleado.Direccion);
                        command.Parameters.AddWithValue("@Telefono", empleado.Telefono);
                        command.Parameters.AddWithValue("@CorreoElectronico", empleado.CorreoElectronico);
                        command.Parameters.AddWithValue("@Cargo", empleado.Cargo);
                        command.Parameters.AddWithValue("@Salario", empleado.Salario);
                        command.Parameters.AddWithValue("@FechaContratacion", empleado.FechaContratacion);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar empleado: " + ex.Message);
            }
        }

        public void EliminarEmpleado(int idEmpleado)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Empleados WHERE EmpleadoID = @EmpleadoID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EmpleadoID", idEmpleado);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar empleado: " + ex.Message);
            }
        }

        public void EditarEmpleado(Empleado empleado)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Empleados SET Nombre = @Nombre, Direccion = @Direccion, Telefono = @Telefono, CorreoElectronico = @CorreoElectronico, " +
                                   "Cargo = @Cargo, Salario = @Salario, FechaContratacion = @FechaContratacion WHERE EmpleadoID = @EmpleadoID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                        command.Parameters.AddWithValue("@Direccion", empleado.Direccion);
                        command.Parameters.AddWithValue("@Telefono", empleado.Telefono);
                        command.Parameters.AddWithValue("@CorreoElectronico", empleado.CorreoElectronico);
                        command.Parameters.AddWithValue("@Cargo", empleado.Cargo);
                        command.Parameters.AddWithValue("@Salario", empleado.Salario);
                        command.Parameters.AddWithValue("@FechaContratacion", empleado.FechaContratacion);
                        command.Parameters.AddWithValue("@EmpleadoID", empleado.EmpleadoID);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar empleado: " + ex.Message);
            }
        }
    }
}
