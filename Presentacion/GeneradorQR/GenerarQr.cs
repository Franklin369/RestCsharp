using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RestCsharp.Logica;
using RestCsharp.Datos;
using RestCsharp.Presentacion.Reportes;
namespace RestCsharp.Presentacion.GeneradorQR
{
    public partial class GenerarQr : UserControl
    {
        public GenerarQr()
        {
            InitializeComponent();
        }
        string Ip;
        private void GenerarQr_Load(object sender, EventArgs e)
        {
            ObtenerIp();
        }
        private void MostrarCodigosQR()
        {
            var dt = new DataTable();
            var funcion = new Dmesas();
            funcion.RptcodigosQR(ref dt,txtIp.Text);
            var rpt = new Rcodigosqr();
            rpt.DataSource = dt;
            reportViewer1.Visible = true;
            reportViewer1.Report = rpt;
            reportViewer1.RefreshReport();

        }
        private void ObtenerIp()
        {
            Bases.ObtenerIp(ref Ip);
            txtIp.Text = Ip;
        }

        private void BtnGenerar_Click(object sender, EventArgs e)
        {
            MostrarCodigosQR();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
