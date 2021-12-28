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
using System.Drawing.Printing;
using RestCsharp.Presentacion.Reportes;
namespace RestCsharp.Presentacion.Impresoras
{
    public partial class ImpresorasConf : Form
    {
        public ImpresorasConf()
        {
            InitializeComponent();
        }
        int Idarea;
        string codigoArea;
        string Impresora;
        DataTable dtImpresorasxArea;
        private void ImpresorasConf_Load(object sender, EventArgs e)
        {
            panel_Bienvenida.Visible = true;
            panel_Bienvenida.Dock = DockStyle.Fill;
            dibujarAreas();
        }

        private void dibujarAreas()
        {
            PanelAreas.Controls.Clear();

            var dt = new DataTable();
            Dareasimpresion funcion = new Dareasimpresion();
            funcion.mostrar_AreasImpresion(ref dt);
            foreach (DataRow rdr in dt.Rows)
            {
                Button b = new Button();
                Panel panelC1 = new Panel();
                Panel panelLATERAL = new Panel();
                b.Text = rdr["Area"].ToString();
                b.Name = rdr["Id_area"].ToString();
                b.Tag = rdr["Codigo"].ToString();
                b.Dock = DockStyle.Fill;
                b.BackColor = Color.Transparent;
                b.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                b.FlatStyle = FlatStyle.Flat;
                b.FlatAppearance.BorderSize = 0;
                b.FlatAppearance.MouseDownBackColor = Color.FromArgb(64, 64, 64);
                b.FlatAppearance.MouseOverBackColor = Color.FromArgb(43, 43, 43);
                b.TextAlign = ContentAlignment.MiddleLeft;
                b.ForeColor = Color.White;

                panelC1.Size = new Size(244, 58);
                panelC1.Name = rdr["Id_area"].ToString();

                panelLATERAL.Size = new Size(3, 58);
                panelLATERAL.Dock = DockStyle.Left;
                panelLATERAL.BackColor = Color.Transparent;
                panelLATERAL.Name = rdr["Id_area"].ToString();

                panelC1.Controls.Add(b);
                panelC1.Controls.Add(panelLATERAL);
                PanelAreas.Controls.Add(panelC1);
                b.BringToFront();
                panelLATERAL.SendToBack();
                b.Click += B_Click;

            }


        }

        private void B_Click(object sender, EventArgs e)
        {
            Idarea = Convert.ToInt32(((Button)sender).Name);
            codigoArea = (((Button)sender).Tag).ToString();
            panel_Bienvenida.Visible = false;
            panel_Bienvenida.Dock = DockStyle.None;
            PanelImpresoras.Visible = true;
            PanelImpresoras.Dock = DockStyle.Fill;
            mostrarImpresorasxArea();
            dibujar_impresoras();
            foreach (Control panelC2 in PanelAreas.Controls)
            {
                if (panelC2 is System.Windows.Forms.Panel)
                {
                    foreach (Control panelLATERAL2 in panelC2.Controls)
                    {
                        if (panelLATERAL2 is System.Windows.Forms.Panel)
                        {
                            panelLATERAL2.BackColor = Color.Transparent;
                            panelC2.BackColor = Color.Transparent;
                            break;

                        }
                    }
                }
            }
            foreach (Control panelC1 in PanelAreas.Controls)
            {
                if (panelC1 is System.Windows.Forms.Panel)
                {

                    foreach (Control panelLATERAL in panelC1.Controls)
                    {
                        if (panelLATERAL is System.Windows.Forms.Panel)
                        {
                            if (panelLATERAL.Name == Idarea.ToString())
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
        private void mostrarImpresorasxArea()
        {
            var funcion = new Dimpresoras();
            var parametros = new Limpresoras();
            parametros.Id_Areas_de_Impresion = Idarea;
            dtImpresorasxArea = new DataTable();
            funcion.mostrarImpresorasArea(ref dtImpresorasxArea, parametros);
        }

        private void dibujar_impresoras()
        {
            PanelImpresoras.Controls.Clear();
            foreach (var I in PrinterSettings.InstalledPrinters)
            {
                var b = new Button();
                var panel = new Panel();
                var a = new Button();
                //
                b.Text = I.ToString();
                b.Name = I.ToString();
                b.Dock = DockStyle.Fill;
                b.Font = new Font("Microsoft Sans Serif", 10);
                b.BackColor = Color.FromArgb(27, 27, 27);
                b.FlatStyle = FlatStyle.Flat;
                b.FlatAppearance.BorderSize = 0;
                b.ForeColor = Color.White;
                b.Cursor = Cursors.Hand;

                panel.Size = new Size(160, 158);
                panel.BackColor = Color.Transparent;

                //boton para pruebas
                a.Text = "Probar";
                a.Name = I.ToString();
                a.Dock = DockStyle.Bottom;
                a.Font = new Font("Microsoft Sans Serif", 10);
                a.BackColor = Color.OrangeRed;
                a.ForeColor = Color.White;
                a.FlatStyle = FlatStyle.Flat;
                a.FlatAppearance.BorderSize = 0;
                a.Cursor = Cursors.Hand;
                //
                try
                {
                    foreach (DataRow row in dtImpresorasxArea.Rows)
                    {
                        string impresora = row["Impresora"].ToString();
                        if(impresora ==b.Text)
                        {
                            b.BackColor = Color.OrangeRed;
                            panel.Controls.Add(a);
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }

                panel.Controls.Add(b);
                PanelImpresoras.Controls.Add(panel);
                b.BringToFront();
                b.Click += B_Click1;
                a.Click += A_Click;
            }
        }

        private void A_Click(object sender, EventArgs e)
        {
            Impresora= (((Button)sender).Name).ToString();
            ProbarImpresora();
        }
        private void ProbarImpresora()
        {
            var rpt = new Pruebaimpresora();
            rptPruebas.Report = rpt;
            rptPruebas.RefreshReport();
            var funcionReporte = new Dimpresoras();
            funcionReporte.ProbarImpresoras(rptPruebas.ReportSource, Impresora);  
        }

        private void B_Click1(object sender, EventArgs e)
        {
            try
            {
                Impresora = ((Button)sender).Name;
                foreach(Control PanelC1 in PanelImpresoras.Controls)
                {
                    if(PanelC1 is Panel)
                    {
                        foreach (Control boton in PanelC1.Controls)
                        {
                            if(boton is Button)
                            {
                                if(boton.Name==Impresora)
                                {
                                    boton.BackColor = Color.OrangeRed;
                                    break;
                                }
                            }
                        }
                    }
                }
                Insertar_Impresoras_por_Area();
                mostrarImpresorasxArea();
                dibujar_impresoras();
            }
            catch (Exception)
            {

                throw;
            }

        }
        private void Insertar_Impresoras_por_Area()
        {
            var funcion = new Dimpresoras();
            var parametros = new Limpresoras();
            parametros.Id_Areas_de_Impresion = Idarea;
            parametros.Impresora = Impresora;
            if (funcion.InsertarImpresorasArea(parametros) == false)
            {
                eliminarImpresoraArea();
            }
        }
        private void eliminarImpresoraArea()
        {
            var funcion = new Dimpresoras();
            var parametros = new Limpresoras();
            parametros.Id_Areas_de_Impresion = Idarea;
            parametros.Impresora = Impresora;
            funcion.eliminarImporesorasArea(parametros);
        }

    }
}
