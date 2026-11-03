namespace PY.Presentacion.Forms
{
  partial class FormPartidas
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
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbAnio = new System.Windows.Forms.ComboBox();
            this.cmbTrimestre = new System.Windows.Forms.ComboBox();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.dgvPartidas = new System.Windows.Forms.DataGridView();
            this.colPartidaId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAnio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrimestre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMontoAsignado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMontoComprometido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMontoEjecutado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFechaAsignacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartidas)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(15, 71);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(100, 20);
            this.txtBuscar.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nombre";
            // 
            // cmbAnio
            // 
            this.cmbAnio.FormattingEnabled = true;
            this.cmbAnio.Location = new System.Drawing.Point(121, 70);
            this.cmbAnio.Name = "cmbAnio";
            this.cmbAnio.Size = new System.Drawing.Size(100, 21);
            this.cmbAnio.TabIndex = 2;
            // 
            // cmbTrimestre
            // 
            this.cmbTrimestre.FormattingEnabled = true;
            this.cmbTrimestre.Location = new System.Drawing.Point(333, 70);
            this.cmbTrimestre.Name = "cmbTrimestre";
            this.cmbTrimestre.Size = new System.Drawing.Size(100, 21);
            this.cmbTrimestre.TabIndex = 3;
            // 
            // cmbEstado
            // 
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Location = new System.Drawing.Point(227, 70);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(100, 21);
            this.cmbEstado.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(224, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Estado";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(330, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Trimestre";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(118, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Año";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(547, 42);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiar.TabIndex = 8;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(451, 68);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 9;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Location = new System.Drawing.Point(547, 71);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(75, 23);
            this.btnNuevo.TabIndex = 10;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // dgvPartidas
            // 
            this.dgvPartidas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPartidas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPartidaId,
            this.colAnio,
            this.colTrimestre,
            this.colMontoAsignado,
            this.colMontoComprometido,
            this.colMontoEjecutado,
            this.colFechaAsignacion,
            this.colActivo});
            this.dgvPartidas.Location = new System.Drawing.Point(12, 112);
            this.dgvPartidas.Name = "dgvPartidas";
            this.dgvPartidas.Size = new System.Drawing.Size(610, 221);
            this.dgvPartidas.TabIndex = 11;
            this.dgvPartidas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPartidas_CellDoubleClick);
            this.dgvPartidas.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvPartidas_CellFormatting);
            // 
            // colPartidaId
            // 
            this.colPartidaId.DataPropertyName = "PartidaPresupuestariaId";
            this.colPartidaId.HeaderText = "ID";
            this.colPartidaId.Name = "colPartidaId";
            this.colPartidaId.Visible = false;
            // 
            // colAnio
            // 
            this.colAnio.DataPropertyName = "Anio";
            this.colAnio.HeaderText = "Año";
            this.colAnio.Name = "colAnio";
            // 
            // colTrimestre
            // 
            this.colTrimestre.DataPropertyName = "Trimestre";
            this.colTrimestre.HeaderText = "Trimestre";
            this.colTrimestre.Name = "colTrimestre";
            // 
            // colMontoAsignado
            // 
            this.colMontoAsignado.DataPropertyName = "MontoAsignado";
            this.colMontoAsignado.HeaderText = "Monto Asignado";
            this.colMontoAsignado.Name = "colMontoAsignado";
            // 
            // colMontoComprometido
            // 
            this.colMontoComprometido.DataPropertyName = "MontoComprometido";
            this.colMontoComprometido.HeaderText = "Comprometido";
            this.colMontoComprometido.Name = "colMontoComprometido";
            // 
            // colMontoEjecutado
            // 
            this.colMontoEjecutado.DataPropertyName = "MontoEjecutado";
            this.colMontoEjecutado.HeaderText = "Ejecutado";
            this.colMontoEjecutado.Name = "colMontoEjecutado";
            // 
            // colFechaAsignacion
            // 
            this.colFechaAsignacion.DataPropertyName = "FechaAsignacion";
            this.colFechaAsignacion.HeaderText = "Fecha";
            this.colFechaAsignacion.Name = "colFechaAsignacion";
            // 
            // colActivo
            // 
            this.colActivo.DataPropertyName = "Activo";
            this.colActivo.HeaderText = "Estado";
            this.colActivo.Name = "colActivo";
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Location = new System.Drawing.Point(12, 96);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(49, 13);
            this.lblCantidad.TabIndex = 12;
            this.lblCantidad.Text = "Cantidad";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(460, 42);
            this.label5.TabIndex = 13;
            this.label5.Text = "Partidas Presupuestarias";
            // 
            // FormPartidas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 345);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblCantidad);
            this.Controls.Add(this.dgvPartidas);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbEstado);
            this.Controls.Add(this.cmbTrimestre);
            this.Controls.Add(this.cmbAnio);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBuscar);
            this.Name = "FormPartidas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Partidas Prespuestarias";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartidas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtBuscar;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cmbAnio;
    private System.Windows.Forms.ComboBox cmbTrimestre;
    private System.Windows.Forms.ComboBox cmbEstado;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Button btnLimpiar;
    private System.Windows.Forms.Button btnBuscar;
    private System.Windows.Forms.Button btnNuevo;
    private System.Windows.Forms.DataGridView dgvPartidas;
    private System.Windows.Forms.Label lblCantidad;
    private System.Windows.Forms.DataGridViewTextBoxColumn colPartidaId;
    private System.Windows.Forms.DataGridViewTextBoxColumn colAnio;
    private System.Windows.Forms.DataGridViewTextBoxColumn colTrimestre;
    private System.Windows.Forms.DataGridViewTextBoxColumn colMontoAsignado;
    private System.Windows.Forms.DataGridViewTextBoxColumn colMontoComprometido;
    private System.Windows.Forms.DataGridViewTextBoxColumn colMontoEjecutado;
    private System.Windows.Forms.DataGridViewTextBoxColumn colFechaAsignacion;
    private System.Windows.Forms.DataGridViewTextBoxColumn colActivo;
    private System.Windows.Forms.Label label5;
  }
}