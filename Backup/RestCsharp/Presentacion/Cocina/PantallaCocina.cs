using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RestCsharp.Datos;
using RestCsharp.Logica;
using Sunat.Logica;

namespace RestCsharp.Presentacion.Cocina
{
    public partial class PantallaCocina : Form
    {
        public PantallaCocina()
        {
            InitializeComponent();
        }
        int idventa1;
        int idventa2;
        int idventa3;
        int idventa4;
        int idventa5;
        int idventa6;
        private int IddetalleVentaGeneral;
        private int idventaGeneral;
        private int cantidad_de_ventas_Pedidos_nuevos;
        private int contador_minutero1;
        private int contador_minutero2;
        private int contador_minutero3;
        private int contador_minutero4;
        private int contador_minutero5;
        private int contador_minutero6;
        string notageneral;
        string mesageneral;
        DataTable dtventas;
        Panel panel;
        private void PantallaCocina_Load(object sender, EventArgs e)
        {
            mostrarIdVentas();
            ObtenerDatos();
            TimerNUEVOSPEDIDOS.Start();
            FlowLayoutPanel1.Dock = DockStyle.Fill;

        }
        private void ObtenerDatos()
        {
            MostrarDetalleVenta1();
            MostrarDetalleVenta2();
            MostrarDetalleVenta3();
            MostrarDetalleVenta4();
            MostrarDetalleVenta5();
            MostrarDetalleVenta6();
        }
        private void mostrarIdVentas()
        {
            dtventas = new DataTable();
            var funcion = new Dventas();
            funcion.mostrarVentas(ref dtventas);
            foreach (DataRow rows in dtventas.Rows)
            {
                int Orden = Convert.ToInt32(rows["Orden"]);
                if (Orden == 1)
                {
                    idventa1 = Convert.ToInt32(rows["idventa"]);
                }
                if (Orden == 2)
                {
                    idventa2 = Convert.ToInt32(rows["idventa"]);
                }
                if (Orden == 3)
                {
                    idventa3 = Convert.ToInt32(rows["idventa"]);
                }
                if (Orden == 4)
                {
                    idventa4 = Convert.ToInt32(rows["idventa"]);
                }
                if (Orden == 5)
                {
                    idventa5 = Convert.ToInt32(rows["idventa"]);
                }
                if (Orden == 6)
                {
                    idventa6 = Convert.ToInt32(rows["idventa"]);
                }

            }
            ocultar_paneles_de_pedidos();
        }
        public void ocultar_paneles_de_pedidos()
        {
            var dt = new DataTable();
            var funcion = new Dventas();
            funcion.mostrarVentas(ref dt);
            int contador = dt.Rows.Count;
            if (contador == 0)
            {
                PSinpedidos.Visible = true;
                PSinpedidos.BringToFront();
                PSinpedidos.Dock = DockStyle.Fill;
                P1.Visible = false;
                P2.Visible = false;
                P3.Visible = false;
                P4.Visible = false;
                P5.Visible = false;
                P6.Visible = false;
            }
            if (contador > 0)
            {
                PSinpedidos.Visible = false;
                PSinpedidos.Dock = DockStyle.None;
            }
            if (contador == 1)
            {
                P1.Visible = true;
                P2.Visible = false;
                P3.Visible = false;
                P4.Visible = false;
                P5.Visible = false;
                P6.Visible = false;
            }
            if (contador == 2)
            {
                P1.Visible = true;
                P2.Visible = true;
                P3.Visible = false;
                P4.Visible = false;
                P5.Visible = false;
                P6.Visible = false;
            }
            if (contador == 3)
            {
                P1.Visible = true;
                P2.Visible = true;
                P3.Visible = true;
                P4.Visible = false;
                P5.Visible = false;
                P6.Visible = false;
            }

            if (contador == 4)
            {
                P1.Visible = true;
                P2.Visible = true;
                P3.Visible = true;
                P4.Visible = true;
                P5.Visible = false;
                P6.Visible = false;
            }
            if (contador == 5)
            {
                P1.Visible = true;
                P2.Visible = true;
                P3.Visible = true;
                P4.Visible = true;
                P5.Visible = true;
                P6.Visible = false;
            }
            if (contador == 6)
            {
                P1.Visible = true;
                P2.Visible = true;
                P3.Visible = true;
                P4.Visible = true;
                P5.Visible = true;
                P6.Visible = true;
            }
        }


        private void MostrarDetalleVenta1()
        {
            var dt = new DataTable();
            var funcion = new Ddetalleventas();
            var parametros = new Ldetalleventas();
            parametros.idventa = idventa1;
            funcion.mostrarDetalleVentaCocina(ref dt, parametros);
            datalistadoDetalledeventa1.DataSource = dt;
            Bases pintar = new Bases();
            pintar.DiseñoDatagridviewOscuro(ref datalistadoDetalledeventa1);
            datalistadoDetalledeventa1.Columns[2].Visible = false;
            datalistadoDetalledeventa1.Columns[3].Visible = false;
            datalistadoDetalledeventa1.Columns[6].Visible = false;
            datalistadoDetalledeventa1.Columns[7].Visible = false;
            datalistadoDetalledeventa1.Columns[8].Visible = false;
            datalistadoDetalledeventa1.Columns[9].Visible = false;
            datalistadoDetalledeventa1.Columns[10].Visible = false;
            datalistadoDetalledeventa1.Columns[11].Visible = false;
            datalistadoDetalledeventa1.Columns[12].Visible = false;
            datalistadoDetalledeventa1.Columns[13].Visible = false;
            datalistadoDetalledeventa1.Columns[15].Visible = false;
            datalistadoDetalledeventa1.Columns[16].Visible = false;
            foreach (DataGridViewRow rows in datalistadoDetalledeventa1.Rows)
            {
                string estado = Convert.ToString(rows.Cells["Estado"].Value);
                string hora = Convert.ToString(rows.Cells["Hora"].Value);
                string Mozo = Convert.ToString(rows.Cells["Mozo"].Value);
                string Mesa = Convert.ToString(rows.Cells["Mesa"].Value);
                string Donde_se_consumira = Convert.ToString(rows.Cells["Consumo"].Value);
                string alias = Convert.ToString(rows.Cells["Nombrellevar"].Value);
                string Minutos_transcurridos = Convert.ToString(rows.Cells["Minutos_transcurridos"].Value);
                string nota = Convert.ToString(rows.Cells["Nota"].Value);
                if (nota == "-")
                {
                    
                    panelm1.Visible = false;
                }
                else
                {
                    
                    panelm1.Visible = true;
                }
                if (estado == "ENVIADO")
                {
                    rows.Cells[0].Value = "Preparar";
                    rows.Cells[1].Value = "";
                    rows.DefaultCellStyle.BackColor = Color.FromArgb(49, 49, 49);
                    rows.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 49, 49);
                }
                else if (estado == "EN PREPARACION")
                {
                    rows.Cells[0].Value = "Despachar";
                    rows.Cells[1].Value = "Volver";
                    rows.DefaultCellStyle.BackColor = Color.SeaGreen;
                    rows.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
                }
                if (Mesa == "!@PARA LLEVAR@!")
                {
                    lblmesa1.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
                    lblmesa1.Text = alias;
                    etiquetamesa1.Visible = false;
                    btnNPedido1.BackgroundImage = Properties.Resources.naranja;
                    btnNPedido1.Font = new Font("Microsoft Sans Serif", 20, FontStyle.Bold);
                    btnNPedido1.Text = "Para llevar";
                }
                else
                {
                    lblmesa1.Font = new Font("Microsoft Sans Serif", 16, FontStyle.Regular | FontStyle.Bold);

                    lblmesa1.Text = Mesa;
                    etiquetamesa1.Visible = true;

                    btnNPedido1.BackgroundImage = Properties.Resources.verde;
                    btnNPedido1.Font = new Font("Microsoft Sans Serif", 57, FontStyle.Bold);
                    btnNPedido1.Text = "1";
                }
                lblMozo1.Text = Mozo;
                lblfecha1.Text = hora;
                lblminutos1.Text = Minutos_transcurridos;
                TimerP1.Start();
            }

        }
        private void MostrarDetalleVenta2()
        {
            DataTable dt = new DataTable();
            Ddetalleventas funcion = new Ddetalleventas();
            Ldetalleventas parametros = new Ldetalleventas();
            parametros.idventa = idventa2;
            funcion.mostrarDetalleVentaCocina(ref dt, parametros);
            datalistadoDetalledeventa2.DataSource = dt;
            Bases pintar = new Bases();
            pintar.DiseñoDatagridviewOscuro(ref datalistadoDetalledeventa2);
            datalistadoDetalledeventa2.Columns[2].Visible = false;
            datalistadoDetalledeventa2.Columns[3].Visible = false;
            datalistadoDetalledeventa2.Columns[6].Visible = false;
            datalistadoDetalledeventa2.Columns[7].Visible = false;
            datalistadoDetalledeventa2.Columns[8].Visible = false;
            datalistadoDetalledeventa2.Columns[9].Visible = false;
            datalistadoDetalledeventa2.Columns[10].Visible = false;
            datalistadoDetalledeventa2.Columns[11].Visible = false;
            datalistadoDetalledeventa2.Columns[12].Visible = false;
            datalistadoDetalledeventa2.Columns[13].Visible = false;
            datalistadoDetalledeventa2.Columns[15].Visible = false;
            datalistadoDetalledeventa2.Columns[16].Visible = false;

            foreach (DataGridViewRow rows in datalistadoDetalledeventa2.Rows)
            {
                string estado = Convert.ToString(rows.Cells["Estado"].Value);
                string hora = Convert.ToString(rows.Cells["Hora"].Value);
                string Mozo = Convert.ToString(rows.Cells["Mozo"].Value);
                string Mesa = Convert.ToString(rows.Cells["Mesa"].Value);
                string Donde_se_consumira = Convert.ToString(rows.Cells["Consumo"].Value);
                string alias = Convert.ToString(rows.Cells["Nombrellevar"].Value);

                string Minutos_transcurridos = Convert.ToString(rows.Cells["Minutos_transcurridos"].Value);
                string nota = Convert.ToString(rows.Cells["Nota"].Value);
                if (nota == "-")
                {
                    panelm2.Visible = false;
                }
                else
                {
                   
                    panelm2.Visible = true;
                }
                if (estado == "ENVIADO")
                {
                    rows.Cells[0].Value = "Preparar";
                    rows.Cells[1].Value = "";
                    rows.DefaultCellStyle.BackColor = Color.FromArgb(49, 49, 49);
                    rows.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 49, 49);
                }
                else if (estado == "EN PREPARACION")
                {
                    rows.Cells[0].Value = "Despachar";
                    rows.Cells[1].Value = "Volver";
                    rows.DefaultCellStyle.BackColor = Color.SeaGreen;
                    rows.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
                }
                if (Mesa == "!@PARA LLEVAR@!")
                {
                    lblmesa2.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);

                    lblmesa2.Text = alias;
                    etiquetamesa2.Visible = false;

                    btnNPedido2.BackgroundImage = Properties.Resources.naranja;
                    btnNPedido2.Font = new Font("Microsoft Sans Serif", 20, FontStyle.Bold);
                    btnNPedido2.Text = "Para llevar";
                }
                else
                {
                    lblmesa2.Font = new Font("Microsoft Sans Serif", 16, FontStyle.Regular | FontStyle.Bold);

                    lblmesa2.Text = Mesa;
                    etiquetamesa2.Visible = true;

                    btnNPedido2.BackgroundImage = Properties.Resources.verde;
                    btnNPedido2.Font = new Font("Microsoft Sans Serif", 57, FontStyle.Bold);
                    btnNPedido2.Text = "2";

                }

                lblMozo2.Text = Mozo;
                lblfecha2.Text = hora;
                lblminutos2.Text = Minutos_transcurridos;
                TimerP2.Start();
            }
        }
        private void MostrarDetalleVenta3()
        {
            DataTable dt = new DataTable();
            Ddetalleventas funcion = new Ddetalleventas();
            Ldetalleventas parametros = new Ldetalleventas();
            parametros.idventa = idventa3;
            funcion.mostrarDetalleVentaCocina(ref dt, parametros);
            datalistadoDetalledeventa3.DataSource = dt;
            Bases pintar = new Bases();
            pintar.DiseñoDatagridviewOscuro(ref datalistadoDetalledeventa3);
            datalistadoDetalledeventa3.Columns[2].Visible = false;
            datalistadoDetalledeventa3.Columns[3].Visible = false;
            datalistadoDetalledeventa3.Columns[6].Visible = false;
            datalistadoDetalledeventa3.Columns[7].Visible = false;
            datalistadoDetalledeventa3.Columns[8].Visible = false;
            datalistadoDetalledeventa3.Columns[9].Visible = false;
            datalistadoDetalledeventa3.Columns[10].Visible = false;
            datalistadoDetalledeventa3.Columns[11].Visible = false;
            datalistadoDetalledeventa3.Columns[12].Visible = false;
            datalistadoDetalledeventa3.Columns[13].Visible = false;
            datalistadoDetalledeventa3.Columns[15].Visible = false;
            datalistadoDetalledeventa3.Columns[16].Visible = false;

            foreach (DataGridViewRow rows in datalistadoDetalledeventa3.Rows)
            {
                string estado = Convert.ToString(rows.Cells["Estado"].Value);
                string hora = Convert.ToString(rows.Cells["Hora"].Value);
                string Mozo = Convert.ToString(rows.Cells["Mozo"].Value);
                string Mesa = Convert.ToString(rows.Cells["Mesa"].Value);
                string Donde_se_consumira = Convert.ToString(rows.Cells["Consumo"].Value);
                string alias = Convert.ToString(rows.Cells["Nombrellevar"].Value);

                string Minutos_transcurridos = Convert.ToString(rows.Cells["Minutos_transcurridos"].Value);
                string nota = Convert.ToString(rows.Cells["Nota"].Value);
                if (nota == "-")
                {
                    panelm3.Visible = false;
                }
                else
                {
                    
                    panelm3.Visible = true;
                }
                if (estado == "ENVIADO")
                {
                    rows.Cells[0].Value = "Preparar";
                    rows.Cells[1].Value = "";
                    rows.DefaultCellStyle.BackColor = Color.FromArgb(49, 49, 49);
                    rows.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 49, 49);
                }
                else if (estado == "EN PREPARACION")
                {
                    rows.Cells[0].Value = "Despachar";
                    rows.Cells[1].Value = "Volver";
                    rows.DefaultCellStyle.BackColor = Color.SeaGreen;
                    rows.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
                }
                if (Mesa == "!@PARA LLEVAR@!")
                {
                    lblmesa3.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);

                    lblmesa3.Text = alias;
                    etiquetamesa3.Visible = false;

                    btnNPedido3.BackgroundImage = Properties.Resources.naranja;
                    btnNPedido3.Font = new Font("Microsoft Sans Serif", 20, FontStyle.Bold);
                    btnNPedido3.Text = "Para llevar";
                }
                else
                {
                    lblmesa3.Font = new Font("Microsoft Sans Serif", 16, FontStyle.Regular | FontStyle.Bold);

                    lblmesa3.Text = Mesa;
                    etiquetamesa3.Visible = true;

                    btnNPedido3.BackgroundImage = Properties.Resources.verde;
                    btnNPedido3.Font = new Font("Microsoft Sans Serif", 57, FontStyle.Bold);
                    btnNPedido3.Text = "3";

                }

                lblMozo3.Text = Mozo;
                lblfecha3.Text = hora;
                lblminutos3.Text = Minutos_transcurridos;
                TimerP3.Start();
            }
        }
        private void MostrarDetalleVenta4()
        {
            DataTable dt = new DataTable();
            Ddetalleventas funcion = new Ddetalleventas();
            Ldetalleventas parametros = new Ldetalleventas();
            parametros.idventa = idventa4;
            funcion.mostrarDetalleVentaCocina(ref dt, parametros);
            datalistadoDetalledeventa4.DataSource = dt;
            Bases pintar = new Bases();
            pintar.DiseñoDatagridviewOscuro(ref datalistadoDetalledeventa4);
            datalistadoDetalledeventa4.Columns[2].Visible = false;
            datalistadoDetalledeventa4.Columns[3].Visible = false;
            datalistadoDetalledeventa4.Columns[6].Visible = false;
            datalistadoDetalledeventa4.Columns[7].Visible = false;
            datalistadoDetalledeventa4.Columns[8].Visible = false;
            datalistadoDetalledeventa4.Columns[9].Visible = false;
            datalistadoDetalledeventa4.Columns[10].Visible = false;
            datalistadoDetalledeventa4.Columns[11].Visible = false;
            datalistadoDetalledeventa4.Columns[12].Visible = false;
            datalistadoDetalledeventa4.Columns[13].Visible = false;
            datalistadoDetalledeventa4.Columns[15].Visible = false;
            datalistadoDetalledeventa4.Columns[16].Visible = false;

            foreach (DataGridViewRow rows in datalistadoDetalledeventa4.Rows)
            {
                string estado = Convert.ToString(rows.Cells["Estado"].Value);
                string hora = Convert.ToString(rows.Cells["Hora"].Value);
                string Mozo = Convert.ToString(rows.Cells["Mozo"].Value);
                string Mesa = Convert.ToString(rows.Cells["Mesa"].Value);
                string Donde_se_consumira = Convert.ToString(rows.Cells["Consumo"].Value);
                string alias = Convert.ToString(rows.Cells["Nombrellevar"].Value);

                string Minutos_transcurridos = Convert.ToString(rows.Cells["Minutos_transcurridos"].Value);
                string nota = Convert.ToString(rows.Cells["Nota"].Value);
                if (nota == "-")
                {
                    panelm4.Visible = false;
                }
                else
                {
                   
                    panelm4.Visible = true;
                }
                if (estado == "ENVIADO")
                {
                    rows.Cells[0].Value = "Preparar";
                    rows.Cells[1].Value = "";
                    rows.DefaultCellStyle.BackColor = Color.FromArgb(49, 49, 49);
                    rows.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 49, 49);
                }
                else if (estado == "EN PREPARACION")
                {
                    rows.Cells[0].Value = "Despachar";
                    rows.Cells[1].Value = "Volver";
                    rows.DefaultCellStyle.BackColor = Color.SeaGreen;
                    rows.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
                }
                if (Mesa == "!@PARA LLEVAR@!")
                {
                    lblmesa4.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);

                    lblmesa4.Text = alias;
                    etiquetamesa4.Visible = false;

                    btnNPedido4.BackgroundImage = Properties.Resources.naranja;
                    btnNPedido4.Font = new Font("Microsoft Sans Serif", 20, FontStyle.Bold);
                    btnNPedido4.Text = "Para llevar";
                }
                else
                {
                    lblmesa4.Font = new Font("Microsoft Sans Serif", 16, FontStyle.Regular | FontStyle.Bold);

                    lblmesa4.Text = Mesa;
                    etiquetamesa4.Visible = true;

                    btnNPedido4.BackgroundImage = Properties.Resources.verde;
                    btnNPedido4.Font = new Font("Microsoft Sans Serif", 57, FontStyle.Bold);
                    btnNPedido4.Text = "4";

                }

                lblMozo4.Text = Mozo;
                lblfecha4.Text = hora;
                lblminutos4.Text = Minutos_transcurridos;
                TimerP4.Start();
            }
        }
        private void MostrarDetalleVenta5()
        {
            DataTable dt = new DataTable();
            Ddetalleventas funcion = new Ddetalleventas();
            Ldetalleventas parametros = new Ldetalleventas();
            parametros.idventa = idventa5;
            funcion.mostrarDetalleVentaCocina(ref dt, parametros);
            datalistadoDetalledeventa5.DataSource = dt;
            Bases pintar = new Bases();
            pintar.DiseñoDatagridviewOscuro(ref datalistadoDetalledeventa5);
            datalistadoDetalledeventa5.Columns[2].Visible = false;
            datalistadoDetalledeventa5.Columns[3].Visible = false;
            datalistadoDetalledeventa5.Columns[6].Visible = false;
            datalistadoDetalledeventa5.Columns[7].Visible = false;
            datalistadoDetalledeventa5.Columns[8].Visible = false;
            datalistadoDetalledeventa5.Columns[9].Visible = false;
            datalistadoDetalledeventa5.Columns[10].Visible = false;
            datalistadoDetalledeventa5.Columns[11].Visible = false;
            datalistadoDetalledeventa5.Columns[12].Visible = false;
            datalistadoDetalledeventa5.Columns[13].Visible = false;
            datalistadoDetalledeventa5.Columns[15].Visible = false;
            datalistadoDetalledeventa5.Columns[16].Visible = false;

            foreach (DataGridViewRow rows in datalistadoDetalledeventa5.Rows)
            {
                string estado = Convert.ToString(rows.Cells["Estado"].Value);
                string hora = Convert.ToString(rows.Cells["Hora"].Value);
                string Mozo = Convert.ToString(rows.Cells["Mozo"].Value);
                string Mesa = Convert.ToString(rows.Cells["Mesa"].Value);
                string Donde_se_consumira = Convert.ToString(rows.Cells["Consumo"].Value);
                string alias = Convert.ToString(rows.Cells["Nombrellevar"].Value);

                string Minutos_transcurridos = Convert.ToString(rows.Cells["Minutos_transcurridos"].Value);
                string nota = Convert.ToString(rows.Cells["Nota"].Value);
                if (nota == "-")
                {
                    panelm5.Visible = false;
                }
                else
                {
                  
                    panelm5.Visible = true;
                }
                if (estado == "ENVIADO")
                {
                    rows.Cells[0].Value = "Preparar";
                    rows.Cells[1].Value = "";
                    rows.DefaultCellStyle.BackColor = Color.FromArgb(49, 49, 49);
                    rows.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 49, 49);
                }
                else if (estado == "EN PREPARACION")
                {
                    rows.Cells[0].Value = "Despachar";
                    rows.Cells[1].Value = "Volver";
                    rows.DefaultCellStyle.BackColor = Color.SeaGreen;
                    rows.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
                }
                if (Mesa == "!@PARA LLEVAR@!")
                {
                    lblmesa5.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);

                    lblmesa5.Text = alias;
                    etiquetamesa5.Visible = false;

                    btnNPedido5.BackgroundImage = Properties.Resources.naranja;
                    btnNPedido5.Font = new Font("Microsoft Sans Serif", 20, FontStyle.Bold);
                    btnNPedido5.Text = "Para llevar";
                }
                else
                {
                    lblmesa5.Font = new Font("Microsoft Sans Serif", 16, FontStyle.Regular | FontStyle.Bold);

                    lblmesa5.Text = Mesa;
                    etiquetamesa5.Visible = true;

                    btnNPedido5.BackgroundImage = Properties.Resources.verde;
                    btnNPedido5.Font = new Font("Microsoft Sans Serif", 57, FontStyle.Bold);
                    btnNPedido5.Text = "5";

                }

                lblMozo5.Text = Mozo;
                lblfecha5.Text = hora;
                lblminutos5.Text = Minutos_transcurridos;
                TimerP5.Start();
            }
        }
        private void MostrarDetalleVenta6()
        {
            DataTable dt = new DataTable();
            Ddetalleventas funcion = new Ddetalleventas();
            Ldetalleventas parametros = new Ldetalleventas();
            parametros.idventa = idventa6;
            funcion.mostrarDetalleVentaCocina(ref dt, parametros);
            datalistadoDetalledeventa6.DataSource = dt;
            Bases pintar = new Bases();
            pintar.DiseñoDatagridviewOscuro(ref datalistadoDetalledeventa6);
            datalistadoDetalledeventa6.Columns[2].Visible = false;
            datalistadoDetalledeventa6.Columns[3].Visible = false;
            datalistadoDetalledeventa6.Columns[6].Visible = false;
            datalistadoDetalledeventa6.Columns[7].Visible = false;
            datalistadoDetalledeventa6.Columns[8].Visible = false;
            datalistadoDetalledeventa6.Columns[9].Visible = false;
            datalistadoDetalledeventa6.Columns[10].Visible = false;
            datalistadoDetalledeventa6.Columns[11].Visible = false;
            datalistadoDetalledeventa6.Columns[12].Visible = false;
            datalistadoDetalledeventa6.Columns[13].Visible = false;
            datalistadoDetalledeventa6.Columns[15].Visible = false;
            datalistadoDetalledeventa6.Columns[16].Visible = false;

            foreach (DataGridViewRow rows in datalistadoDetalledeventa6.Rows)
            {
                string estado = Convert.ToString(rows.Cells["Estado"].Value);
                string hora = Convert.ToString(rows.Cells["Hora"].Value);
                string Mozo = Convert.ToString(rows.Cells["Mozo"].Value);
                string Mesa = Convert.ToString(rows.Cells["Mesa"].Value);
                string Donde_se_consumira = Convert.ToString(rows.Cells["Consumo"].Value);
                string alias = Convert.ToString(rows.Cells["Nombrellevar"].Value);

                string Minutos_transcurridos = Convert.ToString(rows.Cells["Minutos_transcurridos"].Value);
                string nota = Convert.ToString(rows.Cells["Nota"].Value);
                if (nota == "-")
                {
                    panelm6.Visible = false;
                }
                else
                {
                   
                    panelm6.Visible = true;
                }
                if (estado == "ENVIADO")
                {
                    rows.Cells[0].Value = "Preparar";
                    rows.Cells[1].Value = "";
                    rows.DefaultCellStyle.BackColor = Color.FromArgb(49, 49, 49);
                    rows.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 49, 49);
                }
                else if (estado == "EN PREPARACION")
                {
                    rows.Cells[0].Value = "Despachar";
                    rows.Cells[1].Value = "Volver";
                    rows.DefaultCellStyle.BackColor = Color.SeaGreen;
                    rows.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
                }
                if (Mesa == "!@PARA LLEVAR@!")
                {
                    lblmesa6.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);

                    lblmesa6.Text = alias;
                    etiquetamesa6.Visible = false;

                    btnNPedido6.BackgroundImage = Properties.Resources.naranja;
                    btnNPedido6.Font = new Font("Microsoft Sans Serif", 20, FontStyle.Bold);
                    btnNPedido6.Text = "Para llevar";
                }
                else
                {
                    lblmesa6.Font = new Font("Microsoft Sans Serif", 16, FontStyle.Regular | FontStyle.Bold);

                    lblmesa6.Text = Mesa;
                    etiquetamesa6.Visible = true;

                    btnNPedido6.BackgroundImage = Properties.Resources.verde;
                    btnNPedido6.Font = new Font("Microsoft Sans Serif", 57, FontStyle.Bold);
                    btnNPedido6.Text = "6";

                }

                lblMozo6.Text = Mozo;
                lblfecha6.Text = hora;
                lblminutos6.Text = Minutos_transcurridos;
                TimerP6.Start();
            }
        }

        private void TimerP1_Tick(object sender, EventArgs e)
        {
            if (idventa1 != 0)
            {
                contador_minutero1 += 1;
                if (contador_minutero1 == 60)
                {
                    int minutos_transcurridosEn_vivo = 0;
                    var funcion = new Dventas();
                    var parametros = new Lventas();
                    parametros.idventa = idventa1;
                    funcion.mostrarMinPedido(parametros, minutos_transcurridosEn_vivo);
                    lblminutos1.Text = minutos_transcurridosEn_vivo.ToString();
                    if (string.IsNullOrEmpty(lblminutos1.Text))
                    {
                        TimerP1.Stop();
                    }
                    contador_minutero1 = 0;

                }

            }
            else
            {
                TimerP1.Stop();
            }
        }

        private void TimerNUEVOSPEDIDOS_Tick(object sender, EventArgs e)
        {
            MostrarVentasnuevas();
            if (cantidad_de_ventas_Pedidos_nuevos > dtventas.Rows.Count)
            {
                TimerActualizador.Start();
            }
        }
        private void MostrarVentasnuevas()
        {
            Dventas funcion = new Dventas();
            funcion.MostrarVentasnuevas(ref cantidad_de_ventas_Pedidos_nuevos);
        }

        private void TimerP2_Tick(object sender, EventArgs e)
        {
            if (idventa2 != 0)
            {
                contador_minutero2 += 1;
                if (contador_minutero2 == 60)
                {
                    int minutos_transcurridosEn_vivo = 0;
                    Dventas funcion = new Dventas();
                    Lventas parametros = new Lventas();
                    parametros.idventa = idventa2;
                    funcion.mostrarMinPedido(parametros, minutos_transcurridosEn_vivo);
                    lblminutos2.Text = minutos_transcurridosEn_vivo.ToString();

                    if (string.IsNullOrEmpty(lblminutos2.Text))
                    {
                        TimerP2.Stop();

                    }

                    contador_minutero2 = 0;
                }
            }
            else
            {
                TimerP2.Stop();

            }
        }

        private void TimerP4_Tick(object sender, EventArgs e)
        {
            if (idventa4 != 0)
            {
                contador_minutero4 += 1;
                if (contador_minutero4 == 60)
                {
                    int minutos_transcurridosEn_vivo = 0;
                    Dventas funcion = new Dventas();
                    Lventas parametros = new Lventas();
                    parametros.idventa = idventa4;
                    funcion.mostrarMinPedido(parametros, minutos_transcurridosEn_vivo);
                    lblminutos4.Text = minutos_transcurridosEn_vivo.ToString();

                    if (string.IsNullOrEmpty(lblminutos4.Text))
                    {
                        TimerP4.Stop();

                    }

                    contador_minutero4 = 0;
                }
            }
            else
            {
                TimerP4.Stop();

            }
        }

        private void TimerP3_Tick(object sender, EventArgs e)
        {
            if (idventa3 != 0)
            {
                contador_minutero3 += 1;
                if (contador_minutero3 == 60)
                {
                    int minutos_transcurridosEn_vivo = 0;
                    Dventas funcion = new Dventas();
                    Lventas parametros = new Lventas();
                    parametros.idventa = idventa3;
                    funcion.mostrarMinPedido(parametros, minutos_transcurridosEn_vivo);
                    lblminutos3.Text = minutos_transcurridosEn_vivo.ToString();

                    if (string.IsNullOrEmpty(lblminutos3.Text))
                    {
                        TimerP3.Stop();

                    }

                    contador_minutero3 = 0;
                }
            }
            else
            {
                TimerP3.Stop();

            }
        }

        private void TimerP5_Tick(object sender, EventArgs e)
        {
            if (idventa5 != 0)
            {
                contador_minutero5 += 1;
                if (contador_minutero5 == 60)
                {
                    int minutos_transcurridosEn_vivo = 0;
                    Dventas funcion = new Dventas();
                    Lventas parametros = new Lventas();
                    parametros.idventa = idventa5;
                    funcion.mostrarMinPedido(parametros, minutos_transcurridosEn_vivo);
                    lblminutos5.Text = minutos_transcurridosEn_vivo.ToString();

                    if (string.IsNullOrEmpty(lblminutos5.Text))
                    {
                        TimerP5.Stop();

                    }

                    contador_minutero5 = 0;
                }
            }
            else
            {
                TimerP5.Stop();

            }
        }

        private void TimerP6_Tick(object sender, EventArgs e)
        {
            if (idventa6 != 0)
            {
                contador_minutero6 += 1;
                if (contador_minutero6 == 60)
                {
                    int minutos_transcurridosEn_vivo = 0;
                    Dventas funcion = new Dventas();
                    Lventas parametros = new Lventas();
                    parametros.idventa = idventa6;
                    funcion.mostrarMinPedido(parametros, minutos_transcurridosEn_vivo);
                    lblminutos6.Text = minutos_transcurridosEn_vivo.ToString();

                    if (string.IsNullOrEmpty(lblminutos6.Text))
                    {
                        TimerP6.Stop();

                    }

                    contador_minutero6 = 0;
                }
            }
            else
            {
                TimerP6.Stop();

            }
        }

        private void TimerActualizador_Tick(object sender, EventArgs e)
        {
            mostrarIdVentas();
            ObtenerDatos();
            TimerActualizador.Stop();
        }

        private void datalistadoDetalledeventa1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int iddetalle = Convert.ToInt32(datalistadoDetalledeventa1.SelectedCells[6].Value);
            if (e.ColumnIndex == datalistadoDetalledeventa1.Columns["Accion1"].Index)
            {
                foreach (DataGridViewRow rows in datalistadoDetalledeventa1.Rows)
                {
                    string Accion = Convert.ToString(rows.Cells["Accion1"].Value);
                    int iddetalleventa = Convert.ToInt32(rows.Cells["iddetalle_venta"].Value);
                    IddetalleVentaGeneral = iddetalle;
                    if (Accion == "Despachar")
                    {

                        if (iddetalle == iddetalleventa)
                        {
                            editarAdespachado();
                            datalistadoDetalledeventa1.Rows.Remove(datalistadoDetalledeventa1.CurrentRow);
                            mostrarIdVentas();
                            ObtenerDatos();
                        }
                    }

                    if (Accion == "Preparar")
                    {
                        if (iddetalle == iddetalleventa)
                        {
                            rows.Cells[0].Value = "Despachar";
                            rows.Cells[1].Value = "Volver";
                            rows.Cells[7].Value = "EN PREPARACION";
                            rows.DefaultCellStyle.BackColor = Color.SeaGreen;
                            rows.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
                            editarAenpreparacion();

                        }
                    }


                }

            }
            if (e.ColumnIndex == this.datalistadoDetalledeventa1.Columns["Volver1"].Index)
            {
                foreach (DataGridViewRow rows in datalistadoDetalledeventa1.Rows)
                {
                    string Accion = Convert.ToString(rows.Cells["Accion1"].Value);
                    int iddetalleventa = Convert.ToInt32(rows.Cells["iddetalle_venta"].Value);
                    IddetalleVentaGeneral = iddetalle;
                    if (Accion == "Despachar")
                    {
                        if (iddetalle == iddetalleventa)
                        {
                           
                            editarAenviado();
                            rows.Cells[0].Value = "Preparar";
                            rows.Cells[1].Value = "";
                            rows.Cells[7].Value = "ENVIADO";
                            rows.DefaultCellStyle.BackColor = Color.FromArgb(49, 49, 49);
                            rows.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 49, 49);
                        }
                    }
                }

            }

        }

        private void editarAdespachado()
        {
            var funcion = new Ddetalleventas();
            var parametros = new Ldetalleventas();
            parametros.iddetalle_venta = IddetalleVentaGeneral;
            funcion.editarAdespachado(parametros);
        }
        private void editarAenviado()
        {
            var funcion = new Ddetalleventas();
            var parametros = new Ldetalleventas();
            parametros.iddetalle_venta = IddetalleVentaGeneral;
            funcion.editarAenviadoSolo(parametros);
        }
        private void editarAenpreparacion()
        {
            var funcion = new Ddetalleventas();
            var parametros = new Ldetalleventas();
            parametros.iddetalle_venta = IddetalleVentaGeneral;
            funcion.editarAenpreparacion(parametros);
        }

        private void btnPrepararTodos1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow rows in datalistadoDetalledeventa1.Rows)
            {
                string Accion = Convert.ToString(rows.Cells["Accion1"].Value);
                int iddetalleventa = Convert.ToInt32(rows.Cells["iddetalle_venta"].Value);
                if (Accion == "Preparar")
                {
                    IddetalleVentaGeneral = iddetalleventa;
                    rows.Cells[0].Value = "Despachar";
                    rows.Cells[1].Value = "Volver";
                    rows.Cells[7].Value = "EN PREPARACION";
                    rows.DefaultCellStyle.BackColor = Color.SeaGreen;
                    rows.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
                    editarAenpreparacion();
                }
            }

        }

        private void btnDespacharTodos1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow rows in datalistadoDetalledeventa1.Rows)
            {
                int iddetalleventa = Convert.ToInt32(rows.Cells["iddetalle_venta"].Value);
                IddetalleVentaGeneral = iddetalleventa;
                editarAdespachado();
            }
            mostrarIdVentas();
            ObtenerDatos();
        }

        private void btnVer1_Click(object sender, EventArgs e)
        {
            mostrarNotasVentas(idventa1);
            MostrarNota();
        }
        private void mostrarNotasVentas(int idventa)
        {
            var funcion = new Dventas();
            var parametros = new Lventas();
            var dt = new DataTable();
            parametros.idventa = idventa;
            funcion.mostrarNotasVentasDt(ref dt, parametros);
            notageneral = dt.Rows[0][0].ToString();
            mesageneral = dt.Rows[0][1].ToString();
        }
        private void MostrarNota()
        {
            FlowLayoutPanel1.Enabled = false;
            panel = new Panel();
            var labelmesa = new Label ();
            var labelnota = new RichTextBox();
            var btncerrar = new Button();
            panel.Size = new Size(377, 335);
            panel.BackColor = Color.FromArgb(255, 222, 84);
            labelmesa.Text = "Mesa: " + mesageneral;
            labelmesa.Dock = DockStyle.Top;
            labelmesa.Font = new Font("Microsoft Sans Serif", 15, FontStyle.Bold);
            //*****
            labelnota.Text = notageneral;
            labelnota.Dock = DockStyle.Fill;
            labelnota.BorderStyle = BorderStyle.None;
            labelnota.BackColor = Color.FromArgb(255, 222, 84);
            labelnota.ReadOnly = true;
            labelnota.Font = new Font("Microsoft Sans Serif", 20);
            //******
            btncerrar.Text = "OK";
            btncerrar.Dock = DockStyle.Bottom;
            btncerrar.Font = new Font("Microsoft Sans Serif", 15,FontStyle.Bold);
            btncerrar.Size = new Size(50, 60);
            btncerrar.BackColor = Color.FromArgb(255, 204, 1);
            //******

            panel.Controls.Add(labelmesa);
            panel.Controls.Add(labelnota);
            panel.Controls.Add(btncerrar);
            this.Controls.Add(panel);
            panel.Location = new Point((Width - panel.Width) / 2, (Height - panel.Height) / 2);
            labelnota.BringToFront();
            panel.BringToFront();
            btncerrar.Click += Btncerrar_Click;

        }

        private void Btncerrar_Click(object sender, EventArgs e)
        {
            FlowLayoutPanel1.Enabled = true;
            this.Controls.Remove(panel);
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void datalistadoDetalledeventa2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int iddetalle = Convert.ToInt32(datalistadoDetalledeventa2.SelectedCells[6].Value);
            if (e.ColumnIndex == datalistadoDetalledeventa2.Columns["Accion2"].Index)
            {
                foreach (DataGridViewRow rows in datalistadoDetalledeventa2.Rows)
                {
                    string Accion = Convert.ToString(rows.Cells["Accion2"].Value);
                    int iddetalleventa = Convert.ToInt32(rows.Cells["iddetalle_venta"].Value);
                    IddetalleVentaGeneral = iddetalle;
                    if (Accion == "Despachar")
                    {

                        if (iddetalle == iddetalleventa)
                        {
                            editarAdespachado();
                            datalistadoDetalledeventa2.Rows.Remove(datalistadoDetalledeventa2.CurrentRow);
                            mostrarIdVentas();
                            ObtenerDatos();
                        }
                    }

                    if (Accion == "Preparar")
                    {
                        if (iddetalle == iddetalleventa)
                        {
                            rows.Cells[0].Value = "Despachar";
                            rows.Cells[1].Value = "Volver";
                            rows.Cells[7].Value = "EN PREPARACION";
                            rows.DefaultCellStyle.BackColor = Color.SeaGreen;
                            rows.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
                            editarAenpreparacion();

                        }
                    }


                }

            }
            if (e.ColumnIndex == this.datalistadoDetalledeventa2.Columns["Volver2"].Index)
            {
                foreach (DataGridViewRow rows in datalistadoDetalledeventa2.Rows)
                {
                    string Accion = Convert.ToString(rows.Cells["Accion2"].Value);
                    int iddetalleventa = Convert.ToInt32(rows.Cells["iddetalle_venta"].Value);
                    IddetalleVentaGeneral = iddetalle;
                    if (Accion == "Despachar")
                    {
                        if (iddetalle == iddetalleventa)
                        {

                            editarAenviado();
                            rows.Cells[0].Value = "Preparar";
                            rows.Cells[1].Value = "";
                            rows.Cells[7].Value = "ENVIADO";
                            rows.DefaultCellStyle.BackColor = Color.FromArgb(49, 49, 49);
                            rows.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 49, 49);
                        }
                    }
                }

            }

        }

        private void btnver2_Click(object sender, EventArgs e)
        {
            mostrarNotasVentas(idventa2);
            MostrarNota();
        }

        private void datalistadoDetalledeventa3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int iddetalle = Convert.ToInt32(datalistadoDetalledeventa3.SelectedCells[6].Value);
            if (e.ColumnIndex == datalistadoDetalledeventa3.Columns["Accion3"].Index)
            {
                foreach (DataGridViewRow rows in datalistadoDetalledeventa3.Rows)
                {
                    string Accion = Convert.ToString(rows.Cells["Accion3"].Value);
                    int iddetalleventa = Convert.ToInt32(rows.Cells["iddetalle_venta"].Value);
                    IddetalleVentaGeneral = iddetalle;
                    if (Accion == "Despachar")
                    {

                        if (iddetalle == iddetalleventa)
                        {
                            editarAdespachado();
                            datalistadoDetalledeventa3.Rows.Remove(datalistadoDetalledeventa3.CurrentRow);
                            mostrarIdVentas();
                            ObtenerDatos();
                        }
                    }

                    if (Accion == "Preparar")
                    {
                        if (iddetalle == iddetalleventa)
                        {
                            rows.Cells[0].Value = "Despachar";
                            rows.Cells[1].Value = "Volver";
                            rows.Cells[7].Value = "EN PREPARACION";
                            rows.DefaultCellStyle.BackColor = Color.SeaGreen;
                            rows.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
                            editarAenpreparacion();

                        }
                    }


                }

            }
            if (e.ColumnIndex == this.datalistadoDetalledeventa3.Columns["Volver3"].Index)
            {
                foreach (DataGridViewRow rows in datalistadoDetalledeventa3.Rows)
                {
                    string Accion = Convert.ToString(rows.Cells["Accion3"].Value);
                    int iddetalleventa = Convert.ToInt32(rows.Cells["iddetalle_venta"].Value);
                    IddetalleVentaGeneral = iddetalle;
                    if (Accion == "Despachar")
                    {
                        if (iddetalle == iddetalleventa)
                        {

                            editarAenviado();
                            rows.Cells[0].Value = "Preparar";
                            rows.Cells[1].Value = "";
                            rows.Cells[7].Value = "ENVIADO";
                            rows.DefaultCellStyle.BackColor = Color.FromArgb(49, 49, 49);
                            rows.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 49, 49);
                        }
                    }
                }

            }

        }

        private void datalistadoDetalledeventa4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int iddetalle = Convert.ToInt32(datalistadoDetalledeventa4.SelectedCells[6].Value);
            if (e.ColumnIndex == datalistadoDetalledeventa4.Columns["Accion4"].Index)
            {
                foreach (DataGridViewRow rows in datalistadoDetalledeventa4.Rows)
                {
                    string Accion = Convert.ToString(rows.Cells["Accion4"].Value);
                    int iddetalleventa = Convert.ToInt32(rows.Cells["iddetalle_venta"].Value);
                    IddetalleVentaGeneral = iddetalle;
                    if (Accion == "Despachar")
                    {

                        if (iddetalle == iddetalleventa)
                        {
                            editarAdespachado();
                            datalistadoDetalledeventa4.Rows.Remove(datalistadoDetalledeventa4.CurrentRow);
                            mostrarIdVentas();
                            ObtenerDatos();
                        }
                    }

                    if (Accion == "Preparar")
                    {
                        if (iddetalle == iddetalleventa)
                        {
                            rows.Cells[0].Value = "Despachar";
                            rows.Cells[1].Value = "Volver";
                            rows.Cells[7].Value = "EN PREPARACION";
                            rows.DefaultCellStyle.BackColor = Color.SeaGreen;
                            rows.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
                            editarAenpreparacion();

                        }
                    }


                }

            }
            if (e.ColumnIndex == this.datalistadoDetalledeventa4.Columns["Volver4"].Index)
            {
                foreach (DataGridViewRow rows in datalistadoDetalledeventa4.Rows)
                {
                    string Accion = Convert.ToString(rows.Cells["Accion4"].Value);
                    int iddetalleventa = Convert.ToInt32(rows.Cells["iddetalle_venta"].Value);
                    IddetalleVentaGeneral = iddetalle;
                    if (Accion == "Despachar")
                    {
                        if (iddetalle == iddetalleventa)
                        {

                            editarAenviado();
                            rows.Cells[0].Value = "Preparar";
                            rows.Cells[1].Value = "";
                            rows.Cells[7].Value = "ENVIADO";
                            rows.DefaultCellStyle.BackColor = Color.FromArgb(49, 49, 49);
                            rows.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 49, 49);
                        }
                    }
                }

            }

        }

        private void datalistadoDetalledeventa5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int iddetalle = Convert.ToInt32(datalistadoDetalledeventa5.SelectedCells[6].Value);
            if (e.ColumnIndex == datalistadoDetalledeventa5.Columns["Accion5"].Index)
            {
                foreach (DataGridViewRow rows in datalistadoDetalledeventa5.Rows)
                {
                    string Accion = Convert.ToString(rows.Cells["Accion5"].Value);
                    int iddetalleventa = Convert.ToInt32(rows.Cells["iddetalle_venta"].Value);
                    IddetalleVentaGeneral = iddetalle;
                    if (Accion == "Despachar")
                    {

                        if (iddetalle == iddetalleventa)
                        {
                            editarAdespachado();
                            datalistadoDetalledeventa5.Rows.Remove(datalistadoDetalledeventa5.CurrentRow);
                            mostrarIdVentas();
                            ObtenerDatos();
                        }
                    }

                    if (Accion == "Preparar")
                    {
                        if (iddetalle == iddetalleventa)
                        {
                            rows.Cells[0].Value = "Despachar";
                            rows.Cells[1].Value = "Volver";
                            rows.Cells[7].Value = "EN PREPARACION";
                            rows.DefaultCellStyle.BackColor = Color.SeaGreen;
                            rows.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
                            editarAenpreparacion();

                        }
                    }


                }

            }
            if (e.ColumnIndex == this.datalistadoDetalledeventa5.Columns["Volver5"].Index)
            {
                foreach (DataGridViewRow rows in datalistadoDetalledeventa5.Rows)
                {
                    string Accion = Convert.ToString(rows.Cells["Accion5"].Value);
                    int iddetalleventa = Convert.ToInt32(rows.Cells["iddetalle_venta"].Value);
                    IddetalleVentaGeneral = iddetalle;
                    if (Accion == "Despachar")
                    {
                        if (iddetalle == iddetalleventa)
                        {

                            editarAenviado();
                            rows.Cells[0].Value = "Preparar";
                            rows.Cells[1].Value = "";
                            rows.Cells[7].Value = "ENVIADO";
                            rows.DefaultCellStyle.BackColor = Color.FromArgb(49, 49, 49);
                            rows.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 49, 49);
                        }
                    }
                }

            }

        }

        private void datalistadoDetalledeventa6_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int iddetalle = Convert.ToInt32(datalistadoDetalledeventa6.SelectedCells[6].Value);
            if (e.ColumnIndex == datalistadoDetalledeventa6.Columns["Accion6"].Index)
            {
                foreach (DataGridViewRow rows in datalistadoDetalledeventa6.Rows)
                {
                    string Accion = Convert.ToString(rows.Cells["Accion6"].Value);
                    int iddetalleventa = Convert.ToInt32(rows.Cells["iddetalle_venta"].Value);
                    IddetalleVentaGeneral = iddetalle;
                    if (Accion == "Despachar")
                    {

                        if (iddetalle == iddetalleventa)
                        {
                            editarAdespachado();
                            datalistadoDetalledeventa6.Rows.Remove(datalistadoDetalledeventa6.CurrentRow);
                            mostrarIdVentas();
                            ObtenerDatos();
                        }
                    }

                    if (Accion == "Preparar")
                    {
                        if (iddetalle == iddetalleventa)
                        {
                            rows.Cells[0].Value = "Despachar";
                            rows.Cells[1].Value = "Volver";
                            rows.Cells[7].Value = "EN PREPARACION";
                            rows.DefaultCellStyle.BackColor = Color.SeaGreen;
                            rows.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
                            editarAenpreparacion();

                        }
                    }


                }

            }
            if (e.ColumnIndex == this.datalistadoDetalledeventa6.Columns["Volver6"].Index)
            {
                foreach (DataGridViewRow rows in datalistadoDetalledeventa6.Rows)
                {
                    string Accion = Convert.ToString(rows.Cells["Accion6"].Value);
                    int iddetalleventa = Convert.ToInt32(rows.Cells["iddetalle_venta"].Value);
                    IddetalleVentaGeneral = iddetalle;
                    if (Accion == "Despachar")
                    {
                        if (iddetalle == iddetalleventa)
                        {

                            editarAenviado();
                            rows.Cells[0].Value = "Preparar";
                            rows.Cells[1].Value = "";
                            rows.Cells[7].Value = "ENVIADO";
                            rows.DefaultCellStyle.BackColor = Color.FromArgb(49, 49, 49);
                            rows.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 49, 49);
                        }
                    }
                }

            }

        }

        private void btnPrepararTodos2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow rows in datalistadoDetalledeventa2.Rows)
            {
                string Accion = Convert.ToString(rows.Cells["Accion2"].Value);
                int iddetalleventa = Convert.ToInt32(rows.Cells["iddetalle_venta"].Value);
                if (Accion == "Preparar")
                {
                    IddetalleVentaGeneral = iddetalleventa;
                    rows.Cells[0].Value = "Despachar";
                    rows.Cells[1].Value = "Volver";
                    rows.Cells[7].Value = "EN PREPARACION";
                    rows.DefaultCellStyle.BackColor = Color.SeaGreen;
                    rows.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
                    editarAenpreparacion();
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow rows in datalistadoDetalledeventa3.Rows)
            {
                string Accion = Convert.ToString(rows.Cells["Accion3"].Value);
                int iddetalleventa = Convert.ToInt32(rows.Cells["iddetalle_venta"].Value);
                if (Accion == "Preparar")
                {
                    IddetalleVentaGeneral = iddetalleventa;
                    rows.Cells[0].Value = "Despachar";
                    rows.Cells[1].Value = "Volver";
                    rows.Cells[7].Value = "EN PREPARACION";
                    rows.DefaultCellStyle.BackColor = Color.SeaGreen;
                    rows.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
                    editarAenpreparacion();
                }
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow rows in datalistadoDetalledeventa4.Rows)
            {
                string Accion = Convert.ToString(rows.Cells["Accion4"].Value);
                int iddetalleventa = Convert.ToInt32(rows.Cells["iddetalle_venta"].Value);
                if (Accion == "Preparar")
                {
                    IddetalleVentaGeneral = iddetalleventa;
                    rows.Cells[0].Value = "Despachar";
                    rows.Cells[1].Value = "Volver";
                    rows.Cells[7].Value = "EN PREPARACION";
                    rows.DefaultCellStyle.BackColor = Color.SeaGreen;
                    rows.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
                    editarAenpreparacion();
                }
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow rows in datalistadoDetalledeventa5.Rows)
            {
                string Accion = Convert.ToString(rows.Cells["Accion5"].Value);
                int iddetalleventa = Convert.ToInt32(rows.Cells["iddetalle_venta"].Value);
                if (Accion == "Preparar")
                {
                    IddetalleVentaGeneral = iddetalleventa;
                    rows.Cells[0].Value = "Despachar";
                    rows.Cells[1].Value = "Volver";
                    rows.Cells[7].Value = "EN PREPARACION";
                    rows.DefaultCellStyle.BackColor = Color.SeaGreen;
                    rows.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
                    editarAenpreparacion();
                }
            }
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow rows in datalistadoDetalledeventa6.Rows)
            {
                string Accion = Convert.ToString(rows.Cells["Accion6"].Value);
                int iddetalleventa = Convert.ToInt32(rows.Cells["iddetalle_venta"].Value);
                if (Accion == "Preparar")
                {
                    IddetalleVentaGeneral = iddetalleventa;
                    rows.Cells[0].Value = "Despachar";
                    rows.Cells[1].Value = "Volver";
                    rows.Cells[7].Value = "EN PREPARACION";
                    rows.DefaultCellStyle.BackColor = Color.SeaGreen;
                    rows.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
                    editarAenpreparacion();
                }
            }
        }

        private void btnDespacharTodos2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow rows in datalistadoDetalledeventa2.Rows)
            {
                int iddetalleventa = Convert.ToInt32(rows.Cells["iddetalle_venta"].Value);
                IddetalleVentaGeneral = iddetalleventa;
                editarAdespachado();
            }
            mostrarIdVentas();
            ObtenerDatos();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow rows in datalistadoDetalledeventa3.Rows)
            {
                int iddetalleventa = Convert.ToInt32(rows.Cells["iddetalle_venta"].Value);
                IddetalleVentaGeneral = iddetalleventa;
                editarAdespachado();
            }
            mostrarIdVentas();
            ObtenerDatos();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow rows in datalistadoDetalledeventa4.Rows)
            {
                int iddetalleventa = Convert.ToInt32(rows.Cells["iddetalle_venta"].Value);
                IddetalleVentaGeneral = iddetalleventa;
                editarAdespachado();
            }
            mostrarIdVentas();
            ObtenerDatos();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow rows in datalistadoDetalledeventa5.Rows)
            {
                int iddetalleventa = Convert.ToInt32(rows.Cells["iddetalle_venta"].Value);
                IddetalleVentaGeneral = iddetalleventa;
                editarAdespachado();
            }
            mostrarIdVentas();
            ObtenerDatos();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow rows in datalistadoDetalledeventa6.Rows)
            {
                int iddetalleventa = Convert.ToInt32(rows.Cells["iddetalle_venta"].Value);
                IddetalleVentaGeneral = iddetalleventa;
                editarAdespachado();
            }
            mostrarIdVentas();
            ObtenerDatos();
        }

        private void btnver3_Click(object sender, EventArgs e)
        {
            mostrarNotasVentas(idventa3);
            MostrarNota();
        }

        private void btnver6_Click(object sender, EventArgs e)
        {
            mostrarNotasVentas(idventa6);
            MostrarNota();
        }

        private void btnver5_Click(object sender, EventArgs e)
        {
            mostrarNotasVentas(idventa5);
            MostrarNota();
        }

        private void btnver4_Click(object sender, EventArgs e)
        {
            mostrarNotasVentas(idventa4);
            MostrarNota();
        }
    }
}
