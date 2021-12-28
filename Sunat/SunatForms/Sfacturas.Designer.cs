
namespace Ada369Csharp.Presentacion.SunatForms
{
    partial class Sfacturas
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAceptadas = new System.Windows.Forms.Button();
            this.btnEspera = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.txtbuscar = new System.Windows.Forms.TextBox();
            this.Panelvisor = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.flowLayoutPanel1);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.Panel4);
            this.panel2.Controls.Add(this.txtbuscar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1056, 57);
            this.panel2.TabIndex = 3;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnAceptadas);
            this.flowLayoutPanel1.Controls.Add(this.btnEspera);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(271, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(785, 57);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btnAceptadas
            // 
            this.btnAceptadas.BackColor = System.Drawing.Color.Transparent;
            this.btnAceptadas.FlatAppearance.BorderSize = 0;
            this.btnAceptadas.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAceptadas.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAceptadas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptadas.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptadas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnAceptadas.Location = new System.Drawing.Point(3, 3);
            this.btnAceptadas.Name = "btnAceptadas";
            this.btnAceptadas.Size = new System.Drawing.Size(174, 43);
            this.btnAceptadas.TabIndex = 0;
            this.btnAceptadas.Text = "Aceptadas (10)";
            this.btnAceptadas.UseVisualStyleBackColor = false;
            // 
            // btnEspera
            // 
            this.btnEspera.BackColor = System.Drawing.Color.Transparent;
            this.btnEspera.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnEspera.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnEspera.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEspera.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEspera.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(198)))), ((int)(((byte)(73)))));
            this.btnEspera.Location = new System.Drawing.Point(183, 3);
            this.btnEspera.Name = "btnEspera";
            this.btnEspera.Size = new System.Drawing.Size(346, 43);
            this.btnEspera.TabIndex = 1;
            this.btnEspera.Text = "En espera (50)";
            this.btnEspera.UseVisualStyleBackColor = false;
            this.btnEspera.Click += new System.EventHandler(this.btnEspera_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::RestCsharp.Properties.Resources.Caja_activa;
            this.pictureBox1.Location = new System.Drawing.Point(14, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 28);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 608;
            this.pictureBox1.TabStop = false;
            // 
            // Panel4
            // 
            this.Panel4.BackColor = System.Drawing.Color.White;
            this.Panel4.Location = new System.Drawing.Point(41, 48);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(224, 1);
            this.Panel4.TabIndex = 607;
            // 
            // txtbuscar
            // 
            this.txtbuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.txtbuscar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtbuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.txtbuscar.ForeColor = System.Drawing.Color.White;
            this.txtbuscar.Location = new System.Drawing.Point(40, 15);
            this.txtbuscar.Name = "txtbuscar";
            this.txtbuscar.Size = new System.Drawing.Size(225, 31);
            this.txtbuscar.TabIndex = 606;
            this.txtbuscar.TextChanged += new System.EventHandler(this.txtbuscar_TextChanged);
            // 
            // Panelvisor
            // 
            this.Panelvisor.AutoScroll = true;
            this.Panelvisor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panelvisor.Location = new System.Drawing.Point(0, 57);
            this.Panelvisor.Name = "Panelvisor";
            this.Panelvisor.Size = new System.Drawing.Size(1056, 518);
            this.Panelvisor.TabIndex = 4;
            // 
            // Sfacturas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.Controls.Add(this.Panelvisor);
            this.Controls.Add(this.panel2);
            this.Name = "Sfacturas";
            this.Size = new System.Drawing.Size(1056, 575);
            this.Load += new System.EventHandler(this.Sfacturas_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnAceptadas;
        private System.Windows.Forms.Button btnEspera;
        private System.Windows.Forms.PictureBox pictureBox1;
        internal System.Windows.Forms.Panel Panel4;
        internal System.Windows.Forms.TextBox txtbuscar;
        private System.Windows.Forms.FlowLayoutPanel Panelvisor;
    }
}
