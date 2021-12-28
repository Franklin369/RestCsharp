using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunat.Logica
{
   public class Lresumendiario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string FechaEmision { get; set; }
        public string FechaGeneracion { get; set; }
        public string IdTipoComp { get; set; }
        public string IdTipoDoc { get; set; }
        public string NumDoc { get; set; }
        public string NumeroComprobante { get; set; }
        public string IdMoneda { get; set; }
        public decimal TOT_VALOR_VENTA { get; set; }
        public decimal TOT_OPI { get; set; }
        public decimal TOT_OPE { get; set; }
        public decimal TOT_OPG { get; set; }
        public decimal TOT_OPOT { get; set; }
        public decimal TOT_OTOT { get; set; }
        public decimal TOT_ISC { get; set; }
        public decimal TOT_IGV { get; set; }
        public decimal TOT_NETO { get; set; }
        public string TipoDocRef { get; set; }
        public string SerieDocModifica { get; set; }
        public string NumeroDocModifica { get; set; }
        public string tipRegPercepcion { get; set; }
        public string porPercepcion { get; set; }
        public string monBasePercepcion { get; set; }
        public string monPercepcion { get; set; }
        public string monTotIncPercepcion { get; set; }
        public string Adicionar { get; set; }
        public virtual ICollection<Ldetalleventas> resumen { get; set; }

    }
}
