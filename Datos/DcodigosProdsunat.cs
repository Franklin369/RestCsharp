using RestCsharp;
using Sunat.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestCsharp.Datos
{
   public class DcodigosProdsunat
    {
        public void buscarCodProdSunat(ref DataTable dt, string buscador)
        {
            try
            {

                CONEXIONMAESTRA.abrir();
                SqlDataAdapter da = new SqlDataAdapter("buscarCodProdSunat", CONEXIONMAESTRA.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@buscador", buscador);
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
        public void mostrarCodSunatXCod(ref DataTable dt, LcodigosSunat parametros)
        {
            try
            {

                CONEXIONMAESTRA.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrarCodSunatXCod", CONEXIONMAESTRA.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@codigo", parametros.codigo);
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
        public void InsertarcodigosProductosSunat()
        {
            try
            {
                string rutatxt = Path.GetDirectoryName(Application.ExecutablePath) + @"\catalogo.txt";

                CONEXIONMAESTRA.abrir();
                string sql = "BULK INSERT CodigosProdSunat " + "FROM '" + rutatxt + "' WITH (" + "CODEPAGE = 'ACP'," + "FIELDTERMINATOR = ';'," + "ROWTERMINATOR = '\n')";
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

    }
}
