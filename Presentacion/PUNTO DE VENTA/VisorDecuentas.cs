using RestCsharp.Datos;
using RestCsharp.Logica;
using Sunat.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RestCsharp.Presentacion.PUNTO_DE_VENTA
{
    public partial class VisorDecuentas : UserControl

    {
        public delegate void ButtonClick(object sender, EventArgs e);
        public event ButtonClick volverClick;
        public VisorDecuentas()
        {
            InitializeComponent();
            btnvolver.Click += new EventHandler((sender, args) =>
            {
                volverClick?.Invoke(this, null);
            });
        }
        int idmesa1;
        string mesa;
        int filas = 0;
        int idventa1;
        string estado;
        int cantidadPersonas;
        int idventadividida;
        private void VisorDecuentas_Load(object sender, EventArgs e)
        {
            mostrarPedidos();
            lblDivisiondeCuentas.Dock = DockStyle.Fill;
        }
        private void mostrarPedidos()
        {
            datalistadoPedidos.Rows.Clear();
            filas = 0;
            DataTable dt = new DataTable();
            Dmesas funcion = new Dmesas();
            funcion.mostrarCuentasMesas(ref dt);
            foreach (DataRow row in dt.Rows)
            {
                mesa = row["Mesa"].ToString();
                idmesa1 =Convert.ToInt32( row["Id_mesa"].ToString());
                datalistadoPedidos.Rows.Add();
                datalistadoPedidos[0, filas].Value = mesa;
                datalistadoPedidos[1, filas].Value = "# Pedido";
                datalistadoPedidos[2, filas].Value = "Estado";
                datalistadoPedidos[3, filas].Value = "Idmesa";
                datalistadoPedidos.Columns[3].Visible = false;
                datalistadoPedidos.Columns[2].Visible = false;

                DataTable dtVentas = new DataTable();
                Dventas funcionVentas = new Dventas();
                Lventas parametros = new Lventas();
                parametros.Id_mesa = idmesa1;
                funcionVentas.mostrarVentasNoTerminado(ref dtVentas, parametros);
                filas += 1;
                foreach (DataRow row2 in dtVentas.Rows)
                {
                    idventa1 =Convert.ToInt32 ( row2["idventa"].ToString());
                    estado = row2["Estado"].ToString();
                    datalistadoPedidos.Rows.Add();
                    datalistadoPedidos[1, filas].Value = idventa1;
                    datalistadoPedidos[2, filas].Value = estado;
                    datalistadoPedidos[3, filas].Value = idmesa1;
                    filas += 1;

                }

            }
        }

        private void datalistadoPedidos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                idventa1 =Convert.ToInt32( datalistadoPedidos.SelectedCells[1].Value);
                idmesa1 = Convert.ToInt32(datalistadoPedidos.SelectedCells[3].Value);
                lblOrden1.Text = idventa1.ToString();
                mostrarDatosVenta();
                Listarproductosagregados1();
            }
            catch (Exception ex)
            {
              
            }
        }
        private void Listarproductosagregados1()
        {
            DataTable dt = new DataTable();
            Ddetalleventas funcion = new Ddetalleventas();
            Ldetalleventas parametros = new Ldetalleventas();
            var diseño = new Bases();
            parametros.idventa = idventa1;
            funcion.mostrarproductosporcuenta(ref dt, idmesa1, parametros);
            datalistadoDetalledeventa1.DataSource = dt;
            diseño.DiseñoDatagridview(ref datalistadoDetalledeventa1);
            datalistadoDetalledeventa1.Columns[1].Visible = false;
            datalistadoDetalledeventa1.Columns[6].Visible = false;
            datalistadoDetalledeventa1.Columns[7].Visible = false;
            datalistadoDetalledeventa1.Columns[8].Visible = false;
            datalistadoDetalledeventa1.Columns[9].Visible = false;
            datalistadoDetalledeventa1.Columns[10].Visible = false;
            sumar1();

        }
        private void Listarproductosagregados2()
        {
            DataTable dt = new DataTable();
            Ddetalleventas funcion = new Ddetalleventas();
            Ldetalleventas parametros = new Ldetalleventas();
            var diseño = new Bases();
            parametros.idventa = idventadividida;
            funcion.mostrarproductosporcuenta(ref dt, idmesa1, parametros);
            datalistadoDetalledeventa2.DataSource = dt;
            diseño.DiseñoDatagridview(ref datalistadoDetalledeventa2);
            datalistadoDetalledeventa2.Columns[1].Visible = false;
            datalistadoDetalledeventa2.Columns[6].Visible = false;
            datalistadoDetalledeventa2.Columns[7].Visible = false;
            datalistadoDetalledeventa2.Columns[8].Visible = false;
            datalistadoDetalledeventa2.Columns[9].Visible = false;
            datalistadoDetalledeventa2.Columns[10].Visible = false;
            sumar2();

        }
        private void sumar2()
        {
            double Total = 0;
            foreach (DataGridViewRow fila in datalistadoDetalledeventa2.Rows)
            {
                Total += Convert.ToDouble(fila.Cells["Importe"].Value);
                lbltotal2.Text = Total.ToString();
            }
        }
        private void sumar1()
        {
            double Total = 0;
            foreach (DataGridViewRow fila in datalistadoDetalledeventa1.Rows)
            {
                Total+= Convert.ToDouble(fila.Cells["Importe"].Value);
                txtTotal1.Text = Total.ToString();
            }
        }
        private void mostrarDatosVenta()
        {
            DataTable dt = new DataTable();
            Lventas parametros = new Lventas();
            Dventas funcion = new Dventas();
            parametros.idventa = idventa1;
            funcion.mostrarVentasVisorcuentas(ref dt, parametros);
            lblfecha1.Text = dt.Rows[0]["fecha_venta"].ToString();
            lblmozo1.Text= dt.Rows[0]["Login"].ToString();
            btnPersonas1.Text ="# Personas"+ dt.Rows[0]["Numero_personas"].ToString();
            lblmesa1.Text = dt.Rows[0]["Mesa"].ToString();
            cantidadPersonas = Convert.ToInt32(dt.Rows[0]["Numero_personas"]);

        }

        private void btnvermesas_Click(object sender, EventArgs e)
        {
            Dispose();
          //  frm.ShowDialog();
        }

        private void lblDivisiondeCuentas_Click(object sender, EventArgs e)
        {
            if (datalistadoDetalledeventa1.RowCount>1)
            {
                InsertarVenta();
                InsetarDetalleVentaDiv();
                Listarproductosagregados1();
                Listarproductosagregados2();
                mostrarPedidos();
                EditarEstadoVentasEspera();
                lblDivisiondeCuentas.Visible = false;
                Panelcuenta2.Visible = true;
                Panelcuenta2.Dock = DockStyle.Fill;
            }
        }
        private void EditarEstadoVentasEspera()
        {
            Dventas funcion = new Dventas();
            Lventas parametros = new Lventas();
            parametros.idventa = idventadividida;
            funcion.EditarEstadoVentasEspera(parametros);
        }

        private void InsetarDetalleVentaDiv()
        {
            Ldetalleventas parametros = new Ldetalleventas();
            Ddetalleventas funcion = new Ddetalleventas();
            foreach (DataGridViewRow row in datalistadoDetalledeventa1.Rows)
            {
                int iddetalleventa = Convert.ToInt32(row.Cells["iddetalle_venta"].Value);
                string Estado = (row.Cells["Estado"].Value).ToString();
                if (Estado == "SELECCIONADO")
                {
                    parametros.idventa = idventadividida;
                    parametros.iddetalle_venta = iddetalleventa;
                    funcion.insertarDetalleVentaDiv(parametros);



                }

            }

        }
        private void InsertarVenta()
        {
            Lventas parametros = new Lventas();
            Dventas funcion = new Dventas();
            parametros.Id_mesa = idmesa1;
            parametros.Numero_personas = cantidadPersonas;
            parametros.NombreLlevar = "-";
            if (funcion.Insertar_ventas (parametros)==true)
            {
                MostrarIdventaMesa();
            }
        }
        private void MostrarIdventaMesa()
        {
            Dventas funcion = new Dventas();
            Lventas parametros = new Lventas();
            parametros.Id_mesa = idmesa1;
            funcion.mostrarIdventaMesa(ref idventadividida, parametros);

        }

        private void datalistadoDetalledeventa1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
          if (datalistadoDetalledeventa1.RowCount >1)
            {
              
                    lblDivisiondeCuentas.Visible = true;
                    Panelcuenta2.Visible = false;
                    int idDvprincipal = Convert.ToInt32( datalistadoDetalledeventa1.SelectedCells[6].Value);
                    foreach (DataGridViewRow row in datalistadoDetalledeventa1.Rows)
                    {
                        string Estado = Convert.ToString(row.Cells["Estado"].Value);
                        int iddetalle_venta = Convert.ToInt32(row.Cells["iddetalle_venta"].Value);
                        if (Estado!="SELECCIONADO")
                        {
                            if (idDvprincipal == iddetalle_venta)
                            {
                                row.DefaultCellStyle.BackColor = Color.SeaGreen;
                                row.DefaultCellStyle.SelectionBackColor= Color.SeaGreen;
                                row.Cells[7].Value = "SELECCIONADO";
                            }
                        }
                        if (Estado== "SELECCIONADO")
                        {
                            if (idDvprincipal == iddetalle_venta)
                            {
                                row.Cells[7].Value = "DESELECCIONADO";
                                row.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                                row.DefaultCellStyle.SelectionBackColor = Color.WhiteSmoke;
                            }
                        }

                    }
                                     
                
            }
          else
            {
                lblDivisiondeCuentas.Visible = false;
            }

            }

        private void btnDespachar_Click(object sender, EventArgs e)
        {

        }

        private void VisorDecuentas_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
            var frm = new Visor_de_mesas();
         //   frm.ShowDialog();
        }
    }
    }

