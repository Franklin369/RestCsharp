using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using RestCsharp.Datos;
using RestCsharp.Logica;

namespace RestCsharp.Presentacion.Mesas_salones
{
    public partial class Salones : UserControl
    {
        public delegate void ButtonClick(object sender, EventArgs e);
        public event ButtonClick OnButtonClick;
        public Salones()
        {
            InitializeComponent();
            btnguardar.Click += new EventHandler((sender, args) =>
            {
                OnButtonClick?.Invoke(this, null);
            });
            btnvolver.Click += new EventHandler((sender, args) =>
            {
                OnButtonClick?.Invoke(this, null);
            });
            btncerrar.Click += new EventHandler((sender, args) =>
            {
                OnButtonClick?.Invoke(this, null);
            });
        }
        public static int idsalon;
        public static string salon;
        public static string proceso;

        private void Salones_Load(object sender, EventArgs e)
        {
            if (proceso == "EDICION")
            {
                txtSalonedicion.Text = salon;
            }


            txtSalonedicion.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (proceso == "NUEVO")
            {
                insertar_salon();
            }
            else
            {
                Editarsalon();
            }

        }
        private void Editarsalon()
        {
            var funcion = new Dsalon();
            var parametros = new Lsalon();
            parametros.Id_salon = idsalon;
            parametros.Salon = txtSalonedicion.Text;
            funcion.editarSalon(parametros);
            Dispose();
        }
        private void insertar_mesas_vacias()
        {
            for (int i = 1; i <= 80; i++)
            {
                try
                {
                    CONEXIONMAESTRA.abrir();
                    SqlCommand cmd = new SqlCommand("insertar_mesa", CONEXIONMAESTRA.conectar);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@mesa", "NULO");
                    cmd.Parameters.AddWithValue("@idsalon", idsalon);
                    cmd.ExecuteNonQuery();
                    CONEXIONMAESTRA.cerrar();
                }
                catch (Exception ex)
                {
                    CONEXIONMAESTRA.cerrar();
                    MessageBox.Show(ex.StackTrace);
                }
            }
        }
        private void mostrar_id_salon_recien_ingresado()
        {
            SqlCommand com = new SqlCommand("mostrar_id_salon_recien_ingresado", CONEXIONMAESTRA.conectar);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Salon", txtSalonedicion.Text);
            try
            {
                CONEXIONMAESTRA.abrir();
                idsalon = Convert.ToInt32(com.ExecuteScalar());
                CONEXIONMAESTRA.cerrar();
            }
            catch (Exception ex)
            {
                CONEXIONMAESTRA.cerrar();
                MessageBox.Show(ex.StackTrace);
            }
        }
        private void insertar_salon()
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("insertar_Salon", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Salon", txtSalonedicion.Text);
                cmd.ExecuteNonQuery();
                CONEXIONMAESTRA.cerrar();
                mostrar_id_salon_recien_ingresado();
                insertar_mesas_vacias();
                Dispose();
            }
            catch (Exception ex)
            {

                CONEXIONMAESTRA.conectar.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnvolver_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
