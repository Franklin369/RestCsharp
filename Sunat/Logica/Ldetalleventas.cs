using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunat.Logica
{
   public  class Ldetalleventas
    {
        public int iddetalle_venta { get; set; }
        public int idventa { get; set; }
        public int Id_producto { get; set; }
        public decimal  cantidad { get; set; }
        public decimal preciounitario { get; set; }
        public string Moneda { get; set; }
        public decimal Total_a_pagar { get; set; }
        public string Unidad_de_medida { get; set; }
        public string Estado { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }
        public double  Costo { get; set; }
        public double  Ganancia { get; set; }
        public decimal mtoValorVentaItem { get; set; }
        public decimal porIgvItem { get; set; }
        public string CodigoProdSunat { get; set; }
        public string Nota { get; set; }
        public string Estado_de_pago { get; set; }
        public string Donde_se_consumira { get; set; }

    }
}
