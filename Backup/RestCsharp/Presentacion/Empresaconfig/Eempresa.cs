using RestCsharp.Datos;
using RestCsharp.Logica;
using Sunat.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RestCsharp.Presentacion.Empresaconfig
{
    public partial class Eempresa : Form
    {
        public Eempresa()
        {
            InitializeComponent();
        }
        string venderImpuesto;
        string notas;
        string Conectarsunat;
        string Servidor;
        string Codigoubigeo;
        int PorcImpuesto;
        string Trabajasconimpuestos;
        string Modobusqueda;
        private void Eempresa_Load(object sender, EventArgs e)
        {
            //  ObtenerDatos();
            Cargarubigeos();
            mostrarEmpresa();

        }
        private void mostrarEmpresa()
        {
            var funcion = new Dempresa();
            var dt = new DataTable();
            funcion.mostrar_empresa(ref dt);
            txtrazonsocial.Text = dt.Rows[0][15].ToString();
            ImagenEmpresa.BackgroundImage = null;
            byte[] b = (Byte[])dt.Rows[0][1];
            MemoryStream ms = new MemoryStream(b);
            ImagenEmpresa.Image = Image.FromStream(ms);
            txtmoneda.Text = dt.Rows[0][4].ToString();
            Trabajasconimpuestos = dt.Rows[0][5].ToString();
            if (Trabajasconimpuestos == "SI")
            {
                si.Checked = true;
                panelImpuestos.Visible = true;
            }
            if (Trabajasconimpuestos == "NO")
            {
                panelImpuestos.Visible = false;
                no.Checked = true;
            }
            txtporcentaje.Text = dt.Rows[0][3].ToString();
            txtimpuesto.Text = dt.Rows[0][2].ToString();
            txtRuta.Text = dt.Rows[0][7].ToString();

            Conectarsunat = dt.Rows[0][18].ToString();
            txtdireccionfiscal.Text = dt.Rows[0][16].ToString();
            if (Conectarsunat == "SI")
            {
                checConectarsunat.Checked = true;
                panelSunat.Visible = true;
            }
            Codigoubigeo = dt.Rows[0][17].ToString();
            txtRuc.Text = dt.Rows[0][14].ToString();
            txtCertificado.Text = dt.Rows[0][20].ToString();
            txtContraseñaCert.Text = dt.Rows[0][21].ToString();
            txtUsuarioSunat.Text = dt.Rows[0][22].ToString();
            txtContraseñaSunat.Text = dt.Rows[0][23].ToString();
            txtCodigoMoneda.Text = dt.Rows[0][24].ToString();
            Servidor = dt.Rows[0][19].ToString();

            if (Servidor.Contains("beta"))
            {
                cbxServidor.Text = "Pruebas";
            }
            else
            {
                cbxServidor.Text = "Produccion";
            }
            notas = dt.Rows[0][6].ToString();
            validarNotas();
            ObtenerUbicaionXubigeo();
        }
        private void Cargarubigeos()
        {
            var funcion = new Dcodigosubigeo();
            var dt = new DataTable();
            funcion.mostrarCodigosubigeo(ref dt);
            txtdepartamento.DisplayMember = "Departamento";
            txtdepartamento.DataSource = dt;
            txtprovincia.DisplayMember = "Provincia";
            txtprovincia.DataSource = dt;
            txtdistrito.DisplayMember = "Distrito";
            txtdistrito.DataSource = dt;
        }
        private void ObtenerDatos()
        {
            var funcion = new Dempresa();
            var dt = new DataTable();
            funcion.mostrar_empresa(ref dt);
            foreach (DataRow hobbit in dt.Rows)
            {
                txtrazonsocial.Text = dt.Rows[0][15].ToString();
                ImagenEmpresa.BackgroundImage = null;
                byte[] b = (Byte[])dt.Rows[0][1];
                MemoryStream ms = new MemoryStream(b);
                ImagenEmpresa.Image = Image.FromStream(ms);
                txtmoneda.Text = dt.Rows[0][4].ToString();
                venderImpuesto = dt.Rows[0][0].ToString();
                validarImpuestos();
                notas = hobbit["Tiponotas"].ToString();
                validarNotas();
                txtporcentaje.Text = dt.Rows[0][3].ToString();
                txtimpuesto.Text = dt.Rows[0][2].ToString();

                txtRuta.Text = dt.Rows[0][7].ToString();

                Conectarsunat = dt.Rows[0][18].ToString();
                txtdireccionfiscal.Text = dt.Rows[0][16].ToString();
                if (Conectarsunat == "SI")
                {
                    checConectarsunat.Checked = true;
                    panelSunat.Visible = true;
                }
                Codigoubigeo = dt.Rows[0][17].ToString();
                txtRuc.Text = dt.Rows[0][14].ToString();
                txtCertificado.Text = dt.Rows[0][20].ToString();
                txtContraseñaCert.Text = dt.Rows[0][21].ToString();
                txtUsuarioSunat.Text = dt.Rows[0][22].ToString();
                txtContraseñaSunat.Text = dt.Rows[0][23].ToString();
                Servidor = dt.Rows[0][19].ToString();
                if (Servidor.Contains("beta"))
                {
                    cbxServidor.Text = "Pruebas";
                }
                else
                {
                    cbxServidor.Text = "Produccion";
                }
                ObtenerUbicaionXubigeo();
            }

            //var dt = new DataTable();
            //var funcion = new Dempresa();
            //funcion.mostrarempresa(ref dt);
            //foreach(DataRow hobbit in dt.Rows)
            //{
            //    txtrazonsocial.Text = hobbit["Nombre_Empresa"].ToString();
            //    txtpais.Text = hobbit["Pais"].ToString();
            //    venderImpuesto = hobbit["Trabajas_con_impuestos"].ToString();
            //    validarImpuestos();
            //    txtimpuesto.Text = hobbit["Impuesto"].ToString();
            //    txtporcentaje.Text = hobbit["Porcentaje_impuesto"].ToString();
            //    notas = hobbit["Tiponotas"].ToString();
            //    validarNotas();
            //    txtRuta.Text = hobbit["Carpeta_para_copias_de_seguridad"].ToString();
            //    ImagenEmpresa.BackgroundImage = null;
            //    byte[] b = (byte[])(hobbit["Logo"]);
            //    MemoryStream ms = new MemoryStream(b);
            //    ImagenEmpresa.Image = Image.FromStream(ms);
            //}
        }
        private void ObtenerUbicaionXubigeo()
        {
            var funcion = new Dcodigosubigeo();
            var parametros = new Lcodigosubigeos();
            var dt = new DataTable();
            parametros.Ubigeo = Codigoubigeo;
            funcion.ObtenerUbicaionXubigeo(ref dt, parametros);
            txtdepartamento.Text = dt.Rows[0][0].ToString();
            txtprovincia.Text = dt.Rows[0][1].ToString();
            txtdistrito.Text = dt.Rows[0][2].ToString();

        }
        private void validarNotas()
        {
            if (notas == "General")
            {
                chGeneral.Checked = true;
            }
            else
            {
                chPorpedido.Checked = true;
            }
        }

        private void validarImpuestos()
        {
            if (venderImpuesto == "SI")
            {
                panelImpuestos.Visible = true;
                si.Checked = true;
            }
            else
            {
                panelImpuestos.Visible = false;
                no.Checked = true;
            }
        }

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



        private void si_CheckedChanged(object sender, EventArgs e)
        {
            panelImpuestos.Visible = true;
        }

        private void no_CheckedChanged(object sender, EventArgs e)
        {
            panelImpuestos.Visible = false;
        }

        private void chGeneral_CheckedChanged(object sender, EventArgs e)
        {
            notas = "General";
        }

        private void chPorpedido_CheckedChanged(object sender, EventArgs e)
        {
            notas = "Porpedido";
        }

        private void btnsiguiente_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtrazonsocial.Text))
            {
                EditarEmpresa();
            }
            else
            {
                MessageBox.Show("Ingrese un Nombre de Empresa", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void EditarEmpresa()
        {
            ValidarConexionsunat();
            ValidarImpuestos();
            var funcion = new Dempresa();
            var parametros = new Lempresa();
            parametros.RazonSocial = txtrazonsocial.Text;
            parametros.Impuesto = txtimpuesto.Text;
            parametros.Porcentaje_impuesto = PorcImpuesto;
            parametros.SimboloMoneda = txtmoneda.Text;
            parametros.Trabajas_con_impuestos = Trabajasconimpuestos;
            MemoryStream ms = new MemoryStream();
            ImagenEmpresa.Image.Save(ms, ImagenEmpresa.Image.RawFormat);
            parametros.Logo = ms.GetBuffer();
            parametros.Carpeta_para_copias_de_seguridad = txtRuta.Text;
            parametros.Correo_para_envio_de_reportes = "-";
            parametros.ConectarSunat = Conectarsunat;
            validarNotas();
            parametros.Tiponotas = notas;
            funcion.EditarEmpresa(parametros);
            MessageBox.Show("Cambios guardados", "Guardando Cambios", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Dispose();
            //var funcion = new Dempresa();
            //var parametros = new Lempresa();
            //parametros.Nombre_Empresa = txtempresa.Text;
            //MemoryStream ms = new MemoryStream();
            //ImagenEmpresa.Image.Save(ms, ImagenEmpresa.Image.RawFormat);
            //parametros.Logo = ms.GetBuffer();
            //parametros.Impuesto = txtimpuesto.Text;
            //parametros.Porcentaje_impuesto = Convert.ToDouble(txtporcentaje.Text);
            //parametros.Moneda = txtmoneda.Text;
            //if (si.Checked == true)
            //{
            //    parametros.Trabajas_con_impuestos = "SI";
            //}
            //if (no.Checked == true)
            //{
            //    parametros.Trabajas_con_impuestos = "NO";
            //}
            //parametros.Carpeta_para_copias_de_seguridad = txtRuta.Text;
            //parametros.Pais = txtpais.Text;
            //validarNotas();
            //parametros.Tiponotas = notas;
            //if (funcion.editarEmpresa(parametros) == true)
            //{
            //    MessageBox.Show("Registros guardados");
            //    Dispose();
            //}




        }
        private void ValidarConexionsunat()
        {
            if (checConectarsunat.Checked == true)
            {
                Conectarsunat = "SI";
                Validarsunat();
            }
            else
            {
                Conectarsunat = "NO";
            }
        }
        private void ValidarImpuestos()
        {
            if (si.Checked == true)
            {
                PorcImpuesto = Convert.ToInt32(txtporcentaje.Text);
                Trabajasconimpuestos = "SI";
            }
            else if (no.Checked == true)
            {
                PorcImpuesto = 0;
                Trabajasconimpuestos = "NO";
            }
        }
        private void Validarsunat()
        {
            ObtenercodigoUbigeo();
            Validarservidor();
            var funcion = new Dempresa();
            var parametros = new Lempresa();

            parametros.Ruc = txtRuc.Text;
            parametros.DireccionFiscal = txtdireccionfiscal.Text;
            parametros.Ubigeo = Codigoubigeo;
            parametros.Servidor = Servidor;
            parametros.CarpetaCertificado = txtCertificado.Text;
            parametros.Passcertificado = txtContraseñaCert.Text;
            parametros.UserSecundario = txtUsuarioSunat.Text;
            parametros.PassSecundario = txtContraseñaSunat.Text;
            parametros.CodMoneda = txtCodigoMoneda.Text;
            funcion.editarSunatEmpresa(parametros);
        }
        private void Validarservidor()
        {
            if (cbxServidor.Text == "Pruebas")
            {
                Servidor = "https://e-beta.sunat.gob.pe/ol-ti-itcpfegem-beta/billService";
            }
            else
            {
                Servidor = "https://e-factura.sunat.gob.pe/ol-ti-itcpfegem/billService";
            }
        }
        private void ObtenercodigoUbigeo()
        {
            var funcion = new Dcodigosubigeo();
            var parametros = new Lcodigosubigeos();
            parametros.Departamento = txtdepartamento.Text;
            parametros.Provincia = txtprovincia.Text;
            parametros.Distrito = txtdistrito.Text;
            funcion.ObtenercodigoUbigeo(ref Codigoubigeo, parametros);
        }

        private void checConectarsunat_CheckedChanged(object sender, EventArgs e)
        {
            if (checConectarsunat.Checked == true)
            {
                panelSunat.Visible = true;
            }
            else
            {
                panelSunat.Visible = false;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Obtenerutacertificado();
        }
        private void Obtenerutacertificado()
        {
            var dalg = new OpenFileDialog();
            dalg.InitialDirectory = "";
            dalg.Filter = "Certificado Sunat|*.pfx";
            dalg.FilterIndex = 2;
            dalg.Title = "Cargador de certificado digital";
            if (dalg.ShowDialog() == DialogResult.OK)
            {
                txtCertificado.Text = Path.GetFullPath(dalg.FileName);
            }
        }
    }
}
