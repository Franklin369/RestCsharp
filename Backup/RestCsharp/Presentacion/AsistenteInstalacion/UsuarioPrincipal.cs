using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RestCsharp.Datos;
using RestCsharp.Logica;
using Sunat.Logica;

namespace RestCsharp.Presentacion.AsistenteInstalacion
{
    public partial class UsuarioPrincipal : Form
    {
        public UsuarioPrincipal()
        {
            InitializeComponent();
        }
        int idsalon;
        int idusuario;
        int idcaja;
        private void UsuarioPrincipal_Load(object sender, EventArgs e)
        {
            CentrarPaneles();
        }
        private void CentrarPaneles()
        {
            Panel2.Location = new Point((Width - Panel2.Width) / 2, (Height - Panel2.Height) / 2);

        }
        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtnombre.Text))
            {
                if (!string.IsNullOrEmpty(TXTCONTRASEÑA.Text))
                {
                    if (TXTCONTRASEÑA.Text == txtconfirmarcontraseña.Text)
                    {
                        insertarUsuarioDefecto();
                    }
                    else
                    {
                        MessageBox.Show("Las contraseñas no coinsiden", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Falta ingresar la Contraseña", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else
            {
                MessageBox.Show("Falta ingresar el Nombre", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void insertarComprobantes()
        {
            var funcion = new Dserealizacion();
            //**** ticket
            funcion.Insertar_Serializacion();
           

        }
        private void InsertacodigosUbigeo()
        {
            var funcion = new Dcodigosubigeo();
            funcion.Insertarcodigosubigeo();
        }
        private void InsertarCodProdSunat()
        {
            var funcion = new DcodigosProdsunat();
            funcion.InsertarcodigosProductosSunat();
        }
        private void InsertarTipoNc()
        {
            var funcion = new Dtiponotascredito();
            funcion.InsertarTipoNc();
        }
        private void InsertarUmedida()
        {
            var funcion = new DunidadM();
            funcion.InsertarUmedida();
        }
        private void InsertarTipoNd()
        {
            var funcion = new Dnotasdebito();
            funcion.InsertarTiposNd();
        }
        private void insertarUsuarioDefecto()
        {
            Lusuarios parametros = new Lusuarios();
            Dusuarios funcion = new Dusuarios();
            parametros.Nombre = txtnombre.Text;
            parametros.Login = TXTUSUARIO.Text;
            parametros.Password = Bases.Encriptar(TXTCONTRASEÑA.Text);
            MemoryStream ms = new MemoryStream();
            Icono.Image.Save(ms, Icono.Image.RawFormat);
            parametros.Icono = ms.GetBuffer();
            parametros.Correo = "-";
            parametros.Rol = "Administrador";
            if (funcion.InsertarUsuarios(parametros) == true)
            {
                InsertarCodProdSunat();
                InsertacodigosUbigeo();
                InsertarTipoNc();
                InsertarTipoNd();
                InsertarUmedida();



                InsertarLicencia();
                InsertarUsuarioCliente();
                InsertarAreasImpresion();
                insertarCaja();
                insertarCajaRemota();
                insertarComprobantes();
                insertarSalonDefecto();
                insertarMesaLlevar();
                insertar_mesasDefecto();
                insertarPropidadesMesas();
                Insertar_Modulos();
                InsertarTicket();
                InsertarClienteStandar();
                ObtenerIdUsuario();
                insertarMovcajaremota();
                InsertarColores();
                insertarPermisos();


            }
        }
        private void InsertarLicencia()
        {
            var funcion = new Dlicencias();
            funcion.InsertarLicencia();
        }
        private void InsertarTicket()
        {
            var funcion = new Dticketventa();
            funcion.Insertar_Ticket();
        }
        private void InsertarUsuarioCliente()
        {
            Lusuarios parametros = new Lusuarios();
            Dusuarios funcion = new Dusuarios();
            parametros.Nombre = "Cliente";
            parametros.Login = "Cliente";
            parametros.Password = Bases.Encriptar("Cliente369");
            MemoryStream ms = new MemoryStream();
            Icono.Image.Save(ms, Icono.Image.RawFormat);
            parametros.Icono = ms.GetBuffer();
            parametros.Correo = "Cliente";
            parametros.Rol = "Cliente";
            funcion.InsertarUsuarios(parametros);
        }
        private void insertarMesaLlevar()
        {
            var funcion = new Dmesas();
            funcion.insertarMesaLlevar();
        }
        private void InsertarClienteStandar()
        {
            var funcion = new Dclientes();
            var parametros = new Lclientes();
            parametros.Nombre = "GENERICO";
            parametros.Nrodoc = "-";
            parametros.Celular = "GENERICO";
            parametros.Direccion = "GENERICO";
            parametros.Tipodoc = "GENERICO";
            funcion.insertar_clientes(parametros);
        }

        private void InsertarAreasImpresion()
        {
            var funcion = new Dareasimpresion();
            funcion.InsertarAreasImpresion();
        }
        private void InsertarColores()
        {
            Dcolores funcion = new Dcolores();
            funcion.InsertarColores();
        }
        private void insertarPermisos()
        {
            DataTable dt = new DataTable();
            Dmodulos funcionModulos = new Dmodulos();
            funcionModulos.mostrar_Modulos(ref dt);
            foreach (DataRow row in dt.Rows)
            {
                int idModulo = Convert.ToInt32(row["IdModulo"]);
                Lpermisos parametros = new Lpermisos();
                Dpermisos funcion = new Dpermisos();
                parametros.IdModulo = idModulo;
                parametros.IdUsuario = idusuario;
                funcion.Insertar_Permisos(parametros);
               
            }
            MessageBox.Show("!LISTO! RECUERDA que para Iniciar Sesión tu Usuario es: " + TXTUSUARIO.Text + " y tu Contraseña es: " + TXTCONTRASEÑA.Text, "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Dispose();
            Login.LoginForm frm = new Login.LoginForm();
            frm.ShowDialog();
        }
        private void ObtenerIdUsuario()
        {
            Dusuarios funcion = new Dusuarios();
            funcion.ObtenerIdUsuario(ref idusuario, TXTUSUARIO.Text);

        }
        private void Insertar_Modulos()
        {
            Lmodulos parametros = new Lmodulos();
            Dmodulos funcion = new Dmodulos();
            var Modulos = new List<string> {"Para llevar", "Cambio de mesa", "Ver cuentas","Cerrar caja","Cocina","Administrar","Generar codigos QR","Ingreso / Salida de dinero","Cobrar","SUNAT" };
            foreach (var Modulo in Modulos )
            {
                parametros.Modulo = Modulo;
                funcion.Insertar_Modulos(parametros);
            }
        }
        private void insertarPropidadesMesas()
        {
            DpropiedadesMesas funcion = new DpropiedadesMesas();
            funcion.insertarPropidadesMesas();
        }
        private void insertar_mesasDefecto()
        {
            ObtenerIdsalon();
            Lmesas parametros = new Lmesas();
            Dmesas funcion = new Dmesas();
            parametros.Mesa = "NULO";
            parametros.Id_salon = idsalon;
            for (int i=1;i<=80;i++)
            {
                funcion.insertar_mesa(parametros);
            }

        }
        private void ObtenerIdsalon()
        {
            Dsalon funcion = new Dsalon();
            funcion.ObtenerSalonInicial(ref idsalon);
        }
        private void insertarSalonDefecto()
        {
            Lsalon parametros = new Lsalon();
            Dsalon funcion = new Dsalon();
            parametros.Salon = "Principal";
            funcion.insertar_Salon(parametros);
        }
        private void insertarCaja()
        {
            Lcaja parametros = new Lcaja();
            Dcaja funcion = new Dcaja();
            parametros.Descripcion = "Caja principal";
            parametros.Tema = "Claro";
            parametros.Tipo = "PRINCIPAL";
            funcion.Insertar_caja(parametros);
        }
        private void insertarCajaRemota()
        {
            Lcaja parametros = new Lcaja();
            Dcaja funcion = new Dcaja();
            parametros.Descripcion = "Caja remota";
            parametros.Tema = "Claro";
            parametros.Tipo = "REMOTA";
            funcion.Insertar_cajaRemota(parametros);
        }
        private void insertarMovcajaremota()
        {
            mostrarIdcajaremota();
            var parametros = new LmovientosCaja();
            var funcion = new DmovimientoCaja();
            parametros.Idusuario = idusuario;
            parametros.IdCaja =idcaja;
            funcion.insertar_movCajaremota(parametros);
        }
      private void mostrarIdcajaremota()
        {
            var funcion = new Dcaja();
            funcion.mostrarCajaRemota(ref idcaja);
        }

    }
}
