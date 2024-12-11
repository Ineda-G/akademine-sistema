namespace akademine_sistema
{
    partial class destytojas
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
            this.cmbStudentai = new System.Windows.Forms.ComboBox();
            this.dgvPazymiai = new System.Windows.Forms.DataGridView();
            this.btnPakeisti = new System.Windows.Forms.Button();
            this.btnIstrinti = new System.Windows.Forms.Button();
            this.btnPrideti = new System.Windows.Forms.Button();
            this.Atsijungti = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPazymiai)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbStudentai
            // 
            this.cmbStudentai.FormattingEnabled = true;
            this.cmbStudentai.Location = new System.Drawing.Point(23, 104);
            this.cmbStudentai.Name = "cmbStudentai";
            this.cmbStudentai.Size = new System.Drawing.Size(121, 24);
            this.cmbStudentai.TabIndex = 0;
            this.cmbStudentai.SelectedIndexChanged += new System.EventHandler(this.cmbStudentai_SelectedIndexChanged);
            // 
            // dgvPazymiai
            // 
            this.dgvPazymiai.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPazymiai.Location = new System.Drawing.Point(176, 104);
            this.dgvPazymiai.Name = "dgvPazymiai";
            this.dgvPazymiai.RowHeadersWidth = 51;
            this.dgvPazymiai.RowTemplate.Height = 24;
            this.dgvPazymiai.Size = new System.Drawing.Size(466, 322);
            this.dgvPazymiai.TabIndex = 1;
            // 
            // btnPakeisti
            // 
            this.btnPakeisti.Location = new System.Drawing.Point(23, 296);
            this.btnPakeisti.Name = "btnPakeisti";
            this.btnPakeisti.Size = new System.Drawing.Size(75, 23);
            this.btnPakeisti.TabIndex = 2;
            this.btnPakeisti.Text = "Pakeisti";
            this.btnPakeisti.UseVisualStyleBackColor = true;
            this.btnPakeisti.Click += new System.EventHandler(this.btnPakeisti_Click);
            // 
            // btnIstrinti
            // 
            this.btnIstrinti.Location = new System.Drawing.Point(23, 267);
            this.btnIstrinti.Name = "btnIstrinti";
            this.btnIstrinti.Size = new System.Drawing.Size(75, 23);
            this.btnIstrinti.TabIndex = 3;
            this.btnIstrinti.Text = "Ištrinti";
            this.btnIstrinti.UseVisualStyleBackColor = true;
            this.btnIstrinti.Click += new System.EventHandler(this.btnIstrinti_Click);
            // 
            // btnPrideti
            // 
            this.btnPrideti.Location = new System.Drawing.Point(23, 238);
            this.btnPrideti.Name = "btnPrideti";
            this.btnPrideti.Size = new System.Drawing.Size(75, 23);
            this.btnPrideti.TabIndex = 4;
            this.btnPrideti.Text = "Pridėti";
            this.btnPrideti.UseVisualStyleBackColor = true;
            this.btnPrideti.Click += new System.EventHandler(this.btnPrideti_Click);
            // 
            // Atsijungti
            // 
            this.Atsijungti.Location = new System.Drawing.Point(643, 33);
            this.Atsijungti.Name = "Atsijungti";
            this.Atsijungti.Size = new System.Drawing.Size(90, 32);
            this.Atsijungti.TabIndex = 5;
            this.Atsijungti.Text = "Atsijungti";
            this.Atsijungti.UseVisualStyleBackColor = true;
            this.Atsijungti.Click += new System.EventHandler(this.Atsijungti_Click);
            // 
            // destytojas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Atsijungti);
            this.Controls.Add(this.btnPrideti);
            this.Controls.Add(this.btnIstrinti);
            this.Controls.Add(this.btnPakeisti);
            this.Controls.Add(this.dgvPazymiai);
            this.Controls.Add(this.cmbStudentai);
            this.Name = "destytojas";
            this.Text = "destytojas";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPazymiai)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbStudentai;
        private System.Windows.Forms.DataGridView dgvPazymiai;
        private System.Windows.Forms.Button btnPakeisti;
        private System.Windows.Forms.Button btnIstrinti;
        private System.Windows.Forms.Button btnPrideti;
        private System.Windows.Forms.Button Atsijungti;
    }
}