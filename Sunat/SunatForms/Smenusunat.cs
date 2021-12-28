using Ada369Csharp.Presentacion.SunatForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestCsharp.Sunat.SunatForms
{
    public partial class Smenusunat : UserControl
    {
        public Smenusunat()
        {
            InitializeComponent();
        }



        private void Smenusunat_Load(object sender, EventArgs e)
        {
            Dibujarbotones();
        }
        private void Dibujarbotones()
        {
            panelbotones.Controls.Clear();
            var botones = new string[] { "Facturas", "Boletas", "Notas de credito", "Notas de debito","Bajas" };
            foreach (string boton in botones)
            {
                Button btn = new Button();
                btn.Text = boton.ToString();
                btn.BackgroundImageLayout = ImageLayout.Stretch;
                btn.FlatAppearance.MouseDownBackColor = Color.Transparent;
                btn.FlatAppearance.MouseOverBackColor = Color.Transparent;
                btn.BackColor = Color.FromArgb(39, 39, 39);
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Size = new Size(153, 69);
                btn.ForeColor = Color.White;
                btn.Font = new Font("Consolas", 13, FontStyle.Bold);
                panelbotones.Controls.Add(btn);
                btn.Click += Btn_Click;
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            string texto = ((Button)sender).Text;
            foreach (Control control in panelbotones.Controls)
            {
                if (control is Button)
                {
                    if (control.Text == texto)
                    {
                        control.BackgroundImage = Properties.Resources.azul;
                    }
                    else
                    {
                        control.BackgroundImage = null;
                    }
                }
            }
            if (texto == "Notas de credito")
            {
                Notascredito();
            }
            if (texto == "Facturas")
            {
                Facturas();
            }
            if (texto == "Boletas")
            {
                Boletas();
            }
            if (texto == "Notas de debito")
            {
                Notasdebito();
            }
            if (texto == "Bajas")
            {
                Bajas();
            }
        }
        private void Bajas()
        {
            panelVisor.Controls.Clear();
            var ctl = new ComBaja();
            ctl.Dock = DockStyle.Fill;
            panelVisor.Controls.Add(ctl);
        }
        private void Notasdebito()
        {
            panelVisor.Controls.Clear();
            var ctl = new Snotasdebito();
            ctl.Dock = DockStyle.Fill;
            panelVisor.Controls.Add(ctl);
        }
        private void Boletas()
        {
            panelVisor.Controls.Clear();
            var ctl = new Sboletas();
            ctl.Dock = DockStyle.Fill;
            panelVisor.Controls.Add(ctl);
        }
        private void Facturas()
        {
            panelVisor.Controls.Clear();
            var ctl = new Sfacturas();
            ctl.Dock = DockStyle.Fill;
            panelVisor.Controls.Add(ctl);
        }
        private void Notascredito()
        {
            panelVisor.Controls.Clear();
            var ctl = new Snotascredito();
            ctl.Dock = DockStyle.Fill;
            panelVisor.Controls.Add(ctl);
        }
    }
}
