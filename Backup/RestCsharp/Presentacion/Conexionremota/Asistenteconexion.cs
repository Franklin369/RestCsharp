using RestCsharp.Datos;
using RestCsharp.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace RestCsharp.Presentacion.Conexionremota
{
    public partial class Asistenteconexion : Form
    {
        public Asistenteconexion()
        {
            InitializeComponent();
        }
        private string indicador_de_conexion;
        private Logica.AES aes = new Logica.AES();
        private string indicador;
        public string cadena_de_conexion;
        int idcaja;
        string serialpc;
        int numerocaja;
        private void btnconectar_Click(object sender, EventArgs e)
        {
            conectar_manualmente();

        }
        public void conectar_manualmente()
        {
            string IP = txtservidor.Text;
            cadena_de_conexion = "Data Source =" + IP + ";Initial Catalog=BASEBRIRESTCSHARP;Integrated Security=False; User Id=buman;Password=softwarereal";
            mostrar_conexion();
            if (indicador_de_conexion == "HAY CONEXION")
            {
                SavetoXML(aes.Encrypt(cadena_de_conexion, Logica.Desencryptacion.appPwdUnique, int.Parse("256")));
                mostrar_caja_por_serial();
                if (idcaja > 0)
                {
                    Label3.Text = "!Conectado!";
                    MessageBox.Show("Conexion Correcta. Vuelve a Abrir el Sistema", "Conexion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Dispose();
                }
                else
                {
                    InsertarCajasecundaria();
                }
            }
            else
            {
                MessageBox.Show("Sin conexion", "Desconectado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Mostrartipocaja()
        {
           
            SqlConnection conexionManual = new SqlConnection(cadena_de_conexion);
            conexionManual.Open();
            SqlCommand da = new SqlCommand("select max(Id_Caja)+1 from Caja", conexionManual);   
            numerocaja = Convert.ToInt32(da.ExecuteScalar());
            conexionManual.Close();
           
        }
        private void InsertarCajasecundaria()
        {
            try
            {
                Mostrartipocaja();
                Bases.Obtener_serialPC(ref serialpc);
                SqlConnection conexionManual = new SqlConnection(cadena_de_conexion);
                conexionManual.Open();
                SqlCommand da = new SqlCommand("Insertar_caja", conexionManual);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@descripcion", "Caja "+ numerocaja);
                da.Parameters.AddWithValue("@Tema", "Claro");
                da.Parameters.AddWithValue("@Serial_PC", serialpc);
                da.Parameters.AddWithValue("@Tipo", "SECUNDARIA");

                da.ExecuteNonQuery();
                conexionManual.Close();

               

                Label3.Text = "!Conectado!";
                MessageBox.Show("Conexion Correcta. Vuelve a Abrir el Sistema", "Conexion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("InsertarCajasecundaria", ex.Message);
            }



        }
        private void mostrar_caja_por_serial()
        {
           
            try
            {
                DataTable dt = new DataTable();
                Bases.Obtener_serialPC(ref serialpc);
                SqlConnection conexionManual = new SqlConnection(cadena_de_conexion);
                conexionManual.Open();
                SqlCommand da = new SqlCommand("mostrarCajaSerial", conexionManual);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@Serial", serialpc);
              
                idcaja = Convert.ToInt32(da.ExecuteScalar());
                conexionManual.Close();
                indicador_de_conexion = "HAY CONEXION";

            }
            catch (Exception ex)
            {
                idcaja = 0;
                indicador_de_conexion = "NO HAY CONEXION";
                MessageBox.Show("mostrar_caja_por_serial", ex.Message);
            }

        }
        public void mostrar_conexion()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            try
            {
                SqlConnection conexionManual = new SqlConnection(cadena_de_conexion);
                conexionManual.Open();
                da = new SqlDataAdapter("select * from Usuarios", conexionManual);
                da.Fill(dt);
                conexionManual.Close();
                indicador_de_conexion = "HAY CONEXION";

            }
            catch (Exception ex)
            {
                indicador_de_conexion = "NO HAY CONEXION";
            }
        }
        public void SavetoXML(object dbcnString)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("ConnectionString.xml");
            XmlElement root = doc.DocumentElement;
            root.Attributes.Item(0).Value = Convert.ToString(dbcnString);
            XmlTextWriter writer = new XmlTextWriter("ConnectionString.xml", null);
            writer.Formatting = Formatting.Indented;
            doc.Save(writer);
            writer.Close();
        }

        private void Asistenteconexion_Load(object sender, EventArgs e)
        {

        }
    }
}

