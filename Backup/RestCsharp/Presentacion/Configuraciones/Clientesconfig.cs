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
using Sunat.Logica;


namespace RestCsharp.Presentacion.Configuraciones
{
    public partial class Clientesconfig : UserControl
    {
        public Clientesconfig()
        {
            InitializeComponent();
        }
        int idcliente;
        string estado;
        string tipoDoc;
        //Crud--------
        private void insertar()
        {
            validarTipodoc();
            Lclientes  parametros = new Lclientes();
            var funcion = new Dclientes();
            parametros.Nombre = txtnombre.Text;
            parametros.Nrodoc = txtnroDoc.Text;
            parametros.Celular = txtcelular.Text;
            parametros.Direccion = txtdireccion.Text;
            parametros.Tipodoc = tipoDoc;
            if (funcion.insertar_clientes(parametros) == true)
            {
                mostrar();
            }

        }
        private void mostrar()
        {
            DataTable dt = new DataTable();
            var funcion = new Dclientes();
            funcion.mostrar_clientes(ref dt);
            datalistado.DataSource = dt;
            Panelregistro.Visible = false;
            pintarDatalistado();
        }
        private void editar()
        {
            validarTipodoc();
            Lclientes  parametros = new Lclientes();
            var funcion = new Dclientes();
            parametros.idcliente = idcliente;
            parametros.Nombre = txtnombre.Text;
            parametros.Nrodoc = txtnroDoc.Text;
            parametros.Celular = txtcelular.Text;
            parametros.Direccion = txtdireccion.Text;
            parametros.Tipodoc = tipoDoc;
            if (funcion.editar_clientes (parametros) == true)
            {
                mostrar();
            }
        }
        private void eliminar()
        {
            try
            {
                Lclientes  parametros = new Lclientes();
                var funcion = new Dclientes();
                parametros.idcliente = idcliente;
                if (funcion.eliminar_clientes(parametros) == true)
                {
                    mostrar();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
        }
        private void restaurar()
        {
            Lclientes  parametros = new Lclientes();
            var  funcion = new Dclientes();
            parametros.idcliente = idcliente;
            if (funcion.restaurar_clientes (parametros) == true)
            {
                mostrar();
            }

        }
        private void buscar()
        {
            DataTable dt = new DataTable();
            var funcion = new Dclientes();
            funcion.buscar_clientes(ref dt, txtbusca.Text);
            datalistado.DataSource = dt;
            pintarDatalistado();
        }
        //------------
        private void pintarDatalistado()
        {
            var diseño = new Bases();
            diseño.DiseñoDatagridview(ref datalistado);
            datalistado.Columns[2].Visible = false;
            foreach (DataGridViewRow row in datalistado.Rows)
            {
                string estado = Convert.ToString(row.Cells["Estado"].Value);
                if (estado == "ELIMINADO")
                {
                    row.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Strikeout | FontStyle.Bold);
                    row.DefaultCellStyle.ForeColor = Color.Red;
                }

            }
        }
        private void ClientesOk_Load(object sender, EventArgs e)
        {
            mostrar();
        }

        private void pNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
         
        }
        private void Nuevo()
        {
            Panelregistro.Visible = true;
            limpiar();
            btnGuardar.Visible = true;
            btnGuardarCambios.Visible = false;
            txtnombre.Focus();
            Panelregistro.Dock = DockStyle.Fill;
        }
        private void limpiar()
        {
            txtnombre.Clear();
            txtcelular.Clear();
            txtdireccion.Clear();
            txtnroDoc.Clear();
        }
        private void validarTipodoc()
        {
            if(Rdni.Checked==true)
            {
                tipoDoc = "DNI";
            }
            if (RRuc.Checked == true)
            {
                tipoDoc = "RUC";
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtnombre.Text))
            {
                rellenarCamposVacios();
                insertar();
            }
            else
            {
                MessageBox.Show("Ingrese un nombre", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        private void rellenarCamposVacios()
        {
            if (string.IsNullOrEmpty(txtcelular.Text)) { txtcelular.Text = "-"; };
            if (string.IsNullOrEmpty(txtdireccion.Text)) { txtdireccion.Text = "-"; };
            if (string.IsNullOrEmpty(txtnroDoc.Text)) { txtnroDoc.Text = "-"; };

        }

        private void datalistado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == datalistado.Columns["Editar"].Index)
            {
                obtenerDatos();
            }
            if (e.ColumnIndex == datalistado.Columns["Eliminar"].Index)
            {
                obtenerId_estado();
                if (estado == "ACTIVO")
                {
                    DialogResult result = MessageBox.Show("¿Realmente desea eliminar este Registro?", "Eliminando registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        eliminar();
                    }
                }
            }
        }
        private void obtenerId_estado()
        {
            try
            {
                idcliente = Convert.ToInt32(datalistado.SelectedCells[2].Value);
                estado = datalistado.SelectedCells[7].Value.ToString();

            }
            catch (Exception)
            {

            }
        }
        private void obtenerDatos()
        {
            try
            {
                idcliente = Convert.ToInt32(datalistado.SelectedCells[2].Value);
                txtnombre.Text = datalistado.SelectedCells[3].Value.ToString();
                txtdireccion.Text = datalistado.SelectedCells[4].Value.ToString();
                txtnroDoc.Text = datalistado.SelectedCells[5].Value.ToString();
                txtcelular.Text = datalistado.SelectedCells[6].Value.ToString();
                estado = datalistado.SelectedCells[7].Value.ToString();
                tipoDoc= datalistado.SelectedCells[9].Value.ToString();
                if(tipoDoc=="RUC")
                {
                    RRuc.Checked = true;
                }
                else
                {
                    Rdni.Checked = true;
                }
                if (estado == "ELIMINADO")
                {
                    DialogResult result = MessageBox.Show("Este Proveedor se Elimino. ¿Desea Volver a Habilitarlo?", "Restaurando registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        restaurar();
                        prepararEdicion();
                    }

                }
                else
                {
                    prepararEdicion();
                }
            }
            catch (Exception)
            {

            }
        }

        private void prepararEdicion()
        {
            Panelregistro.Visible = true;
            Panelregistro.Dock = DockStyle.Fill;
            btnGuardar.Visible = false;
            btnGuardarCambios.Visible = true;
        }

        private void BtnVolver_Click(object sender, EventArgs e)
        {
            Panelregistro.Visible = false;
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtnombre.Text))
            {
                rellenarCamposVacios();
                editar();
            }
            else
            {
                MessageBox.Show("Ingrese un nombre", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void txtbusca_TextChanged(object sender, EventArgs e)
        {
            buscar();
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void Rdni_CheckedChanged(object sender, EventArgs e)
        {
            
        }
    }
}
