using MiniMarket.DataAccess;
using MiniMarket.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MiniMarket.Presentation
{
    public partial class Productos : Form
    {
        private ProductDataAccess productDataAccess;

        public Productos()
        {
            InitializeComponent();
            productDataAccess = new ProductDataAccess();
            CargarProductos(); // Llama al método al inicializar el formulario
        }

        // Método para cargar los productos en el DataGridView u otro control
        private void CargarProductos()
        {
            try
            {
                DataTable dataTable = new DataTable();

                // Utiliza la propiedad ConnectionString para acceder a la cadena de conexión
                using (SqlConnection connection = new SqlConnection(productDataAccess.ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Productos";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(dataTable);
                }

                dataGridViewProductos.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Producto producto = new Producto
                {
                    ProductoID = int.Parse(txtID.Text),
                    Nombre = txtNombre.Text,
                    Descripcion = txtDescripcion.Text,
                    Precio = decimal.Parse(txtPrecio.Text),
                    CantidadStock = int.Parse(txtCantidadStock.Text),
                    Categoria = txtCategoria.Text
                };

                productDataAccess.AgregarProducto(producto);

                // Limpia los campos después de agregar un producto
                LimpiarCampos();

                // Vuelve a cargar los productos en el DataGridView
                CargarProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int idProducto = int.Parse(txtID.Text);
                productDataAccess.EliminarProducto(idProducto);

                // Vuelve a cargar los productos en el DataGridView
                CargarProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Producto producto = new Producto
                {
                    ProductoID = int.Parse(txtID.Text),
                    Nombre = txtNombre.Text,
                    Descripcion = txtDescripcion.Text,
                    Precio = decimal.Parse(txtPrecio.Text),
                    CantidadStock = int.Parse(txtCantidadStock.Text),
                    Categoria = txtCategoria.Text
                };

                productDataAccess.EditarProducto(producto);

                // Limpia los campos después de editar un producto
                LimpiarCampos();

                // Vuelve a cargar los productos en el DataGridView
                CargarProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar el producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para limpiar los campos de entrada de texto
        private void LimpiarCampos()
        {
            txtID.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            txtCantidadStock.Text = "";
            txtCategoria.Text = "";
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
