using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Sunat.Logica;

namespace RestCsharp.Datos
{
   public class Dproductos
    {
        public bool editarProductos(Lproductos parametros)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("editarProductos", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Descripcion", parametros.Descripcion);
                cmd.Parameters.AddWithValue("@Imagen", parametros.Imagen);
                cmd.Parameters.AddWithValue("@PrecioVenta", parametros.Precio_de_venta);
                cmd.Parameters.AddWithValue("@Preciocompra", parametros.Precio_de_compra);
                cmd.Parameters.AddWithValue("@Estadoimagen", parametros.Estado_imagen);
                cmd.Parameters.AddWithValue("@Idcolor", parametros.Idcolor);
                cmd.Parameters.AddWithValue("@Idproducto", parametros.Id_Producto1);
                cmd.Parameters.AddWithValue("@CodUm", parametros.CodUm);
                cmd.Parameters.AddWithValue("@CodigoSunat", parametros.CodigoSunat);
                cmd.Parameters.AddWithValue("@Codigo", parametros.Codigo);

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
        public bool eliminarProductos(Lproductos parametros)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("eliminarProductos", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Idproducto", parametros.Id_Producto1);
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
        public bool RestaurarProductos(Lproductos parametros)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("RestaurarProductos", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Idproducto", parametros.Id_Producto1);
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
        public bool insertarProductos(Lproductos parametros)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("insertar_Producto", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Descripcion", parametros.Descripcion);
                cmd.Parameters.AddWithValue("@Imagen", parametros.Imagen);
                cmd.Parameters.AddWithValue("@Id_grupo", parametros.Id_grupo);
                cmd.Parameters.AddWithValue("@Precio_de_venta", parametros.Precio_de_venta);
                cmd.Parameters.AddWithValue("@Precio_de_compra", parametros.Precio_de_compra);
                cmd.Parameters.AddWithValue("@Estado_imagen", parametros.Estado_imagen);
                cmd.Parameters.AddWithValue("@Idcolor", parametros.Idcolor);
                cmd.Parameters.AddWithValue("@CodUm", parametros.CodUm);
                cmd.Parameters.AddWithValue("@CodigoSunat", parametros.CodigoSunat);
                cmd.Parameters.AddWithValue("@Codigo", parametros.Codigo);
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
