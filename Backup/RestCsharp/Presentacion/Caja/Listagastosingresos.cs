using RestCsharp.Datos;
using RestCsharp.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RestCsharp.Presentacion.Caja
{
    public partial class Listagastosingresos : Form
    {
        public Listagastosingresos()
        {
            InitializeComponent();
        }
        int idconcepto;
        int idusuario;
        DateTime fechaInicial;
        DateTime fechafinal;
        private void Listagastosingresos_Load(object sender, EventArgs e)
        {
            fechafinal = DateTime.Now;
            Mostrar_cierres_de_caja_pendiente();
            listar_gastos();
            listar_ingresos();
            MostrarIdusuario();
        }
        private void MostrarIdusuario()
        {
            DataTable dt = new DataTable();
            DiniciosSesion funcion = new DiniciosSesion();
            funcion.mostrarInicioSesionTable(ref dt);
            idusuario = Convert.ToInt32(dt.Rows[0][1]);
        }
        private void mostrar_datalistadoConceptos()
        {
            datalistadoConceptos.Visible = true;
            datalistadoConceptos.Location = new Point(PanelDetalle.Location.X, PanelDetalle.Location.Y);
            datalistadoConceptos.Size = new System.Drawing.Size(588, 414);
            datalistadoConceptos.BringToFront();
            panelSalida.Visible = true;
            panelSalida.Size = new Size(panelPrincipal.Width, panelPrincipal.Height);
            panelSalida.Location = new Point(panelPrincipal.Location.X, panelPrincipal.Location.Y);
            panelIngreso.Visible = false;
            buscarConceptos();
        }
        private void sumar_gastos()
        {
            double total = 0;
            foreach (DataGridViewRow fila in datalistadoGastos.Rows)
            {
                total += Convert.ToDouble((fila.Cells["Importe"].Value));
            }
            lbltotalGastos.Text = Convert.ToString(total);
        }
        private void sumar_Ingresos()
        {
            double total = 0;
            foreach (DataGridViewRow fila in datalistadoIngresos.Rows)
            {
                total += Convert.ToDouble((fila.Cells["Importe"].Value));
            }
            lbltotalIngresos.Text = Convert.ToString(total);
        }
        private void listar_gastos()
        {
            DataTable dt = new DataTable();
            Dgastos funcion = new Dgastos();
            funcion.mostrarGastosPorCaja(ref dt);
            datalistadoGastos.DataSource = dt;
            datalistadoGastos.Columns[1].Visible = false;
            datalistadoGastos.Columns[5].Visible = false;
            datalistadoGastos.Columns[6].Visible = false;
            datalistadoGastos.Columns[7].Visible = false;

            Bases diseño = new Bases();
            diseño.DiseñoDatagridview(ref datalistadoGastos);
            sumar_gastos();
        }
        private void listar_ingresos()
        {
            DataTable dt = new DataTable();
            Dingresos funcion = new Dingresos();
            funcion.mostrar_Ingresosvarios(ref dt);
            datalistadoIngresos.DataSource = dt;
            Bases diseño = new Bases();
            diseño.DiseñoDatagridview(ref datalistadoIngresos);
            datalistadoIngresos.Columns[1].Visible = false;
            datalistadoIngresos.Columns[5].Visible = false;
            datalistadoIngresos.Columns[6].Visible = false;


            sumar_Ingresos();
        }
        private void Mostrar_cierres_de_caja_pendiente()
        {
            //DataTable dt = new DataTable();
            //Obtener_datos.mostrar_cierre_de_caja_pendiente(ref dt);
            //foreach (DataRow dr in dt.Rows)
            //{
            //    idcaja = Convert.ToInt32(dr["Id_caja"]);
            //    fechaInicial = Convert.ToDateTime(dr["fechainicio"]);
            //}
        }
        private void buscarConceptos()
        {
            DataTable dt = new DataTable();
            Dconceptos funcion = new Dconceptos();
            funcion.buscarConceptos(ref dt, txtBuscarconcepto.Text);
            datalistadoConceptos.DataSource = dt;
            Bases diseño = new Bases();
            diseño.DiseñoDatagridview(ref datalistadoConceptos);
            datalistadoConceptos.Columns[1].Visible = false;
            datalistadoConceptos.Visible = true;
        }
        private void mostrar_panelconceptos()
        {
            panelConceptos.Visible = true;
            panelConceptos.Dock = DockStyle.Fill;
            panelConceptos.BringToFront();
        }
        private void ocultar_panelConceptos()
        {
            panelConceptos.Visible = false;
            panelConceptos.Dock = DockStyle.None;
        }
        private void GuardarConceptos()
        {
            Dconceptos funcion = new Dconceptos();
            Lconceptos parametros = new Lconceptos();
            parametros.descripcion = txtdescripcionConcepto.Text;
            if (funcion.insertar_Conceptos(parametros) == true)
            {
                buscarConceptos();
                ocultar_panelConceptos();
            }
        }
        private void editarConceptos()
        {
            Dconceptos funcion = new Dconceptos();
            Lconceptos parametros = new Lconceptos();
            parametros.descripcion = txtdescripcionConcepto.Text;
            parametros.idconcepto = idconcepto;

            if (funcion.editarConceptos(parametros) == true)
            {
                ocultar_panelConceptos();
                buscarConceptos();
                txtBuscarconcepto.Text = txtdescripcionConcepto.Text;
            }
            else
            {
                txtdescripcionConcepto.SelectAll();
            }
        }
        private void LimpiarSalida()
        {
            txtimporte.Clear();
            txtdetalle.Clear();
            txtBuscarconcepto.Clear();
        }
        private void rellenar_campos_vaciosGastos()
        {
            txtimporte.Text = txtimporte.Text.Replace(",", ".");
            if (string.IsNullOrEmpty(txtdetalle.Text))
            {
                txtdetalle.Text = "Sin detallar";
            }

        }
        private void InsertarGastos()
        {
            Dgastos funcion = new Dgastos();
            Lgastosvarios parametros = new Lgastosvarios();
            parametros.Fecha = DateTime.Now;
            parametros.Importe = Convert.ToDouble(txtimporte.Text);
            parametros.Descripcion = txtdetalle.Text;
            parametros.Id_concepto = idconcepto;
            parametros.Id_usuario = idusuario;
            if (funcion.Insertar_Gastosvarios(parametros) == true)
            {
                listar_gastos();
                panelSalida.Visible = false;
                panelPrincipal.Visible = true;
            }

        }
        private void LimpiarIngresos()
        {
            panelIngreso.Visible = true;
            panelPrincipal.Visible = false;
            txtmontoIngreso.Clear();
            txtdescripcionIngreso.Clear();
        }
        private void rellenar_campos_vaciosIngresos()
        {
            txtmontoIngreso.Text = txtmontoIngreso.Text.Replace(",", ".");
            if (string.IsNullOrEmpty(txtdescripcionIngreso.Text))
            {
                txtdescripcionIngreso.Text = "Sin detallar";
            }

        }
        private void InsertarIngresos()
        {
            Dingresos funcion = new Dingresos();
            Lingresos parametros = new Lingresos();
            parametros.Fecha = DateTime.Now;
            parametros.Importe = Convert.ToDouble(txtmontoIngreso.Text);
            parametros.Descripcion = txtdescripcionIngreso.Text;
            parametros.IdUsuario = idusuario;
            if (funcion.Insertar_Ingresosvarios(parametros) == true)
            {
                panelIngreso.Visible = false;
                panelPrincipal.Visible = true;
                listar_ingresos();
            }
        }

        private void datalistadoGastos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == datalistadoGastos.Columns["EliminarG"].Index)
            {
                DialogResult result = MessageBox.Show("¿Realmente desea eliminar este Gasto?", "Eliminando registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    int idgasto = Convert.ToInt32(datalistadoGastos.SelectedCells[1].Value);
                    Dgastos funcion = new Dgastos();
                    Lgastosvarios parametros = new Lgastosvarios();
                    parametros.Idgasto = idgasto;
                    if (funcion.Eliminar_Gastosvarios(parametros) == true)
                    {
                        listar_gastos();
                    }

                }
            }
        }

        private void datalistadoIngresos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == datalistadoIngresos.Columns["EliminarI"].Index)
            {
                DialogResult result = MessageBox.Show("¿Realmente desea eliminar este Ingreso?", "Eliminando registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    int idingreso = Convert.ToInt32(datalistadoIngresos.SelectedCells[1].Value);
                    Dingresos funcion = new Dingresos();
                    Lingresos parametros = new Lingresos();
                    parametros.Idingreso = idingreso;
                    if (funcion.Eliminar_Ingresosvarios(parametros) == true)
                    {
                        listar_ingresos();
                    }

                }
            }
        }

        private void txtBuscarconcepto_TextChanged(object sender, EventArgs e)
        {
            buscarConceptos();
        }

        private void btnagregar_Click(object sender, EventArgs e)
        {
            mostrar_panelconceptos();
            btnguardarConceptos.Visible = true;
            btnguardarcambiosConceptos.Visible = false;
            txtdescripcionConcepto.Clear();
        }

        private void btnvolver_Click(object sender, EventArgs e)
        {
            ocultar_panelConceptos();
        }

        private void btnguardarConceptos_Click(object sender, EventArgs e)
        {
            GuardarConceptos();
        }

        private void btnguardarcambiosConceptos_Click(object sender, EventArgs e)
        {
            editarConceptos();
        }

        private void btngasto_Click(object sender, EventArgs e)
        {

            mostrar_datalistadoConceptos();
            panelPrincipal.Visible = false;
            LimpiarSalida();
        }

        private void txtBuscarconcepto_Click(object sender, EventArgs e)
        {
            btnTecladoStandar.AddControl = txtBuscarconcepto;
            btnTecladoStandar.Visible = true;
            btnTecladoNumer.Visible = false;
        }

        private void datalistadoConceptos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idconcepto = Convert.ToInt32(datalistadoConceptos.SelectedCells[1].Value);
            txtBuscarconcepto.Text = datalistadoConceptos.SelectedCells[2].Value.ToString();
            datalistadoConceptos.Visible = false;
            if (e.ColumnIndex == datalistadoConceptos.Columns["Editar"].Index)
            {
                mostrar_panelconceptos();
                btnguardarcambiosConceptos.Visible = true;
                btnguardarConceptos.Visible = false;
                txtdescripcionConcepto.Text = txtBuscarconcepto.Text;
            }
        }

        private void txtimporte_Click(object sender, EventArgs e)
        {
            btnTecladoNumer.Visible = true;
            btnTecladoStandar.Visible = false;
            btnTecladoNumer.AddControl = txtimporte;
        }

        private void txtimporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Separador_de_Numeros(txtimporte, e);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            rellenar_campos_vaciosGastos();
            if (!string.IsNullOrEmpty(txtimporte.Text))
            {
                if (Convert.ToDouble(txtimporte.Text) > 0)
                {
                    InsertarGastos();
                }
            }
        }

        private void txtdetalle_Click(object sender, EventArgs e)
        {
            btnTecladoNumer.Visible = false;
            btnTecladoStandar.Visible = true;
            btnTecladoStandar.AddControl = txtdetalle;
        }

        private void btnvolverGastos_Click(object sender, EventArgs e)
        {
            panelSalida.Visible = false;
            panelPrincipal.Visible = true;
        }

        private void txtmontoIngreso_Click(object sender, EventArgs e)
        {
            btnTecladoNumer.Visible = true;
            btnTecladoStandar.Visible = false;
            btnTecladoNumer.AddControl = txtmontoIngreso;
        }

        private void btnIngreso_Click(object sender, EventArgs e)
        {
            LimpiarIngresos();
        }

        private void btnvolverIngreso_Click(object sender, EventArgs e)
        {
            panelIngreso.Visible = false;
            panelPrincipal.Visible = true;
        }

        private void btnGuardarIngreso_Click(object sender, EventArgs e)
        {
            rellenar_campos_vaciosIngresos();
            if (!string.IsNullOrEmpty(txtmontoIngreso.Text))
            {
                if (Convert.ToDouble(txtmontoIngreso.Text) > 0)
                {
                    InsertarIngresos();
                }
            }
        }

        private void txtmontoIngreso_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Separador_de_Numeros(txtmontoIngreso, e);
        }
    }
    
}
