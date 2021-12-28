using RestCsharp.Datos;
using RestCsharp.Sunat;
using Sunat.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ada369Csharp.Presentacion.SunatForms
{
    public partial class Sboletas : UserControl
    {
        public Sboletas()
        {
            InitializeComponent();
        }
        int idventa;
        private void Sboletas_Load(object sender, EventArgs e)
        {
            buscarFacturasBoletas();
            ContFBAprobadas();
            ContFBPendientes();
        }
        private void ContFBAprobadas()
        {
            var funcion = new Dventas();
            int contador = 0;
            funcion.ContFBAprobadas(ref contador, "03");
            btnAceptadas.Text = "Aceptadas: [" + contador.ToString() + "]";
        }
        private void ContFBPendientes()
        {
            panelpendientes.Controls.Clear();
            var funcion = new Dventas();
            var dt = new DataTable();
            funcion.MostrarBoletaspendientes(ref dt);
            if (dt.Rows.Count == 0)
            {
                var lblcontador = new Label();
                lblcontador.Text = "Sin envios pendientes";
                lblcontador.BackColor = Color.Transparent;
                lblcontador.FlatStyle = FlatStyle.Flat;
                lblcontador.Size = new Size(153, 69);
                lblcontador.ForeColor = Color.FromArgb(27, 254, 193);
                lblcontador.Font = new Font("Consolas", 13, FontStyle.Bold);
                panelpendientes.Controls.Add(lblcontador);
            }
            foreach (DataRow data in dt.Rows)
            {
                var panel = new Panel();
                panel.Size = new Size(220, 69);
                var lblcontador = new Label();
                var btnenviar = new Button();

                lblcontador.Text = Convert.ToDateTime(data["fecha"]).ToString("dd/MM/yyyy") + " pendientes: " + data["contador"].ToString();
                lblcontador.Name = Convert.ToDateTime(data["fecha"]).ToString("dd/MM/yyyy");
                lblcontador.BackColor = Color.FromArgb(39, 39, 39);
                lblcontador.FlatStyle = FlatStyle.Flat;
                lblcontador.Size = new Size(153, 69);
                lblcontador.ForeColor = Color.White;
                lblcontador.Font = new Font("Consolas", 11, FontStyle.Bold);
                lblcontador.Dock = DockStyle.Fill;
                lblcontador.AutoSize = false;
                lblcontador.TextAlign = ContentAlignment.MiddleCenter;
                //
                btnenviar.Text = "Enviar resumen";
                btnenviar.Name = Convert.ToDateTime(data["fecha"]).ToString("dd/MM/yyyy");
                btnenviar.BackColor = Color.FromArgb(41, 206, 105);
                btnenviar.FlatStyle = FlatStyle.Flat;
                btnenviar.FlatAppearance.BorderSize = 0;
                btnenviar.Size = new Size(100, 69);
                btnenviar.ForeColor = Color.White;
                btnenviar.Font = new Font("Consolas", 13, FontStyle.Bold);
                btnenviar.Dock = DockStyle.Right;
                panel.Controls.Add(lblcontador);
                panel.Controls.Add(btnenviar);
                lblcontador.BringToFront();
                //
                panelpendientes.Controls.Add(panel);
                btnenviar.Click += Btnenviar_Click;

            }
        }
        string PassCertificado;
        string PassSecundario;
        string UserSecundario;
        string RutaCertificado;
        string Servidor;
        string Rucemisor;
        string Razonsocial;
        private void Mostrardatosempresa()
        {
            var funcion = new Dempresa();
            var dt = new DataTable();
            funcion.mostrar_empresa(ref dt);
            PassCertificado = dt.Rows[0][21].ToString();
            UserSecundario = dt.Rows[0][22].ToString();
            PassSecundario = dt.Rows[0][23].ToString();
            Rucemisor = dt.Rows[0][14].ToString();
            RutaCertificado = dt.Rows[0][20].ToString();
            Servidor = dt.Rows[0][19].ToString();
            Razonsocial = dt.Rows[0][15].ToString();
        }
        private void Btnenviar_Click(object sender, EventArgs e)
        {
            ListarComprobPendientes();
        }
        public List<Lresumendiario> Obtenerresumendiario ( string fecha)
        {
            var funcion = new Dventas();
            var dt = new DataTable();
            //DateTime fecha = Convert.ToDateTime(fechastring);
            funcion.MostrarResumendiarioPendiente(ref dt, fecha);
        
            var lista = new List<Lresumendiario>();
         
            
                foreach (DataRow items in dt.Rows)
                {
                    var parametros = new Lresumendiario();
                    parametros.id = Convert.ToInt32(items["Id"]);
                    parametros.FechaEmision = items["FECHAEMISION"].ToString();
                    parametros.FechaGeneracion = items["FECHAGENERACION"].ToString();
                    parametros.IdTipoComp = items["IdTipoComp"].ToString();
                    parametros.IdTipoDoc = items["Tipodoc"].ToString();
                    parametros.NumDoc = items["NroDoc"].ToString();
                    parametros.NumeroComprobante = items["NumeroComprobante"].ToString();
                    parametros.IdMoneda = items["codMoneda"].ToString();
                    parametros.TOT_VALOR_VENTA = Convert.ToDecimal(items["TOT_VALOR_VENTA"]);
                    parametros.TOT_OPI = Convert.ToDecimal(items["TOT_OPI"]);
                    parametros.TOT_OPE = Convert.ToDecimal(items["TOT_OPE"]);
                    parametros.TOT_OPG = Convert.ToDecimal(items["TOT_OPG"]);
                    parametros.TOT_OPOT = Convert.ToDecimal(items["TOT_OPG"]);
                    parametros.TOT_OTOT = Convert.ToDecimal(items["TOT_OPG"]);
                    parametros.TOT_ISC = Convert.ToDecimal(items["TOT_OPG"]);
                    parametros.TOT_IGV = Convert.ToDecimal(items["TotalIgv"]);
                    parametros.TOT_NETO = Convert.ToDecimal(items["TOT_NETO"]);
                    parametros.TipoDocRef = "-";
                    parametros.SerieDocModifica = "-";
                    parametros.NumeroDocModifica = "-";
                    parametros.tipRegPercepcion = "-";
                    parametros.porPercepcion = "-";
                    parametros.monBasePercepcion = "-";
                    parametros.monPercepcion = "-";
                    parametros.monTotIncPercepcion = "-";
                    parametros.Adicionar = items["Adicionar"].ToString();

                    lista.Add(parametros);
                }
                return lista;
            
        }
        private void buscarFacturasBoletas()
        {
            Panelvisor.Controls.Clear();
            var funcion = new Dventas();
            var dt = new DataTable();
            funcion.buscarFacturas(ref dt, txtbuscar.Text, "03");
            foreach (DataRow data in dt.Rows)
            {
                Button btn = new Button();
                PictureBox Iconoestado = new PictureBox();
                Panel panelEstado = new Panel();
                Panel panelContenedor = new Panel();
                Label lblestado = new Label();

                panelContenedor.Size = new Size(449, 87);
                panelContenedor.BackColor = Color.FromArgb(64, 64, 64);
                btn.Text = data["Tipo comprobante"].ToString() + ": " + data["Comprobante"].ToString() + "\n" + "Fecha: " + data["fecha_venta"].ToString() + "\n" + "Cliente: " + data["cliente"].ToString();
                btn.Size = new Size(449, 87);
                btn.Dock = DockStyle.Fill;
                btn.FlatStyle = FlatStyle.Flat;
                btn.BackColor = Color.FromArgb(64, 64, 64);
                btn.ForeColor = Color.White;
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(17, 65, 141);
                btn.Font = new Font("Consolas", 12, FontStyle.Bold);
                btn.TextAlign = ContentAlignment.MiddleLeft;
                #region estados
                lblestado.Text = data["Estadosunat"].ToString();

                lblestado.Dock = DockStyle.Bottom;
                lblestado.TextAlign = ContentAlignment.MiddleCenter;
                lblestado.Font = new System.Drawing.Font("Consolas", 10, FontStyle.Bold);
                panelEstado.Size = new Size(101, 150);
                panelEstado.Dock = DockStyle.Right;
                panelEstado.BackColor = Color.Transparent;

                Iconoestado.SizeMode = PictureBoxSizeMode.Zoom;
                Iconoestado.Dock = DockStyle.Bottom;
                panelEstado.Controls.Add(Iconoestado);
                panelEstado.Controls.Add(lblestado);
                if (data["Estadosunat"].ToString() == "ACEPTADA")
                {
                    lblestado.ForeColor = Color.FromArgb(39, 229, 143);
                    Iconoestado.Image = RestCsharp.Properties.Resources.satisfaccion;
                }
                else if (data["Estadosunat"].ToString() == "PENDIENTE")
                {
                    lblestado.ForeColor = Color.FromArgb(255, 198, 73);
                    Iconoestado.Image = RestCsharp.Properties.Resources.dia;
                }
                else if (data["Estadosunat"].ToString() == "RECHAZADA")
                {
                    lblestado.ForeColor = Color.FromArgb(252, 86, 95);
                    Iconoestado.Image = RestCsharp.Properties.Resources.insatisfaccion;
                }
                #endregion


                panelContenedor.Controls.Add(panelEstado);
                panelContenedor.Controls.Add(btn);

                Panelvisor.Controls.Add(panelContenedor);
                btn.BringToFront();
            }
        }

        private void txtbuscar_TextChanged(object sender, EventArgs e)
        {
            buscarFacturasBoletas();
        }

        private void btnEspera_Click(object sender, EventArgs e)
        {
            ListarComprobPendientes();
        }
        private void ListarComprobPendientes()
        {
            var funcion = new Dventas();
            var dt = new DataTable();
            funcion.MostrarventasPend(ref dt, "03");
            foreach (DataRow data in dt.Rows)
            {
                idventa = Convert.ToInt32(data["idventa"]);
                EmitirFacturaContado();
            }
            ContFBAprobadas();
            ContFBPendientes();
            txtbuscar.Clear();
            buscarFacturasBoletas();
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
                parametrosVentas.Ubigeo = dataventas["Ubigeo"].ToString().Trim();
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
            funciondeDv.mostrardv(ref dtDv, parametrosDv);
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
        private void editarEstadoSunatVenta()
        {
            var funcion = new Dventas();
            var parametros = new Lventas();
            parametros.idventa = idventa;
            funcion.editarEstadoSunatVenta(parametros);
        }
    
    }
}
