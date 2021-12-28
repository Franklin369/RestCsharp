namespace RestCsharp.Presentacion.PUNTO_DE_VENTA
{
    partial class MostradorProductos
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.Label16 = new System.Windows.Forms.Label();
            this.PanelProductos = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.UI_GradientPanel3 = new UIDC.UI_GradientPanel();
            this.btnadelante = new System.Windows.Forms.Button();
            this.PanelProductos.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.UI_GradientPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label16
            // 
            this.Label16.BackColor = System.Drawing.Color.Transparent;
            this.Label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label16.ForeColor = System.Drawing.Color.White;
            this.Label16.Location = new System.Drawing.Point(0, 0);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(563, 35);
            this.Label16.TabIndex = 5;
            this.Label16.Text = "PRODUCTOS ";
            this.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PanelProductos
            // 
            this.PanelProductos.Controls.Add(this.panel1);
            this.PanelProductos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelProductos.Location = new System.Drawing.Point(0, 35);
            this.PanelProductos.Name = "PanelProductos";
            this.PanelProductos.Size = new System.Drawing.Size(704, 372);
            this.PanelProductos.TabIndex = 4;
            this.PanelProductos.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelProductos_Paint);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(103, 114);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 78);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pictureBox1.Location = new System.Drawing.Point(0, 78);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(103, 36);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // UI_GradientPanel3
            // 
            this.UI_GradientPanel3.BackColor = System.Drawing.Color.White;
            this.UI_GradientPanel3.Controls.Add(this.Label16);
            this.UI_GradientPanel3.Controls.Add(this.btnadelante);
            this.UI_GradientPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.UI_GradientPanel3.Location = new System.Drawing.Point(0, 0);
            this.UI_GradientPanel3.Name = "UI_GradientPanel3";
            this.UI_GradientPanel3.Size = new System.Drawing.Size(704, 35);
            this.UI_GradientPanel3.TabIndex = 3;
            this.UI_GradientPanel3.UIBackColor = System.Drawing.Color.Navy;
            this.UI_GradientPanel3.UIBottomLeft = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.UI_GradientPanel3.UIBottomRight = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.UI_GradientPanel3.UIForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.UI_GradientPanel3.UIPrimerColor = System.Drawing.Color.MidnightBlue;
            this.UI_GradientPanel3.UIStyle = UIDC.UI_GradientPanel.GradientStyle.Corners;
            this.UI_GradientPanel3.UITopLeft = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.UI_GradientPanel3.UITopRight = System.Drawing.Color.Black;
            // 
            // btnadelante
            // 
            this.btnadelante.BackColor = System.Drawing.Color.Transparent;
            this.btnadelante.BackgroundImage = global::RestCsharp.Properties.Resources.fecha_derecha;
            this.btnadelante.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnadelante.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnadelante.FlatAppearance.BorderSize = 0;
            this.btnadelante.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnadelante.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnadelante.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnadelante.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnadelante.ForeColor = System.Drawing.Color.White;
            this.btnadelante.Location = new System.Drawing.Point(563, 0);
            this.btnadelante.Name = "btnadelante";
            this.btnadelante.Size = new System.Drawing.Size(141, 35);
            this.btnadelante.TabIndex = 8;
            this.btnadelante.UseVisualStyleBackColor = false;
            this.btnadelante.Click += new System.EventHandler(this.btnadelante_Click);
            // 
            // MostradorProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PanelProductos);
            this.Controls.Add(this.UI_GradientPanel3);
            this.Name = "MostradorProductos";
            this.Size = new System.Drawing.Size(704, 407);
            this.Load += new System.EventHandler(this.MostradorProductos_Load);
            this.PanelProductos.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.UI_GradientPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label Label16;
        internal System.Windows.Forms.FlowLayoutPanel PanelProductos;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        internal UIDC.UI_GradientPanel UI_GradientPanel3;
        internal System.Windows.Forms.Button btnadelante;
    }
}
