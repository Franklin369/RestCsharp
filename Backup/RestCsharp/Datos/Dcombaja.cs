using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sunat.Logica;
using System.Data;
using System.Data.SqlClient;
using RestCsharp.Datos;

namespace RestCsharp.Datos
{
   public  class Dcombaja
    {
        public bool InsertarCombaja(Lcombaja parametros)
        {
            try
            {

                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("InsertarCombaja", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Idventa", parametros.Idventa);
                cmd.Parameters.AddWithValue("@Ticket", parametros.Ticket);
                cmd.Parameters.AddWithValue("@Estadosunat", parametros.Estadosunat);
                cmd.Parameters.AddWithValue("@Codigo", parametros.codigo);

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
        public bool EditarestadoCombaja(Lcombaja parametros)
        {
            try
            {

                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("EditarestadoCombaja", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Estadosunat", parametros.Estadosunat);
                cmd.Parameters.AddWithValue("@Ticket", parametros.Ticket);
                cmd.Parameters.AddWithValue("@Codigorespta", parametros.codigo);
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
        public void buscarComBaja(ref DataTable dt, string buscador)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrarComBaja", CONEXIONMAESTRA.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letra", buscador);
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
        public void mostrarCombajapendiente(ref DataTable dt)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrarCombajapendiente", CONEXIONMAESTRA.conectar);
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

        public void contarCombajapendiente(ref int contador)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                var da = new SqlCommand("contarCombajapendiente", CONEXIONMAESTRA.conectar);
                contador = Convert.ToInt32(da.ExecuteScalar());
            }
            catch (Exception ex)
            {
                contador = 0;
            }
            finally
            {
                CONEXIONMAESTRA.cerrar();
            }
        }
        public void contarCombajarechazados(ref int contador)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                var da = new SqlCommand("contarCombajarechazados", CONEXIONMAESTRA.conectar);
                contador = Convert.ToInt32(da.ExecuteScalar());
            }
            catch (Exception ex)
            {
                contador = 0;
            }
            finally
            {
                CONEXIONMAESTRA.cerrar();
            }
        }
        public void contarCombajaaprobados(ref int contador)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                var da = new SqlCommand("contarCombajaaprobados", CONEXIONMAESTRA.conectar);
                contador = Convert.ToInt32(da.ExecuteScalar());
            }
            catch (Exception ex)
            {
                contador = 0;
            }
            finally
            {
                CONEXIONMAESTRA.cerrar();
            }
        }
    }
}
