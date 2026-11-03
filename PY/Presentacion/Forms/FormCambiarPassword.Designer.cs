namespace PY.Presentacion.Forms
{
  partial class FormCambiarPassword
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
            this.lblNuevaContraseña = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNuevaPassword = new System.Windows.Forms.TextBox();
            this.txtConfirmarPassword = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblNuevaContraseña
            // 
            this.lblNuevaContraseña.AutoSize = true;
            this.lblNuevaContraseña.Location = new System.Drawing.Point(42, 39);
            this.lblNuevaContraseña.Name = "lblNuevaContraseña";
            this.lblNuevaContraseña.Size = new System.Drawing.Size(96, 13);
            this.lblNuevaContraseña.TabIndex = 0;
            this.lblNuevaContraseña.Text = "Nueva Contraseña";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Confirmar Contraseña";
            // 
            // txtNuevaPassword
            // 
            this.txtNuevaPassword.Location = new System.Drawing.Point(45, 55);
            this.txtNuevaPassword.Name = "txtNuevaPassword";
            this.txtNuevaPassword.Size = new System.Drawing.Size(100, 20);
            this.txtNuevaPassword.TabIndex = 2;
            this.txtNuevaPassword.UseSystemPasswordChar = true;
            // 
            // txtConfirmarPassword
            // 
            this.txtConfirmarPassword.Location = new System.Drawing.Point(45, 107);
            this.txtConfirmarPassword.Name = "txtConfirmarPassword";
            this.txtConfirmarPassword.Size = new System.Drawing.Size(100, 20);
            this.txtConfirmarPassword.TabIndex = 3;
            this.txtConfirmarPassword.UseSystemPasswordChar = true;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.SystemColors.HighlightText;
            this.btnGuardar.Location = new System.Drawing.Point(54, 148);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 4;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.SystemColors.HighlightText;
            this.btnCancelar.Location = new System.Drawing.Point(54, 190);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FormCambiarPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(193, 258);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txtConfirmarPassword);
            this.Controls.Add(this.txtNuevaPassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblNuevaContraseña);
            this.Name = "FormCambiarPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cambiar Contraseña";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblNuevaContraseña;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtNuevaPassword;
    private System.Windows.Forms.TextBox txtConfirmarPassword;
    private System.Windows.Forms.Button btnGuardar;
    private System.Windows.Forms.Button btnCancelar;
  }
}
