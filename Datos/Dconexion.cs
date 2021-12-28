using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace RestCsharp.Datos
{
   public class Dconexion
    {
		public bool validarConexion(ref int contador)
		{
			try
			{
				CONEXIONMAESTRA.abrir();
				SqlCommand cmd = new SqlCommand("select count(IdUsuario) from Usuarios", CONEXIONMAESTRA.conectar);
				contador = Convert.ToInt32(cmd.ExecuteScalar());
				CONEXIONMAESTRA.cerrar();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
			
		}
	}
}
