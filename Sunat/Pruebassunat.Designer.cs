
namespace RestCsharp.Sunat
{
    partial class Pruebassunat
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
            this.btngenerarxml = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btngenerarxml
            // 
            this.btngenerarxml.Location = new System.Drawing.Point(35, 33);
            this.btngenerarxml.Name = "btngenerarxml";
            this.btngenerarxml.Size = new System.Drawing.Size(196, 51);
            this.btngenerarxml.TabIndex = 0;
            this.btngenerarxml.Text = "Generar xml";
            this.btngenerarxml.UseVisualStyleBackColor = true;
            this.btngenerarxml.Click += new System.EventHandler(this.btngenerarxml_Click);
            // 
            // Pruebassunat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.ClientSize = new System.Drawing.Size(684, 297);
            this.Controls.Add(this.btngenerarxml);
            this.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Pruebassunat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pruebassunat";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btngenerarxml;
    }
}