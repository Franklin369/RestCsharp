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
namespace RestCsharp.Presentacion.Diseñocomp
{
    public partial class Diseño : Form
    {
        public Diseño()
        {
            InitializeComponent();
        }
        string tipo;
        int idformato;
        private void btnTicket_Click(object sender, EventArgs e)
        {
            tipo = "Ticket No Fiscal";
            btnTicket.BackColor = Color.FromArgb(255, 204, 1);
            btnFacturaBoleta.BackColor = Color.WhiteSmoke;
            txtAutorizacion_fiscal.Visible = false;
            txtAutorizacion_fiscal.Text = "-";

        }

        private void btnFacturaBoleta_Click(object sender, EventArgs e)
        {
            tipo = "Factura-Boleta";
            btnFacturaBoleta.BackColor = Color.FromArgb(255, 204, 1);
            btnTicket.BackColor = Color.WhiteSmoke;
            txtAutorizacion_fiscal.Visible = true;

        }

        private void Diseño_Load(object sender, EventArgs e)
        {
            mostrarformatoTicket();
        }
        private void mostrarformatoTicket()
        {
            var funcion = new Dticketventa();
            var dt = new DataTable();
            funcion.mostrarformatoTicket(ref dt);
            idformato = Convert.ToInt32(dt.Rows[0][0]);
            txtEmpresa_RUC.Text = dt.Rows[0][2].ToString();
            txtDireccion.Text = dt.Rows[0][3].ToString();
            txtProvincia_departamento.Text = dt.Rows[0][4].ToString();
            txtMoneda_String.Text = dt.Rows[0][5].ToString();
            txtAgradecimiento.Text = dt.Rows[0][6].ToString();
            txtpagina_o_facebook.Text = dt.Rows[0][7].ToString();
            TXTANUNCIO.Text = dt.Rows[0][8].ToString();
            txtAutorizacion_fiscal.Text = dt.Rows[0][9].ToString();
            tipo= dt.Rows[0][10].ToString();
            if(tipo=="Ticket No Fiscal")
            {
                btnTicket.BackColor = Color.FromArgb(255, 204, 1);
                btnFacturaBoleta.BackColor = Color.WhiteSmoke;
                txtAutorizacion_fiscal.Visible = false;
                txtAutorizacion_fiscal.Text = "-";
            }
            else
            {
                btnFacturaBoleta.BackColor = Color.FromArgb(255, 204, 1);
                btnTicket.BackColor = Color.WhiteSmoke;
                txtAutorizacion_fiscal.Visible = true;
            }
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            EditarTicket();
        }
        private void EditarTicket()
        {
            var funcion = new Dticketventa();
            var parametros = new Lticket();
            parametros.Identificador_fiscal = txtEmpresa_RUC.Text;
            parametros.Direccion = txtDireccion.Text;
            parametros.Provincia_Departamento_Pais = txtProvincia_departamento.Text;
            parametros.Nombre_de_Moneda = txtMoneda_String.Text;
            parametros.Agradecimiento = txtAgradecimiento.Text;
            parametros.pagina_Web_Facebook = txtpagina_o_facebook.Text;
            parametros.Anuncio = TXTANUNCIO.Text;
            if (tipo == "Ticket No Fiscal")
            {
                parametros.Datos_fiscales_de_autorizacion = "-";

            }
            else
            {
                parametros.Datos_fiscales_de_autorizacion = txtAutorizacion_fiscal.Text;
            }
            parametros.Por_defecto = tipo;
            funcion.Editarticket(parametros);
            MessageBox.Show("Datos actualizados correctamente");


        }
    }
}
