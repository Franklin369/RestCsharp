using RestCsharp.Datos;
using RestCsharp.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RestCsharp.Presentacion.Licencia
{
    public partial class Licencias : UserControl
    {
       
        public Licencias()
        {
            InitializeComponent();
            panel2.Location = new Point((Width - panel2.Width) / 2, (Height - panel2.Height) / 2);
        }
        string serial;
        private void Licencias_Load(object sender, EventArgs e)
        {

            Bases.Obtener_serialPC(ref serial);
            txtSerial.Text = serial;
        }

        private void btnActivacioManual_Click(object sender, EventArgs e)
        {
            var funcion = new Dlicencias();
            if ((funcion.ActivarLicencia() == false))
            {
                MessageBox.Show("Activacion fallida");
            }
        }

        private void btnCopiar_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtSerial.Text);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btncomprar_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.facebook.com/codigo369oficial");
        }
    }
}
