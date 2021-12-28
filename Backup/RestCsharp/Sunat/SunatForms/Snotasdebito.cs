using RestCsharp.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ada369Csharp.Presentacion.SunatForms
{
    public partial class Snotasdebito : UserControl
    {
        public Snotasdebito()
        {
            InitializeComponent();
        }

        private void btnagregar_Click(object sender, EventArgs e)
        {
            Agregar();
        }
        private void Agregar()
        {
            var ctl = new AgregarNdebito();
            Controls.Add(ctl);
            ctl.BringToFront();
            ctl.Size = new Size(Width, Height);
        }

        private void txtpaswwor_TextChanged(object sender, EventArgs e)
        {
            BuscarNotasdebito();
        }
        public void BuscarNotasdebito()
        {
            PanelNcredito.Controls.Clear();
            var funcion = new Dnotasdebito();
            var dt = new DataTable();
            funcion.buscarNotasdebito(ref dt, txtbuscar.Text);
            foreach (DataRow data in dt.Rows)
            {
                Button btn = new Button();
                PictureBox Iconoestado = new PictureBox();
                Panel panelEstado = new Panel();
                Panel panelContenedor = new Panel();
                Label lblestado = new Label();

                panelContenedor.Size = new Size(449, 87);
                panelContenedor.BackColor = Color.FromArgb(64, 64, 64);
                btn.Text = data["Tipo comprobante"].ToString() + ": " + data["Comprobante"].ToString() + "\n" + "Afecta a: " + data["Afecta"].ToString();
                btn.Size = new Size(449, 87);
                btn.Dock = DockStyle.Fill;
                btn.FlatStyle = FlatStyle.Flat;
                btn.BackColor = Color.FromArgb(64, 64, 64);
                btn.ForeColor = Color.White;
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(17, 65, 141);
                btn.Font = new Font("Consolas", 12, FontStyle.Bold);

                #region estados
                lblestado.Text = data["Estado envio sunat"].ToString();

                lblestado.Dock = DockStyle.Bottom;
                lblestado.TextAlign = ContentAlignment.MiddleCenter;
                lblestado.Font = new System.Drawing.Font("Consolas", 10, FontStyle.Bold);
                panelEstado.Size = new Size(101, 150);
                panelEstado.Dock = DockStyle.Right;
                panelEstado.BackColor = Color.Transparent;

                Iconoestado.SizeMode = PictureBoxSizeMode.Zoom;
                Iconoestado.Dock = DockStyle.Bottom;
                panelEstado.Controls.Add(Iconoestado);
                panelEstado.Controls.Add(lblestado);
                if (data["Estado envio sunat"].ToString() == "ACEPTADA")
                {
                    lblestado.ForeColor = Color.FromArgb(39, 229, 143);
                    Iconoestado.Image = RestCsharp.Properties.Resources.satisfaccion;
                }
                else if (data["Estado envio sunat"].ToString() == "PENDIENTE")
                {
                    lblestado.ForeColor = Color.FromArgb(117, 129, 243);
                    Iconoestado.Image = RestCsharp.Properties.Resources.dia;
                }
                #endregion


                panelContenedor.Controls.Add(panelEstado);
                panelContenedor.Controls.Add(btn);

                PanelNcredito.Controls.Add(panelContenedor);
                btn.BringToFront();
            }
        }

        private void Snotasdebito_Load(object sender, EventArgs e)
        {
            BuscarNotasdebito();
        }
    }
}
