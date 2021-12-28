using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestCsharp.Logica;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace RestCsharp.Datos
{
   public class Dgrupos
    {
        public bool editarGrupoProd(Lgruposprodc parametros)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("editarGrupoProd", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Grupo", parametros.Grupo);
                cmd.Parameters.AddWithValue("@Idcolor", parametros.Idcolor);
                cmd.Parameters.AddWithValue("@Idgrupo", parametros.Idline);
                cmd.Parameters.AddWithValue("@icono", parametros.Icono);
                cmd.Parameters.AddWithValue("@estadoicono", parametros.Estado_de_icono);

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
        public bool eliminarGrupos(Lgruposprodc parametros)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("eliminarGrupos", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Idgrupo", parametros.Idline);
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
        public bool restaurarGrupos(Lgruposprodc parametros)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("restaurarGrupos", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Idgrupo", parametros.Idline);
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
        public void mostrarColorxgrupo(ref DataTable dt, Lgruposprodc parametros)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrarColorxGrupo", CONEXIONMAESTRA.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Idgrupo", parametros.Idline);
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
    }
}
