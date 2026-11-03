namespace PY.Presentacion.Forms
{
  partial class FormNuevoReclamo
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
            this.gpbVecino = new System.Windows.Forms.GroupBox();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNroCatastral = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtMail = new System.Windows.Forms.TextBox();
            this.lblDNI = new System.Windows.Forms.Label();
            this.txtDNI = new System.Windows.Forms.TextBox();
            this.gpbReclamo = new System.Windows.Forms.GroupBox();
            this.txtLongitud = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtLatitud = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbSeveridad = new System.Windows.Forms.ComboBox();
            this.cmbBarrio = new System.Windows.Forms.ComboBox();
            this.cmbTipoDanio = new System.Windows.Forms.ComboBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panelMapa = new System.Windows.Forms.Panel();
            this.mapa = new GMap.NET.WindowsForms.GMapControl();
            this.label11 = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.gpbVecino.SuspendLayout();
            this.gpbReclamo.SuspendLayout();
            this.panelMapa.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpbVecino
            // 
            this.gpbVecino.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gpbVecino.Controls.Add(this.btnConsultar);
            this.gpbVecino.Controls.Add(this.label5);
            this.gpbVecino.Controls.Add(this.label3);
            this.gpbVecino.Controls.Add(this.label2);
            this.gpbVecino.Controls.Add(this.txtNroCatastral);
            this.gpbVecino.Controls.Add(this.label4);
            this.gpbVecino.Controls.Add(this.txtApellido);
            this.gpbVecino.Controls.Add(this.txtTelefono);
            this.gpbVecino.Controls.Add(this.label1);
            this.gpbVecino.Controls.Add(this.txtNombre);
            this.gpbVecino.Controls.Add(this.txtMail);
            this.gpbVecino.Controls.Add(this.lblDNI);
            this.gpbVecino.Controls.Add(this.txtDNI);
            this.gpbVecino.Location = new System.Drawing.Point(32, 34);
            this.gpbVecino.Name = "gpbVecino";
            this.gpbVecino.Size = new System.Drawing.Size(321, 207);
            this.gpbVecino.TabIndex = 0;
            this.gpbVecino.TabStop = false;
            this.gpbVecino.Text = "Vecino";
            // 
            // btnConsultar
            // 
            this.btnConsultar.Location = new System.Drawing.Point(212, 21);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(94, 23);
            this.btnConsultar.TabIndex = 12;
            this.btnConsultar.Text = "Consultar DNI";
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "N° Catastral";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Teléfono";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Apellido";
            // 
            // txtNroCatastral
            // 
            this.txtNroCatastral.Location = new System.Drawing.Point(75, 154);
            this.txtNroCatastral.Name = "txtNroCatastral";
            this.txtNroCatastral.ReadOnly = true;
            this.txtNroCatastral.Size = new System.Drawing.Size(122, 20);
            this.txtNroCatastral.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Mail";
            // 
            // txtApellido
            // 
            this.txtApellido.Location = new System.Drawing.Point(75, 76);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.ReadOnly = true;
            this.txtApellido.Size = new System.Drawing.Size(122, 20);
            this.txtApellido.TabIndex = 4;
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(75, 102);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.ReadOnly = true;
            this.txtTelefono.Size = new System.Drawing.Size(122, 20);
            this.txtTelefono.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nombre";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(75, 50);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.ReadOnly = true;
            this.txtNombre.Size = new System.Drawing.Size(122, 20);
            this.txtNombre.TabIndex = 2;
            // 
            // txtMail
            // 
            this.txtMail.Location = new System.Drawing.Point(75, 128);
            this.txtMail.Name = "txtMail";
            this.txtMail.ReadOnly = true;
            this.txtMail.Size = new System.Drawing.Size(122, 20);
            this.txtMail.TabIndex = 8;
            // 
            // lblDNI
            // 
            this.lblDNI.AutoSize = true;
            this.lblDNI.Location = new System.Drawing.Point(6, 27);
            this.lblDNI.Name = "lblDNI";
            this.lblDNI.Size = new System.Drawing.Size(26, 13);
            this.lblDNI.TabIndex = 1;
            this.lblDNI.Text = "DNI";
            // 
            // txtDNI
            // 
            this.txtDNI.Location = new System.Drawing.Point(75, 24);
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.Size = new System.Drawing.Size(122, 20);
            this.txtDNI.TabIndex = 0;
            // 
            // gpbReclamo
            // 
            this.gpbReclamo.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gpbReclamo.Controls.Add(this.txtLongitud);
            this.gpbReclamo.Controls.Add(this.label14);
            this.gpbReclamo.Controls.Add(this.txtLatitud);
            this.gpbReclamo.Controls.Add(this.label13);
            this.gpbReclamo.Controls.Add(this.cmbSeveridad);
            this.gpbReclamo.Controls.Add(this.cmbBarrio);
            this.gpbReclamo.Controls.Add(this.cmbTipoDanio);
            this.gpbReclamo.Controls.Add(this.txtDescripcion);
            this.gpbReclamo.Controls.Add(this.txtDireccion);
            this.gpbReclamo.Controls.Add(this.label6);
            this.gpbReclamo.Controls.Add(this.label8);
            this.gpbReclamo.Controls.Add(this.label7);
            this.gpbReclamo.Controls.Add(this.label10);
            this.gpbReclamo.Controls.Add(this.label9);
            this.gpbReclamo.Location = new System.Drawing.Point(406, 34);
            this.gpbReclamo.Name = "gpbReclamo";
            this.gpbReclamo.Size = new System.Drawing.Size(346, 207);
            this.gpbReclamo.TabIndex = 1;
            this.gpbReclamo.TabStop = false;
            this.gpbReclamo.Text = "Reclamo";
            // 
            // txtLongitud
            // 
            this.txtLongitud.Location = new System.Drawing.Point(90, 180);
            this.txtLongitud.Name = "txtLongitud";
            this.txtLongitud.Size = new System.Drawing.Size(122, 20);
            this.txtLongitud.TabIndex = 24;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(9, 183);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(48, 13);
            this.label14.TabIndex = 23;
            this.label14.Text = "Longitud";
            // 
            // txtLatitud
            // 
            this.txtLatitud.Location = new System.Drawing.Point(90, 154);
            this.txtLatitud.Name = "txtLatitud";
            this.txtLatitud.Size = new System.Drawing.Size(122, 20);
            this.txtLatitud.TabIndex = 22;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 157);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(39, 13);
            this.label13.TabIndex = 21;
            this.label13.Text = "Latitud";
            // 
            // cmbSeveridad
            // 
            this.cmbSeveridad.FormattingEnabled = true;
            this.cmbSeveridad.Location = new System.Drawing.Point(90, 47);
            this.cmbSeveridad.Name = "cmbSeveridad";
            this.cmbSeveridad.Size = new System.Drawing.Size(121, 21);
            this.cmbSeveridad.TabIndex = 20;
            // 
            // cmbBarrio
            // 
            this.cmbBarrio.FormattingEnabled = true;
            this.cmbBarrio.Location = new System.Drawing.Point(90, 76);
            this.cmbBarrio.Name = "cmbBarrio";
            this.cmbBarrio.Size = new System.Drawing.Size(121, 21);
            this.cmbBarrio.TabIndex = 19;
            // 
            // cmbTipoDanio
            // 
            this.cmbTipoDanio.FormattingEnabled = true;
            this.cmbTipoDanio.Location = new System.Drawing.Point(91, 23);
            this.cmbTipoDanio.Name = "cmbTipoDanio";
            this.cmbTipoDanio.Size = new System.Drawing.Size(121, 21);
            this.cmbTipoDanio.TabIndex = 18;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(90, 128);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(122, 20);
            this.txtDescripcion.TabIndex = 14;
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(90, 102);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(122, 20);
            this.txtDireccion.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Dirección";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 131);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Descripción";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Barrio";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "Tipo de Daño";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 53);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Severidad";
            // 
            // panelMapa
            // 
            this.panelMapa.Controls.Add(this.mapa);
            this.panelMapa.Location = new System.Drawing.Point(32, 275);
            this.panelMapa.Name = "panelMapa";
            this.panelMapa.Size = new System.Drawing.Size(720, 126);
            this.panelMapa.TabIndex = 2;
            // 
            // mapa
            // 
            this.mapa.Bearing = 0F;
            this.mapa.CanDragMap = true;
            this.mapa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapa.EmptyTileColor = System.Drawing.Color.Navy;
            this.mapa.GrayScaleMode = false;
            this.mapa.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.mapa.LevelsKeepInMemmory = 5;
            this.mapa.Location = new System.Drawing.Point(0, 0);
            this.mapa.MarkersEnabled = true;
            this.mapa.MaxZoom = 2;
            this.mapa.MinZoom = 2;
            this.mapa.MouseWheelZoomEnabled = true;
            this.mapa.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.mapa.Name = "mapa";
            this.mapa.NegativeMode = false;
            this.mapa.PolygonsEnabled = true;
            this.mapa.RetryLoadTile = 0;
            this.mapa.RoutesEnabled = true;
            this.mapa.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.mapa.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.mapa.ShowTileGridLines = false;
            this.mapa.Size = new System.Drawing.Size(720, 126);
            this.mapa.TabIndex = 0;
            this.mapa.Zoom = 0D;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(27, 243);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 29);
            this.label11.TabIndex = 3;
            this.label11.Text = "Mapa";
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btnGuardar.Location = new System.Drawing.Point(231, 406);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(122, 34);
            this.btnGuardar.TabIndex = 4;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btnCancelar.Location = new System.Drawing.Point(406, 406);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(122, 34);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(27, 2);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(198, 29);
            this.label12.TabIndex = 6;
            this.label12.Text = "Nuevo Reclamo";
            // 
            // FormNuevoReclamo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.panelMapa);
            this.Controls.Add(this.gpbReclamo);
            this.Controls.Add(this.gpbVecino);
            this.Name = "FormNuevoReclamo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nuevo Reclamo";
            this.gpbVecino.ResumeLayout(false);
            this.gpbVecino.PerformLayout();
            this.gpbReclamo.ResumeLayout(false);
            this.gpbReclamo.PerformLayout();
            this.panelMapa.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.GroupBox gpbVecino;
    private System.Windows.Forms.TextBox txtDNI;
    private System.Windows.Forms.GroupBox gpbReclamo;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtApellido;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtNombre;
    private System.Windows.Forms.Label lblDNI;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtTelefono;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtMail;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtNroCatastral;
    private System.Windows.Forms.Button btnConsultar;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox txtDescripcion;
    private System.Windows.Forms.TextBox txtDireccion;
    private System.Windows.Forms.ComboBox cmbSeveridad;
    private System.Windows.Forms.ComboBox cmbBarrio;
    private System.Windows.Forms.ComboBox cmbTipoDanio;
    private System.Windows.Forms.Panel panelMapa;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Button btnGuardar;
    private System.Windows.Forms.Button btnCancelar;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.TextBox txtLongitud;
    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.TextBox txtLatitud;
    private System.Windows.Forms.Label label13;
    private System.ComponentModel.BackgroundWorker backgroundWorker1;
    private GMap.NET.WindowsForms.GMapControl mapa;
  }
}
