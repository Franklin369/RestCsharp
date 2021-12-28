using RestCsharp.Datos;
using Sunat.Logica;
using RestCsharp.Sunat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ada369Csharp.Presentacion.SunatForms
{
    public partial class Sfacturas : UserControl
    {
        public Sfacturas()
        {
            InitializeComponent();
        }
        int idventa;
        private void Sfacturas_Load(object sender, EventArgs e)
        {
            buscarFacturasBoletas();
            ContFBAprobadas();
            ContFBPendientes();
        }
        private void ContFBAprobadas()
        {
            var funcion = new Dventas();
            int contador = 0;
            funcion.ContFBAprobadas(ref contador, "01");
            btnAceptadas.Text = "Aceptadas: [" + contador.ToString() + "]";
        }
        private void ContFBPendientes()
        {
            var funcion = new Dventas();
            int contador = 0;
            funcion.ContFBPendientes(ref contador, "01");
            if (contador > 0)
            {
                btnEspera.Visible = true;
                btnEspera.Text = "En espera: [" + contador.ToString() + "] ENVIAR TODO";
            }
            else
            {
                btnEspera.Visible = false;
            }
        }
        private void buscarFacturasBoletas()
        {
            Panelvisor.Controls.Clear();
            var funcion = new Dventas();
            var dt = new DataTable();
            funcion.buscarFacturas(ref dt, txtbuscar.Text, "01");
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
                else if (data["Estadosunat"].ToString() == "ANULADA")
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
            funcion.MostrarventasPend(ref dt, "01");
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
