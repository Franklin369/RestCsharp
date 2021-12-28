using RestCsharp.Datos;
using RestCsharp.Logica;
using Sunat.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace RestCsharp.Presentacion.Copiasbd
{
    public partial class GenerarAut : UserControl
    {
        public GenerarAut()
        {
            InitializeComponent();
        }
        string txtsoftware = "buman";
        string Base_De_datos = "BASEBRIRESTCSHARP";
        int contador = 10;
        private Thread Hilo;
        private bool acaba = false;
        private void BtnCerrar_turno_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void GenerarAut_Load(object sender, EventArgs e)
        {
            mostrarempresa();
            timerContador.Start();
        }

        private void timerContador_Tick(object sender, EventArgs e)
        {
            contador -= 1;
            lbltiempo.Text = contador.ToString();
            if (contador == 0)
            {
                contador = 0;
                timerContador.Stop();
                generarCopia();

            }
        }
        private void generarCopia()
        {
          
            try
            {

                string miCarpeta = "Copias_de_Seguridad_de_" + txtsoftware;
                if (!System.IO.Directory.Exists(txtRuta.Text + miCarpeta))

                {
                    System.IO.Directory.CreateDirectory(txtRuta.Text + miCarpeta);
                }
                string ruta_completa = txtRuta.Text + miCarpeta;
                string SubCarpeta = ruta_completa + @"\Respaldo_al_" + DateTime.Now.Day + "_" + (DateTime.Now.Month) + "_" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute;
                try
                {
                    System.IO.Directory.CreateDirectory(System.IO.Path.Combine(ruta_completa, SubCarpeta));

                }
                catch (Exception)
                {


                }
                try
                {
                    var funcion = new Dempresa();
                    funcion.GenerarCopiaBd(Base_De_datos, SubCarpeta);
                    editarRespaldos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No agregaste una ruta");
            }
        }
      
       
        private void mostrarempresa()
        {
            var dt = new DataTable();

            Dempresa funcion = new Dempresa();
            funcion.mostrar_empresa(ref dt);
            txtRuta.Text = dt.Rows[0][7].ToString();
            
        }
      

        private void editarRespaldos()
        {
            Lempresa parametros = new Lempresa();
            Dempresa funcion = new Dempresa();

            if (funcion.editarRespaldos() == true)
            {
                MessageBox.Show("Copia de seguridad generada en: " + txtRuta.Text);
                Application.Exit();
            }
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
