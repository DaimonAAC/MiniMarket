using MiniMarket.DataAccess;
using MiniMarket.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniMarket.Presentation
{
    public partial class Empleados : Form
    {
        private EmpleadoDataAccess empleadoDataAccess;
        public Empleados()
        {
            InitializeComponent();
            empleadoDataAccess = new EmpleadoDataAccess();
            CargarEmpleados();
        }

        private void CargarEmpleados()
        {
            try
            {
                DataTable dataTable = new DataTable();

                using (SqlConnection connection = new SqlConnection(empleadoDataAccess.ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Empleados";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(dataTable);
                }

                dataGridViewEmpleados.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los empleados: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                Empleado empleado = new Empleado
                {
                    EmpleadoID = int.Parse(txtID.Text),
                    Nombre = txtNombre.Text,
                    Direccion = txtDireccion.Text,
                    Telefono = txtTelefono.Text,
                    CorreoElectronico = txtCorreoElectronico.Text,
                    Cargo = txtCargo.Text,
                    Salario = decimal.Parse(txtSalario.Text),
                    FechaContratacion = DateTime.Parse(dateFechaContratacion.Text)
                };

                empleadoDataAccess.AgregarEmpleado(empleado);
                LimpiarCampos();
                CargarEmpleados();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el empleado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int idEmpleado = int.Parse(txtID.Text);
                empleadoDataAccess.EliminarEmpleado(idEmpleado);
                CargarEmpleados();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el empleado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Empleado empleado = new Empleado
                {
                    EmpleadoID = int.Parse(txtID.Text),
                    Nombre = txtNombre.Text,
                    Direccion = txtDireccion.Text,
                    Telefono = txtTelefono.Text,
                    CorreoElectronico = txtCorreoElectronico.Text,
                    Cargo = txtCargo.Text,
                    Salario = decimal.Parse(txtSalario.Text),
                    FechaContratacion = DateTime.Parse(dateFechaContratacion.Text)
                };

                empleadoDataAccess.EditarEmpleado(empleado);
                LimpiarCampos();
                CargarEmpleados();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar el empleado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            txtID.Text = "";
            txtNombre.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            txtCorreoElectronico.Text = "";
            txtCargo.Text = "";
            txtSalario.Text = "";
            dateFechaContratacion.Text = DateTime.Now.ToString();
        }
    }
}
