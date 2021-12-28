using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using RestCsharp;
using System.IO;
using System.Windows.Forms;
using Sunat.Logica;

namespace RestCsharp.Datos
{
   public class Dcodigosubigeo
    {
        public void Insertarcodigosubigeo()
        {
            try
            {
                string rutatxt = Path.GetDirectoryName(Application.ExecutablePath) + @"\listaubigeos.txt";

                CONEXIONMAESTRA.abrir();
                string sql = "BULK INSERT Codigosubigeos " + "FROM '" +rutatxt  + "' WITH (" +  "CODEPAGE = 'ACP',"  + "FIELDTERMINATOR = ';'," + "ROWTERMINATOR = '\n')";
                var cmd = new SqlCommand(sql, CONEXIONMAESTRA.conectar);
                cmd.ExecuteNonQuery();
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
        public void mostrarCodigosubigeo(ref DataTable dt)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlDataAdapter da = new SqlDataAdapter("select * from Codigosubigeos", CONEXIONMAESTRA.conectar);
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
        public void ObtenerUbicaionXubigeo(ref DataTable dt, Lcodigosubigeos parametros)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                var da = new SqlDataAdapter("ObtenerUbicaionXubigeo", CONEXIONMAESTRA.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue ("@CodUbigeo", parametros.Ubigeo);
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

        public void ObtenercodigoUbigeo(ref string ubigeo,Lcodigosubigeos parametros)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                var da = new SqlCommand("ObtenercodigoUbigeo", CONEXIONMAESTRA.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@Depa", parametros.Departamento);
                da.Parameters.AddWithValue("@Prov", parametros.Provincia);
                da.Parameters.AddWithValue("@Dist", parametros.Distrito);
                ubigeo =Convert.ToString( da.ExecuteScalar());
            }
            catch (Exception ex)
            {
                ubigeo = "-";
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CONEXIONMAESTRA.cerrar();
            }
        }
    }
}
