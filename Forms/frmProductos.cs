using MySql.Data.MySqlClient;
using Punto.conexion;
using System;
using System.Data;
using System.Windows.Forms;

namespace Punto.Forms
{
    public partial class frmProductos : Form
    {
        public frmProductos()
        {
            InitializeComponent();
        }
        private void MostrarProductos()
        {
            try
            {
                Conexion conexion = new Conexion();

                using (MySqlConnection con = conexion.GetConnection())
                {
                    string sql = "SELECT * FROM productos";

                    MySqlDataAdapter da = new MySqlDataAdapter(sql, con);

                    DataTable tabla = new DataTable();

                    da.Fill(tabla);

                    dgvProductos.DataSource = tabla;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, System.EventArgs e)
        {
            DialogResult r = MessageBox.Show(
                "¿Eliminar producto?", "Confirmar",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

            if (r == DialogResult.Yes)
            {
                try
                {
                    Conexion conexion = new Conexion();

                    using (MySqlConnection con = conexion.GetConnection())
                    {
                        string sql = "DELETE FROM productos WHERE producto_id=@id";

                        MySqlCommand cmd = new MySqlCommand(sql, con);

                        cmd.Parameters.AddWithValue("@id", txtCodigo.Text);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Producto eliminado");

                        MostrarProductos();
                  
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void btnEditar_Click(object sender, System.EventArgs e)
        {
            decimal precio;
            int stock;

            if (!decimal.TryParse(txtPrecio.Text, out precio))
            {
                MessageBox.Show("Precio incorrecto");
                return;
            }

            if (!int.TryParse(txtStock.Text, out stock))
            {
                MessageBox.Show("Stock incorrecto");
                return;
            }

            try
            {
                Conexion conexion = new Conexion();

                using (MySqlConnection con = conexion.GetConnection())
                {
                    string sql = @"UPDATE productos
                                   SET codigo=@codigo,
                                       descripcion=@descripcion,
                                       precio=@precio,
                                       stock=@stock
                                   WHERE producto_id=@id";

                    MySqlCommand cmd = new MySqlCommand(sql, con);

                    cmd.Parameters.AddWithValue("@codigo", txtCodigo.Text);
                    cmd.Parameters.AddWithValue("@descripcion", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@precio", precio);
                    cmd.Parameters.AddWithValue("@stock", stock);
                    cmd.Parameters.AddWithValue("@id", txtCodigo.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Producto actualizado");

                    MostrarProductos();
      
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNuevo_Click(object sender, System.EventArgs e)
        {
            decimal precio;
            int stock;

            if (!decimal.TryParse(txtPrecio.Text, out precio))
            {
                MessageBox.Show("Precio incorrecto");
                return;
            }

            if (!int.TryParse(txtStock.Text, out stock))
            {
                MessageBox.Show("Stock incorrecto");
                return;
            }

            try
            {
                Conexion conexion = new Conexion();

                using (MySqlConnection con = conexion.GetConnection())
                {
                    string sql = @"INSERT INTO productos
                                  (codigo,descripcion,precio,stock)
                                  VALUES
                                  (@codigo,@descripcion,@precio,@stock)";

                    MySqlCommand cmd = new MySqlCommand(sql, con);

                    cmd.Parameters.AddWithValue("@codigo", txtCodigo.Text);
                    cmd.Parameters.AddWithValue("@descripcion", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@precio", precio);
                    cmd.Parameters.AddWithValue("@stock", stock);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Producto guardado");

                    MostrarProductos();
         
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvProductos.Rows[e.RowIndex];

                txtCodigo.Text = fila.Cells["producto_id"].Value.ToString();
                txtCodigo.Text = fila.Cells["codigo"].Value.ToString();
                txtNombre.Text = fila.Cells["descripcion"].Value.ToString();
                txtPrecio.Text = fila.Cells["precio"].Value.ToString();
                txtStock.Text = fila.Cells["stock"].Value.ToString();
            }

        }

        private void frmProductos_Load(object sender, EventArgs e)
        {
            MostrarProductos();

        }
    }
}
