using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RestCsharp.Datos;
using RestCsharp.Logica;
using RestCsharp.Presentacion.AsistenteInstalacion;
using RestCsharp.Presentacion.Licencia;
using RestCsharp.Presentacion.PUNTO_DE_VENTA;

namespace RestCsharp.Presentacion.Login
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        string login;
        int idusuario;
        string rol;
        string UsuarioInicioCaja;
        string EstadoAperturaCaja;
        int idsesion;
        int idMovCaja;
        int contadorconexion;
        string Ip;
        string ResultadoLicencia;
        DateTime FechaFinal;
     
        private void LoginForm_Load(object sender, EventArgs e)
        {
            ValidarConexion();
        }

        private void ValidarConexion()
        {
            var funcion = new Dconexion();
            if (funcion.validarConexion(ref contadorconexion) == true)
            {
                if (contadorconexion > 0)
                {

                    centralPaneles();
                    dibujarUsuarios();
                    ObtenerIpLocal();
                    Obtenerempresa();
                }
                else
                {
                    PasarAConfigurar();
                }

            }
            else
            {
                PasarAeleccionServidor();
            }
        }
        private void Obtenerempresa()
        {
            var funcion = new Dempresa();
            var dt = new DataTable();
            funcion.mostrar_empresa(ref dt);
            lblempresa.Text = dt.Rows[0][15].ToString();
            btnpais.Text = dt.Rows[0][14].ToString();
        }
        private void PasarAeleccionServidor()
        {
            Dispose();
            var frm = new EleccionServidor();
            frm.ShowDialog();
        }
        private void PasarAConfigurar()
        {
            Dispose();
            var frm = new Empresaregistro();
            frm.ShowDialog();
        }

        private void ObtenerIpLocal()
        {

            this.Text = Bases.ObtenerIp(ref Ip);
        }
        private void centralPaneles()
        {
            PanelVisorDeUsuarios.Dock = DockStyle.Fill;
            PanelIngresarContraseña.Location = new Point((panel6.Width - PanelIngresarContraseña.Width) / 2, (panel6.Height - PanelIngresarContraseña.Height) / 2);
        }

        private void dibujarUsuarios()
        {
            DataTable dt = new DataTable();
            Dusuarios funcion = new Dusuarios();
            Panel panellicencia = new Panel();
            this.Controls.Add(panellicencia);
            funcion.dibujarUsuarios(ref dt, ref ResultadoLicencia, ref panellicencia);
            lblestadoLicencia.Text = ResultadoLicencia;
            PanelMostradorUsuarios.Controls.Clear();


            foreach (DataRow rdr in dt.Rows)
            {
                Label l = new Label();
                Panel p = new Panel();
                PictureBox Pt = new PictureBox();
                l.Text = rdr["Login"].ToString();
                l.Name = rdr["IdUsuario"].ToString();
                l.Size = new Size(175, 25);
                l.Font = new Font("Microsoft Sans Serif", 13);
                l.BackColor = Color.Transparent;
                l.ForeColor = Color.White;
                l.Dock = DockStyle.Bottom;
                l.TextAlign = ContentAlignment.MiddleCenter;
                l.Cursor = Cursors.Hand;

                p.Size = new Size(155, 167);
                p.BorderStyle = BorderStyle.None;
                p.BackColor = Color.FromArgb(20, 20, 20);

                Pt.BackgroundImage = null;
                byte[] bi = (Byte[])rdr["Icono"];
                MemoryStream ms = new MemoryStream(bi);
                Pt.Image = Image.FromStream(ms);
                Pt.Dock = DockStyle.Fill;
                Pt.SizeMode = PictureBoxSizeMode.Zoom;
                Pt.Tag = rdr["Login"].ToString();
                Pt.Cursor = Cursors.Hand;

                p.Controls.Add(l);
                p.Controls.Add(Pt);
                Pt.BringToFront();
                PanelMostradorUsuarios.Controls.Add(p);
                l.Click += L_Click;
                Pt.Click += Pt_Click;

            }

        }



        private void Pt_Click(object sender, EventArgs e)
        {
            login = ((PictureBox)sender).Tag.ToString();
            verPanelcontraseña();
        }

        private void L_Click(object sender, EventArgs e)
        {
            login = ((Label)sender).Text;
            verPanelcontraseña();


        }
        private void verPanelcontraseña()
        {
            PanelVisorDeUsuarios.Visible = false;
            PanelContraseña.Visible = true;
            PanelContraseña.Dock = DockStyle.Fill;
            PanelIngresarContraseña.Location = new Point((panel6.Width - PanelIngresarContraseña.Width) / 2, (panel6.Height - PanelIngresarContraseña.Height) / 2);
        }

        private void txtcontraseña_TextChanged(object sender, EventArgs e)
        {
            validarUsuarios();
        }
        private void validarUsuarios()
        {
            Lusuarios parametros = new Lusuarios();
            Dusuarios funcion = new Dusuarios();
            parametros.Password = Bases.Encriptar(txtcontraseña.Text);
            parametros.Login = login;
            funcion.validarUsuario(parametros, ref idusuario);
            if (idusuario > 0)
            {
                mostrarRoles();
                if (rol == "Cajero" || rol == "Administrador")
                {

                    ValidarAperturasCaja();
                }
                else
                {
                    ValidarRol();
                }

            }
        }

        private void ValidarAperturasCaja()
        {
            //Mostramos las cajas aperturadas Por serial de Computadora
            MostrarMovimientosCaja();
            if (UsuarioInicioCaja == "Nulo")
            {
                insertar_MovimientosCaja();
                editarIdmovCajaVentas();
                EstadoAperturaCaja = "Nuevo";
                ValidarRol();
            }
            else
            {
                //Mostramos las cajas aperturadas Por serial de Computadora y Usuario
                MostrarMovCajaUser();
            }
        }
        private void editarIdmovCajaVentas()
        {
            var funcion = new DmovimientoCaja();
            funcion.editarIdmovCaja();
        }
        private void MostrarMovCajaUser()
        {
            LmovientosCaja parametros = new LmovientosCaja();
            DmovimientoCaja funcion = new DmovimientoCaja();
            parametros.Idusuario = idusuario;
            funcion.MostrarMovCajaUser(ref idMovCaja, parametros);
            if (idMovCaja == 0)
            {
                if (rol == "Administrador")
                {
                    MessageBox.Show("Todos los Registros seran con el Usuario: " + UsuarioInicioCaja + "* ,Inicia sesion con el Usuario " + UsuarioInicioCaja + " -ó-el Usuario *admin*", "Caja Iniciada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EstadoAperturaCaja = "Aperturada";
                    ValidarRol();
                }
                else
                {
                    MessageBox.Show("Para poder continuar con el Turno de *" + UsuarioInicioCaja + "* ,Inicia sesion con el Usuario " + UsuarioInicioCaja + " -ó-el Usuario *admin*", "Caja Iniciada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                EstadoAperturaCaja = "Aperturada";
                ValidarRol();
            }
        }
        private void insertar_MovimientosCaja()
        {
            LmovientosCaja parametros = new LmovientosCaja();
            DmovimientoCaja funcion = new DmovimientoCaja();
            parametros.Idusuario = idusuario;
            funcion.insertar_MovimientosCaja(parametros);
        }
        private void MostrarMovimientosCaja()
        {
            DataTable dt = new DataTable();
            DmovimientoCaja funcion = new DmovimientoCaja();
            funcion.MostrarMovimientosCaja(ref dt);
            if (dt.Rows.Count > 0)
            {
                UsuarioInicioCaja = dt.Rows[0]["Nombre"].ToString();
            }
            else
            {
                UsuarioInicioCaja = "Nulo";
            }
        }
        private void ValidarRol()
        {
            if (rol == "Cajero" || rol == "Administrador")
            {
                if (EstadoAperturaCaja == "Nuevo")
                {
                    mostrarInicioSesion();
                    Dispose();
                    Caja.AperturaCaja frm = new Caja.AperturaCaja();
                    frm.ShowDialog();
                }
                else
                {
                    PasarVisorMesas();
                }
            }
            else
            {
                PasarVisorMesas();
            }
        }
        private void PasarVisorMesas()
        {
           
            mostrarInicioSesion();
           // Dispose();
            var frm = new Visor_de_mesas();
            frm.Dock = DockStyle.Fill;
            this.Controls.Add(frm);
            frm.BringToFront();
            PanelContraseña.Visible = false;
            PanelVisorDeUsuarios.Visible = true;
            txtcontraseña.Clear();
            //frm.ShowDialog();
        }
        private void mostrarInicioSesion()
        {
            DiniciosSesion funcion = new DiniciosSesion();
            funcion.mostrarInicioSesion(ref idsesion);
            if (idsesion > 0)
            {
                editarInicioSesion();
            }
            else
            {
                insertarInicioSesion();
            }
        }
        private void insertarInicioSesion()
        {
            LiniciosSesion parametros = new LiniciosSesion();
            DiniciosSesion funcion = new DiniciosSesion();
            parametros.IdUsuario = idusuario;
            funcion.insertarInicioSesion(parametros);
        }
        private void editarInicioSesion()
        {
            LiniciosSesion parametros = new LiniciosSesion();
            DiniciosSesion funcion = new DiniciosSesion();
            parametros.IdUsuario = idusuario;
            parametros.Idsesion = idsesion;
            funcion.editarInicioSesion(parametros);
        }

        private void mostrarRoles()
        {
            Lusuarios parametros = new Lusuarios();
            Dusuarios funcion = new Dusuarios();
            parametros.IdUsuario = idusuario;
            funcion.mostrarRoles(parametros, ref rol);
        }
        private void btnIniciar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Contraseña erronea", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtcontraseña.Text += "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtcontraseña.Text += "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtcontraseña.Text += "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtcontraseña.Text += "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtcontraseña.Text += "5";
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtcontraseña.Text += "6";
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtcontraseña.Text += "7";
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtcontraseña.Text += "8";
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtcontraseña.Text += "9";
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            txtcontraseña.Text += "0";
        }

        private void btnborrar_Click(object sender, EventArgs e)
        {
            txtcontraseña.Clear();
        }

        private void btnborrarderecha_Click(object sender, EventArgs e)
        {
            int contador;
            contador = txtcontraseña.Text.Count();
            if (contador > 0)
            {
                txtcontraseña.Text = txtcontraseña.Text.Substring(0, txtcontraseña.Text.Count() - 1);
            }

        }

        private void btnCambioUsuario_Click(object sender, EventArgs e)
        {
            PanelContraseña.Visible = false;
            PanelVisorDeUsuarios.Visible = true;
            txtcontraseña.Clear();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PanelVisorDeUsuarios.Visible = false;
            PanelContraseña.Visible = false;
            var ctl = new Licencias();
            ctl.Dock = DockStyle.Fill;
            this.Controls.Add(ctl);
            ctl.BringToFront();
        }
    }
}
