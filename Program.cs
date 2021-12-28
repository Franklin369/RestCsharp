using RestCsharp.Presentacion.AsistenteInstalacion;
using RestCsharp.Presentacion.Login;
using RestCsharp.Presentacion.PRODUCTOS;
using RestCsharp.Presentacion.Caja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using RestCsharp.Presentacion.PUNTO_DE_VENTA;
using RestCsharp.Presentacion.Impresoras;
using RestCsharp.Presentacion.Cocina;
using RestCsharp.Presentacion.Reportes;
using RestCsharp.Presentacion.Conexionremota;

namespace RestCsharp
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var frm = new LoginForm();
            frm.FormClosed += frm_closed;
            frm.ShowDialog();
            Application.Run();
        }
        private static void frm_closed(object sender, FormClosedEventArgs e)
        {
            Application.ExitThread();
            Application.Exit();
        }
    }
}
