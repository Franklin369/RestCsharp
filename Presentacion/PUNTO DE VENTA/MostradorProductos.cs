using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

using RestCsharp.Datos;
namespace RestCsharp.Presentacion.PUNTO_DE_VENTA
{
    public partial class MostradorProductos : UserControl
    {
        public MostradorProductos()
        {
            InitializeComponent();
        }
        int paginainicio = 1;
        int paginaMaxima = 15;
        int cantidad_productos = 0;
        int id_grupo;
        int idproducto;
        double precioVenta;
        private void MostradorProductos_Load(object sender, EventArgs e)
        {
           
        }
        public void contar_productos()
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlCommand com = new SqlCommand("contar_productos_por_grupo", CONEXIONMAESTRA.conectar);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@idgrupo", id_grupo);
                cantidad_productos = Convert.ToInt32(com.ExecuteScalar());
                CONEXIONMAESTRA.cerrar();
            }
            catch (Exception ex)
            {
                cantidad_productos = 0;
            }
        }
      
        public void dibujarProductos()
        {
            try
            {
                PanelProductos.Controls.Clear();
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("paginar_Productos_por_grupo", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_grupo", id_grupo);
                cmd.Parameters.AddWithValue("@Desde", paginainicio);
                cmd.Parameters.AddWithValue("@Hasta", paginaMaxima);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Label b = new Label();
                    Panel p1 = new Panel();
                    PictureBox I1 = new PictureBox();
                    b.Text = rdr["Descripcion"].ToString();
                    b.Name = rdr["Id_Producto1"].ToString();
                    b.Tag = rdr["Precio_de_venta"].ToString();
                    b.Font = new System.Drawing.Font("Microsoft Sans Serif", 7, FontStyle.Regular | FontStyle.Bold);
                    b.BackColor = Color.Transparent;
                    b.ForeColor = Color.White;
                    b.Dock = DockStyle.Fill;
                    b.TextAlign = ContentAlignment.MiddleCenter;
                    b.Cursor = Cursors.Hand;
                  

                    p1.Size = new System.Drawing.Size(147, 75);
                    p1.BorderStyle = BorderStyle.None;
                    p1.BackColor = Color.Transparent;
                    p1.BackgroundImage = Properties.Resources.azul;
                    p1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;

                    I1.Dock = DockStyle.Top;
                    byte[] bi = (byte[])rdr["Imagen"];
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(bi);
                    I1.Image = Image.FromStream(ms);
                    I1.SizeMode = PictureBoxSizeMode.Zoom;
                    I1.Cursor = Cursors.Hand;
                    I1.Tag = rdr["Precio_de_venta"].ToString();
                    I1.Name = rdr["Id_Producto1"].ToString();
                    I1.BackColor = Color.Transparent;
                    p1.Controls.Add(b);
                    if (rdr["Estado_imagen"].ToString() != "VACIO")
                    {
                        p1.Controls.Add(I1);
                    }
                    b.BringToFront();
                    PanelProductos.Controls.Add(p1);
                    I1.Click += I1_Click;
                    b.Click += B_Click;
                }
                CONEXIONMAESTRA.cerrar();
            }
            catch (Exception ex)
            {
                CONEXIONMAESTRA.cerrar();
                MessageBox.Show(ex.Message);

            }
        }

        private void B_Click(object sender, EventArgs e)
        {
            idproducto =Convert.ToInt32(((Label)sender).Name);
            precioVenta= Convert.ToDouble(((Label)sender).Tag);
            Punto_de_venta.idproducto = idproducto;
            Punto_de_venta.precioventa = Convert.ToDecimal( precioVenta);
            Punto_de_venta.Puerta.insertarVenta();

        }

        private void I1_Click(object sender, EventArgs e)
        {
         
        }

        private void btnatras_Click(object sender, EventArgs e)
        {
            if (paginainicio > 1)
            {
                paginainicio -= 15;
                paginaMaxima -= 15;
                dibujarProductos();
            }
        }

        private void btnadelante_Click(object sender, EventArgs e)
        {
            Dispose();
          
        }

        private void PanelProductos_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
