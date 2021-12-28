using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using RestCsharp.Datos;
using RestCsharp.Logica;
using System.IO;

namespace RestCsharp.Presentacion.PRODUCTOS
{
    public partial class Grupos_De_productos : Form
    {
        public Grupos_De_productos()
        {
            InitializeComponent();
        }
        string ESTADO_IMAGEN;
        int Idcolor;
        public static int idgrupo;
        public static string proceso;
        public static string grupo;
        private void PanelEDICION_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Grupos_De_productos_Load(object sender, EventArgs e)
        {
            ordenarcontroles();
            MostrarColores();
            validarProcesos();
        }
        private void validarProcesos()
        {
            if(proceso=="AGREGAR")
            {
                btnguardar.Visible = true;
                btnguardarcambios.Visible = false;
            }
            else if(proceso=="EDITAR")
            {
                ObtenerColorxgrupo();
                btnguardar.Visible = false;
                btnguardarcambios.Visible = true;
                txtgrupo.Text = grupo;
            }
        }
        private void ObtenerColorxgrupo()
        {
            var funcion = new Dgrupos();
            var parametros = new Lgruposprodc();
            var dt = new DataTable();
            parametros.Idline = idgrupo;
            funcion.mostrarColorxgrupo(ref dt, parametros);
            Idcolor = Convert.ToInt32(dt.Rows[0][1]);
            btncolor.BackColor = ColorTranslator.FromHtml(dt.Rows[0][0].ToString());
            ESTADO_IMAGEN = (dt.Rows[0][3]).ToString();
            ImagenGrupo.BackgroundImage = null;
            byte[] b = (byte[])((dt.Rows[0][2]));
            MemoryStream ms = new MemoryStream(b);
            ImagenGrupo.Image = Image.FromStream(ms);
            if (ESTADO_IMAGEN == "VACIO")
            {
                PanelIcono.Visible = true;
            }
            else
            {
                PanelIcono.Visible = false;

            }



        }
        private void ordenarcontroles()
        {
            PanelIcono.Size = new Size(ImagenGrupo.Width, ImagenGrupo.Height);
            FormBorderStyle = FormBorderStyle.None;
            ESTADO_IMAGEN = "VACIO";


        }
        private void MostrarColores()
        {
            DataTable dt = new DataTable();
            Dcolores funcion = new Dcolores();
            funcion.mostrarcolores(ref dt);
            foreach(DataRow rdr in dt.Rows)
            {
                Button btn1 = new Button();
                btn1.Width = 30;
                btn1.Height = 30;
                btn1.FlatStyle = FlatStyle.Flat;
                btn1.BackColor = ColorTranslator.FromHtml(rdr["ColorHtml"].ToString());
                btn1.Name = rdr["ColorHtml"].ToString();
                btn1.Tag = rdr["Idcolor"].ToString();
                flowLayoutPanel2.Controls.Add(btn1);
                btn1.Click += Btn1_Click;
            }
        }

        private void Btn1_Click(object sender, EventArgs e)
        {
            Idcolor = Convert.ToInt32(((Button)sender).Tag);
            string color;
            color = ((Button)sender).Name;
            btncolor.BackColor = ColorTranslator.FromHtml(color);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if(Idcolor >0)
            {
                if(!string.IsNullOrEmpty(txtgrupo.Text))
                {
                  Insertar_Grupo_de_Productos();
                  Dispose();
                }
                else
                {
                    MessageBox.Show("Ingrese nombre del grupo");
                }

            }
            else
            {
                MessageBox.Show("Elija un color");
            }

        }

        private void Insertar_Grupo_de_Productos()
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("Insertar_Grupo_de_Productos",CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Grupo", txtgrupo .Text);
                cmd.Parameters.AddWithValue("@Por_defecto","NO");
                cmd.Parameters.AddWithValue("@Estado", "ACTIVO");
                cmd.Parameters.AddWithValue("@Estado_de_icono", ESTADO_IMAGEN);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                ImagenGrupo.Image.Save(ms, ImagenGrupo.Image.RawFormat);
                cmd.Parameters.AddWithValue("@Icono", ms.GetBuffer());
                cmd.Parameters.AddWithValue("@Idcolor",Idcolor);
                cmd.ExecuteNonQuery();
               CONEXIONMAESTRA.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void agregar_imagen()
        {
            dlg.InitialDirectory = "";
            dlg.Filter = "Imagenes|*.jpg;*.png";
            dlg.FilterIndex = 2;
            dlg.Title = "Cargador de Imagenes";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ImagenGrupo.BackgroundImage = null;
                ImagenGrupo.Image = new Bitmap(dlg.FileName);
                PanelIcono.Visible = false;
                ESTADO_IMAGEN = "LLENO";
            }
        }

        private void Label2_Click(object sender, EventArgs e)
        {
            agregar_imagen();
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            agregar_imagen();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void ImagenGrupo_Click(object sender, EventArgs e)
        {
            agregar_imagen();
        }

        private void btnvolver_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnguardarcambios_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtgrupo.Text))
            {
                editargrupos();
            }
            else
            {
                MessageBox.Show("Agrege un nombre de grupo");
            }
        }
        private void editargrupos()
        {
            Dgrupos funcion = new Dgrupos();
            Lgruposprodc parametros = new Lgruposprodc();
            parametros.Grupo = txtgrupo.Text;
            parametros.Idcolor = Idcolor;
            parametros.Idline = idgrupo;
            MemoryStream ms = new MemoryStream();
            ImagenGrupo.Image.Save(ms, ImagenGrupo.Image.RawFormat);
            parametros.Icono = ms.GetBuffer();
            parametros.Estado_de_icono = ESTADO_IMAGEN;
            if(funcion.editarGrupoProd(parametros)==true)
            {
                Dispose();
            }
        }
    }
}
