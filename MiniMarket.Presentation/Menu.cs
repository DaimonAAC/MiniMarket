using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniMarket.Presentation
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            panelReportes.Visible = false;
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
            // Instanciar el formulario de productos
            Productos productosForm = new Productos();

            // Mostrar el formulario de productos
            productosForm.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Clientes productosForm = new Clientes();

            // Mostrar el formulario de productos
            productosForm.Show();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Proveedores productosForm = new Proveedores();

            // Mostrar el formulario de productos
            productosForm.Show();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Empleados productosForm = new Empleados();

            // Mostrar el formulario de productos
            productosForm.Show();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Form1 productosForm = new Form1();

            // Mostrar el formulario de productos
            productosForm.Show();

            // Cerrar el formulario actual (Clientes)
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Factura productosForm = new Factura();

            // Mostrar el formulario de productos
            productosForm.Show();
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            // Cambiar la visibilidad del panel de reportes al hacer clic en el botón "Reportes"
            panelReportes.Visible = !panelReportes.Visible;
        }
    }
}
