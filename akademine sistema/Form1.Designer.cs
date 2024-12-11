namespace akademine_sistema
{
    partial class Form1
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
            this.txtVardas = new System.Windows.Forms.TextBox();
            this.txtSlaptazodis = new System.Windows.Forms.TextBox();
            this.btnPrisijungiti = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtVardas
            // 
            this.txtVardas.Location = new System.Drawing.Point(249, 117);
            this.txtVardas.Name = "txtVardas";
            this.txtVardas.Size = new System.Drawing.Size(168, 22);
            this.txtVardas.TabIndex = 0;
            // 
            // txtSlaptazodis
            // 
            this.txtSlaptazodis.Location = new System.Drawing.Point(249, 188);
            this.txtSlaptazodis.Name = "txtSlaptazodis";
            this.txtSlaptazodis.Size = new System.Drawing.Size(168, 22);
            this.txtSlaptazodis.TabIndex = 1;
            this.txtSlaptazodis.UseSystemPasswordChar = true;
            // 
            // btnPrisijungiti
            // 
            this.btnPrisijungiti.Location = new System.Drawing.Point(282, 235);
            this.btnPrisijungiti.Name = "btnPrisijungiti";
            this.btnPrisijungiti.Size = new System.Drawing.Size(86, 32);
            this.btnPrisijungiti.TabIndex = 2;
            this.btnPrisijungiti.Text = "Prisijungti";
            this.btnPrisijungiti.UseVisualStyleBackColor = true;
            this.btnPrisijungiti.Click += new System.EventHandler(this.btnPrisijungiti_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(98, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Prisijungimo vardas:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(145, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Slaptažodis:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnPrisijungiti);
            this.Controls.Add(this.txtSlaptazodis);
            this.Controls.Add(this.txtVardas);
            this.Name = "Form1";
            this.Text = "Prisijungimas";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtVardas;
        private System.Windows.Forms.TextBox txtSlaptazodis;
        private System.Windows.Forms.Button btnPrisijungiti;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

