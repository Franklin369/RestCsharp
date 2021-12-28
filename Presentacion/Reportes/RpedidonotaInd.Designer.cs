
namespace RestCsharp.Presentacion.Reportes
{
    partial class RpedidonotaInd
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.TableGroup tableGroup1 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup2 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup3 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.TextBox8 = new Telerik.Reporting.TextBox();
            this.TextBox11 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.reportHeaderSection1 = new Telerik.Reporting.ReportHeaderSection();
            this.Tabladetalle = new Telerik.Reporting.Table();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // textBox4
            // 
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.736D), Telerik.Reporting.Drawing.Unit.Cm(0.569D));
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox4.Value = "Cant";
            // 
            // textBox6
            // 
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.811D), Telerik.Reporting.Drawing.Unit.Cm(0.569D));
            this.textBox6.Style.Font.Bold = true;
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.Value = "Producto";
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(2D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.TextBox8,
            this.TextBox11,
            this.textBox2,
            this.textBox3});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.6D), Telerik.Reporting.Drawing.Unit.Cm(0.7D));
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "Pedido";
            // 
            // TextBox8
            // 
            this.TextBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.7D));
            this.TextBox8.Name = "TextBox8";
            this.TextBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.4D), Telerik.Reporting.Drawing.Unit.Cm(0.7D));
            this.TextBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.TextBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.TextBox8.Value = "Mesa:";
            // 
            // TextBox11
            // 
            this.TextBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(1.4D));
            this.TextBox11.Name = "TextBox11";
            this.TextBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.4D), Telerik.Reporting.Drawing.Unit.Cm(0.6D));
            this.TextBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.TextBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.TextBox11.Value = "Mozo:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.4D), Telerik.Reporting.Drawing.Unit.Cm(0.7D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.2D), Telerik.Reporting.Drawing.Unit.Cm(0.7D));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.Value = "=IIf(Fields.Mesa<>\"!@PARA LLEVAR@!\",Fields.Mesa,\"PARA LLEVAR-\"+Fields.Nombrelleva" +
    "r)";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.4D), Telerik.Reporting.Drawing.Unit.Cm(1.4D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.2D), Telerik.Reporting.Drawing.Unit.Cm(0.6D));
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.Value = "=Fields.Mozo";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0D);
            this.detail.Name = "detail";
            // 
            // reportHeaderSection1
            // 
            this.reportHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(1.2D);
            this.reportHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.Tabladetalle});
            this.reportHeaderSection1.Name = "reportHeaderSection1";
            // 
            // Tabladetalle
            // 
            this.Tabladetalle.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(1.736D)));
            this.Tabladetalle.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(5.811D)));
            this.Tabladetalle.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Cm(0.622D)));
            this.Tabladetalle.Body.SetCellContent(0, 0, this.textBox5);
            this.Tabladetalle.Body.SetCellContent(0, 1, this.textBox7);
            tableGroup1.Name = "tableGroup";
            tableGroup1.ReportItem = this.textBox4;
            tableGroup2.Name = "tableGroup1";
            tableGroup2.ReportItem = this.textBox6;
            this.Tabladetalle.ColumnGroups.Add(tableGroup1);
            this.Tabladetalle.ColumnGroups.Add(tableGroup2);
            this.Tabladetalle.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox5,
            this.textBox7,
            this.textBox4,
            this.textBox6});
            this.Tabladetalle.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.053D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.Tabladetalle.Name = "Tabladetalle";
            tableGroup3.Groupings.Add(new Telerik.Reporting.Grouping(null));
            tableGroup3.Name = "detailTableGroup";
            this.Tabladetalle.RowGroups.Add(tableGroup3);
            this.Tabladetalle.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.547D), Telerik.Reporting.Drawing.Unit.Cm(1.191D));
            // 
            // textBox5
            // 
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.736D), Telerik.Reporting.Drawing.Unit.Cm(0.622D));
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox5.Value = "=Fields.Cant";
            // 
            // textBox7
            // 
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.811D), Telerik.Reporting.Drawing.Unit.Cm(0.622D));
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox7.Value = "= IIf(Fields.NotasIndividuales<>\"-\",Fields.Producto + \" Nota:\"+Fields.NotasIndivi" +
    "duales,Fields.Producto)\r\n\r\n";
            // 
            // RpedidonotaInd
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail,
            this.reportHeaderSection1});
            this.Name = "RpedidonotaInd";
            this.PageSettings.ContinuousPaper = true;
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(2D), Telerik.Reporting.Drawing.Unit.Mm(2D), Telerik.Reporting.Drawing.Unit.Mm(2D), Telerik.Reporting.Drawing.Unit.Mm(2D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.PageSettings.PaperSize = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8D), Telerik.Reporting.Drawing.Unit.Cm(4D));
            this.Style.Font.Name = "Consolas";
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1});
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(7.6D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox textBox1;
        internal Telerik.Reporting.TextBox TextBox8;
        internal Telerik.Reporting.TextBox TextBox11;
        internal Telerik.Reporting.TextBox textBox2;
        internal Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.ReportHeaderSection reportHeaderSection1;
        public Telerik.Reporting.Table Tabladetalle;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox6;
    }
}