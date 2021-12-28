using RestCsharp.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace RestCsharp.Datos
{
    public class Dlicencias
    {
        DateTime fechaFinal;
        DateTime FechaInicial;
        string estado;
        string SerialPcLicencia;
        DateTime fechaSistema = DateTime.Now;
        string SerialPC;
        string ruta;
        string dbcnString;
        string LicenciaDescifrada;
        private AES aes = new AES();
        string FechaFinLicencia;
        string EstadoLicencia;
        string NombreSoftwareLicencia;
        public bool ValidarLicencias(ref string Resultado)
        {

            try
            {
                Bases.Obtener_serialPC(ref SerialPC);
                DataTable dt = new DataTable();
                CONEXIONMAESTRA.abrir();
                SqlDataAdapter da = new SqlDataAdapter("Select * From Marcan", CONEXIONMAESTRA.conectar);
                da.Fill(dt);
                CONEXIONMAESTRA.cerrar();
                fechaSistema.ToString("yyyy-MM-dd");
                foreach (DataRow rdr in dt.Rows)
                {
                    FechaInicial = Convert.ToDateTime(Bases.Desencriptar(rdr["FA"].ToString()));
                    estado = Bases.Desencriptar(rdr["E"].ToString());
                    fechaFinal = Convert.ToDateTime(Bases.Desencriptar(rdr["F"].ToString()));
                    SerialPcLicencia = rdr["S"].ToString();
                }

                if (estado == "VENCIDA")
                {
                    return false;
                }
                else
                {
                    if (fechaFinal >= fechaSistema)
                    {


                        if (FechaInicial <= fechaSistema)
                        {
                            if (SerialPcLicencia == SerialPC)
                            {
                                if (estado == "?ACTIVO?")
                                {
                                    Resultado = "Licencia de prueba activa hasta el " + fechaFinal.ToString("dd/MM/yyyy");
                                }
                                else if (estado == "?ACTIVADO PRO?")
                                {
                                    Resultado = "Licencia PROFESIONAL activa hasta el " + fechaFinal.ToString("dd/MM/yyyy");
                                }
                                return true;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }
        public void InsertarLicencia()
        {
            try
            {
                string fechafinal;
                string fechaactual;
                string estado;
                Bases.Obtener_serialPC(ref SerialPC);
                DateTime fechaactualdate = DateTime.Now;
                DateTime fechaFinaldate = fechaactualdate.AddDays(30);
                //Encriptar
                fechafinal = Bases.Encriptar(fechaFinaldate.ToString("yyyy-MM-dd"));
                fechaactual = Bases.Encriptar(fechaactualdate.ToString("yyyy-MM-dd"));
                estado = Bases.Encriptar("?ACTIVO?");

                CONEXIONMAESTRA.abrir();

                SqlCommand cmd = new SqlCommand("Insertar_marcan", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@s", SerialPC);
                cmd.Parameters.AddWithValue("@f", fechafinal);
                cmd.Parameters.AddWithValue("@e", estado);
                cmd.Parameters.AddWithValue("@fa", fechaactual);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CONEXIONMAESTRA.cerrar();
            }
        }
        public bool ActivarLicencia()
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "Licencias Bumam|*.xml";
                dlg.Title = "Cargador de Licencias Bumam";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    //Descifrar Licencia
                    Bases.Obtener_serialPC(ref SerialPC);
                    ruta = Path.GetFullPath(dlg.FileName);
                    XmlDocument doc = new XmlDocument();
                    doc.Load(ruta);
                    XmlElement root = doc.DocumentElement;
                    dbcnString = root.Attributes.Item(0).Value;
                    LicenciaDescifrada = (aes.Decrypt(dbcnString, Desencryptacion.appPwdUnique, int.Parse("256")));
                    string cadena = LicenciaDescifrada;
                    string[] separadas = cadena.Split('|');
                    SerialPcLicencia = separadas[1];
                    FechaFinLicencia = separadas[2];
                    EstadoLicencia = separadas[3];
                    NombreSoftwareLicencia = separadas[4];
                    if (NombreSoftwareLicencia == "Bumam")
                    {
                        if (EstadoLicencia == "PENDIENTE")
                        {
                            if (SerialPcLicencia == SerialPC)
                            {
                                string fechaFin = Bases.Encriptar(FechaFinLicencia);
                                string estado = Bases.Encriptar("?ACTIVADO PRO?");
                                string fechaActivacion = Bases.Encriptar(DateTime.Now.ToString("yyyy-MM-dd"));
                                var parametros = new Lmarcan();

                                parametros.E = estado;
                                parametros.FA = fechaActivacion;
                                parametros.F = fechaFin;
                                parametros.S = SerialPC;
                                if (editarMarcan(parametros) == true)
                                {
                                    MessageBox.Show("Licencia activada, se cerrara el sistema para un nuevo Inicio");
                                    Application.Exit();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }
            return false;

        }
        public bool editarMarcan(Lmarcan parametros)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("editarMarcan", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@e", parametros.E);
                cmd.Parameters.AddWithValue("@fa", parametros.FA);
                cmd.Parameters.AddWithValue("@f", parametros.F);
                cmd.Parameters.AddWithValue("@s", parametros.S);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                CONEXIONMAESTRA.cerrar();
            }
        }
      
    }
}
