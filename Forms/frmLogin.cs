using Punto.conexion;
using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace Punto.Forms
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, System.EventArgs e)
        {



            if (txtUser.Text.Trim() == "" || txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("Complete todos los campos.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                Conexion conexion = new Conexion();

                using (MySqlConnection con = conexion.GetConnection())
                {
                    if (con == null)
                        return;


                    string sql = @"SELECT nombre_completo
                       FROM usuarios
                       WHERE username=@usuario
                       AND password=@password";

                    MySqlCommand cmd = new MySqlCommand(sql, con);

                    cmd.Parameters.AddWithValue("@usuario", txtUser.Text.Trim());
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                    MySqlDataReader lector = cmd.ExecuteReader();

                    if (lector.Read())
                    {
                        MessageBox.Show("Bienvenido " + lector["nombre_completo"]);

                        frmPrincipal frm = new frmPrincipal();

                        frm.Show();

                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Usuario o contraseña incorrectos.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexión: " + ex.Message);
            }

        }
    }
}
