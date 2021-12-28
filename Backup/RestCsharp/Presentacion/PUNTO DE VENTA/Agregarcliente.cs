using RestCsharp.Datos;
using Sunat.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestCsharp.Presentacion.Ventas
{
    public partial class Agregarcliente : UserControl
    {
        public Agregarcliente()
        {
            InitializeComponent();
        }
        string tipoDoc;
        private void BtnVolver_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtnombrecliente.Text))
            {
                rellenarCamposVacios();
                insertar();
            }
            else
            {
                MessageBox.Show("Ingrese un nombre", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        private void rellenarCamposVacios()
        {
            if (string.IsNullOrEmpty(txtcelular.Text)) { txtcelular.Text = "-"; };
            if (string.IsNullOrEmpty(txtdirecciondefactura.Text)) { txtdirecciondefactura.Text = "-"; };
            if (string.IsNullOrEmpty(txtnroDoc.Text)) { txtnroDoc.Text = "-"; };

        }
        private void validarTipodoc()
        {
            if (Rdni.Checked == true)
            {
                tipoDoc = "DNI";
            }
            if (RRuc.Checked == true)
            {
                tipoDoc = "RUC";
            }
        }
        private void insertar()
        {
            validarTipodoc();
            Lclientes parametros = new Lclientes();
            var funcion = new Dclientes();
            parametros.Nombre = txtnombrecliente.Text;
            parametros.Nrodoc = txtnroDoc.Text;
            parametros.Celular = txtcelular.Text;
            parametros.Direccion = txtdirecciondefactura.Text;
            parametros.Tipodoc = tipoDoc;
            if (funcion.insertar_clientes(parametros) == true)
            {
                Dispose();
            }

        }

        private void Agregarcliente_Load(object sender, EventArgs e)
        {

        }
    }
}
