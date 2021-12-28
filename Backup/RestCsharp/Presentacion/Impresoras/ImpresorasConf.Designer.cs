
namespace RestCsharp.Presentacion.Impresoras
{
    partial class ImpresorasConf
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImpresorasConf));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.PanelAreas = new System.Windows.Forms.FlowLayoutPanel();
            this.Label1 = new System.Windows.Forms.Label();
            this.panel_Bienvenida = new System.Windows.Forms.Panel();
            this.Label3 = new System.Windows.Forms.Label();
            this.PanelImpresoras = new System.Windows.Forms.FlowLayoutPanel();
            this.rptPruebas = new Telerik.ReportViewer.WinForms.ReportViewer();
            this.Panel1.SuspendLayout();
            this.panel_Bienvenida.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.Panel1.Controls.Add(this.PanelAreas);
            this.Panel1.Controls.Add(this.Label1);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(306, 519);
            this.Panel1.TabIndex = 4;
            // 
            // PanelAreas
            // 
            this.PanelAreas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelAreas.Location = new System.Drawing.Point(0, 43);
            this.PanelAreas.Name = "PanelAreas";
            this.PanelAreas.Size = new System.Drawing.Size(306, 476);
            this.PanelAreas.TabIndex = 0;
            // 
            // Label1
            // 
            this.Label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.Color.White;
            this.Label1.Location = new System.Drawing.Point(0, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(306, 43);
            this.Label1.TabIndex = 1;
            this.Label1.Text = "Impresoras";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_Bienvenida
            // 
            this.panel_Bienvenida.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.panel_Bienvenida.Controls.Add(this.Label3);
            this.panel_Bienvenida.Location = new System.Drawing.Point(378, 43);
            this.panel_Bienvenida.Name = "panel_Bienvenida";
            this.panel_Bienvenida.Size = new System.Drawing.Size(471, 123);
            this.panel_Bienvenida.TabIndex = 7;
            // 
            // Label3
            // 
            this.Label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.Color.DimGray;
            this.Label3.Location = new System.Drawing.Point(0, 0);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(471, 123);
            this.Label3.TabIndex = 627;
            this.Label3.Text = "Elije un área de impresión";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PanelImpresoras
            // 
            this.PanelImpresoras.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.PanelImpresoras.Location = new System.Drawing.Point(339, 208);
            this.PanelImpresoras.Name = "PanelImpresoras";
            this.PanelImpresoras.Size = new System.Drawing.Size(517, 218);
            this.PanelImpresoras.TabIndex = 1;
            this.PanelImpresoras.Visible = false;
            // 
            // rptPruebas
            // 
            this.rptPruebas.AccessibilityKeyMap = null;
            this.rptPruebas.Location = new System.Drawing.Point(562, 446);
            this.rptPruebas.Name = "rptPruebas";
            this.rptPruebas.Size = new System.Drawing.Size(224, 61);
            this.rptPruebas.TabIndex = 8;
            this.rptPruebas.Visible = false;
            // 
            // ImpresorasConf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 519);
            this.Controls.Add(this.rptPruebas);
            this.Controls.Add(this.PanelImpresoras);
            this.Controls.Add(this.panel_Bienvenida);
            this.Controls.Add(this.Panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ImpresorasConf";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuracion de impresoras";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ImpresorasConf_Load);
            this.Panel1.ResumeLayout(false);
            this.panel_Bienvenida.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.FlowLayoutPanel PanelAreas;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Panel panel_Bienvenida;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.FlowLayoutPanel PanelImpresoras;
        private Telerik.ReportViewer.WinForms.ReportViewer rptPruebas;
    }
}