using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Security.Cryptography;
using System.Management;
using System.Net;

namespace RestCsharp.Logica
{
    public class Bases
    {
        public void DiseñoDatagridview(ref DataGridView Listado)
        {
            Listado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            Listado.EnableHeadersVisualStyles = false;
            DataGridViewCellStyle cabecera = new DataGridViewCellStyle();
            cabecera.BackColor = Color.White;
            cabecera.ForeColor = Color.Black;
            cabecera.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            Listado.ColumnHeadersDefaultCellStyle = cabecera;
        }
        public static void Cambiar_idioma_regional()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-Es");
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ",";
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ",";
            System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(4);

        }
        public void DiseñoDatagridviewEliminar(ref DataGridView Listado)
        {
            foreach (DataGridViewRow row in Listado.Rows)
            {
                string estado;
                estado = row.Cells["Estado"].Value.ToString();
                if (estado == "ELIMINADO")
                {
                    row.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Strikeout | FontStyle.Bold);
                    row.DefaultCellStyle.ForeColor = Color.Red;
                }
            }
        }
        public void DiseñoDatagridviewOscuro(ref DataGridView Listado)
        {
            Listado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            Listado.EnableHeadersVisualStyles = false;
            DataGridViewCellStyle cabecera = new DataGridViewCellStyle();
            cabecera.BackColor = Color.FromArgb(49, 49, 49);
            cabecera.ForeColor = Color.White;
            cabecera.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            Listado.ColumnHeadersDefaultCellStyle = cabecera;
        }
        public static TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
        public static MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

        public static string Encriptar(string texto)
        {
            string tempEncriptar = null;
            if (string.IsNullOrEmpty(texto.Trim(' ')))
            {
                tempEncriptar = "";
            }
            else
            {
                des.Key = hashmd5.ComputeHash((new UnicodeEncoding()).GetBytes(Desencryptacion.appPwdUnique));
                des.Mode = CipherMode.ECB;
                ICryptoTransform encrypt = des.CreateEncryptor();
                byte[] buff = UnicodeEncoding.ASCII.GetBytes(texto);
                tempEncriptar = Convert.ToBase64String(encrypt.TransformFinalBlock(buff, 0, buff.Length));
            }
            return tempEncriptar;
        }
        public static string Desencriptar(string texto)
        {
            string tempDesencriptar = null;
            if (string.IsNullOrEmpty(texto.Trim(' ')))
            {
                tempDesencriptar = "";
            }
            else
            {
                des.Key = hashmd5.ComputeHash((new UnicodeEncoding()).GetBytes(Desencryptacion.appPwdUnique));
                des.Mode = CipherMode.ECB;
                ICryptoTransform desencrypta = des.CreateDecryptor();
                byte[] buff = Convert.FromBase64String(texto);
                tempDesencriptar = UnicodeEncoding.ASCII.GetString(desencrypta.TransformFinalBlock(buff, 0, buff.Length));
            }
            return tempDesencriptar;
        }
        public static void Obtener_serialPC(ref string serial)
        {
            ManagementObject serialPC = new ManagementObject("Win32_PhysicalMedia='\\\\.\\PHYSICALDRIVE0'");
            serial = serialPC.Properties["SerialNumber"].Value.ToString();
            serial = Encriptar(serial.Trim());

        }
        public static object Separador_de_Numeros(TextBox CajaTexto, KeyPressEventArgs e)
        {

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-Es");
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ",";
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ",";
            System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(4);


            if ((e.KeyChar == '.') || (e.KeyChar == ','))
            {

                e.KeyChar = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];

            }
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (e.KeyChar == '.' && (~CajaTexto.Text.IndexOf(".")) != 0)
            {
                e.Handled = true;
            }
            else if (e.KeyChar == '.')
            {
                e.Handled = false;
            }
            else if (e.KeyChar == ',')
            {
                e.Handled = false;

            }
            else
            {
                e.Handled = true;

            }
            return null;
        }
        public static object Reemplazarcomas(TextBox CajaTexto, EventArgs e)
        {
            if (CajaTexto.Text.Contains(",") == true)
            {
                CajaTexto.Text.Replace(",", ".");
            }
            return null;
        }
        public static string ObtenerIp(ref string valorIp)
        {
            valorIp = Dns.GetHostEntry(System.Environment.MachineName).AddressList.FirstOrDefault((i) => i.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString();
            return valorIp;
        }
    }
}
