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

using System.IO;
using Sunat.Logica;
using RestCsharp.Logica;

namespace RestCsharp.Presentacion.PRODUCTOS
{
    public partial class Registro_de_productos : UserControl
    {
        public Registro_de_productos()
        {
            InitializeComponent();
            btnguardar.Click += new EventHandler((sender, args) =>
            {
                OnButtonClick?.Invoke(this, null);
            });
        }
        string ESTADO_IMAGEN;
        public static int idproducto;
        public static string producto;
        public static string proceso;
        int Idcolor;
        string codigoProdsunat;
        string CodigoUdm;
        private void Button1_Click(object sender, EventArgs e)
        {
            if (Idcolor > 0)
            {
                if (txtdescripcion.Text != "")
                {
                    if (txtprecioventa.Text != "")
                    {
                        Insertar_Producto();
                    }

                }
            }
            else
            {
                MessageBox.Show("Seleccione un color");
            }


        }

        public delegate void ButtonClick(object sender, EventArgs e);
        public event ButtonClick OnButtonClick;
       
        private void Insertar_Producto()
        {
            var parametros = new Lproductos();
            var funcion = new Dproductos();
            parametros.Descripcion = txtdescripcion.Text;
            parametros.Id_grupo = Productos_rest.id_grupo;
            parametros.Precio_de_venta =Convert.ToDouble( txtprecioventa.Text);
            parametros.Precio_de_compra =Convert.ToDouble( txtpreciocompra.Text);
            parametros.Estado_imagen = ESTADO_IMAGEN;
            MemoryStream ms = new MemoryStream();
            ImagenProducto.Image.Save(ms, ImagenProducto.Image.RawFormat);
            parametros.Imagen = ms.GetBuffer();
            parametros.Idcolor = Idcolor;
            parametros.CodUm = txtum.SelectedValue.ToString();
            parametros.CodigoSunat = codigoProdsunat;
            parametros.Codigo = txtcodigodebarras.Text;
            var estado= funcion.insertarProductos(parametros);  
            if(estado==true)
            {
                Productos_rest frm = new Productos_rest();
                frm.dibujarProductos();
                Dispose();
            }
        }

        private void Registro_de_productos_Load(object sender, EventArgs e)
        {
            mostrarUnidadM();
            ordenarcontroles();
            MostrarColores();
            validarProcesos();
           
        }
        private void mostrarUnidadM()
        {
            var funcion = new DunidadM();
            var dt = new DataTable();
            funcion.mostrarUndm(ref dt);
            txtum.DisplayMember = "Descripcion";
            txtum.ValueMember = "Codigo";
            txtum.DataSource = dt;
        }
        private void validarProcesos()
        {
            if (proceso == "AGREGAR")
            {
                btnguardar.Visible = true;
                btnguardarcambios.Visible = false;
               
                Generarcodigo();
            }
            else if (proceso == "EDITAR")
            {

                ObtenerColorxproducto();
                txtdescripcion.Text = producto;
                btnguardar.Visible = false;
                btnguardarcambios.Visible = true;
            }
        }

        private void ObtenerColorxproducto()
        {
            var funcion = new Dcolores();
            var parametros = new Lproductos();
            var dt = new DataTable();
            parametros.Id_Producto1 = idproducto;
            funcion.mostrarColorxProducto(ref dt, parametros);
            Idcolor = Convert.ToInt32(dt.Rows[0][1]);
            btncolor.BackColor = ColorTranslator.FromHtml(dt.Rows[0][0].ToString());
            ESTADO_IMAGEN = (dt.Rows[0][3]).ToString();
            ImagenProducto.BackgroundImage = null;
            byte[] b = (byte[])((dt.Rows[0][2]));
            MemoryStream ms = new MemoryStream(b);
            ImagenProducto.Image = Image.FromStream(ms);
            txtprecioventa.Text = dt.Rows[0][5].ToString();
            txtpreciocompra.Text = dt.Rows[0][4].ToString();
            txtcodigodebarras.Text= dt.Rows[0][6].ToString();
            codigoProdsunat= dt.Rows[0][7].ToString();
            CodigoUdm= dt.Rows[0][8].ToString();
            if (ESTADO_IMAGEN == "VACIO")
            {
                PanelIcono.Visible = true;
            }
            else
            {
                PanelIcono.Visible = false;

            }
            mostrarUdmXcod();
            mostrarCodigoSunatXcod();
        }
        private void mostrarUdmXcod()
        {
            var funcion = new DunidadM();
            var parametros = new LunidadM();
            var dt = new DataTable();
            parametros.Codigo = CodigoUdm;
            funcion.mostrarUdmXcod(ref dt, parametros);
            txtum.Text = dt.Rows[0][2].ToString();
        }
        private void ordenarcontroles()
        {
            PanelIcono.Size = new Size(ImagenProducto.Width, ImagenProducto.Height);
          //  FormBorderStyle = FormBorderStyle.None;
            ESTADO_IMAGEN = "VACIO";
        }
        private void MostrarColores()
        {
            var dt = new DataTable();
            var funcion = new Dcolores();
            funcion.mostrarcolores(ref dt);
            foreach (DataRow rdr in dt.Rows)
            {
                Button btn1 = new Button();
                btn1.Width = 30;
                btn1.Height = 30;
                btn1.FlatStyle = FlatStyle.Flat;
                btn1.BackColor = ColorTranslator.FromHtml(rdr["ColorHtml"].ToString());
                btn1.Name = rdr["ColorHtml"].ToString();
                btn1.Tag = rdr["Idcolor"].ToString();
                flowLayoutPanel1.Controls.Add(btn1);
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

        private void label4_Click(object sender, EventArgs e)
        {
            agregar_imagen();
        }
        private void agregar_imagen()
        {
            dlg.InitialDirectory = "";
            dlg.Filter = "Imagenes|*.jpg;*.png";
            dlg.FilterIndex = 2;
            dlg.Title = "Cargador de Imagenes";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ImagenProducto.BackgroundImage = null;
                ImagenProducto.Image = new Bitmap(dlg.FileName);
                PanelIcono.Visible = false;
                ESTADO_IMAGEN = "LLENO";
            }
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            agregar_imagen();
        }

        private void btnvolver_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btncerrrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnguardarcambios_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtdescripcion.Text))
            {
                if (!string.IsNullOrEmpty(txtprecioventa.Text))
                {
                    if (string.IsNullOrEmpty(txtpreciocompra.Text))
                    {
                        txtpreciocompra.Text = 0.ToString();
                        editarProductos();
                    }
                    else
                    {
                        editarProductos();
                    }
                }
                else
                {
                    MessageBox.Show("Agrege un precio de venta");
                }

            }
            else
            {
                MessageBox.Show("Agrege un nombre de producto");
            }

        }
        private void editarProductos()
        {
            var funcion = new Dproductos();
            var parametros = new Lproductos();
            parametros.Descripcion = txtdescripcion.Text;
            parametros.Precio_de_venta = Convert.ToDouble(txtprecioventa.Text);
            parametros.Precio_de_compra = Convert.ToDouble(txtpreciocompra.Text);
            parametros.Estado_imagen = ESTADO_IMAGEN;
            parametros.Idcolor = Idcolor;
            parametros.Id_Producto1 = idproducto;
            MemoryStream ms = new MemoryStream();
            ImagenProducto.Image.Save(ms, ImagenProducto.Image.RawFormat);
            parametros.Imagen = ms.GetBuffer();
            parametros.CodUm = txtum.SelectedValue.ToString();
            parametros.CodigoSunat = codigoProdsunat;
            parametros.Codigo = txtcodigodebarras.Text;
            if (funcion.editarProductos(parametros) == true)
            {
                Dispose();
            }



        }

        private void txtprecioventa_KeyPress(object sender, KeyPressEventArgs e)
        {

            Bases.Separador_de_Numeros(txtprecioventa, e);

        }

        private void txtpreciocompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Separador_de_Numeros(txtpreciocompra, e);

        }

        private void txtprecioventa_TextChanged(object sender, EventArgs e)
        {
            //Bases.Reemplazarcomas(txtpreciocompra, e);
        }

        private void txtpreciocompra_TextChanged(object sender, EventArgs e)
        {
            //Bases.Reemplazarcomas(txtpreciocompra, e);
        }

        private void dgcodigossunat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            codigoProdsunat = dgcodigossunat.SelectedCells[1].Value.ToString();
            txtcodigoSunat.Text = dgcodigossunat.SelectedCells[0].Value.ToString();
            dgcodigossunat.Visible = false;
        }
        private void mostrarCodigoSunatXcod()
        {
            var funcion = new DcodigosProdsunat();
            var parametros = new LcodigosSunat();
            var dt = new DataTable();
            parametros.codigo = codigoProdsunat;
            funcion.mostrarCodSunatXCod(ref dt, parametros);
            txtcodigoSunat.Text = dt.Rows[0][1].ToString();
        }

        private void txtcodigoSunat_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtcodigoSunat.Text))
            {
                BuscarcodProdSunat();
            }
            else
            {
                dgcodigossunat.Visible = false;
            }
        }
        private void BuscarcodProdSunat()
        {
            var funcion = new DcodigosProdsunat();
            var dt = new DataTable();
            funcion.buscarCodProdSunat(ref dt, txtcodigoSunat.Text);
            dgcodigossunat.DataSource = dt;
            dgcodigossunat.Visible = true;
            dgcodigossunat.Columns[1].Visible = false;
        }

        private void btnGenerarCodigo_Click(object sender, EventArgs e)
        {
            Generarcodigo();
        }
        private void Generarcodigo()
        {
            double resultado;
            string queryMoneda;
            queryMoneda = "SELECT max(Id_Producto1)  FROM Producto1";
            SqlConnection con = new SqlConnection();
            con.ConnectionString =CONEXIONMAESTRA.conexion;
            SqlCommand comMoneda = new SqlCommand(queryMoneda, con);
            try
            {
                con.Open();
                resultado = Convert.ToDouble(comMoneda.ExecuteScalar()) + 1;
                con.Close();
            }
            catch (Exception ex)
            {
                resultado = 1;
            }

            string Cadena = Productos_rest.grupo;
            string[] Palabra;
            String espacio = " ";
            Palabra = Cadena.Split(Convert.ToChar(espacio));
            try
            {

                txtcodigodebarras.Text = resultado + Palabra[0].Substring(0, 2) + 369;
            }
            catch (Exception ex)
            {
            }
        }
    }
}
