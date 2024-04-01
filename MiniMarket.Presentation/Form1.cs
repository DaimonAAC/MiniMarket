using System;
using System.Windows.Forms;
using MiniMarket.DataAccess;

namespace MiniMarket.Presentation
{
    public partial class Form1 : Form
    {
        private UserDataAccess userDataAccess;

        public Form1()
        {
            InitializeComponent();
            userDataAccess = new UserDataAccess();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            bool loginSuccessful = userDataAccess.VerifyLogin(username, password);

            if (loginSuccessful)
            {
                // Iniciar sesión exitosamente, abrir el formulario de menú principal
                Menu menuForm = new Menu();
                menuForm.Show();
                this.Hide();
            }
            else
            {
                // Error de inicio de sesión, muestra un mensaje al usuario
                MessageBox.Show("Credenciales incorrectas. Por favor, inténtelo de nuevo.");
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
    }
}
