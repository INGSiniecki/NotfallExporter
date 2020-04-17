namespace NotfallExporterUI
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxError = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxImport = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxBackup = new System.Windows.Forms.TextBox();
            this.buttonErrorDurchsuchen = new System.Windows.Forms.Button();
            this.buttonImportDurchsuchen = new System.Windows.Forms.Button();
            this.buttonBackupDurchsuchen = new System.Windows.Forms.Button();
            this.button_startImport = new System.Windows.Forms.Button();
            this.button_stopImport = new System.Windows.Forms.Button();
            this.buttonIdexSpezifikationDurchsuchen = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxIndexSpezifikation = new System.Windows.Forms.TextBox();
            this.buttonAccountConfigDurchsuchen = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxAccountConfig = new System.Windows.Forms.TextBox();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxError
            // 
            this.textBoxError.Location = new System.Drawing.Point(17, 68);
            this.textBoxError.Name = "textBoxError";
            this.textBoxError.Size = new System.Drawing.Size(379, 26);
            this.textBoxError.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Error-Verzeichnis";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Import-Verzeichnis";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // textBoxImport
            // 
            this.textBoxImport.Location = new System.Drawing.Point(17, 141);
            this.textBoxImport.Name = "textBoxImport";
            this.textBoxImport.Size = new System.Drawing.Size(379, 26);
            this.textBoxImport.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Backup-Verzeichnis";
            // 
            // textBoxBackup
            // 
            this.textBoxBackup.Location = new System.Drawing.Point(17, 213);
            this.textBoxBackup.Name = "textBoxBackup";
            this.textBoxBackup.Size = new System.Drawing.Size(379, 26);
            this.textBoxBackup.TabIndex = 5;
            // 
            // buttonErrorDurchsuchen
            // 
            this.buttonErrorDurchsuchen.Location = new System.Drawing.Point(402, 65);
            this.buttonErrorDurchsuchen.Name = "buttonErrorDurchsuchen";
            this.buttonErrorDurchsuchen.Size = new System.Drawing.Size(139, 29);
            this.buttonErrorDurchsuchen.TabIndex = 9;
            this.buttonErrorDurchsuchen.Text = "Durchsuchen...";
            this.buttonErrorDurchsuchen.UseVisualStyleBackColor = true;
            this.buttonErrorDurchsuchen.Click += new System.EventHandler(this.buttonErrorDurchsuchen_Click);
            // 
            // buttonImportDurchsuchen
            // 
            this.buttonImportDurchsuchen.Location = new System.Drawing.Point(402, 138);
            this.buttonImportDurchsuchen.Name = "buttonImportDurchsuchen";
            this.buttonImportDurchsuchen.Size = new System.Drawing.Size(139, 29);
            this.buttonImportDurchsuchen.TabIndex = 11;
            this.buttonImportDurchsuchen.Text = "Durchsuchen...";
            this.buttonImportDurchsuchen.UseVisualStyleBackColor = true;
            this.buttonImportDurchsuchen.Click += new System.EventHandler(this.buttonImportDurchsuchen_Click);
            // 
            // buttonBackupDurchsuchen
            // 
            this.buttonBackupDurchsuchen.Location = new System.Drawing.Point(402, 210);
            this.buttonBackupDurchsuchen.Name = "buttonBackupDurchsuchen";
            this.buttonBackupDurchsuchen.Size = new System.Drawing.Size(139, 29);
            this.buttonBackupDurchsuchen.TabIndex = 12;
            this.buttonBackupDurchsuchen.Text = "Durchsuchen...";
            this.buttonBackupDurchsuchen.UseVisualStyleBackColor = true;
            this.buttonBackupDurchsuchen.Click += new System.EventHandler(this.buttonBackupDurchsuchen_Click);
            // 
            // button_startImport
            // 
            this.button_startImport.Location = new System.Drawing.Point(22, 423);
            this.button_startImport.Name = "button_startImport";
            this.button_startImport.Size = new System.Drawing.Size(96, 77);
            this.button_startImport.TabIndex = 17;
            this.button_startImport.Text = "Start Import";
            this.button_startImport.UseVisualStyleBackColor = true;
            this.button_startImport.Click += new System.EventHandler(this.button_startImport_Click);
            // 
            // button_stopImport
            // 
            this.button_stopImport.Location = new System.Drawing.Point(134, 423);
            this.button_stopImport.Name = "button_stopImport";
            this.button_stopImport.Size = new System.Drawing.Size(96, 77);
            this.button_stopImport.TabIndex = 18;
            this.button_stopImport.Text = "Stop Import";
            this.button_stopImport.UseVisualStyleBackColor = true;
            this.button_stopImport.Click += new System.EventHandler(this.button_stopImport_Click);
            // 
            // buttonIdexSpezifikationDurchsuchen
            // 
            this.buttonIdexSpezifikationDurchsuchen.Location = new System.Drawing.Point(402, 278);
            this.buttonIdexSpezifikationDurchsuchen.Name = "buttonIdexSpezifikationDurchsuchen";
            this.buttonIdexSpezifikationDurchsuchen.Size = new System.Drawing.Size(139, 29);
            this.buttonIdexSpezifikationDurchsuchen.TabIndex = 21;
            this.buttonIdexSpezifikationDurchsuchen.Text = "Durchsuchen...";
            this.buttonIdexSpezifikationDurchsuchen.UseVisualStyleBackColor = true;
            this.buttonIdexSpezifikationDurchsuchen.Click += new System.EventHandler(this.buttonIdexSpezifikationDurchsuchen_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 255);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 20);
            this.label4.TabIndex = 20;
            this.label4.Text = "Index-Spezifikation";
            // 
            // textBoxIndexSpezifikation
            // 
            this.textBoxIndexSpezifikation.Location = new System.Drawing.Point(17, 281);
            this.textBoxIndexSpezifikation.Name = "textBoxIndexSpezifikation";
            this.textBoxIndexSpezifikation.Size = new System.Drawing.Size(379, 26);
            this.textBoxIndexSpezifikation.TabIndex = 19;
            // 
            // buttonAccountConfigDurchsuchen
            // 
            this.buttonAccountConfigDurchsuchen.Location = new System.Drawing.Point(402, 345);
            this.buttonAccountConfigDurchsuchen.Name = "buttonAccountConfigDurchsuchen";
            this.buttonAccountConfigDurchsuchen.Size = new System.Drawing.Size(139, 29);
            this.buttonAccountConfigDurchsuchen.TabIndex = 24;
            this.buttonAccountConfigDurchsuchen.Text = "Durchsuchen...";
            this.buttonAccountConfigDurchsuchen.UseVisualStyleBackColor = true;
            this.buttonAccountConfigDurchsuchen.Click += new System.EventHandler(this.buttonAccountConfigDurchsuchen_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 322);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 20);
            this.label5.TabIndex = 23;
            this.label5.Text = "Account-Config";
            // 
            // textBoxAccountConfig
            // 
            this.textBoxAccountConfig.Location = new System.Drawing.Point(17, 348);
            this.textBoxAccountConfig.Name = "textBoxAccountConfig";
            this.textBoxAccountConfig.Size = new System.Drawing.Size(379, 26);
            this.textBoxAccountConfig.TabIndex = 22;
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Location = new System.Drawing.Point(568, 65);
            this.textBoxOutput.Multiline = true;
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.Size = new System.Drawing.Size(650, 435);
            this.textBoxOutput.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(568, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 20);
            this.label6.TabIndex = 26;
            this.label6.Text = "Ausgabe:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1259, 596);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxOutput);
            this.Controls.Add(this.buttonAccountConfigDurchsuchen);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxAccountConfig);
            this.Controls.Add(this.buttonIdexSpezifikationDurchsuchen);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxIndexSpezifikation);
            this.Controls.Add(this.button_stopImport);
            this.Controls.Add(this.button_startImport);
            this.Controls.Add(this.buttonBackupDurchsuchen);
            this.Controls.Add(this.buttonImportDurchsuchen);
            this.Controls.Add(this.buttonErrorDurchsuchen);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxBackup);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxImport);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxError);
            this.Name = "Form1";
            this.Text = "NotfallImporterUI";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxError;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxImport;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxBackup;
        private System.Windows.Forms.Button buttonErrorDurchsuchen;
        private System.Windows.Forms.Button buttonImportDurchsuchen;
        private System.Windows.Forms.Button buttonBackupDurchsuchen;
        private System.Windows.Forms.Button button_startImport;
        private System.Windows.Forms.Button button_stopImport;
        private System.Windows.Forms.Button buttonIdexSpezifikationDurchsuchen;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxIndexSpezifikation;
        private System.Windows.Forms.Button buttonAccountConfigDurchsuchen;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxAccountConfig;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.Label label6;
    }
}

