using MiniMarket.DataAccess;
using MiniMarket.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniMarket.Presentation
{
    public partial class Factura : Form
    {
        private ClienteDataAccess clienteDataAccess;
        private EmpleadoDataAccess empleadoDataAccess;
        private ProductDataAccess productoDataAccess;
        private string stockProducto;
        public Factura()
        {
            InitializeComponent();
            clienteDataAccess = new ClienteDataAccess();
            empleadoDataAccess = new EmpleadoDataAccess();
            productoDataAccess = new ProductDataAccess();
            txtEfectivo.TextChanged += txtEfectivo_TextChanged;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int clienteID = int.Parse(txtClienteID.Text);
                Cliente cliente = clienteDataAccess.BuscarClientePorID(clienteID);

                if (cliente != null)
                {
                    txtNombreCliente.Text = cliente.Nombre;
                }
                else
                {
                    MessageBox.Show("Cliente no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int empleadoID = int.Parse(txtEmpleadoID.Text);
                Empleado empleado = empleadoDataAccess.BuscarEmpleadoPorID(empleadoID);

                if (empleado != null)
                {
                    txtNombreEmpleado.Text = empleado.Nombre;
                }
                else
                {
                    MessageBox.Show("Empleado no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar empleado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int productoID = int.Parse(txtProductoID.Text);
                Producto producto = productoDataAccess.BuscarProductoPorID(productoID);

                if (producto != null)
                {
                    txtNombreProducto.Text = producto.Nombre;
                    txtDescripcion.Text = producto.Descripcion;
                    txtPrecio.Text = producto.Precio.ToString();
                    // No llenamos automáticamente el textbox de stock
                    stockProducto = producto.CantidadStock.ToString();
                }
                else
                {
                    MessageBox.Show("Producto no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener los valores de los TextBox
                string productoID = txtProductoID.Text;
                string nombreProducto = txtNombreProducto.Text;
                string descripcion = txtDescripcion.Text;
                string precio = txtPrecio.Text;
                string stockProducto = txtCantidadStock.Text; // Usar el valor ingresado manualmente

                // Agregar las columnas al DataGridView si no se han agregado
                if (dataGridViewFactura.Columns.Count == 0)
                {
                    dataGridViewFactura.Columns.Add("ProductoID", "ProductoID");
                    dataGridViewFactura.Columns.Add("Nombre", "Nombre");
                    dataGridViewFactura.Columns.Add("Descripcion", "Descripción");
                    dataGridViewFactura.Columns.Add("Precio", "Precio");
                    dataGridViewFactura.Columns.Add("CantidadStock", "Cantidad en Stock");
                }

                // Agregar fila al DataGridView con los valores obtenidos de los TextBox
                dataGridViewFactura.Rows.Add(productoID, nombreProducto, descripcion, precio, stockProducto);

                // Calcular el total a pagar y actualizar el TextBox correspondiente en el panel 2
                CalcularTotalAPagar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar lista: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalcularTotalAPagar()
        {
            decimal totalAPagar = 0;

            foreach (DataGridViewRow row in dataGridViewFactura.Rows)
            {
                decimal precio = Convert.ToDecimal(row.Cells["Precio"].Value);
                decimal cantidad = Convert.ToDecimal(row.Cells["CantidadStock"].Value);
                totalAPagar += precio * cantidad;
            }

            // Actualizar el TextBox correspondiente en el panel 2
            txtTotalPagar.Text = totalAPagar.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar si hay filas seleccionadas
                if (dataGridViewFactura.SelectedRows.Count > 0)
                {
                    // Obtener el ID de la fila seleccionada
                    int idSeleccionado = Convert.ToInt32(dataGridViewFactura.SelectedRows[0].Cells["ProductoID"].Value);

                    // Buscar y eliminar la fila con el ID seleccionado
                    foreach (DataGridViewRow row in dataGridViewFactura.Rows)
                    {
                        if (Convert.ToInt32(row.Cells["ProductoID"].Value) == idSeleccionado)
                        {
                            dataGridViewFactura.Rows.Remove(row);
                            break; // Salir del bucle una vez eliminada la fila
                        }
                    }

                    // Recalcular el total a pagar después de eliminar la fila
                    CalcularTotalAPagar();
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione una fila para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar fila: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtEfectivo_TextChanged(object sender, EventArgs e)
        {
            CalcularDevolucion();
        }

        private void CalcularDevolucion()
        {
            try
            {
                decimal totalAPagar = Convert.ToDecimal(txtTotalPagar.Text);
                decimal efectivo = Convert.ToDecimal(txtEfectivo.Text);
                decimal devolucion = efectivo - totalAPagar;
                txtDevolucion.Text = devolucion.ToString();
            }
            catch (FormatException)
            {
                // Si hay un error al convertir los valores, dejar el TextBox de devolución en blanco
                txtDevolucion.Text = "";
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Crear una instancia de SaveFileDialog para que el usuario pueda elegir dónde guardar la factura
                SaveFileDialog saveFileDialog1 = new SaveFileDialog
                {
                    Filter = "Archivo de texto|*.txt",
                    Title = "Guardar factura",
                    FileName = "Factura.txt"
                };

                // Mostrar el diálogo para guardar archivo
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    // Obtener la ruta del archivo seleccionado por el usuario
                    string filePath = saveFileDialog1.FileName;

                    // Crear un nuevo archivo de texto para guardar la factura
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        // Escribir encabezado de la factura
                        writer.WriteLine("************ FACTURA ************");
                        writer.WriteLine($"Fecha: {DateTime.Now}");
                        writer.WriteLine($"Cliente: {txtNombreCliente.Text}");
                        writer.WriteLine($"Empleado: {txtNombreEmpleado.Text}");
                        writer.WriteLine("---------------------------------");

                        // Escribir detalles de los productos comprados
                        writer.WriteLine("Productos:");

                        foreach (DataGridViewRow row in dataGridViewFactura.Rows)
                        {
                            string producto = $"{row.Cells["Nombre"].Value}, {row.Cells["Descripcion"].Value}, Precio: {row.Cells["Precio"].Value}, Cantidad: {row.Cells["CantidadStock"].Value}";
                            writer.WriteLine(producto);
                        }

                        writer.WriteLine("---------------------------------");

                        // Escribir total a pagar y efectivo entregado
                        writer.WriteLine($"Total a pagar: {txtTotalPagar.Text}");
                        writer.WriteLine($"Efectivo entregado: {txtEfectivo.Text}");
                        writer.WriteLine($"Devolución: {txtDevolucion.Text}");

                        writer.WriteLine("---------------------------------");

                        writer.WriteLine("¡Gracias por su compra!");
                    }

                    MessageBox.Show("Factura guardada exitosamente.", "Factura Guardada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al imprimir factura: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
