using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace RestCsharp.Sunat
{
    public partial class Pruebassunat : Form
    {
        public Pruebassunat()
        {
            InitializeComponent();
        }

        private void btngenerarxml_Click(object sender, EventArgs e)
        {
            //Cabecera del xml
            CreditNoteType Factura = new CreditNoteType();
            Factura.Cac = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2";
            Factura.Cbc = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2";
            Factura.Ccts = "urn:un:unece:uncefact:documentation:2";
            Factura.Ds = "http://www.w3.org/2000/09/xmldsig#";
            Factura.Ext = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2";
            Factura.Qdt = "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2";
            Factura.Udt = "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2";
            UBLExtensionType[] ublextensiones = new UBLExtensionType[11];
            UBLExtensionType ublExtension = new UBLExtensionType();
            ublextensiones[0] = ublExtension;
            Factura.UBLExtensions = ublextensiones;

            //Otorgamos la version UBL y la version del esquema del documento
            Factura.UBLVersionID = new UBLVersionIDType();
            Factura.UBLVersionID.Value = "2.1";
            Factura.CustomizationID = new CustomizationIDType();
            Factura.CustomizationID.Value = "2.0";

            //Ingresar serie y numero de comprobante
            Factura.ID = new IDType();
            Factura.ID.Value = "F001" + "-" + "00000001";


            //Generar el xml
            XmlWriterSettings propiedades = new XmlWriterSettings();
            propiedades.Indent = true;
            propiedades.IndentChars = "\t";
            string rutaxml = Path.GetDirectoryName(Application.ExecutablePath) + @"\factura.xml";
            using (XmlWriter escribir =XmlWriter.Create(rutaxml,propiedades))
            {
                Type serializacion = typeof(InvoiceType);
                XmlSerializer crearxml = new XmlSerializer(serializacion);
                crearxml.Serialize(escribir, Factura);
            }

        }

    }
}
