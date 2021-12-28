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
  public   class DmovimientoCaja
    {
        string SerialPc;
        int idcaja;
        int Idmovimientocaja;

        public void MostrarMovimientosCaja(ref DataTable dt)
        {
            try
            {
                Bases.Obtener_serialPC(ref SerialPc);
                CONEXIONMAESTRA.abrir();
                SqlDataAdapter da = new SqlDataAdapter("MostrarMovimientosCaja", CONEXIONMAESTRA.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@serial", SerialPc);
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
        public bool insertar_MovimientosCaja(LmovientosCaja parametros)
        {
            try
            {
                Dcaja funcion = new Dcaja();
                funcion.mostrarCajaSerial(ref idcaja);
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("insertar_MovimientosCaja", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Idusuario", parametros.Idusuario);
                cmd.Parameters.AddWithValue("@IdCaja", idcaja);
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
        public bool insertar_movCajaremota(LmovientosCaja parametros)
        {
            try
            {
                Dcaja funcion = new Dcaja();
                funcion.mostrarCajaSerial(ref idcaja);
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("insertar_MovimientosCaja", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Idusuario", parametros.Idusuario);
                cmd.Parameters.AddWithValue("@IdCaja", parametros.IdCaja);
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
        public void MostrarMovCajaUser(ref int idMov, LmovientosCaja parametros)
        {
            try
            {
                Bases.Obtener_serialPC(ref SerialPc);
                CONEXIONMAESTRA.abrir();
                SqlCommand da = new SqlCommand("MostrarMovCajaUser", CONEXIONMAESTRA.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@serial", SerialPc);
                da.Parameters.AddWithValue("@idusuario", parametros.Idusuario);
                idMov = Convert.ToInt32(da.ExecuteScalar());

            }
            catch (Exception)
            {
                idMov = 0;
            }
            finally
            {
                CONEXIONMAESTRA.cerrar();
            }
        }
        public bool EditarDineroInicial(LmovientosCaja parametros)
        {
            try
            {
                Dcaja funcion = new Dcaja();
                funcion.mostrarCajaSerial(ref idcaja);
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("editarDineroInicial", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EfectivoInicial", parametros.EfectivoInicial);
                cmd.Parameters.AddWithValue("@Id_caja", idcaja);
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
        public bool cerrarCaja(LmovientosCaja parametros)
        {
            try
            {
                var dt = new DataTable();
                MostrarMovimientosCaja(ref dt);
                Idmovimientocaja = Convert.ToInt32(dt.Rows[0][0]);
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("cerrarCaja", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdMovimientoCaja", Idmovimientocaja);
                cmd.Parameters.AddWithValue("@fechafin", parametros.fechafin);
                cmd.Parameters.AddWithValue("@ingresos", parametros.ingresos);
                cmd.Parameters.AddWithValue("@egresos", parametros.egresos);
                cmd.Parameters.AddWithValue("@VEfectivo", parametros.VEfectivo);
                cmd.Parameters.AddWithValue("@VCredito", parametros.VCredito);
                cmd.Parameters.AddWithValue("@VTarjeta", parametros.VTarjeta);
                cmd.Parameters.AddWithValue("@EfectivoCalculado", parametros.EfectivoCalculado);
                cmd.Parameters.AddWithValue("@EfectivoReal", parametros.EfectivoReal);
                cmd.Parameters.AddWithValue("@EfectivoDiferencia", parametros.EfectivoDiferencia);

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
        public bool editarIdmovCaja()
        {
            try
            {
                var dt = new DataTable();
                MostrarMovimientosCaja(ref dt);
                Idmovimientocaja = Convert.ToInt32(dt.Rows[0][0]);
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("editarIdmovCaja", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Idmovicaja", Idmovimientocaja);
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
        public void Rptcierrecaja(ref DataTable dt)
        {
            try
            {
                var funcion = new Dcaja();
                funcion.mostrarCajaSerial(ref idcaja);
                CONEXIONMAESTRA.abrir();
                SqlDataAdapter da = new SqlDataAdapter("Rptcierrecaja", CONEXIONMAESTRA.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Idcaja", idcaja);
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
