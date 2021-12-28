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
    public partial class AgregarNdebito : UserControl
    {
        public AgregarNdebito()
        {
            InitializeComponent();
        }
        string serieRef;
        string correlativoRef;
        int idventa;
        string codigoComprobanteRef;
        string motivo;
        Panel panelcomprobante = new Panel();
        string CodTipoNd;
        int idcomprobanteNd;
        private void AgregarNdebito_Load(object sender, EventArgs e)
        {
            MostrarIdComproNd();
            mostrarTiposnotas();
        }
        private void MostrarIdComproNd()
        {
            var funcion = new Dserealizacion();
            var dt = new DataTable();
            funcion.MostrarNotadebito(ref dt);
            idcomprobanteNd = Convert.ToInt32(dt.Rows[0][0]);
        }
        private void mostrarTiposnotas()
        {
            var funcion = new Dnotasdebito();
            var dt = new DataTable();
            funcion.mostrarTipoNd(ref dt);
            txttipo.DisplayMember = "Descripcion";
            txttipo.ValueMember = "Codigo";
            txttipo.DataSource = dt;
        }

        private void txtbuscar_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbuscar.Text))
            {
                ocultarPanelComprobante();
            }
            else
            {
                mostrarComprobantes();
            }
        }
        private void ocultarPanelComprobante()
        {
            panelcomprobante.Visible = false;
            dgcomprobantes.Visible = false;
        }
        private void mostrarComprobantes()
        {
            panelcomprobante.Size = new Size(600, 186);
            panelcomprobante.BackColor = Color.White;
            panelcomprobante.Location = new Point(txtbuscar.Location.X, txtbuscar.Location.Y + 22);
            panelcomprobante.Visible = true;
            dgcomprobantes.Visible = true;
            dgcomprobantes.Dock = DockStyle.Fill;
            dgcomprobantes.BackgroundColor = Color.White;
            panelcomprobante.Controls.Add(dgcomprobantes);
            this.Controls.Add(panelcomprobante);
            panelcomprobante.BringToFront();
            buscarventas();
        }
        private void buscarventas()
        {
            var funcion = new Dventas();
            var dt = new DataTable();
            funcion.buscarVentas(ref dt, txtbuscar.Text);
            dgcomprobantes.DataSource = dt;
        }

        private void dgcomprobantes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            serieRef = dgcomprobantes.SelectedCells[4].Value.ToString();
            correlativoRef = dgcomprobantes.SelectedCells[5].Value.ToString();

            idventa = Convert.ToInt32(dgcomprobantes.SelectedCells[2].Value);
            codigoComprobanteRef = dgcomprobantes.SelectedCells[3].Value.ToString();
            txtbuscar.Text = dgcomprobantes.SelectedCells[1].Value.ToString();
            ocultarPanelComprobante();
        }

        private void btnconfirmar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtmotivo.Text))
            {
                InsertarNotacredito();
            }
            else
            {
                MessageBox.Show("Ingrese un motivo");
            }
        }
        private void InsertarNotacredito()
        {
            CodTipoNd = txttipo.SelectedValue.ToString();
            var funcion = new Dnotasdebito();
            var parametros = new Lnotasdebito();
            parametros.idventa = idventa;
            parametros.idcomprobante = idcomprobanteNd;
            parametros.codTipond = CodTipoNd;
            if (funcion.InsertarNotaDebito(parametros) == true)
            {
                EmitirNotadebito();
            }
        }
        private void EmitirNotadebito()
        {
            var parametrosVentas = new Lventas();
            var funcionVentas = new Dnotasdebito();
            var dtventas = new DataTable();
            var parametros = new Lventas();
            parametros.idventa = idventa;
            funcionVentas.mostrarNotasdebitoXidventa(ref dtventas, parametros);

            foreach (DataRow dataventas in dtventas.Rows)
            {
                #region parametros exclusivos de Nota credito
                parametrosVentas.Cab_Ref_Serie = serieRef;
                parametrosVentas.Cab_Ref_Numero = correlativoRef;
                parametrosVentas.Cab_Ref_Motivo = txtmotivo.Text;
                parametrosVentas.Cab_Ref_TipoComprobante = codigoComprobanteRef;
                parametrosVentas.CodigoTipoNotacredito = CodTipoNd;
                #endregion
                parametrosVentas.idventa = Convert.ToInt32(dataventas["idventa"]);
                parametrosVentas.Serie = dataventas["SerieNc"].ToString();
                parametrosVentas.Correlativo = dataventas["CorrelativoNc"].ToString();
                parametrosVentas.fecha_venta = Convert.ToDateTime(dataventas["fecha_venta"]);
                parametrosVentas.Fecha_de_pago = Convert.ToDateTime(dataventas["Fecha_de_pago"]);
                parametrosVentas.CodigoComprobante = "07";
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
                parametrosVentas.EmpresaRUCcliente = dataventas["Ruc"].ToString();
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
                datadv.Unidad_de_medida = items["Unidad_de_medida"].ToString();
                datadv.cantidad = Convert.ToDecimal(items["Cant"]);
                datadv.Total_a_pagar = Convert.ToDecimal(items["Importe"]);
                datadv.preciounitario = Convert.ToDecimal(items["P_Unit"]);
                datadv.Descripcion = items["Producto"].ToString();
                datadv.Codigo = items["Codigo"].ToString();
                detalle.Add(datadv);
            }
            var funcion = new EmitirComprobante();
            int resultado = 0;
            resultado = funcion.EmitirNotaDebito(parametrosVentas, detalle);
            if (resultado == 1)
            {

                ConfirmarEnvioSunat();
                Dispose();
            }
            else
            {

                MessageBox.Show("Ocurrio problemas de validacion");
                Dispose();
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
        private void ConfirmarEnvioSunat()
        {
            var funcion = new Dnotasdebito();
            var parametros = new Lnotasdebito();
            parametros.idventa = idventa;
            funcion.ConfirmarSunatNd(parametros);
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
