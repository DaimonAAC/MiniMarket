using MiniMarket.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MiniMarket.DataAccess
{
    public class ProductDataAccess
    {
        private string connectionString = "Data Source=DAIMON-AQUINO\\MSSQLSERVER01;Initial Catalog=MiniMarket;Integrated Security=True";

        // Propiedad para acceder al connectionString
        public string ConnectionString
        {
            get { return connectionString; }
        }

        public Producto BuscarProductoPorID(int productoID)
        {
            Producto producto = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Productos WHERE ProductoID = @ProductoID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductoID", productoID);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                producto = new Producto
                                {
                                    ProductoID = Convert.ToInt32(reader["ProductoID"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    Descripcion = reader["Descripcion"].ToString(),
                                    Precio = Convert.ToDecimal(reader["Precio"]),
                                    CantidadStock = Convert.ToInt32(reader["CantidadStock"]),
                                    Categoria = reader["Categoria"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar producto por ID: " + ex.Message);
            }
            return producto;
        }

        // Método para agregar un nuevo producto
        public void AgregarProducto(Producto producto)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Productos (ProductoID, Nombre, Descripcion, Precio, CantidadStock, Categoria) VALUES (@ProductoID, @Nombre, @Descripcion, @Precio, @CantidadStock, @Categoria)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductoID", producto.ProductoID);
                        command.Parameters.AddWithValue("@Nombre", producto.Nombre);
                        command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                        command.Parameters.AddWithValue("@Precio", producto.Precio);
                        command.Parameters.AddWithValue("@CantidadStock", producto.CantidadStock);
                        command.Parameters.AddWithValue("@Categoria", producto.Categoria);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Registra la excepción o maneja de otra manera
                throw new Exception("Error al agregar producto: " + ex.Message);
            }
        }

        // Método para eliminar un producto
        public void EliminarProducto(int idProducto)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Productos WHERE ProductoID = @ProductoID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductoID", idProducto);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Registra la excepción o maneja de otra manera
                throw new Exception("Error al eliminar producto: " + ex.Message);
            }
        }

        // Método para editar un producto
        public void EditarProducto(Producto producto)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Productos SET Nombre = @Nombre, Descripcion = @Descripcion, Precio = @Precio, CantidadStock = @CantidadStock, Categoria = @Categoria WHERE ProductoID = @ProductoID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Nombre", producto.Nombre);
                        command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                        command.Parameters.AddWithValue("@Precio", producto.Precio);
                        command.Parameters.AddWithValue("@CantidadStock", producto.CantidadStock);
                        command.Parameters.AddWithValue("@Categoria", producto.Categoria);
                        command.Parameters.AddWithValue("@ProductoID", producto.ProductoID);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Registra la excepción o maneja de otra manera
                throw new Exception("Error al editar producto: " + ex.Message);
            }
        }
    }
}
