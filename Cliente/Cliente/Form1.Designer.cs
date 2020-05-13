namespace Cliente
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.textDialogo = new System.Windows.Forms.TextBox();
            this.textEscreva = new System.Windows.Forms.TextBox();
            this.Dialogo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textDialogo
            // 
            this.textDialogo.Location = new System.Drawing.Point(12, 50);
            this.textDialogo.Multiline = true;
            this.textDialogo.Name = "textDialogo";
            this.textDialogo.ReadOnly = true;
            this.textDialogo.Size = new System.Drawing.Size(296, 203);
            this.textDialogo.TabIndex = 0;
            // 
            // textEscreva
            // 
            this.textEscreva.Location = new System.Drawing.Point(12, 272);
            this.textEscreva.Multiline = true;
            this.textEscreva.Name = "textEscreva";
            this.textEscreva.Size = new System.Drawing.Size(296, 54);
            this.textEscreva.TabIndex = 1;
            this.textEscreva.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textEscreva_KeyDown);
            this.textEscreva.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textEscreva_KeyUp);
            // 
            // Dialogo
            // 
            this.Dialogo.AutoSize = true;
            this.Dialogo.Location = new System.Drawing.Point(9, 34);
            this.Dialogo.Name = "Dialogo";
            this.Dialogo.Size = new System.Drawing.Size(43, 13);
            this.Dialogo.TabIndex = 2;
            this.Dialogo.Text = "Dialogo";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 256);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Escreva";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 338);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Dialogo);
            this.Controls.Add(this.textEscreva);
            this.Controls.Add(this.textDialogo);
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Cliente";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textDialogo;
        private System.Windows.Forms.TextBox textEscreva;
        private System.Windows.Forms.Label Dialogo;
        private System.Windows.Forms.Label label1;
    }
}

