using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using RestCsharp.Datos;
using Sunat.Logica;
using RestCsharp.servicioFacturacion2021;
using RestCsharp.Sunat.Resumenes;

namespace RestCsharp.Sunat
{
    public class Envios
    {
        string versionUbl;
        string versionEstruc;
        public string Ruta_Certificado { get; set; }
        public string Password_Certificado { get; set; }
        public string Rutaxml { get; set; }
        public string RutaEnvios { get; set; }
        public string RutaCDR { get; set; }
        public string Servidor { get; set; }
        public string Usuariosecundario { get; set; }
        public string Passsecundario { get; set; }

        private void ObtenerEmpresa()
        {
            var dt = new DataTable();
            var funcion = new Dempresa();
            funcion.mostrar_empresa(ref dt);
            versionUbl = dt.Rows[0][12].ToString();
            versionEstruc = dt.Rows[0][13].ToString();

        }

        public void ConsultaComprobante()
        {

        }
        public string FirmarXML(string cRutaArchivo, string cCertificado, string cClave)
        {

            string file = cRutaArchivo;
            string text = File.ReadAllText(file);
            text = text.Replace(@"<ext:UBLExtension />", @"<ext:UBLExtension> <ext:ExtensionContent /></ext:UBLExtension>");
            text = text.Replace("xsi:type=", "");
            text = text.Replace("cbc:SerialIDType", "");
            text = text.Replace("\"\"", "");
            File.WriteAllText(file, text);
            string cRpta;
            string tipo = Path.GetFileName(cRutaArchivo);

            string local_typoDocumento = tipo.Substring(12, 2);
            string l_xpath = "";
            string f_certificat = cCertificado;
            string f_pwd = cClave;
            string xmlFile = cRutaArchivo;

            X509Certificate2 MonCertificat = new X509Certificate2(f_certificat, f_pwd);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.Load(xmlFile);
            SignedXml signedXml = new SignedXml(xmlDoc);
            signedXml.SigningKey = MonCertificat.PrivateKey;
            KeyInfo KeyInfo = new KeyInfo();
            Reference Reference = new Reference();
            Reference.Uri = "";
            Reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
            signedXml.AddReference(Reference);
            X509Chain X509Chain = new X509Chain();
            X509Chain.Build(MonCertificat);
            X509ChainElement local_element = X509Chain.ChainElements[0];
            KeyInfoX509Data x509Data = new KeyInfoX509Data(local_element.Certificate);
            string subjectName = local_element.Certificate.Subject;
            x509Data.AddSubjectName(subjectName);
            KeyInfo.AddClause(x509Data);
            signedXml.KeyInfo = KeyInfo;
            signedXml.ComputeSignature();
            XmlElement signature = signedXml.GetXml();
            signature.Prefix = "ds";
            signedXml.ComputeSignature();
            foreach (XmlNode node in signature.SelectNodes("descendant-or-self::*[namespace-uri()='http://www.w3.org/2000/09/xmldsig#']"))
            {

                if (node.LocalName == "Signature")
                {
                    XmlAttribute newAttribute = xmlDoc.CreateAttribute("Id");
                    newAttribute.Value = "SignSUNAT";
                    node.Attributes.Append(newAttribute);
                }
            }
            XmlNamespaceManager nsMgr;
            nsMgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsMgr.AddNamespace("sac", "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1");
            nsMgr.AddNamespace("ccts", "urn:un:unece:uncefact:documentation:2");
            nsMgr.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");

            switch (local_typoDocumento)
            {
                case "01":
                case "03" // factura y boleta
               :
                    {
                        nsMgr.AddNamespace("tns", "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2");
                        l_xpath = "/tns:Invoice/ext:UBLExtensions/ext:UBLExtension[1]/ext:ExtensionContent";
                        break;
                    }

                case "07" // nota de credito
         :
                    {
                        nsMgr.AddNamespace("tns", "urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2");
                        l_xpath = "/tns:CreditNote/ext:UBLExtensions/ext:UBLExtension[1]/ext:ExtensionContent";
                        break;
                    }

                case "08" // nota de debito
                    :
                    {
                        nsMgr.AddNamespace("tns", "urn:oasis:names:specification:ubl:schema:xsd:DebitNote-2");
                        l_xpath = "/tns:DebitNote/ext:UBLExtensions/ext:UBLExtension[1]/ext:ExtensionContent";
                        break;
                    }
                case "RA" // Comunicacion de baja
               :
                    {
                        nsMgr.AddNamespace("tns", "urn:sunat:names:specification:ubl:peru:schema:xsd:VoidedDocuments-1");
                        l_xpath = "/tns:VoidedDocuments/ext:UBLExtensions/ext:UBLExtension/ext:ExtensionContent";
                        break;
                    }
                case "RC" // Resumen diario
                :
                    {
                        nsMgr.AddNamespace("tns", "urn:sunat:names:specification:ubl:peru:schema:xsd:SummaryDocuments-1");
                        l_xpath = "/tns:SummaryDocuments/ext:UBLExtensions/ext:UBLExtension/ext:ExtensionContent";

                        break;
                    }

            }
            nsMgr.AddNamespace("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            nsMgr.AddNamespace("udt", "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2");
            nsMgr.AddNamespace("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
            nsMgr.AddNamespace("qdt", "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2");
            nsMgr.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            nsMgr.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
            xmlDoc.SelectSingleNode(l_xpath, nsMgr).AppendChild(xmlDoc.ImportNode(signature, true));
            xmlDoc.Save(xmlFile);
            XmlNodeList nodeList = xmlDoc.GetElementsByTagName("ds:Signature");
            if (nodeList.Count != 1)
            {
                MessageBox.Show("Problemas con la firma");
                cRpta = "Problemas con la firma";
            }
            signedXml.LoadXml((XmlElement)nodeList[0]);
            if (signedXml.CheckSignature() == false)
                cRpta = "No se logro firmar el comprobante";
            else
                cRpta = "OK";
            return cRpta;
        }
        public string ComprimirZip(string nombrearchivo, string rutadestino)
        {
            Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile();
            zip.AddFile(nombrearchivo, "");
            zip.Save(rutadestino);
            string respuesta = "Listo";
            return respuesta;
        }
        public void Enviardocumento(string Archivo)
        {
            string filezip = Archivo;
            string filepath = filezip;
            byte[] bitArray = File.ReadAllBytes(filepath);
            try
            {
                BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.TransportWithMessageCredential);
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
                binding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;
                binding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
                binding.Security.Message.AlgorithmSuite = System.ServiceModel.Security.SecurityAlgorithmSuite.Default;

                EndpointAddress remoteAddress = new EndpointAddress(Servidor);
                billServiceClient servicio = new billServiceClient(binding, remoteAddress);
                ServicePointManager.UseNagleAlgorithm = true;
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.CheckCertificateRevocationList = true;
                servicio.ClientCredentials.UserName.UserName = Usuariosecundario;
                servicio.ClientCredentials.UserName.Password = Passsecundario;

                var elements = servicio.Endpoint.Binding.CreateBindingElements();
                elements.Find<SecurityBindingElement>().EnableUnsecuredResponse = true;
                servicio.Endpoint.Binding = new CustomBinding(elements);
                servicio.Open();
                filezip = Path.GetFileName(filezip);
                byte[] returByte = servicio.sendBill(filezip, bitArray, "0");
                servicio.Close();
                filezip = Path.GetFileName(filezip);
                FileStream fs = new FileStream(RutaCDR + "R-" + filezip, FileMode.Create);
                fs.Write(returByte, 0, returByte.Length);
                fs.Close();
                var respuesta = new EmitirComprobante();
                respuesta.ObtenerRespuestaZIPSunat(RutaCDR + "R-" + filezip);
            }
            catch (FaultException ex)
            {
                MessageBox.Show(ex.Code.Name);
            }

        }

        #region Factura 
        public void GenerarFacturaBoletaXML(Lventas parametros)
        {

            //Cabecera del xml
            InvoiceType Factura = new InvoiceType();
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
            ObtenerEmpresa();
            Factura.UBLVersionID = new UBLVersionIDType();
            Factura.UBLVersionID.Value = versionUbl;
            Factura.CustomizationID = new CustomizationIDType();
            Factura.CustomizationID.Value = versionEstruc;

            //Ingresar serie y numero de comprobante
            Factura.ID = new IDType();
            Factura.ID.Value = parametros.Serie + "-" + parametros.Correlativo;
            //Fecha de emision
            Factura.IssueDate = new IssueDateType();
            string fechaemision = Convert.ToDateTime(parametros.fecha_venta).ToString("yyyy-MM-dd");
            Factura.IssueDate.Value = Convert.ToDateTime(fechaemision);
            Factura.IssueTime = new IssueTimeType();
            string hora = Convert.ToDateTime(parametros.fecha_venta).ToString("HH:mm:ss");
            Factura.IssueTime.Value = Convert.ToDateTime(hora);

            //Fecha de vencimiento
            Factura.DueDate = new DueDateType();
            string fechavencimiento = Convert.ToDateTime(parametros.Fecha_de_pago).ToString("yyyy-MM-dd");
            Factura.DueDate.Value = Convert.ToDateTime(fechavencimiento);

            //Tipo de factura
            InvoiceTypeCodeType TipoFactura = new InvoiceTypeCodeType();
            TipoFactura.listID = "0101";//Factura  de venta interna
            TipoFactura.listAgencyName = "PE:SUNAT";
            TipoFactura.listName = "Tipo de Documento";
            TipoFactura.name = "Tipo de Operacion";
            TipoFactura.listURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo01";
            TipoFactura.listSchemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo51";
            TipoFactura.Value = parametros.CodigoComprobante;
            Factura.InvoiceTypeCode = TipoFactura;

            //Leyenda del comprobante
            NoteType Leyenda = new NoteType();
            Leyenda.languageLocaleID = "1000";
            Leyenda.Value = "MONTO EN SOLES";
            List<NoteType> notas = new List<NoteType>();
            notas.Add(Leyenda);
            Factura.Note = notas.ToArray();

            //Tipo de moneda
            DocumentCurrencyCodeType moneda = new DocumentCurrencyCodeType();
            moneda.listID = "ISO 4217 Alpha";
            moneda.listName = "Currency";
            moneda.listAgencyName = "United Nations Economic Commission for Europe";
            moneda.Value = "PEN";
            Factura.DocumentCurrencyCode = moneda;

            //Cantidad de productos en el detalle de venta
            LineCountNumericType numeroProductos = new LineCountNumericType();
            numeroProductos.Value = parametros.contadorProductos;


            //Ingresar datos de la empresa emisora
            SignatureType Firma = new SignatureType();
            SignatureType[] Firmas = new SignatureType[2];
            PartyType partySign = new PartyType();
            ////Agregar el ruc Empresa emisora
            PartyIdentificationType partyIdentificacion = new PartyIdentificationType();
            PartyIdentificationType[] partyIdentificacions = new PartyIdentificationType[2];
            IDType idFirma = new IDType();
            idFirma.Value = parametros.EmpresaRUCemisor;
            Firma.ID = idFirma;
            partyIdentificacion.ID = idFirma;
            partyIdentificacions[0] = partyIdentificacion;
            partySign.PartyIdentification = partyIdentificacions;
            Firma.SignatoryParty = partySign;
            ////Agregar notas
            NoteType Nota = new NoteType();
            NoteType[] Notas = new NoteType[2];
            Nota.Value = "Elaborado por codigo 369";
            Notas[0] = Nota;
            Firma.Note = Notas;
            ////Ingresar la razon social de la empresa emisora
            PartyNameType partyName = new PartyNameType();
            PartyNameType[] partyNames = new PartyNameType[2];
            NameType1 RazonSocialFirma = new NameType1();
            RazonSocialFirma.Value = parametros.EmpresaRazonsocialEmisora;
            partyName.Name = RazonSocialFirma;
            partyNames[0] = partyName;
            partySign.PartyName = partyNames;

            AttachmentType attachType = new AttachmentType();
            ExternalReferenceType externaReferencia = new ExternalReferenceType();
            URIType uri = new URIType();
            uri.Value = parametros.EmpresaRUCemisor;
            externaReferencia.URI = uri;
            Firma.DigitalSignatureAttachment = attachType;

            attachType.ExternalReference = externaReferencia;
            Firma.DigitalSignatureAttachment = attachType;

            Firmas[0] = Firma;
            Factura.Signature = Firmas;
            //Codigo del documento de identidad de la empresa emisora
            SupplierPartyType empresa = new SupplierPartyType();
            PartyType party = new PartyType();
            PartyIdentificationType partyidentificacionN = new PartyIdentificationType();
            PartyIdentificationType[] partyidentificacionsN = new PartyIdentificationType[2];
            IDType idEmpresa = new IDType();
            idEmpresa.schemeID = "6";
            idEmpresa.schemeName = "Documento de Identidad";
            idEmpresa.schemeAgencyName = "PE:SUNAT";
            idEmpresa.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06";
            idEmpresa.Value = parametros.EmpresaRUCemisor;
            partyidentificacionN.ID = idEmpresa;
            partyidentificacionsN[0] = partyidentificacionN;
            party.PartyIdentification = partyidentificacionsN;
            ////Razon social empresa
            PartyNameType partyname = new PartyNameType();
            List<PartyNameType> partynames = new List<PartyNameType>();
            NameType1 nameEmisor = new NameType1();
            nameEmisor.Value = parametros.EmpresaRazonsocialEmisora;
            partyname.Name = nameEmisor;
            partynames.Add(partyname);
            party.PartyName = partynames.ToArray();

            //Establecimientos anexos y direccion de empresa emisora
            List<PartyLegalEntityType> partelegals = new List<PartyLegalEntityType>();
            PartyLegalEntityType partelegal = new PartyLegalEntityType();
            RegistrationNameType registronombre = new RegistrationNameType();
            registronombre.Value = parametros.EmpresaRUCemisor;
            partelegal.RegistrationName = registronombre;
            ////Direccion de la empresa emisora
            AddressType direccionPL = new AddressType();
            IDType iddireccionPL = new IDType();
            iddireccionPL.schemeAgencyName = "PE:INEI";
            iddireccionPL.schemeName = "Ubigeos";
            iddireccionPL.Value = parametros.Ubigeo;
            direccionPL.ID = iddireccionPL;
            AddressTypeCodeType anexo = new AddressTypeCodeType();
            anexo.listAgencyName = "PE:SUNAT";
            anexo.listName = "Establecimientos anexos";
            anexo.Value = "0000";
            direccionPL.AddressTypeCode = anexo;
            ////Indicar direccion fiscal de la empresa emisora
            CityNameType Departamento = new CityNameType();
            Departamento.Value = parametros.DptoempresaEmisora;
            direccionPL.CityName = Departamento;
            CountrySubentityType provincia = new CountrySubentityType();
            provincia.Value = parametros.ProvempresaEmisora;
            direccionPL.CountrySubentity = provincia;
            DistrictType distrito = new DistrictType();
            distrito.Value = parametros.DistmpresaEmisora;
            direccionPL.District = distrito;

            List<AddressLineType> direcciones = new List<AddressLineType>();
            AddressLineType direccionEmisor = new AddressLineType();
            LineType datalocal = new LineType();
            datalocal.Value = parametros.DireccionEmpresaEmisora;
            direccionPL.AddressLine = direcciones.ToArray();
            direccionEmisor.Line = datalocal;
            direcciones.Add(direccionEmisor);
            direccionPL.AddressLine = direcciones.ToArray();
            ////Pais empresa emisora
            CountryType pais = new CountryType();
            IdentificationCodeType codigoPais = new IdentificationCodeType();
            codigoPais.listName = "Country";
            codigoPais.listAgencyName = "United Nations Economic Commission for Europe";
            codigoPais.listID = "ISO 3166-1";
            codigoPais.Value = "PE";
            pais.IdentificationCode = codigoPais;
            direccionPL.Country = pais;
            partelegal.RegistrationAddress = direccionPL;
            partelegals.Add(partelegal);
            party.PartyLegalEntity = partelegals.ToArray();
            ////Agregando sublistas
            party.PartyIdentification = partyidentificacionsN;
            party.PartyName = partynames.ToArray();
            empresa.Party = party;
            Factura.AccountingSupplierParty = empresa;

            //Empresa receptora (Cliente)
            CustomerPartyType cliente = new CustomerPartyType();
            PartyType partyCliente = new PartyType();
            List<PartyIdentificationType> partyIdentificationClientes = new List<PartyIdentificationType>();
            PartyIdentificationType partyIdentificationCliente = new PartyIdentificationType();
            IDType idtipoCliente = new IDType();
            if (parametros.EmpresaRUCcliente.Length == 11)
            {
                parametros.CodigoTipoIdentificacion = "6";
            }
            else if (parametros.EmpresaRUCcliente.Length == 8)
            {
                parametros.CodigoTipoIdentificacion = "1";
                parametros.DireccionCliente = "-";
            }
            else
            {
                parametros.CodigoTipoIdentificacion = "-";
                parametros.EmpresaRUCcliente = "-";
                parametros.EmpresaRazonsocialCliente = "![CDATA[-]]";
                parametros.DireccionCliente = "-";
            }
            idtipoCliente.schemeID = parametros.CodigoTipoIdentificacion;
            idtipoCliente.schemeName = "Documento de Identidad";
            idtipoCliente.schemeAgencyName = "PE:SUNAT";
            idtipoCliente.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06";
            idtipoCliente.Value = parametros.EmpresaRUCcliente;
            partyIdentificationCliente.ID = idtipoCliente;
            partyIdentificationClientes.Add(partyIdentificationCliente);
            partyCliente.PartyIdentification = partyIdentificationClientes.ToArray();
            ////Razon social cliente
            List<PartyLegalEntityType> partylegalClientes = new List<PartyLegalEntityType>();
            PartyLegalEntityType partylegalCliente = new PartyLegalEntityType();
            RegistrationNameType razonsocialcliente = new RegistrationNameType();
            razonsocialcliente.Value = parametros.EmpresaRazonsocialCliente;
            partylegalCliente.RegistrationName = razonsocialcliente;
            ////Direccion del cliente (OPCIONAL)
            AddressType direccionclienteType = new AddressType();
            List<AddressLineType> direccionclientes = new List<AddressLineType>();
            AddressLineType direccioncliente = new AddressLineType();
            List<LineType> lineas = new List<LineType>();
            LineType linea = new LineType();
            linea.Value = parametros.DireccionCliente;
            direccioncliente.Line = linea;
            direccionclientes.Add(direccioncliente);
            direccionclienteType.AddressLine = direccionclientes.ToArray();
            partylegalClientes.Add(partylegalCliente);
            partyCliente.PartyLegalEntity = partylegalClientes.ToArray();
            cliente.Party = partyCliente;
            Factura.AccountingCustomerParty = cliente;

            #region <cac:PaymentTerms> Forma de pago
            PaymentTermsType tipopago = new PaymentTermsType();
            PaymentTermsType[] tipopagos = new PaymentTermsType[2];
            IDType idpago = new IDType();
            idpago.Value = "FormaPago";
            tipopago.ID = idpago;

            PaymentMeansIDType formapago = new PaymentMeansIDType();
            PaymentMeansIDType[] formapagos = new PaymentMeansIDType[1];
            formapago.Value = "Contado";
            formapagos[0] = formapago;
            tipopago.PaymentMeansID = formapagos;

            tipopagos[0] = tipopago;
            Factura.PaymentTerms = tipopagos;
            #endregion

            #region <cac:TaxTotal> IMPUESTOS AL TOTAL
            //<cac:TaxTotal>
            ////<cbc:TaxAmount
            TaxTotalType TotalImptos = new TaxTotalType();
            List<TaxTotalType> TotalImptosLista = new List<TaxTotalType>();

            TaxAmountType taxAmountImpto = new TaxAmountType();
            taxAmountImpto.currencyID = "PEN";
            taxAmountImpto.Value = Convert.ToDecimal(string.Format("{0:0.00}", parametros.TotalIgv));
            TotalImptos.TaxAmount = taxAmountImpto;
            ////</cbc:TaxAmount>

            ////<cac:TaxSubtotal>
            List<TaxSubtotalType> subtotales = new List<TaxSubtotalType>();
            TaxSubtotalType subtotal = new TaxSubtotalType();

            //////<cbc:TaxableAmount
            TaxableAmountType taxsubtotal = new TaxableAmountType();
            taxsubtotal.currencyID = "PEN";
            taxsubtotal.Value = Convert.ToDecimal(string.Format("{0:0.00}", parametros.TotSubtotal));
            subtotal.TaxableAmount = taxsubtotal;
            //////</cbc:TaxableAmount>

            //////<cbc:TaxAmount
            TaxAmountType TotalTaxAmountTotal = new TaxAmountType();
            TotalTaxAmountTotal.currencyID = "PEN";
            TotalTaxAmountTotal.Value = Convert.ToDecimal(string.Format("{0:0.00}", parametros.TotalIgv));
            subtotal.TaxAmount = TotalTaxAmountTotal;
            //subtotales.Add(subtotal);
            //TotalImptos.TaxSubtotal = subtotales.ToArray();
            //////</cbc:TaxAmount>

            //////<cac:TaxCategory>
            TaxCategoryType taxcategoryTotal = new TaxCategoryType();
            ////////<cac:TaxScheme>
            TaxSchemeType taxScheme = new TaxSchemeType();
            //////////<cbc:ID
            IDType idtotal = new IDType();
            idtotal.schemeName = "Codigo de tributos";
            idtotal.schemeAgencyName = "PE:SUNAT";
            idtotal.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo05";
            idtotal.Value = "1000";
            taxScheme.ID = idtotal;
            //////////</cbc:ID>
            //////////<cbc:Name>
            NameType1 nombreImpuesto = new NameType1();
            nombreImpuesto.Value = "IGV";
            taxScheme.Name = nombreImpuesto;
            //////////</cbc:Name>
            //////////<cbc:TaxTypeCode>
            TaxTypeCodeType taxtypecodeImpto = new TaxTypeCodeType();
            taxtypecodeImpto.Value = "VAT";
            taxScheme.TaxTypeCode = taxtypecodeImpto;
            //////////<cbc:TaxTypeCode>
            ////////</cac:TaxScheme>
            taxcategoryTotal.TaxScheme = taxScheme;
            //////</cac:TaxCategory>
            subtotal.TaxCategory = taxcategoryTotal;
            ////</cac:TaxSubtotal>
            subtotales.Add(subtotal);
            #region Impuesto por bolsa
            foreach (Ldetalleventas det in parametros.Detalles)
            {
                if (det.Codigo.Contains("BBBB"))
                {
                    TaxSubtotalType TotalICBPER = new TaxSubtotalType();
                    TaxAmountType taxICBPER = new TaxAmountType();
                    taxICBPER.currencyID = "PEN";
                    taxICBPER.Value = Math.Round((det.cantidad * det.preciounitario), 2);
                    TotalICBPER.TaxAmount = taxICBPER;
                    TaxCategoryType taxCategoria = new TaxCategoryType();
                    TaxSchemeType taxSchemaicb = new TaxSchemeType();
                    IDType idTaschema = new IDType();
                    idTaschema.Value = "7152";
                    NameType1 nombreICB = new NameType1();
                    nombreICB.Value = "ICBPER";
                    TaxTypeCodeType taxtypecodeICPER = new TaxTypeCodeType();
                    taxtypecodeICPER.Value = "OTH";

                    taxSchemaicb.ID = idTaschema;
                    taxSchemaicb.Name = nombreICB;
                    taxSchemaicb.TaxTypeCode = taxtypecodeICPER;

                    taxCategoria.TaxScheme = taxSchemaicb;

                    TotalICBPER.TaxCategory = taxCategoria;
                    subtotales.Add(TotalICBPER);
                    break;
                }

            }
            #endregion

            TotalImptos.TaxSubtotal = subtotales.ToArray();
            TotalImptosLista.Add(TotalImptos);
            Factura.TaxTotal = TotalImptosLista.ToArray();
            //</cac:TaxTotal>
            #endregion
            #region <cac:LegalMonetaryTotal> TOTALES
            MonetaryTotalType TotalValorVenta = new MonetaryTotalType();
            LineExtensionAmountType lineExtensionAmount = new LineExtensionAmountType();
            lineExtensionAmount.currencyID = "PEN";
            lineExtensionAmount.Value = Convert.ToDecimal(string.Format("{0:0.00}", parametros.TotSubtotal));
            TotalValorVenta.LineExtensionAmount = lineExtensionAmount;

            TaxInclusiveAmountType taxInclusiveAmount = new TaxInclusiveAmountType();
            taxInclusiveAmount.currencyID = "PEN";
            taxInclusiveAmount.Value = Convert.ToDecimal(string.Format("{0:0.00}", parametros.Monto_total));
            TotalValorVenta.TaxInclusiveAmount = taxInclusiveAmount;

            AllowanceTotalAmountType allowanceTotalAmount = new AllowanceTotalAmountType();
            allowanceTotalAmount.currencyID = "PEN";
            allowanceTotalAmount.Value = 0.00m;
            TotalValorVenta.AllowanceTotalAmount = allowanceTotalAmount;

            PrepaidAmountType prepaidAmount = new PrepaidAmountType();
            prepaidAmount.currencyID = "PEN";
            prepaidAmount.Value = 0.00m;
            TotalValorVenta.PrepaidAmount = prepaidAmount;

            ChargeTotalAmountType chargeTotalAmount = new ChargeTotalAmountType();
            chargeTotalAmount.currencyID = "PEN";
            chargeTotalAmount.Value = 0.00m;
            TotalValorVenta.ChargeTotalAmount = chargeTotalAmount;



            PayableAmountType payableAmount = new PayableAmountType();
            payableAmount.currencyID = "PEN";
            payableAmount.Value = Convert.ToDecimal(string.Format("{0:0.00}", parametros.Monto_total));
            TotalValorVenta.PayableAmount = payableAmount;
            Factura.LegalMonetaryTotal = TotalValorVenta;

            #endregion
            #region <cac:InvoiceLine> PRODUCTOS DE LA FACTURA
            List<InvoiceLineType> items = new List<InvoiceLineType>();
            int idtem = 1;
            foreach (Ldetalleventas detalle in parametros.Detalles)
            {
                InvoiceLineType item = new InvoiceLineType();
                IDType numeroItem = new IDType();
                numeroItem.Value = idtem.ToString();
                item.ID = numeroItem;

                InvoicedQuantityType cantidad = new InvoicedQuantityType();
                cantidad.unitCode = detalle.Unidad_de_medida;
                cantidad.unitCodeListID = "UN/ECE rec 20";
                cantidad.unitCodeListAgencyName = "United Nations Economic Commission for Europe";
                cantidad.Value = detalle.cantidad;
                item.InvoicedQuantity = cantidad;

                LineExtensionAmountType ValorVenta = new LineExtensionAmountType();
                ValorVenta.currencyID = "PEN";
                ValorVenta.Value = Convert.ToDecimal(string.Format("{0:0.00}", detalle.Total_a_pagar / (1 + 0.18m)));
                item.LineExtensionAmount = ValorVenta;

                PricingReferenceType ValorReferenUnitario = new PricingReferenceType();

                List<PriceType> TipoPrecios = new List<PriceType>();
                PriceType TipoPrecio = new PriceType();
                PriceAmountType PrecioMonto = new PriceAmountType();
                PrecioMonto.currencyID = "PEN";
                PrecioMonto.Value = Convert.ToDecimal(string.Format("{0:0.00}", detalle.preciounitario));
                TipoPrecio.PriceAmount = PrecioMonto;

                PriceTypeCodeType TipoPrecioCode = new PriceTypeCodeType();
                TipoPrecioCode.listName = "Tipo de Precio";
                TipoPrecioCode.listAgencyName = "PE:SUNAT";
                TipoPrecioCode.listURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo16";
                TipoPrecioCode.Value = "01";
                TipoPrecio.PriceTypeCode = TipoPrecioCode;

                TipoPrecios.Add(TipoPrecio);

                ValorReferenUnitario.AlternativeConditionPrice = TipoPrecios.ToArray();
                item.PricingReference = ValorReferenUnitario;

                List<TaxTotalType> Totales_Items = new List<TaxTotalType>();
                TaxTotalType Totales_Item = new TaxTotalType();
                TaxAmountType Total_Item = new TaxAmountType();
                Total_Item.currencyID = "PEN";
                Total_Item.Value = Convert.ToDecimal(string.Format("{0:0.00}", detalle.mtoValorVentaItem - (detalle.mtoValorVentaItem / (1.18m))));
                Totales_Item.TaxAmount = Total_Item;

                List<TaxSubtotalType> subtotal_Items = new List<TaxSubtotalType>();
                TaxSubtotalType subtotal_Item = new TaxSubtotalType();
                TaxableAmountType taxsubtotal_IGVItem = new TaxableAmountType();
                taxsubtotal_IGVItem.currencyID = "PEN";
                taxsubtotal_IGVItem.Value = Convert.ToDecimal(string.Format("{0:0.00}", detalle.mtoValorVentaItem / 1.18m));
                subtotal_Item.TaxableAmount = taxsubtotal_IGVItem;

                TaxAmountType TotalTaxAmount_IGVItem = new TaxAmountType();
                TotalTaxAmount_IGVItem.currencyID = "PEN";
                TotalTaxAmount_IGVItem.Value = Convert.ToDecimal(string.Format("{0:0.00}", detalle.mtoValorVentaItem - detalle.mtoValorVentaItem / 1.18m));
                subtotal_Item.TaxAmount = TotalTaxAmount_IGVItem;
                subtotal_Items.Add(subtotal_Item);

                TaxCategoryType taxcategory_IGVItem = new TaxCategoryType();
                PercentType1 porcentaje = new PercentType1();
                porcentaje.Value = Convert.ToDecimal(string.Format("{0:0.00}", 18));
                taxcategory_IGVItem.Percent = porcentaje;
                subtotal_Item.TaxCategory = taxcategory_IGVItem;

                TaxExemptionReasonCodeType ReasonCode = new TaxExemptionReasonCodeType();
                ReasonCode.listAgencyName = "PE:SUNAT";
                ReasonCode.listName = "Afectacion del IGV";
                ReasonCode.listURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo07";
                ReasonCode.Value = "10";
                taxcategory_IGVItem.TaxExemptionReasonCode = ReasonCode;

                TaxSchemeType taxscheme_IGVItem = new TaxSchemeType();
                IDType id2_IGVItem = new IDType();
                id2_IGVItem.schemeName = "Codigo de tributos";
                id2_IGVItem.schemeAgencyName = "PE:SUNAT";
                id2_IGVItem.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo05";
                id2_IGVItem.Value = "1000";
                taxscheme_IGVItem.ID = id2_IGVItem;

                NameType1 nombreImpto_IGVItem = new NameType1();
                nombreImpto_IGVItem.Value = "IGV";
                taxscheme_IGVItem.Name = nombreImpto_IGVItem;

                TaxTypeCodeType nombreImpto_IGVItemInter = new TaxTypeCodeType();
                nombreImpto_IGVItemInter.Value = "VAT";
                taxscheme_IGVItem.TaxTypeCode = nombreImpto_IGVItemInter;

                taxcategory_IGVItem.TaxScheme = taxscheme_IGVItem;

                //Si encuentra bolsa
                if (detalle.Codigo.Contains("BBBB"))
                {
                    TaxSubtotalType TotalIcb = new TaxSubtotalType();
                   TaxAmountType taxAmounticb = new TaxAmountType();
                    taxAmounticb.currencyID = "PEN";
                    taxAmounticb.Value = Math.Round((detalle.cantidad * detalle.preciounitario), 2);
                    BaseUnitMeasureType baseicb = new BaseUnitMeasureType();
                    baseicb.unitCode = detalle.Unidad_de_medida;
                    baseicb.Value = Convert.ToInt32(detalle.cantidad);
                    PerUnitAmountType perunicb = new PerUnitAmountType();
                    perunicb.currencyID = "PEN";
                    perunicb.Value = detalle.preciounitario;

                    TotalIcb.TaxAmount = taxAmounticb;
                    TotalIcb.BaseUnitMeasure = baseicb;

                    TaxCategoryType categoryicb = new TaxCategoryType();
                    TaxSchemeType taxicb = new TaxSchemeType();
                    IDType idtaxcat = new IDType();
                    idtaxcat.schemeID = "UN/ECE 5305";
                    idtaxcat.schemeName = "Codigo de tributos";
                    idtaxcat.schemeAgencyName = "PE:SUNAT";
                    idtaxcat.Value = "S";
                    categoryicb.ID = idtaxcat;
                    categoryicb.PerUnitAmount = perunicb;


                    IDType idicp = new IDType();
                    idicp.Value = "7152";
                    NameType1 nombreicb = new NameType1();
                    nombreicb.Value = "ICBPER";
                    TaxTypeCodeType codicb = new TaxTypeCodeType();
                    codicb.Value = "OTH";

                    taxicb.ID = idicp;
                    taxicb.Name = nombreicb;
                    taxicb.TaxTypeCode = codicb;
                    categoryicb.TaxScheme = taxicb;
                    TotalIcb.TaxCategory = categoryicb;
                    subtotal_Items.Add(TotalIcb);

                }
                Totales_Item.TaxSubtotal = subtotal_Items.ToArray();
                Totales_Items.Add(Totales_Item);
                item.TaxTotal = Totales_Items.ToArray();


                ItemType itemTipo = new ItemType();
                DescriptionType description = new DescriptionType();
                List<DescriptionType> descriptions = new List<DescriptionType>();
                description.Value = detalle.Descripcion;
                descriptions.Add(description);

                ItemIdentificationType codigoProd = new ItemIdentificationType();
                IDType id = new IDType();
                id.Value = detalle.Codigo;
                codigoProd.ID = id;
                itemTipo.Description = descriptions.ToArray();
                itemTipo.SellersItemIdentification = codigoProd;

                List<CommodityClassificationType> codSunats = new List<CommodityClassificationType>();
                CommodityClassificationType codSunat = new CommodityClassificationType();
                ItemClassificationCodeType codClas = new ItemClassificationCodeType();
                codClas.listID = "UNSPSC";
                codClas.listAgencyName = "GS1 US";
                codClas.listName = "Item Classification";
                codClas.Value = detalle.CodigoProdSunat;
                codSunat.ItemClassificationCode = codClas;
                codSunats.Add(codSunat);
                itemTipo.CommodityClassification = codSunats.ToArray();


                PriceType PrecioProducto = new PriceType();
                PriceAmountType PrecioMontoTipo = new PriceAmountType();
                PrecioMontoTipo.currencyID = "PEN";
                decimal porcentajeIgv = Convert.ToDecimal(parametros.Porcentaje_IGV / 100);

                PrecioMontoTipo.Value = Convert.ToDecimal(string.Format("{0:0.00}", detalle.preciounitario / (1 + porcentajeIgv)));
                PrecioProducto.PriceAmount = PrecioMontoTipo;


                item.Item = itemTipo;
                item.Price = PrecioProducto;
                items.Add(item);
                idtem += 1;
            }
            Factura.InvoiceLine = items.ToArray();

            #endregion
            string rutaxml = CrearArchivoxml(Factura, parametros.EmpresaRUCemisor, parametros.CodigoComprobante, parametros.Serie, parametros.Correlativo);
            FirmarXML(rutaxml, Ruta_Certificado, Password_Certificado);
            string rutaenvio = RutaEnvios + Path.GetFileName(rutaxml).Replace(".xml", ".zip");
            ComprimirZip(rutaxml, rutaenvio);
            Enviardocumento(rutaenvio);
        }
        private string CrearArchivoxml(InvoiceType Factura, string RUCEmisor, string CodigoTipoComprobante, string serie, string correlativo)
        {
            //Generar el archivo xml
            XmlWriterSettings propiedades = new XmlWriterSettings();
            propiedades.Indent = true;
            propiedades.IndentChars = "\t";
            string Nombrearchivoxml = RUCEmisor + "-" + CodigoTipoComprobante + "-" + serie + "-" + correlativo;
            string rutaxml = string.Format(@"{0}{1}.xml", Rutaxml, Nombrearchivoxml);
            using (XmlWriter escribir = XmlWriter.Create(rutaxml, propiedades))
            {
                Type serializacion = typeof(InvoiceType);
                XmlSerializer crearxml = new XmlSerializer(serializacion);
                crearxml.Serialize(escribir, Factura);
                return rutaxml;
            }
        }
        #endregion
        #region Nota de credito
        public void GenerarNotaCredito(Lventas parametros)
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
            UBLExtensionType[] ublextensiones = new UBLExtensionType[5];
            UBLExtensionType ublExtension = new UBLExtensionType();
            ublextensiones[0] = ublExtension;
            Factura.UBLExtensions = ublextensiones;

            //Otorgamos la version UBL y la version del esquema del documento
            ObtenerEmpresa();
            Factura.UBLVersionID = new UBLVersionIDType();
            Factura.UBLVersionID.Value = versionUbl;
            Factura.CustomizationID = new CustomizationIDType();
            Factura.CustomizationID.Value = versionEstruc;

            //Ingresar serie y numero de comprobante
            Factura.ID = new IDType();
            Factura.ID.Value = parametros.Serie + "-" + parametros.Correlativo;
            //Fecha de emision
            Factura.IssueDate = new IssueDateType();
            string fechaemision = Convert.ToDateTime(parametros.fecha_venta).ToString("yyyy-MM-dd");
            Factura.IssueDate.Value = Convert.ToDateTime(fechaemision);
            Factura.IssueTime = new IssueTimeType();
            string hora = Convert.ToDateTime(parametros.fecha_venta).ToString("HH:mm:ss");
            Factura.IssueTime.Value = Convert.ToDateTime(hora);

            //Tipo de moneda
            DocumentCurrencyCodeType moneda = new DocumentCurrencyCodeType();
            moneda.listID = "ISO 4217 Alpha";
            moneda.listName = "Currency";
            moneda.listAgencyName = "United Nations Economic Commission for Europe";
            moneda.Value = "PEN";
            Factura.DocumentCurrencyCode = moneda;


            #region Datos Exclusivos para nota de credito
            ResponseType DocumentoRel = new ResponseType();
            ResponseType[] DocumentoRels = new ResponseType[2];
            ReferenceIDType NumeroDocRel = new ReferenceIDType();
            NumeroDocRel.Value = parametros.Cab_Ref_Serie + "-" + parametros.Cab_Ref_Numero;
            ResponseCodeType TipoDocRel = new ResponseCodeType();
            TipoDocRel.Value = parametros.CodigoTipoNotacredito;
            DescriptionType Motivo = new DescriptionType();
            DescriptionType[] Motivos = new DescriptionType[2];
            Motivos[0] = Motivo;
            Motivo.Value = parametros.Cab_Ref_Motivo;
            DocumentoRel.ReferenceID = NumeroDocRel;
            DocumentoRel.ResponseCode = TipoDocRel;
            DocumentoRel.Description = Motivos;
            DocumentoRels[0] = DocumentoRel;
            Factura.DiscrepancyResponse = DocumentoRels;

            BillingReferenceType[] referencias = new BillingReferenceType[2];
            BillingReferenceType referencia = new BillingReferenceType();

            DocumentReferenceType documento = new DocumentReferenceType();
            IDType docRela = new IDType();
            docRela.Value = parametros.Cab_Ref_Serie + "-" + parametros.Cab_Ref_Numero;
            DocumentTypeCodeType TipoDocumentoRel = new DocumentTypeCodeType();
            TipoDocumentoRel.Value = parametros.Cab_Ref_TipoComprobante;
            documento.DocumentTypeCode = TipoDocumentoRel;
            documento.ID = docRela;
            referencia.InvoiceDocumentReference = documento;
            referencias[0] = referencia;
            Factura.BillingReference = referencias;

            #endregion

            #region nuevo

            SignatureType Firma = new SignatureType();
            SignatureType[] Firmas = new SignatureType[2];

            PartyType partySign = new PartyType();
            PartyIdentificationType partyIdentificacion = new PartyIdentificationType();
            PartyIdentificationType[] partyIdentificacions = new PartyIdentificationType[2];
            IDType idFirma = new IDType();
            idFirma.Value = parametros.EmpresaRUCemisor;
            Firma.ID = idFirma;

            partyIdentificacion.ID = idFirma;
            partyIdentificacions[0] = partyIdentificacion;
            partySign.PartyIdentification = partyIdentificacions;
            Firma.SignatoryParty = partySign;

            SupplierPartyType empresa = new SupplierPartyType();
            PartyType party = new PartyType();
            PartyIdentificationType partyidentificacion = new PartyIdentificationType();
            PartyIdentificationType[] partyidentificacions = new PartyIdentificationType[2];
            IDType idEmpresa = new IDType();

            idEmpresa.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06";
            idEmpresa.schemeName = "Documento de Identidad";
            idEmpresa.schemeID = "6";
            idEmpresa.schemeAgencyName = "PE:SUNAT";
            idEmpresa.Value = parametros.EmpresaRUCemisor;

            partyidentificacion.ID = idEmpresa;
            partyidentificacions[0] = partyidentificacion;
            party.PartyIdentification = partyidentificacions;

            PartyNameType partyname = new PartyNameType();
            List<PartyNameType> partynames = new List<PartyNameType>();
            NameType1 nameEmisor = new NameType1();
            nameEmisor.Value = parametros.EmpresaRazonsocialEmisora;
            partyname.Name = nameEmisor;
            partynames.Add(partyname);
            party.PartyName = partynames.ToArray();

            RegistrationNameType registerNameEmisor = new RegistrationNameType();
            registerNameEmisor.Value = parametros.EmpresaRazonsocialEmisora;

            //Direccion emisor                
            CompanyIDType compañia = new CompanyIDType();
            compañia.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06";
            compañia.schemeAgencyName = "PE:SUNAT";
            compañia.schemeName = "SUNAT:Identificador de Documento de Identidad";
            compañia.schemeID = "6";
            compañia.Value = parametros.EmpresaRUCemisor;

            AddressType direccion = new AddressType();
            AddressTypeCodeType addrestypecode = new AddressTypeCodeType();
            addrestypecode.listName = "Establecimientos anexos";
            addrestypecode.listAgencyName = "PE:SUNAT";
            addrestypecode.Value = "0000";

            TaxSchemeType taxSchema = new TaxSchemeType();
            IDType idsupplier = new IDType();
            idsupplier.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06";
            idsupplier.schemeAgencyName = "PE:SUNAT";
            idsupplier.schemeName = "SUNAT:Identificador de Documento de Identidad";
            idsupplier.schemeID = "6";
            idsupplier.Value = parametros.EmpresaRUCemisor;
            taxSchema.ID = idsupplier;


            List<PartyLegalEntityType> partelegals = new List<PartyLegalEntityType>();
            PartyLegalEntityType partelegal = new PartyLegalEntityType();
            RegistrationNameType registerNamePL = new RegistrationNameType();
            registerNamePL.Value = parametros.EmpresaRazonsocialEmisora;
            partelegal.RegistrationName = registerNamePL;

            AddressType direccionPL = new AddressType();
            IDType iddireccionPL = new IDType();
            iddireccionPL.schemeAgencyName = "PE:INEI";
            iddireccionPL.schemeName = "Ubigeos";
            iddireccionPL.Value = parametros.Ubigeo;
            direccionPL.ID = iddireccionPL;

            AddressTypeCodeType address_TypeCodeType = new AddressTypeCodeType();
            address_TypeCodeType.listName = "Establecimientos anexos";
            address_TypeCodeType.listAgencyName = "PE:SUNAT";
            address_TypeCodeType.Value = "0000";
            direccionPL.AddressTypeCode = address_TypeCodeType;

            CityNameType Departamento = new CityNameType();
            Departamento.Value = parametros.DptoempresaEmisora;
            direccionPL.CityName = Departamento;

            CountrySubentityType Provincia = new CountrySubentityType();
            Provincia.Value = parametros.ProvempresaEmisora;
            direccionPL.CountrySubentity = Provincia;

            DistrictType distrito = new DistrictType();
            distrito.Value = parametros.DistmpresaEmisora;
            direccionPL.District = distrito;
            List<AddressLineType> direcciones = new List<AddressLineType>();
            AddressLineType direccionEmisor = new AddressLineType();
            LineType local1 = new LineType();
            local1.Value = parametros.DireccionEmpresaEmisora;
            direccionPL.AddressLine = direcciones.ToArray();
            direccionEmisor.Line = local1;
            direcciones.Add(direccionEmisor);
            direccionPL.AddressLine = direcciones.ToArray();

            CountryType pais = new CountryType();
            IdentificationCodeType codigoPais = new IdentificationCodeType();

            codigoPais.listName = "Country";
            codigoPais.listAgencyName = "United Nations Economic Commission for Europe";
            codigoPais.listID = "ISO 3166-1";
            codigoPais.Value = "PE";
            pais.IdentificationCode = codigoPais;

            direccionPL.Country = pais;
            partelegal.RegistrationAddress = direccionPL;

            partelegals.Add(partelegal);
            party.PartyLegalEntity = partelegals.ToArray();

            party.PartyName = partynames.ToArray();
            party.PartyIdentification = partyidentificacions;
            empresa.Party = party;
            Factura.AccountingSupplierParty = empresa;

            //EMPRESA CLIENTE

            TaxSchemeType taxschemeCliente = new TaxSchemeType();
            CustomerPartyType CustomerPartyCliente = new CustomerPartyType();
            PartyType partyCliente = new PartyType();
            PartyIdentificationType partyIdentificion = new PartyIdentificationType();
            List<PartyIdentificationType> partyIdentificions = new List<PartyIdentificationType>();
            IDType idtipo = new IDType();
            idtipo.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06";
            idtipo.schemeName = "Documento de Identidad";
            idtipo.schemeAgencyName = "PE:SUNAT";
            idtipo.schemeID = "6";
            idtipo.Value = parametros.EmpresaRUCcliente;
            partyIdentificion.ID = idtipo;

            partyIdentificions.Add(partyIdentificion);
            partyCliente.PartyIdentification = partyIdentificions.ToArray();

            List<PartyNameType> RazSocClientes = new List<PartyNameType>();
            PartyNameType RazSocCliente = new PartyNameType();
            NameType1 razSocial = new NameType1();
            razSocial.Value = parametros.EmpresaRazonsocialCliente;
            RazSocCliente.Name = razSocial;
            RazSocClientes.Add(RazSocCliente);

            List<PartyTaxSchemeType> partySchemas = new List<PartyTaxSchemeType>();
            PartyTaxSchemeType partySchema = new PartyTaxSchemeType();
            RegistrationNameType RegistroNombre = new RegistrationNameType();
            RegistroNombre.Value = parametros.EmpresaRazonsocialCliente;
            partySchema.RegistrationName = RegistroNombre;

            CompanyIDType idcompañia = new CompanyIDType();
            idcompañia.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06";
            idcompañia.schemeAgencyName = "PE:SUNAT";
            idcompañia.schemeName = "SUNAT:Identificador de Documento de Identidad";
            idcompañia.schemeID = "6";
            idcompañia.Value = parametros.EmpresaRUCcliente;

            TaxSchemeType schemeType = new TaxSchemeType();
            IDType idc = new IDType();
            idc.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06";
            idc.schemeAgencyName = "PE:SUNAT";
            idc.schemeName = "SUNAT:Identificador de Documento de Identidad";
            idc.schemeID = "6";
            idc.Value = parametros.EmpresaRUCcliente;
            schemeType.ID = idc;

            idcompañia.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06";
            idcompañia.schemeAgencyName = "PE:SUNAT";
            idcompañia.schemeName = "SUNAT:Identificador de Documento de Identidad";
            idcompañia.schemeID = "6";
            idcompañia.Value = parametros.EmpresaRUCcliente;

            List<PartyLegalEntityType> partyLegals = new List<PartyLegalEntityType>();
            PartyLegalEntityType partyLegal = new PartyLegalEntityType();
            RegistrationNameType Registro_Nombre = new RegistrationNameType();
            Registro_Nombre.Value = parametros.EmpresaRazonsocialCliente;
            partyLegal.RegistrationName = Registro_Nombre;

            AddressType direccionCliente = new AddressType();
            List<AddressLineType> dirs = new List<AddressLineType>();
            AddressLineType dir = new AddressLineType();
            List<LineType> lineas = new List<LineType>();

            LineType linea = new LineType();
            linea.Value = parametros.DireccionCliente;
            dir.Line = linea;
            dirs.Add(dir);
            direccionCliente.AddressLine = dirs.ToArray();


            CountryType paisC = new CountryType();
            IdentificationCodeType codigoPaisC = new IdentificationCodeType();

            codigoPaisC.Value = "PE";
            paisC.IdentificationCode = codigoPaisC;

            partyLegals.Add(partyLegal);

            partySchema.CompanyID = idcompañia;
            partySchema.TaxScheme = schemeType;

            partySchemas.Add(partySchema);

            partyCliente.PartyLegalEntity = partyLegals.ToArray();
            CustomerPartyCliente.Party = partyCliente;

            CustomerPartyType accoutingCustomerParty = new CustomerPartyType();
            accoutingCustomerParty.Party = partyCliente;

            Factura.AccountingCustomerParty = accoutingCustomerParty;

            //Monto total de impuestos

            TaxTotalType TotalImptos = new TaxTotalType();
            TaxAmountType taxAmountImpto = new TaxAmountType();
            taxAmountImpto.currencyID = "PEN";
            taxAmountImpto.Value = Convert.ToDecimal(parametros.TotalIgv);
            TotalImptos.TaxAmount = taxAmountImpto;

            TaxSubtotalType[] subtotales = new TaxSubtotalType[2];
            TaxSubtotalType subtotal = new TaxSubtotalType();

            TaxableAmountType taxsubtotal = new TaxableAmountType();
            taxsubtotal.currencyID = "PEN";
            taxsubtotal.Value = Convert.ToDecimal(string.Format("{0:0.00}", parametros.TotSubtotal));
            subtotal.TaxableAmount = taxsubtotal;

            TaxAmountType TotalTaxAmountTotal = new TaxAmountType();
            TotalTaxAmountTotal.currencyID = "PEN";
            TotalTaxAmountTotal.Value = Convert.ToDecimal(string.Format("{0:0.00}", parametros.TotalIgv));
            subtotal.TaxAmount = TotalTaxAmountTotal;

            TaxSubtotalType subTotalIGV = new TaxSubtotalType();
            subTotalIGV.TaxableAmount = taxsubtotal;

            subtotales[0] = subtotal;
            TotalImptos.TaxSubtotal = subtotales;


            //Pago de IGV
            TaxCategoryType taxcategoryTotal = new TaxCategoryType();
            TaxSchemeType taxScheme = new TaxSchemeType();
            IDType idTotal = new IDType();
            idTotal.schemeID = "UN/ECE 5305";
            idTotal.schemeName = "Tax Category Identifier";
            idTotal.schemeAgencyName = "United Nations Economic Commission for Europe";
            idTotal.Value = "S";

            NameType1 nametypeImpto = new NameType1();
            nametypeImpto.Value = "IGV";
            TaxTypeCodeType taxtypecodeImpto = new TaxTypeCodeType();
            taxtypecodeImpto.Value = "VAT";

            IDType idTot = new IDType();

            idTot.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo05";
            idTot.schemeAgencyName = "PE:SUNAT";
            idTot.schemeName = "Codigo de tributos";
            idTot.Value = "1000";
            taxScheme.ID = idTot;

            NameType1 nametypeImptoIGV = new NameType1();
            nametypeImptoIGV.Value = "IGV";
            TaxTypeCodeType taxtypecodeImpuesto = new TaxTypeCodeType();
            taxtypecodeImpuesto.Value = "VAT";

            taxScheme.Name = nametypeImpto;
            taxScheme.TaxTypeCode = taxtypecodeImpto;
            taxcategoryTotal.TaxScheme = taxScheme;
            subtotal.TaxCategory = taxcategoryTotal;

            List<TaxSubtotalType> TaxSubtotals = new List<TaxSubtotalType>();
            TaxSubtotals.Add(subtotal);
            TotalImptos.TaxSubtotal = TaxSubtotals.ToArray();

            List<TaxTotalType> taxTotals = new List<TaxTotalType>();

            taxTotals.Add(TotalImptos);
            Factura.TaxTotal = taxTotals.ToArray();




            ////Total valor de venta 

            MonetaryTotalType TotalValorVenta = new MonetaryTotalType();
            LineExtensionAmountType TValorVenta = new LineExtensionAmountType();

            TValorVenta.currencyID = "PEN";
            TValorVenta.Value = Convert.ToDecimal(string.Format("{0:0.00}", parametros.TotSubtotal));
            TotalValorVenta.LineExtensionAmount = TValorVenta;

            TaxInclusiveAmountType TotalPrecioVenta = new TaxInclusiveAmountType();
            TotalPrecioVenta.currencyID = "PEN";
            TotalPrecioVenta.Value = Convert.ToDecimal(string.Format("{0:0.00}", parametros.Monto_total));


            AllowanceTotalAmountType MtoTotalDsctos = new AllowanceTotalAmountType();
            MtoTotalDsctos.currencyID = "PEN";
            MtoTotalDsctos.Value = Convert.ToDecimal(0);

            ChargeTotalAmountType MtoTotalOtrosCargos = new ChargeTotalAmountType();
            MtoTotalOtrosCargos.currencyID = "PEN";
            MtoTotalOtrosCargos.Value = Convert.ToDecimal(string.Format("{0:0.00}", 0));
            TotalValorVenta.ChargeTotalAmount = MtoTotalOtrosCargos;

            PrepaidAmountType MtoCargos = new PrepaidAmountType();
            MtoCargos.currencyID = "PEN";
            MtoCargos.Value = Convert.ToDecimal(string.Format("{0:0.00}", 0));
            MtoCargos.Value = Convert.ToDecimal(string.Format("{0:0.00}", 0));
            TotalValorVenta.PrepaidAmount = MtoCargos;

            PayableAmountType ImporteTotalVenta = new PayableAmountType();
            ImporteTotalVenta.currencyID = "PEN";
            ImporteTotalVenta.Value = Convert.ToDecimal(string.Format("{0:0.00}", parametros.Monto_total));

            TotalValorVenta.LineExtensionAmount = TValorVenta;
            TotalValorVenta.TaxInclusiveAmount = TotalPrecioVenta;
            TotalValorVenta.AllowanceTotalAmount = MtoTotalDsctos;
            TotalValorVenta.ChargeTotalAmount = MtoTotalOtrosCargos;
            TotalValorVenta.PrepaidAmount = MtoCargos;
            TotalValorVenta.PayableAmount = ImporteTotalVenta;
            Factura.LegalMonetaryTotal = TotalValorVenta;


            //Items de Factura
            CreditNoteLineType[] items =
                new CreditNoteLineType[10];
            int iditem = 1;

            foreach (Ldetalleventas det in parametros.Detalles)
            {
                CreditNoteLineType item =
                    new CreditNoteLineType();
                IDType numeroItem = new IDType();
                numeroItem.Value = iditem.ToString();
                item.ID = numeroItem;

                CreditedQuantityType cantidad = new CreditedQuantityType();
                cantidad.unitCodeListAgencyName = "United Nations Economic Commission for Europe";
                cantidad.unitCodeListID = "UN/ECE rec 20";
                cantidad.unitCode = det.Unidad_de_medida;

                cantidad.Value = Convert.ToInt32(det.cantidad);
                item.CreditedQuantity = cantidad;

                LineExtensionAmountType ValorVenta = new LineExtensionAmountType();
                ValorVenta.currencyID = "PEN";
                ValorVenta.Value = Convert.ToDecimal(string.Format("{0:0.00}", det.Total_a_pagar / 1.18m));
                item.LineExtensionAmount = ValorVenta;

                //Precio de venta unitario por item y código 
                PricingReferenceType ValorReferenUnitario = new PricingReferenceType();

                PriceType[] TipoPrecios = new PriceType[2];
                PriceType TipoPrecio = new PriceType();

                PriceAmountType PrecioMonto = new PriceAmountType();


                PrecioMonto.currencyID = "PEN";
                PrecioMonto.Value = Convert.ToDecimal(string.Format("{0:0.00}", det.preciounitario));
                TipoPrecio.PriceAmount = PrecioMonto;

                PriceTypeCodeType TipoPrecioCode = new PriceTypeCodeType();
                TipoPrecioCode.listName = "Tipo de Precio";
                TipoPrecioCode.listAgencyName = "PE:SUNAT";
                TipoPrecioCode.listURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo16";
                TipoPrecioCode.Value = "01";


                TipoPrecio.PriceTypeCode = TipoPrecioCode;
                TipoPrecios[0] = TipoPrecio;
                ValorReferenUnitario.AlternativeConditionPrice = TipoPrecios;
                item.PricingReference = ValorReferenUnitario;

                TaxTotalType[] Totales_Items = new TaxTotalType[2];
                TaxTotalType Totales_Item = new TaxTotalType();

                TaxAmountType Total_Item = new TaxAmountType();
                Total_Item.currencyID = "PEN";
                Total_Item.Value = Convert.ToDecimal(string.Format("{0:0.00}", det.mtoValorVentaItem - (det.mtoValorVentaItem / 1.18m)));
                Totales_Item.TaxAmount = Total_Item;

                TaxSubtotalType[] subtotal_Items = new TaxSubtotalType[2];
                TaxSubtotalType subtotal_Item = new TaxSubtotalType();

                TaxableAmountType taxsubtotal_IGVItem = new TaxableAmountType();
                taxsubtotal_IGVItem.currencyID = "PEN";
                taxsubtotal_IGVItem.Value = Convert.ToDecimal(string.Format("{0:0.00}", det.mtoValorVentaItem / 1.18m));
                subtotal_Item.TaxableAmount = taxsubtotal_IGVItem;

                TaxAmountType TotalTaxAmount_IGVItem = new TaxAmountType();
                TotalTaxAmount_IGVItem.currencyID = "PEN";
                TotalTaxAmount_IGVItem.Value = Convert.ToDecimal(string.Format("{0:0.00}", det.mtoValorVentaItem - (det.mtoValorVentaItem / 1.18m)));
                subtotal_Item.TaxAmount = TotalTaxAmount_IGVItem;

                subtotal_Items[0] = subtotal_Item;
                Totales_Item.TaxSubtotal = subtotal_Items;

                TaxCategoryType taxcategory_IGVItem = new TaxCategoryType();

                IDType idTaxCategoria = new IDType();
                idTaxCategoria.schemeAgencyName = "United Nations Economic Commission for Europe";
                idTaxCategoria.schemeName = "Tax Category Identifier";
                idTaxCategoria.schemeID = "UN/ECE 5305";
                idTaxCategoria.Value = "S";


                PercentType1 porcentaje = new PercentType1();
                porcentaje.Value = Convert.ToDecimal(string.Format("{0:0.00}", 18));
                taxcategory_IGVItem.Percent = porcentaje;
                subtotal_Item.TaxCategory = taxcategory_IGVItem;

                TaxExemptionReasonCodeType ReasonCode = new TaxExemptionReasonCodeType();
                ReasonCode.listURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo07";
                ReasonCode.listName = "Afectacion del IGV";
                ReasonCode.listAgencyName = "PE:SUNAT";
                ReasonCode.Value = "10";

                taxcategory_IGVItem.TaxExemptionReasonCode = ReasonCode;

                TaxSchemeType taxscheme_IGVItem = new TaxSchemeType();
                IDType id2_IGVItem = new IDType();

                id2_IGVItem.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo05";
                id2_IGVItem.schemeAgencyName = "PE:SUNAT";
                id2_IGVItem.schemeName = "Codigo de tributos";


                id2_IGVItem.Value = "1000";
                taxscheme_IGVItem.ID = id2_IGVItem;

                NameType1 nombreImpto_IGVItem = new NameType1();
                nombreImpto_IGVItem.Value = "IGV";
                taxscheme_IGVItem.Name = nombreImpto_IGVItem;

                TaxTypeCodeType nombreImpto_IGVItemInter = new TaxTypeCodeType();
                nombreImpto_IGVItemInter.Value = "VAT";
                taxscheme_IGVItem.TaxTypeCode = nombreImpto_IGVItemInter;
                taxscheme_IGVItem.Name = nombreImpto_IGVItem;


                taxcategory_IGVItem.TaxScheme = taxscheme_IGVItem;

                subtotal_Items[0] = subtotal_Item;
                Totales_Item.TaxSubtotal = subtotal_Items;
                Totales_Items[0] = Totales_Item;

                item.TaxTotal = Totales_Items;

                DescriptionType[] descriptions = new DescriptionType[2];
                DescriptionType description = new DescriptionType();
                description.Value = det.Descripcion;
                ItemIdentificationType codigoProd = new ItemIdentificationType();
                IDType id = new IDType();
                id.Value = det.Codigo;
                codigoProd.ID = id;

                PriceType PrecioProducto = new PriceType();
                PriceAmountType PrecioMontoTipo = new PriceAmountType();
                decimal porcentajeIgv = Convert.ToDecimal(parametros.Porcentaje_IGV / 100);
                PrecioMontoTipo.Value = Convert.ToDecimal(string.Format("{0:0.00}", det.preciounitario / (1 + porcentajeIgv)));

                PrecioMontoTipo.currencyID = "PEN";
                PrecioProducto.PriceAmount = PrecioMontoTipo;

                ItemType itemTipo = new ItemType();
                descriptions[0] = description;
                itemTipo.Description = descriptions;
                itemTipo.SellersItemIdentification = codigoProd;

                List<CommodityClassificationType> codSunats = new List<CommodityClassificationType>();
                CommodityClassificationType codSunat = new CommodityClassificationType();
                ItemClassificationCodeType codClas = new ItemClassificationCodeType();
                codClas.listName = "Item Classification";
                codClas.listAgencyName = "GS1 US";
                codClas.listID = "UNSPSC";
                codClas.Value = det.CodigoProdSunat;
                codSunat.ItemClassificationCode = codClas;
                codSunats.Add(codSunat);
                itemTipo.CommodityClassification = codSunats.ToArray();

                item.Item = itemTipo;
                item.Price = PrecioProducto;

                items[iditem] = item;
                iditem += 1;
            }
            Factura.CreditNoteLine = items;

            string archXML = CrearArchivoxmlNotaCredito(Factura, parametros.EmpresaRUCemisor, parametros.Serie, parametros.Correlativo);
            FirmarXML(archXML, Ruta_Certificado, Password_Certificado);
            string strEnvio = RutaEnvios + Path.GetFileName(archXML).Replace(".xml", ".zip");
            ComprimirZip(archXML, strEnvio);
            Enviardocumento(strEnvio);
            #endregion
        }
        private string CrearArchivoxmlNotaCredito(CreditNoteType Nota, string RUCEmisor, string serie, string correlativo)
        {
            //Generar el archivo xml
            XmlWriterSettings propiedades = new XmlWriterSettings();
            propiedades.Indent = true;
            propiedades.IndentChars = "\t";
            string Nombrearchivoxml = RUCEmisor + "-07-" + serie + "-" + correlativo;
            string rutaxml = string.Format(@"{0}{1}.xml", Rutaxml, Nombrearchivoxml);
            using (XmlWriter escribir = XmlWriter.Create(rutaxml, propiedades))
            {
                Type serializacion = typeof(CreditNoteType);
                XmlSerializer crearxml = new XmlSerializer(serializacion);
                crearxml.Serialize(escribir, Nota);
                return rutaxml;
            }
        }
        #endregion
        #region Nota de debito
        public void GenerarNotaDebito(Lventas parametros)
        {

            //Cabecera del xml
            var Factura = new DebitNoteType();
            Factura.Cac = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2";
            Factura.Cbc = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2";
            Factura.Ccts = "urn:un:unece:uncefact:documentation:2";
            Factura.Ds = "http://www.w3.org/2000/09/xmldsig#";
            Factura.Ext = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2";
            Factura.Qdt = "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2";
            Factura.Udt = "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2";
            UBLExtensionType[] ublextensiones = new UBLExtensionType[5];
            UBLExtensionType ublExtension = new UBLExtensionType();
            ublextensiones[0] = ublExtension;
            Factura.UBLExtensions = ublextensiones;

            //Otorgamos la version UBL y la version del esquema del documento
            ObtenerEmpresa();
            Factura.UBLVersionID = new UBLVersionIDType();
            Factura.UBLVersionID.Value = versionUbl;
            Factura.CustomizationID = new CustomizationIDType();
            Factura.CustomizationID.Value = versionEstruc;

            //Ingresar serie y numero de comprobante
            Factura.ID = new IDType();
            Factura.ID.Value = parametros.Serie + "-" + parametros.Correlativo;
            //Fecha de emision
            Factura.IssueDate = new IssueDateType();
            string fechaemision = Convert.ToDateTime(parametros.fecha_venta).ToString("yyyy-MM-dd");
            Factura.IssueDate.Value = Convert.ToDateTime(fechaemision);
            Factura.IssueTime = new IssueTimeType();
            string hora = Convert.ToDateTime(parametros.fecha_venta).ToString("HH:mm:ss");
            Factura.IssueTime.Value = Convert.ToDateTime(hora);

            //Tipo de moneda
            DocumentCurrencyCodeType moneda = new DocumentCurrencyCodeType();
            moneda.listID = "ISO 4217 Alpha";
            moneda.listName = "Currency";
            moneda.listAgencyName = "United Nations Economic Commission for Europe";
            moneda.Value = "PEN";
            Factura.DocumentCurrencyCode = moneda;


            #region Datos Exclusivos para nota de credito
            ResponseType DocumentoRel = new ResponseType();
            ResponseType[] DocumentoRels = new ResponseType[2];
            ReferenceIDType NumeroDocRel = new ReferenceIDType();
            NumeroDocRel.Value = parametros.Cab_Ref_Serie + "-" + parametros.Cab_Ref_Numero;
            ResponseCodeType TipoDocRel = new ResponseCodeType();
            TipoDocRel.Value = parametros.CodigoTipoNotacredito;
            DescriptionType Motivo = new DescriptionType();
            DescriptionType[] Motivos = new DescriptionType[2];
            Motivos[0] = Motivo;
            Motivo.Value = parametros.Cab_Ref_Motivo;
            DocumentoRel.ReferenceID = NumeroDocRel;
            DocumentoRel.ResponseCode = TipoDocRel;
            DocumentoRel.Description = Motivos;
            DocumentoRels[0] = DocumentoRel;
            Factura.DiscrepancyResponse = DocumentoRels;

            BillingReferenceType[] referencias = new BillingReferenceType[2];
            BillingReferenceType referencia = new BillingReferenceType();

            DocumentReferenceType documento = new DocumentReferenceType();
            IDType docRela = new IDType();
            docRela.Value = parametros.Cab_Ref_Serie + "-" + parametros.Cab_Ref_Numero;
            DocumentTypeCodeType TipoDocumentoRel = new DocumentTypeCodeType();
            TipoDocumentoRel.Value = parametros.Cab_Ref_TipoComprobante;
            documento.DocumentTypeCode = TipoDocumentoRel;
            documento.ID = docRela;
            referencia.InvoiceDocumentReference = documento;
            referencias[0] = referencia;
            Factura.BillingReference = referencias;

            #endregion

            #region nuevo

            SignatureType Firma = new SignatureType();
            SignatureType[] Firmas = new SignatureType[2];

            PartyType partySign = new PartyType();
            PartyIdentificationType partyIdentificacion = new PartyIdentificationType();
            PartyIdentificationType[] partyIdentificacions = new PartyIdentificationType[2];
            IDType idFirma = new IDType();
            idFirma.Value = parametros.EmpresaRUCemisor;
            Firma.ID = idFirma;

            partyIdentificacion.ID = idFirma;
            partyIdentificacions[0] = partyIdentificacion;
            partySign.PartyIdentification = partyIdentificacions;
            Firma.SignatoryParty = partySign;

            SupplierPartyType empresa = new SupplierPartyType();
            PartyType party = new PartyType();
            PartyIdentificationType partyidentificacion = new PartyIdentificationType();
            PartyIdentificationType[] partyidentificacions = new PartyIdentificationType[2];
            IDType idEmpresa = new IDType();

            idEmpresa.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06";
            idEmpresa.schemeName = "Documento de Identidad";
            idEmpresa.schemeID = "6";
            idEmpresa.schemeAgencyName = "PE:SUNAT";
            idEmpresa.Value = parametros.EmpresaRUCemisor;

            partyidentificacion.ID = idEmpresa;
            partyidentificacions[0] = partyidentificacion;
            party.PartyIdentification = partyidentificacions;

            PartyNameType partyname = new PartyNameType();
            List<PartyNameType> partynames = new List<PartyNameType>();
            NameType1 nameEmisor = new NameType1();
            nameEmisor.Value = parametros.EmpresaRazonsocialEmisora;
            partyname.Name = nameEmisor;
            partynames.Add(partyname);
            party.PartyName = partynames.ToArray();

            RegistrationNameType registerNameEmisor = new RegistrationNameType();
            registerNameEmisor.Value = parametros.EmpresaRazonsocialEmisora;

            //Direccion emisor                
            CompanyIDType compañia = new CompanyIDType();
            compañia.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06";
            compañia.schemeAgencyName = "PE:SUNAT";
            compañia.schemeName = "SUNAT:Identificador de Documento de Identidad";
            compañia.schemeID = "6";
            compañia.Value = parametros.EmpresaRUCemisor;

            AddressType direccion = new AddressType();
            AddressTypeCodeType addrestypecode = new AddressTypeCodeType();
            addrestypecode.listName = "Establecimientos anexos";
            addrestypecode.listAgencyName = "PE:SUNAT";
            addrestypecode.Value = "0000";

            TaxSchemeType taxSchema = new TaxSchemeType();
            IDType idsupplier = new IDType();
            idsupplier.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06";
            idsupplier.schemeAgencyName = "PE:SUNAT";
            idsupplier.schemeName = "SUNAT:Identificador de Documento de Identidad";
            idsupplier.schemeID = "6";
            idsupplier.Value = parametros.EmpresaRUCemisor;
            taxSchema.ID = idsupplier;


            List<PartyLegalEntityType> partelegals = new List<PartyLegalEntityType>();
            PartyLegalEntityType partelegal = new PartyLegalEntityType();
            RegistrationNameType registerNamePL = new RegistrationNameType();
            registerNamePL.Value = parametros.EmpresaRazonsocialEmisora;
            partelegal.RegistrationName = registerNamePL;

            AddressType direccionPL = new AddressType();
            IDType iddireccionPL = new IDType();
            iddireccionPL.schemeAgencyName = "PE:INEI";
            iddireccionPL.schemeName = "Ubigeos";
            iddireccionPL.Value = parametros.Ubigeo;
            direccionPL.ID = iddireccionPL;

            AddressTypeCodeType address_TypeCodeType = new AddressTypeCodeType();
            address_TypeCodeType.listName = "Establecimientos anexos";
            address_TypeCodeType.listAgencyName = "PE:SUNAT";
            address_TypeCodeType.Value = "0000";
            direccionPL.AddressTypeCode = address_TypeCodeType;

            CityNameType Departamento = new CityNameType();
            Departamento.Value = parametros.DptoempresaEmisora;
            direccionPL.CityName = Departamento;

            CountrySubentityType Provincia = new CountrySubentityType();
            Provincia.Value = parametros.ProvempresaEmisora;
            direccionPL.CountrySubentity = Provincia;

            DistrictType distrito = new DistrictType();
            distrito.Value = parametros.DistmpresaEmisora;
            direccionPL.District = distrito;
            List<AddressLineType> direcciones = new List<AddressLineType>();
            AddressLineType direccionEmisor = new AddressLineType();
            LineType local1 = new LineType();
            local1.Value = parametros.DireccionEmpresaEmisora;
            direccionPL.AddressLine = direcciones.ToArray();
            direccionEmisor.Line = local1;
            direcciones.Add(direccionEmisor);
            direccionPL.AddressLine = direcciones.ToArray();

            CountryType pais = new CountryType();
            IdentificationCodeType codigoPais = new IdentificationCodeType();

            codigoPais.listName = "Country";
            codigoPais.listAgencyName = "United Nations Economic Commission for Europe";
            codigoPais.listID = "ISO 3166-1";
            codigoPais.Value = "PE";
            pais.IdentificationCode = codigoPais;

            direccionPL.Country = pais;
            partelegal.RegistrationAddress = direccionPL;

            partelegals.Add(partelegal);
            party.PartyLegalEntity = partelegals.ToArray();

            party.PartyName = partynames.ToArray();
            party.PartyIdentification = partyidentificacions;
            empresa.Party = party;
            Factura.AccountingSupplierParty = empresa;

            //EMPRESA CLIENTE

            TaxSchemeType taxschemeCliente = new TaxSchemeType();
            CustomerPartyType CustomerPartyCliente = new CustomerPartyType();
            PartyType partyCliente = new PartyType();
            PartyIdentificationType partyIdentificion = new PartyIdentificationType();
            List<PartyIdentificationType> partyIdentificions = new List<PartyIdentificationType>();
            IDType idtipo = new IDType();
            idtipo.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06";
            idtipo.schemeName = "Documento de Identidad";
            idtipo.schemeAgencyName = "PE:SUNAT";
            idtipo.schemeID = "6";
            idtipo.Value = parametros.EmpresaRUCcliente;
            partyIdentificion.ID = idtipo;

            partyIdentificions.Add(partyIdentificion);
            partyCliente.PartyIdentification = partyIdentificions.ToArray();

            List<PartyNameType> RazSocClientes = new List<PartyNameType>();
            PartyNameType RazSocCliente = new PartyNameType();
            NameType1 razSocial = new NameType1();
            razSocial.Value = parametros.EmpresaRazonsocialCliente;
            RazSocCliente.Name = razSocial;
            RazSocClientes.Add(RazSocCliente);

            List<PartyTaxSchemeType> partySchemas = new List<PartyTaxSchemeType>();
            PartyTaxSchemeType partySchema = new PartyTaxSchemeType();
            RegistrationNameType RegistroNombre = new RegistrationNameType();
            RegistroNombre.Value = parametros.EmpresaRazonsocialCliente;
            partySchema.RegistrationName = RegistroNombre;

            CompanyIDType idcompañia = new CompanyIDType();
            idcompañia.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06";
            idcompañia.schemeAgencyName = "PE:SUNAT";
            idcompañia.schemeName = "SUNAT:Identificador de Documento de Identidad";
            idcompañia.schemeID = "6";
            idcompañia.Value = parametros.EmpresaRUCcliente;

            TaxSchemeType schemeType = new TaxSchemeType();
            IDType idc = new IDType();
            idc.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06";
            idc.schemeAgencyName = "PE:SUNAT";
            idc.schemeName = "SUNAT:Identificador de Documento de Identidad";
            idc.schemeID = "6";
            idc.Value = parametros.EmpresaRUCcliente;
            schemeType.ID = idc;

            idcompañia.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06";
            idcompañia.schemeAgencyName = "PE:SUNAT";
            idcompañia.schemeName = "SUNAT:Identificador de Documento de Identidad";
            idcompañia.schemeID = "6";
            idcompañia.Value = parametros.EmpresaRUCcliente;

            List<PartyLegalEntityType> partyLegals = new List<PartyLegalEntityType>();
            PartyLegalEntityType partyLegal = new PartyLegalEntityType();
            RegistrationNameType Registro_Nombre = new RegistrationNameType();
            Registro_Nombre.Value = parametros.EmpresaRazonsocialCliente;
            partyLegal.RegistrationName = Registro_Nombre;

            AddressType direccionCliente = new AddressType();
            List<AddressLineType> dirs = new List<AddressLineType>();
            AddressLineType dir = new AddressLineType();
            List<LineType> lineas = new List<LineType>();

            LineType linea = new LineType();
            linea.Value = parametros.DireccionCliente;
            dir.Line = linea;
            dirs.Add(dir);
            direccionCliente.AddressLine = dirs.ToArray();


            CountryType paisC = new CountryType();
            IdentificationCodeType codigoPaisC = new IdentificationCodeType();

            codigoPaisC.Value = "PE";
            paisC.IdentificationCode = codigoPaisC;

            partyLegals.Add(partyLegal);

            partySchema.CompanyID = idcompañia;
            partySchema.TaxScheme = schemeType;

            partySchemas.Add(partySchema);

            partyCliente.PartyLegalEntity = partyLegals.ToArray();
            CustomerPartyCliente.Party = partyCliente;

            CustomerPartyType accoutingCustomerParty = new CustomerPartyType();
            accoutingCustomerParty.Party = partyCliente;

            Factura.AccountingCustomerParty = accoutingCustomerParty;

            //Monto total de impuestos

            TaxTotalType TotalImptos = new TaxTotalType();
            TaxAmountType taxAmountImpto = new TaxAmountType();
            taxAmountImpto.currencyID = "PEN";
            taxAmountImpto.Value = Convert.ToDecimal(parametros.TotalIgv);
            TotalImptos.TaxAmount = taxAmountImpto;

            TaxSubtotalType[] subtotales = new TaxSubtotalType[2];
            TaxSubtotalType subtotal = new TaxSubtotalType();

            TaxableAmountType taxsubtotal = new TaxableAmountType();
            taxsubtotal.currencyID = "PEN";
            taxsubtotal.Value = Convert.ToDecimal(string.Format("{0:0.00}", parametros.TotSubtotal));
            subtotal.TaxableAmount = taxsubtotal;

            TaxAmountType TotalTaxAmountTotal = new TaxAmountType();
            TotalTaxAmountTotal.currencyID = "PEN";
            TotalTaxAmountTotal.Value = Convert.ToDecimal(string.Format("{0:0.00}", parametros.TotalIgv));
            subtotal.TaxAmount = TotalTaxAmountTotal;

            TaxSubtotalType subTotalIGV = new TaxSubtotalType();
            subTotalIGV.TaxableAmount = taxsubtotal;

            subtotales[0] = subtotal;
            TotalImptos.TaxSubtotal = subtotales;


            //Pago de IGV
            TaxCategoryType taxcategoryTotal = new TaxCategoryType();
            TaxSchemeType taxScheme = new TaxSchemeType();
            IDType idTotal = new IDType();
            idTotal.schemeID = "UN/ECE 5305";
            idTotal.schemeName = "Tax Category Identifier";
            idTotal.schemeAgencyName = "United Nations Economic Commission for Europe";
            idTotal.Value = "S";

            NameType1 nametypeImpto = new NameType1();
            nametypeImpto.Value = "IGV";
            TaxTypeCodeType taxtypecodeImpto = new TaxTypeCodeType();
            taxtypecodeImpto.Value = "VAT";

            IDType idTot = new IDType();

            idTot.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo05";
            idTot.schemeAgencyName = "PE:SUNAT";
            idTot.schemeName = "Codigo de tributos";
            idTot.Value = "1000";
            taxScheme.ID = idTot;

            NameType1 nametypeImptoIGV = new NameType1();
            nametypeImptoIGV.Value = "IGV";
            TaxTypeCodeType taxtypecodeImpuesto = new TaxTypeCodeType();
            taxtypecodeImpuesto.Value = "VAT";

            taxScheme.Name = nametypeImpto;
            taxScheme.TaxTypeCode = taxtypecodeImpto;
            taxcategoryTotal.TaxScheme = taxScheme;
            subtotal.TaxCategory = taxcategoryTotal;

            List<TaxSubtotalType> TaxSubtotals = new List<TaxSubtotalType>();
            TaxSubtotals.Add(subtotal);
            TotalImptos.TaxSubtotal = TaxSubtotals.ToArray();

            List<TaxTotalType> taxTotals = new List<TaxTotalType>();

            taxTotals.Add(TotalImptos);
            Factura.TaxTotal = taxTotals.ToArray();




            ////Total valor de venta 
            MonetaryTotalType TotalValorVenta = new MonetaryTotalType();
            PayableAmountType ImporteTotalVenta = new PayableAmountType();
            ImporteTotalVenta.currencyID = "PEN";
            ImporteTotalVenta.Value = Convert.ToDecimal(string.Format("{0:0.00}", parametros.Monto_total));
            TotalValorVenta.PayableAmount = ImporteTotalVenta;
            Factura.RequestedMonetaryTotal = TotalValorVenta;

            //Items de Nota debito
            DebitNoteLineType[] items = new DebitNoteLineType[10];
            int iditem = 1;

            foreach (Ldetalleventas det in parametros.Detalles)
            {
                DebitNoteLineType item = new DebitNoteLineType();
                IDType numeroItem = new IDType();
                numeroItem.Value = iditem.ToString();
                item.ID = numeroItem;

                DebitedQuantityType cantidad = new DebitedQuantityType();
                cantidad.unitCodeListAgencyName = "United Nations Economic Commission for Europe";
                cantidad.unitCodeListID = "UN/ECE rec 20";
                cantidad.unitCode = det.Unidad_de_medida;

                cantidad.Value = Convert.ToInt32(det.cantidad);
                item.DebitedQuantity = cantidad;

                LineExtensionAmountType ValorVenta = new LineExtensionAmountType();
                ValorVenta.currencyID = "PEN";
                ValorVenta.Value = Convert.ToDecimal(string.Format("{0:0.00}", det.Total_a_pagar / 1.18m));
                item.LineExtensionAmount = ValorVenta;

                //Precio de venta unitario por item y código 
                PricingReferenceType ValorReferenUnitario = new PricingReferenceType();

                PriceType[] TipoPrecios = new PriceType[2];
                PriceType TipoPrecio = new PriceType();

                PriceAmountType PrecioMonto = new PriceAmountType();


                PrecioMonto.currencyID = "PEN";
                PrecioMonto.Value = Convert.ToDecimal(string.Format("{0:0.00}", det.preciounitario));
                TipoPrecio.PriceAmount = PrecioMonto;

                PriceTypeCodeType TipoPrecioCode = new PriceTypeCodeType();
                TipoPrecioCode.listName = "Tipo de Precio";
                TipoPrecioCode.listAgencyName = "PE:SUNAT";
                TipoPrecioCode.listURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo16";
                TipoPrecioCode.Value = "01";


                TipoPrecio.PriceTypeCode = TipoPrecioCode;
                TipoPrecios[0] = TipoPrecio;
                ValorReferenUnitario.AlternativeConditionPrice = TipoPrecios;
                item.PricingReference = ValorReferenUnitario;

                TaxTotalType[] Totales_Items = new TaxTotalType[2];
                TaxTotalType Totales_Item = new TaxTotalType();

                TaxAmountType Total_Item = new TaxAmountType();
                Total_Item.currencyID = "PEN";
                Total_Item.Value = Convert.ToDecimal(string.Format("{0:0.00}", det.mtoValorVentaItem - (det.mtoValorVentaItem / 1.18m)));
                Totales_Item.TaxAmount = Total_Item;

                TaxSubtotalType[] subtotal_Items = new TaxSubtotalType[2];
                TaxSubtotalType subtotal_Item = new TaxSubtotalType();

                TaxableAmountType taxsubtotal_IGVItem = new TaxableAmountType();
                taxsubtotal_IGVItem.currencyID = "PEN";
                taxsubtotal_IGVItem.Value = Convert.ToDecimal(string.Format("{0:0.00}", det.mtoValorVentaItem / 1.18m));
                subtotal_Item.TaxableAmount = taxsubtotal_IGVItem;

                TaxAmountType TotalTaxAmount_IGVItem = new TaxAmountType();
                TotalTaxAmount_IGVItem.currencyID = "PEN";
                TotalTaxAmount_IGVItem.Value = Convert.ToDecimal(string.Format("{0:0.00}", det.mtoValorVentaItem - (det.mtoValorVentaItem / 1.18m)));
                subtotal_Item.TaxAmount = TotalTaxAmount_IGVItem;

                subtotal_Items[0] = subtotal_Item;
                Totales_Item.TaxSubtotal = subtotal_Items;

                TaxCategoryType taxcategory_IGVItem = new TaxCategoryType();

                IDType idTaxCategoria = new IDType();
                idTaxCategoria.schemeAgencyName = "United Nations Economic Commission for Europe";
                idTaxCategoria.schemeName = "Tax Category Identifier";
                idTaxCategoria.schemeID = "UN/ECE 5305";
                idTaxCategoria.Value = "S";


                PercentType1 porcentaje = new PercentType1();
                porcentaje.Value = Convert.ToDecimal(string.Format("{0:0.00}", 18));
                taxcategory_IGVItem.Percent = porcentaje;
                subtotal_Item.TaxCategory = taxcategory_IGVItem;

                TaxExemptionReasonCodeType ReasonCode = new TaxExemptionReasonCodeType();
                ReasonCode.listURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo07";
                ReasonCode.listName = "Afectacion del IGV";
                ReasonCode.listAgencyName = "PE:SUNAT";
                ReasonCode.Value = "10";

                taxcategory_IGVItem.TaxExemptionReasonCode = ReasonCode;

                TaxSchemeType taxscheme_IGVItem = new TaxSchemeType();
                IDType id2_IGVItem = new IDType();

                id2_IGVItem.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo05";
                id2_IGVItem.schemeAgencyName = "PE:SUNAT";
                id2_IGVItem.schemeName = "Codigo de tributos";


                id2_IGVItem.Value = "1000";
                taxscheme_IGVItem.ID = id2_IGVItem;

                NameType1 nombreImpto_IGVItem = new NameType1();
                nombreImpto_IGVItem.Value = "IGV";
                taxscheme_IGVItem.Name = nombreImpto_IGVItem;

                TaxTypeCodeType nombreImpto_IGVItemInter = new TaxTypeCodeType();
                nombreImpto_IGVItemInter.Value = "VAT";
                taxscheme_IGVItem.TaxTypeCode = nombreImpto_IGVItemInter;
                taxscheme_IGVItem.Name = nombreImpto_IGVItem;


                taxcategory_IGVItem.TaxScheme = taxscheme_IGVItem;

                subtotal_Items[0] = subtotal_Item;
                Totales_Item.TaxSubtotal = subtotal_Items;
                Totales_Items[0] = Totales_Item;

                item.TaxTotal = Totales_Items;

                DescriptionType[] descriptions = new DescriptionType[2];
                DescriptionType description = new DescriptionType();
                description.Value = det.Descripcion;
                ItemIdentificationType codigoProd = new ItemIdentificationType();
                IDType id = new IDType();
                id.Value = det.Codigo;
                codigoProd.ID = id;

                PriceType PrecioProducto = new PriceType();
                PriceAmountType PrecioMontoTipo = new PriceAmountType();
                decimal porcentajeIgv = Convert.ToDecimal(parametros.Porcentaje_IGV / 100);
                PrecioMontoTipo.Value = Convert.ToDecimal(string.Format("{0:0.00}", det.preciounitario / (1 + porcentajeIgv)));

                PrecioMontoTipo.currencyID = "PEN";
                PrecioProducto.PriceAmount = PrecioMontoTipo;

                ItemType itemTipo = new ItemType();
                descriptions[0] = description;
                itemTipo.Description = descriptions;
                itemTipo.SellersItemIdentification = codigoProd;

                List<CommodityClassificationType> codSunats = new List<CommodityClassificationType>();
                CommodityClassificationType codSunat = new CommodityClassificationType();
                ItemClassificationCodeType codClas = new ItemClassificationCodeType();
                codClas.listName = "Item Classification";
                codClas.listAgencyName = "GS1 US";
                codClas.listID = "UNSPSC";
                codClas.Value = det.CodigoProdSunat;
                codSunat.ItemClassificationCode = codClas;
                codSunats.Add(codSunat);
                itemTipo.CommodityClassification = codSunats.ToArray();

                item.Item = itemTipo;
                item.Price = PrecioProducto;

                items[iditem] = item;
                iditem += 1;
            }
            Factura.DebitNoteLine = items;
            string archXML = CrearArchivoxmlNotaDebito(Factura, parametros.EmpresaRUCemisor, parametros.Serie, parametros.Correlativo);
            FirmarXML(archXML, Ruta_Certificado, Password_Certificado);
            string strEnvio = RutaEnvios + Path.GetFileName(archXML).Replace(".xml", ".zip");
            ComprimirZip(archXML, strEnvio);
            Enviardocumento(strEnvio);
            #endregion
        }
        private string CrearArchivoxmlNotaDebito(DebitNoteType Nota, string RUCEmisor, string serie, string correlativo)
        {
            //Generar el archivo xml
            XmlWriterSettings propiedades = new XmlWriterSettings();
            propiedades.Indent = true;
            propiedades.IndentChars = "\t";
            string Nombrearchivoxml = RUCEmisor + "-08-" + serie + "-" + correlativo;
            string rutaxml = string.Format(@"{0}{1}.xml", Rutaxml, Nombrearchivoxml);
            using (XmlWriter escribir = XmlWriter.Create(rutaxml, propiedades))
            {
                Type serializacion = typeof(DebitNoteType);
                XmlSerializer crearxml = new XmlSerializer(serializacion);
                crearxml.Serialize(escribir, Nota);
                return rutaxml;
            }
        }
        #endregion
        public string GenerarResumenDiario_XML(string Fecha, string EmpresaRUC, string EmpresaRazonSocial,
                                                                              List<Lresumendiario> dsResumen

                                                                           )
        {
            SummaryDocumentsType Resumen = new SummaryDocumentsType();
            string numTicket = "";
            try
            {
                //------Namespace necesarios para el UBL
                Resumen.Sac = "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1";
                Resumen.Ext = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2";
                Resumen.Ds = "http://www.w3.org/2000/09/xmldsig#";
                Resumen.Cbc = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2";
                Resumen.Cac = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2";

                //Resumen.Ccts = "urn:un:unece:uncefact:documentation:2";                           
                //Resumen.Qdt = "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2";
                //Resumen.Udt = "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2";
                //Resumen.Xsi = "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2";

                //------
                //-----Datos
                // CabeceraDAO db = new CabeceraDAO();

                //System.Data.DataSet dsResumen = new System.Data.DataSet();
                ////ARREGLAR LA CONSULTA DE RESUMEN DIARIO
                // dsResumen = db.ListarResumenDiario(Fecha);

                if (dsResumen.Count > 0)
                {
                    Resumenes.UBLExtensionType[] ublExtensiones = new Resumenes.UBLExtensionType[5];
                    Resumenes.UBLExtensionType ublExtension = new Resumenes.UBLExtensionType();

                    ublExtensiones[0] = ublExtension;
                    Resumen.UBLExtensions = ublExtensiones;

                    Resumen.UBLVersionID = new Resumenes.UBLVersionIDType();
                    Resumen.UBLVersionID.Value = "2.0";

                    Resumen.CustomizationID = new Resumenes.CustomizationIDType();
                    //Resumen.CustomizationID.schemeAgencyName = "PE:SUNAT";
                    Resumen.CustomizationID.Value = "1.1";
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                        
                    //Numeración, conformada por serie y número correlativo
                    Resumen.ID = new Resumenes.IDType();
                    Resumen.ID.Value = "RC-" + DateTime.Now.ToString("yyyyMMdd") + "-001";
                    //Fecha de emisión y hora de emision
                    Resumenes.ReferenceDateType FechaEmision = new Resumenes.ReferenceDateType();
                    FechaEmision.Value = Convert.ToDateTime(dsResumen[0].FechaEmision);
                    Resumen.ReferenceDate = FechaEmision;

                    Resumen.IssueDate = new Resumenes.IssueDateType();
                    DateTime fechaGeneracion = DateTime.Now.Date;
                    Resumen.IssueDate.Value = Convert.ToDateTime(fechaGeneracion);

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////            
                    //Nombre Comercial del emisor Apellidos y nombres, denominación o razón social del emisor Tipo y Número de RUC del emisor Código del domicilio fiscal 
                    //o de local anexo del emisor 
                    Resumenes.SignatureType Firma = new Resumenes.SignatureType();
                    Resumenes.SignatureType[] Firmas = new Resumenes.SignatureType[2];

                    Resumenes.PartyType partySign = new Resumenes.PartyType();
                    Resumenes.PartyIdentificationType partyIdentificacion = new Resumenes.PartyIdentificationType();
                    Resumenes.PartyIdentificationType[] partyIdentificacions = new Resumenes.PartyIdentificationType[2];
                    Resumenes.IDType idFirma = new Resumenes.IDType();
                    idFirma.Value = EmpresaRUC;
                    Firma.ID = idFirma;

                    partyIdentificacion.ID = idFirma;
                    partyIdentificacions[0] = partyIdentificacion;
                    partySign.PartyIdentification = partyIdentificacions;
                    Firma.SignatoryParty = partySign;

                    Resumenes.NoteType Nota = new Resumenes.NoteType();

                    Nota.Value = "Elaborado por codigo 369";
                    Firma.Note = Nota;

                    Resumenes.PartyNameType partyName = new Resumenes.PartyNameType();
                    Resumenes.PartyNameType[] partyNames = new Resumenes.PartyNameType[2];

                    Resumenes.NameType1 RazonSocialFirma = new Resumenes.NameType1();
                    RazonSocialFirma.Value = EmpresaRazonSocial;
                    partyName.Name = RazonSocialFirma;
                    partyNames[0] = partyName;
                    partySign.PartyName = partyNames;

                    Resumenes.AttachmentType attachType = new Resumenes.AttachmentType();
                    Resumenes.ExternalReferenceType externaReferencia = new Resumenes.ExternalReferenceType();
                    Resumenes.URIType uri = new Resumenes.URIType();
                    uri.Value = "SIGN";
                    externaReferencia.URI = uri;
                    Firma.DigitalSignatureAttachment = attachType;

                    attachType.ExternalReference = externaReferencia;
                    Firma.DigitalSignatureAttachment = attachType;
                    Firmas[0] = Firma;
                    Resumen.Signature = Firmas;

                    Resumenes.SupplierPartyType empresa = new Resumenes.SupplierPartyType();
                    Resumenes.PartyType party = new Resumenes.PartyType();

                    Resumenes.AdditionalAccountIDType TipoDocumentoEmisor = new Resumenes.AdditionalAccountIDType();
                    Resumenes.AdditionalAccountIDType[] TipoDocumentoEmisors = new Resumenes.AdditionalAccountIDType[2];
                    TipoDocumentoEmisors[0] = TipoDocumentoEmisor;
                    TipoDocumentoEmisor.Value = "6";
                    empresa.AdditionalAccountID = TipoDocumentoEmisors;

                    Resumenes.CustomerAssignedAccountIDType RUCEmisor = new Resumenes.CustomerAssignedAccountIDType();
                    RUCEmisor.Value = EmpresaRUC;
                    empresa.CustomerAssignedAccountID = RUCEmisor;

                    Resumenes.PartyLegalEntityType parteLegalEntity = new Resumenes.PartyLegalEntityType();
                    Resumenes.PartyLegalEntityType[] parteLegalEntitys = new Resumenes.PartyLegalEntityType[2];

                    Resumenes.RegistrationNameType registerNameEmisor = new Resumenes.RegistrationNameType();
                    registerNameEmisor.Value = EmpresaRazonSocial;
                    parteLegalEntity.RegistrationName = registerNameEmisor;

                    parteLegalEntitys[0] = parteLegalEntity;
                    party.PartyLegalEntity = parteLegalEntitys;
                    empresa.Party = party;

                    Resumen.AccountingSupplierParty = empresa;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////            
                    //Número de orden del Ítem 
                    //Cantidad y Unidad de medida por ítem 
                    //Valor de venta del ítem
                    //Items de Resumen

                    Resumenes.SummaryDocumentsLineType[] items = new Resumenes.SummaryDocumentsLineType[100];
                    int iditem = 1;
                    Resumenes.TaxTotalType[] TotalesTributos = new Resumenes.TaxTotalType[100];

                    foreach (var reg in dsResumen)
                    {
                        Resumenes.SummaryDocumentsLineType item = new Resumenes.SummaryDocumentsLineType();

                        Resumenes.LineIDType numeroItem = new Resumenes.LineIDType();
                        numeroItem.Value = iditem.ToString();
                        item.LineID = numeroItem;

                        Resumenes.DocumentTypeCodeType TipoDocumento = new Resumenes.DocumentTypeCodeType();
                        TipoDocumento.Value = reg.IdTipoComp;
                        item.DocumentTypeCode = TipoDocumento;

                        Resumenes.IDType NumDocumento = new Resumenes.IDType();
                        NumDocumento.Value = reg.NumeroComprobante;
                        item.ID = NumDocumento;

                        Resumenes.CustomerPartyType Cliente = new Resumenes.CustomerPartyType();
                        Resumenes.CustomerAssignedAccountIDType NumeroDocumento = new Resumenes.CustomerAssignedAccountIDType();
                        NumeroDocumento.Value = reg.NumDoc;
                        Cliente.CustomerAssignedAccountID = NumeroDocumento;

                        Resumenes.AdditionalAccountIDType TipoDocumentoCliente = new Resumenes.AdditionalAccountIDType();
                        Resumenes.AdditionalAccountIDType[] TipoDocumentoClientes = new Resumenes.AdditionalAccountIDType[2];
                        TipoDocumentoClientes[0] = TipoDocumentoCliente;
                        TipoDocumentoCliente.Value = "1";
                        Cliente.AdditionalAccountID = TipoDocumentoClientes;
                        item.AccountingCustomerParty = Cliente;

                        //< !--Documento que modifica -->    
                        //< !--Datos de Percepcion -PER-- >
                        //< !--PER-- >
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ///
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        Resumenes.StatusType Estado = new Resumenes.StatusType();
                        Resumenes.ConditionCodeType condicion = new Resumenes.ConditionCodeType();
                        condicion.Value = reg.Adicionar;
                        Estado.ConditionCode = condicion;
                        item.Status = Estado;

                        Resumenes.AmountType1 Total = new Resumenes.AmountType1();
                        if (reg.IdMoneda == "PEN")
                        {
                            Total.currencyID = CurrencyCodeContentType.PEN;
                        }
                        else if (reg.IdMoneda == "USD")
                        {
                            Total.currencyID = CurrencyCodeContentType.USD;
                        }
                        //< !--Total Importe Total-->
                        Total.Value = Convert.ToDecimal(string.Format("{0:0.00}", reg.TOT_NETO));
                        item.TotalAmount = Total;
                        //< !--Total Venta Operaciones Gravadas - 01-- >
                        Resumenes.PaymentType PagoSubtotal = new Resumenes.PaymentType();
                        Resumenes.PaymentType[] PagoSubtotals = new Resumenes.PaymentType[2];
                        Resumenes.PaidAmountType SubTotal = new Resumenes.PaidAmountType();
                        //Identificación del tipo de importe total
                        //01: Valor de venta de las operaciones gravadas con el IGV
                        //02: Valores de venta de las operaciones exoneradas del IGV
                        //03: Valores de venta de las operaciones inafectas del IGV
                        //04: Valor de venta de las exportaciones del item
                        //05: Valor de venta de las operaciones gratuitas
                        Resumenes.InstructionIDType TipoImporteTotal = new Resumenes.InstructionIDType();
                        TipoImporteTotal.Value = "01";

                        if (reg.IdMoneda == "PEN")
                        {
                            SubTotal.currencyID = CurrencyCodeContentType.PEN;
                        }
                        else if (reg.IdMoneda == "USD")
                        {
                            SubTotal.currencyID = CurrencyCodeContentType.USD;
                        }
                        SubTotal.Value = Convert.ToDecimal(string.Format("{0:0.00}", reg.TOT_VALOR_VENTA));
                        Resumenes.InstructionIDType Tipo = new Resumenes.InstructionIDType();
                        Tipo.Value = "01";
                        ////< !--fin 01-- >
                        ////< !--Total Venta Operaciones Exoneradas - 02-- > 
                        ////< !--fin 02-- >
                        ////< !--Total Venta Operaciones Inafectas - 03-- >
                        ////< !--fin 03-- >
                        ////< !--Total Venta Operaciones Gratuitas - 05-- >  
                        ////< !--fin 05-- >   
                        ////< !--Total SUMATORIO OTROS CARGOS - Cargos-- >
                        ////< !--fin Cargos-- >
                        ////< !--TOTAL ISC-- >
                        ///
                        PagoSubtotal.PaidAmount = SubTotal;
                        PagoSubtotals[0] = PagoSubtotal;
                        PagoSubtotal.InstructionID = Tipo;

                        // Total ISC                                
                        Resumenes.TaxTotalType[] Totals_ISCItems = new Resumenes.TaxTotalType[2];
                        Resumenes.TaxTotalType Total_ISCItem = new Resumenes.TaxTotalType();

                        Resumenes.TaxAmountType Total_ItemISC = new Resumenes.TaxAmountType();
                        Total_ItemISC.Value = Convert.ToDecimal(string.Format("{0:0.00}", reg.TOT_ISC));
                        if (reg.IdMoneda == "PEN")
                        {
                            Total_ItemISC.currencyID = CurrencyCodeContentType.PEN;
                        }
                        else if (reg.IdMoneda == "USD")
                        {
                            Total_ItemISC.currencyID = CurrencyCodeContentType.USD;
                        }
                        Total_ISCItem.TaxAmount = Total_ItemISC;
                        Totals_ISCItems[0] = Total_ISCItem;

                        Resumenes.TaxCategoryType Category_ISCItem = new Resumenes.TaxCategoryType();

                        Resumenes.TaxSchemeType TaxScheme_ISCItem = new Resumenes.TaxSchemeType();
                        Resumenes.IDType id_ISCitem = new Resumenes.IDType();
                        id_ISCitem.Value = "2000";
                        TaxScheme_ISCItem.ID = id_ISCitem;

                        Resumenes.NameType1 nombreImpto_ISCItem = new Resumenes.NameType1();
                        nombreImpto_ISCItem.Value = "ISC";
                        TaxScheme_ISCItem.Name = nombreImpto_ISCItem;

                        Resumenes.TaxTypeCodeType nombreImpto_ISCItemInter = new Resumenes.TaxTypeCodeType();
                        nombreImpto_ISCItemInter.Value = "EXC";
                        TaxScheme_ISCItem.TaxTypeCode = nombreImpto_ISCItemInter;

                        Category_ISCItem.TaxScheme = TaxScheme_ISCItem;
                        Resumenes.TaxSubtotalType[] subtotal_ISCs = new Resumenes.TaxSubtotalType[2];
                        Resumenes.TaxSubtotalType subtotal_ISC = new Resumenes.TaxSubtotalType();

                        subtotal_ISC.TaxCategory = Category_ISCItem;
                        subtotal_ISC.TaxAmount = Total_ItemISC;
                        subtotal_ISCs[0] = subtotal_ISC;

                        Total_ISCItem.TaxSubtotal = subtotal_ISCs;
                        TotalesTributos[0] = Total_ISCItem;

                        //< !--TOTAL IGV-- >

                        Resumenes.TaxTotalType[] Totals_IGVItems = new Resumenes.TaxTotalType[2];
                        Resumenes.TaxTotalType Total_IGVItem = new Resumenes.TaxTotalType();

                        Resumenes.TaxAmountType Total_ItemIGV = new Resumenes.TaxAmountType();
                        Total_ItemIGV.Value = Convert.ToDecimal(string.Format("{0:0.00}", reg.TOT_IGV));
                        if (reg.IdMoneda == "PEN")
                        {
                            Total_ItemIGV.currencyID = Resumenes.CurrencyCodeContentType.PEN;
                        }
                        else if (reg.IdMoneda == "USD")
                        {
                            Total_ItemIGV.currencyID = Resumenes.CurrencyCodeContentType.USD;
                        }
                        Total_IGVItem.TaxAmount = Total_ItemIGV;
                        Totals_IGVItems[0] = Total_IGVItem;

                        Resumenes.TaxCategoryType Category_IGVItem = new Resumenes.TaxCategoryType();
                        Resumenes.TaxSchemeType TaxScheme_IGVItem = new Resumenes.TaxSchemeType();
                        Resumenes.IDType id_IGVitem = new Resumenes.IDType();
                        id_IGVitem.Value = "1000";
                        TaxScheme_IGVItem.ID = id_IGVitem;

                        Resumenes.NameType1 nombreImpto_IGVItem = new Resumenes.NameType1();
                        nombreImpto_IGVItem.Value = "IGV";
                        TaxScheme_IGVItem.Name = nombreImpto_IGVItem;

                        Resumenes.TaxTypeCodeType nombreImpto_IGVItemInter = new Resumenes.TaxTypeCodeType();
                        nombreImpto_IGVItemInter.Value = "VAT";
                        TaxScheme_IGVItem.TaxTypeCode = nombreImpto_IGVItemInter;

                        Category_IGVItem.TaxScheme = TaxScheme_IGVItem;

                        Resumenes.TaxSubtotalType[] subtotal_IGVs = new Resumenes.TaxSubtotalType[2];
                        Resumenes.TaxSubtotalType subtotal_IGV = new Resumenes.TaxSubtotalType();

                        subtotal_IGV.TaxCategory = Category_IGVItem;
                        subtotal_IGV.TaxAmount = Total_ItemIGV;
                        subtotal_IGVs[0] = subtotal_IGV;

                        Total_IGVItem.TaxSubtotal = subtotal_IGVs;
                        TotalesTributos[1] = Total_IGVItem;

                        //< !--TOTAL OTROS-- >

                        Resumenes.TaxTotalType[] Totals_OtrosItems = new Resumenes.TaxTotalType[2];
                        Resumenes.TaxTotalType Total_OtrosItem = new Resumenes.TaxTotalType();

                        Resumenes.TaxAmountType Total_ItemOtros = new Resumenes.TaxAmountType();
                        Total_ItemOtros.Value = Convert.ToDecimal(string.Format("{0:0.00}", reg.TOT_OPOT));

                        if (reg.IdMoneda == "PEN")
                        {
                            Total_ItemOtros.currencyID = Resumenes.CurrencyCodeContentType.PEN;
                        }
                        else if (reg.IdMoneda == "USD")
                        {
                            Total_ItemOtros.currencyID = Resumenes.CurrencyCodeContentType.USD;
                        }
                        Total_OtrosItem.TaxAmount = Total_ItemOtros;
                        Totals_OtrosItems[0] = Total_OtrosItem;

                        Resumenes.TaxSubtotalType[] subtotal_Otross = new Resumenes.TaxSubtotalType[2];
                        Resumenes.TaxSubtotalType subtotal_Otros = new Resumenes.TaxSubtotalType();

                        Resumenes.TaxAmountType Total_ItemOtrosSub = new Resumenes.TaxAmountType();
                        if (reg.IdMoneda == "PEN")
                        {
                            Total_ItemOtrosSub.currencyID = Resumenes.CurrencyCodeContentType.PEN;
                        }
                        else if (reg.IdMoneda == "USD")
                        {
                            Total_ItemOtrosSub.currencyID = Resumenes.CurrencyCodeContentType.USD;
                        }
                        subtotal_Otros.TaxAmount = Total_ItemOtrosSub;

                        Resumenes.TaxCategoryType Category_OtrosItem = new Resumenes.TaxCategoryType();
                        Resumenes.TaxSchemeType TaxScheme_OtrosItem = new Resumenes.TaxSchemeType();
                        Resumenes.IDType id_Otrositem = new Resumenes.IDType();
                        id_Otrositem.Value = "9999";
                        TaxScheme_OtrosItem.ID = id_Otrositem;

                        Resumenes.NameType1 nombreImpto_OtrosItem = new Resumenes.NameType1();
                        nombreImpto_OtrosItem.Value = "OTROS";
                        TaxScheme_OtrosItem.Name = nombreImpto_OtrosItem;

                        Resumenes.TaxTypeCodeType nombreImpto_OtrosItemInter = new Resumenes.TaxTypeCodeType();
                        nombreImpto_OtrosItemInter.Value = "OTH";
                        TaxScheme_OtrosItem.TaxTypeCode = nombreImpto_OtrosItemInter;
                        Category_OtrosItem.TaxScheme = TaxScheme_OtrosItem;

                        subtotal_Otros.TaxCategory = Category_OtrosItem;
                        subtotal_Otros.TaxAmount = Total_ItemOtrosSub;
                        subtotal_Otross[0] = subtotal_Otros;
                        Total_OtrosItem.TaxSubtotal = subtotal_Otross;
                        TotalesTributos[2] = Total_OtrosItem;
                        item.TaxTotal = TotalesTributos;

                        items[iditem] = item;
                        iditem += 1;

                    }

                    Resumen.SummaryDocumentsLine = items;
                    string archXML = GenerarResumenDiario(Resumen, Fecha, "RC", 1, EmpresaRUC, Rutaxml);

                    FirmarXML(archXML, Ruta_Certificado, Password_Certificado);
                    string strEnvio = RutaEnvios + Path.GetFileName(archXML).Replace(".xml", ".zip");
                    ComprimirZip(archXML, strEnvio);
                    numTicket = EnviarResumenDiario(strEnvio);

                }
                MessageBox.Show(numTicket);
                return numTicket;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private string GenerarResumenDiario(SummaryDocumentsType ResumenDiario,
                                                                      string Fecha, string TipoDocumento, int correlativo,
                                                                      string EmpresaRUC, string rutaxml)
        {
            //-----Generando el archivo XML
            XmlWriterSettings setting = new XmlWriterSettings();
            setting.Indent = true;
            setting.IndentChars = "\t";

            string ArchivoXML = EmpresaRUC + "-" + TipoDocumento + "-" + DateTime.Now.ToString("yyyyMMdd") + "-" +
                                             correlativo.ToString().PadLeft(3, '0');

            string rutaXML = string.Format(@"{0}{1}.xml", rutaxml, ArchivoXML);

            using (XmlWriter writer = XmlWriter.Create(rutaXML, setting))
            {
                Type typeToSerialize = typeof(SummaryDocumentsType);
                XmlSerializer xs = new XmlSerializer(typeToSerialize);
                xs.Serialize(writer, ResumenDiario);
                return rutaXML;
            }
        }

        private String vl_Ticket = "";
        private String ARCHIVO_RES = "";
        private String strRetorno = "";


        #region comunicacion de baja

        public string GenerarComunicacionBaja_XML(DateTime Fecha, string EmpresaRUC,
                                                                        string EmpresaRazonSocial, 
                                                                        string TipoDocumento, 
                                                                        string SerieDocumento,
                                                                        string NumeroDocumento, 
                                                                        string MotivoBaja,
                                                                        string rutacert, 
                                                                        string passwordcert, 
                                                                        string rutaXML)
        {
            Bajas.VoidedDocumentsType Baja = new Bajas.VoidedDocumentsType();
            string numTicket = "";
            try
            {
                Baja.Cac = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2";
                Baja.Cbc = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2";
                Baja.Ds = "http://www.w3.org/2000/09/xmldsig#";
                Baja.Ext = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2";
                Baja.Sac = "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1";

                Bajas.UBLExtensionType[] ublExtensiones = new Bajas.UBLExtensionType[5];
                Bajas.UBLExtensionType ublExtension = new Bajas.UBLExtensionType();

                ublExtensiones[0] = ublExtension;
                Baja.UBLExtensions = ublExtensiones;

                Baja.UBLVersionID = new Bajas.UBLVersionIDType();
                Baja.UBLVersionID.Value = "2.0";

                Baja.CustomizationID = new Bajas.CustomizationIDType();
                Baja.CustomizationID.Value = "1.0";
                Baja.ID = new Bajas.IDType();
                Baja.ID.Value = "RA-" + DateTime.Now.ToString("yyyyMMdd") + "-001";
                Bajas.ReferenceDateType FechaEmision = new Bajas.ReferenceDateType();
                FechaEmision.Value = Fecha;
                Baja.ReferenceDate = FechaEmision;

                Baja.IssueDate = new Bajas.IssueDateType();
                DateTime fechaGeneracion = DateTime.Now.Date;
                Baja.IssueDate.Value = Convert.ToDateTime(fechaGeneracion);

                Bajas.SignatureType Firma = new Bajas.SignatureType();
                Bajas.SignatureType[] Firmas = new Bajas.SignatureType[2];

                Bajas.PartyType partySign = new Bajas.PartyType();
                Bajas.PartyIdentificationType partyIdentificacion = new Bajas.PartyIdentificationType();
                Bajas.PartyIdentificationType[] partyIdentificacions = new Bajas.PartyIdentificationType[2];
                Bajas.IDType idFirma = new Bajas.IDType();
                idFirma.Value = EmpresaRUC;
                Firma.ID = idFirma;

                partyIdentificacion.ID = idFirma;
                partyIdentificacions[0] = partyIdentificacion;
                partySign.PartyIdentification = partyIdentificacions;
                Firma.SignatoryParty = partySign;

                Bajas.NoteType Nota = new Bajas.NoteType();

                Nota.Value = "Elaborado por codigo 369";
                Firma.Note = Nota;

                Bajas.PartyNameType partyName = new Bajas.PartyNameType();
                Bajas.PartyNameType[] partyNames = new Bajas.PartyNameType[2];

                Bajas.NameType1 RazonSocialFirma = new Bajas.NameType1();
                RazonSocialFirma.Value = EmpresaRazonSocial;
                partyName.Name = RazonSocialFirma;
                partyNames[0] = partyName;
                partySign.PartyName = partyNames;

                Bajas.AttachmentType attachType = new Bajas.AttachmentType();
                Bajas.ExternalReferenceType externaReferencia = new Bajas.ExternalReferenceType();
                Bajas.URIType uri = new Bajas.URIType();
                uri.Value = "SIGN";
                externaReferencia.URI = uri;
                Firma.DigitalSignatureAttachment = attachType;

                attachType.ExternalReference = externaReferencia;
                Firma.DigitalSignatureAttachment = attachType;
                Firmas[0] = Firma;
                Baja.Signature = Firmas;

                Bajas.SupplierPartyType empresa = new Bajas.SupplierPartyType();
                Bajas.PartyType party = new Bajas.PartyType();

                Bajas.AdditionalAccountIDType TipoDocumentoEmisor = new Bajas.AdditionalAccountIDType();
                Bajas.AdditionalAccountIDType[] TipoDocumentoEmisors = new Bajas.AdditionalAccountIDType[2];
                TipoDocumentoEmisors[0] = TipoDocumentoEmisor;
                TipoDocumentoEmisor.Value = "6";
                empresa.AdditionalAccountID = TipoDocumentoEmisors;

                Bajas.CustomerAssignedAccountIDType RUCEmisor = new Bajas.CustomerAssignedAccountIDType();
                RUCEmisor.Value = EmpresaRUC;
                empresa.CustomerAssignedAccountID = RUCEmisor;

                Bajas.PartyLegalEntityType parteLegalEntity = new Bajas.PartyLegalEntityType();
                Bajas.PartyLegalEntityType[] parteLegalEntitys = new Bajas.PartyLegalEntityType[2];

                Bajas.RegistrationNameType registerNameEmisor = new Bajas.RegistrationNameType();
                registerNameEmisor.Value = EmpresaRazonSocial;
                parteLegalEntity.RegistrationName = registerNameEmisor;

                parteLegalEntitys[0] = parteLegalEntity;
                party.PartyLegalEntity = parteLegalEntitys;
                empresa.Party = party;

                Baja.AccountingSupplierParty = empresa;
                Bajas.VoidedDocumentsLineType ItemBaja = new Bajas.VoidedDocumentsLineType();
                Bajas.VoidedDocumentsLineType[] ItemsBajas = new Bajas.VoidedDocumentsLineType[2];

                Bajas.LineIDType numeroItem = new Bajas.LineIDType();
                numeroItem.Value = "1";
                ItemBaja.LineID = numeroItem;

                Bajas.DocumentTypeCodeType Tipo_Documento = new Bajas.DocumentTypeCodeType();
                Tipo_Documento.Value = TipoDocumento.PadLeft(2, '0');
                ItemBaja.DocumentTypeCode = Tipo_Documento;

                Bajas.IdentifierType Serie_Documento = new Bajas.SerialIDType();
                Serie_Documento.Value = SerieDocumento;
                ItemBaja.DocumentSerialID = Serie_Documento;

                Bajas.IdentifierType Numero_Documento = new Bajas.SerialIDType();
                Numero_Documento.Value = NumeroDocumento;
                ItemBaja.DocumentNumberID = Numero_Documento;

                Bajas.TextType Motivo_Baja = new Bajas.TextType();
                Motivo_Baja.Value = MotivoBaja;
                ItemBaja.VoidReasonDescription = Motivo_Baja;
                ItemsBajas[0] = ItemBaja;
                Baja.VoidedDocumentsLine = ItemsBajas;
                string archXML = GenerarComunicacionBaja(Baja, Fecha, "RA", 1, EmpresaRUC, rutaXML);
                FirmarXML(archXML, rutacert, passwordcert);
                numTicket = EnviarResumenDiario(archXML);
                return numTicket;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private string GenerarComunicacionBaja(Bajas.VoidedDocumentsType Baja,
                                                                      DateTime Fecha, string TipoDocumento,
                                                                      int correlativo, string RUCEmpresa, string pathXML)
        {
            XmlWriterSettings setting = new XmlWriterSettings();
            setting.Indent = true;
            setting.IndentChars = "\t";

            string ArchivoXML = RUCEmpresa + "-" + TipoDocumento + "-" + DateTime.Now.ToString("yyyyMMdd") + "-" +
                                             correlativo.ToString().PadLeft(3, '0');

            string rutaXML = string.Format(@"{0}{1}.xml", pathXML, ArchivoXML);

            using (XmlWriter writer = XmlWriter.Create(rutaXML, setting))
            {
                Type typeToSerialize = typeof(Bajas.VoidedDocumentsType);
                XmlSerializer xs = new XmlSerializer(typeToSerialize);
                xs.Serialize(writer, Baja);
                return rutaXML;
            }
        }
        public string EnviarResumenDiario(string pArchivo)
        {
            vl_Ticket = "";

            try
            {
                string sUsuarioSunat = Usuariosecundario;
                string sclaveSunat = Passsecundario;

                List<string> oLstRespuesta = new List<string>();

                string pPath = Path.GetDirectoryName(pArchivo) + Path.DirectorySeparatorChar;
                string pFileName = Path.GetFileName(pArchivo);

                string sNombreXml = pFileName;
                string sNombreZip = pFileName.Replace(".xml", ".zip");
                string sFileXMl = pPath + sNombreXml;
                string sFileZip = pPath + sNombreZip;


                if (File.Exists(sFileZip))
                    File.Delete(sFileZip);

                using (ZipArchive newFile = ZipFile.Open(sFileZip, ZipArchiveMode.Create))
                    newFile.CreateEntryFromFile(sFileXMl, sNombreXml, CompressionLevel.Fastest);

                byte[] byteArray = File.ReadAllBytes(sFileZip);

                System.Net.ServicePointManager.UseNagleAlgorithm = true;
                System.Net.ServicePointManager.Expect100Continue = false;
                System.Net.ServicePointManager.CheckCertificateRevocationList = true;

                #region Config

                BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.TransportWithMessageCredential);
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
                binding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;
                binding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
                binding.Security.Message.AlgorithmSuite = System.ServiceModel.Security.SecurityAlgorithmSuite.Default;
                EndpointAddress remoteAddress = new EndpointAddress(Servidor);
                #endregion
                billServiceClient ws = new billServiceClient(binding, remoteAddress);

                ws.ClientCredentials.UserName.UserName = sUsuarioSunat;
                ws.ClientCredentials.UserName.Password = sclaveSunat;

                var elements = ws.Endpoint.Binding.CreateBindingElements();
                elements.Find<SecurityBindingElement>().EnableUnsecuredResponse = true;
                ws.Endpoint.Binding = new CustomBinding(elements);

                ws.Open();
                String oRespuestaXML = ws.sendSummary(sNombreZip, byteArray, "1");
                strRetorno = oRespuestaXML;
                ws.Close();




            }

            catch (System.ServiceModel.FaultException ex)
            {
                strRetorno = "Error : " + ex.Message + " -  ";
            }

            return strRetorno;
        }

        public string ObtenerEstado(string ticket)
        {
            string strRetorno = "";
            try
            {

                BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.TransportWithMessageCredential);
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
                binding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;
                binding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
                binding.Security.Message.AlgorithmSuite = System.ServiceModel.Security.SecurityAlgorithmSuite.Default;

                EndpointAddress remoteAddress = new EndpointAddress(Servidor);
                billServiceClient servicio = new billServiceClient(binding, remoteAddress);
                servicio.ClientCredentials.UserName.UserName = Usuariosecundario;
                servicio.ClientCredentials.UserName.Password = Passsecundario;

                var elements = servicio.Endpoint.Binding.CreateBindingElements();
                elements.Find<SecurityBindingElement>().EnableUnsecuredResponse = true;
                servicio.Endpoint.Binding = new CustomBinding(elements);
                {
                    servicio.Open();
                    statusResponse returnstring = servicio.getStatus(ticket);
                    strRetorno = returnstring.statusCode;
                    servicio.Close();
                    return strRetorno;
                }
            }
            catch (FaultException ex)
            {
                throw new Exception(ex.Code.Name);
            }
        }
        #endregion

    }
}
