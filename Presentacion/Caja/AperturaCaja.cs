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
using RestCsharp.Presentacion.PUNTO_DE_VENTA;
namespace RestCsharp.Presentacion.Caja
{
    public partial class AperturaCaja : UserControl
    {
        public AperturaCaja()
        {
            InitializeComponent();
        }
        char[] numeros;
        private void BtnIniciar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtmonto.Text))
            {
                EditarDineroInicial();       
            }
            else
            {
                Pasaraventas();
            }


          
        }
        private void EditarDineroInicial()
        {
            var funcion = new DmovimientoCaja();
            var parametros = new LmovientosCaja();
            parametros.EfectivoInicial =Convert.ToDouble( txtmonto.Text);
           if( funcion.EditarDineroInicial(parametros)==true)
            {
                Pasaraventas();
            }
        }
        private void Pasaraventas()
        {
            Dispose();
            var frm = new Visor_de_mesas();
          //  frm.ShowDialog();
        }

        private void AperturaCaja_Load(object sender, EventArgs e)
        {
            PanelCaja.Location = new Point((Width-PanelCaja.Width)/2, (Height-PanelCaja.Height)/2);
            AgregarNumeros();
        }
        private void AgregarNumeros()
        {
            numeros = "1234567890".ToCharArray();
            foreach(char numer in numeros)
            {
                Button btnnumero = new Button();
                btnnumero.Text = numer.ToString();
                btnnumero.BackgroundImage = Properties.Resources.negro;
                btnnumero.BackgroundImageLayout = ImageLayout.Stretch;
                btnnumero.BackColor = Color.Transparent;
                btnnumero.FlatStyle = FlatStyle.Flat;
                btnnumero.FlatAppearance.BorderSize = 0;
                btnnumero.FlatAppearance.MouseDownBackColor = Color.Transparent;
                btnnumero.FlatAppearance.MouseOverBackColor = Color.Transparent;
                btnnumero.Size = new Size(70, 70);
                btnnumero.ForeColor = Color.White;
                btnnumero.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                Panelbotones.Controls.Add(btnnumero);
                btnnumero.Click += Btnnumero_Click;

            }
            Button btnBorrar = new Button();
            btnBorrar.Text = "Borrar";
            btnBorrar.BackgroundImage = Properties.Resources.Rojo;
            btnBorrar.BackgroundImageLayout = ImageLayout.Stretch;
            btnBorrar.BackColor = Color.Transparent;
            btnBorrar.FlatStyle = FlatStyle.Flat;
            btnBorrar.FlatAppearance.BorderSize = 0;
            btnBorrar.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnBorrar.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnBorrar.Size = new Size(70, 70);
            btnBorrar.ForeColor = Color.White;
            btnBorrar.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
            Panelbotones.Controls.Add(btnBorrar);
            btnBorrar.Click += BtnBorrar_Click;
        }

        private void BtnBorrar_Click(object sender, EventArgs e)
        {
            txtmonto.Clear();
        }

        private void Btnnumero_Click(object sender, EventArgs e)
        {
            txtmonto.Text += ((Button)sender).Text;
        }

        private void BtnOmitir_Click(object sender, EventArgs e)
        {
            Pasaraventas();
        }

        private void txtmonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Separador_de_Numeros(txtmonto, e);
        }
    }
}
