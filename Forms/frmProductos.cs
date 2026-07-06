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
                    if (con == null)
                        return;

                    string sql = "SELECT * FROM productos ORDER BY producto_id";

                    MySqlDataAdapter da = new MySqlDataAdapter(sql, con);

                    DataTable tabla = new DataTable();

                    da.Fill(tabla);

                    dgvProductos.DataSource = tabla;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, System.EventArgs e)
        {
            if (lblId.Text == "0")
            {
                MessageBox.Show("Seleccione un producto.");
                return;
            }

            DialogResult r = MessageBox.Show(
                "¿Eliminar producto?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (r == DialogResult.Yes)
            {
                try
                {
                    Conexion conexion = new Conexion();

                    using (MySqlConnection con = conexion.GetConnection())
                    {
                        if (con == null)
                            return;

                        string sql = "DELETE FROM productos WHERE producto_id=@id";

                        MySqlCommand cmd = new MySqlCommand(sql, con);

                        cmd.Parameters.AddWithValue("@id", lblId.Text);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Producto eliminado.");

                        MostrarProductos();
                        Limpiar();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnEditar_Click(object sender, System.EventArgs e)

        {
            if (lblId.Text == "0")
            {
                MessageBox.Show("Seleccione un producto.");
                return;
            }

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
                    if (con == null)
                        return;

                    string sql = @"UPDATE productos
                           SET codigo=@codigo,
                               descripcion=@descripcion,
                               precio=@precio,
                               stock=@stock
                           WHERE producto_id=@id";

                    MySqlCommand cmd = new MySqlCommand(sql, con);

                    cmd.Parameters.AddWithValue("@codigo", txtCodigo.Text.Trim());
                    cmd.Parameters.AddWithValue("@descripcion", txtNombre.Text.Trim());
                    cmd.Parameters.AddWithValue("@precio", precio);
                    cmd.Parameters.AddWithValue("@stock", stock);
                    cmd.Parameters.AddWithValue("@id", lblId.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Producto actualizado.");

                    MostrarProductos();
                    Limpiar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnNuevo_Click(object sender, System.EventArgs e)
        {
            if (txtCodigo.Text.Trim() == "" ||
       txtNombre.Text.Trim() == "" ||
       txtPrecio.Text.Trim() == "" ||
       txtStock.Text.Trim() == "")
            {
                MessageBox.Show("Complete todos los campos.");
                return;
            }

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
                    if (con == null)
                        return;

                    string sql = @"INSERT INTO productos (codigo,descripcion,precio,stock) VALUES (@codigo,@descripcion,@precio,@stock)";

                    MySqlCommand cmd = new MySqlCommand(sql, con);

                    cmd.Parameters.AddWithValue("@codigo", txtCodigo.Text.Trim());
                    cmd.Parameters.AddWithValue("@descripcion", txtNombre.Text.Trim());
                    cmd.Parameters.AddWithValue("@precio", precio);
                    cmd.Parameters.AddWithValue("@stock", stock);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Producto guardado correctamente.");

                    MostrarProductos();
                    Limpiar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvProductos.Rows[e.RowIndex];

                lblId.Text = fila.Cells["producto_id"].Value.ToString();
                txtCodigo.Text = fila.Cells["codigo"].Value.ToString();
                txtNombre.Text = fila.Cells["descripcion"].Value.ToString();
                txtPrecio.Text = fila.Cells["precio"].Value.ToString();
                txtStock.Text = fila.Cells["stock"].Value.ToString();
            }

        }

        private void frmProductos_Load(object sender, EventArgs e)
        {
            MostrarProductos();
            Limpiar();

        }
        private void Limpiar()
        {
            lblId.Text = "0";
            txtCodigo.Clear();
            txtNombre.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
            txtCodigo.Focus();
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Conexion conexion = new Conexion();

                using (MySqlConnection con = conexion.GetConnection())
                {
                    if (con == null)
                        return;

                    string sql = @"SELECT *FROM productos WHERE codigo LIKE @buscar OR descripcion LIKE @buscar";

                    MySqlDataAdapter da = new MySqlDataAdapter(sql, con);

                    da.SelectCommand.Parameters.AddWithValue("@buscar", "%" + txtBusqueda.Text.Trim() + "%");

                    DataTable tabla = new DataTable();

                    da.Fill(tabla);

                    dgvProductos.DataSource = tabla;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void cmbCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
