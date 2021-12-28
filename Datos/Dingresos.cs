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
    public class Dingresos
    {
        int Idmovcaja;
        private void mostrarIdmovcaja()
        {
            var funcion = new DmovimientoCaja();
            var dt = new DataTable();
            funcion.MostrarMovimientosCaja(ref dt);
            Idmovcaja = Convert.ToInt32(dt.Rows[0][0]);
        }
        public bool Insertar_Ingresosvarios(Lingresos parametros)
        {
            try
            {
                mostrarIdmovcaja();

                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("insertarIngresos", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fecha", parametros.Fecha);
                cmd.Parameters.AddWithValue("@importe", parametros.Importe);
                cmd.Parameters.AddWithValue("@Descripcion", parametros.Descripcion);
                cmd.Parameters.AddWithValue("@Idmovcaja", Idmovcaja);
                cmd.Parameters.AddWithValue("@Id_usuario", parametros.IdUsuario);
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
        public bool Eliminar_Ingresosvarios(Lingresos parametros)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("eliminarIngresos", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Idingreso", parametros.Idingreso);
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
        public void mostrar_Ingresosvarios(ref DataTable dt)
        {
            try
            {
                mostrarIdmovcaja();
                CONEXIONMAESTRA.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrarIngresosPorCaja", CONEXIONMAESTRA.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Idmovcaja", Idmovcaja);
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
        public void RptIngresosVarios(ref double total)
        {
            try
            {
                mostrarIdmovcaja();
                CONEXIONMAESTRA.abrir();
                var da = new SqlCommand("RptIngresosVarios", CONEXIONMAESTRA.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@Idmovcaja", Idmovcaja);
                total = Convert.ToDouble(da.ExecuteScalar());
            }
            catch (Exception ex)
            {
                total = 0;
            }
            finally
            {
                CONEXIONMAESTRA.cerrar();
            }
        }

    }
}
