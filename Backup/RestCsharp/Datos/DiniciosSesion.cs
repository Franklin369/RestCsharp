using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RestCsharp.Logica;
using System.Windows.Forms;
namespace RestCsharp.Datos
{
  public   class DiniciosSesion
    {
        int idcaja;
        public void mostrarInicioSesion(ref int idsesion)
        {
            try
            {
                Dcaja funcion = new Dcaja();
                funcion.mostrarCajaSerial(ref idcaja);
                CONEXIONMAESTRA.abrir();
                SqlCommand da = new SqlCommand("mostrarInicioSesion", CONEXIONMAESTRA.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@idcaja", idcaja);
                idsesion = Convert.ToInt32(da.ExecuteScalar());
            }
            catch (Exception ex)
            {
                idsesion = 0;
            }
            finally
            {
                CONEXIONMAESTRA.cerrar();
            }
        }
        public bool insertarInicioSesion(LiniciosSesion parametros)
        {
            try
            {
                Dcaja funcion = new Dcaja();
                funcion.mostrarCajaSerial(ref idcaja);
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("insertarInicioSesion", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idcaja", idcaja);
                cmd.Parameters.AddWithValue("@idusuario", parametros.IdUsuario);
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
        public bool editarInicioSesion(LiniciosSesion parametros)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("editarInicioSesion", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idsesion", parametros.Idsesion);
                cmd.Parameters.AddWithValue("@idusuario", parametros.IdUsuario);
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
        public void mostrarInicioSesionTable(ref DataTable dt)
        {
            try
            {
                Dcaja funcion = new Dcaja();
                funcion.mostrarCajaSerial(ref idcaja);
                CONEXIONMAESTRA.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrarInicioSesion", CONEXIONMAESTRA.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idcaja", idcaja);
                da.Fill(dt);
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
    }
}
