
namespace RestCsharp.Presentacion.Reportes
{
    partial class Menureportes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menureportes));
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnVentas = new System.Windows.Forms.Button();
            this.btnProductos = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.PictureBox4 = new System.Windows.Forms.PictureBox();
            this.panelVentas = new System.Windows.Forms.Panel();
            this.reportViewer1 = new Telerik.ReportViewer.WinForms.ReportViewer();
            this.panelCondicionales = new System.Windows.Forms.Panel();
            this.PanelEmpleado = new System.Windows.Forms.Panel();
            this.txtEmpleado = new System.Windows.Forms.ComboBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.btnHoy = new System.Windows.Forms.Button();
            this.chekFiltros = new System.Windows.Forms.CheckBox();
            this.PanelFiltros = new System.Windows.Forms.Panel();
            this.TXTFF = new System.Windows.Forms.DateTimePicker();
            this.TXTFI = new System.Windows.Forms.DateTimePicker();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.MenuStrip6 = new System.Windows.Forms.MenuStrip();
            this.TFILTROS = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnResumenVentas = new System.Windows.Forms.Button();
            this.PResumenVentas = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnEmpleado = new System.Windows.Forms.Button();
            this.PVentasPorempleado = new System.Windows.Forms.Panel();
            this.panelProductos = new System.Windows.Forms.Panel();
            this.reportViewer2 = new Telerik.ReportViewer.WinForms.ReportViewer();
            this.btnsalir = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).BeginInit();
            this.panelVentas.SuspendLayout();
            this.panelCondicionales.SuspendLayout();
            this.PanelEmpleado.SuspendLayout();
            this.PanelFiltros.SuspendLayout();
            this.MenuStrip6.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panelProductos.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(213, 522);
            this.panel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnVentas);
            this.flowLayoutPanel1.Controls.Add(this.btnProductos);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 148);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(213, 374);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btnVentas
            // 
            this.btnVentas.FlatAppearance.BorderSize = 0;
            this.btnVentas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVentas.Font = new System.Drawing.Font("Consolas", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVentas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnVentas.Location = new System.Drawing.Point(4, 4);
            this.btnVentas.Margin = new System.Windows.Forms.Padding(4);
            this.btnVentas.Name = "btnVentas";
            this.btnVentas.Size = new System.Drawing.Size(205, 60);
            this.btnVentas.TabIndex = 615;
            this.btnVentas.Text = "Ventas";
            this.btnVentas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVentas.UseVisualStyleBackColor = true;
            this.btnVentas.Click += new System.EventHandler(this.btnVentas_Click);
            // 
            // btnProductos
            // 
            this.btnProductos.FlatAppearance.BorderSize = 0;
            this.btnProductos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProductos.Font = new System.Drawing.Font("Consolas", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProductos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnProductos.Location = new System.Drawing.Point(4, 72);
            this.btnProductos.Margin = new System.Windows.Forms.Padding(4);
            this.btnProductos.Name = "btnProductos";
            this.btnProductos.Size = new System.Drawing.Size(205, 60);
            this.btnProductos.TabIndex = 616;
            this.btnProductos.Text = "Productos + vendidos";
            this.btnProductos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProductos.UseVisualStyleBackColor = true;
            this.btnProductos.Click += new System.EventHandler(this.btnProductos_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnsalir);
            this.panel2.Controls.Add(this.PictureBox4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(213, 148);
            this.panel2.TabIndex = 0;
            // 
            // PictureBox4
            // 
            this.PictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PictureBox4.Image = global::RestCsharp.Properties.Resources.Buman;
            this.PictureBox4.Location = new System.Drawing.Point(0, 79);
            this.PictureBox4.Margin = new System.Windows.Forms.Padding(4);
            this.PictureBox4.Name = "PictureBox4";
            this.PictureBox4.Size = new System.Drawing.Size(213, 69);
            this.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBox4.TabIndex = 613;
            this.PictureBox4.TabStop = false;
            // 
            // panelVentas
            // 
            this.panelVentas.Controls.Add(this.reportViewer1);
            this.panelVentas.Controls.Add(this.panelCondicionales);
            this.panelVentas.Controls.Add(this.flowLayoutPanel2);
            this.panelVentas.Location = new System.Drawing.Point(604, 112);
            this.panelVentas.Name = "panelVentas";
            this.panelVentas.Size = new System.Drawing.Size(806, 537);
            this.panelVentas.TabIndex = 1;
            this.panelVentas.Visible = false;
            // 
            // reportViewer1
            // 
            this.reportViewer1.AccessibilityKeyMap = null;
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.Location = new System.Drawing.Point(0, 187);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(806, 350);
            this.reportViewer1.TabIndex = 4;
            this.reportViewer1.ViewMode = Telerik.ReportViewer.WinForms.ViewMode.PrintPreview;
            // 
            // panelCondicionales
            // 
            this.panelCondicionales.Controls.Add(this.PanelEmpleado);
            this.panelCondicionales.Controls.Add(this.btnHoy);
            this.panelCondicionales.Controls.Add(this.chekFiltros);
            this.panelCondicionales.Controls.Add(this.PanelFiltros);
            this.panelCondicionales.Controls.Add(this.MenuStrip6);
            this.panelCondicionales.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCondicionales.Location = new System.Drawing.Point(0, 53);
            this.panelCondicionales.Margin = new System.Windows.Forms.Padding(4);
            this.panelCondicionales.Name = "panelCondicionales";
            this.panelCondicionales.Size = new System.Drawing.Size(806, 134);
            this.panelCondicionales.TabIndex = 3;
            // 
            // PanelEmpleado
            // 
            this.PanelEmpleado.Controls.Add(this.txtEmpleado);
            this.PanelEmpleado.Controls.Add(this.Label4);
            this.PanelEmpleado.Location = new System.Drawing.Point(6, 72);
            this.PanelEmpleado.Margin = new System.Windows.Forms.Padding(4);
            this.PanelEmpleado.Name = "PanelEmpleado";
            this.PanelEmpleado.Size = new System.Drawing.Size(500, 51);
            this.PanelEmpleado.TabIndex = 616;
            this.PanelEmpleado.Visible = false;
            // 
            // txtEmpleado
            // 
            this.txtEmpleado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtEmpleado.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.txtEmpleado.Font = new System.Drawing.Font("Consolas", 14F);
            this.txtEmpleado.FormattingEnabled = true;
            this.txtEmpleado.Location = new System.Drawing.Point(112, 9);
            this.txtEmpleado.Margin = new System.Windows.Forms.Padding(4);
            this.txtEmpleado.Name = "txtEmpleado";
            this.txtEmpleado.Size = new System.Drawing.Size(376, 30);
            this.txtEmpleado.TabIndex = 3;
            this.txtEmpleado.SelectedIndexChanged += new System.EventHandler(this.txtEmpleado_SelectedIndexChanged);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("Consolas", 14F);
            this.Label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Label4.Location = new System.Drawing.Point(4, 13);
            this.Label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(100, 22);
            this.Label4.TabIndex = 2;
            this.Label4.Text = "Empleado:";
            // 
            // btnHoy
            // 
            this.btnHoy.BackColor = System.Drawing.Color.Transparent;
            this.btnHoy.FlatAppearance.BorderSize = 0;
            this.btnHoy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHoy.Font = new System.Drawing.Font("Consolas", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHoy.ForeColor = System.Drawing.Color.DimGray;
            this.btnHoy.Location = new System.Drawing.Point(0, 20);
            this.btnHoy.Margin = new System.Windows.Forms.Padding(4);
            this.btnHoy.Name = "btnHoy";
            this.btnHoy.Size = new System.Drawing.Size(155, 41);
            this.btnHoy.TabIndex = 615;
            this.btnHoy.Text = "Hasta HOY";
            this.btnHoy.UseVisualStyleBackColor = false;
            // 
            // chekFiltros
            // 
            this.chekFiltros.AutoSize = true;
            this.chekFiltros.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.chekFiltros.Location = new System.Drawing.Point(183, 36);
            this.chekFiltros.Margin = new System.Windows.Forms.Padding(4);
            this.chekFiltros.Name = "chekFiltros";
            this.chekFiltros.Size = new System.Drawing.Size(15, 14);
            this.chekFiltros.TabIndex = 613;
            this.chekFiltros.UseVisualStyleBackColor = true;
            this.chekFiltros.CheckedChanged += new System.EventHandler(this.chekFiltros_CheckedChanged);
            // 
            // PanelFiltros
            // 
            this.PanelFiltros.BackColor = System.Drawing.Color.White;
            this.PanelFiltros.Controls.Add(this.TXTFF);
            this.PanelFiltros.Controls.Add(this.TXTFI);
            this.PanelFiltros.Controls.Add(this.Label2);
            this.PanelFiltros.Controls.Add(this.Label3);
            this.PanelFiltros.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.PanelFiltros.Location = new System.Drawing.Point(313, 15);
            this.PanelFiltros.Margin = new System.Windows.Forms.Padding(4);
            this.PanelFiltros.Name = "PanelFiltros";
            this.PanelFiltros.Size = new System.Drawing.Size(469, 51);
            this.PanelFiltros.TabIndex = 611;
            this.PanelFiltros.Visible = false;
            // 
            // TXTFF
            // 
            this.TXTFF.Font = new System.Drawing.Font("Consolas", 14F);
            this.TXTFF.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.TXTFF.Location = new System.Drawing.Point(288, 11);
            this.TXTFF.Margin = new System.Windows.Forms.Padding(4);
            this.TXTFF.Name = "TXTFF";
            this.TXTFF.Size = new System.Drawing.Size(141, 29);
            this.TXTFF.TabIndex = 2;
            this.TXTFF.Value = new System.DateTime(2020, 6, 2, 0, 0, 0, 0);
            this.TXTFF.ValueChanged += new System.EventHandler(this.TXTFF_ValueChanged);
            // 
            // TXTFI
            // 
            this.TXTFI.Font = new System.Drawing.Font("Consolas", 14F);
            this.TXTFI.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.TXTFI.Location = new System.Drawing.Point(68, 10);
            this.TXTFI.Margin = new System.Windows.Forms.Padding(4);
            this.TXTFI.Name = "TXTFI";
            this.TXTFI.Size = new System.Drawing.Size(137, 29);
            this.TXTFI.TabIndex = 2;
            this.TXTFI.Value = new System.DateTime(2020, 6, 2, 0, 0, 0, 0);
            this.TXTFI.ValueChanged += new System.EventHandler(this.TXTFI_ValueChanged);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Bold);
            this.Label2.ForeColor = System.Drawing.Color.OrangeRed;
            this.Label2.Location = new System.Drawing.Point(213, 14);
            this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(70, 22);
            this.Label2.TabIndex = 1;
            this.Label2.Text = "Hasta:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Bold);
            this.Label3.ForeColor = System.Drawing.Color.OrangeRed;
            this.Label3.Location = new System.Drawing.Point(4, 14);
            this.Label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(70, 22);
            this.Label3.TabIndex = 1;
            this.Label3.Text = "Desde:";
            // 
            // MenuStrip6
            // 
            this.MenuStrip6.AutoSize = false;
            this.MenuStrip6.BackColor = System.Drawing.Color.Transparent;
            this.MenuStrip6.Dock = System.Windows.Forms.DockStyle.None;
            this.MenuStrip6.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TFILTROS});
            this.MenuStrip6.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.MenuStrip6.Location = new System.Drawing.Point(192, 25);
            this.MenuStrip6.Name = "MenuStrip6";
            this.MenuStrip6.ShowItemToolTips = true;
            this.MenuStrip6.Size = new System.Drawing.Size(121, 32);
            this.MenuStrip6.TabIndex = 612;
            this.MenuStrip6.Text = "MenuStrip6";
            // 
            // TFILTROS
            // 
            this.TFILTROS.BackColor = System.Drawing.Color.Transparent;
            this.TFILTROS.Checked = true;
            this.TFILTROS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TFILTROS.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Bold);
            this.TFILTROS.ForeColor = System.Drawing.Color.DimGray;
            this.TFILTROS.Image = ((System.Drawing.Image)(resources.GetObject("TFILTROS.Image")));
            this.TFILTROS.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TFILTROS.Name = "TFILTROS";
            this.TFILTROS.Size = new System.Drawing.Size(108, 28);
            this.TFILTROS.Text = "Filtros";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoScroll = true;
            this.flowLayoutPanel2.Controls.Add(this.panel4);
            this.flowLayoutPanel2.Controls.Add(this.panel5);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(806, 53);
            this.flowLayoutPanel2.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnResumenVentas);
            this.panel4.Controls.Add(this.PResumenVentas);
            this.panel4.Location = new System.Drawing.Point(4, 4);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(240, 48);
            this.panel4.TabIndex = 405;
            // 
            // btnResumenVentas
            // 
            this.btnResumenVentas.BackColor = System.Drawing.Color.Transparent;
            this.btnResumenVentas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnResumenVentas.FlatAppearance.BorderSize = 0;
            this.btnResumenVentas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResumenVentas.Font = new System.Drawing.Font("Consolas", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResumenVentas.ForeColor = System.Drawing.Color.DimGray;
            this.btnResumenVentas.Location = new System.Drawing.Point(0, 0);
            this.btnResumenVentas.Margin = new System.Windows.Forms.Padding(4);
            this.btnResumenVentas.Name = "btnResumenVentas";
            this.btnResumenVentas.Size = new System.Drawing.Size(240, 44);
            this.btnResumenVentas.TabIndex = 614;
            this.btnResumenVentas.Text = "Resumen de Ventas";
            this.btnResumenVentas.UseVisualStyleBackColor = false;
            this.btnResumenVentas.Click += new System.EventHandler(this.btnResumenVentas_Click);
            // 
            // PResumenVentas
            // 
            this.PResumenVentas.BackColor = System.Drawing.Color.OrangeRed;
            this.PResumenVentas.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PResumenVentas.ForeColor = System.Drawing.Color.OrangeRed;
            this.PResumenVentas.Location = new System.Drawing.Point(0, 44);
            this.PResumenVentas.Margin = new System.Windows.Forms.Padding(4);
            this.PResumenVentas.Name = "PResumenVentas";
            this.PResumenVentas.Size = new System.Drawing.Size(240, 4);
            this.PResumenVentas.TabIndex = 0;
            this.PResumenVentas.Visible = false;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnEmpleado);
            this.panel5.Controls.Add(this.PVentasPorempleado);
            this.panel5.Location = new System.Drawing.Point(252, 4);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(287, 48);
            this.panel5.TabIndex = 618;
            // 
            // btnEmpleado
            // 
            this.btnEmpleado.BackColor = System.Drawing.Color.Transparent;
            this.btnEmpleado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEmpleado.FlatAppearance.BorderSize = 0;
            this.btnEmpleado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmpleado.Font = new System.Drawing.Font("Consolas", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmpleado.ForeColor = System.Drawing.Color.DimGray;
            this.btnEmpleado.Location = new System.Drawing.Point(0, 0);
            this.btnEmpleado.Margin = new System.Windows.Forms.Padding(4);
            this.btnEmpleado.Name = "btnEmpleado";
            this.btnEmpleado.Size = new System.Drawing.Size(287, 44);
            this.btnEmpleado.TabIndex = 615;
            this.btnEmpleado.Text = "Ventas por Empleado";
            this.btnEmpleado.UseVisualStyleBackColor = false;
            this.btnEmpleado.Click += new System.EventHandler(this.btnEmpleado_Click);
            // 
            // PVentasPorempleado
            // 
            this.PVentasPorempleado.BackColor = System.Drawing.Color.OrangeRed;
            this.PVentasPorempleado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PVentasPorempleado.Location = new System.Drawing.Point(0, 44);
            this.PVentasPorempleado.Margin = new System.Windows.Forms.Padding(4);
            this.PVentasPorempleado.Name = "PVentasPorempleado";
            this.PVentasPorempleado.Size = new System.Drawing.Size(287, 4);
            this.PVentasPorempleado.TabIndex = 0;
            this.PVentasPorempleado.Visible = false;
            // 
            // panelProductos
            // 
            this.panelProductos.Controls.Add(this.reportViewer2);
            this.panelProductos.Location = new System.Drawing.Point(221, 36);
            this.panelProductos.Name = "panelProductos";
            this.panelProductos.Size = new System.Drawing.Size(361, 320);
            this.panelProductos.TabIndex = 2;
            this.panelProductos.Visible = false;
            // 
            // reportViewer2
            // 
            this.reportViewer2.AccessibilityKeyMap = null;
            this.reportViewer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer2.Location = new System.Drawing.Point(0, 0);
            this.reportViewer2.Name = "reportViewer2";
            this.reportViewer2.Size = new System.Drawing.Size(361, 320);
            this.reportViewer2.TabIndex = 0;
            // 
            // btnsalir
            // 
            this.btnsalir.BackgroundImage = global::RestCsharp.Properties.Resources.naranja;
            this.btnsalir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnsalir.FlatAppearance.BorderSize = 0;
            this.btnsalir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnsalir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnsalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsalir.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsalir.ForeColor = System.Drawing.Color.White;
            this.btnsalir.Location = new System.Drawing.Point(16, 15);
            this.btnsalir.Name = "btnsalir";
            this.btnsalir.Size = new System.Drawing.Size(117, 50);
            this.btnsalir.TabIndex = 619;
            this.btnsalir.Text = "Volver";
            this.btnsalir.UseVisualStyleBackColor = true;
            this.btnsalir.Click += new System.EventHandler(this.btnsalir_Click);
            // 
            // Menureportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.panelProductos);
            this.Controls.Add(this.panelVentas);
            this.Controls.Add(this.panel1);
            this.Name = "Menureportes";
            this.Size = new System.Drawing.Size(1115, 522);
            this.Load += new System.EventHandler(this.Menureportes_Load);
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).EndInit();
            this.panelVentas.ResumeLayout(false);
            this.panelCondicionales.ResumeLayout(false);
            this.panelCondicionales.PerformLayout();
            this.PanelEmpleado.ResumeLayout(false);
            this.PanelEmpleado.PerformLayout();
            this.PanelFiltros.ResumeLayout(false);
            this.PanelFiltros.PerformLayout();
            this.MenuStrip6.ResumeLayout(false);
            this.MenuStrip6.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panelProductos.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.PictureBox PictureBox4;
        internal System.Windows.Forms.Button btnVentas;
        private System.Windows.Forms.Panel panelVentas;
        internal System.Windows.Forms.Panel panelCondicionales;
        internal System.Windows.Forms.Panel PanelEmpleado;
        internal System.Windows.Forms.ComboBox txtEmpleado;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Button btnHoy;
        internal System.Windows.Forms.CheckBox chekFiltros;
        internal System.Windows.Forms.Panel PanelFiltros;
        internal System.Windows.Forms.DateTimePicker TXTFF;
        internal System.Windows.Forms.DateTimePicker TXTFI;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.MenuStrip MenuStrip6;
        internal System.Windows.Forms.ToolStripMenuItem TFILTROS;
        internal System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        internal System.Windows.Forms.Panel panel4;
        internal System.Windows.Forms.Button btnResumenVentas;
        internal System.Windows.Forms.Panel PResumenVentas;
        internal System.Windows.Forms.Panel panel5;
        internal System.Windows.Forms.Button btnEmpleado;
        internal System.Windows.Forms.Panel PVentasPorempleado;
        private Telerik.ReportViewer.WinForms.ReportViewer reportViewer1;
        internal System.Windows.Forms.Button btnProductos;
        private System.Windows.Forms.Panel panelProductos;
        private Telerik.ReportViewer.WinForms.ReportViewer reportViewer2;
        private System.Windows.Forms.Button btnsalir;
    }
}