using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Sunat.Logica
{
    public class Lventas
    {
        public int idventa { get; set; }
        public int idclientev { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fecha_venta { get; set; }
        public decimal Monto_total { get; set; }
        public string Tipo_de_pago { get; set; }
        public string Estado { get; set; }
        public decimal TotalIgv { get; set; }
        public string Serie { get; set; }
        public string Correlativo { get; set; }
        public int Id_usuario { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha_de_pago { get; set; }
        public string ACCION { get; set; }
        public decimal Saldo { get; set; }
        public decimal Pago_con { get; set; }
        public decimal Porcentaje_IGV { get; set; }
        public int Id_caja { get; set; }
        public string Referencia_tarjeta { get; set; }
        public decimal Vuelto { get; set; }
        public decimal Efectivo { get; set; }
        public decimal Credito { get; set; }
        public decimal Tarjeta { get; set; }
        public string CodigoComprobante { get; set; }
        public int Idcomprobante { get; set; }

        public int contadorProductos { get; set; }
        public string EmpresaRUCemisor { get; set; }
        public string EmpresaRUCcliente { get; set; }

        public string EmpresaRazonsocialEmisora { get; set; }
        public string EmpresaRazonsocialCliente { get; set; }

        public string Ubigeo { get; set; }


        public string DptoempresaEmisora { get; set; }
        public string ProvempresaEmisora { get; set; }
        public string DistmpresaEmisora { get; set; }
        public string DireccionEmpresaEmisora { get; set; }
        public string DireccionCliente { get; set; }


        public decimal TotSubtotal { get; set; }
        public virtual ICollection<Ldetalleventas> Detalles { get; set; }
        public string Cab_Ref_Motivo { get; set; }
        public string Cab_Ref_Serie { get; set; }
        public string Cab_Ref_TipoComprobante { get; set; }
        public string Cab_Ref_Numero { get; set; }
        public string CodigoTipoNotacredito { get; set; }
        public string CodigoTipoIdentificacion { get; set; }
        public int Id_mesa { get; set; }
        public int Numero_personas { get; set; }
        public string NombreLlevar { get; set; }
        public string Nota { get; set; }
        public string Tiposolicitud { get; set; }
        public string Totalletras { get; set; }
    }
}
