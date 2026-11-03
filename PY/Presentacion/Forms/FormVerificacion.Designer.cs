namespace PY.Presentacion.Forms
{
  partial class FormVerificacion
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
            this.label1 = new System.Windows.Forms.Label();
            this.grpDatosReclamo = new System.Windows.Forms.GroupBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblSeveridad = new System.Windows.Forms.Label();
            this.lblTipoDanio = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblBarrio = new System.Windows.Forms.Label();
            this.lblDireccion = new System.Windows.Forms.Label();
            this.lblDni = new System.Windows.Forms.Label();
            this.lblVecino = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.grpAcciones = new System.Windows.Forms.GroupBox();
            this.lblArchivoSeleccionado = new System.Windows.Forms.Label();
            this.btnSeleccionarArchivo = new System.Windows.Forms.Button();
            this.btnCancelarReclamo = new System.Windows.Forms.Button();
            this.btnFinalizar = new System.Windows.Forms.Button();
            this.btnIniciarObra = new System.Windows.Forms.Button();
            this.btnVerificacion = new System.Windows.Forms.Button();
            this.lblNumeroReclamo = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.lblMensajeEstado = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.lstEvidencias = new System.Windows.Forms.ListBox();
            this.btnAbrirEvidencia = new System.Windows.Forms.Button();
            this.grpDatosReclamo.SuspendLayout();
            this.grpAcciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(222, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "VERIFICACIÓN";
            // 
            // grpDatosReclamo
            // 
            this.grpDatosReclamo.BackColor = System.Drawing.SystemColors.Menu;
            this.grpDatosReclamo.Controls.Add(this.txtDescripcion);
            this.grpDatosReclamo.Controls.Add(this.lblSeveridad);
            this.grpDatosReclamo.Controls.Add(this.lblTipoDanio);
            this.grpDatosReclamo.Controls.Add(this.lblDescripcion);
            this.grpDatosReclamo.Controls.Add(this.lblBarrio);
            this.grpDatosReclamo.Controls.Add(this.lblDireccion);
            this.grpDatosReclamo.Controls.Add(this.lblDni);
            this.grpDatosReclamo.Controls.Add(this.lblVecino);
            this.grpDatosReclamo.Controls.Add(this.lblFecha);
            this.grpDatosReclamo.Location = new System.Drawing.Point(17, 79);
            this.grpDatosReclamo.Name = "grpDatosReclamo";
            this.grpDatosReclamo.Size = new System.Drawing.Size(216, 284);
            this.grpDatosReclamo.TabIndex = 1;
            this.grpDatosReclamo.TabStop = false;
            this.grpDatosReclamo.Text = "Datos del reclamo";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(11, 202);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.ReadOnly = true;
            this.txtDescripcion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescripcion.Size = new System.Drawing.Size(177, 76);
            this.txtDescripcion.TabIndex = 14;
            // 
            // lblSeveridad
            // 
            this.lblSeveridad.AutoSize = true;
            this.lblSeveridad.Location = new System.Drawing.Point(8, 159);
            this.lblSeveridad.Name = "lblSeveridad";
            this.lblSeveridad.Size = new System.Drawing.Size(58, 13);
            this.lblSeveridad.TabIndex = 11;
            this.lblSeveridad.Text = "Severidad:";
            // 
            // lblTipoDanio
            // 
            this.lblTipoDanio.AutoSize = true;
            this.lblTipoDanio.Location = new System.Drawing.Point(8, 134);
            this.lblTipoDanio.Name = "lblTipoDanio";
            this.lblTipoDanio.Size = new System.Drawing.Size(75, 13);
            this.lblTipoDanio.TabIndex = 10;
            this.lblTipoDanio.Text = "Tipo de Daño:";
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new System.Drawing.Point(8, 183);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(63, 13);
            this.lblDescripcion.TabIndex = 12;
            this.lblDescripcion.Text = "Descripción";
            // 
            // lblBarrio
            // 
            this.lblBarrio.AutoSize = true;
            this.lblBarrio.Location = new System.Drawing.Point(8, 114);
            this.lblBarrio.Name = "lblBarrio";
            this.lblBarrio.Size = new System.Drawing.Size(40, 13);
            this.lblBarrio.TabIndex = 9;
            this.lblBarrio.Text = "Barrio: ";
            // 
            // lblDireccion
            // 
            this.lblDireccion.AutoSize = true;
            this.lblDireccion.Location = new System.Drawing.Point(8, 90);
            this.lblDireccion.Name = "lblDireccion";
            this.lblDireccion.Size = new System.Drawing.Size(58, 13);
            this.lblDireccion.TabIndex = 8;
            this.lblDireccion.Text = "Dirección: ";
            // 
            // lblDni
            // 
            this.lblDni.AutoSize = true;
            this.lblDni.Location = new System.Drawing.Point(8, 66);
            this.lblDni.Name = "lblDni";
            this.lblDni.Size = new System.Drawing.Size(29, 13);
            this.lblDni.TabIndex = 7;
            this.lblDni.Text = "DNI:";
            // 
            // lblVecino
            // 
            this.lblVecino.AutoSize = true;
            this.lblVecino.Location = new System.Drawing.Point(8, 42);
            this.lblVecino.Name = "lblVecino";
            this.lblVecino.Size = new System.Drawing.Size(46, 13);
            this.lblVecino.TabIndex = 6;
            this.lblVecino.Text = "Vecino: ";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(8, 18);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(43, 13);
            this.lblFecha.TabIndex = 5;
            this.lblFecha.Text = "Fecha: ";
            // 
            // grpAcciones
            // 
            this.grpAcciones.BackColor = System.Drawing.SystemColors.Menu;
            this.grpAcciones.Controls.Add(this.btnAbrirEvidencia);
            this.grpAcciones.Controls.Add(this.lstEvidencias);
            this.grpAcciones.Controls.Add(this.lblArchivoSeleccionado);
            this.grpAcciones.Controls.Add(this.btnSeleccionarArchivo);
            this.grpAcciones.Controls.Add(this.btnCancelarReclamo);
            this.grpAcciones.Controls.Add(this.btnFinalizar);
            this.grpAcciones.Controls.Add(this.btnIniciarObra);
            this.grpAcciones.Controls.Add(this.btnVerificacion);
            this.grpAcciones.Location = new System.Drawing.Point(254, 79);
            this.grpAcciones.Name = "grpAcciones";
            this.grpAcciones.Size = new System.Drawing.Size(216, 255);
            this.grpAcciones.TabIndex = 2;
            this.grpAcciones.TabStop = false;
            this.grpAcciones.Text = "Panel de acciones";
            // 
            // lblArchivoSeleccionado
            // 
            this.lblArchivoSeleccionado.AutoSize = true;
            this.lblArchivoSeleccionado.Location = new System.Drawing.Point(18, 66);
            this.lblArchivoSeleccionado.Name = "lblArchivoSeleccionado";
            this.lblArchivoSeleccionado.Size = new System.Drawing.Size(145, 13);
            this.lblArchivoSeleccionado.TabIndex = 14;
            this.lblArchivoSeleccionado.Text = "Ningún archivo seleccionado";
            // 
            // btnSeleccionarArchivo
            // 
            this.btnSeleccionarArchivo.BackColor = System.Drawing.SystemColors.HighlightText;
            this.btnSeleccionarArchivo.Location = new System.Drawing.Point(18, 42);
            this.btnSeleccionarArchivo.Name = "btnSeleccionarArchivo";
            this.btnSeleccionarArchivo.Size = new System.Drawing.Size(125, 23);
            this.btnSeleccionarArchivo.TabIndex = 5;
            this.btnSeleccionarArchivo.Text = "Seleccionar Archivo...";
            this.btnSeleccionarArchivo.UseVisualStyleBackColor = false;
            this.btnSeleccionarArchivo.Click += new System.EventHandler(this.btnSeleccionarArchivo_Click);
            // 
            // btnCancelarReclamo
            // 
            this.btnCancelarReclamo.BackColor = System.Drawing.SystemColors.HighlightText;
            this.btnCancelarReclamo.Location = new System.Drawing.Point(18, 124);
            this.btnCancelarReclamo.Name = "btnCancelarReclamo";
            this.btnCancelarReclamo.Size = new System.Drawing.Size(125, 23);
            this.btnCancelarReclamo.TabIndex = 3;
            this.btnCancelarReclamo.Text = "Cancelar Reclamo";
            this.btnCancelarReclamo.UseVisualStyleBackColor = false;
            this.btnCancelarReclamo.Click += new System.EventHandler(this.btnCancelarReclamo_Click);
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.BackColor = System.Drawing.SystemColors.HighlightText;
            this.btnFinalizar.Location = new System.Drawing.Point(18, 92);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(125, 23);
            this.btnFinalizar.TabIndex = 2;
            this.btnFinalizar.Text = "Finalizar Obra";
            this.btnFinalizar.UseVisualStyleBackColor = false;
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // btnIniciarObra
            // 
            this.btnIniciarObra.BackColor = System.Drawing.SystemColors.HighlightText;
            this.btnIniciarObra.Location = new System.Drawing.Point(18, 92);
            this.btnIniciarObra.Name = "btnIniciarObra";
            this.btnIniciarObra.Size = new System.Drawing.Size(125, 23);
            this.btnIniciarObra.TabIndex = 1;
            this.btnIniciarObra.Text = "Registrar inicio de obra";
            this.btnIniciarObra.UseVisualStyleBackColor = false;
            this.btnIniciarObra.Click += new System.EventHandler(this.btnIniciarObra_Click);
            // 
            // btnVerificacion
            // 
            this.btnVerificacion.BackColor = System.Drawing.SystemColors.HighlightText;
            this.btnVerificacion.Location = new System.Drawing.Point(18, 92);
            this.btnVerificacion.Name = "btnVerificacion";
            this.btnVerificacion.Size = new System.Drawing.Size(125, 23);
            this.btnVerificacion.TabIndex = 0;
            this.btnVerificacion.Text = "Verificar";
            this.btnVerificacion.UseVisualStyleBackColor = false;
            this.btnVerificacion.Click += new System.EventHandler(this.btnVerificacion_Click);
            // 
            // lblNumeroReclamo
            // 
            this.lblNumeroReclamo.AutoSize = true;
            this.lblNumeroReclamo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumeroReclamo.Location = new System.Drawing.Point(17, 53);
            this.lblNumeroReclamo.Name = "lblNumeroReclamo";
            this.lblNumeroReclamo.Size = new System.Drawing.Size(130, 16);
            this.lblNumeroReclamo.TabIndex = 0;
            this.lblNumeroReclamo.Text = "Detalle del reclamo: ";
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstado.Location = new System.Drawing.Point(251, 53);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(56, 16);
            this.lblEstado.TabIndex = 3;
            this.lblEstado.Text = "Estado: ";
            // 
            // lblMensajeEstado
            // 
            this.lblMensajeEstado.AutoSize = true;
            this.lblMensajeEstado.Location = new System.Drawing.Point(269, 258);
            this.lblMensajeEstado.Name = "lblMensajeEstado";
            this.lblMensajeEstado.Size = new System.Drawing.Size(0, 13);
            this.lblMensajeEstado.TabIndex = 13;
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.SystemColors.HighlightText;
            this.btnCerrar.Location = new System.Drawing.Point(394, 340);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(76, 23);
            this.btnCerrar.TabIndex = 4;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // lstEvidencias
            // 
            this.lstEvidencias.FormattingEnabled = true;
            this.lstEvidencias.Location = new System.Drawing.Point(18, 153);
            this.lstEvidencias.Name = "lstEvidencias";
            this.lstEvidencias.Size = new System.Drawing.Size(177, 69);
            this.lstEvidencias.TabIndex = 15;
            // 
            // btnAbrirEvidencia
            // 
            this.btnAbrirEvidencia.Location = new System.Drawing.Point(18, 226);
            this.btnAbrirEvidencia.Name = "btnAbrirEvidencia";
            this.btnAbrirEvidencia.Size = new System.Drawing.Size(88, 23);
            this.btnAbrirEvidencia.TabIndex = 16;
            this.btnAbrirEvidencia.Text = "Abrir Evidencia";
            this.btnAbrirEvidencia.UseVisualStyleBackColor = true;
            this.btnAbrirEvidencia.Click += new System.EventHandler(this.btnAbrirEvidencia_Click);
            // 
            // FormVerificacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(495, 379);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.lblMensajeEstado);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.lblNumeroReclamo);
            this.Controls.Add(this.grpAcciones);
            this.Controls.Add(this.grpDatosReclamo);
            this.Controls.Add(this.label1);
            this.Name = "FormVerificacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Verificación";
            this.Load += new System.EventHandler(this.FormVerificacion_Load);
            this.grpDatosReclamo.ResumeLayout(false);
            this.grpDatosReclamo.PerformLayout();
            this.grpAcciones.ResumeLayout(false);
            this.grpAcciones.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.GroupBox grpDatosReclamo;
    private System.Windows.Forms.Label lblBarrio;
    private System.Windows.Forms.Label lblDireccion;
    private System.Windows.Forms.Label lblDni;
    private System.Windows.Forms.Label lblVecino;
    private System.Windows.Forms.GroupBox grpAcciones;
    private System.Windows.Forms.Label lblNumeroReclamo;
    private System.Windows.Forms.Label lblEstado;
    private System.Windows.Forms.Label lblFecha;
    private System.Windows.Forms.TextBox txtDescripcion;
    private System.Windows.Forms.Label lblSeveridad;
    private System.Windows.Forms.Label lblTipoDanio;
    private System.Windows.Forms.Label lblDescripcion;
    private System.Windows.Forms.Label lblMensajeEstado;
    private System.Windows.Forms.Button btnSeleccionarArchivo;
    private System.Windows.Forms.Button btnCerrar;
    private System.Windows.Forms.Button btnCancelarReclamo;
    private System.Windows.Forms.Button btnFinalizar;
    private System.Windows.Forms.Button btnIniciarObra;
    private System.Windows.Forms.Button btnVerificacion;
    private System.Windows.Forms.Label lblArchivoSeleccionado;
    private System.Windows.Forms.ListBox lstEvidencias;
    private System.Windows.Forms.Button btnAbrirEvidencia;
  }
}
