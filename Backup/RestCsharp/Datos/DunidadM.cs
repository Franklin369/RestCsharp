using Sunat.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestCsharp.Datos
{
   public class DunidadM
    {
        public bool InsertarUmedida()
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("InsertarUmedida", CONEXIONMAESTRA.conectar);
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
        public void mostrarUndm( ref DataTable dt)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                var cmd = new SqlDataAdapter("Select * from UnidadesMedida", CONEXIONMAESTRA.conectar);
                cmd.Fill(dt);               
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
        public void mostrarUdmXcod(ref DataTable dt,LunidadM parametros)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                var cmd = new SqlDataAdapter("mostrarUdmXcod", CONEXIONMAESTRA.conectar);
                cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmd.SelectCommand.Parameters.AddWithValue("@codigo", parametros.Codigo);
                cmd.Fill(dt);
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
