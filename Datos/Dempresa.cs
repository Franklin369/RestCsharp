using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using RestCsharp.Logica;
using Sunat.Logica;

namespace RestCsharp.Datos
{
    public class Dempresa
    {
        public bool Insertar_Empresa(Lempresa parametros)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("insertar_Empresa", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RazonSocial", parametros.RazonSocial);
                cmd.Parameters.AddWithValue("@logo", parametros.Logo);
                cmd.Parameters.AddWithValue("@Impuesto", parametros.Impuesto);
                cmd.Parameters.AddWithValue("@Porcentaje_impuesto", parametros.Porcentaje_impuesto);
                cmd.Parameters.AddWithValue("@SimboloMoneda", parametros.SimboloMoneda);
                cmd.Parameters.AddWithValue("@Trabajas_con_impuestos", parametros.Trabajas_con_impuestos);
                cmd.Parameters.AddWithValue("@Tiponotas", parametros.Tiponotas);
                cmd.Parameters.AddWithValue("@Carpeta_para_copias_de_seguridad", parametros.Carpeta_para_copias_de_seguridad);
                cmd.Parameters.AddWithValue("@Correo_para_envio_de_reportes", parametros.Correo_para_envio_de_reportes);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
                return false;
            }
            finally
            {
                CONEXIONMAESTRA.cerrar();
            }
        }
        #region Copias de seguridad
        public bool editarRespaldos()
        {
            try
            {

                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("editarRespaldos", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Ultima_fecha_de_copia_date", DateTime.Now);
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
        public void GenerarCopiaBd(string Base_De_datos, string SubCarpeta)
        {
            try
            {
                string v_nombre_respaldo = Base_De_datos + ".bak";
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("BACKUP DATABASE " + Base_De_datos + " TO DISK = '" + SubCarpeta + @"\" + v_nombre_respaldo + "'", CONEXIONMAESTRA.conectar);
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {


            }
            finally
            {
                CONEXIONMAESTRA.cerrar();
            }

        }
        #endregion
        public void mostrar_empresa(ref DataTable dt)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_Empresa", CONEXIONMAESTRA.conectar);
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
        public bool EditarEmpresa(Lempresa parametros)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("editar_Empresa", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Razonsocial", parametros.RazonSocial);
                cmd.Parameters.AddWithValue("@logo", parametros.Logo);
                cmd.Parameters.AddWithValue("@Impuesto", parametros.Impuesto);
                cmd.Parameters.AddWithValue("@Porcentaje_impuesto", parametros.Porcentaje_impuesto);
                cmd.Parameters.AddWithValue("@SimboloMoneda", parametros.SimboloMoneda);
                cmd.Parameters.AddWithValue("@Trabajas_con_impuestos", parametros.Trabajas_con_impuestos);
                cmd.Parameters.AddWithValue("@Tiponotas", parametros.Tiponotas);
                cmd.Parameters.AddWithValue("@Carpeta_para_copias_de_seguridad", parametros.Carpeta_para_copias_de_seguridad);
                cmd.Parameters.AddWithValue("@Correo_para_envio_de_reportes", parametros.Correo_para_envio_de_reportes);
                cmd.Parameters.AddWithValue("@ConectarSunat", parametros.ConectarSunat);
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
        public bool editarSunatEmpresa(Lempresa parametros)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("editarSunatEmpresa", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Ruc", parametros.Ruc);
                cmd.Parameters.AddWithValue("@DireccionFiscal", parametros.DireccionFiscal);
                cmd.Parameters.AddWithValue("@Ubigeo", parametros.Ubigeo);
                cmd.Parameters.AddWithValue("@Servidor", parametros.Servidor);
                cmd.Parameters.AddWithValue("@CarpetaCertificado", parametros.CarpetaCertificado);
                cmd.Parameters.AddWithValue("@Passcertificado", parametros.Passcertificado);
                cmd.Parameters.AddWithValue("@UserSecundario", parametros.UserSecundario);
                cmd.Parameters.AddWithValue("@PassSecundario", parametros.PassSecundario);
                cmd.Parameters.AddWithValue("@Codmoneda", parametros.CodMoneda);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
                return false;
            }
            finally
            {
                CONEXIONMAESTRA.cerrar();
            }
        }
    }
}
