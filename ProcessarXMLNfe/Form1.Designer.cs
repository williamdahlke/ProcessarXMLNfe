namespace ProcessarXMLNfe
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txt_chave = new System.Windows.Forms.TextBox();
            this.label_chave = new System.Windows.Forms.Label();
            this.bt_processar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_chave
            // 
            this.txt_chave.Location = new System.Drawing.Point(122, 31);
            this.txt_chave.MaxLength = 44;
            this.txt_chave.Name = "txt_chave";
            this.txt_chave.Size = new System.Drawing.Size(314, 20);
            this.txt_chave.TabIndex = 0;
            // 
            // label_chave
            // 
            this.label_chave.AutoSize = true;
            this.label_chave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_chave.Location = new System.Drawing.Point(28, 34);
            this.label_chave.Name = "label_chave";
            this.label_chave.Size = new System.Drawing.Size(88, 13);
            this.label_chave.TabIndex = 1;
            this.label_chave.Text = "Chave da NFe";
            // 
            // bt_processar
            // 
            this.bt_processar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_processar.Location = new System.Drawing.Point(200, 87);
            this.bt_processar.Name = "bt_processar";
            this.bt_processar.Size = new System.Drawing.Size(76, 24);
            this.bt_processar.TabIndex = 2;
            this.bt_processar.Text = "Processar XML";
            this.bt_processar.UseVisualStyleBackColor = true;
            this.bt_processar.Click += new System.EventHandler(this.bt_processar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 128);
            this.Controls.Add(this.bt_processar);
            this.Controls.Add(this.label_chave);
            this.Controls.Add(this.txt_chave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Processar Arquivos XML";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_chave;
        private System.Windows.Forms.Label label_chave;
        private System.Windows.Forms.Button bt_processar;
    }
}

