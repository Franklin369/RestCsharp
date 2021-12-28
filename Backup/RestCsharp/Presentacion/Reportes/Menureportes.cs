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

namespace RestCsharp.Presentacion.Reportes
{
    public partial class Menureportes : Form
    {
        public Menureportes()
        {
            InitializeComponent();
        }
        int Idusuario;
        private void btnVentas_Click(object sender, EventArgs e)
        {
            panelVentas.Visible = true;
            panelVentas.Dock = DockStyle.Fill;
            panelProductos.Visible = false;
            panelCondicionales.Enabled = false;
            chekFiltros.Checked = false;
            PanelFiltros.Visible = false;
        }
        private void ReporteResumenVentasHoy()
        {
            var dt = new DataTable();
            var funcion = new Dventas();
            funcion.RptresumenventasHoy(ref dt);
            var rpt = new Rresumenventas();
            rpt.DataSource = dt;
            rpt.table1.DataSource = dt;
            reportViewer1.Report = rpt;
            reportViewer1.RefreshReport();
        }

        private void chekFiltros_CheckedChanged(object sender, EventArgs e)
        {
            if (chekFiltros.Checked == true)
            {
                if (PResumenVentas.Visible == true)
                {
                    RptresumenventasFechas();
                }
                if (PVentasPorempleado.Visible == true)
                {
                    ReporteResumenVentasEmpleadoFechas();
                }
                btnHoy.ForeColor = Color.DimGray;
                PanelFiltros.Visible = true;
                TFILTROS.ForeColor = Color.OrangeRed;

            }
            else
            {
                if (PResumenVentas.Visible == true)
                {
                    ReporteResumenVentasHoy();
                }
                if (PVentasPorempleado.Visible == true)
                {
                    ReporteResumenVentasHoyEmpleado();
                }
                btnHoy.ForeColor = Color.OrangeRed;
                PanelFiltros.Visible = false;
                TFILTROS.ForeColor = Color.DimGray;
            }
        }
        private void ReporteResumenVentasEmpleadoFechas()
        {
            var dt = new DataTable();
            var parametros = new Lventas();
            var funcion = new Dventas();
            parametros.Id_usuario = Idusuario;
            funcion.RptresumenventasFechasUsuarios(ref dt, TXTFI.Value, TXTFF.Value, parametros);
            var rpt = new Rresumenventas();
            rpt.table1.DataSource = dt;
            rpt.DataSource = dt;
            reportViewer1.Report = rpt;
            reportViewer1.RefreshReport();

        }
        private void RptresumenventasFechas()
        {
            var dt = new DataTable();
            var funcion = new Dventas();
            funcion.RptresumenventasFechas(ref dt, TXTFI.Value, TXTFF.Value);
            var rpt = new Rresumenventas();
            rpt.DataSource = dt;
            rpt.table1.DataSource = dt;
            reportViewer1.Report = rpt;
            reportViewer1.RefreshReport();
        }
        private void btnResumenVentas_Click(object sender, EventArgs e)
        {
            panelCondicionales.Enabled = true;
            PResumenVentas.Visible = true;
            PVentasPorempleado.Visible = false;
            btnHoy.ForeColor = Color.OrangeRed;
            PanelEmpleado.Visible = false;
            chekFiltros.Checked = false;
            PanelFiltros.Visible = false;
            TFILTROS.ForeColor = Color.DimGray;
            ReporteResumenVentasHoy();
        }

        private void TXTFI_ValueChanged(object sender, EventArgs e)
        {
            validarFiltros();
        }
        private void validarFiltros()
        {
            if (chekFiltros.Checked == true)
            {
                if (PResumenVentas.Visible == true)
                {
                    RptresumenventasFechas();
                }
                if (PVentasPorempleado.Visible == true)
                {
                    ReporteResumenVentasEmpleadoFechas();
                }
            }

        }

        private void TXTFF_ValueChanged(object sender, EventArgs e)
        {
            validarFiltros();
        }

        private void btnEmpleado_Click(object sender, EventArgs e)
        {
            panelCondicionales.Enabled = true;
            btnResumenVentas.ForeColor = Color.DimGray;
            btnEmpleado.ForeColor = Color.OrangeRed;
            PResumenVentas.Visible = false;
            PVentasPorempleado.Visible = true;
            btnHoy.ForeColor = Color.OrangeRed;
            chekFiltros.Checked = false;
            PanelFiltros.Visible = false;
            TFILTROS.ForeColor = Color.DimGray;
            PanelEmpleado.Visible = true;
            mostrarUsuarios();
        }
        private void mostrarUsuarios()
        {
            var dt = new DataTable();
            var funcion = new Dusuarios();
            funcion.mostrar_Usuarios(ref dt);
            txtEmpleado.DisplayMember = "Nombre";
            txtEmpleado.ValueMember = "IdUsuario";
            txtEmpleado.DataSource = dt;
        }
        private void ReporteResumenVentasHoyEmpleado()
        {
            var dt = new DataTable();
            var funcion = new Dventas();
            var parametros = new Lventas();

            parametros.Id_usuario = Idusuario;
            funcion.RptresumenventasHoyUsuario(ref dt, parametros);

            var rpt = new Rresumenventas();
            rpt.table1.DataSource = dt;
            rpt.DataSource = dt;
            reportViewer1.Report = rpt;
            reportViewer1.RefreshReport();
        }

        private void txtEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            Idusuario = Convert.ToInt32(txtEmpleado.SelectedValue);
            if (chekFiltros.Checked == true)
            {
                ReporteResumenVentasEmpleadoFechas();
            }
            else
            {
                ReporteResumenVentasHoyEmpleado();
            }
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            panelVentas.Visible = false;
            panelProductos.Visible = true;
            panelProductos.Dock = DockStyle.Fill;
            ReporteProductosmasV();
        }
        private void ReporteProductosmasV()
        {
            var funcion = new Ddetalleventas();
            var dt = new DataTable();
            funcion.RptproductosmasV(ref dt);
            var rpt = new Rproductosmasv();
            rpt.table1.DataSource = dt;
            rpt.DataSource = dt;
            reportViewer2.Report = rpt;
            reportViewer2.RefreshReport();
        }

        private void Menureportes_Load(object sender, EventArgs e)
        {
            Actualizarfechas();

        }
        private void Actualizarfechas()
        {
            TXTFI.Value = DateTime.Now;
            TXTFF.Value = DateTime.Now;
        }
    }
}
