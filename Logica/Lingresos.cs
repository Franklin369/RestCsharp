using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestCsharp.Logica
{
    public class Lingresos
    {
        public int Idingreso { get; set; }
        public DateTime Fecha { get; set; }
        public double Importe { get; set; }
        public string Descripcion { get; set; }
        public int Idmovcaja { get; set; }
        public int IdUsuario { get; set; }

    }
}
