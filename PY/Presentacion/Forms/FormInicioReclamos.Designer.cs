namespace PY.Presentacion.Forms
{
  partial class FormInicioReclamos
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
            this.btnNuevoReclamo = new System.Windows.Forms.Button();
            this.btnExportarPdf = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.dgvReclamos = new System.Windows.Forms.DataGridView();
            this.colReclamoId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vecino = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ubicación = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Barrio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lblEstado = new System.Windows.Forms.Label();
            this.lblBarrio = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.cmbBarrio = new System.Windows.Forms.ComboBox();
            this.txtDNI = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReclamos)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 42);
            this.label1.TabIndex = 0;
            this.label1.Text = "Reclamos";
            // 
            // btnNuevoReclamo
            // 
            this.btnNuevoReclamo.Location = new System.Drawing.Point(523, 36);
            this.btnNuevoReclamo.Name = "btnNuevoReclamo";
            this.btnNuevoReclamo.Size = new System.Drawing.Size(99, 23);
            this.btnNuevoReclamo.TabIndex = 1;
            this.btnNuevoReclamo.Text = "Nuevo Reclamo";
            this.btnNuevoReclamo.UseVisualStyleBackColor = true;
            this.btnNuevoReclamo.Click += new System.EventHandler(this.btnNuevoReclamo_Click);
            //
            // btnExportarPdf
            //
            this.btnExportarPdf.Location = new System.Drawing.Point(418, 36);
            this.btnExportarPdf.Name = "btnExportarPdf";
            this.btnExportarPdf.Size = new System.Drawing.Size(99, 23);
            this.btnExportarPdf.TabIndex = 3;
            this.btnExportarPdf.Text = "Exportar PDF";
            this.btnExportarPdf.UseVisualStyleBackColor = true;
            this.btnExportarPdf.Click += new System.EventHandler(this.btnExportarPdf_Click);
            //
            // panel1
            // 
            this.panel1.Controls.Add(this.lblCantidad);
            this.panel1.Controls.Add(this.dgvReclamos);
            this.panel1.Controls.Add(this.btnLimpiar);
            this.panel1.Controls.Add(this.btnBuscar);
            this.panel1.Controls.Add(this.lblEstado);
            this.panel1.Controls.Add(this.lblBarrio);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dtpHasta);
            this.panel1.Controls.Add(this.dtpDesde);
            this.panel1.Controls.Add(this.cmbEstado);
            this.panel1.Controls.Add(this.cmbBarrio);
            this.panel1.Controls.Add(this.txtDNI);
            this.panel1.Location = new System.Drawing.Point(12, 65);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(610, 275);
            this.panel1.TabIndex = 2;
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Location = new System.Drawing.Point(15, 306);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(112, 13);
            this.lblCantidad.TabIndex = 13;
            this.lblCantidad.Text = "Mostrando X reclamos";
            // 
            // dgvReclamos
            // 
            this.dgvReclamos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReclamos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colReclamoId,
            this.ID,
            this.Fecha,
            this.Vecino,
            this.Ubicación,
            this.Barrio,
            this.Estado});
            this.dgvReclamos.Location = new System.Drawing.Point(15, 101);
            this.dgvReclamos.Name = "dgvReclamos";
            this.dgvReclamos.Size = new System.Drawing.Size(595, 171);
            this.dgvReclamos.TabIndex = 12;
            this.dgvReclamos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReclamos_CellContentClick);
            this.dgvReclamos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReclamos_CellDoubleClick);
            // 
            // colReclamoId
            // 
            this.colReclamoId.DataPropertyName = "ReclamoId";
            this.colReclamoId.HeaderText = "ID";
            this.colReclamoId.Name = "colReclamoId";
            this.colReclamoId.Visible = false;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "NumeroReclamo";
            this.ID.HeaderText = "Número";
            this.ID.Name = "ID";
            // 
            // Fecha
            // 
            this.Fecha.DataPropertyName = "FechaCreacion";
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.Name = "Fecha";
            // 
            // Vecino
            // 
            this.Vecino.DataPropertyName = "Vecino";
            this.Vecino.HeaderText = "Vecino";
            this.Vecino.Name = "Vecino";
            // 
            // Ubicación
            // 
            this.Ubicación.DataPropertyName = "Direccion";
            this.Ubicación.HeaderText = "Dirección";
            this.Ubicación.Name = "Ubicación";
            // 
            // Barrio
            // 
            this.Barrio.DataPropertyName = "Barrio";
            this.Barrio.HeaderText = "Barrio";
            this.Barrio.Name = "Barrio";
            // 
            // Estado
            // 
            this.Estado.DataPropertyName = "Estado";
            this.Estado.HeaderText = "Estado";
            this.Estado.Name = "Estado";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(492, 59);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiar.TabIndex = 11;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(492, 21);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 10;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(317, 9);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(40, 13);
            this.lblEstado.TabIndex = 9;
            this.lblEstado.Text = "Estado";
            // 
            // lblBarrio
            // 
            this.lblBarrio.AutoSize = true;
            this.lblBarrio.Location = new System.Drawing.Point(152, 7);
            this.lblBarrio.Name = "lblBarrio";
            this.lblBarrio.Size = new System.Drawing.Size(34, 13);
            this.lblBarrio.TabIndex = 8;
            this.lblBarrio.Text = "Barrio";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "DNI";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(241, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Hasta";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Desde";
            // 
            // dtpHasta
            // 
            this.dtpHasta.Location = new System.Drawing.Point(15, 75);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.ShowCheckBox = true;
            this.dtpHasta.Size = new System.Drawing.Size(200, 20);
            this.dtpHasta.TabIndex = 4;
            // 
            // dtpDesde
            // 
            this.dtpDesde.Location = new System.Drawing.Point(241, 75);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.ShowCheckBox = true;
            this.dtpDesde.Size = new System.Drawing.Size(200, 20);
            this.dtpDesde.TabIndex = 3;
            // 
            // cmbEstado
            // 
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Location = new System.Drawing.Point(320, 25);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(121, 21);
            this.cmbEstado.TabIndex = 2;
            // 
            // cmbBarrio
            // 
            this.cmbBarrio.FormattingEnabled = true;
            this.cmbBarrio.Location = new System.Drawing.Point(155, 23);
            this.cmbBarrio.Name = "cmbBarrio";
            this.cmbBarrio.Size = new System.Drawing.Size(121, 21);
            this.cmbBarrio.TabIndex = 1;
            // 
            // txtDNI
            // 
            this.txtDNI.Location = new System.Drawing.Point(15, 25);
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.Size = new System.Drawing.Size(100, 20);
            this.txtDNI.TabIndex = 0;
            // 
            // FormInicioReclamos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 345);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnExportarPdf);
            this.Controls.Add(this.btnNuevoReclamo);
            this.Controls.Add(this.label1);
            this.Name = "FormInicioReclamos";
            this.Text = "Reclamos";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReclamos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnNuevoReclamo;
    private System.Windows.Forms.Button btnExportarPdf;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.DateTimePicker dtpHasta;
    private System.Windows.Forms.DateTimePicker dtpDesde;
    private System.Windows.Forms.ComboBox cmbEstado;
    private System.Windows.Forms.ComboBox cmbBarrio;
    private System.Windows.Forms.TextBox txtDNI;
    private System.Windows.Forms.Button btnLimpiar;
    private System.Windows.Forms.Button btnBuscar;
    private System.Windows.Forms.Label lblEstado;
    private System.Windows.Forms.Label lblBarrio;
    private System.Windows.Forms.DataGridView dgvReclamos;
    private System.Windows.Forms.Label lblCantidad;
    private System.Windows.Forms.DataGridViewTextBoxColumn colReclamoId;
    private System.Windows.Forms.DataGridViewTextBoxColumn ID;
    private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
    private System.Windows.Forms.DataGridViewTextBoxColumn Vecino;
    private System.Windows.Forms.DataGridViewTextBoxColumn Ubicación;
    private System.Windows.Forms.DataGridViewTextBoxColumn Barrio;
    private System.Windows.Forms.DataGridViewTextBoxColumn Estado;
  }
}
