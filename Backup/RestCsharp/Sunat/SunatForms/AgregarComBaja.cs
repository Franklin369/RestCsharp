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

namespace RestCsharp.Presentacion.SunatForms
{
    public partial class AgregarComBaja : UserControl
    {
        public AgregarComBaja()
        {
            InitializeComponent();
        }
        string serieRef;
        string correlativoRef;
        int idventa;
        string codigoComprobanteRef;
        string motivo;
        Panel panelcomprobante = new Panel();
        int idcomprobanteNc;
        string CodTipoNcredito;
        string resultado = "";

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
                if (codigoComprobanteRef != "03")
                {
                    EmitirCombaja();
                }
                else
                {
                    MessageBox.Show("No se acepta la comunicación de baja de boletas");
                }
            }
            else
            {
                MessageBox.Show("Ingrese un motivo");
            }
        }
        private void InsertarBaja()
        {
            var funcion = new Dcombaja();
            var parametros = new Lcombaja();
            parametros.Idventa = idventa;
            parametros.Ticket = resultado;
            parametros.Estadosunat = "Pendiente";
            parametros.codigo = "98";
            if (funcion.InsertarCombaja(parametros) == true)
            {
                //EmitirCombaja();
                MessageBox.Show("Documento enviado, Ticket para consulta del estado " + resultado);
                Dispose();
            }
        }
        private void EmitirCombaja()
        {

            var funcion = new EmitirComprobante();
            resultado = funcion.EmitirComBaja(codigoComprobanteRef, serieRef, correlativoRef, txtmotivo.Text);
            if (resultado != "")
            {
                InsertarBaja();
            }
            else
            {

                MessageBox.Show("Ocurrio problemas de validacion");
                Dispose();
            }
        }
        private void ConfirmarEnvioSunat()
        {
            var funcion = new Dnotascredito();
            var parametros = new Lnotacredito();
            parametros.idventa = idventa;
            funcion.ConfirmarSunatNc(parametros);
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void AgregarComBaja_Load(object sender, EventArgs e)
        {

        }
    }
}
