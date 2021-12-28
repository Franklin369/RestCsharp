using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestCsharp.Logica
{
    public class Lgastosvarios
    {
        public int Idgasto { get; set; }
        public DateTime Fecha { get; set; }
        public double Importe { get; set; }
        public string Descripcion { get; set; }
        public int Id_concepto { get; set; }
        public int Id_usuario { get; set; }
        public int Idmovcaja { get; set; }

    }
}
