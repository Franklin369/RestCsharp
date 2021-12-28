using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
namespace RestCsharp.Datos
{
  public   class Dsesion
    {
        public void ContarSesiones(ref int contadorSesion)
        {
			try
			{
				CONEXIONMAESTRA.abrir();
				SqlCommand cmd = new SqlCommand("ContarInicioSesion", CONEXIONMAESTRA.conectar);
				cmd.CommandType = CommandType.StoredProcedure;
				//cmd.Parameters.AddWithValue("@idcaja",);
				contadorSesion =Convert.ToInt32 (cmd.ExecuteScalar());
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
