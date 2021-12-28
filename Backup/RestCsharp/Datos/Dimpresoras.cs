using RestCsharp.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.Reporting.Processing;

namespace RestCsharp.Datos
{
    public class Dimpresoras
    {
        int idcaja;
        private void Mostraridcaja()
        {
            var funcion =new Dcaja();
            funcion.mostrarCajaSerial(ref idcaja);
        }
        public void mostrarImpresorasArea(ref DataTable dt, Limpresoras parametros)
        {
            try
            {
                Mostraridcaja();
                CONEXIONMAESTRA.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrarImpresorasArea", CONEXIONMAESTRA.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idarea", parametros.Id_Areas_de_Impresion);
                da.SelectCommand.Parameters.AddWithValue("@Idcaja", idcaja);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
            finally
            {
                CONEXIONMAESTRA.cerrar();
            }
        }
        public bool InsertarImpresorasArea(Limpresoras parametros)
        {
            try
            {
                Mostraridcaja();
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("InsertarImpresorasArea", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_areas_de_impresion", parametros.Id_Areas_de_Impresion);
                cmd.Parameters.AddWithValue("@Impresora", parametros.Impresora);
                cmd.Parameters.AddWithValue("@Idcaja", idcaja);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                CONEXIONMAESTRA.cerrar();
            }
        }
        public bool eliminarImporesorasArea(Limpresoras parametros)
        {
            try
            {
                Mostraridcaja();
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("eliminarImpresorasXarea", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Idarea", parametros.Id_Areas_de_Impresion);
                cmd.Parameters.AddWithValue("@Impresora", parametros.Impresora);
                cmd.Parameters.AddWithValue("@Idcaja", idcaja);
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
        public void mostrarImpresorasAreaCod(ref DataTable dt, Lareasimpresion parametros)
        {
            try
            {
                Mostraridcaja();
                CONEXIONMAESTRA.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrarImpresorasAreaCod", CONEXIONMAESTRA.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@CodigoArea", parametros.Codigo);
                da.SelectCommand.Parameters.AddWithValue ("@Idcaja",idcaja);

                da.Fill(dt);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
            finally
            {
                CONEXIONMAESTRA.cerrar();
            }
        }
        public void ProbarImpresoras(Telerik.Reporting.ReportSource reporte, string Impresora)
        {
            var documento = new PrintDocument();
            documento.PrinterSettings.PrinterName = Impresora;
            if (documento.PrinterSettings.IsValid)
            {
                var configuracionImpresora = new PrinterSettings();
                configuracionImpresora.PrinterName = Impresora;
                var procesoReporte = new ReportProcessor();
                procesoReporte.PrintReport(reporte, configuracionImpresora);
            }

        }
        public void ImprimirVarios(Telerik.Reporting.ReportSource reporte, string Codigo)
        {
            var funcion = new Dimpresoras();
            var dt = new DataTable();
            var parametros = new Lareasimpresion();
            parametros.Codigo = Codigo;
            funcion.mostrarImpresorasAreaCod(ref dt, parametros);
            int contador = 0;
            contador = dt.Rows.Count;
            string Impresora;
            if (contador > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Impresora = row["Impresora"].ToString();
                    var documento = new PrintDocument();
                    documento.PrinterSettings.PrinterName = Impresora;
                    if (documento.PrinterSettings.IsValid)
                    {
                        var configuracionImpresora = new PrinterSettings();
                        configuracionImpresora.PrinterName = Impresora;
                        var procesoReporte = new ReportProcessor();
                        procesoReporte.PrintReport(reporte, configuracionImpresora);
                    }
                }

            }
            //else
            //{
            //    MessageBox.Show("No hay impresoras agregadas");
            //}


        }
    }
}
