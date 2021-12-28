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
    public partial class Agregar_mesa_ok : UserControl
    {
        public delegate void ButtonClick(object sender, EventArgs e);
        public event ButtonClick guardarClick;
        public event ButtonClick cerrarClick;
        public Agregar_mesa_ok()
        {
            InitializeComponent();
            btnguardar.Click += new EventHandler((sender, args) =>
            {
                guardarClick?.Invoke(this, null);
            });
            btnVolver.Click += new EventHandler((sender, args) =>
            {
                cerrarClick?.Invoke(this, null);
            });
            btnCerrar.Click += new EventHandler((sender, args) =>
            {
                cerrarClick?.Invoke(this, null);
            });
        }

        private void Agregar_mesa_ok_Load(object sender, EventArgs e)
        {

            txtmesaedicion.Text = Configurar_mesas_ok.nombre_mesa;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtmesaedicion.Text != "")
            {
                editar_mesa();
            }
        }
        private void editar_mesa()
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("editar_mesa", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mesa", txtmesaedicion.Text);
                cmd.Parameters.AddWithValue("@id_mesa", Configurar_mesas_ok.idmesa);
                cmd.ExecuteNonQuery();
                CONEXIONMAESTRA.cerrar();
                Dispose();
            }
            catch (Exception ex)
            {
                CONEXIONMAESTRA.cerrar();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            eliminarmesa();
        }
        private void eliminarmesa()
        {
            var funcion = new Dmesas();
            var parametros = new Lmesas();
            parametros.Id_mesa = Configurar_mesas_ok.idmesa;
            funcion.Eliminarmesa(parametros);
            Dispose();
        }
    }
}
