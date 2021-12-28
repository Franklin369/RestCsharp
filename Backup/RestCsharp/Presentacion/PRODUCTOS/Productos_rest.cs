using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using RestCsharp.Datos;
using RestCsharp.Logica;
using Sunat.Logica;

namespace RestCsharp.Presentacion.PRODUCTOS
{
    public partial class Productos_rest : Form
    {
        public Productos_rest()
        {
            InitializeComponent();
        }
        public static int id_grupo;
        int idproducto;
        string estadogrupo;
        public static  string grupo;
        private void Productos_rest_Load(object sender, EventArgs e)
        {
            dibujarGrupos();
            presentar();
        }
        private void presentar()
        {
            PanelBienvienida.Visible = true;
            PanelBienvienida.Dock = DockStyle.Fill;
        }
        private void dibujarGrupos()
        {
            try
            {
                Panel_grupos.Controls.Clear();
                CONEXIONMAESTRA.abrir();
                var cmd = new SqlDataAdapter("buscarGrupos", CONEXIONMAESTRA.conectar);
                cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmd.SelectCommand.Parameters.AddWithValue("@buscador", txtgrupo.Text);
                var dt = new DataTable();
                cmd.Fill(dt);

                foreach (DataRow rdr in dt.Rows)
                {
                    Label b = new Label();
                    Panel p1 = new Panel();
                    Panel p2 = new Panel();
                    PictureBox I1 = new PictureBox();

                    b.Text = rdr["Grupo"].ToString();
                    b.Name = rdr["Idline"].ToString();
                    b.Tag = rdr["Estado"].ToString();

                    b.Size = new System.Drawing.Size(119, 25);
                    b.Font = new System.Drawing.Font("Microsoft Sans Serif", 13);
                    b.BackColor = Color.Transparent;
                    b.ForeColor = Color.White;
                    b.Dock = DockStyle.Fill;
                    b.TextAlign = ContentAlignment.MiddleCenter;
                    b.Cursor = Cursors.Hand;

                    p1.Size = new System.Drawing.Size(140, 133);
                    p1.BorderStyle = BorderStyle.FixedSingle;
                    p1.Dock = DockStyle.Bottom;
                    p1.Name = rdr["Idline"].ToString();
                    string estado = rdr["Estado"].ToString();
                    if (estado == "ELIMINADO")
                    {
                        p1.BackColor = Color.FromArgb(255, 45, 54);
                    }
                    else
                    {
                        p1.BackColor = Color.FromArgb(43, 43, 43);
                    }



                    p2.Size = new System.Drawing.Size(140, 25);
                    p2.Dock = DockStyle.Top;
                    p2.BackColor = Color.Transparent;
                    p2.BorderStyle = BorderStyle.None;

                    I1.Size = new System.Drawing.Size(140, 76);
                    I1.Dock = DockStyle.Top;
                    I1.BackgroundImage = null;
                    byte[] bi = (Byte[])rdr["Icono"];
                    MemoryStream ms = new MemoryStream(bi);
                    I1.Image = Image.FromStream(ms);
                    I1.SizeMode = PictureBoxSizeMode.Zoom;
                    I1.Cursor = Cursors.Hand;
                    I1.Tag = rdr["Idline"].ToString();
                    I1.Name = rdr["Grupo"].ToString();
                    MenuStrip Menustrip = new MenuStrip();
                    Menustrip.BackColor = Color.Transparent;
                    Menustrip.AutoSize = false;
                    Menustrip.Size = new System.Drawing.Size(28, 24);
                    Menustrip.Dock = DockStyle.Right;
                    Menustrip.Name = rdr["Idline"].ToString();
                    ToolStripMenuItem ToolStripPRINCIPAL = new ToolStripMenuItem();
                    ToolStripMenuItem ToolStripEDITAR = new ToolStripMenuItem();
                    ToolStripMenuItem ToolStripELIMINAR = new ToolStripMenuItem();
                    ToolStripMenuItem ToolStripRESTAURAR = new ToolStripMenuItem();

                    ToolStripPRINCIPAL.Image = Properties.Resources.menuCajas_claro;
                    ToolStripPRINCIPAL.BackColor = Color.Transparent;

                    ToolStripEDITAR.Text = "Editar";
                    ToolStripEDITAR.Name = rdr["Grupo"].ToString();
                    ToolStripEDITAR.Tag = rdr["Idline"].ToString();

                    ToolStripELIMINAR.Text = "Eliminar";
                    ToolStripELIMINAR.Tag = rdr["Idline"].ToString();

                    ToolStripRESTAURAR.Text = "Restaurar";
                    ToolStripRESTAURAR.Tag = rdr["Idline"].ToString();


                    Menustrip.Items.Add(ToolStripPRINCIPAL);
                    if (rdr["Estado"].ToString() == "ELIMINADO")
                    {
                        ToolStripPRINCIPAL.DropDownItems.Add(ToolStripRESTAURAR);
                    }
                    else
                    {
                        ToolStripPRINCIPAL.DropDownItems.Add(ToolStripEDITAR);
                        ToolStripPRINCIPAL.DropDownItems.Add(ToolStripELIMINAR);
                    }




                    p2.Controls.Add(Menustrip);
                    p1.Controls.Add(b);
                    if (rdr["Estado_de_icono"].ToString() != "VACIO")
                    {
                        p1.Controls.Add(I1);
                    }


                    p1.Controls.Add(p2);
                    b.BringToFront();
                    p2.SendToBack();
                    Panel_grupos.Controls.Add(p1);
                    b.Click += new EventHandler(miEventoLabel);
                    I1.Click += new EventHandler(miEventoImagen);
                    ToolStripEDITAR.Click += ToolStripEDITAR_Click;
                    ToolStripELIMINAR.Click += ToolStripELIMINAR_Click;
                    ToolStripRESTAURAR.Click += ToolStripRESTAURAR_Click;
                }
                CONEXIONMAESTRA.cerrar();
            }
            catch (Exception ex)
            {
                CONEXIONMAESTRA.cerrar();
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void ToolStripRESTAURAR_Click(object sender, EventArgs e)
        {
            id_grupo = Convert.ToInt32(((ToolStripMenuItem)sender).Tag);
            DialogResult result;
            result = MessageBox.Show("¿Desea restaurar este grupo?", "Grupo eliminado", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                restaurarGrupos();
            }

        }
        private void restaurarGrupos()
        {
            var funcion = new Dgrupos();
            var parametros = new Lgruposprodc();
            parametros.Idline = id_grupo;
            if (funcion.restaurarGrupos(parametros) == true)
            {
                dibujarGrupos();
            }
        }

        private void ToolStripELIMINAR_Click(object sender, EventArgs e)
        {
            id_grupo = Convert.ToInt32(((ToolStripMenuItem)sender).Tag);
            EliminarGrupos();
        }
        private void EliminarGrupos()
        {
            var funcion = new Dgrupos();
            var parametros = new Lgruposprodc();
            parametros.Idline = id_grupo;
            if (funcion.eliminarGrupos(parametros) == true)
            {
                dibujarGrupos();
            }
        }


        private void ToolStripEDITAR_Click(object sender, EventArgs e)
        {
            id_grupo = Convert.ToInt32(((ToolStripMenuItem)sender).Tag);
            grupo=((ToolStripMenuItem)sender).Name.ToString();
            Grupos_De_productos frm = new Grupos_De_productos();
            Grupos_De_productos.idgrupo = id_grupo;
            Grupos_De_productos.proceso = "EDITAR";
            Grupos_De_productos.grupo = grupo;
            frm.FormClosed += Frm_FormClosed;
            frm.ShowDialog();
        }

        private void Frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            dibujarGrupos();
        }

        private void miEventoLabel(System.Object sender, EventArgs e)
        {
            grupo = ((Label)sender).Text;
            id_grupo = Convert.ToInt32(((Label)sender).Name);
            estadogrupo = (((Label)sender).Name).ToString();
            if (estadogrupo == "ELIMINADO")
            {
                DialogResult result;
                result = MessageBox.Show("¿Desea restaurar este grupo?", "Grupo eliminado", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    restaurarGrupos();
                }
            }
            else
            {
                ver_productos_por_grupo();
                Seleccionar_Deseleccionar_grupos();
            }

        }

        private void miEventoImagen(System.Object sender, EventArgs e)
        {
            grupo = ((PictureBox)sender).Name;
            id_grupo = Convert.ToInt32(((PictureBox)sender).Tag);
            ver_productos_por_grupo();
            Seleccionar_Deseleccionar_grupos();
        }
        private void Seleccionar_Deseleccionar_grupos()
        {


            //Sin seleccionar
            foreach (Panel panelP1 in Panel_grupos.Controls)
            {
                if (panelP1 is Panel)
                {
                    foreach (Label PanLateral2 in panelP1.Controls)
                    {
                        if (PanLateral2 is Label)
                        {
                            panelP1.BackColor = Color.FromArgb(43, 43, 43);
                            break;
                        }
                    }
                }
            }

            //Seleccionado
            foreach (Panel PanelP2 in Panel_grupos.Controls)
            {
                if (PanelP2 is Panel)
                {
                    if (PanelP2.Name == Convert.ToString(id_grupo))
                    {
                        PanelP2.BackColor = Color.Black;
                    }
                }
            }
        }
        private void ver_productos_por_grupo()
        {
            PanelBienvienida.Visible = false;
            PanelvisorProductos.Visible = true;
            PanelvisorProductos.Dock = DockStyle.Fill;
            dibujarProductos();

        }

        public void dibujarProductos()
        {
            try
            {
                PanelProductos.Controls.Clear();
                CONEXIONMAESTRA.abrir();
                var cmd = new SqlDataAdapter("mostrar_Productos_por_grupo", CONEXIONMAESTRA.conectar);
                cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmd.SelectCommand.Parameters.AddWithValue("@id_grupo", id_grupo);
                cmd.SelectCommand.Parameters.AddWithValue("@buscador", txtbuscarproductos.Text);
                var dt = new DataTable();
                cmd.Fill(dt);
                foreach (DataRow rdr in dt.Rows)
                {
                    Label lblprecio = new Label();
                    Label b = new Label();
                    Panel p1 = new Panel();
                    Panel p2 = new Panel();
                    PictureBox I1 = new PictureBox();
                    b.Text = rdr["Descripcion"].ToString();
                    b.Name = rdr["Id_Producto1"].ToString();
                    b.Size = new System.Drawing.Size(119, 25);
                    b.Font = new System.Drawing.Font("Microsoft Sans Serif", 13);
                    b.BackColor = Color.Transparent;
                    b.ForeColor = Color.White;
                    b.Dock = DockStyle.Fill;
                    b.TextAlign = ContentAlignment.MiddleCenter;
                    b.Cursor = Cursors.Hand;

                    lblprecio.Text = rdr["Moneda"].ToString() + " " + rdr["Precio_de_venta"].ToString();
                    lblprecio.Name = rdr["Id_Producto1"].ToString();
                    lblprecio.Size = new System.Drawing.Size(119, 25);
                    lblprecio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10);
                    lblprecio.BackColor = Color.Transparent;
                    lblprecio.ForeColor = Color.White;
                    lblprecio.Dock = DockStyle.Bottom;
                    lblprecio.TextAlign = ContentAlignment.MiddleCenter;
                    lblprecio.Cursor = Cursors.Hand;

                    p1.Size = new System.Drawing.Size(140, 133);
                    p1.BorderStyle = BorderStyle.FixedSingle;
                    p1.Dock = DockStyle.Bottom;
                    string estado = rdr["Estado"].ToString();
                    if (estado == "ELIMINADO")
                    {
                        p1.BackColor = Color.FromArgb(255, 45, 54);
                    }
                    else
                    {
                        p1.BackColor = Color.FromArgb(43, 43, 43);
                    }

                    p2.Size = new System.Drawing.Size(140, 25);
                    p2.Dock = DockStyle.Top;
                    p2.BackColor = Color.Transparent;
                    p2.BorderStyle = BorderStyle.None;

                    I1.Size = new System.Drawing.Size(140, 76);
                    I1.Dock = DockStyle.Top;
                    I1.BackgroundImage = null;
                    byte[] bi = (Byte[])rdr["Imagen"];
                    MemoryStream ms = new MemoryStream(bi);
                    I1.Image = Image.FromStream(ms);
                    I1.SizeMode = PictureBoxSizeMode.Zoom;
                    I1.Cursor = Cursors.Hand;
                    I1.Tag = rdr["Id_Producto1"].ToString();
                    I1.Size = new Size(100, 30);
                    MenuStrip Menustrip = new MenuStrip();
                    Menustrip.BackColor = Color.Transparent;
                    Menustrip.AutoSize = false;
                    Menustrip.Size = new System.Drawing.Size(28, 24);
                    Menustrip.Dock = DockStyle.Right;
                    Menustrip.Name = rdr["Id_Producto1"].ToString();
                    ToolStripMenuItem ToolStripPRINCIPAL = new ToolStripMenuItem();
                    ToolStripMenuItem ToolStripEDITAR = new ToolStripMenuItem();
                    ToolStripMenuItem ToolStripELIMINAR = new ToolStripMenuItem();
                    ToolStripMenuItem ToolStripRESTAURAR = new ToolStripMenuItem();
                    ToolStripPRINCIPAL.Image = Properties.Resources.menuCajas_claro;
                    ToolStripPRINCIPAL.BackColor = Color.Transparent;
                    ToolStripEDITAR.Text = "Editar";
                    ToolStripEDITAR.Name = rdr["Descripcion"].ToString();
                    ToolStripEDITAR.Tag = rdr["Id_Producto1"].ToString();

                    ToolStripELIMINAR.Text = "Eliminar";
                    ToolStripELIMINAR.Tag = rdr["Id_Producto1"].ToString();

                    ToolStripRESTAURAR.Text = "Restaurar";
                    ToolStripRESTAURAR.Tag = rdr["Id_Producto1"].ToString();
                    Menustrip.Items.Add(ToolStripPRINCIPAL);
                    if (rdr["Estado"].ToString() == "ELIMINADO")
                    {
                        ToolStripPRINCIPAL.DropDownItems.Add(ToolStripRESTAURAR);
                    }
                    else
                    {
                        ToolStripPRINCIPAL.DropDownItems.Add(ToolStripEDITAR);
                        ToolStripPRINCIPAL.DropDownItems.Add(ToolStripELIMINAR);
                    }

                    p2.Controls.Add(Menustrip);
                    p1.Controls.Add(b);
                    p1.Controls.Add(lblprecio);
                    if (rdr["Estado_imagen"].ToString() != "VACIO")
                    {
                        p1.Controls.Add(I1);
                    }




                    p1.Controls.Add(p2);
                    b.BringToFront();
                    p2.SendToBack();
                    PanelProductos.Controls.Add(p1);
                    ToolStripEDITAR.Click += ToolStripEDITAR_Click1;
                    ToolStripELIMINAR.Click += ToolStripELIMINAR_Click1;
                    ToolStripRESTAURAR.Click += ToolStripRESTAURAR_Click1;
                }

                CONEXIONMAESTRA.cerrar();
            }
            catch (Exception ex)
            {
                CONEXIONMAESTRA.cerrar();
                MessageBox.Show(ex.Message);
            }
        }

        private void ToolStripRESTAURAR_Click1(object sender, EventArgs e)
        {
            idproducto = Convert.ToInt32(((ToolStripMenuItem)sender).Tag);
            DialogResult result;
            result = MessageBox.Show("¿Desea restaurar este producto?", "Producto eliminado", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                restaurarProductos();
            }
        }
        private void restaurarProductos()
        {
            var funcion = new Dproductos();
            var parametros = new Lproductos();
            parametros.Id_Producto1 = idproducto;
            if (funcion.RestaurarProductos(parametros) == true)
            {
                dibujarProductos();
            }
        }
        private void ToolStripELIMINAR_Click1(object sender, EventArgs e)
        {
            idproducto = Convert.ToInt32(((ToolStripMenuItem)sender).Tag);
            EliminarProductos();
        }
        private void EliminarProductos()
        {
            var funcion = new Dproductos();
            var parametros = new Lproductos();
            parametros.Id_Producto1 = idproducto;
            if (funcion.eliminarProductos(parametros) == true)
            {
                dibujarProductos();
            }
        }
        private void ToolStripEDITAR_Click1(object sender, EventArgs e)
        {
            idproducto = Convert.ToInt32(((ToolStripMenuItem)sender).Tag);
            var frm = new Registro_de_productos();
            Registro_de_productos.idproducto = idproducto;
            Registro_de_productos.producto = (((ToolStripMenuItem)sender).Name).ToString();
            Registro_de_productos.proceso = "EDITAR";
          //  frm.c += Frm_FormClosed1;
            //frm.ShowDialog();
        }

        private void Frm_FormClosed1(object sender, IDisposable e)
        {
            dibujarProductos();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Grupos_De_productos frm = new Grupos_De_productos();
            frm.FormClosed += frmGrupos_FormClosed;
            Grupos_De_productos.proceso = "AGREGAR";
            frm.ShowDialog();
        }
        public void frmGrupos_FormClosed(Object sender, FormClosedEventArgs e)
        {
            dibujarGrupos();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            var frm = new Registro_de_productos();
            frm.OnButtonClick += UsrCtrl_OnButtonClick;
            PanelvisorProductos.Controls.Add(frm);
            frm.Dock = DockStyle.Fill;
            frm.BringToFront();
          //  frm.FormClosed += new FormClosedEventHandler(frmRegistroProducto_FormClosed);
            Registro_de_productos.proceso = "AGREGAR";
           // frm.ShowDialog();
        }
        private void UsrCtrl_OnButtonClick(object sender, EventArgs e)
        {
            dibujarProductos();
        }
        public void frmRegistroProducto_FormClosed(Object sender, FormClosedEventArgs e)
        {
            dibujarProductos();
        }

        private void txtbuscarproductos_TextChanged(object sender, EventArgs e)
        {
            dibujarProductos();
        }

        private void txtgrupo_TextChanged(object sender, EventArgs e)
        {
            dibujarGrupos();
        }
    }

}
