using RestCsharp.Datos;
using RestCsharp.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RestCsharp.Presentacion.PUNTO_DE_VENTA
{
    public partial class Cobrar : Form
    {
        public Cobrar()
        {
            InitializeComponent();
        }
        public static double total;
        double vuelto = 0;
        double efectivo_calculado = 0;
        double restante = 0;
        double efectivo = 0;
        double tarjeta = 0;
        string Tipopago;
        private void Cobrar_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            panelPrincipal.Location = new Point((Width - panelPrincipal.Width) / 2, (Height - panelPrincipal.Height) / 2);
            lbltotal.Text = total.ToString("N");
            calcular_restante();
        }

        void calcular_restante()
        {
            try
            {


                if (txtefectivo.Text == "")
                {
                    efectivo = 0;
                }
                else
                {
                    efectivo = Convert.ToDouble(txtefectivo.Text);
                }

                if (txttarjeta.Text == "")
                {
                    tarjeta = 0;
                }
                else
                {
                    tarjeta = Convert.ToDouble(txttarjeta.Text);
                }

                if (txtefectivo.Text == "0.00")
                {
                    efectivo = 0;
                }

                if (txttarjeta.Text == "0.00")
                {
                    tarjeta = 0;

                }

                if (txtefectivo.Text == ".")
                {
                    efectivo = 0;
                }


                try
                {
                    if (efectivo > total)
                    {
                        efectivo_calculado = efectivo - (total + tarjeta);
                        if (efectivo_calculado < 0)
                        {
                            vuelto = 0;
                            TXTVUELTO.Text = "0";
                            txtrestante.Text = Convert.ToString(efectivo_calculado);
                            restante = efectivo_calculado;
                        }
                        else
                        {
                            vuelto = efectivo - (total - tarjeta);
                            TXTVUELTO.Text = Convert.ToString(vuelto);
                            restante = efectivo - (total + tarjeta + efectivo_calculado);
                            txtrestante.Text = Convert.ToString(restante);
                            txtrestante.Text = decimal.Parse(txtrestante.Text).ToString("##0.00");
                        }

                    }
                    else
                    {
                        vuelto = 0;
                        TXTVUELTO.Text = "0";
                        efectivo_calculado = efectivo;
                        restante = total - efectivo_calculado - tarjeta;
                        txtrestante.Text = Convert.ToString(restante);
                        txtrestante.Text = decimal.Parse(txtrestante.Text).ToString("##0.00");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void txtefectivo_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtefectivo_TextChanged(object sender, EventArgs e)
        {
            calcular_restante();
        }

        private void txttarjeta_TextChanged(object sender, EventArgs e)
        {
            calcular_restante();
        }
        void ObtenerTipopago()
        {
            int indicadorEfectivo = 4;
            int indicadorTarjeta = 3;

            // validacion para evitar valores vacios
            if (txtefectivo.Text == "")
            {
                txtefectivo.Text = "0";
            }
            if (txttarjeta.Text == "")
            {
                txttarjeta.Text = "0";
            }

            //validacion de .
            if (txtefectivo.Text == ".")
            {
                txtefectivo.Text = "0";
            }
            if (txttarjeta.Text == ".")
            {
                txttarjeta.Text = "0";
            }

            //validacion de 0
            if (txtefectivo.Text == "0")
            {
                indicadorEfectivo = 0;
            }
            if (txttarjeta.Text == "0")
            {
                indicadorTarjeta = 0;
            }

            //calculo de indicador
            int calculo_identificacion = indicadorEfectivo + indicadorTarjeta;
            //consulta al identificador
            if (calculo_identificacion == 4)
            {
                Tipopago = "EFECTIVO";
            }
            if (calculo_identificacion == 3)
            {
                Tipopago = "TARJETA";
            }
            if (calculo_identificacion > 4)
            {
                Tipopago = "MIXTO";
            }
        }
        private void btnGuardarImprimirdirecto_Click(object sender, EventArgs e)
        {
            ObtenerTipopago();
            Confirmarventa();
        }
        private void Confirmarventa()
        {
           
        }

    }
}
