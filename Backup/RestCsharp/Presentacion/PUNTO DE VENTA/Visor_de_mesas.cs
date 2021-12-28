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
using RestCsharp.Presentacion.Cocina;
using RestCsharp.Presentacion.Login;
using RestCsharp.Presentacion.Caja;
using RestCsharp.Presentacion.GeneradorQR;
using RestCsharp.Presentacion.Reportes;
using Sunat.Logica;
using RestCsharp.Sunat.SunatForms;

namespace RestCsharp.Presentacion.PUNTO_DE_VENTA
{
    public partial class Visor_de_mesas : UserControl
    {
        public Visor_de_mesas()
        {
            InitializeComponent();
        }
        int id_salon;
        int contadorMesasxSalon;
        string estado;
        string Union_de_mesas;
        int Paso = 0;
        int idmesa_Origen;
        int idmesa_Destino;
        int idmesa;
        string nombre_mesa;
        string estado_de_mesa;
        int id_venta_mesa_origen;
        int id_venta_mesa_destino;
        int Estado_de_herramientas = 0;
        DataTable dtmozos;
        string Tiponotas;
        string Ip;
        int idventa = 0;
        int contadorSoliEsc = 0;

        string tipocaja;
        #region TAREAS DE EVENTOS
        private void UserCtrl_SalirClick(object sender, EventArgs e)
        {
            Dispose();
        }
        #endregion

        private void Visor_de_mesas_Load(object sender, EventArgs e)
        {
            ValidarPermisos();
            ObtenerIpLocal();
            PanelBienvenida.Dock = DockStyle.Fill;
            dibujarSalones();
            Union_de_mesas = "INACTIVO";
            ObtenerIdsalonInicio();
            ContarMesasxSalon();
            ObtenerTipoNotas();
            Mostrartipocaja();
        }

        private void ObtenerTipoNotas()
        {
            var funcion = new Dempresa();
            var dt = new DataTable();
            funcion.mostrar_empresa(ref dt);
            Tiponotas = dt.Rows[0][10].ToString();
        }

        private void EliminarSolImpri()
        {
            var funcion = new Dventas();
            var parametros = new Lventas();
            parametros.idventa = idventa;
            funcion.eliminarSolicitudImpr(parametros);
        }
        private void ValidarPermisos()
        {
            var dt = new DataTable();
            var funcion = new Dpermisos();
            funcion.mostrar_Permisos(ref dt);
            foreach (DataRow data in dt.Rows)
            {
                string modulo = data["Modulo"].ToString();
                foreach (Control button in Panelbotones.Controls)
                {
                    if (button is Button)
                    {
                        if (button.Text == modulo)
                        {
                            button.Visible = true;
                        }
                    }
                }
                foreach (Control button in PanelHerramientas.Controls)
                {
                    if (button is Button)
                    {
                        if (button.Text == modulo)
                        {

                            button.Visible = true;

                        }
                    }
                }

            }
        }
        private void ObtenerIpLocal()
        {

            this.Text = Bases.ObtenerIp(ref Ip);
        }
        private void ContarMesasxSalon()
        {
            Lmesas parametros = new Lmesas();
            Dmesas funcion = new Dmesas();
            parametros.Id_salon = id_salon;
            funcion.ContarMesasSalon(ref contadorMesasxSalon, parametros);
            if (contadorMesasxSalon == 0)
            {
                PanelBienvenida.Visible = true;
                PanelBienvenida.Dock = DockStyle.Fill;
                PanelMesas.Visible = false;
                PanelMesas.Dock = DockStyle.None;
            }
            else
            {
                PanelBienvenida.Visible = false;
                PanelBienvenida.Dock = DockStyle.Fill;
                PanelMesas.Visible = true;
                PanelMesas.Dock = DockStyle.Fill;
                dibujarMESAS();
            }
        }
        private void ObtenerIdsalonInicio()
        {
            Dsalon funcion = new Dsalon();
            funcion.ObtenerSalonInicial(ref id_salon);
        }
        void dibujarSalones()
        {
            FlowLayoutPanel1.Controls.Clear();
            try
            {
                CONEXIONMAESTRA.abrir();
                string query = "Select * from SALON Where Estado='ACTIVO'";
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
                    b.ForeColor = Color.White;

                    panelC1.Size = new System.Drawing.Size(244, 58);
                    panelC1.Name = rdr["Id_salon"].ToString();

                    panelLATERAL.Size = new System.Drawing.Size(3, 58);
                    panelLATERAL.Dock = DockStyle.Left;
                    panelLATERAL.BackColor = Color.Transparent;
                    panelLATERAL.Name = rdr["Id_salon"].ToString();

                    panelC1.Controls.Add(b);
                    panelC1.Controls.Add(panelLATERAL);
                    FlowLayoutPanel1.Controls.Add(panelC1);
                    b.BringToFront();
                    panelLATERAL.SendToBack();

                    b.Click += new EventHandler(miEvento_salon_button);
                }
                CONEXIONMAESTRA.cerrar();
            }
            catch (Exception ex)
            {
                CONEXIONMAESTRA.cerrar();
                MessageBox.Show(ex.StackTrace);
            }
        }
        private void miEvento_salon_button(System.Object sender, EventArgs e)
        {
            try
            {
                PanelMesas.Visible = true;
                PanelMesas.Dock = DockStyle.Fill;
                id_salon = Convert.ToInt32(((Button)sender).Name);
                PanelBienvenida.Visible = false;
                PanelBienvenida.Dock = DockStyle.Fill;
                ContarMesasxSalon();
                foreach (System.Windows.Forms.Control panelC2 in FlowLayoutPanel1.Controls)
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
                foreach (System.Windows.Forms.Control panelC1 in FlowLayoutPanel1.Controls)
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

                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void mostrarMozoxMesa()
        {
            var funcion = new Dmesas();
            dtmozos = new DataTable();
            funcion.mostrar_mozo_por_mesa(ref dtmozos);
        }
        void dibujarMESAS()
        {
            mostrarMozoxMesa();
            PanelMesas.Controls.Clear();
            try
            {
                CONEXIONMAESTRA.abrir();
                string query = "mostrar_mesas_por_salon";
                SqlCommand cmd = new SqlCommand(query, CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_salon", id_salon);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Button b = new Button();
                    Panel panel = new Panel();
                    Label LabelMozo = new Label();
                    Panel panelContieneaB = new Panel();


                    int alto = Convert.ToInt32(rdr["y"].ToString());
                    int ancho = Convert.ToInt32(rdr["x"].ToString());
                    int tamanio_letra = Convert.ToInt32(rdr["Tamanio_letra"].ToString());
                    Point tamanio = new Point(ancho, alto);

                    panel.Tag = rdr["Id_mesa"].ToString();

                    b.Text = rdr["Mesa"].ToString();
                    b.Name = rdr["Id_mesa"].ToString();
                    b.Tag = rdr["Estado_de_Disponibilidad"].ToString();

                    panel.Size = new Size(tamanio);
                    ///
                    panelContieneaB.BackgroundImageLayout = ImageLayout.Stretch;


                    if (Convert.ToString(b.Tag) == "LIBRE")
                    {
                        panelContieneaB.BackgroundImage = Properties.Resources.verde;
                    }
                    else
                    {
                        panelContieneaB.BackgroundImage = Properties.Resources.rosa;
                        LabelMozo.Size = new Size(120, 30);
                        LabelMozo.BackColor = Color.Transparent;
                        foreach (DataRow row in dtmozos.Rows)
                        {
                            string mesa = Convert.ToString(row["Mesa"]);
                            string usuario = Convert.ToString(row["Login"]);
                            if (mesa == b.Text)
                            {
                                LabelMozo.Text = usuario;
                            }
                        }
                        b.Dock = DockStyle.Fill;
                        LabelMozo.Dock = DockStyle.Bottom;
                        LabelMozo.TextAlign = ContentAlignment.MiddleCenter;
                        LabelMozo.ForeColor = Color.White;
                        LabelMozo.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                        panelContieneaB.Controls.Add(LabelMozo);


                    }
                    if (b.Text != "NULO")
                    {
                        b.Size = new Size(tamanio);
                        b.Font = new Font("Microsoft Sans Serif", tamanio_letra);
                        b.FlatStyle = FlatStyle.Flat;
                        b.FlatAppearance.BorderSize = 0;
                        b.ForeColor = Color.White;

                        b.Cursor = Cursors.Hand;
                        b.BackgroundImageLayout = ImageLayout.Zoom;
                        b.BackColor = Color.Transparent;
                        b.FlatAppearance.MouseDownBackColor = Color.Transparent;
                        b.FlatAppearance.MouseOverBackColor = Color.Transparent;
                        panelContieneaB.Size = new Size(tamanio);
                        panelContieneaB.Controls.Add(b);
                        PanelMesas.Controls.Add(panelContieneaB);
                    }
                    else
                    {
                        PanelMesas.Controls.Add(panel);
                    }

                    b.Click += new EventHandler(miEvento_buton_mesa);
                }
                CONEXIONMAESTRA.cerrar();
            }
            catch (Exception ex)
            {
                CONEXIONMAESTRA.cerrar();
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void miEvento_buton_mesa(System.Object sender, EventArgs e)
        {
            if (Union_de_mesas == "INACTIVO")
            {
                try
                {
                    estado = ((Button)sender).Tag.ToString();
                    Punto_de_venta.estado = estado;
                    idmesa = Convert.ToInt32(((Button)sender).Name);
                    Punto_de_venta.Idmesa = idmesa;
                    Punto_de_venta.DondeConsumir = "EN LOCAL";
                    Punto_de_venta.Mesa = ((Button)sender).Text;
                    nombre_mesa = ((Button)sender).Text;
                   //Dispose();
                    var frm = new Punto_de_venta();
                    frm.SalirClick += UserCtrl_SalirClick;
                    frm.Dock = DockStyle.Fill;
                    this.Controls.Add(frm);
                    frm.BringToFront();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace + ex.Message);
                }
            }
            else if (Union_de_mesas == "ACTIVADO")
            {
                idmesa = Convert.ToInt32(((Button)sender).Name);
                estado_de_mesa = (((Button)sender).Tag).ToString();
                if (estado_de_mesa == "OCUPADO")
                {
                    Paso += 1;
                    labelPasos.Text = @"PASO 2
                    Seleccione la mesa de destino";
                    if (Paso == 1)
                    {
                        idmesa_Origen = idmesa;
                        mostrarIdVentamesaOrigen();
                    }
                    if (Paso == 2)
                    {
                        idmesa_Destino = idmesa;
                        mostrarIdVentamesaDestino();
                        EditarDvCambioMesa();
                        Cambio_de_Estado_de_mesa_de_origen();
                        Terminar_cambio_de_mesa();
                        dibujarMESAS();
                    }
                }
                else if (estado_de_mesa == "LIBRE" && Paso == 1)
                {
                    Paso += 1;
                    if (Paso == 2)
                    {
                        idmesa_Destino = idmesa;
                        Terminar_cambio_de_mesa();
                        editar_mesa_en_la_tabla_ventas();
                        Cambio_de_Estado_de_mesa_de_origen();
                        Cambio_de_Estado_de_mesa_de_DESTINO();
                        dibujarMESAS();
                        Paso = 0;

                    }
                }

            }

        }
        private void Cambio_de_Estado_de_mesa_de_DESTINO()
        {
            var funcion = new Dmesas();
            var parametros = new Lmesas();
            parametros.Id_mesa = idmesa_Destino;
            funcion.EditarEstadoMesaOcupado(parametros);


        }

        private void editar_mesa_en_la_tabla_ventas()
        {
            var funcion = new Dventas();
            var parametros = new Lventas();
            parametros.Id_mesa = idmesa_Destino;
            parametros.idventa = id_venta_mesa_origen;
            funcion.editaMesaenVentas(parametros);

        }

        private void Terminar_cambio_de_mesa()
        {
            PanelSalones.Visible = true;
            PanelUNIONMesas.Visible = false;
            Union_de_mesas = "INACTIVO";
            Paso = 0;
        }
        private void Cambio_de_Estado_de_mesa_de_origen()
        {
            var funcion = new Dmesas();
            var parametros = new Lmesas();
            parametros.Id_mesa = idmesa_Origen;
            funcion.EditarMesaAotra(parametros);
        }
        private void EditarDvCambioMesa()
        {
            var funcion = new Dventas();
            funcion.EditarDvCambioMesa(idmesa_Origen, idmesa_Destino);
        }
        private void mostrarIdVentamesaDestino()
        {
            var funcion = new Dventas();
            var parametros = new Lventas();
            parametros.Id_mesa = idmesa_Origen;
            funcion.mostrarIdventaMesa(ref id_venta_mesa_destino, parametros);
        }
        private void mostrarIdVentamesaOrigen()
        {
            var funcion = new Dventas();
            var parametros = new Lventas();
            parametros.Id_mesa = idmesa_Origen;
            funcion.mostrarIdventaMesa(ref id_venta_mesa_origen, parametros);
        }
        private void btnHerramientas_Click(object sender, EventArgs e)
        {
            if (Estado_de_herramientas == 1)
            {
                PanelHerramientas.Visible = false;
                Estado_de_herramientas = 0;
            }
            else if (Estado_de_herramientas == 0)
            {

                PanelHerramientas.Location = new Point(PanelBienvenida.Location.X, Panelbotones.Location.Y + btnHerramientas.Location.Y);
                PanelHerramientas.Visible = true;
                PanelHerramientas.BringToFront();

                Estado_de_herramientas = 1;
            }
        }

        private void btnadministrar_Click(object sender, EventArgs e)
        {
            var frm = new Configuraciones.Menu_de_configuraciones();
            frm.Dock = DockStyle.Fill;
            this.Controls.Add(frm);
            frm.BringToFront();
        }

        private void btnVerCuentas_Click(object sender, EventArgs e)
        {
            Dispose();
            VisorDecuentas frm = new VisorDecuentas();
            frm.ShowDialog();
        }

        private void btnParallevar_Click(object sender, EventArgs e)
        {
           // Dispose();
           // var frm = new Punto_de_venta();
            Punto_de_venta.DondeConsumir = "PARA LLEVAR";
            var frm = new Punto_de_venta();
            frm.SalirClick += UserCtrl_SalirClick;
            frm.Dock = DockStyle.Fill;
            this.Controls.Add(frm);
            frm.BringToFront();
        }

        private void btnCambiomesa_Click(object sender, EventArgs e)
        {
            panelSunat.Visible = false;
            PanelMesas.Visible = true;
            PanelUNIONMesas.Visible = true;
            PanelUNIONMesas.Dock = DockStyle.Left;
            PanelUNIONMesas.SendToBack();
            PanelSalones.Visible = false;
            Union_de_mesas = "ACTIVADO";
            labelPasos.Text = @"PASO 1
            Seleccione una mesa de Origen";
            Paso = 0;
            OcultarPanelesConfiguraciones();
        }
        private void OcultarPanelesConfiguraciones()
        {
            if (Estado_de_herramientas == 1)
            {
                PanelHerramientas.Visible = false;
                Estado_de_herramientas = 0;
            }
        }

        private void btncocina_Click(object sender, EventArgs e)
        {
            var frm = new PantallaCocina();
            frm.ShowDialog();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Dispose();
           
        }

        private void btnIngresoSalida_Click(object sender, EventArgs e)
        {
            var frm = new Listagastosingresos();
            frm.ShowDialog();
        }

        private void btncerrarcaja_Click(object sender, EventArgs e)
        {
            var frm = new CierreCaja();
            frm.ShowDialog();
        }

        private void btncerrartodo_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btncodigosQR_Click(object sender, EventArgs e)
        {
            var frm = new GenerarQr();
            frm.ShowDialog();
        }

        private void btnvolver_Click(object sender, EventArgs e)
        {
            Terminar_cambio_de_mesa();
        }
        private void Mostrartipocaja()
        {

            var funcion = new Dcaja();
            var dt = new DataTable();
            funcion.mostrarCajaSerialTable(ref dt);
            tipocaja = dt.Rows[0][1].ToString();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            var funcionImpr = new Dventas();
            funcionImpr.MostrarsolicitudImpr(ref idventa);
            if (idventa > 0)
            {
                timerImprimir.Stop();
                if (tipocaja == "PRINCIPAL")
                {
                    var funciondv = new Ddetalleventas();
                    var parametrosdv = new Ldetalleventas();
                    var dtdv = new DataTable();
                    parametrosdv.idventa = idventa;
                    funciondv.rptTicketCocina(ref dtdv, parametrosdv);
                    if (Tiponotas == "General")
                    {
                        var rpt = new Rpedidos();
                        rpt.DataSource = dtdv;
                        rpt.Tabladetalle.DataSource = dtdv;
                        rptComunicador.Report = rpt;
                    }
                    else
                    {
                        var rpt = new RpedidonotaInd();
                        rpt.DataSource = dtdv;
                        rpt.Tabladetalle.DataSource = dtdv;
                        rptComunicador.Report = rpt;
                    }
                    rptComunicador.RefreshReport();
                    var funcionImpresoras = new Dimpresoras();
                    funcionImpresoras.ImprimirVarios(rptComunicador.ReportSource, "pedidos");
                    EliminarSolImpri();
                    dibujarMESAS();
                    idventa = 0;
                    timerImprimir.Start();
                }
                else
                {

                    EliminarSolImpri();
                    dibujarMESAS();
                    idventa = 0;
                    timerImprimir.Start();
                }
            }
        }

        private void timerEsc_Tick(object sender, EventArgs e)
        {
            mostrarSolicitudes();
        }
        private void mostrarSolicitudes()
        {
            var funcion = new Dventas();
            funcion.mostrarSolicitudesEsc(ref contadorSoliEsc);
            if (contadorSoliEsc > 0)
            {
                dibujarMESAS();
                EliminarEsc();
            }
        }
        private void EliminarEsc()
        {
            var funcion = new Dventas();
            funcion.eliminarSolicitudEsc();
        }

        private void btnSunat_Click(object sender, EventArgs e)
        {
            var ctl = new Smenusunat();
            panelSunat.Controls.Add(ctl);
            ctl.Dock = DockStyle.Fill;
            panelSunat.Dock = DockStyle.Fill;
            panelSunat.Visible = true;
            PanelBienvenida.Visible = false;
            PanelHerramientas.Visible = false;
            PanelUNIONMesas.Visible = false;
            PanelMesas.Visible = false;
        }
    }
}
