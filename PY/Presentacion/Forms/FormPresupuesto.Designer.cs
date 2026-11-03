namespace PY.Presentacion.Forms
{
  partial class FormPresupuesto
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
            this.lblNumeroReclamo = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.grpPresupuesto = new System.Windows.Forms.GroupBox();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.btnCancelarReclamo = new System.Windows.Forms.Button();
            this.lblObservaciones = new System.Windows.Forms.Label();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.btnSeleccionarArchivo = new System.Windows.Forms.Button();
            this.txtMontoFinal = new System.Windows.Forms.TextBox();
            this.txtMontoEstimado = new System.Windows.Forms.TextBox();
            this.cmbProveedor = new System.Windows.Forms.ComboBox();
            this.lblArchivoPdf = new System.Windows.Forms.Label();
            this.lblArchivoSeleccionado = new System.Windows.Forms.Label();
            this.lblMontoEstimado = new System.Windows.Forms.Label();
            this.lblMontoFinal = new System.Windows.Forms.Label();
            this.lblProveedor = new System.Windows.Forms.Label();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.grpPresupuesto.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(12, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(231, 31);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "PRESUPUESTO";
            // 
            // lblNumeroReclamo
            // 
            this.lblNumeroReclamo.AutoSize = true;
            this.lblNumeroReclamo.Location = new System.Drawing.Point(18, 51);
            this.lblNumeroReclamo.Name = "lblNumeroReclamo";
            this.lblNumeroReclamo.Size = new System.Drawing.Size(116, 13);
            this.lblNumeroReclamo.TabIndex = 1;
            this.lblNumeroReclamo.Text = "Detalle del reclamo #...";
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(19, 70);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(87, 13);
            this.lblEstado.TabIndex = 2;
            this.lblEstado.Text = "Estado actual: ...";
            this.lblEstado.Click += new System.EventHandler(this.lblEstado_Click);
            // 
            // grpPresupuesto
            // 
            this.grpPresupuesto.BackColor = System.Drawing.SystemColors.Window;
            this.grpPresupuesto.Controls.Add(this.txtObservaciones);
            this.grpPresupuesto.Controls.Add(this.btnCancelarReclamo);
            this.grpPresupuesto.Controls.Add(this.lblObservaciones);
            this.grpPresupuesto.Controls.Add(this.btnConfirmar);
            this.grpPresupuesto.Controls.Add(this.btnSeleccionarArchivo);
            this.grpPresupuesto.Controls.Add(this.txtMontoFinal);
            this.grpPresupuesto.Controls.Add(this.txtMontoEstimado);
            this.grpPresupuesto.Controls.Add(this.cmbProveedor);
            this.grpPresupuesto.Controls.Add(this.lblArchivoPdf);
            this.grpPresupuesto.Controls.Add(this.lblArchivoSeleccionado);
            this.grpPresupuesto.Controls.Add(this.lblMontoEstimado);
            this.grpPresupuesto.Controls.Add(this.lblMontoFinal);
            this.grpPresupuesto.Controls.Add(this.lblProveedor);
            this.grpPresupuesto.Location = new System.Drawing.Point(17, 90);
            this.grpPresupuesto.Name = "grpPresupuesto";
            this.grpPresupuesto.Size = new System.Drawing.Size(334, 303);
            this.grpPresupuesto.TabIndex = 12;
            this.grpPresupuesto.TabStop = false;
            this.grpPresupuesto.Text = "Presupuesto";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Location = new System.Drawing.Point(128, 185);
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtObservaciones.Size = new System.Drawing.Size(174, 51);
            this.txtObservaciones.TabIndex = 17;
            // 
            // btnCancelarReclamo
            // 
            this.btnCancelarReclamo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCancelarReclamo.Location = new System.Drawing.Point(193, 257);
            this.btnCancelarReclamo.Name = "btnCancelarReclamo";
            this.btnCancelarReclamo.Size = new System.Drawing.Size(109, 23);
            this.btnCancelarReclamo.TabIndex = 16;
            this.btnCancelarReclamo.Text = "Cancelar Reclamo";
            this.btnCancelarReclamo.UseVisualStyleBackColor = false;
            this.btnCancelarReclamo.Click += new System.EventHandler(this.btnCancelarReclamo_Click);
            // 
            // lblObservaciones
            // 
            this.lblObservaciones.AutoSize = true;
            this.lblObservaciones.Location = new System.Drawing.Point(24, 185);
            this.lblObservaciones.Name = "lblObservaciones";
            this.lblObservaciones.Size = new System.Drawing.Size(78, 13);
            this.lblObservaciones.TabIndex = 16;
            this.lblObservaciones.Text = "Observaciones";
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnConfirmar.Location = new System.Drawing.Point(19, 257);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(109, 23);
            this.btnConfirmar.TabIndex = 15;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // btnSeleccionarArchivo
            // 
            this.btnSeleccionarArchivo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSeleccionarArchivo.Location = new System.Drawing.Point(118, 116);
            this.btnSeleccionarArchivo.Name = "btnSeleccionarArchivo";
            this.btnSeleccionarArchivo.Size = new System.Drawing.Size(121, 20);
            this.btnSeleccionarArchivo.TabIndex = 13;
            this.btnSeleccionarArchivo.Text = "Seleccionar Archivo";
            this.btnSeleccionarArchivo.UseVisualStyleBackColor = false;
            this.btnSeleccionarArchivo.Click += new System.EventHandler(this.btnSeleccionarArchivo_Click);
            // 
            // txtMontoFinal
            // 
            this.txtMontoFinal.Location = new System.Drawing.Point(118, 81);
            this.txtMontoFinal.Name = "txtMontoFinal";
            this.txtMontoFinal.Size = new System.Drawing.Size(121, 20);
            this.txtMontoFinal.TabIndex = 15;
            // 
            // txtMontoEstimado
            // 
            this.txtMontoEstimado.Location = new System.Drawing.Point(118, 55);
            this.txtMontoEstimado.Name = "txtMontoEstimado";
            this.txtMontoEstimado.Size = new System.Drawing.Size(121, 20);
            this.txtMontoEstimado.TabIndex = 14;
            // 
            // cmbProveedor
            // 
            this.cmbProveedor.FormattingEnabled = true;
            this.cmbProveedor.Location = new System.Drawing.Point(118, 28);
            this.cmbProveedor.Name = "cmbProveedor";
            this.cmbProveedor.Size = new System.Drawing.Size(121, 21);
            this.cmbProveedor.TabIndex = 13;
            // 
            // lblArchivoPdf
            // 
            this.lblArchivoPdf.AutoSize = true;
            this.lblArchivoPdf.Location = new System.Drawing.Point(24, 116);
            this.lblArchivoPdf.Name = "lblArchivoPdf";
            this.lblArchivoPdf.Size = new System.Drawing.Size(93, 13);
            this.lblArchivoPdf.TabIndex = 10;
            this.lblArchivoPdf.Text = "Presupuesto PDF:";
            // 
            // lblArchivoSeleccionado
            // 
            this.lblArchivoSeleccionado.AutoSize = true;
            this.lblArchivoSeleccionado.Location = new System.Drawing.Point(24, 149);
            this.lblArchivoSeleccionado.Name = "lblArchivoSeleccionado";
            this.lblArchivoSeleccionado.Size = new System.Drawing.Size(117, 13);
            this.lblArchivoSeleccionado.TabIndex = 9;
            this.lblArchivoSeleccionado.Text = "Archivo Seleccionado: ";
            // 
            // lblMontoEstimado
            // 
            this.lblMontoEstimado.AutoSize = true;
            this.lblMontoEstimado.Location = new System.Drawing.Point(24, 55);
            this.lblMontoEstimado.Name = "lblMontoEstimado";
            this.lblMontoEstimado.Size = new System.Drawing.Size(83, 13);
            this.lblMontoEstimado.TabIndex = 8;
            this.lblMontoEstimado.Text = "Monto Estimado";
            // 
            // lblMontoFinal
            // 
            this.lblMontoFinal.AutoSize = true;
            this.lblMontoFinal.Location = new System.Drawing.Point(24, 88);
            this.lblMontoFinal.Name = "lblMontoFinal";
            this.lblMontoFinal.Size = new System.Drawing.Size(65, 13);
            this.lblMontoFinal.TabIndex = 7;
            this.lblMontoFinal.Text = "Monto Final:";
            // 
            // lblProveedor
            // 
            this.lblProveedor.AutoSize = true;
            this.lblProveedor.Location = new System.Drawing.Point(24, 28);
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.Size = new System.Drawing.Size(56, 13);
            this.lblProveedor.TabIndex = 4;
            this.lblProveedor.Text = "Proveedor";
            // 
            // btnModificar
            // 
            this.btnModificar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnModificar.Location = new System.Drawing.Point(17, 404);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(75, 23);
            this.btnModificar.TabIndex = 13;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = false;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCerrar.Location = new System.Drawing.Point(276, 404);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(75, 23);
            this.btnCerrar.TabIndex = 17;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnGuardar.Location = new System.Drawing.Point(98, 404);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 14;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Visible = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // FormPresupuesto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(372, 437);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.grpPresupuesto);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.lblNumeroReclamo);
            this.Controls.Add(this.lblTitulo);
            this.Name = "FormPresupuesto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detalle del reclamo #...";
            this.Load += new System.EventHandler(this.FormPresupuesto_Load);
            this.grpPresupuesto.ResumeLayout(false);
            this.grpPresupuesto.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblTitulo;
    private System.Windows.Forms.Label lblNumeroReclamo;
    private System.Windows.Forms.GroupBox grpPresupuesto;
    private System.Windows.Forms.TextBox txtMontoEstimado;
    private System.Windows.Forms.ComboBox cmbProveedor;
    private System.Windows.Forms.Label lblArchivoPdf;
    private System.Windows.Forms.Label lblArchivoSeleccionado;
    private System.Windows.Forms.Label lblMontoEstimado;
    private System.Windows.Forms.Label lblMontoFinal;
    private System.Windows.Forms.Label lblProveedor;
    private System.Windows.Forms.TextBox txtMontoFinal;
    private System.Windows.Forms.Button btnSeleccionarArchivo;
    private System.Windows.Forms.TextBox txtObservaciones;
    private System.Windows.Forms.Label lblObservaciones;
    private System.Windows.Forms.Button btnConfirmar;
    private System.Windows.Forms.Button btnModificar;
    private System.Windows.Forms.Button btnCancelarReclamo;
    private System.Windows.Forms.Button btnCerrar;
    private System.Windows.Forms.Label lblEstado;
    private System.Windows.Forms.Button btnGuardar;
  }
}
