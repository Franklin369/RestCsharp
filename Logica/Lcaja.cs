using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestCsharp.Logica
{
  public   class Lcaja
    {
        public int Id_Caja { get; set; }
        public string Descripcion { get; set; }
        public string Tema { get; set; }
        public string Serial_PC { get; set; }
        public string Estado { get; set; }
        public string Tipo { get; set; }
        public string Ruta_para_copias_de_seguridad { get; set; }
        public string Ultima_fecha_de_copia_de_seguridad { get; set; }
        public DateTime Ultima_fecha_de_copia_date { get; set; }
        public int Frecuencia_de_copias { get; set; }
        public string Estado_Copia_De_seguridad { get; set; }

    }
}
