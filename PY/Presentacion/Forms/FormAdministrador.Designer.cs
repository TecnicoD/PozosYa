namespace PY.Presentacion.Forms
{
    partial class FormAdministrador
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
            this.lstBackups = new System.Windows.Forms.ListBox();
            this.btnRestaurar = new System.Windows.Forms.Button();
            this.btnBackUp = new System.Windows.Forms.Button();
            this.lblCarpetaBKP = new System.Windows.Forms.Label();
            this.txbCarpetaBkp = new System.Windows.Forms.TextBox();
            this.grbBackup = new System.Windows.Forms.GroupBox();
            this.btnVerificarIntegridad = new System.Windows.Forms.Button();
            this.grbBackup.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstBackups
            // 
            this.lstBackups.FormattingEnabled = true;
            this.lstBackups.Location = new System.Drawing.Point(6, 19);
            this.lstBackups.Name = "lstBackups";
            this.lstBackups.Size = new System.Drawing.Size(234, 134);
            this.lstBackups.TabIndex = 0;
            // 
            // btnRestaurar
            // 
            this.btnRestaurar.Location = new System.Drawing.Point(247, 82);
            this.btnRestaurar.Name = "btnRestaurar";
            this.btnRestaurar.Size = new System.Drawing.Size(117, 23);
            this.btnRestaurar.TabIndex = 1;
            this.btnRestaurar.Text = "Restaurar";
            this.btnRestaurar.UseVisualStyleBackColor = true;
            this.btnRestaurar.Click += new System.EventHandler(this.btnRestaurar_Click);
            // 
            // btnBackUp
            // 
            this.btnBackUp.Location = new System.Drawing.Point(247, 53);
            this.btnBackUp.Name = "btnBackUp";
            this.btnBackUp.Size = new System.Drawing.Size(117, 23);
            this.btnBackUp.TabIndex = 2;
            this.btnBackUp.Text = "Realizar Backup";
            this.btnBackUp.UseVisualStyleBackColor = true;
            this.btnBackUp.Click += new System.EventHandler(this.btnBackUp_Click);
            // 
            // lblCarpetaBKP
            // 
            this.lblCarpetaBKP.AutoSize = true;
            this.lblCarpetaBKP.Location = new System.Drawing.Point(6, 162);
            this.lblCarpetaBKP.Name = "lblCarpetaBKP";
            this.lblCarpetaBKP.Size = new System.Drawing.Size(107, 13);
            this.lblCarpetaBKP.TabIndex = 3;
            this.lblCarpetaBKP.Text = "Carpeta de BackUp: ";
            // 
            // txbCarpetaBkp
            // 
            this.txbCarpetaBkp.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txbCarpetaBkp.Enabled = false;
            this.txbCarpetaBkp.Location = new System.Drawing.Point(6, 178);
            this.txbCarpetaBkp.Name = "txbCarpetaBkp";
            this.txbCarpetaBkp.Size = new System.Drawing.Size(358, 20);
            this.txbCarpetaBkp.TabIndex = 5;
            // 
            // grbBackup
            // 
            this.grbBackup.Controls.Add(this.btnVerificarIntegridad);
            this.grbBackup.Controls.Add(this.txbCarpetaBkp);
            this.grbBackup.Controls.Add(this.lstBackups);
            this.grbBackup.Controls.Add(this.lblCarpetaBKP);
            this.grbBackup.Controls.Add(this.btnBackUp);
            this.grbBackup.Controls.Add(this.btnRestaurar);
            this.grbBackup.Location = new System.Drawing.Point(12, 12);
            this.grbBackup.Name = "grbBackup";
            this.grbBackup.Size = new System.Drawing.Size(374, 210);
            this.grbBackup.TabIndex = 4;
            this.grbBackup.TabStop = false;
            this.grbBackup.Text = "Administración de Respaldo e Integridad";
            // 
            // btnVerificarIntegridad
            // 
            this.btnVerificarIntegridad.Location = new System.Drawing.Point(247, 19);
            this.btnVerificarIntegridad.Name = "btnVerificarIntegridad";
            this.btnVerificarIntegridad.Size = new System.Drawing.Size(117, 23);
            this.btnVerificarIntegridad.TabIndex = 6;
            this.btnVerificarIntegridad.Text = "Verificar Integridad";
            this.btnVerificarIntegridad.UseVisualStyleBackColor = true;
            this.btnVerificarIntegridad.Click += new System.EventHandler(this.btnVerificarIntegridad_Click);
            // 
            // FormAdministrador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 229);
            this.Controls.Add(this.grbBackup);
            this.Name = "FormAdministrador";
            this.Text = "FormAdministrador";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grbBackup.ResumeLayout(false);
            this.grbBackup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstBackups;
        private System.Windows.Forms.Button btnRestaurar;
        private System.Windows.Forms.Button btnBackUp;
        private System.Windows.Forms.Label lblCarpetaBKP;
        private System.Windows.Forms.TextBox txbCarpetaBkp;
        private System.Windows.Forms.GroupBox grbBackup;
        private System.Windows.Forms.Button btnVerificarIntegridad;
    }
}