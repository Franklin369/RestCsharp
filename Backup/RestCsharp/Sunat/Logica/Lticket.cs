using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunat.Logica
{
   public class Lticket
    {
        public int Id_ticket { get; set; }
        public int Id_Empresa { get; set; }
        public string Nombre_de_Moneda { get; set; }
        public string Agradecimiento { get; set; }
        public string pagina_Web_Facebook { get; set; }
        public string Anuncio { get; set; }
        public string Datos_fiscales_de_autorizacion { get; set; }
        public string Por_defecto { get; set; }
        public int Idventa { get; set; }
        public string Totalletras { get; set; }

    }
}
