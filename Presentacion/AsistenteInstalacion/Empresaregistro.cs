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
using System.IO;
using Sunat.Logica;

namespace RestCsharp.Presentacion.AsistenteInstalacion
{
    public partial class Empresaregistro : Form
    {
        public Empresaregistro()
        {
            InitializeComponent();
        }
        string notas;
        private void lbleditarLogo_Click(object sender, EventArgs e)
        {
            dlg.InitialDirectory = "";
            dlg.Filter = "Imagenes|*.jpg;*.png";
            dlg.FilterIndex = 2;
            dlg.Title = "Cargador de Imagenes BUMAM";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ImagenEmpresa.BackgroundImage = null;
                ImagenEmpresa.Image = new Bitmap(dlg.FileName);
            }

        }

        private void txtpais_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtmoneda.SelectedIndex = txtpais.SelectedIndex;
        }

        private void si_CheckedChanged(object sender, EventArgs e)
        {
            panelImpuestos.Visible = true;
        }

        private void no_CheckedChanged(object sender, EventArgs e)
        {
            panelImpuestos.Visible = false;

        }

        private void Label9_Click(object sender, EventArgs e)
        {
            ObtenerRuta();
        }
        private void ObtenerRuta()
        {
            if (Fbd.ShowDialog() == DialogResult.OK)
            {
                txtRuta.Text = Fbd.SelectedPath;
            }
        }

        private void ToolStripButton22_Click(object sender, EventArgs e)
        {
            ObtenerRuta();

        }

        private void btnsiguiente_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtrazonsocial.Text))
            {         
                    InsertarEmpresa();
            }
            else
            {
                MessageBox.Show("Ingrese un Nombre de Empresa", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void InsertarEmpresa()
        {
          
            var funcion = new Dempresa();
            var parametros = new Lempresa();
            parametros.RazonSocial = txtrazonsocial.Text;
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            ImagenEmpresa.Image.Save(ms, ImagenEmpresa.Image.RawFormat);
            parametros.Logo = ms.GetBuffer();
            parametros.Impuesto = txtimpuesto.Text;
            parametros.Porcentaje_impuesto = Convert.ToDouble(txtporcentaje.Text);
            parametros.SimboloMoneda = txtmoneda.Text;
            if (si.Checked == true)
            {
                parametros.Trabajas_con_impuestos = "SI";
            }
            if (no.Checked == true)
            {
                parametros.Trabajas_con_impuestos = "NO";
            }
            parametros.Carpeta_para_copias_de_seguridad = txtRuta.Text;
            parametros.Correo_para_envio_de_reportes = "-";
            validarNotas();
            parametros.Tiponotas = notas;
            if (funcion.Insertar_Empresa(parametros) == true)
            {
                Dispose();
                var frm = new UsuarioPrincipal();
                frm.ShowDialog();
            }
        }
        private void validarNotas()
        {
            if(chGeneral.Checked==true)
            {
                notas = "General";
            }
            else if(chPorpedido.Checked==true)
            {
                notas = "Porpedido";
            }
        }
        private void Empresaregistro_Load(object sender, EventArgs e)
        {
            CentrarPaneles();
        }
        private void CentrarPaneles()
        {
            PanelEmpresa.Location = new Point((Width -PanelEmpresa.Width)/2, (Height-PanelEmpresa.Height)/2);

        }

        private void chGeneral_CheckedChanged(object sender, EventArgs e)
        {
            notas = "General";
        }

        private void chPorpedido_CheckedChanged(object sender, EventArgs e)
        {
            notas = "Porpedido";
        }
    }
}
