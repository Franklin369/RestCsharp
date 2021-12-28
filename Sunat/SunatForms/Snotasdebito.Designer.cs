
namespace Ada369Csharp.Presentacion.SunatForms
{
    partial class Snotasdebito
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
            this.PanelNcredito = new System.Windows.Forms.FlowLayoutPanel();
            this.btnagregar = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.txtbuscar = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelNcredito
            // 
            this.PanelNcredito.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelNcredito.Location = new System.Drawing.Point(0, 57);
            this.PanelNcredito.Name = "PanelNcredito";
            this.PanelNcredito.Size = new System.Drawing.Size(971, 536);
            this.PanelNcredito.TabIndex = 5;
            // 
            // btnagregar
            // 
            this.btnagregar.BackColor = System.Drawing.Color.Transparent;
            this.btnagregar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnagregar.FlatAppearance.BorderSize = 0;
            this.btnagregar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnagregar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnagregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnagregar.Font = new System.Drawing.Font("Consolas", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnagregar.ForeColor = System.Drawing.Color.White;
            this.btnagregar.Location = new System.Drawing.Point(399, 3);
            this.btnagregar.Name = "btnagregar";
            this.btnagregar.Size = new System.Drawing.Size(171, 49);
            this.btnagregar.TabIndex = 604;
            this.btnagregar.Text = "Agregar +";
            this.btnagregar.UseVisualStyleBackColor = false;
            this.btnagregar.Click += new System.EventHandler(this.btnagregar_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(576, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(401, 51);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnagregar);
            this.panel2.Controls.Add(this.flowLayoutPanel1);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.Panel4);
            this.panel2.Controls.Add(this.txtbuscar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(971, 57);
            this.panel2.TabIndex = 4;
            // 
            // Panel4
            // 
            this.Panel4.BackColor = System.Drawing.Color.White;
            this.Panel4.Location = new System.Drawing.Point(64, 45);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(310, 1);
            this.Panel4.TabIndex = 607;
            // 
            // txtbuscar
            // 
            this.txtbuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.txtbuscar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtbuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.txtbuscar.ForeColor = System.Drawing.Color.White;
            this.txtbuscar.Location = new System.Drawing.Point(63, 12);
            this.txtbuscar.Name = "txtbuscar";
            this.txtbuscar.PasswordChar = '*';
            this.txtbuscar.Size = new System.Drawing.Size(311, 31);
            this.txtbuscar.TabIndex = 606;
            this.txtbuscar.TextChanged += new System.EventHandler(this.txtpaswwor_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::RestCsharp.Properties.Resources.Caja_activa;
            this.pictureBox1.Location = new System.Drawing.Point(37, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 28);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 608;
            this.pictureBox1.TabStop = false;
            // 
            // Snotasdebito
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.Controls.Add(this.PanelNcredito);
            this.Controls.Add(this.panel2);
            this.Name = "Snotasdebito";
            this.Size = new System.Drawing.Size(971, 593);
            this.Load += new System.EventHandler(this.Snotasdebito_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel PanelNcredito;
        internal System.Windows.Forms.Button btnagregar;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        internal System.Windows.Forms.Panel Panel4;
        internal System.Windows.Forms.TextBox txtbuscar;
    }
}
