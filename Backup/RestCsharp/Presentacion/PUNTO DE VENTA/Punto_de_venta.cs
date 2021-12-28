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
using System.Xml;
using RestCsharp.Presentacion.Reportes;
using RestCsharp.Presentacion.Login;
using Sunat.Logica;
using RestCsharp.Sunat;
using RestCsharp.Presentacion.Ventas;

namespace RestCsharp.Presentacion.PUNTO_DE_VENTA
{

    public partial class Punto_de_venta : UserControl
    {
        public delegate void ButtonClick(object sender, EventArgs e);
        public event ButtonClick SalirClick;
        #region eve
        private void EventoDelegado()
        {
            btnsalir.Click += new EventHandler((sender, args) =>
                       {
                           SalirClick?.Invoke(this, null);
                       });
        }

        #endregion
        public Punto_de_venta()
        {
            InitializeComponent();
            EventoDelegado();
        }
        int paginainicio = 1;
        int paginaMaxima = 10;
        public static int id_grupo;
        int cantidad_grupos;
        private Button PaginadorSiguiente = new Button();
        private Button PaginadorAtras = new Button();
        public static Punto_de_venta _Puerta;
        public static int Idmesa;
        public static string estado;
        public static int cantidadPersonas;
        public static string DondeConsumir;
        public static string ventagenerada;
        public static int idventa;
        int idventaSolicitud = 0;
        int idventadividida;
        public static int idproducto;
        public static decimal precioventa;
        decimal Preciocompra;
        public static string Mesa;
        int paginainicioPro = 1;
        int paginaMaximaPro = 15;
        int cantidad_productos;
        public static decimal Acumulado = 0;
        public static decimal totalpagar;
        public static decimal totalpagarDIV;
        public static decimal vuelto;
        int indexx;
        int contadordetalleventa;
        public static string nombrellevar;
        int Iddetalle_venta;
        int contadorDventa = 0;
        int idcliente = 0;
        //Variables para impuestos
        string TipoImpuesto;
        decimal Porcentajeimpu;
        string Trabajasconimp;
        decimal subtotal;
        decimal impuestocalculado;
        int contadorventas;
        int incrementoVentas;
        int contadorVentaslimpio;
        DataTable dtidVentasmesas;
        string Comprobante;
        decimal Tarjeta = 0;
        string Tipopago = "EFECTIVO";
        decimal efectivocalculado = 0;
        decimal restante = 0;
        decimal efectivo = 0;
        Panel panelContCobro;
        string Ip;
        string Tiponotas;
        bool permisocobrar;
        string referenciaTarjeta = "-";
        string Tema;
        string tipocaja;
        int contadorSoliEsc = 0;
        int idcomprobante;
        string Envioinmediato;
        decimal total;
        string Totalletras;
        string conexionsunat;
        string Tipocliente;
        private void Punto_de_venta_Load(object sender, EventArgs e)
        {
            ValidarTema();
            ValidarPermisos();
            ObtenerImpuesto();
            ObtenerIpLocal();
            Crearpanelcobro();
            eliminarVentaIncompleta();
            ObtenerMesa();
            dibujarGrupos();
            contar_grupos();
            contarVentas();
            ValidarEstadoMesa();
            MostrarIdventaMesa();
            eliminarDvincompletos();
            ValidarDondeConsumir();

            MostrarDetalleVenta();
            //  mostrarClienteEstandar();
            ObtenerComprobanteDefecto();
            obtener_ultima_venta();
            Mostrartipocaja();
        }
        private void ObtenerComprobanteDefecto()
        {
            //var funcion = new Dserealizacion();
            //funcion.mostrarCompDefecto(ref comprobante);
            var funcion = new Dserealizacion();
            var dt = new DataTable();
            funcion.mostrarCompDefecto(ref dt);
            Comprobante = dt.Rows[0][0].ToString();
            lblComprobante.Text = Comprobante;
            idcomprobante = Convert.ToInt32(dt.Rows[0][1]);
            Envioinmediato = dt.Rows[0][2].ToString();
            dibujarComprobantes();
            validarPedidodeCliente();
        }
        private void dibujarComprobantes()
        {
            Panelcomprobante.Controls.Clear();
            try
            {
                var funcion = new Dserealizacion();
                var dt = new DataTable();
                funcion.mostrarSerializacion(ref dt);

                foreach (DataRow rdr in dt.Rows)
                {
                    Button b = new Button();
                    b.Text = rdr["tipodoc"].ToString();
                    b.Name = rdr["Id_serializacion"].ToString();
                    b.Tag = rdr["Envioinmediato"].ToString();
                    b.Size = new System.Drawing.Size(150, 60);
                    b.BackColor = Color.FromArgb(70, 70, 71);
                    b.Font = new Font("Segoe UI", 12);
                    b.FlatStyle = FlatStyle.Flat;
                    b.ForeColor = Color.WhiteSmoke;
                    Panelcomprobante.Controls.Add(b);
                    if (b.Text == Comprobante)
                    {
                        b.Visible = false;
                    }
                    b.Click += miEvento;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        private void miEvento(object sender, EventArgs e)
        {
            idcomprobante = Convert.ToInt32(((Button)sender).Name);
            Envioinmediato = Convert.ToString(((Button)sender).Tag);
            Comprobante = ((Button)sender).Text;
            lblComprobante.Text = Comprobante;
            dibujarComprobantes();
            identificarTipopago();
            validarPedidodeCliente();

        }
        private void validarPedidodeCliente()
        {


            if (Comprobante == "FACTURA" && Tipopago == "CREDITO")
            {
                panelClienteFactura.Visible = false;
            }
            if (Comprobante == "FACTURA" && Tipopago == "EFECTIVO")
            {
                panelClienteFactura.Visible = true;
                lblCliente.Text = "Cliente: (Obligatorio)";
                lblCliente.ForeColor = Color.Crimson;

            }
            else if (Comprobante != "FACTURA" && Tipopago == "EFECTIVO")
            {
                panelClienteFactura.Visible = true;
                lblCliente.Text = "Cliente: (Opcional)";
                lblCliente.ForeColor = Color.DimGray;

            }

            if (Comprobante == "FACTURA")
            {
                panelClienteFactura.Visible = true;
                lblCliente.Text = "Cliente: (Obligatorio)";
                lblCliente.ForeColor = Color.Crimson;

            }
            else if (Comprobante != "FACTURA" && Tipopago == "TARJETA")
            {
                panelClienteFactura.Visible = true;
                lblCliente.Text = "Cliente: (Opcional)";
                lblCliente.ForeColor = Color.DimGray;
            }
            else if (Comprobante != "FACTURA" && Tipopago != "CREDITO")
            {
                panelClienteFactura.Visible = true;
                lblCliente.Text = "Cliente: (Opcional)";
                lblCliente.ForeColor = Color.DimGray;
            }


        }
        private void identificarTipopago()
        {
            int indicadorEfectivo = 4;
            int indicadorCredito = 2;
            int indicadorTarjeta = 3;

            // validacion para evitar valores vacios
            if (txtefectivo.Text == "")
            {
                txtefectivo.Text = "0";
            }
            if (txttarjeta.Text == "")
            {
                txttarjeta.Text = "0";
            }
            //if (txtcredito.Text == "")
            //{
            //    txtcredito.Text = "0";
            //}
            //validacion de .
            if (txtefectivo.Text == ".")
            {
                txtefectivo.Text = "0";
            }
            if (txttarjeta.Text == ".")
            {
                txttarjeta.Text = "0";
            }
            //if (txtcredito.Text == ".")
            //{
            //    txtcredito.Text = "0";
            //}
            //validacion de 0
            if (txtefectivo.Text == "0")
            {
                indicadorEfectivo = 0;
            }
            if (txttarjeta.Text == "0")
            {
                indicadorTarjeta = 0;
            }
            //if (txtcredito.Text == "0")
            //{
            //    indicadorCredito = 0;
            //}
            //calculo de indicador
            int calculo_identificacion = indicadorCredito + indicadorEfectivo + indicadorTarjeta;
            //consulta al identificador
            if (calculo_identificacion == 4)
            {
                Tipopago = "EFECTIVO";
            }
            if (calculo_identificacion == 2)
            {
                Tipopago = "CREDITO";
            }
            if (calculo_identificacion == 3)
            {
                Tipopago = "TARJETA";
            }
            if (calculo_identificacion > 4)
            {
                Tipopago = "MIXTO";
            }

        }
        private void Mostrartipocaja()
        {

            var funcion = new Dcaja();
            var dt = new DataTable();
            funcion.mostrarCajaSerialTable(ref dt);
            tipocaja = dt.Rows[0][1].ToString();
        }
        private void ValidarTema()
        {
            var funcion = new Dcaja();
            var dt = new DataTable();
            funcion.mostrarCajaSerialTable(ref dt);
            Tema = dt.Rows[0][2].ToString();
            if (Tema == "Claro")
            {
                TemaClaro();
                IndicadorTema.Checked = false;

            }
            else
            {
                TemaOscuro();
                IndicadorTema.Checked = true;
            }
        }
        private void ValidarPermisos()
        {
            var dt = new DataTable();
            var funcion = new Dpermisos();
            funcion.mostrar_Permisos(ref dt);
            foreach (DataRow data in dt.Rows)
            {
                string modulo = data["Modulo"].ToString();
                if (modulo == "Cobrar")
                {
                    btncobrar.Visible = true;
                    permisocobrar = true;
                    foreach (Control button in Panelpagorapido.Controls)
                    {
                        if (button is Button)
                        {
                            button.Visible = true;
                        }
                    }
                    break;
                }
                else
                {
                    permisocobrar = false;
                    btncobrar.Visible = false;
                }
            }
        }
        private void ObtenerIpLocal()
        {

            this.Text = Bases.ObtenerIp(ref Ip);
        }
        public void obtener_ultima_venta()
        {
            var funcion = new Dventas();
            var dt = new DataTable();
            funcion.mostrarultimaventa(ref dt);
            try
            {
                lblUltimoTotal.Text = dt.Rows[0][0].ToString();
                lblUltimoImpuesto.Text = dt.Rows[0][1].ToString();
                lblUltimoVuelto.Text = dt.Rows[0][2].ToString();
                lblUltimoSubtotal.Text = dt.Rows[0][3].ToString();
                lblUltimoPagoCon.Text = dt.Rows[0][4].ToString();

                lblUltimoTotal.Text = decimal.Parse(lblUltimoTotal.Text).ToString("##0.00");
                lblUltimoImpuesto.Text = decimal.Parse(lblUltimoImpuesto.Text).ToString("##0.00");
                lblUltimoVuelto.Text = decimal.Parse(lblUltimoVuelto.Text).ToString("##0.00");
                lblUltimoSubtotal.Text = decimal.Parse(lblUltimoSubtotal.Text).ToString("##0.00");
                lblUltimoPagoCon.Text = decimal.Parse(lblUltimoPagoCon.Text).ToString("##0.00");


            }
            catch (Exception)
            {

            }


        }
        private void Crearpanelcobro()
        {
            panelContCobro = new Panel();
            panelContCobro.Size = new Size(883, 402);
            panelContCobro.Location = new Point((Width - panelContCobro.Width) / 2, (Height - panelContCobro.Height) / 2);
            panelContCobro.Controls.Add(panelCobro);
            panelContCobro.Visible = false;
            panelCobro.Visible = false;
            panelCobro.Dock = DockStyle.Fill;
            this.Controls.Add(panelContCobro);
            panelContCobro.BringToFront();
        }


        private void contarVentas()
        {
            Dventas funcion = new Dventas();
            funcion.contarVentas(ref contadorventas, Idmesa);
            contadorVentaslimpio = contadorventas;
            contadorventas -= 1;
            incrementoVentas = contadorventas;
            if (contadorventas == 0)
            {
                btncuentaadelante.Visible = false;
                btncuentaatras.Visible = false;
            }


        }

        private void ObtenerImpuesto()
        {
            var funcion = new Dempresa();
            var dt = new DataTable();
            funcion.mostrar_empresa(ref dt);

            Trabajasconimp = dt.Rows[0][5].ToString();
            conexionsunat = dt.Rows[0][18].ToString();
            if (Trabajasconimp == "NO")
            {
                Porcentajeimpu = 0;
            }
            else
            {
                Porcentajeimpu = Convert.ToDecimal(dt.Rows[0][3]);
            }

            TipoImpuesto = (dt.Rows[0][2]).ToString();
            lblImpuesto1.Text = dt.Rows[0][25].ToString();
            lblImpuesto2.Text = dt.Rows[0][25].ToString();
            lbltipoimpuesto.Text = dt.Rows[0][25].ToString();
            Tiponotas = dt.Rows[0][6].ToString();
        }

        private void eliminarDvincompletos()
        {
            var funcion = new Ddetalleventas();
            var parametros = new Ldetalleventas();
            parametros.idventa = idventa;
            funcion.eliminarDvincompletos(parametros);
        }
        private void ValidarDondeConsumir()
        {
            if (DondeConsumir == "EN LOCAL")
            {
                nombrellevar = "-";
                if (idventa > 0)
                {
                    if (permisocobrar == true)
                    {
                        Panelpagorapido.Visible = true;
                        btncobrar.Visible = true;
                    }

                }
                else
                {
                    Panelpagorapido.Visible = false;
                    btncobrar.Visible = false;
                }

            }
            else
            {
                lblMesa.Font = new Font("Microsoft Sans Serif", 13, FontStyle.Bold);
                lblMesa.Size = new Size(148, 45);

                if (permisocobrar == true)
                {
                    Panelpagorapido.Visible = true;
                    btncobrar.Visible = true;
                }
                ventagenerada = "VENTA NUEVA";
                btnEnviarpedido.Visible = false;
                ObtenerIdmesaLlevar();
                var frm = new NombreLlevar();
                frm.FormClosing += Frm_FormClosing; ;
                frm.ShowDialog();
            }
        }

        private void Frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            lblMesa.Text = nombrellevar;
        }



        private void ObtenerIdmesaLlevar()
        {
            Dmesas funcion = new Dmesas();
            funcion.ObtenerIdmesaLlevar(ref Idmesa);
        }
        private void MostrarDetalleVenta()
        {
            detalleventa.Controls.Clear();
            try
            {
                DataTable dt = new DataTable();
                Ddetalleventas funcion = new Ddetalleventas();
                Ldetalleventas parametros = new Ldetalleventas();
                parametros.idventa = idventa;
                funcion.mostrarDetalleVenta(ref dt, Idmesa, parametros);
                contadorDventa = dt.Rows.Count;
                foreach (DataRow data in dt.Rows)
                {
                    string estado = data["Estado"].ToString();
                    if (estado == "EN ESPERA" && DondeConsumir == "EN LOCAL")
                    {
                        btnEnviarpedido.Visible = true;
                        break;
                    }
                }
                foreach (DataRow rdr in dt.Rows)
                {
                    Label lblCant = new Label();
                    Label lblproducto = new Label();
                    Label lblimporte = new Label();
                    Button btnmas = new Button();
                    Button btnmenos = new Button();
                    Panel p1 = new Panel();
                    Button btnquitar = new Button();
                    //**************
                    Panel Pcontenedor = new Panel();
                    Label lblEstadococina = new Label();
                    PictureBox Iconococina = new PictureBox();
                    Panel panelEstado = new Panel();
                    //*** Para notas por pedido
                    Label lblNotaproducto = new Label();
                    Panel panelNota = new Panel();
                    Button btneliminarnota = new Button();

                    //*****************
                    btnquitar.Text = "Borrar";
                    btnquitar.Name = rdr["iddetalle_venta"].ToString();
                    btnquitar.Size = new Size(55, 51);
                    btnquitar.Font = new Font("Microsoft Sans Serif", 9);
                    btnquitar.BackColor = Color.Transparent;
                    btnquitar.ForeColor = Color.White;
                    btnquitar.Dock = DockStyle.Left;
                    btnquitar.TextAlign = ContentAlignment.MiddleCenter;
                    btnquitar.Cursor = Cursors.Hand;
                    btnquitar.BackgroundImage = Properties.Resources.Rojo;
                    btnquitar.BackgroundImageLayout = ImageLayout.Stretch;
                    btnquitar.FlatStyle = FlatStyle.Flat;
                    btnquitar.FlatAppearance.MouseDownBackColor = Color.Transparent;
                    btnquitar.FlatAppearance.MouseOverBackColor = Color.Transparent;
                    btnquitar.FlatAppearance.BorderSize = 0;
                    //*****************
                    lblCant.Text = rdr["Cant"].ToString();
                    lblCant.Name = rdr["iddetalle_venta"].ToString();
                    lblCant.Tag = rdr["Importe"].ToString();
                    lblCant.Size = new Size(127, 25);
                    lblCant.Font = new Font("Microsoft Sans Serif", 11);
                    lblCant.BackColor = Color.Transparent;

                    lblCant.Dock = DockStyle.Top;
                    lblCant.TextAlign = ContentAlignment.MiddleCenter;
                    lblCant.Cursor = Cursors.Hand;
                    //*****************
                    lblproducto.Text = rdr["Producto"].ToString();
                    lblproducto.Name = rdr["iddetalle_venta"].ToString();
                    lblproducto.Tag = rdr["Importe"].ToString();
                    lblproducto.Size = new Size(158, 51);
                    lblproducto.Font = new Font("Microsoft Sans Serif", 11);
                    lblproducto.BackColor = Color.Transparent;
                    lblproducto.Dock = DockStyle.Fill;
                    lblproducto.TextAlign = ContentAlignment.MiddleCenter;
                    lblproducto.Cursor = Cursors.Hand;
                    //*****************
                    lblimporte.Text = rdr["Importe"].ToString();
                    lblimporte.Name = rdr["iddetalle_venta"].ToString();
                    lblimporte.Tag = rdr["Importe"].ToString();
                    lblimporte.Size = new Size(127, 51);
                    lblimporte.Font = new Font("Microsoft Sans Serif", 11);
                    lblimporte.BackColor = Color.Transparent;
                    lblimporte.Dock = DockStyle.Right;
                    lblimporte.TextAlign = ContentAlignment.MiddleCenter;
                    lblimporte.Cursor = Cursors.Hand;
                    //*********************
                    panelEstado.Size = new Size(127, 25);
                    panelEstado.BorderStyle = BorderStyle.None;
                    panelEstado.BackColor = Color.Transparent;
                    panelEstado.Name = rdr["iddetalle_venta"].ToString();
                    panelEstado.Dock = DockStyle.Left;
                    //**************
                    p1.Size = new Size(467, 51);
                    p1.BorderStyle = BorderStyle.None;
                    p1.BackColor = Color.Transparent;
                    p1.Name = rdr["iddetalle_venta"].ToString();
                    p1.Tag = rdr["Importe"].ToString();
                    p1.Dock = DockStyle.Top;

                    Pcontenedor.Size = new Size(467, 52);
                    Pcontenedor.BorderStyle = BorderStyle.None;
                    Pcontenedor.BackColor = Color.Transparent;
                    Pcontenedor.Name = rdr["iddetalle_venta"].ToString();
                    Pcontenedor.Tag = rdr["Importe"].ToString();
                    //*****************
                    lblEstadococina.Text = rdr["Estado"].ToString();
                    lblEstadococina.Size = new Size(25, 21);
                    lblEstadococina.Font = new Font("Microsoft Sans Serif", 7, FontStyle.Bold);
                    lblEstadococina.BackColor = Color.Transparent;
                    lblEstadococina.Dock = DockStyle.Fill;
                    lblEstadococina.TextAlign = ContentAlignment.MiddleCenter;
                    lblEstadococina.Cursor = Cursors.Hand;
                    lblEstadococina.Name = rdr["iddetalle_venta"].ToString();

                    //*****************
                    Iconococina.Image = Properties.Resources.cocin;
                    Iconococina.SizeMode = PictureBoxSizeMode.Zoom;
                    Iconococina.Size = new Size(50, 25);
                    Iconococina.Dock = DockStyle.Left;
                    Iconococina.Name = rdr["iddetalle_venta"].ToString();



                    //***
                    //*** Notas por producto
                    if (rdr["NotaProducto"].ToString() != "-" && Tiponotas == "Porpedido")
                    {
                        Pcontenedor.Size = new Size(467, 102);
                        lblNotaproducto.Text = "Nota: " + rdr["NotaProducto"].ToString();
                        lblNotaproducto.Size = new Size(158, 51);
                        lblNotaproducto.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold);
                        lblNotaproducto.BackColor = Color.Transparent;
                        lblNotaproducto.ForeColor = Color.FromArgb(255, 222, 70);
                        lblNotaproducto.Dock = DockStyle.Right;
                        lblNotaproducto.TextAlign = ContentAlignment.MiddleRight;
                        lblNotaproducto.Cursor = Cursors.Hand;
                        panelNota.Size = new Size(158, 51);
                        panelNota.Dock = DockStyle.Bottom;
                        btneliminarnota.Text = "Eliminar nota";
                        btneliminarnota.Name = rdr["iddetalle_venta"].ToString();
                        btneliminarnota.Dock = DockStyle.Fill;
                        btneliminarnota.ForeColor = Color.FromArgb(252, 125, 118);
                        btneliminarnota.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold);
                        btneliminarnota.BackColor = Color.Transparent;
                        btneliminarnota.FlatStyle = FlatStyle.Flat;
                        btneliminarnota.FlatAppearance.MouseDownBackColor = Color.Transparent;
                        btneliminarnota.FlatAppearance.MouseOverBackColor = Color.Transparent;
                        panelNota.Controls.Add(btneliminarnota);
                        panelNota.Controls.Add(lblNotaproducto);
                        Pcontenedor.Controls.Add(panelNota);
                    }
                    //
                    if (Tema == "Claro")
                    {
                        lblproducto.ForeColor = Color.FromArgb(29, 29, 29);
                        lblCant.ForeColor = Color.FromArgb(29, 29, 29);
                        lblimporte.ForeColor = Color.FromArgb(85, 74, 28);
                        lblEstadococina.ForeColor = Color.FromArgb(0, 166, 63);
                        //Colorear fila seleccionada
                        string ruta = richTextBox1.Text;
                        if (ruta.Contains("'" + rdr["iddetalle_venta"].ToString() + "'"))
                        {
                            p1.BackColor = Color.FromArgb(255, 222, 84);
                        }
                    }
                    else
                    {
                        lblproducto.ForeColor = Color.White;
                        lblCant.ForeColor = Color.White;
                        lblimporte.ForeColor = Color.FromArgb(255, 222, 84);
                        lblEstadococina.ForeColor = Color.FromArgb(165, 220, 105);
                        //Colorear fila seleccionada
                        string ruta = richTextBox1.Text;
                        if (ruta.Contains("'" + rdr["iddetalle_venta"].ToString() + "'"))
                        {
                            p1.BackColor = Color.FromArgb(45, 45, 45);
                        }
                    }
                    panelEstado.Controls.Add(lblCant);
                    panelEstado.Controls.Add(Iconococina);
                    panelEstado.Controls.Add(lblEstadococina);
                    p1.Controls.Add(btnquitar);
                    p1.Controls.Add(lblproducto);
                    p1.Controls.Add(lblimporte);
                    p1.Controls.Add(panelEstado);
                    Pcontenedor.Controls.Add(p1);


                    //*************

                    btnquitar.SendToBack();
                    lblproducto.BringToFront();
                    lblCant.BringToFront();
                    Iconococina.BringToFront();
                    lblEstadococina.BringToFront();
                    //***************

                    detalleventa.Controls.Add(Pcontenedor);
                    lblproducto.Click += Lblproducto_Click;
                    lblCant.Click += Lblproducto_Click;
                    lblimporte.Click += Lblproducto_Click;
                    btnquitar.Click += Btnquitar_Click;
                    btneliminarnota.Click += Btneliminarnota_Click;
                }
                sumar1(ref dt);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);

            }
        }

        private void Btneliminarnota_Click(object sender, EventArgs e)
        {
            Iddetalle_venta = Convert.ToInt32(((Button)sender).Name);
            EliminarNotaproduc();
        }
        private void EliminarNotaproduc()
        {
            var funcion = new Ddetalleventas();
            var parametros = new Ldetalleventas();
            parametros.iddetalle_venta = Iddetalle_venta;
            parametros.Nota = "-";
            funcion.editarNotaproducto(parametros);
            MostrarDetalleVenta();
        }
        private void Btnquitar_Click(object sender, EventArgs e)
        {
            Iddetalle_venta = Convert.ToInt32(((Button)sender).Name);
            EliminarDetalleVenta();
        }
        private void EliminarDetalleVenta()
        {
            var funcion = new Ddetalleventas();
            var parametros = new Ldetalleventas();
            parametros.iddetalle_venta = Iddetalle_venta;
            funcion.eliminarDetalleventa(parametros);
            MostrarDetalleVenta();
            if (contadorDventa == 0)
            {
                eliminarVenta();
                EditarEstadoMesaLibre();
                VolverVisorMesas();
            }

        }

        private void EditarEstadoMesaLibre()
        {
            if (contadorVentaslimpio == 1)
            {
                if (DondeConsumir == "EN LOCAL")
                {
                    var funcion = new Dmesas();
                    var parametros = new Lmesas();
                    parametros.Id_mesa = Idmesa;
                    funcion.EditarEstadoMesaLibre(parametros);
                }
            }


        }
        private void eliminarVenta()
        {
            var funcion = new Dventas();
            var parametros = new Lventas();
            parametros.idventa = idventa;
            funcion.eliminarVenta(parametros);
        }
        private void sumar1(ref DataTable dt)
        {
            decimal Total = 0;
            foreach (DataRow fila in dt.Rows)
            {
                Total += Convert.ToDecimal(fila["Importe"]);
            }
            totalpagar = Total;
            txtTotal1.Text = Total.ToString();
            //
            if (Trabajasconimp == "SI")
            {

                impuestocalculado = (Porcentajeimpu * Total) / 100;
                subtotal = Total - impuestocalculado;
                lblSubtotal1.Text = subtotal.ToString();
                lblImpuestocal1.Text = impuestocalculado.ToString();
            }
            else
            {
                impuestocalculado = 0;
                subtotal = Total;
                lblSubtotal1.Text = subtotal.ToString();
                lblImpuestocal1.Text = impuestocalculado.ToString();
            }
        }
        private void Lblproducto_Click(object sender, EventArgs e)
        {
            Contadordetalleventa();
            Iddetalle_venta = Convert.ToInt32(((Label)sender).Name);
            if (contadordetalleventa > 1 && DondeConsumir == "EN LOCAL")
            {
                string idDetalleventa = ((Label)sender).Name.ToString();
                string importe = ((Label)sender).Tag.ToString();
                string ruta = richTextBox1.Text;
                if (ruta.Contains("'" + idDetalleventa + "'"))
                {
                    richTextBox1.Text = richTextBox1.Text.Replace("'" + idDetalleventa + "'", "");
                    foreach (DataGridViewRow row in datalistadoPedidos.Rows)
                    {
                        int id = Convert.ToInt32(row.Cells["Iddetalleventa"].Value);
                        if (id == Convert.ToInt32(idDetalleventa))
                        {
                            indexx = row.Index;
                            datalistadoPedidos.Rows.RemoveAt(indexx);
                            break;
                        }
                    }
                }
                else
                {
                    datalistadoPedidos.Rows.Add(idDetalleventa, importe);
                    richTextBox1.Text += "'" + idDetalleventa + "'";
                }
                MostrarDetalleVenta();
                if (datalistadoPedidos.RowCount == 0)
                {
                    Paneltotal2.Visible = false;
                    panelTotal1.Dock = DockStyle.Fill;
                    panelTotal1.Visible = true;
                }
                else
                {
                    Paneltotal2.Visible = true;
                    Paneltotal2.Dock = DockStyle.Fill;
                    panelTotal1.Visible = false;
                    sumar2();
                }

            }
        }
        private void sumar2()
        {
            decimal Total = 0;
            foreach (DataGridViewRow fila in datalistadoPedidos.Rows)
            {
                Total += Convert.ToDecimal(fila.Cells["Importe"].Value);

            }
            totalpagarDIV = Total;
            txtTotal2.Text = Total.ToString();
            //
            if (Trabajasconimp == "SI")
            {

                impuestocalculado = (Porcentajeimpu * Total) / 100;
                subtotal = Total - impuestocalculado;
                lblSubtotal2.Text = subtotal.ToString();
                lblImpuestocal2.Text = impuestocalculado.ToString();
            }
            else
            {
                impuestocalculado = 0;
                subtotal = Total;
                lblSubtotal2.Text = subtotal.ToString();
                lblImpuestocal2.Text = impuestocalculado.ToString();
            }
        }
        private void eliminarVentaIncompleta()
        {
            Dventas funcion = new Dventas();
            funcion.eliminarVentaIncompleta();
        }
        private void ObtenerMesa()
        {
            lblMesa.Text = Mesa;
        }
        private void ValidarEstadoMesa()
        {

            if (estado == "LIBRE" && DondeConsumir == "EN LOCAL")
            {
                CantPersonas frm = new CantPersonas();
                frm.OnButtonClick += UsrCtrl_OnButtonClick;
                // frm.FormClosed += Frm_FormClosed1;
                // frm.ShowDialog();
                frm.Dock = DockStyle.Fill;
                this.Controls.Add(frm);
                frm.BringToFront();
            }
        }
        private void UsrCtrl_OnButtonClick(object sender, EventArgs e)
        {
            lblcantidadPersonas.Text = cantidadPersonas.ToString();
        }


        public static Punto_de_venta Puerta
        {
            get
            {
                if (_Puerta == null || _Puerta.IsDisposed)
                    _Puerta = new Punto_de_venta();
                return _Puerta;
            }
        }

        public void insertarVenta()
        {

            if (ventagenerada == "VENTA NUEVA")
            {

                Lventas parametros = new Lventas();
                Dventas funcion = new Dventas();
                parametros.Id_mesa = Idmesa;
                parametros.Numero_personas = cantidadPersonas;
                parametros.NombreLlevar = nombrellevar;
                if (funcion.Insertar_ventas(parametros) == true)
                {
                    MostrarIdventaMesa();
                }
            }
            if (ventagenerada == "VENTA GENERADA")
            {
                insertarDetalleventa();
            }



        }
        private void insertarDetalleventa()
        {
            Ddetalleventas funcion = new Ddetalleventas();
            Ldetalleventas parametros = new Ldetalleventas();
            parametros.idventa = idventa;
            parametros.Id_producto = idproducto;
            parametros.cantidad = 1;
            parametros.Estado = "EN ESPERA";
            parametros.Estado_de_pago = "DEBE";
            parametros.Donde_se_consumira = DondeConsumir;
            funcion.insertarDetalle_venta(parametros);
            MostrarDetalleVenta();
        }

        public void MostrarIdventaMesa()
        {
            Dventas funcion = new Dventas();
            Lventas parametros = new Lventas();
            parametros.Id_mesa = Idmesa;
            funcion.mostrarIdventaMesa(ref idventa, parametros);
            Validarnotas();

            if (idventa > 0)

            {
                ObtenerCantPersonas();
                lblcuenta.Text = idventa.ToString();
                mostrarNotasVentas();
                ventagenerada = "VENTA GENERADA";
                btnEnviarpedido.Visible = false;
                if (DondeConsumir == "EN LOCAL")
                {
                    btnPrecuenta.Visible = true;

                }
            }
            else
            {
                ventagenerada = "VENTA NUEVA";
                btnEnviarpedido.Visible = true;
                btnPrecuenta.Visible = false;

            }

        }
        private void ObtenerCantPersonas()
        {
            var funcion = new Dventas();
            var parametros = new Lventas();
            parametros.idventa = idventa;
            funcion.MostrarCantPersonas(ref cantidadPersonas, parametros);
            lblcantidadPersonas.Text = cantidadPersonas.ToString();
        }
        private void Validarnotas()
        {
            if (Tiponotas == "General")
            {
                panelNotas.Visible = true;
            }
            else
            {
                panelNotas.Visible = false;
            }
        }
        private void mostrarNotasVentas()
        {
            var funcion = new Dventas();
            var parametros = new Lventas();
            parametros.idventa = idventa;
            string nota = "";
            funcion.mostrarNotasVentas(ref nota, parametros);
            lblNota.Text = nota;
        }
        void contar_grupos()
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlCommand com = new SqlCommand("select count(Idline) from Grupo_de_Productos", CONEXIONMAESTRA.conectar);
                cantidad_grupos = Convert.ToInt32(com.ExecuteScalar());
                CONEXIONMAESTRA.cerrar();
            }
            catch (Exception ex)
            {
                cantidad_grupos = 0;
            }

        }
        public void dibujarGrupos()
        {
            Panel_grupos.Controls.Clear();
            try
            {
                CONEXIONMAESTRA.abrir();
                string query = "Paginar_grupos";
                SqlCommand cmd = new SqlCommand(query, CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Desde", paginainicio);
                cmd.Parameters.AddWithValue("@Hasta", paginaMaxima);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Label b = new Label();
                    Panel p1 = new Panel();
                    PictureBox I1 = new PictureBox();
                    b.Text = rdr["Grupo"].ToString();
                    b.Name = rdr["Idline"].ToString();
                    b.Size = new System.Drawing.Size(119, 25);
                    b.Font = new System.Drawing.Font("Microsoft Sans Serif", 11);
                    b.BackColor = Color.Transparent;
                    b.ForeColor = Color.White;
                    b.Dock = DockStyle.Fill;
                    b.TextAlign = ContentAlignment.MiddleCenter;
                    b.Cursor = Cursors.Hand;

                    p1.Size = new System.Drawing.Size(110, 75);
                    p1.BorderStyle = System.Windows.Forms.BorderStyle.None;
                    p1.BackColor = Color.Transparent;
                    p1.Name = rdr["Idline"].ToString();
                    p1.BackgroundImage = Properties.Resources.naranja;
                    p1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;

                    I1.Size = new System.Drawing.Size(140, 50);
                    I1.Dock = DockStyle.Top;
                    I1.BackgroundImage = null;
                    byte[] bi = (byte[])rdr["Icono"];
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(bi);
                    I1.Image = Image.FromStream(ms);
                    I1.SizeMode = PictureBoxSizeMode.Zoom;
                    I1.Cursor = Cursors.Hand;
                    I1.Name = rdr["Idline"].ToString();
                    I1.BackColor = Color.Transparent;

                    p1.Controls.Add(b);
                    if (rdr["Estado_de_icono"].ToString() != "VACIO")
                    {
                        p1.Controls.Add(I1);
                    }
                    b.BringToFront();
                    Panel_grupos.Controls.Add(p1);
                    b.Click += new EventHandler(mieventoLabel);
                    I1.Click += new EventHandler(miEventoImagen);
                }
                CONEXIONMAESTRA.cerrar();

            }
            catch (Exception ex)
            {

            }
        }
        public void dibujarProductos()
        {
            try
            {

                flowLayoutPanel5.Controls.Clear();
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("paginar_Productos_por_grupo", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_grupo", id_grupo);
                cmd.Parameters.AddWithValue("@Desde", paginainicioPro);
                cmd.Parameters.AddWithValue("@Hasta", paginaMaximaPro);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    Label b = new Label();
                    Panel p1 = new Panel();
                    PictureBox I1 = new PictureBox();
                    Label lblprecio = new Label();
                    b.Text = rdr["Descripcion"].ToString();
                    b.Name = rdr["Id_Producto1"].ToString();
                    b.Tag = rdr["Precio_de_venta"].ToString() + '|' + rdr["Precio_de_compra"].ToString();
                    b.Font = new System.Drawing.Font("Microsoft Sans Serif", 7, FontStyle.Regular | FontStyle.Bold);
                    b.BackColor = Color.Transparent;
                    b.ForeColor = Color.White;
                    b.Dock = DockStyle.Fill;
                    b.TextAlign = ContentAlignment.MiddleCenter;
                    b.Cursor = Cursors.Hand;

                    lblprecio.Text = rdr["Moneda"].ToString() + " " + rdr["Precio_de_venta"].ToString();
                    lblprecio.Name = rdr["Id_Producto1"].ToString();
                    lblprecio.Tag = rdr["Precio_de_venta"].ToString() + '|' + rdr["Precio_de_compra"].ToString();
                    lblprecio.Size = new Size(119, 25);
                    lblprecio.Font = new Font("Microsoft Sans Serif", 9);
                    lblprecio.BackColor = Color.Transparent;
                    lblprecio.ForeColor = Color.White;
                    lblprecio.Dock = DockStyle.Bottom;
                    lblprecio.TextAlign = ContentAlignment.TopCenter;
                    lblprecio.Cursor = Cursors.Hand;

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
                    I1.Tag = rdr["Precio_de_venta"].ToString() + '|' + rdr["Precio_de_compra"].ToString();
                    I1.Name = rdr["Id_Producto1"].ToString();
                    I1.BackColor = Color.Transparent;
                    I1.Size = new Size(100, 30);

                    p1.Controls.Add(b);
                    p1.Controls.Add(lblprecio);
                    if (rdr["Estado_imagen"].ToString() != "VACIO")
                    {
                        p1.Controls.Add(I1);
                    }
                    b.BringToFront();
                    flowLayoutPanel5.Controls.Add(p1);
                    I1.Click += I1_Click;
                    b.Click += B_Click;
                    lblprecio.Click += B_Click;
                }
                CONEXIONMAESTRA.cerrar();
            }
            catch (Exception ex)
            {
                CONEXIONMAESTRA.cerrar();
                MessageBox.Show(ex.Message);

            }
        }
        public void B_Click(object sender, EventArgs e)
        {
            idproducto = Convert.ToInt32(((Label)sender).Name);
            string cadena = ((Label)sender).Tag.ToString();
            string[] separadas = cadena.Split('|');
            precioventa = Convert.ToDecimal(separadas[0]);
            Preciocompra = Convert.ToDecimal(separadas[1]);
            insertarVenta();
        }

        private void I1_Click(object sender, EventArgs e)
        {
            idproducto = Convert.ToInt32(((PictureBox)sender).Name);
            string cadena = ((PictureBox)sender).Tag.ToString();
            string[] separadas = cadena.Split('|');
            precioventa = Convert.ToDecimal(separadas[0]);
            Preciocompra = Convert.ToDecimal(separadas[1]);
            insertarVenta();
        }
        private void miEventoImagen(object sender, EventArgs e)
        {
            try
            {
                id_grupo = Convert.ToInt32(((PictureBox)sender).Name);
                paginainicioPro = 1;
                paginaMaximaPro = 15;
                Seleccionar_Deseleccionar_grupos();
                dibujarProductos();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mieventoLabel(object sender, EventArgs e)
        {
            try
            {
                id_grupo = Convert.ToInt32(((Label)sender).Name);
                paginainicioPro = 1;
                paginaMaximaPro = 15;
                Seleccionar_Deseleccionar_grupos();
                dibujarProductos();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Seleccionar_Deseleccionar_grupos()
        {
            try
            {
                foreach (System.Windows.Forms.Control panelP1 in Panel_grupos.Controls)
                {
                    if (panelP1 is System.Windows.Forms.Panel)
                    {
                        foreach (var PanelSecundario in panelP1.Controls)
                        {
                            panelP1.BackColor = Color.Transparent;
                            panelP1.BackgroundImage = Properties.Resources.naranja;
                            panelP1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                            break;

                        }
                    }
                }

                foreach (System.Windows.Forms.Control PanelP2 in Panel_grupos.Controls)
                {
                    if (PanelP2 is System.Windows.Forms.Panel)
                    {
                        if (PanelP2.Name == id_grupo.ToString())
                        {
                            PanelP2.BackColor = Color.Transparent;
                            PanelP2.BackgroundImage = Properties.Resources.azul;
                            PanelP2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;

                        }

                    }
                }


            }
            catch (Exception)
            {


            }
        }

        private void Label16_Click(object sender, EventArgs e)
        {

        }

        private void Label17_Click(object sender, EventArgs e)
        {

        }

        private void btnGrupoadelante_Click(object sender, EventArgs e)
        {
            contar_grupos();
            if (cantidad_grupos > paginaMaxima)
            {

                paginainicio += 10;
                paginaMaxima += 10;
                dibujarGrupos();
            }
        }

        private void btngrupoAtras_Click(object sender, EventArgs e)
        {
            if (paginainicio > 1)
            {

                paginainicio -= 10;
                paginaMaxima -= 10;
                dibujarGrupos();
            }
        }

        private void btnvermesas_Click(object sender, EventArgs e)
        {
            VolverVisorMesas();
        }
        private void VolverVisorMesas()
        {
            Dispose();
            // Visor_de_mesas frm = new Visor_de_mesas();
            //  frm.ShowDialog();
        }


        private void btnEnviarpedido_Click(object sender, EventArgs e)
        {
            if (idventa > 0)
            {
                Insertarsolicitud();
                EditarEstadoMesaOcupado();
                EditarEstadoVentasEspera();
                ImprimirPedido();
                EditardetalleventaAenviado();
                VolverVisorMesas();
            }
            else
            {
                MessageBox.Show("Agrege productos a la venta", "Sin Registro de Productos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void Insertarsolicitud()
        {
            var funcion = new Dventas();
            var parametros = new Lventas();
            parametros.Tiposolicitud = "ESC";
            funcion.Insertarsolicitud(parametros);
        }
        private void ImprimirPedido()
        {
            var funcion = new Ddetalleventas();
            var parametros = new Ldetalleventas();
            var dt = new DataTable();
            parametros.idventa = idventa;

            funcion.rptTicketCocina(ref dt, parametros);
            //************


            if (Tiponotas == "General")
            {
                var rpt = new Rpedidos();
                rpt.DataSource = dt;
                rpt.Tabladetalle.DataSource = dt;
                rptPedidos.Report = rpt;
            }
            else
            {
                var rpt = new RpedidonotaInd();
                rpt.DataSource = dt;
                rpt.Tabladetalle.DataSource = dt;
                rptPedidos.Report = rpt;
            }

            rptPedidos.RefreshReport();
            var funcionImpresoras = new Dimpresoras();
            funcionImpresoras.ImprimirVarios(rptPedidos.ReportSource, "pedidos");
        }
        private void EditardetalleventaAenviado()
        {
            var funcionmostrar = new Ddetalleventas();
            var parametrosmostrar = new Ldetalleventas();
            parametrosmostrar.idventa = idventa;
            var dt = new DataTable();
            funcionmostrar.mostrarDetalleVenta(ref dt, Idmesa, parametrosmostrar);
            //*********************
            var funcion = new Ddetalleventas();
            var parametros = new Ldetalleventas();

            foreach (DataRow rdr in dt.Rows)
            {
                int iddetalleventa = Convert.ToInt32(rdr["iddetalle_venta"]);
                parametros.iddetalle_venta = iddetalleventa;
                funcion.editarAenviado(parametros);

            }

        }
        private void EditarEstadoVentasEspera()
        {
            Dventas funcion = new Dventas();
            Lventas parametros = new Lventas();
            parametros.idventa = idventa;
            funcion.EditarEstadoVentasEspera(parametros);
        }
        private void EditarEstadoMesaOcupado()
        {
            Dmesas funcion = new Dmesas();
            Lmesas parametros = new Lmesas();
            parametros.Id_mesa = Idmesa;
            funcion.EditarEstadoMesaOcupado(parametros);
        }
        private void btnadelante_Click(object sender, EventArgs e)
        {
            contar_productos();
            if (cantidad_productos > paginaMaximaPro)
            {
                paginainicioPro += 15;
                paginaMaximaPro += 15;
                dibujarProductos();
            }
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

        private void btnatras_Click(object sender, EventArgs e)
        {

        }

        private void timerFechaHora_Tick(object sender, EventArgs e)
        {
            lblfecha.Text = DateTime.Now.ToShortDateString();
            lblhora.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Acumulado = totalpagar;
            txtacumulado.Text = Acumulado.ToString();
            editarventaClientStandar();
            Validartiposdeventa();
        }

        private void Bn1_Click(object sender, EventArgs e)
        {
            Acumulado += 1;
            txtacumulado.Text = Acumulado.ToString();
            editarventaClientStandar();
            Validartiposdeventa();
        }

        private void Bn5_Click(object sender, EventArgs e)
        {
            Acumulado += 5;
            txtacumulado.Text = Acumulado.ToString();
            editarventaClientStandar();
            Validartiposdeventa();
        }

        private void Bn10_Click(object sender, EventArgs e)
        {
            Acumulado += 10;
            txtacumulado.Text = Acumulado.ToString();
            editarventaClientStandar();
            Validartiposdeventa();
        }

        private void Bn20_Click(object sender, EventArgs e)
        {
            Acumulado += 20;
            txtacumulado.Text = Acumulado.ToString();
            editarventaClientStandar();
            Validartiposdeventa();
        }

        private void Bn50_Click(object sender, EventArgs e)
        {
            Acumulado += 50;
            txtacumulado.Text = Acumulado.ToString();
            editarventaClientStandar();
            Validartiposdeventa();
        }

        private void Bn100_Click(object sender, EventArgs e)
        {
            Acumulado += 100;
            txtacumulado.Text = Acumulado.ToString();
            editarventaClientStandar();
            Validartiposdeventa();
        }

        private void confirmarVenta()
        {
            ValidarClienteStandar();
            Lventas parametros = new Lventas();
            Dventas funcion = new Dventas();
            parametros.idventa = idventa;
            parametros.idclientev = idcliente;
            parametros.fecha_venta = DateTime.Now;
            parametros.Monto_total = totalpagar;
            parametros.Tipo_de_pago = Tipopago;//Pasar
            parametros.TotalIgv = impuestocalculado;
            parametros.Fecha_de_pago = DateTime.Now;
            parametros.Saldo = 0;
            parametros.Pago_con = Acumulado;//Pasar
            parametros.Referencia_tarjeta = referenciaTarjeta;
            parametros.Vuelto = vuelto;//Pasar
            parametros.Efectivo = efectivocalculado;//Pasar
            parametros.Credito = 0;
            parametros.Tarjeta = Tarjeta;//Pasar
            parametros.Idcomprobante = idcomprobante;
            funcion.Confirmar_venta(parametros);
            Acumulado = 0;
            convertirTotalaletras();
        }
        void convertirTotalaletras()
        {
            try
            {

                int numero = Convert.ToInt32(Math.Floor(Convert.ToDecimal(totalpagar)));
                var totalString = total_en_letras.Num2Text(numero);
                string[] a = totalpagar.ToString().Split('.');
                string txttotaldecimal;
                txttotaldecimal = a[1];
                Totalletras = totalString + " CON " + txttotaldecimal + "/100 ";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }

        }
        private void Validartiposdeventa()
        {

            if (datalistadoPedidos.RowCount > 0)
            {
                if (Acumulado >= totalpagarDIV)
                {
                    InsertarVentaDiv();
                }
            }
            else
            {
                Validarventanormal();
            }
        }
        private void Validarventanormal()
        {
            Contadordetalleventa();
            if (contadordetalleventa > 0)
            {
                if (Acumulado >= totalpagar || panelContCobro.Visible == true)
                {

                    calcularVuelto();
                    confirmarVenta();
                    if (conexionsunat == "SI" && Comprobante != "TICKET")
                    {
                        if (Envioinmediato == "SI")
                        {
                            EmitirFacturaContado();
                        }

                    }
                    if (DondeConsumir != "EN LOCAL")
                    {
                        editarventaClientStandar();
                        ImprimirPedido();
                        EditardetalleventaAenviado();
                    }
                    EditarEstadoDvNormal();
                    ImprimirComprobante();
                    EditarEstadoMesaLibre();
                    VolverVisorMesas();
                }
            }
        }

        private void editarventaClientStandar()
        {
            var funcion = new Dventas();
            var parametros = new Lventas();
            parametros.idventa = idventa;
            funcion.editarventaClientStandar(parametros);
        }
        private void EmitirFacturaContado()
        {
            var parametrosVentas = new Lventas();
            var funcionVentas = new Dventas();
            var dtventas = new DataTable();
            parametrosVentas.idventa = idventa;
            funcionVentas.mostrarVentasId(ref dtventas, parametrosVentas);

            foreach (DataRow dataventas in dtventas.Rows)
            {
                parametrosVentas.idventa = Convert.ToInt32(dataventas["idventa"]);
                parametrosVentas.Serie = dataventas["Serie"].ToString();
                parametrosVentas.Correlativo = dataventas["Correlativo"].ToString();
                parametrosVentas.fecha_venta = Convert.ToDateTime(dataventas["fecha_venta"]);
                parametrosVentas.Fecha_de_pago = Convert.ToDateTime(dataventas["Fecha_de_pago"]);
                parametrosVentas.CodigoComprobante = dataventas["Codigo"].ToString();
                parametrosVentas.contadorProductos = Convert.ToInt32(dataventas["ContadorProductos"]);
                parametrosVentas.EmpresaRUCemisor = dataventas["Ruc"].ToString();
                parametrosVentas.EmpresaRazonsocialEmisora = dataventas["RazonSocial"].ToString();
                parametrosVentas.Ubigeo = dataventas["Ubigeo"].ToString();
                var parametrosUbigeos = new Lcodigosubigeos();
                ObtenerUbigeos(parametrosVentas.Ubigeo, parametrosUbigeos);
                parametrosVentas.DptoempresaEmisora = parametrosUbigeos.Departamento;
                parametrosVentas.ProvempresaEmisora = parametrosUbigeos.Provincia;
                parametrosVentas.DistmpresaEmisora = parametrosUbigeos.Distrito;
                parametrosVentas.DireccionEmpresaEmisora = dataventas["DireccionFiscal"].ToString();
                parametrosVentas.EmpresaRUCcliente = dataventas["NroDoc"].ToString();
                parametrosVentas.EmpresaRazonsocialCliente = dataventas["Nombre"].ToString();
                parametrosVentas.DireccionCliente = dataventas["Direccion"].ToString();
                parametrosVentas.TotalIgv = Convert.ToDecimal(dataventas["TotalIgv"]);
                parametrosVentas.TotSubtotal = Convert.ToDecimal(dataventas["TotSubtotal"]);
                parametrosVentas.Monto_total = Convert.ToDecimal(dataventas["Monto_total"]);
                parametrosVentas.Porcentaje_IGV = Convert.ToDecimal(dataventas["Porcentaje_IGV"]);

            }
            var funciondeDv = new Ddetalleventas();
            var parametrosDv = new Ldetalleventas();
            var dtDv = new DataTable();
            parametrosDv.idventa = parametrosVentas.idventa;
            funciondeDv.mostrarDetalleVenta(ref dtDv, Idmesa, parametrosDv);
            var detalle = new List<Ldetalleventas>();
            foreach (DataRow items in dtDv.Rows)
            {
                var datadv = new Ldetalleventas();
                datadv.Unidad_de_medida = items["CodUm"].ToString();
                datadv.cantidad = Convert.ToDecimal(items["Cant"]);
                datadv.Total_a_pagar = Convert.ToDecimal(items["Importe"]);
                datadv.preciounitario = Convert.ToDecimal(items["P_Unit"]);
                datadv.Descripcion = items["Producto"].ToString();
                datadv.Codigo = items["Codigo"].ToString();
                datadv.CodigoProdSunat = items["CodigoSunat"].ToString();
                detalle.Add(datadv);
            }
            int estado = 0;
            var funcion = new EmitirComprobante();
            estado = funcion.EmitirFacturasContado(parametrosVentas, detalle);
            if (estado == 1)
            {
                editarEstadoSunatVenta();
            }
        }
        private void editarEstadoSunatVenta()
        {
            var funcion = new Dventas();
            var parametros = new Lventas();
            parametros.idventa = idventa;
            funcion.editarEstadoSunatVenta(parametros);
        }
        private void ObtenerUbigeos(string ubigeo, Lcodigosubigeos parametros)
        {
            var dt = new DataTable();
            var funcion = new Dcodigosubigeo();
            var parametrosUbigeo = new Lcodigosubigeos();
            parametrosUbigeo.Ubigeo = ubigeo;
            funcion.ObtenerUbicaionXubigeo(ref dt, parametrosUbigeo);
            parametros.Departamento = dt.Rows[0][0].ToString();
            parametros.Provincia = dt.Rows[0][1].ToString();
            parametros.Distrito = dt.Rows[0][2].ToString();

        }
        private void ImprimirComprobante()
        {
            var funcion = new Dventas();
            var parametros = new Lventas();
            parametros.idventa = idventa;
            parametros.Totalletras = Totalletras;
            var dt = new DataTable();
            funcion.RptComprobVenta(ref dt, parametros);
            //**********************
            var rpt = new RcomprobVenta();
            rpt.TablaDetalle.DataSource = dt;
            rpt.DataSource = dt;
            rptPedidos.Report = rpt;
            rptPedidos.RefreshReport();
            var funcionImpresoras = new Dimpresoras();
            funcionImpresoras.ImprimirVarios(rptPedidos.ReportSource, "CAJA");
        }
        private void Contadordetalleventa()
        {
            DataTable dt = new DataTable();
            Ddetalleventas funcion = new Ddetalleventas();
            Ldetalleventas parametros = new Ldetalleventas();
            parametros.idventa = idventa;
            funcion.mostrarDetalleVenta(ref dt, Idmesa, parametros);
            contadordetalleventa = dt.Rows.Count;
        }
        private void InsertarVentaDiv()
        {
            Lventas parametros = new Lventas();
            Dventas funcion = new Dventas();
            parametros.Id_mesa = Idmesa;
            parametros.Numero_personas = cantidadPersonas;
            parametros.NombreLlevar = nombrellevar;

            if (funcion.Insertar_ventas(parametros) == true)
            {
                MostrarIdventadividida();
            }
            InsertarDetalleVentaDiv();
            calcularVueltoDiv();
            EditarEstadoVentasDiv();
            confirmarVentaDiv();
            idventa = idventadividida;
            if (conexionsunat == "SI" && Comprobante != "TICKET")
            {
                if (Envioinmediato == "SI")
                {
                    EmitirFacturaContado();
                }

            }
            if (DondeConsumir != "EN LOCAL")
            {
                ImprimirPedido();
                EditardetalleventaAenviado();
            }


            ImprimirComprobante();
            MostrarIdventaMesa();
            MostrarDetalleVenta();
            datalistadoPedidos.Rows.Clear();
            richTextBox1.Clear();
            panelTotal1.Visible = true;
            Paneltotal2.Visible = false;
            Acumulado = 0;
            txtacumulado.Text = Acumulado.ToString();
        }
        private void confirmarVentaDiv()
        {

            //parametros.idventa = idventa;
            //parametros.fecha_venta = DateTime.Now;
            //parametros.Monto_total = totalpagar;
            //parametros.Tipo_de_pago = Tipopago;//Pasar
            //parametros.Estado = "TERMINADO";
            //parametros.IGV = impuestocalculado;
            //parametros.Comprobante = comprobante;
            //parametros.Fecha_de_pago = "-";
            //parametros.Pago_con = Acumulado;//Pasar
            //parametros.Referencia_tarjeta = "-";
            //parametros.Vuelto = vuelto;//Pasar
            //parametros.Efectivo = totalpagar;//Pasar
            //parametros.Credito = 0;
            //parametros.Tarjeta = Tarjeta;//Pasar
            //parametros.Porcentaje_IGV = Porcentajeimpu;
            //parametros.idclientev = idcliente;
            //Lventas parametros = new Lventas();
            //Dventas funcion = new Dventas();
            //parametros.idventa = idventadividida;
            //parametros.fecha_venta = DateTime.Now;
            //parametros.Monto_total = totalpagarDIV;
            //parametros.Tipo_de_pago = Tipopago;
            //parametros.Estado = "TERMINADO";
            //parametros.IGV = impuestocalculado;
            //parametros.Comprobante = comprobante;
            //parametros.Fecha_de_pago = "-";
            //parametros.Pago_con = Acumulado;
            //parametros.Referencia_tarjeta = "-";
            //parametros.Vuelto = vuelto;
            //parametros.Efectivo = efectivocalculado;
            //parametros.Credito = 0;
            //parametros.Tarjeta = Tarjeta;
            //parametros.Porcentaje_IGV = Porcentajeimpu;
            //parametros.idclientev = idcliente;
            //funcion.Confirmar_venta(parametros);
            ValidarClienteStandar();
            Lventas parametros = new Lventas();
            Dventas funcion = new Dventas();
            parametros.idventa = idventadividida;
            parametros.idclientev = idcliente;
            parametros.fecha_venta = DateTime.Now;
            parametros.Monto_total = totalpagarDIV;
            parametros.Tipo_de_pago = Tipopago;//Pasar
            parametros.TotalIgv = impuestocalculado;
            parametros.Fecha_de_pago = DateTime.Now;
            parametros.Saldo = 0;
            parametros.Pago_con = Acumulado;//Pasar
            parametros.Referencia_tarjeta = referenciaTarjeta;
            parametros.Vuelto = vuelto;//Pasar
            parametros.Efectivo = efectivocalculado;//Pasar
            parametros.Credito = 0;
            parametros.Tarjeta = Tarjeta;//Pasar
            parametros.Idcomprobante = idcomprobante;
            funcion.Confirmar_venta(parametros);
            Acumulado = 0;
            convertirTotalaletras();
        }
        private void calcularVueltoDiv()
        {
            if (panelContCobro.Visible == false)
            {
                vuelto = Acumulado - totalpagarDIV;
                efectivocalculado = Acumulado - vuelto;
            }
        }
        private void EditarEstadoVentasDiv()
        {
            Ldetalleventas parametros = new Ldetalleventas();
            Ddetalleventas funcion = new Ddetalleventas();
            foreach (DataGridViewRow row in datalistadoPedidos.Rows)
            {
                int iddetalleventa = Convert.ToInt32(row.Cells["Iddetalleventa"].Value);
                parametros.iddetalle_venta = iddetalleventa;
                funcion.editarEstadoDv(parametros);
            }
        }
        private void InsertarDetalleVentaDiv()
        {
            Ldetalleventas parametros = new Ldetalleventas();
            Ddetalleventas funcion = new Ddetalleventas();
            foreach (DataGridViewRow row in datalistadoPedidos.Rows)
            {
                int iddetalleventa = Convert.ToInt32(row.Cells["Iddetalleventa"].Value);
                parametros.idventa = idventadividida;
                parametros.iddetalle_venta = iddetalleventa;
                funcion.insertarDetalleVentaDiv(parametros);

            }
        }
        private void MostrarIdventadividida()
        {
            Dventas funcion = new Dventas();
            Lventas parametros = new Lventas();
            parametros.Id_mesa = Idmesa;
            funcion.mostrarIdventaMesa(ref idventadividida, parametros);
        }

        private void calcularVuelto()
        {
            if (panelContCobro.Visible == false)
            {
                vuelto = Acumulado - totalpagar;
                efectivocalculado = Acumulado - vuelto;
            }

        }
        private void EditarEstadoDvNormal()
        {
            //Editar detalle venta
            Ldetalleventas parametros = new Ldetalleventas();
            Ddetalleventas funcion = new Ddetalleventas();
            //

            //Para mostrar detalle venta
            DataTable dt = new DataTable();
            Ddetalleventas funcionmostrar = new Ddetalleventas();
            parametros.idventa = idventa;
            funcionmostrar.mostrarDetalleVenta(ref dt, Idmesa, parametros);
            //
            foreach (DataRow row in dt.Rows)
            {
                int iddetalleventa = Convert.ToInt32(row["iddetalle_venta"]);
                parametros.iddetalle_venta = iddetalleventa;
                funcion.editarEstadoDv(parametros);

            }
        }

        private void btnNotas_Click(object sender, EventArgs e)
        {
            var frm = new Notas();
            Notas.nota = lblNota.Text;
            Notas.idventa = idventa;
            frm.FormClosed += Frm_FormClosed;
            frm.ShowDialog();
        }

        private void Frm_FormClosed(object sender, FormClosedEventArgs e)
        {

            if (Tiponotas == "Porpedido")
            {
                editarNotasporpedido();

            }
            else
            {
                lblNota.Text = Notas.nota;
            }


        }
        private void editarNotasporpedido()
        {
            var funcion = new Ddetalleventas();
            var parametros = new Ldetalleventas();
            parametros.iddetalle_venta = Iddetalle_venta;
            parametros.Nota = Notas.nota;
            funcion.editarNotaproducto(parametros);
            richTextBox1.Clear();
            MostrarDetalleVenta();

        }
        private void btneliminarVenta_Click(object sender, EventArgs e)
        {
            eliminarVenta();
            LiberarMesa();
            VolverVisorMesas();
        }
        private void LiberarMesa()
        {
            var funcion = new Dmesas();
            var parametros = new Lmesas();
            parametros.Id_mesa = Idmesa;
            funcion.EditarEstadoMesaLibre(parametros);
        }
        private void btnPrecuenta_Click(object sender, EventArgs e)
        {
            ImprimirPrecuenta();
        }
        private void ImprimirPrecuenta()
        {
            MostrarIdventasxMesa();
            var funcion = new Dventas();
            var parametros = new Lventas();

            foreach (DataRow dr in dtidVentasmesas.Rows)
            {
                var idventas = Convert.ToInt32(dr["idventa"]);
                //*************
                parametros.idventa = idventas;
                var dt = new DataTable();
                funcion.RptPrecuenta(ref dt, parametros);
                //*************
                var rpt = new Rprecuenta();
                rpt.DataSource = dt;
                rpt.TablaDetalle.DataSource = dt;
                rptPedidos.Report = rpt;
                rptPedidos.RefreshReport();
                var funcionImpresora = new Dimpresoras();
                funcionImpresora.ImprimirVarios(rptPedidos.ReportSource, "precuenta");
            }




        }

        private void btncuentaadelante_Click(object sender, EventArgs e)
        {
            contadorventas -= 1;
            if (contadorventas < 0)
            {
                btncuentaadelante.Visible = false;
                btncuentaatras.Visible = true;
            }
            else
            {
                btncuentaadelante.Visible = true;
                btncuentaatras.Visible = true;
                ValidarVentasxMesa();
            }
            if (contadorventas == 0)
            {
                btncuentaadelante.Visible = false;
                btncuentaatras.Visible = true;
            }
            MostrarDetalleVenta();

        }
        private void ValidarVentasxMesa()
        {
            MostrarIdventasxMesa();
            int contador = 0;
            contador = dtidVentasmesas.Rows.Count;
            if (contador > 0)
            {
                if (contador > 1)
                {
                    idventa = Convert.ToInt32(dtidVentasmesas.Rows[contadorventas][0]);
                    lblNota.Text = dtidVentasmesas.Rows[contadorventas][1].ToString();
                }
                else
                {
                    idventa = Convert.ToInt32(dtidVentasmesas.Rows[0][0]);
                    lblNota.Text = (dtidVentasmesas.Rows[0][1]).ToString();
                }
            }
            else
            {
                idventa = 0;
            }
            lblcuenta.Text = idventa.ToString();
            if (idventa > 0)
            {
                ventagenerada = "VENTA GENERADA";

            }
            else
            {
                ventagenerada = "VENTA NUEVA";
            }
        }
        private void MostrarIdventasxMesa()
        {
            var funcion = new Dventas();
            var parametros = new Lventas();
            dtidVentasmesas = new DataTable();
            parametros.Id_mesa = Idmesa;
            funcion.mostrarIdventasMesa(ref dtidVentasmesas, parametros);
        }

        private void btncuentaatras_Click(object sender, EventArgs e)
        {
            contadorventas += 1;
            if (contadorventas > incrementoVentas)
            {
                btncuentaatras.Visible = false;
                btncuentaadelante.Visible = true;
            }

            else
            {
                btncuentaatras.Visible = true;
                btncuentaadelante.Visible = true;

                ValidarVentasxMesa();

            }
            if (contadorventas == incrementoVentas)
            {
                btncuentaatras.Visible = false;
                btncuentaadelante.Visible = true;

            }
            MostrarDetalleVenta();
        }

        private void btnDividirCuenta_Click(object sender, EventArgs e)
        {
            Dispose();
            VisorDecuentas frm = new VisorDecuentas();
            frm.ShowDialog();
        }

        private void btncobrar_Click(object sender, EventArgs e)
        {
            panelCobro.Visible = true;
            panelContCobro.Visible = true;
            PanelVentasGeneral.Enabled = false;
            panel2.Enabled = false;
            txttarjeta.Clear();

            if (datalistadoPedidos.RowCount > 0)
            {

                lbltotal.Text = totalpagarDIV.ToString();
                totalpagar = totalpagarDIV;
                txtefectivo.Text = totalpagarDIV.ToString();

            }
            else
            {
                lbltotal.Text = totalpagar.ToString();
                txtefectivo.Text = totalpagar.ToString();
            }
            ObtenerComprobanteDefecto();

        }



        private void btnsalir_Click(object sender, EventArgs e)
        {
            Dispose();
            var frm = new Visor_de_mesas();

            frm.Dispose();
        }
        void ObtenerTipopago()
        {
            int indicadorEfectivo = 4;
            int indicadorTarjeta = 3;

            // validacion para evitar valores vacios
            if (txtefectivo.Text == "")
            {
                txtefectivo.Text = "0";
            }
            if (txttarjeta.Text == "")
            {
                txttarjeta.Text = "0";
            }

            //validacion de .
            if (txtefectivo.Text == ".")
            {
                txtefectivo.Text = "0";
            }
            if (txttarjeta.Text == ".")
            {
                txttarjeta.Text = "0";
            }

            //validacion de 0
            if (txtefectivo.Text == "0")
            {
                indicadorEfectivo = 0;
            }
            if (txttarjeta.Text == "0")
            {
                indicadorTarjeta = 0;
            }

            //calculo de indicador
            int calculo_identificacion = indicadorEfectivo + indicadorTarjeta;
            //consulta al identificador
            if (calculo_identificacion == 4)
            {
                Tipopago = "EFECTIVO";
            }
            if (calculo_identificacion == 3)
            {
                Tipopago = "TARJETA";
            }
            if (calculo_identificacion > 4)
            {
                Tipopago = "MIXTO";
            }
        }
        private void ValidarCliente()
        {

            if (Comprobante == "FACTURA" && idcliente == 0 && Tipopago != "CREDITO")
            {
                MessageBox.Show("Seleccione un Cliente, para Facturas es Obligatorio", "Datos Incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (Comprobante == "FACTURA" && idcliente != 0)
            {
                if (Tipocliente == "RUC")
                {
                    procesarVenta();
                }
                else
                {
                    MessageBox.Show("Ingrese cliente con RUC");
                }

            }

            else if (Comprobante != "FACTURA" && Tipopago != "CREDITO")
            {
                ValidarClienteStandar();
                procesarVenta();
            }
            else if (Comprobante != "FACTURA" && Tipopago == "CREDITO")
            {
                if (idcliente == 0)
                {
                    procesarVenta();
                }
                else
                {
                    MessageBox.Show("Seleccione un Cliente", "Datos Incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
        }
        private void ValidarClienteStandar()
        {
            if (idcliente == 0)
            {
                MostrarclienteStandar();
            }
        }
        void MostrarclienteStandar()
        {
            var funcion = new Dclientes();
            funcion.mostrarClienteEstandar(ref idcliente);
        }
        private void procesarVenta()
        {
            if (restante == 0)
            {
                if (datalistadoPedidos.RowCount > 0)
                {
                    Acumulado = totalpagarDIV;
                }
                else
                {
                    Acumulado = totalpagar;
                }
                ObtenerTipopago();
                int contadorreferencia = 0;
                contadorreferencia = txtreferencia.Text.Length;
                if ((Tipopago == "TARJETA" || Tipopago == "MIXTO") && txtreferencia.Text == "")
                {
                    MessageBox.Show("Ingrese los ultimos 4 digitos de la tarjeta");
                }
                else if ((Tipopago == "TARJETA" || Tipopago == "MIXTO") && contadorreferencia < 4)
                {
                    MessageBox.Show("Ingrese los ultimos 4 digitos de la tarjeta");
                }
                else if ((Tipopago == "TARJETA" || Tipopago == "MIXTO") && contadorreferencia > 4)
                {
                    MessageBox.Show("Ingrese los ultimos 4 digitos de la tarjeta");
                }
                else
                {
                    if (Tipopago != "EFECTIVO")
                    {
                        referenciaTarjeta = txtreferencia.Text;
                    }
                    Validartiposdeventa();
                    panelContCobro.Visible = false;
                    panelCobro.Visible = false;
                    PanelVentasGeneral.Enabled = true;
                    panel2.Enabled = true;

                }


            }
            else
            {
                MessageBox.Show("El restante debe ser 0", "Datos incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void btnguardar_Click(object sender, EventArgs e)
        {
            ValidarCliente();
        }

        private void txtefectivo_TextChanged(object sender, EventArgs e)
        {
            // txtefectivo.Text = txtefectivo.Text.Replace(",", ".");
            calcular_restante();
        }
        private void calcular_restante()
        {

            try
            {
                // lbltotal.Text = lbltotal.Text.Replace(".", ",");

                if (txtefectivo.Text == "")
                {
                    efectivo = 0;
                }
                else
                {
                    //string efec = txtefectivo.Text + "M";
                    //efectivo =Convert.ToDecimal( efec);
                    double efectivodouble = Convert.ToDouble(txtefectivo.Text);
                    efectivo = Convert.ToDecimal(efectivodouble);
                }

                if (txttarjeta.Text == "")
                {
                    Tarjeta = 0;
                }
                else
                {
                    Tarjeta = Convert.ToDecimal(txttarjeta.Text);
                }

                if (txtefectivo.Text == "0.00")
                {
                    efectivo = 0;
                }

                if (txttarjeta.Text == "0.00")
                {
                    Tarjeta = 0;

                }

                if (txtefectivo.Text == ".")
                {
                    efectivo = 0;
                }

                try
                {
                    if (efectivo > totalpagar)
                    {
                        efectivocalculado = (totalpagar - Tarjeta);
                        if (efectivocalculado < 0)
                        {
                            vuelto = 0;
                            txtvuelto.Text = "0";
                            txtrestante.Text = Convert.ToString(efectivocalculado);
                            restante = efectivocalculado;
                        }
                        else
                        {

                            vuelto = efectivo - (totalpagar - Tarjeta);
                            txtvuelto.Text = Convert.ToString(vuelto);
                            restante = 0;
                            txtrestante.Text = Convert.ToString(restante);
                            txtrestante.Text = decimal.Parse(txtrestante.Text).ToString("##0.00");
                        }

                    }
                    else
                    {
                        vuelto = 0;
                        txtvuelto.Text = "0";
                        efectivocalculado = efectivo;
                        restante = totalpagar - efectivocalculado - Tarjeta;
                        txtrestante.Text = Convert.ToString(restante);
                        txtrestante.Text = decimal.Parse(txtrestante.Text).ToString("##0.00");
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.StackTrace);
                }

            }
            catch (Exception ex)
            {
                //  MessageBox.Show(ex.Message);
            }
        }

        private void txttarjeta_TextChanged(object sender, EventArgs e)
        {
            txttarjeta.Text = txttarjeta.Text.Replace(",", ".");
            calcular_restante();
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            panelCobro.Visible = false;
            panelContCobro.Visible = false;
            PanelVentasGeneral.Enabled = true;
            panel2.Enabled = true;
        }

        private void btnGrupoadelante_Click_1(object sender, EventArgs e)
        {
            contar_grupos();
            if (cantidad_grupos > paginaMaxima)
            {

                paginainicio += 10;
                paginaMaxima += 10;
                dibujarGrupos();
            }
        }

        private void btngrupoAtras_Click_1(object sender, EventArgs e)
        {
            if (paginainicio > 1)
            {

                paginainicio -= 10;
                paginaMaxima -= 10;
                dibujarGrupos();
            }
        }

        private void btnadelante_Click_1(object sender, EventArgs e)
        {
            contar_productos();
            if (cantidad_productos > paginaMaximaPro)
            {
                paginainicioPro += 15;
                paginaMaximaPro += 15;
                dibujarProductos();
            }
        }

        private void btnatras_Click_1(object sender, EventArgs e)
        {
            if (paginainicioPro > 1)
            {
                paginainicioPro -= 15;
                paginaMaximaPro -= 15;
                dibujarProductos();
            }
        }

        private void txtefectivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Separador_de_Numeros(txtefectivo, e);
        }

        private void txttarjeta_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Separador_de_Numeros(txttarjeta, e);

        }

        private void txtreferencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        public void Mostrarpaneles()
        {
            panel4.Visible = true;
            Panel_grupos.Visible = true;
            panel7.Visible = true;
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            Panel_grupos.Visible = false;
            panel7.Visible = false;
            var ctl = new Buscadorproductos();
            ctl.Dock = DockStyle.Fill;
            Panel9.Controls.Add(ctl);
            ctl.BringToFront();

        }

        private void IndicadorTema_CheckedChanged(object sender, EventArgs e)
        {
            if (IndicadorTema.Checked == true)
            {
                Tema = "Oscuro";
                Editartema();
            }
            else
            {
                Tema = "Claro";
                Editartema();
            }
        }
        private void Editartema()
        {
            var funcion = new Dcaja();
            var parametros = new Lcaja();
            parametros.Tema = Tema;
            funcion.editarTemacaja(parametros);
            ValidarTema();
            MostrarDetalleVenta();
        }
        private void TemaOscuro()
        {
            this.BackColor = Color.FromArgb(29, 29, 29);
            lblfecha.ForeColor = Color.Silver;
            lblhora.ForeColor = Color.Silver;
            lblMesa.ForeColor = Color.Silver;
            label19.ForeColor = Color.Silver;
            //
            label20.ForeColor = Color.FromArgb(255, 231, 89);
            lblcuenta.ForeColor = Color.FromArgb(255, 231, 89);
            txtacumulado.ForeColor = Color.FromArgb(255, 231, 89);
            lblCantclientesti.ForeColor = Color.White;
            lblcantidadPersonas.ForeColor = Color.White;
            Label18.ForeColor = Color.White;
            panel33.BackColor = Color.White;
            //
            lblNota.ForeColor = Color.White;
            label35.ForeColor = Color.LightSkyBlue;
        }
        private void TemaClaro()
        {
            this.BackColor = Color.WhiteSmoke;
            lblfecha.ForeColor = Color.FromArgb(29, 29, 29);
            lblhora.ForeColor = Color.FromArgb(29, 29, 29);
            lblMesa.ForeColor = Color.FromArgb(29, 29, 29);
            label19.ForeColor = Color.FromArgb(29, 29, 29);
            //
            label20.ForeColor = Color.FromArgb(29, 29, 29);
            lblcuenta.ForeColor = Color.FromArgb(29, 29, 29);
            txtacumulado.ForeColor = Color.FromArgb(29, 29, 29);
            lblCantclientesti.ForeColor = Color.FromArgb(29, 29, 29);
            lblcantidadPersonas.ForeColor = Color.FromArgb(29, 29, 29);
            Label18.ForeColor = Color.FromArgb(29, 29, 29);
            panel33.BackColor = Color.FromArgb(29, 29, 29);
            lblNota.ForeColor = Color.FromArgb(29, 29, 29);
            label35.ForeColor = Color.FromArgb(16, 107, 179);

        }
        private void txtacumulado_TextChanged(object sender, EventArgs e)
        {

        }

        private void timerImprimir_Tick(object sender, EventArgs e)
        {
            var funcionImpr = new Dventas();
            funcionImpr.MostrarsolicitudImpr(ref idventaSolicitud);

            if (idventaSolicitud > 0)
            {
                timerImprimir.Stop();
                if (tipocaja == "PRINCIPAL")
                {
                    var funciondv = new Ddetalleventas();
                    var parametrosdv = new Ldetalleventas();
                    var dtdv = new DataTable();
                    parametrosdv.idventa = idventaSolicitud;
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

                    idventaSolicitud = 0;
                    timerImprimir.Start();
                }
                else
                {

                    EliminarSolImpri();

                    idventaSolicitud = 0;
                    timerImprimir.Start();
                }
            }
        }
        private void EliminarSolImpri()
        {
            var funcion = new Dventas();
            var parametros = new Lventas();
            parametros.idventa = idventa;
            funcion.eliminarSolicitudImpr(parametros);
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
                EliminarEsc();
            }
        }
        private void EliminarEsc()
        {
            var funcion = new Dventas();
            funcion.eliminarSolicitudEsc();
        }

        private void txtbuscarcliente_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtbuscarcliente.Text))
            {
                buscarclientes();
            }
            else
            {
                dgClientes.Visible = false;
            }
        }
        private void buscarclientes()
        {
            DataTable dt = new DataTable();
            var funcion = new Dclientes();
            funcion.buscar_clientes(ref dt, txtbuscarcliente.Text);
            dgClientes.DataSource = dt;
            dgClientes.Visible = true;
            dgClientes.Size = new Size(288, 78);
            dgClientes.Location = new Point(panelClienteFactura.Location.X, panelClienteFactura.Location.Y + 80);
            dgClientes.Columns[0].Visible = false;
            dgClientes.Columns[2].Visible = false;
            dgClientes.Columns[3].Visible = false;
            dgClientes.Columns[4].Visible = false;
            dgClientes.Columns[5].Visible = false;
            dgClientes.Columns[6].Visible = false;
            dgClientes.BringToFront();

        }

        private void btnAgregarcliente_Click(object sender, EventArgs e)
        {
            var ctl = new Agregarcliente();
            ctl.Dock = DockStyle.Fill;
            panelCobro.Controls.Add(ctl);
            ctl.BringToFront();
        }

        private void dgClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idcliente = Convert.ToInt32(dgClientes.SelectedCells[0].Value);
            txtbuscarcliente.Text = dgClientes.SelectedCells[1].Value.ToString();
            Tipocliente = dgClientes.SelectedCells[7].Value.ToString();
            dgClientes.Visible = false;
        }
    }
}
