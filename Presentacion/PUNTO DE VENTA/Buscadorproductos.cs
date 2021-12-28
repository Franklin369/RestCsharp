using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RestCsharp.Presentacion.PUNTO_DE_VENTA
{
    public partial class Buscadorproductos : UserControl
    {
        public Buscadorproductos()
        {
            InitializeComponent();
        }
        char[] alfabeto;
        char[] numeros;
        public static int idventa;
        public static string nota;
        private void Buscadorproductos_Load(object sender, EventArgs e)
        {
            agregarNumeros();
            agregarLetras();
        }
        private void agregarNumeros()
        {
            numeros = "0123456789".ToCharArray();
            foreach (char numer in numeros)
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
                btnnumero.Size = new Size(55, 55);
                btnnumero.ForeColor = Color.White;
                btnnumero.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                PanelNumeros.Controls.Add(btnnumero);
                btnnumero.Click += Btnnumero_Click;

            }
        }
        private void agregarLetras()
        {
            alfabeto = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ".ToCharArray();
            foreach (char Letra in alfabeto)
            {

                Button btnletra = new Button();
                btnletra.Text = Letra.ToString();
                btnletra.BackgroundImage = Properties.Resources.naranja;
                btnletra.BackgroundImageLayout = ImageLayout.Stretch;
                btnletra.BackColor = Color.Transparent;
                btnletra.FlatStyle = FlatStyle.Flat;
                btnletra.FlatAppearance.BorderSize = 0;
                btnletra.FlatAppearance.MouseDownBackColor = Color.Transparent;
                btnletra.FlatAppearance.MouseOverBackColor = Color.Transparent;
                btnletra.Size = new Size(55, 55);
                btnletra.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                PanelLetras.Controls.Add(btnletra);
                btnletra.Click += Btnletra_Click;
            }


        }
        private void Btnletra_Click(object sender, EventArgs e)
        {
            var letra = ((Button)sender).Text;
            txtnota.Text += letra;
        }

        private void Btnnumero_Click(object sender, EventArgs e)
        {
            var numero = ((Button)sender).Text;
            txtnota.Text += numero;
        }

        private void btnespacio_Click(object sender, EventArgs e)
        {
            Punto_de_venta._Puerta.Mostrarpaneles();
            Dispose();
        }
    }
}
