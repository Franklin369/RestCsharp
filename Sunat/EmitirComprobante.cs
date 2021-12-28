using RestCsharp.Datos;
using Sunat.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace RestCsharp.Sunat
{
    public class EmitirComprobante
    {
        string PassCertificado;
        string PassSecundario;
        string UserSecundario;
        string RutaCertificado;
        string Servidor;
        string Rucemisor;
        string Razonsocial;
        private void Mostrardatosempresa()
        {
            var funcion = new Dempresa();
            var dt = new DataTable();
            funcion.mostrar_empresa(ref dt);
            PassCertificado = dt.Rows[0][21].ToString();
            UserSecundario = dt.Rows[0][22].ToString();
            PassSecundario = dt.Rows[0][23].ToString();
            Rucemisor = dt.Rows[0][14].ToString();
            RutaCertificado = dt.Rows[0][20].ToString();
            Servidor = dt.Rows[0][19].ToString();
            Razonsocial = dt.Rows[0][15].ToString();
        }
        public int EmitirFacturasContado(Lventas parametrosPasar, List<Ldetalleventas> detalleventa)
        {
            Mostrardatosempresa();
            var parametros = new Lventas();
            parametros.Serie = parametrosPasar.Serie;
            parametros.Correlativo = parametrosPasar.Correlativo;
            parametros.fecha_venta = parametrosPasar.fecha_venta;
            parametros.Fecha_de_pago = parametrosPasar.Fecha_de_pago;
            parametros.CodigoComprobante = parametrosPasar.CodigoComprobante;
            parametros.contadorProductos = parametrosPasar.contadorProductos;
            parametros.EmpresaRUCemisor = parametrosPasar.EmpresaRUCemisor;
            parametros.EmpresaRazonsocialEmisora = parametrosPasar.EmpresaRazonsocialEmisora;
            parametros.DptoempresaEmisora = parametrosPasar.DptoempresaEmisora;
            parametros.ProvempresaEmisora = parametrosPasar.ProvempresaEmisora;
            parametros.DistmpresaEmisora = parametrosPasar.DistmpresaEmisora;
            parametros.DireccionEmpresaEmisora = parametrosPasar.DireccionEmpresaEmisora;
            parametros.Ubigeo = parametrosPasar.Ubigeo.Trim();

            parametros.EmpresaRUCcliente = parametrosPasar.EmpresaRUCcliente;
            parametros.EmpresaRazonsocialCliente = parametrosPasar.EmpresaRazonsocialCliente;
            parametros.DireccionCliente = parametrosPasar.DireccionCliente;
            parametros.TotalIgv = decimal.Round(parametrosPasar.TotalIgv, 2);
            parametros.TotSubtotal = parametrosPasar.TotSubtotal;
            parametros.Monto_total = parametrosPasar.Monto_total;
            parametros.Porcentaje_IGV = parametrosPasar.Porcentaje_IGV;

            //Agregamos el detalle de la venta
            List<Ldetalleventas> detalles = new List<Ldetalleventas>();
            foreach (var item in detalleventa)
            {
                var datadv = new Ldetalleventas();
                datadv.Unidad_de_medida = item.Unidad_de_medida;
                datadv.cantidad = item.cantidad;
                datadv.Total_a_pagar = item.Total_a_pagar;
                datadv.preciounitario = item.preciounitario;
                datadv.mtoValorVentaItem = datadv.Total_a_pagar;
                datadv.porIgvItem = parametrosPasar.TotalIgv;
                datadv.Descripcion = item.Descripcion;
                datadv.Codigo = item.Codigo;
                datadv.CodigoProdSunat = item.CodigoProdSunat;
                detalles.Add(datadv);
            }
            parametros.Detalles = detalles;




            var envios = new Envios();
            envios.Rutaxml = Path.GetDirectoryName(Application.ExecutablePath) + @"\XML\";
            envios.Ruta_Certificado = RutaCertificado;
            envios.Password_Certificado = PassCertificado;
            envios.RutaEnvios = Path.GetDirectoryName(Application.ExecutablePath) + @"\ENVIOS\";
            envios.RutaCDR = Path.GetDirectoryName(Application.ExecutablePath) + @"\CDR\";
            envios.Servidor = Servidor;
            envios.Usuariosecundario = Rucemisor + UserSecundario;
            envios.Passsecundario = PassSecundario;

            try
            {

                envios.GenerarFacturaBoletaXML(parametros);
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                return 0;
            }
        }
        public int EmitirNotaCredito(Lventas parametrosPasar, List<Ldetalleventas> detalleventa)
        {
            Mostrardatosempresa();
            var parametros = new Lventas();
            parametros.Cab_Ref_Serie = parametrosPasar.Cab_Ref_Serie;
            parametros.Cab_Ref_Numero = parametrosPasar.Cab_Ref_Numero;
            parametros.Cab_Ref_Motivo = parametrosPasar.Cab_Ref_Motivo;
            parametros.Cab_Ref_TipoComprobante = parametrosPasar.Cab_Ref_TipoComprobante;
            parametros.CodigoTipoNotacredito = parametrosPasar.CodigoTipoNotacredito;

            parametros.Serie = parametrosPasar.Serie;
            parametros.Correlativo = parametrosPasar.Correlativo;
            parametros.fecha_venta = parametrosPasar.fecha_venta;
            parametros.Fecha_de_pago = parametrosPasar.Fecha_de_pago;
            parametros.CodigoComprobante = parametrosPasar.CodigoComprobante;
            parametros.contadorProductos = parametrosPasar.contadorProductos;
            parametros.EmpresaRUCemisor = parametrosPasar.EmpresaRUCemisor;
            parametros.EmpresaRazonsocialEmisora = parametrosPasar.EmpresaRazonsocialEmisora;
            parametros.DptoempresaEmisora = parametrosPasar.DptoempresaEmisora;
            parametros.ProvempresaEmisora = parametrosPasar.ProvempresaEmisora;
            parametros.DistmpresaEmisora = parametrosPasar.DistmpresaEmisora;
            parametros.DireccionEmpresaEmisora = parametrosPasar.DireccionEmpresaEmisora;
            parametros.Ubigeo = parametrosPasar.Ubigeo.Trim();

            parametros.EmpresaRUCcliente = parametrosPasar.EmpresaRUCcliente;
            parametros.EmpresaRazonsocialCliente = parametrosPasar.EmpresaRazonsocialCliente;
            parametros.DireccionCliente = parametrosPasar.DireccionCliente;
            parametros.TotalIgv = decimal.Round(parametrosPasar.TotalIgv, 2);
            parametros.TotSubtotal = parametrosPasar.TotSubtotal;
            parametros.Monto_total = parametrosPasar.Monto_total;
            parametros.Porcentaje_IGV = parametrosPasar.Porcentaje_IGV;

            //Agregamos el detalle de la venta
            List<Ldetalleventas> detalles = new List<Ldetalleventas>();
            foreach (var item in detalleventa)
            {
                var datadv = new Ldetalleventas();
                datadv.Unidad_de_medida = item.Unidad_de_medida;
                datadv.cantidad = item.cantidad;
                datadv.Total_a_pagar = item.Total_a_pagar;
                datadv.preciounitario = item.preciounitario;
                datadv.mtoValorVentaItem = datadv.Total_a_pagar;
                datadv.porIgvItem = parametrosPasar.TotalIgv;
                datadv.Descripcion = item.Descripcion;
                datadv.Codigo = item.Codigo;
                detalles.Add(datadv);
            }
            parametros.Detalles = detalles;




            var envios = new Envios();
            envios.Rutaxml = Path.GetDirectoryName(Application.ExecutablePath) + @"\XML\";
            envios.Ruta_Certificado = RutaCertificado;
            envios.Password_Certificado = PassCertificado;
            envios.RutaEnvios = Path.GetDirectoryName(Application.ExecutablePath) + @"\ENVIOS\";
            envios.RutaCDR = Path.GetDirectoryName(Application.ExecutablePath) + @"\CDR\";
            envios.Servidor = Servidor;
            envios.Usuariosecundario = Rucemisor + UserSecundario;
            envios.Passsecundario = PassSecundario;

            try
            {

                envios.GenerarNotaCredito(parametros);
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
        public int EmitirNotaDebito(Lventas parametrosPasar, List<Ldetalleventas> detalleventa)
        {
            Mostrardatosempresa();
            var parametros = new Lventas();
            parametros.Cab_Ref_Serie = parametrosPasar.Cab_Ref_Serie;
            parametros.Cab_Ref_Numero = parametrosPasar.Cab_Ref_Numero;
            parametros.Cab_Ref_Motivo = parametrosPasar.Cab_Ref_Motivo;
            parametros.Cab_Ref_TipoComprobante = parametrosPasar.Cab_Ref_TipoComprobante;
            parametros.CodigoTipoNotacredito = parametrosPasar.CodigoTipoNotacredito;

            parametros.Serie = parametrosPasar.Serie;
            parametros.Correlativo = parametrosPasar.Correlativo;
            parametros.fecha_venta = parametrosPasar.fecha_venta;
            parametros.Fecha_de_pago = parametrosPasar.Fecha_de_pago;
            parametros.CodigoComprobante = parametrosPasar.CodigoComprobante;
            parametros.contadorProductos = parametrosPasar.contadorProductos;
            parametros.EmpresaRUCemisor = parametrosPasar.EmpresaRUCemisor;
            parametros.EmpresaRazonsocialEmisora = parametrosPasar.EmpresaRazonsocialEmisora;
            parametros.DptoempresaEmisora = parametrosPasar.DptoempresaEmisora;
            parametros.ProvempresaEmisora = parametrosPasar.ProvempresaEmisora;
            parametros.DistmpresaEmisora = parametrosPasar.DistmpresaEmisora;
            parametros.DireccionEmpresaEmisora = parametrosPasar.DireccionEmpresaEmisora;
            parametros.Ubigeo = parametrosPasar.Ubigeo.Trim();

            parametros.EmpresaRUCcliente = parametrosPasar.EmpresaRUCcliente;
            parametros.EmpresaRazonsocialCliente = parametrosPasar.EmpresaRazonsocialCliente;
            parametros.DireccionCliente = parametrosPasar.DireccionCliente;
            parametros.TotalIgv = decimal.Round(parametrosPasar.TotalIgv, 2);
            parametros.TotSubtotal = parametrosPasar.TotSubtotal;
            parametros.Monto_total = parametrosPasar.Monto_total;
            parametros.Porcentaje_IGV = parametrosPasar.Porcentaje_IGV;

            //Agregamos el detalle de la venta
            List<Ldetalleventas> detalles = new List<Ldetalleventas>();
            foreach (var item in detalleventa)
            {
                var datadv = new Ldetalleventas();
                datadv.Unidad_de_medida = item.Unidad_de_medida;
                datadv.cantidad = item.cantidad;
                datadv.Total_a_pagar = item.Total_a_pagar;
                datadv.preciounitario = item.preciounitario;
                datadv.mtoValorVentaItem = datadv.Total_a_pagar;
                datadv.porIgvItem = parametrosPasar.TotalIgv;
                datadv.Descripcion = item.Descripcion;
                datadv.Codigo = item.Codigo;
                detalles.Add(datadv);
            }
            parametros.Detalles = detalles;




            var envios = new Envios();
            envios.Rutaxml = Path.GetDirectoryName(Application.ExecutablePath) + @"\XML\";
            envios.Ruta_Certificado = RutaCertificado;
            envios.Password_Certificado = PassCertificado;
            envios.RutaEnvios = Path.GetDirectoryName(Application.ExecutablePath) + @"\ENVIOS\";
            envios.RutaCDR = Path.GetDirectoryName(Application.ExecutablePath) + @"\CDR\";
            envios.Servidor = Servidor;
            envios.Usuariosecundario = Rucemisor + UserSecundario;
            envios.Passsecundario = PassSecundario;

            try
            {

                envios.GenerarNotaDebito(parametros);
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
        public string[] ObtenerRespuestaZIPSunat(string ruta)
        {
            FileInfo arch = new FileInfo(ruta);
            if (arch.Extension == ".zip")
            {
                return LeerRespuestaCDR(ruta, Path.GetDirectoryName(ruta));
            }
            else
            {
                return null;
            }
        }
        public string[] LeerRespuestaCDR(string ruta, string nomfile)
        {
            string r = "";
            string file = "";
            string[] datos = new string[3];
            try
            {
                using (ZipArchive zip = ZipFile.Open(ruta, ZipArchiveMode.Read))
                {
                    ZipArchiveEntry zentry = null;
                    file = zip.Entries[1].ToString();
                    zentry = zip.GetEntry(file);
                    XmlDocument xd = new XmlDocument();
                    xd.Load(zentry.Open());
                    XmlNodeList xnl = xd.GetElementsByTagName("cbc:Description");
                    foreach (XmlElement item in xnl)
                    {
                        r = item.InnerText;
                    }
                    //MessageBox.Show(r);

                }
            }
            catch (Exception)
            {
            }
            datos[0] = r;
            datos[1] = file;
            datos[2] = nomfile;
            return datos;
        }
        public int EmitirResumenBoletas(List<Lresumendiario> Listaresumen, string fecha)
        {
            Mostrardatosempresa();
            List<Lresumendiario> resumen = new List<Lresumendiario>();
            foreach (var parametrosPasar in Listaresumen)
            {
                var parametros = new Lresumendiario();
                parametros.id = parametrosPasar.id;
                parametros.FechaEmision = parametrosPasar.FechaEmision;
                parametros.FechaGeneracion = parametrosPasar.FechaGeneracion;
                parametros.IdTipoComp = parametrosPasar.IdTipoComp;
                parametros.IdTipoDoc = parametrosPasar.IdTipoDoc;
                parametros.NumDoc = parametrosPasar.NumDoc;
                parametros.NumeroComprobante = parametrosPasar.NumeroComprobante;
                parametros.IdMoneda = parametrosPasar.IdMoneda;
                parametros.TOT_VALOR_VENTA = parametrosPasar.TOT_VALOR_VENTA;
                parametros.TOT_OPI = parametrosPasar.TOT_OPI;
                parametros.TOT_OPE = parametrosPasar.TOT_OPE;
                parametros.TOT_OPG = parametrosPasar.TOT_OPG;
                parametros.TOT_OPOT = parametrosPasar.TOT_OPOT;
                parametros.TOT_OTOT = parametrosPasar.TOT_OTOT;
                parametros.TOT_ISC = parametrosPasar.TOT_ISC;
                parametros.TOT_IGV = parametrosPasar.TOT_IGV;
                parametros.TOT_NETO = parametrosPasar.TOT_NETO;
                parametros.TipoDocRef = parametrosPasar.TipoDocRef;
                parametros.SerieDocModifica = parametrosPasar.SerieDocModifica;
                parametros.NumeroDocModifica = parametrosPasar.NumeroDocModifica;
                parametros.tipRegPercepcion = parametrosPasar.tipRegPercepcion;
                parametros.porPercepcion = parametrosPasar.porPercepcion;
                parametros.monBasePercepcion = parametrosPasar.monBasePercepcion;
                parametros.monPercepcion = parametrosPasar.monPercepcion;
                parametros.monTotIncPercepcion = parametrosPasar.monTotIncPercepcion;
                parametros.Adicionar = parametrosPasar.Adicionar;
                resumen.Add(parametros);
            }



            var envios = new Envios();
            envios.Rutaxml = Path.GetDirectoryName(Application.ExecutablePath) + @"\XML\";
            envios.Ruta_Certificado = RutaCertificado;
            envios.Password_Certificado = PassCertificado;
            envios.RutaEnvios = Path.GetDirectoryName(Application.ExecutablePath) + @"\ENVIOS\";
            envios.RutaCDR = Path.GetDirectoryName(Application.ExecutablePath) + @"\CDR\";
            envios.Servidor = Servidor;
            envios.Usuariosecundario = Rucemisor + UserSecundario;
            envios.Passsecundario = PassSecundario;
            try
            {
                envios.GenerarResumenDiario_XML(fecha, Rucemisor, Razonsocial, resumen);
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
        public string EmitirComBaja(string Tipodoc, string Serie, string NroDoc,
            string motivo)
        {
            string respuesta = "";
            Mostrardatosempresa();
            DateTime fecha = DateTime.Now;
            var envios = new Envios();
            string rutaXml = Path.GetDirectoryName(Application.ExecutablePath) + @"\XML\";
            envios.Rutaxml = Path.GetDirectoryName(Application.ExecutablePath) + @"\XML\";
            envios.Ruta_Certificado = RutaCertificado;
            envios.Password_Certificado = PassCertificado;
            envios.RutaEnvios = Path.GetDirectoryName(Application.ExecutablePath) + @"\ENVIOS\";
            envios.RutaCDR = Path.GetDirectoryName(Application.ExecutablePath) + @"\CDR\";
            envios.Servidor = Servidor;
            envios.Usuariosecundario = Rucemisor + UserSecundario;
            envios.Passsecundario = PassSecundario;
            try
            {
                respuesta = envios.GenerarComunicacionBaja_XML(fecha, Rucemisor, Razonsocial, Tipodoc, Serie, NroDoc, motivo, RutaCertificado, PassCertificado, rutaXml);
                return respuesta;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return respuesta;
            }
        }
        public string ObtenerrespuestaCbaja(string ticket)
        {
            Mostrardatosempresa();
            string respuesta = "";
            var envios = new Envios();
            envios.Rutaxml = Path.GetDirectoryName(Application.ExecutablePath) + @"\XML\";
            envios.Ruta_Certificado = RutaCertificado;
            envios.Password_Certificado = PassCertificado;
            envios.RutaEnvios = Path.GetDirectoryName(Application.ExecutablePath) + @"\ENVIOS\";
            envios.RutaCDR = Path.GetDirectoryName(Application.ExecutablePath) + @"\CDR\";
            envios.Servidor = Servidor;
            envios.Usuariosecundario = Rucemisor + UserSecundario;
            envios.Passsecundario = PassSecundario;
            respuesta = envios.ObtenerEstado(ticket);

            return respuesta;
        }
    }
}
