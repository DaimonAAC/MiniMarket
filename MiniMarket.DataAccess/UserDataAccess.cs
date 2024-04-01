using System;
using System.Data.SqlClient;

namespace MiniMarket.DataAccess
{
    public class UserDataAccess
    {
        private string connectionString = "Data Source=DAIMON-AQUINO\\MSSQLSERVER01;Initial Catalog=MiniMarket;Integrated Security=True";

        public bool VerifyLogin(string username, string password)
        {
            bool loginSuccessful = false;

            string query = "SELECT COUNT(*) FROM Usuarios WHERE NombreUsuario = @Username AND Contraseña = @Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                try
                {
                    connection.Open();
                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        loginSuccessful = true;
                    }
                }
                catch (Exception ex)
                {
                    // Captura y maneja la excepción, no muestres la ventana de MessageBox aquí
                    // Pero puedes registrar el error en un archivo de registro o en la base de datos
                    // Esto ayudará a depurar problemas sin que el usuario final lo note directamente
                    // También puedes relanzar la excepción si deseas que la capa superior la maneje
                    throw new Exception("Error al intentar iniciar sesión: " + ex.Message);
                }
            }

            return loginSuccessful;
        }
    }
}
