using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using RestCsharp.Logica;

namespace RestCsharp.Datos
{
   public   class Dticketventa
    {
        public void mostrarformatoTicket(ref DataTable dt)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlDataAdapter da = new SqlDataAdapter("select * from Ticket", CONEXIONMAESTRA.conectar);
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
        public bool Insertar_Ticket()
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("Insertar_Ticket", CONEXIONMAESTRA.conectar);
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
        public bool Editarticket(Lticket parametros)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("editarFormatoticket", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Identificador_fiscal", parametros.Identificador_fiscal);
                cmd.Parameters.AddWithValue("@Direccion", parametros.Direccion);
                cmd.Parameters.AddWithValue("@Provincia_Departamento_Pais", parametros.Provincia_Departamento_Pais);
                cmd.Parameters.AddWithValue("@Nombre_de_Moneda", parametros.Nombre_de_Moneda);
                cmd.Parameters.AddWithValue("@Agradecimiento", parametros.Agradecimiento);
                cmd.Parameters.AddWithValue("@pagina_Web_Facebook", parametros.pagina_Web_Facebook);
                cmd.Parameters.AddWithValue("@Anuncio", parametros.Anuncio);
                cmd.Parameters.AddWithValue("@Datos_fiscales_de_autorizacion", parametros.Datos_fiscales_de_autorizacion);
                cmd.Parameters.AddWithValue("@Por_defecto", parametros.Por_defecto);
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
