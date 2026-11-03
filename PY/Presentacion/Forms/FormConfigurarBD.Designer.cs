namespace PY.Presentacion.Forms
{
  partial class FormConfigurarBD
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
            this.lblEstado = new System.Windows.Forms.Label();
            this.lblServidor = new System.Windows.Forms.Label();
            this.txtServidor = new System.Windows.Forms.TextBox();
            this.lblAyudaServidor = new System.Windows.Forms.Label();
            this.btnCrearDemo = new System.Windows.Forms.Button();
            this.lblAyudaDemo = new System.Windows.Forms.Label();
            this.btnRestaurar = new System.Windows.Forms.Button();
            this.lblAyudaRestaurar = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(18, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(257, 24);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Configurar base de datos";
            //
            // lblEstado
            //
            this.lblEstado.ForeColor = System.Drawing.Color.DarkRed;
            this.lblEstado.Location = new System.Drawing.Point(20, 48);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(440, 45);
            this.lblEstado.TabIndex = 1;
            this.lblEstado.Text = "No se encontró la base de datos.";
            //
            // lblServidor
            //
            this.lblServidor.AutoSize = true;
            this.lblServidor.Location = new System.Drawing.Point(20, 103);
            this.lblServidor.Name = "lblServidor";
            this.lblServidor.Size = new System.Drawing.Size(70, 13);
            this.lblServidor.TabIndex = 2;
            this.lblServidor.Text = "Servidor SQL";
            //
            // txtServidor
            //
            this.txtServidor.Location = new System.Drawing.Point(110, 100);
            this.txtServidor.Name = "txtServidor";
            this.txtServidor.Size = new System.Drawing.Size(200, 20);
            this.txtServidor.TabIndex = 3;
            //
            // lblAyudaServidor
            //
            this.lblAyudaServidor.AutoSize = true;
            this.lblAyudaServidor.ForeColor = System.Drawing.Color.DimGray;
            this.lblAyudaServidor.Location = new System.Drawing.Point(107, 124);
            this.lblAyudaServidor.Name = "lblAyudaServidor";
            this.lblAyudaServidor.Size = new System.Drawing.Size(189, 13);
            this.lblAyudaServidor.TabIndex = 4;
            this.lblAyudaServidor.Text = "Ejemplos: localhost, .\\SQLEXPRESS";
            //
            // btnCrearDemo
            //
            this.btnCrearDemo.Location = new System.Drawing.Point(23, 155);
            this.btnCrearDemo.Name = "btnCrearDemo";
            this.btnCrearDemo.Size = new System.Drawing.Size(160, 30);
            this.btnCrearDemo.TabIndex = 5;
            this.btnCrearDemo.Text = "Crear base de demo";
            this.btnCrearDemo.UseVisualStyleBackColor = true;
            this.btnCrearDemo.Click += new System.EventHandler(this.btnCrearDemo_Click);
            //
            // lblAyudaDemo
            //
            this.lblAyudaDemo.ForeColor = System.Drawing.Color.DimGray;
            this.lblAyudaDemo.Location = new System.Drawing.Point(195, 155);
            this.lblAyudaDemo.Name = "lblAyudaDemo";
            this.lblAyudaDemo.Size = new System.Drawing.Size(265, 30);
            this.lblAyudaDemo.TabIndex = 6;
            this.lblAyudaDemo.Text = "Crea la base con datos de ejemplo. Funciona en cualquier versión de SQL Server (2016 o superior).";
            //
            // btnRestaurar
            //
            this.btnRestaurar.Location = new System.Drawing.Point(23, 200);
            this.btnRestaurar.Name = "btnRestaurar";
            this.btnRestaurar.Size = new System.Drawing.Size(160, 30);
            this.btnRestaurar.TabIndex = 7;
            this.btnRestaurar.Text = "Restaurar desde .bak";
            this.btnRestaurar.UseVisualStyleBackColor = true;
            this.btnRestaurar.Click += new System.EventHandler(this.btnRestaurar_Click);
            //
            // lblAyudaRestaurar
            //
            this.lblAyudaRestaurar.ForeColor = System.Drawing.Color.DimGray;
            this.lblAyudaRestaurar.Location = new System.Drawing.Point(195, 200);
            this.lblAyudaRestaurar.Name = "lblAyudaRestaurar";
            this.lblAyudaRestaurar.Size = new System.Drawing.Size(265, 30);
            this.lblAyudaRestaurar.TabIndex = 8;
            this.lblAyudaRestaurar.Text = "Solo backups hechos en la misma versión de SQL Server o en una anterior.";
            //
            // btnSalir
            //
            this.btnSalir.Location = new System.Drawing.Point(385, 250);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 25);
            this.btnSalir.TabIndex = 9;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            //
            // FormConfigurarBD
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(480, 290);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.lblAyudaRestaurar);
            this.Controls.Add(this.btnRestaurar);
            this.Controls.Add(this.lblAyudaDemo);
            this.Controls.Add(this.btnCrearDemo);
            this.Controls.Add(this.lblAyudaServidor);
            this.Controls.Add(this.txtServidor);
            this.Controls.Add(this.lblServidor);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConfigurarBD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pozos Ya - Base de datos";
            this.Load += new System.EventHandler(this.FormConfigurarBD_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblTitulo;
    private System.Windows.Forms.Label lblEstado;
    private System.Windows.Forms.Label lblServidor;
    private System.Windows.Forms.TextBox txtServidor;
    private System.Windows.Forms.Label lblAyudaServidor;
    private System.Windows.Forms.Button btnCrearDemo;
    private System.Windows.Forms.Label lblAyudaDemo;
    private System.Windows.Forms.Button btnRestaurar;
    private System.Windows.Forms.Label lblAyudaRestaurar;
    private System.Windows.Forms.Button btnSalir;
  }
}
