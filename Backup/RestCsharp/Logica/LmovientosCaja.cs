using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestCsharp.Logica
{
  public   class LmovientosCaja
    {
        public int IdMovimientoCaja { get; set; }
        public DateTime fechainicio { get; set; }
        public DateTime fechafin { get; set; }
        public double ingresos { get; set; }
        public double egresos { get; set; }
        public double VEfectivo { get; set; }
        public double VCredito { get; set; }
        public double VTarjeta { get; set; }
        public int Idusuario { get; set; }
        public double EfectivoCalculado { get; set; }
        public double EfectivoReal { get; set; }
        public double EfectivoDiferencia { get; set; }
        public int IdCaja { get; set; }
        public string Estado { get; set; }
        public double EfectivoInicial { get; set; }

    }
}
