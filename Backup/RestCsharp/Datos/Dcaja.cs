using RestCsharp.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RestCsharp.Datos
{
  public   class Dcaja
    {
        string SerialPc;
        public void mostrarCajaSerial(ref int idcaja)
        {
            try 
            {
                Bases.Obtener_serialPC(ref SerialPc);
                CONEXIONMAESTRA.abrir();
                SqlCommand da = new SqlCommand("mostrarCajaSerial", CONEXIONMAESTRA.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@Serial", SerialPc);
                idcaja = Convert.ToInt32 ( da.ExecuteScalar());
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
        public void mostrarIdmaxcaja(ref int numerocaja)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlCommand da = new SqlCommand("select max(Id_Caja) from Caja", CONEXIONMAESTRA.conectar);
                numerocaja = Convert.ToInt32(da.ExecuteScalar());
            }
            catch (Exception ex)
            {
                numerocaja = 0;
                MessageBox.Show(ex.StackTrace);
            }
            finally
            {
                CONEXIONMAESTRA.cerrar();
            }
        }
        public void mostrarCajaSerialTable(ref DataTable dt)
        {
            try
            {
                Bases.Obtener_serialPC(ref SerialPc);
                CONEXIONMAESTRA.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrarCajaSerial", CONEXIONMAESTRA.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Serial", SerialPc);
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
        public void mostrarCajaRemota(ref int idcaja)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlCommand da = new SqlCommand("mostrarCajaRemota", CONEXIONMAESTRA.conectar);   
                idcaja = Convert.ToInt32(da.ExecuteScalar());
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
        public bool Insertar_caja(Lcaja parametros)
        {
            try
            {
                Bases.Obtener_serialPC(ref SerialPc);
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("Insertar_caja", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@descripcion", parametros.Descripcion);
                cmd.Parameters.AddWithValue("@Tema", parametros.Tema);
                cmd.Parameters.AddWithValue("@Serial_PC", SerialPc);
                cmd.Parameters.AddWithValue("@Tipo", parametros.Tipo);      
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                CONEXIONMAESTRA.cerrar();
            }
        }
        public bool editarTemacaja(Lcaja parametros)
        {
            try
            {
                Bases.Obtener_serialPC(ref SerialPc);
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("editarTemacaja", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@Serialpc",SerialPc);
                cmd.Parameters.AddWithValue("@Tema", parametros.Tema);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                CONEXIONMAESTRA.cerrar();
            }
        }

        public bool Insertar_cajaRemota(Lcaja parametros)
        {
            try
            {
                Bases.Obtener_serialPC(ref SerialPc);
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("Insertar_caja", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@descripcion", parametros.Descripcion);
                cmd.Parameters.AddWithValue("@Tema", parametros.Tema);
                cmd.Parameters.AddWithValue("@Serial_PC", "REMOTA");
                cmd.Parameters.AddWithValue("@Tipo", parametros.Tipo);
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
        public bool ReemplazarSerialPc()
        {
            try
            {

                Bases.Obtener_serialPC(ref SerialPc);
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("ReemplazarSerialPc", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Serialpc", SerialPc);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                CONEXIONMAESTRA.cerrar();
            }
        }
    }
}
