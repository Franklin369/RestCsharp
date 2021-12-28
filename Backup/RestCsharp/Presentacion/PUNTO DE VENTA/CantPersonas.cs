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
    public partial class CantPersonas : UserControl
    {
        public delegate void ButtonClick(object sender, EventArgs e);
        public event ButtonClick OnButtonClick;
        public CantPersonas()
        {
            InitializeComponent();
            btnAceptar.Click += new EventHandler((sender, args) =>
            {
                OnButtonClick?.Invoke(this, null);
            });
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Punto_de_venta.cantidadPersonas = 1;
            Dispose();
          
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtnumero.Text += "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtnumero.Text += "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtnumero.Text += "3";

        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtnumero.Text += "4";

        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtnumero.Text += "5";

        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtnumero.Text += "6";

        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtnumero.Text += "7";

        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtnumero.Text += "8";

        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtnumero.Text += "9";

        }

        private void btn0_Click(object sender, EventArgs e)
        {
            txtnumero.Text += "0";

        }

        private void btnborrar_Click(object sender, EventArgs e)
        {
            txtnumero.Clear();

        }

        private void btnborrarderecha_Click(object sender, EventArgs e)
        {
            int contador;
            contador = txtnumero.Text.Count();
            if (contador > 0)
            {
                txtnumero.Text = txtnumero.Text.Substring(0, txtnumero.Text.Count() - 1);
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtnumero.Text))
            {
                Punto_de_venta.cantidadPersonas =Convert.ToInt32 ( txtnumero.Text);
                Dispose();
            }
            else
            {
                MessageBox.Show("Valor vacio", "Indica la cantidad de personas");
            }
        }

        private void CantPersonas_Load(object sender, EventArgs e)
        {
           // FormBorderStyle = FormBorderStyle.None;
        }
    }
}
