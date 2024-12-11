namespace akademine_sistema
{
    partial class studentas
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
            this.dgvPazymiai = new System.Windows.Forms.DataGridView();
            this.Atsijungti = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPazymiai)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPazymiai
            // 
            this.dgvPazymiai.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPazymiai.Location = new System.Drawing.Point(152, 85);
            this.dgvPazymiai.Name = "dgvPazymiai";
            this.dgvPazymiai.RowHeadersWidth = 51;
            this.dgvPazymiai.RowTemplate.Height = 24;
            this.dgvPazymiai.Size = new System.Drawing.Size(430, 271);
            this.dgvPazymiai.TabIndex = 0;
            // 
            // Atsijungti
            // 
            this.Atsijungti.Location = new System.Drawing.Point(622, 30);
            this.Atsijungti.Name = "Atsijungti";
            this.Atsijungti.Size = new System.Drawing.Size(112, 31);
            this.Atsijungti.TabIndex = 1;
            this.Atsijungti.Text = "Atsijungti";
            this.Atsijungti.UseVisualStyleBackColor = true;
            this.Atsijungti.Click += new System.EventHandler(this.Atsijungti_Click);
            // 
            // studentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Atsijungti);
            this.Controls.Add(this.dgvPazymiai);
            this.Name = "studentas";
            this.Text = "studentas";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPazymiai)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPazymiai;
        private System.Windows.Forms.Button Atsijungti;
    }
}