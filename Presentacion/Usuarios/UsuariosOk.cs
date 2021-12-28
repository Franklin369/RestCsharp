using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RestCsharp.Datos;
using RestCsharp.Logica;
using System.IO;
using System.Net.NetworkInformation;

namespace RestCsharp.Presentacion.Usuarios
{
    public partial class UsuariosOk : UserControl
    {
        public UsuariosOk()
        {
            InitializeComponent();
        }
        bool procede;
        int idusuario;
        string estado;
        private void txtbuscar_TextChanged(object sender, EventArgs e)
        {
            buscar();
        }
        private void buscar()
        {
            DataTable dt = new DataTable();
            Dusuarios funcion = new Dusuarios();
            funcion.buscar_usuarios(ref dt, txtbuscar.Text);
            datalistadoUsuarios.DataSource = dt;
            PintarGrid();
        }


        private void btnagregar_Click(object sender, EventArgs e)
        {
            limpiar();
            habilitarPaneles();
            dibujarModulos();
            ValidarRoles();
        }
        private void habilitarPaneles()
        {
            panelregistro.Visible = true;
            lblanuncioIcono.Visible = true;
            panelIcono.Visible = false;
            panelregistro.Dock = DockStyle.Fill;
            btnguardar.Visible = true;
            btnActualizar.Visible = false;
        }
        private void limpiar()
        {
            txtnombre.Clear();
            txtcontraseña.Clear();
            txtusuario.Clear();
            txtcorreo.Clear();
        }
        private void dibujarModulos()
        {
            Dmodulos funcion = new Dmodulos();
            DataTable dt = new DataTable();
            funcion.mostrar_Modulos(ref dt);
            datalistadoPermisos.DataSource = dt;
            datalistadoPermisos.Columns[1].Visible = false;
        }

        private void UsuariosOk_Load(object sender, EventArgs e)
        {
            mostrarUsuarios();
        }
        private void mostrarUsuarios()
        {
            DataTable dt = new DataTable();
            Dusuarios funcion = new Dusuarios();
            funcion.mostrar_Usuarios(ref dt);
            datalistadoUsuarios.DataSource = dt;

            PintarGrid();
        }
        private void PintarGrid()
        {
            Bases propiedad = new Bases();
            propiedad.DiseñoDatagridview(ref datalistadoUsuarios);
            propiedad.DiseñoDatagridviewEliminar(ref datalistadoUsuarios);
            datalistadoUsuarios.Columns[2].Visible = false;
            datalistadoUsuarios.Columns[6].Visible = false;
            datalistadoUsuarios.Columns[7].Visible = false;

        }
        private void cbxRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidarRoles();
        }
        private void ValidarRoles()
        {
            DataTable dt = new DataTable();
            Dpermisos funcion = new Dpermisos();
            funcion.mostrar_Permisos(ref dt);

            foreach (DataRow rowPermisos in dt.Rows)
            {
                foreach (DataGridViewRow rowModulos in datalistadoPermisos.Rows)
                {
                    string modulo = (rowModulos.Cells["Modulo"].Value).ToString();
                    if (cbxRol.Text == "Cajero")
                    {
                        if (modulo == "Cobrar" || modulo == "Cerrar caja" || modulo == "Ingreso / Salida de dinero")
                        {
                            rowModulos.Cells[0].Value = true;
                            rowModulos.Cells[0].ReadOnly = true;
                        }
                        if (modulo == "Administrar" || modulo=="SUNAT")
                        {
                            rowModulos.Cells[0].Value = false;
                            rowModulos.Cells[0].ReadOnly = true;
                        }
                    }
                    if (cbxRol.Text == "Mozo")
                    {
                        if (modulo == "Para llevar" || modulo == "Ver cuentas" || modulo == "Cocina" || modulo == "Cambio de mesa")
                        {
                            rowModulos.Cells[0].Value = true;
                            rowModulos.Cells[0].ReadOnly = true;
                        }
                        if (modulo == "Administrar" || modulo == "Cobrar" || modulo == "Cerrar caja" || modulo == "Ingreso / Salida de dinero" || modulo == "SUNAT")
                        {
                            rowModulos.Cells[0].Value = false;
                            rowModulos.Cells[0].ReadOnly = true;
                        }
                    }
                    if (cbxRol.Text == "Administrador" )
                    {
                        rowModulos.Cells[0].Value = true;
                        rowModulos.Cells[0].ReadOnly = true;
                    }
                }
            }

        }
        private void btnguardar_Click(object sender, EventArgs e)
        {
            Validaciones();
            if (procede == true)
            {
                insertarUsuarios();
            }

        }
        private void insertarUsuarios()
        {
            Lusuarios parametros = new Lusuarios();
            Dusuarios funcion = new Dusuarios();
            parametros.Nombre = txtnombre.Text;
            parametros.Login = txtusuario.Text;
            parametros.Password = Bases.Encriptar(txtcontraseña.Text);
            MemoryStream ms = new MemoryStream();
            Icono.Image.Save(ms, Icono.Image.RawFormat);
            parametros.Icono = ms.GetBuffer();
            parametros.Correo = txtcorreo.Text;
            parametros.Rol = cbxRol.Text;
            if (funcion.InsertarUsuarios(parametros) == true)
            {
                ObtenerIdUsuario();
                insertarPermisos();

            }
        }
        private void Validaciones()
        {
            if (!string.IsNullOrEmpty(txtnombre.Text))
            {
                if (!string.IsNullOrEmpty(txtusuario.Text))
                {
                    if (!string.IsNullOrEmpty(txtcontraseña.Text))
                    {
                        if (!string.IsNullOrEmpty(cbxRol.Text))
                        {
                            if (lblanuncioIcono.Visible == false)
                            {
                                procede = true;
                                if (string.IsNullOrEmpty(txtcorreo.Text)) { txtcorreo.Text = "-"; }

                            }
                            else
                            {
                                MessageBox.Show("Seleccione un Icono");
                                procede = false;
                            }

                        }
                        else
                        {
                            MessageBox.Show("Seleccione un rol");
                            procede = false;

                        }
                    }
                    else
                    {
                        MessageBox.Show("Ingrese la contraseña");
                        procede = false;

                    }
                }
                else
                {
                    MessageBox.Show("Ingrese el Usuario");
                    procede = false;

                }
            }
            else
            {
                procede = false;
                MessageBox.Show("Ingrese el Nombre");
            }
        }
        private void ObtenerIdUsuario()
        {
            Dusuarios funcion = new Dusuarios();
            funcion.ObtenerIdUsuario(ref idusuario, txtusuario.Text);
        }
        private void insertarPermisos()
        {
            foreach (DataGridViewRow row in datalistadoPermisos.Rows)
            {
                int idModulo = Convert.ToInt32(row.Cells["IdModulo"].Value);
                bool marcado = Convert.ToBoolean(row.Cells["Marcar"].Value);
                if (marcado == true)
                {
                    Lpermisos parametros = new Lpermisos();
                    Dpermisos funcion = new Dpermisos();
                    parametros.IdModulo = idModulo;
                    parametros.IdUsuario = idusuario;
                    if (funcion.Insertar_Permisos(parametros) == true)
                    {
                        mostrarUsuarios();
                        panelregistro.Visible = false;
                    }
                }
            }


        }

        private void AgregarIcono_Click(object sender, EventArgs e)
        {
            dlg.InitialDirectory = "";
            dlg.Filter = "Imagenes|*.jpg;*.png";
            dlg.FilterIndex = 2;
            dlg.Title = "Cargador de imagenes";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Icono.BackgroundImage = null;
                Icono.Image = new Bitmap(dlg.FileName);
                ocultarPanelIconos();
            }
        }
        private void ocultarPanelIconos()
        {
            panelIcono.Visible = false;
            lblanuncioIcono.Visible = false;
            Icono.Visible = true;
        }
        private void lblanuncioIcono_Click(object sender, EventArgs e)
        {
            EligirIcono();
        }
        private void EligirIcono()
        {
            panelIcono.Visible = true;
            panelIcono.Dock = DockStyle.Fill;
            panelIcono.BringToFront();
        }
        private void p8_Click(object sender, EventArgs e)
        {
            Icono.Image = p8.Image;
            ocultarPanelIconos();
        }

        private void p7_Click(object sender, EventArgs e)
        {
            Icono.Image = p7.Image;
            ocultarPanelIconos();
        }

        private void p6_Click(object sender, EventArgs e)
        {
            Icono.Image = p6.Image;
            ocultarPanelIconos();
        }

        private void p5_Click(object sender, EventArgs e)
        {
            Icono.Image = p5.Image;
            ocultarPanelIconos();
        }

        private void p4_Click(object sender, EventArgs e)
        {
            Icono.Image = p4.Image;
            ocultarPanelIconos();
        }

        private void p3_Click(object sender, EventArgs e)
        {
            Icono.Image = p3.Image;
            ocultarPanelIconos();
        }

        private void p2_Click(object sender, EventArgs e)
        {
            Icono.Image = p2.Image;
            ocultarPanelIconos();
        }

        private void p1_Click(object sender, EventArgs e)
        {
            Icono.Image = p1.Image;
            ocultarPanelIconos();
        }

        private void btnVolverIcono_Click(object sender, EventArgs e)
        {
            ocultarPanelIconos();
        }

        private void datalistadoUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == datalistadoUsuarios.Columns["Editar"].Index)
            {
               
                obtenerEstado();
                if (estado == "ELIMINADO")
                {
                    DialogResult resultado = MessageBox.Show("Este Usuario se Elimino. ¿Desea Volver a Habilitarlo?", "Restauracion de registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (resultado == DialogResult.OK)
                    {
                        restaurar();
                    }
                }
                else
                {
                    obtenerDatos();
                }

            }
            if (e.ColumnIndex == datalistadoUsuarios.Columns["Eliminar"].Index)
            {
                DialogResult resultado = MessageBox.Show("¿Realmente desea eliminar este Registro?", "Eliminando registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (resultado == DialogResult.OK)
                {

                    eliminarUsuarios();
                }
            }
        }
        private void restaurar()
        {
            capturarIdUsuario();
            Lusuarios parametros = new Lusuarios();
            Dusuarios funcion = new Dusuarios();
            parametros.IdUsuario = idusuario;
            if (funcion.restaurar_usuario(parametros) == true)
            {
                mostrarUsuarios();
            }
        }
        private void eliminarUsuarios()
        {
            capturarIdUsuario();
            Lusuarios parametros = new Lusuarios();
            Dusuarios funcion = new Dusuarios();
            parametros.IdUsuario = idusuario;
            if (funcion.eliminar_Usuarios(parametros) == true)
            {
                mostrarUsuarios();
            }
        }
        private void capturarIdUsuario()
        {
            idusuario = Convert.ToInt32(datalistadoUsuarios.SelectedCells[2].Value);
        }
        private void obtenerEstado()
        {
            estado = datalistadoUsuarios.SelectedCells[9].Value.ToString();
        }
        private void obtenerDatos()
        {
            capturarIdUsuario();
            txtnombre.Text = datalistadoUsuarios.SelectedCells[3].Value.ToString();
            txtusuario.Text = datalistadoUsuarios.SelectedCells[4].Value.ToString();
            txtcontraseña.Text = Bases.Desencriptar(datalistadoUsuarios.SelectedCells[5].Value.ToString());
            Icono.BackgroundImage = null;
            byte[] b = (byte[])(datalistadoUsuarios.SelectedCells[6].Value);
            MemoryStream ms = new MemoryStream(b);
            Icono.Image = Image.FromStream(ms);
            txtcorreo.Text = datalistadoUsuarios.SelectedCells[7].Value.ToString();
            cbxRol.Text = datalistadoUsuarios.SelectedCells[8].Value.ToString();

            panelregistro.Visible = true;
            panelregistro.Dock = DockStyle.Fill;
            lblanuncioIcono.Visible = false;
            btnActualizar.Visible = true;
            btnguardar.Visible = false;
            dibujarModulos();
            mostrarPermisos();
            ValidarRoles();
        }
        private void mostrarPermisos()
        {
            DataTable dt = new DataTable();
            Dpermisos funcion = new Dpermisos();
            var parametros = new Lpermisos();
            parametros.IdUsuario = idusuario;
            funcion.mostrar_PermisosXid(ref dt, parametros);

            foreach (DataRow rowPermisos in dt.Rows)
            {
                int idmoduloPermisos = Convert.ToInt32(rowPermisos["IdModulo"]);
                foreach (DataGridViewRow rowModulos in datalistadoPermisos.Rows)
                {
                    int idmodulo = Convert.ToInt32(rowModulos.Cells["IdModulo"].Value);
                    if (idmoduloPermisos == idmodulo)
                    {
                        rowModulos.Cells[0].Value = true;

                    }
                }
            }

        }
        private void btnvolver_Click(object sender, EventArgs e)
        {
            panelregistro.Visible = false;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Validaciones();
            if (procede == true)
            {
                editarUsuarios();
            }
        }
        private void editarUsuarios()
        {
            Lusuarios parametros = new Lusuarios();
            Dusuarios funcion = new Dusuarios();
            parametros.IdUsuario = idusuario;
            parametros.Nombre = txtnombre.Text;
            parametros.Login = txtusuario.Text;
            parametros.Password = Bases.Encriptar(txtcontraseña.Text);
            MemoryStream ms = new MemoryStream();
            Icono.Image.Save(ms, Icono.Image.RawFormat);
            parametros.Icono = ms.GetBuffer();
            parametros.Correo = txtcorreo.Text;
            parametros.Rol = cbxRol.Text;
            if (funcion.editar_Usuarios(parametros) == true)
            {
                eliminarPermisos();
                insertarPermisos();
            }
        }
        private void eliminarPermisos()
        {
            Lpermisos parametros = new Lpermisos();
            Dpermisos funcion = new Dpermisos();
            parametros.IdUsuario = idusuario;
            funcion.Eliminar_Permisos(parametros);


        }

        private void txtcontraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void Icono_Click(object sender, EventArgs e)
        {
            EligirIcono();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
