
namespace RestCsharp.Presentacion.Copiasbd
{
    partial class GenerarAut
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
            this.components = new System.ComponentModel.Container();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btncerrar = new System.Windows.Forms.Button();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbltiempo = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Panel12 = new System.Windows.Forms.Panel();
            this.Label1 = new System.Windows.Forms.Label();
            this.timerContador = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.Panel12.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Panel1.Controls.Add(this.pictureBox1);
            this.Panel1.Controls.Add(this.panel2);
            this.Panel1.Controls.Add(this.lbltiempo);
            this.Panel1.Controls.Add(this.Label4);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel1.Location = new System.Drawing.Point(0, 45);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(500, 447);
            this.Panel1.TabIndex = 618;
            this.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel1_Paint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pictureBox1.Image = global::RestCsharp.Properties.Resources.Buman;
            this.pictureBox1.Location = new System.Drawing.Point(0, 401);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(500, 46);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 614;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btncerrar);
            this.panel2.Controls.Add(this.txtRuta);
            this.panel2.Controls.Add(this.Label2);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 143);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(500, 69);
            this.panel2.TabIndex = 615;
            // 
            // btncerrar
            // 
            this.btncerrar.BackColor = System.Drawing.Color.Transparent;
            this.btncerrar.BackgroundImage = global::RestCsharp.Properties.Resources.naranja;
            this.btncerrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btncerrar.Dock = System.Windows.Forms.DockStyle.Left;
            this.btncerrar.FlatAppearance.BorderSize = 0;
            this.btncerrar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btncerrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btncerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncerrar.ForeColor = System.Drawing.Color.White;
            this.btncerrar.Location = new System.Drawing.Point(10, 0);
            this.btncerrar.Name = "btncerrar";
            this.btncerrar.Size = new System.Drawing.Size(125, 69);
            this.btncerrar.TabIndex = 613;
            this.btncerrar.Text = "Cancelar";
            this.btncerrar.UseVisualStyleBackColor = false;
            this.btncerrar.Click += new System.EventHandler(this.BtnCerrar_turno_Click);
            // 
            // txtRuta
            // 
            this.txtRuta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.txtRuta.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRuta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtRuta.Enabled = false;
            this.txtRuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtRuta.ForeColor = System.Drawing.Color.White;
            this.txtRuta.Location = new System.Drawing.Point(166, 41);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(307, 19);
            this.txtRuta.TabIndex = 1;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Label2.ForeColor = System.Drawing.Color.White;
            this.Label2.Location = new System.Drawing.Point(162, 18);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(218, 20);
            this.Label2.TabIndex = 0;
            this.Label2.Text = "Ruta de Copias de Seguridad";
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(10, 69);
            this.panel3.TabIndex = 614;
            // 
            // lbltiempo
            // 
            this.lbltiempo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbltiempo.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F);
            this.lbltiempo.ForeColor = System.Drawing.Color.White;
            this.lbltiempo.Location = new System.Drawing.Point(0, 67);
            this.lbltiempo.Name = "lbltiempo";
            this.lbltiempo.Size = new System.Drawing.Size(500, 76);
            this.lbltiempo.TabIndex = 599;
            this.lbltiempo.Text = "10";
            this.lbltiempo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.Label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Label4.Location = new System.Drawing.Point(0, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(500, 67);
            this.Label4.TabIndex = 0;
            this.Label4.Text = "SE GENERARA COPIA DE SEGURIDAD \r\nPROGRAMADA EN:";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Panel12
            // 
            this.Panel12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.Panel12.Controls.Add(this.Label1);
            this.Panel12.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel12.ForeColor = System.Drawing.Color.White;
            this.Panel12.Location = new System.Drawing.Point(0, 0);
            this.Panel12.Name = "Panel12";
            this.Panel12.Size = new System.Drawing.Size(500, 45);
            this.Panel12.TabIndex = 617;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.Label1.Location = new System.Drawing.Point(3, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(193, 24);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Copia de seguridad";
            // 
            // timerContador
            // 
            this.timerContador.Interval = 1000;
            this.timerContador.Tick += new System.EventHandler(this.timerContador_Tick);
            // 
            // timer1
            // 
            // 
            // GenerarAut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.Panel12);
            this.Name = "GenerarAut";
            this.Size = new System.Drawing.Size(500, 492);
            this.Load += new System.EventHandler(this.GenerarAut_Load);
            this.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.Panel12.ResumeLayout(false);
            this.Panel12.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.Button btncerrar;
        internal System.Windows.Forms.TextBox txtRuta;
        internal System.Windows.Forms.Label Label2;
        private System.Windows.Forms.Panel panel3;
        internal System.Windows.Forms.Label lbltiempo;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Panel Panel12;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Timer timerContador;
        private System.Windows.Forms.Timer timer1;
    }
}
