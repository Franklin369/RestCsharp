
namespace RestCsharp.Presentacion.Conexionremota
{
    partial class Asistenteconexion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Asistenteconexion));
            this.Label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnconectar = new System.Windows.Forms.Button();
            this.txtservidor = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Label3
            // 
            this.Label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 34F, System.Drawing.FontStyle.Bold);
            this.Label3.ForeColor = System.Drawing.Color.White;
            this.Label3.Location = new System.Drawing.Point(0, 0);
            this.Label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(800, 190);
            this.Label3.TabIndex = 626;
            this.Label3.Text = "Conectandose a la PC Principal...";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.PictureBox1);
            this.panel1.Controls.Add(this.btnconectar);
            this.panel1.Controls.Add(this.txtservidor);
            this.panel1.Controls.Add(this.Label5);
            this.panel1.Controls.Add(this.Label1);
            this.panel1.Controls.Add(this.Label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 190);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 446);
            this.panel1.TabIndex = 627;
            // 
            // PictureBox1
            // 
            this.PictureBox1.Image = global::RestCsharp.Properties.Resources.ipimagen;
            this.PictureBox1.Location = new System.Drawing.Point(119, 148);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(343, 207);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBox1.TabIndex = 635;
            this.PictureBox1.TabStop = false;
            // 
            // btnconectar
            // 
            this.btnconectar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnconectar.Location = new System.Drawing.Point(119, 54);
            this.btnconectar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnconectar.Name = "btnconectar";
            this.btnconectar.Size = new System.Drawing.Size(272, 57);
            this.btnconectar.TabIndex = 634;
            this.btnconectar.Text = "Conectar";
            this.btnconectar.UseVisualStyleBackColor = true;
            this.btnconectar.Click += new System.EventHandler(this.btnconectar_Click);
            // 
            // txtservidor
            // 
            this.txtservidor.Location = new System.Drawing.Point(119, 18);
            this.txtservidor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtservidor.Name = "txtservidor";
            this.txtservidor.Size = new System.Drawing.Size(442, 26);
            this.txtservidor.TabIndex = 630;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.ForeColor = System.Drawing.Color.White;
            this.Label5.Location = new System.Drawing.Point(115, 367);
            this.Label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(432, 40);
            this.Label5.TabIndex = 631;
            this.Label5.Text = "Abra el sistema en la PC Principal y Copie los caracteres\r\nLuego agregelos en la " +
    "caja de texto y de click a CONECTAR";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.ForeColor = System.Drawing.Color.White;
            this.Label1.Location = new System.Drawing.Point(115, 125);
            this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(61, 20);
            this.Label1.TabIndex = 632;
            this.Label1.Text = "Tutorial";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.ForeColor = System.Drawing.Color.White;
            this.Label2.Location = new System.Drawing.Point(9, 21);
            this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(102, 20);
            this.Label2.TabIndex = 633;
            this.Label2.Text = "Ingresa la IP:";
            // 
            // Asistenteconexion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.ClientSize = new System.Drawing.Size(800, 610);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Label3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Asistenteconexion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asistente conexion";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Asistenteconexion_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label Label3;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.PictureBox PictureBox1;
        internal System.Windows.Forms.Button btnconectar;
        internal System.Windows.Forms.TextBox txtservidor;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label2;
    }
}