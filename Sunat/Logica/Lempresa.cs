using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunat.Logica
{
   public  class Lempresa
    {
        public int Id_empresa { get; set; }
        public byte[] Logo { get; set; }
        public string Impuesto { get; set; }
        public double  Porcentaje_impuesto { get; set; }
        public string SimboloMoneda { get; set; }
        public string Trabajas_con_impuestos { get; set; }
        public string Modo_de_busqueda { get; set; }
        public string Carpeta_para_copias_de_seguridad { get; set; }
        public string Correo_para_envio_de_reportes { get; set; }
        public string Ultima_fecha_de_copia_de_seguridad { get; set; }
        public DateTime Ultima_fecha_de_copia_date { get; set; }
        public int Frecuencia_de_copias { get; set; }
        public string Estado { get; set; }
        public string Tipo_de_empresa { get; set; }
        public string Tiponotas { get; set; }
        public string VersionUbl { get; set; }
        public string VersionEstDoc { get; set; }

        public string Ruc { get; set; }
        public string RazonSocial { get; set; }
        public string DireccionFiscal { get; set; }
        public string Ubigeo { get; set; }
        public string ConectarSunat { get; set; }
        public string Servidor { get; set; }
        public string CarpetaCertificado { get; set; }
        public string Passcertificado { get; set; }
        public string UserSecundario { get; set; }
        public string PassSecundario { get; set; }
        public string CodMoneda { get; set; }

    }
}
