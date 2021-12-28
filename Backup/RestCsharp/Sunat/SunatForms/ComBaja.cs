using RestCsharp.Datos;
using Sunat.Logica;
using RestCsharp.Sunat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestCsharp.Presentacion.SunatForms;

namespace Ada369Csharp.Presentacion.SunatForms
{
    public partial class ComBaja : UserControl
    {
        public ComBaja()
        {
            InitializeComponent();
        }

        private void btnagregar_Click(object sender, EventArgs e)
        {
            Agregar();
        }
        private void Agregar()
        {
            var ctl = new AgregarComBaja();
            Controls.Add(ctl);
            ctl.BringToFront();
            ctl.Size = new Size(Width, Height);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void txtbuscar_TextChanged(object sender, EventArgs e)
        {
            BuscarComunBaja();
        }
        private void BuscarComunBaja()
        {
            PanelNcredito.Controls.Clear();
            var funcion = new Dcombaja();
            var dt = new DataTable();
            funcion.buscarComBaja(ref dt, txtbuscar.Text);
            foreach (DataRow data in dt.Rows)
            {
                Button btn = new Button();
                PictureBox Iconoestado = new PictureBox();
                Panel panelEstado = new Panel();
                Panel panelContenedor = new Panel();
                Label lblestado = new Label();

                panelContenedor.Size = new Size(449, 87);
                panelContenedor.BackColor = Color.FromArgb(64, 64, 64);
                btn.Text = "Ticket: " + data["Ticket"].ToString() + "\n" + "Anula a: " + data["Afecta"].ToString();
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
                if (data["Codigorespuesta"].ToString()=="0")
                {
                    lblestado.ForeColor = Color.FromArgb(39, 229, 143);
                    Iconoestado.Image = RestCsharp.Properties.Resources.satisfaccion;
                }
                else if (data["Codigorespuesta"].ToString() == "99")
                {
                    lblestado.ForeColor = Color.FromArgb(117, 129, 243);
                    Iconoestado.Image = RestCsharp.Properties.Resources.insatisfaccion;
                }
                else if (data["Codigorespuesta"].ToString() == "98")
                {
                    lblestado.ForeColor = Color.FromArgb(117, 129, 243);
                    Iconoestado.Image = RestCsharp.Properties.Resources.dia;
                }
                else
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

        private void ComBaja_Load(object sender, EventArgs e)
        {
            BuscarComunBaja();
            Combajaspendientes();
            Combajasrechazados();
        }
        private void Combajaspendientes()
        {
            var funcion = new Dcombaja();
            int contador = 0;
            funcion.contarCombajapendiente(ref contador);
            if (contador > 0)
            {
                btnpendientes.Text = "Pendientes [" + contador + "] - Consultar";
                btnpendientes.Visible = true;
            }
            else
            {
                btnpendientes.Visible = false;
            }

        }
        private void Combajasrechazados()
        {
            var funcion = new Dcombaja();
            int contador = 0;
            funcion.contarCombajarechazados(ref contador);
            if (contador > 0)
            {
                btnrechazados.Text = "Rechazados [" + contador + "] - Consultar";
                btnrechazados.Visible = true;
            }
            else
            {
                btnrechazados.Visible = false;
            }

        }

        private void btnpendientes_Click(object sender, EventArgs e)
        {
            var funcion = new Dcombaja();
            var dt = new DataTable();
            funcion.mostrarCombajapendiente(ref dt);
            var funcionenvio = new EmitirComprobante();
            var funcioneditar = new Dcombaja();
            var parametros = new Lcombaja();
            foreach (DataRow data in dt.Rows)
            {
                string ticket = data["Ticket"].ToString();
                string codigoRespuesta = funcionenvio.ObtenerrespuestaCbaja(ticket);
                if (codigoRespuesta == "98")
                {
                    parametros.Estadosunat = "Pendiente";
                }
                if (codigoRespuesta == "99")
                {
                    parametros.Estadosunat = "Rechazado";
                }
                if (codigoRespuesta == "0")
                {
                    parametros.Estadosunat = "Aprobado";
                }
                else
                {
                    parametros.Estadosunat = "Pendiente";
                }
                parametros.Ticket = ticket;
                parametros.codigo = codigoRespuesta;
                funcioneditar.EditarestadoCombaja(parametros);

            }

            BuscarComunBaja();
            Combajaspendientes();
            Combajasrechazados();
        }
    }
}
