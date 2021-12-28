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

namespace RestCsharp.Presentacion.Mesas_salones
{
    public partial class Configurar_mesas_ok : UserControl
    {
        int id_salon = 0;
        string estado;
        public static string nombre_mesa;
        public static int idmesa;
        string salon;
        public Configurar_mesas_ok()
        {
            InitializeComponent();
        }

        private void Configurar_mesas_ok_Load(object sender, EventArgs e)
        {
            PanelBienvenida.Dock = DockStyle.Fill;
            dibujarSalones();
        }

        private void dibujarMESAS()
        {
            try
            {
                PanelMesas.Controls.Clear();
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("mostrar_mesas_por_salon", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_salon", id_salon);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Button b = new Button();
                    Panel panel = new Panel();
                    int alto = Convert.ToInt32(rdr["y"].ToString());
                    int ancho = Convert.ToInt32(rdr["x"].ToString());
                    int tamanio_letra = Convert.ToInt32(rdr["Tamanio_letra"].ToString());
                    Point tamanio = new Point(ancho, alto);

                    panel.BackgroundImage = Properties.Resources.mesa_vacia;
                    panel.BackgroundImageLayout = ImageLayout.Zoom;
                    panel.Cursor = Cursors.Hand;
                    panel.Tag = rdr["Id_mesa"].ToString();
                    panel.Size = new System.Drawing.Size(tamanio);


                    b.Text = rdr["Mesa"].ToString();
                    b.Name = rdr["Id_mesa"].ToString();

                    if (b.Text != "NULO")
                    {
                        b.Size = new System.Drawing.Size(tamanio);
                        b.BackColor = Color.Transparent;
                        b.BackgroundImage = Properties.Resources.verde;
                        b.BackgroundImageLayout = ImageLayout.Zoom;
                        b.FlatAppearance.MouseDownBackColor = Color.Transparent;
                        b.FlatAppearance.MouseOverBackColor = Color.Transparent;
                        b.Font = new System.Drawing.Font("Microsoft Sans Serif", tamanio_letra);
                        b.FlatStyle = FlatStyle.Flat;
                        b.FlatAppearance.BorderSize = 0;
                        b.ForeColor = Color.White;
                        PanelMesas.Controls.Add(b);
                    }
                    else
                    {
                        PanelMesas.Controls.Add(panel);
                    }
                    b.Click += new EventHandler(miEvento);
                    panel.Click += new EventHandler(miEventopanel_click);
                }
                CONEXIONMAESTRA.cerrar();

            }
            catch (Exception ex)
            {
                CONEXIONMAESTRA.cerrar();
                MessageBox.Show(ex.StackTrace);
            }

        }
        private void miEvento(System.Object sender, EventArgs e)
        {
            nombre_mesa = ((Button)sender).Text;
            idmesa = Convert.ToInt32(((Button)sender).Name);
            Iragregarmesa();
        }
        private void miEventopanel_click(System.Object sender, EventArgs e)
        {
            idmesa = Convert.ToInt32(((Panel)sender).Tag);
            Iragregarmesa();
        }
        private void Iragregarmesa()
        {
            var frm = new Agregar_mesa_ok();
            panel1.Visible = false;
            frm.guardarClick += guardarmesaClick;
            frm.cerrarClick += cerrarmesaClick;
            frm.Dock = DockStyle.Fill;
            this.Controls.Add(frm);
            frm.BringToFront();
        }

        private void guardarmesaClick(object sender, EventArgs e)
        {
            panel1.Visible = true;
            dibujarMESAS();
        }
        private void cerrarmesaClick(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }


        private void dibujarSalones()
        {


            flowLayoutPanel1.Controls.Clear();
            try
            {
                CONEXIONMAESTRA.abrir();
                string query = "select * from SALON";
                SqlCommand cmd = new SqlCommand(query, CONEXIONMAESTRA.conectar);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    Button b = new Button();
                    Panel panelC1 = new Panel();
                    Panel panelLATERAL = new Panel();

                    b.Text = rdr["Salon"].ToString();
                    b.Name = rdr["Id_salon"].ToString();
                    b.Tag = rdr["Estado"].ToString();
                    b.Dock = DockStyle.Fill;
                    b.BackColor = Color.Transparent;
                    b.Font = new System.Drawing.Font("Microsoft Sans Serif", 12);
                    b.FlatStyle = FlatStyle.Flat;
                    b.FlatAppearance.BorderSize = 0;
                    b.FlatAppearance.MouseDownBackColor = Color.FromArgb(64, 64, 64);
                    b.FlatAppearance.MouseOverBackColor = Color.FromArgb(43, 43, 43);

                    b.TextAlign = ContentAlignment.MiddleLeft;
                    b.Tag = rdr["Estado"].ToString();

                    panelC1.Size = new System.Drawing.Size(244, 58);
                    panelC1.Name = rdr["Id_salon"].ToString();

                    panelLATERAL.Size = new System.Drawing.Size(3, 58);
                    panelLATERAL.Dock = DockStyle.Left;
                    panelLATERAL.BackColor = Color.Transparent;
                    panelLATERAL.Name = rdr["Id_salon"].ToString();

                    string estado = rdr["Estado"].ToString();
                    if (estado == "ELIMINADO")
                    {
                        b.Text = rdr["Salon"].ToString() + " - Eliminado";
                        b.ForeColor = Color.FromArgb(231, 63, 67);
                    }
                    else
                    {
                        b.ForeColor = Color.White;
                    }
                    panelC1.Controls.Add(b);
                    panelC1.Controls.Add(panelLATERAL);
                    flowLayoutPanel1.Controls.Add(panelC1);

                    b.BringToFront();
                    panelLATERAL.SendToBack();

                    b.Click += new EventHandler(miEvento_salon_button);
                }
                CONEXIONMAESTRA.cerrar();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void miEvento_salon_button(System.Object sender, EventArgs e)
        {
            PanelBienvenida.Visible = false;
            PanelBienvenida.Dock = DockStyle.None;
            PanelMesas.Visible = true;
            PanelMesas.Dock = DockStyle.Fill;
            id_salon = Convert.ToInt32(((Button)sender).Name);
            estado = Convert.ToString(((Button)sender).Tag);
            salon = Convert.ToString(((Button)sender).Text);
            dibujarMESAS();
            foreach (System.Windows.Forms.Control panelC2 in flowLayoutPanel1.Controls)
            {
                if (panelC2 is Panel)
                {
                    foreach (System.Windows.Forms.Control panelLATERAL2 in panelC2.Controls)
                    {
                        if (panelLATERAL2 is Panel)
                        {
                            panelLATERAL2.BackColor = Color.Transparent;
                            panelC2.BackColor = Color.Transparent;
                            break;

                        }
                    }
                }
            }


            string NOMBRE = ((Button)sender).Name;
            foreach (System.Windows.Forms.Control panelC1 in flowLayoutPanel1.Controls)
            {
                if (panelC1 is Panel)
                {

                    foreach (System.Windows.Forms.Control panelLATERAL in panelC1.Controls)
                    {
                        if (panelLATERAL is Panel)
                        {
                            if (panelLATERAL.Name == NOMBRE)
                            {
                                panelLATERAL.BackColor = Color.OrangeRed;
                                panelC1.BackColor = Color.FromArgb(43, 43, 43);
                                break;
                                break;
                            }
                        }
                    }

                }
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Salones.proceso = "NUEVO";
            panel1.Visible = false;
            //var frm = new Salones();
            //frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
            //frm.ShowDialog();
            var frm = new Salones();
            frm.OnButtonClick += UsrCtrl_OnButtonClick;
            frm.Dock = DockStyle.Fill;
            this.Controls.Add(frm);
            frm.BringToFront();
        }
       
        private void UsrCtrl_OnButtonClick(object sender, EventArgs e)
        {
            panel1.Visible = true;
            dibujarSalones();
        }
       

        private void Button9_Click(object sender, EventArgs e)
        {
            aumentar_tamaño_mesa();
        }
        internal void aumentar_tamaño_mesa()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                CONEXIONMAESTRA.abrir();
                cmd = new SqlCommand("aumentar_tamanio_mesa", CONEXIONMAESTRA.conectar);
                cmd.ExecuteNonQuery();
                CONEXIONMAESTRA.cerrar();
                dibujarMESAS();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        internal void disminuir_tamanio_mesa()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                CONEXIONMAESTRA.abrir();
                cmd = new SqlCommand("disminuir_tamanio_mesa", CONEXIONMAESTRA.conectar);
                cmd.ExecuteNonQuery();
                CONEXIONMAESTRA.cerrar();
                dibujarMESAS();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        internal void aumentar_tamanio_letra()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                CONEXIONMAESTRA.abrir();
                cmd = new SqlCommand("aumentar_tamanio_letra", CONEXIONMAESTRA.conectar);
                cmd.ExecuteNonQuery();
                CONEXIONMAESTRA.cerrar();
                dibujarMESAS();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Button8_Click(object sender, EventArgs e)
        {
            disminuir_tamanio_mesa();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            aumentar_tamanio_letra();
        }
        internal void disminuir_tamanio_letra()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                CONEXIONMAESTRA.abrir();
                cmd = new SqlCommand("disminuir_tamanio_letra", CONEXIONMAESTRA.conectar);
                cmd.ExecuteNonQuery();
                CONEXIONMAESTRA.cerrar();
                dibujarMESAS();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            disminuir_tamanio_letra();
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (id_salon > 0)
            {
                var funcion = new Dsalon();
                var parametros = new Lsalon();
                parametros.Id_salon = id_salon;
                funcion.eliminarSalon(parametros);
                dibujarSalones();
                dibujarMESAS();
                id_salon = 0;
            }
            else
            {
                MessageBox.Show("Seleccione un salon para eliminar");
            }
        }

        private void btneditar_Click(object sender, EventArgs e)
        {
            if (id_salon > 0)
            {
                Salones.idsalon = id_salon;
                Salones.proceso = "EDICION";
                Salones.salon = salon;
                var frm = new Salones();
                frm.OnButtonClick += UsrCtrl_OnButtonClick;
                frm.Dock = DockStyle.Fill;
                this.Controls.Add(frm);
                frm.BringToFront();
            }
            else
            {
                MessageBox.Show("Seleccione un salon para eliminar");
            }
        }

        private void Frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            dibujarSalones();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
