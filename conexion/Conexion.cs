using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Punto.conexion
{
    internal class Conexion
    {
        private readonly string cadena;

        public Conexion()
        {
            cadena = "Server=127.0.0.1;" + "Database=PuntoDB;" + "Uid=root;" + "Pwd=;" + "Port=3306;";

        }

        public MySqlConnection GetConnection()
        {
            try
            {
                MySqlConnection conexion = new MySqlConnection(cadena);
                conexion.Open();
                return conexion;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al conectar a la base de datos: " + ex.Message);
                return null;
            }

        }
    }
}
