using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Data.SqlClient;
namespace RestCsharp.Presentacion
{
    public partial class CONEXION_MANUAL : Form
    {
        private Logica.AES aes = new Logica.AES();
        int idtabla;
        public CONEXION_MANUAL()
        {
            InitializeComponent();
        }
        public void SavetoXML(Object dbcnString)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("ConnectionString.xml");
            XmlElement root = doc.DocumentElement;
            root.Attributes[0].Value = Convert.ToString(dbcnString);
            XmlTextWriter writer = new XmlTextWriter("ConnectionString.xml", null);
            writer.Formatting = Formatting.Indented;
            doc.Save(writer);
            writer.Close();
        }
        string dbcnString;
        public void ReadfromXML()
        {
        try
            {

                XmlDocument doc = new XmlDocument();
                doc.Load("ConnectionString.xml");
                XmlElement root = doc.DocumentElement;
                dbcnString = root.Attributes[0].Value;
                txtCnString.Text = (aes.Decrypt(dbcnString, Logica.Desencryptacion.appPwdUnique, int.Parse("256")));
            }
            catch (System.Security.Cryptography.CryptographicException ex)
            {

            }
        }
        private void CONEXION_MANUAL_Load(object sender, EventArgs e)
        {
            ReadfromXML();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            comprobar_conexion();
        }
        private void comprobar_conexion()
        {
                SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = txtCnString.Text;
                SqlCommand com = new SqlCommand("select * from SALON", con);
                con.Open();
                idtabla =Convert.ToInt32(com.ExecuteScalar());
                con.Close();
                 SavetoXML(aes.Encrypt(txtCnString.Text, Logica.Desencryptacion.appPwdUnique, int.Parse("256")));
                MessageBox.Show("Coneccion realizada correctamente", "Conexion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();

            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show("Sin conexion", "Conexion fallida", MessageBoxButtons.OK, MessageBoxIcon.Error);
   
            }
        }
    }
}
