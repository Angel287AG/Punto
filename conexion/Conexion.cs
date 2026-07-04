using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Punto.conexion
{
    internal class Conexion
    {
        private string cadena = "Server=127.0.0.1;" + "Database=PuntoDB;" +"Uid=root;" +"Pwd=;" +"Port=3306;";

        public MySqlConnection ObtenerConexion()
        {
            MySqlConnection conexion = new MySqlConnection(cadena);
            conexion.Open();
            return conexion;
        }

    }
}
