using MiniMarket.DataAccess;
using MiniMarket.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MiniMarket.Presentation
{
    public partial class Clientes : Form
    {
        private ClienteDataAccess clienteDataAccess;
        public Clientes()
        {
            InitializeComponent();
            clienteDataAccess = new ClienteDataAccess();
            CargarClientes();
        }

        private void CargarClientes()
        {
            try
            {
                DataTable dataTable = new DataTable();

                // Utiliza la propiedad ConnectionString para acceder a la cadena de conexión
                using (SqlConnection connection = new SqlConnection(clienteDataAccess.ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Clientes";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(dataTable);
                }

                dataGridViewClientes.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los clientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Cliente cliente = new Cliente
                {
                    // Asigna los valores de los campos del formulario a las propiedades del objeto Cliente
                    ClienteID = int.Parse(txtID.Text),
                    Nombre = txtNombre.Text,
                    Direccion = txtDireccion.Text,
                    Telefono = txtTelefono.Text,
                    CorreoElectronico = txtCorreoElectronico.Text
                };

                clienteDataAccess.AgregarCliente(cliente); // Método para agregar un cliente a la base de datos

                // Limpia los campos después de agregar un cliente
                LimpiarCampos();

                // Vuelve a cargar los clientes en el DataGridView
                CargarClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int idCliente = int.Parse(txtID.Text);
                clienteDataAccess.EliminarCliente(idCliente);

                // Vuelve a cargar los productos en el DataGridView
                CargarClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Cliente cliente = new Cliente
                {
                    ClienteID = int.Parse(txtID.Text),
                    Nombre = txtNombre.Text,
                    Direccion = txtDireccion.Text,
                    Telefono = txtTelefono.Text,
                    CorreoElectronico = txtCorreoElectronico.Text
                };

                clienteDataAccess.EditarCliente(cliente);

                // Limpia los campos después de editar un producto
                LimpiarCampos();

                // Vuelve a cargar los productos en el DataGridView
                CargarClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar el cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LimpiarCampos()
        {
            txtID.Text = "";
            txtNombre.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            txtCorreoElectronico.Text = "";
        }
    }
}
