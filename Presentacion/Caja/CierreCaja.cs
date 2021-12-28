using RestCsharp.Datos;
using RestCsharp.Logica;
using RestCsharp.Presentacion.Copiasbd;
using RestCsharp.Presentacion.Reportes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RestCsharp.Presentacion.Caja
{
    public partial class CierreCaja : UserControl
    {
        public CierreCaja()
        {
            InitializeComponent();
        }
        double efectivoInicial = 0;
        double TotalVentasEfectivo = 0;
        double TotalVentasTarjeta = 0;
        double Totalgastosvarios;
        double Totalingresosvarios;
        double TotalCalculadoEfectivo;
        double TotalDiferencia;
        public static string EstadoApagado;
        double Totalventas;
        private void CierreCaja_Load(object sender, EventArgs e)
        {
            MostrarFechaInicial();
            TotalVentasTipoPago();
            TotalGastosvarios();
            TotalIngresosvarios();
            TotalCalculado();
            VentasTotales();
        }
        private void VentasTotales()
        {
            Totalventas = TotalVentasEfectivo + TotalVentasTarjeta;
            lblVentasTotal.Text = Totalventas.ToString();
        }

        private void TotalCalculado()
        {
            TotalCalculadoEfectivo = TotalVentasEfectivo - Totalgastosvarios + Totalingresosvarios+efectivoInicial;
            lbldineroTotalCaja.Text = (TotalCalculadoEfectivo).ToString();
        }
        private void MostrarFechaInicial()
        {
            var funcion = new DmovimientoCaja();
            var dt = new DataTable();
            funcion.MostrarMovimientosCaja(ref dt);
            string fechainicial = dt.Rows[0][2].ToString();
            lbldesdehasta.Text = "Desde " + fechainicial + " hasta " + DateTime.Now;
            efectivoInicial = Convert.ToDouble(dt.Rows[0][3]);
            lblfondodeCaja.Text = efectivoInicial.ToString();
        }
        private void TotalVentasTipoPago()
        {
            var funcion = new Dventas();
            var dt = new DataTable();
            funcion.RptVentasTurno(ref dt);
            string efectivo = dt.Rows[0][0].ToString();
            string tarjeta = dt.Rows[0][1].ToString();
            string credito = dt.Rows[0][2].ToString();

            if (efectivo != "")
            {
                TotalVentasEfectivo = Convert.ToDouble(efectivo);
            }
            if (tarjeta != "")
            {
                TotalVentasTarjeta = Convert.ToDouble(tarjeta);
            }

            lblventasefectivoGeneral.Text = TotalVentasEfectivo.ToString();
            lblventas_Tarjeta.Text = TotalVentasTarjeta.ToString();

        }
        private void TotalGastosvarios()
        {
            var funcion = new Dgastos();
            funcion.RptGastosvarios(ref Totalgastosvarios);
            lblgastos.Text = Totalgastosvarios.ToString();
        }
        private void TotalIngresosvarios()
        {
            var funcion = new Dingresos();
            funcion.RptIngresosVarios(ref Totalingresosvarios);
            lblingresos.Text = Totalingresosvarios.ToString();
        }

        private void txtmonto_TextChanged(object sender, EventArgs e)
        {
            CalcularDiferencia();
        }
        private void CalcularDiferencia()
        {
            try
            {
                TotalDiferencia = Convert.ToDouble(txtmonto.Text) - TotalCalculadoEfectivo;
                lbldiferencia.Text = TotalDiferencia.ToString();
                validacionesCalculo();
            }
            catch (Exception)
            {

            }

        }
        private void validacionesCalculo()
        {
            if (TotalDiferencia == 0)
            {
                lblanuncio.Text = "Genial, Todo esta perfecto";
                lblanuncio.ForeColor = Color.FromArgb(0, 166, 63);
                lbldiferencia.ForeColor = Color.FromArgb(0, 166, 63);
                lblanuncio.Visible = true;

            }
            if (TotalDiferencia < TotalCalculadoEfectivo & TotalDiferencia != 0)
            {
                lblanuncio.Text = "La diferencia sera Registrada en su Turno y se enviara a Gerencia";
                lblanuncio.ForeColor = Color.FromArgb(231, 63, 67);
                lbldiferencia.ForeColor = Color.FromArgb(231, 63, 67);
                lblanuncio.Visible = true;

            }
            if (TotalDiferencia > TotalCalculadoEfectivo)
            {
                lblanuncio.Text = "La diferencia sera Registrada en su Turno y se enviara a Gerencia";
                lblanuncio.ForeColor = Color.FromArgb(231, 63, 67);
                lbldiferencia.ForeColor = Color.FromArgb(231, 63, 67);
                lblanuncio.Visible = true;
            }
        }

        private void BtnCerrar_turno_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtmonto.Text))
            {
                txtmonto.Text = 0.ToString();
                CalcularDiferencia();
                CerrarCaja();
            }
            else
            {
                CerrarCaja();
            }
        }
        private void Imprimircierrecaja()
        {
            var funcion = new DmovimientoCaja();
            var dt = new DataTable();
            funcion.Rptcierrecaja(ref dt);
            //*****
            var rpt = new Rcierrecaja();
            rpt.DataSource = dt;
            rptCierre.Report = rpt;
            rptCierre.RefreshReport();
            var funcionImpresoras = new Dimpresoras();
            funcionImpresoras.ImprimirVarios(rptCierre.ReportSource, "CAJA");
        }
        private void CerrarCaja()
        {
            DmovimientoCaja funcion = new DmovimientoCaja();
            LmovientosCaja parametros = new LmovientosCaja();
            parametros.fechafin = DateTime.Now;
            parametros.ingresos = Totalingresosvarios;
            parametros.egresos = Totalgastosvarios;
            parametros.VEfectivo = TotalVentasEfectivo;
            parametros.VTarjeta = TotalVentasTarjeta;
            parametros.EfectivoCalculado = TotalCalculadoEfectivo;
            parametros.EfectivoReal = Convert.ToDouble(txtmonto.Text);
            parametros.EfectivoDiferencia = TotalDiferencia;
            if (funcion.cerrarCaja(parametros) == true)
            {
                Imprimircierrecaja();
                Apagar();
            }
        }
        private void Apagar()
        {
            this.Controls.Clear();
            BackColor = Color.FromArgb(29, 29, 29);
            var frm = new GenerarAut();
            //frm.Dock = DockStyle.Fill;
            Panel panelcopias = new Panel();
            panelcopias.Size = new Size(489, 550);
            panelcopias.BackColor = Color.Transparent;
            panelcopias.Location = new Point((Width - panelcopias.Width) / 2, (Height - panelcopias.Height) / 2);
            panelcopias.Controls.Add(frm);
            this.Controls.Add(panelcopias);
            frm.Show();
            EstadoApagado = "OFF";
        }

        private void btnvolver_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void txtmonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Separador_de_Numeros(txtmonto, e);
        }
    }
}
