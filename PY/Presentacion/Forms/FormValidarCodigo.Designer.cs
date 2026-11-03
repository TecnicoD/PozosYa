namespace PY.Presentacion.Forms
{
  partial class FormValidarCodigo
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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.txtNuevaPassword = new System.Windows.Forms.TextBox();
            this.txtConfirmarPassword = new System.Windows.Forms.TextBox();
            this.btnRestablecer = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(37, 18);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(266, 25);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Restablecer Contraseña";
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Location = new System.Drawing.Point(55, 59);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(40, 13);
            this.lblCodigo.TabIndex = 1;
            this.lblCodigo.Text = "Código";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nueva contraseña";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Confirmar contraseña";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(175, 56);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(100, 20);
            this.txtCodigo.TabIndex = 4;
            // 
            // txtNuevaPassword
            // 
            this.txtNuevaPassword.Location = new System.Drawing.Point(175, 82);
            this.txtNuevaPassword.Name = "txtNuevaPassword";
            this.txtNuevaPassword.PasswordChar = '*';
            this.txtNuevaPassword.Size = new System.Drawing.Size(100, 20);
            this.txtNuevaPassword.TabIndex = 5;
            // 
            // txtConfirmarPassword
            // 
            this.txtConfirmarPassword.Location = new System.Drawing.Point(175, 112);
            this.txtConfirmarPassword.Name = "txtConfirmarPassword";
            this.txtConfirmarPassword.PasswordChar = '*';
            this.txtConfirmarPassword.Size = new System.Drawing.Size(100, 20);
            this.txtConfirmarPassword.TabIndex = 6;
            // 
            // btnRestablecer
            // 
            this.btnRestablecer.BackColor = System.Drawing.SystemColors.HighlightText;
            this.btnRestablecer.Location = new System.Drawing.Point(87, 148);
            this.btnRestablecer.Name = "btnRestablecer";
            this.btnRestablecer.Size = new System.Drawing.Size(75, 23);
            this.btnRestablecer.TabIndex = 7;
            this.btnRestablecer.Text = "Restablecer";
            this.btnRestablecer.UseVisualStyleBackColor = false;
            this.btnRestablecer.Click += new System.EventHandler(this.btnRestablecer_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.SystemColors.HighlightText;
            this.btnCancelar.Location = new System.Drawing.Point(175, 148);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FormValidarCodigo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 183);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnRestablecer);
            this.Controls.Add(this.txtConfirmarPassword);
            this.Controls.Add(this.txtNuevaPassword);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCodigo);
            this.Controls.Add(this.lblTitulo);
            this.Name = "FormValidarCodigo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Validar Código";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblTitulo;
    private System.Windows.Forms.Label lblCodigo;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtCodigo;
    private System.Windows.Forms.TextBox txtNuevaPassword;
    private System.Windows.Forms.TextBox txtConfirmarPassword;
    private System.Windows.Forms.Button btnRestablecer;
    private System.Windows.Forms.Button btnCancelar;
  }
}
