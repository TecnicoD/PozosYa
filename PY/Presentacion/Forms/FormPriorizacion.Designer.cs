using System;
using System.Windows.Forms;

namespace PY.Presentacion.Forms
{
  partial class FormPriorizacion
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
            this.lblPartida = new System.Windows.Forms.Label();
            this.cmbPartida = new System.Windows.Forms.ComboBox();
            this.grpFondos = new System.Windows.Forms.GroupBox();
            this.lblMontoEjecutado = new System.Windows.Forms.Label();
            this.lblMontoAsignado = new System.Windows.Forms.Label();
            this.lblMontoComprometido = new System.Windows.Forms.Label();
            this.lblFondosDisponibles = new System.Windows.Forms.Label();
            this.lblPartidaSeleccionada = new System.Windows.Forms.Label();
            this.grpFiltros = new System.Windows.Forms.GroupBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.cmbLocalidad = new System.Windows.Forms.ComboBox();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.lblLocalidad = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.dgvReclamos = new System.Windows.Forms.DataGridView();
            this.colSeleccionar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colCosto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNumeroReclamo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDireccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBarrio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTotalSeleccionado = new System.Windows.Forms.Label();
            this.gpbOrden = new System.Windows.Forms.GroupBox();
            this.btnBajar = new System.Windows.Forms.Button();
            this.btnSubir = new System.Windows.Forms.Button();
            this.lstOrdenPrioridad = new System.Windows.Forms.ListBox();
            this.btnProcesar = new System.Windows.Forms.Button();
            this.grpFondos.SuspendLayout();
            this.grpFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReclamos)).BeginInit();
            this.gpbOrden.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(16, 7);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(225, 42);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Priorización";
            // 
            // lblPartida
            // 
            this.lblPartida.AutoSize = true;
            this.lblPartida.Location = new System.Drawing.Point(20, 55);
            this.lblPartida.Name = "lblPartida";
            this.lblPartida.Size = new System.Drawing.Size(40, 13);
            this.lblPartida.TabIndex = 1;
            this.lblPartida.Text = "Partida";
            // 
            // cmbPartida
            // 
            this.cmbPartida.DisplayMember = "PartidaPresupuestariaId";
            this.cmbPartida.FormattingEnabled = true;
            this.cmbPartida.Location = new System.Drawing.Point(66, 52);
            this.cmbPartida.Name = "cmbPartida";
            this.cmbPartida.Size = new System.Drawing.Size(157, 21);
            this.cmbPartida.TabIndex = 2;
            this.cmbPartida.ValueMember = "PartidaPresupuestariaId";
            this.cmbPartida.SelectedIndexChanged += new System.EventHandler(this.cmbPartida_SelectedIndexChanged);
            // 
            // grpFondos
            // 
            this.grpFondos.Controls.Add(this.lblMontoEjecutado);
            this.grpFondos.Controls.Add(this.lblMontoAsignado);
            this.grpFondos.Controls.Add(this.lblMontoComprometido);
            this.grpFondos.Controls.Add(this.lblFondosDisponibles);
            this.grpFondos.Controls.Add(this.lblPartidaSeleccionada);
            this.grpFondos.Location = new System.Drawing.Point(239, 45);
            this.grpFondos.Name = "grpFondos";
            this.grpFondos.Size = new System.Drawing.Size(182, 139);
            this.grpFondos.TabIndex = 3;
            this.grpFondos.TabStop = false;
            this.grpFondos.Text = "Fondos";
            // 
            // lblMontoEjecutado
            // 
            this.lblMontoEjecutado.AutoSize = true;
            this.lblMontoEjecutado.Location = new System.Drawing.Point(6, 91);
            this.lblMontoEjecutado.Name = "lblMontoEjecutado";
            this.lblMontoEjecutado.Size = new System.Drawing.Size(88, 13);
            this.lblMontoEjecutado.TabIndex = 8;
            this.lblMontoEjecutado.Text = "Monto Ejecutado";
            // 
            // lblMontoAsignado
            // 
            this.lblMontoAsignado.AutoSize = true;
            this.lblMontoAsignado.Location = new System.Drawing.Point(6, 43);
            this.lblMontoAsignado.Name = "lblMontoAsignado";
            this.lblMontoAsignado.Size = new System.Drawing.Size(84, 13);
            this.lblMontoAsignado.TabIndex = 7;
            this.lblMontoAsignado.Text = "Monto Asignado";
            // 
            // lblMontoComprometido
            // 
            this.lblMontoComprometido.AutoSize = true;
            this.lblMontoComprometido.Location = new System.Drawing.Point(6, 66);
            this.lblMontoComprometido.Name = "lblMontoComprometido";
            this.lblMontoComprometido.Size = new System.Drawing.Size(107, 13);
            this.lblMontoComprometido.TabIndex = 6;
            this.lblMontoComprometido.Text = "Monto Comprometido";
            // 
            // lblFondosDisponibles
            // 
            this.lblFondosDisponibles.AutoSize = true;
            this.lblFondosDisponibles.Location = new System.Drawing.Point(6, 115);
            this.lblFondosDisponibles.Name = "lblFondosDisponibles";
            this.lblFondosDisponibles.Size = new System.Drawing.Size(99, 13);
            this.lblFondosDisponibles.TabIndex = 5;
            this.lblFondosDisponibles.Text = "Fondos Disponibles";
            // 
            // lblPartidaSeleccionada
            // 
            this.lblPartidaSeleccionada.AutoSize = true;
            this.lblPartidaSeleccionada.Location = new System.Drawing.Point(6, 20);
            this.lblPartidaSeleccionada.Name = "lblPartidaSeleccionada";
            this.lblPartidaSeleccionada.Size = new System.Drawing.Size(108, 13);
            this.lblPartidaSeleccionada.TabIndex = 4;
            this.lblPartidaSeleccionada.Text = "Partida Seleccionada";
            // 
            // grpFiltros
            // 
            this.grpFiltros.Controls.Add(this.btnBuscar);
            this.grpFiltros.Controls.Add(this.btnLimpiar);
            this.grpFiltros.Controls.Add(this.cmbLocalidad);
            this.grpFiltros.Controls.Add(this.cmbEstado);
            this.grpFiltros.Controls.Add(this.lblLocalidad);
            this.grpFiltros.Controls.Add(this.lblEstado);
            this.grpFiltros.Location = new System.Drawing.Point(23, 78);
            this.grpFiltros.Name = "grpFiltros";
            this.grpFiltros.Size = new System.Drawing.Size(200, 104);
            this.grpFiltros.TabIndex = 9;
            this.grpFiltros.TabStop = false;
            this.grpFiltros.Text = "Filtros";
            this.grpFiltros.Enter += new System.EventHandler(this.grpFiltros_Enter);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnBuscar.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnBuscar.Location = new System.Drawing.Point(9, 77);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 13;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnLimpiar.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnLimpiar.Location = new System.Drawing.Point(99, 77);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiar.TabIndex = 12;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // cmbLocalidad
            // 
            this.cmbLocalidad.DisplayMember = "PartidaPresupuestariaId";
            this.cmbLocalidad.FormattingEnabled = true;
            this.cmbLocalidad.Location = new System.Drawing.Point(65, 50);
            this.cmbLocalidad.Name = "cmbLocalidad";
            this.cmbLocalidad.Size = new System.Drawing.Size(96, 21);
            this.cmbLocalidad.TabIndex = 11;
            this.cmbLocalidad.ValueMember = "PartidaPresupuestariaId";
            // 
            // cmbEstado
            // 
            this.cmbEstado.DisplayMember = "PartidaPresupuestariaId";
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Location = new System.Drawing.Point(65, 19);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(96, 21);
            this.cmbEstado.TabIndex = 10;
            this.cmbEstado.ValueMember = "PartidaPresupuestariaId";
            // 
            // lblLocalidad
            // 
            this.lblLocalidad.AutoSize = true;
            this.lblLocalidad.Location = new System.Drawing.Point(6, 53);
            this.lblLocalidad.Name = "lblLocalidad";
            this.lblLocalidad.Size = new System.Drawing.Size(53, 13);
            this.lblLocalidad.TabIndex = 6;
            this.lblLocalidad.Text = "Localidad";
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(6, 19);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(40, 13);
            this.lblEstado.TabIndex = 4;
            this.lblEstado.Text = "Estado";
            // 
            // dgvReclamos
            // 
            this.dgvReclamos.AllowUserToAddRows = false;
            this.dgvReclamos.AllowUserToDeleteRows = false;
            this.dgvReclamos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReclamos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSeleccionar,
            this.colCosto,
            this.colNumeroReclamo,
            this.colDireccion,
            this.colBarrio,
            this.colEstado});
            this.dgvReclamos.Location = new System.Drawing.Point(18, 190);
            this.dgvReclamos.MultiSelect = false;
            this.dgvReclamos.Name = "dgvReclamos";
            this.dgvReclamos.RowHeadersVisible = false;
            this.dgvReclamos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReclamos.Size = new System.Drawing.Size(604, 120);
            this.dgvReclamos.TabIndex = 10;
            this.dgvReclamos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReclamos_CellContentClick);
            // 
            // colSeleccionar
            // 
            this.colSeleccionar.HeaderText = "Seleccionar";
            this.colSeleccionar.Name = "colSeleccionar";
            // 
            // colCosto
            // 
            this.colCosto.DataPropertyName = "MontoPresupuesto";
            this.colCosto.HeaderText = "Costo";
            this.colCosto.Name = "colCosto";
            this.colCosto.ReadOnly = true;
            // 
            // colNumeroReclamo
            // 
            this.colNumeroReclamo.DataPropertyName = "NumeroReclamo";
            this.colNumeroReclamo.HeaderText = "N° Reclamo";
            this.colNumeroReclamo.Name = "colNumeroReclamo";
            this.colNumeroReclamo.ReadOnly = true;
            // 
            // colDireccion
            // 
            this.colDireccion.DataPropertyName = "Direccion";
            this.colDireccion.HeaderText = "Dirección";
            this.colDireccion.Name = "colDireccion";
            this.colDireccion.ReadOnly = true;
            // 
            // colBarrio
            // 
            this.colBarrio.DataPropertyName = "Barrio";
            this.colBarrio.HeaderText = "Barrio";
            this.colBarrio.Name = "colBarrio";
            this.colBarrio.ReadOnly = true;
            // 
            // colEstado
            // 
            this.colEstado.DataPropertyName = "Estado";
            this.colEstado.HeaderText = "Estado";
            this.colEstado.Name = "colEstado";
            this.colEstado.ReadOnly = true;
            // 
            // lblTotalSeleccionado
            // 
            this.lblTotalSeleccionado.AutoSize = true;
            this.lblTotalSeleccionado.Location = new System.Drawing.Point(20, 327);
            this.lblTotalSeleccionado.Name = "lblTotalSeleccionado";
            this.lblTotalSeleccionado.Size = new System.Drawing.Size(115, 13);
            this.lblTotalSeleccionado.TabIndex = 11;
            this.lblTotalSeleccionado.Text = "Total seleccionado: $0";
            // 
            // gpbOrden
            // 
            this.gpbOrden.Controls.Add(this.btnBajar);
            this.gpbOrden.Controls.Add(this.btnSubir);
            this.gpbOrden.Controls.Add(this.lstOrdenPrioridad);
            this.gpbOrden.Location = new System.Drawing.Point(427, 45);
            this.gpbOrden.Name = "gpbOrden";
            this.gpbOrden.Size = new System.Drawing.Size(195, 139);
            this.gpbOrden.TabIndex = 12;
            this.gpbOrden.TabStop = false;
            this.gpbOrden.Text = "Orden de priorización";
            // 
            // btnBajar
            // 
            this.btnBajar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnBajar.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnBajar.Location = new System.Drawing.Point(106, 110);
            this.btnBajar.Name = "btnBajar";
            this.btnBajar.Size = new System.Drawing.Size(75, 23);
            this.btnBajar.TabIndex = 2;
            this.btnBajar.Text = "↓ Bajar";
            this.btnBajar.UseVisualStyleBackColor = false;
            this.btnBajar.Click += new System.EventHandler(this.btnBajar_Click);
            // 
            // btnSubir
            // 
            this.btnSubir.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSubir.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnSubir.Location = new System.Drawing.Point(6, 110);
            this.btnSubir.Name = "btnSubir";
            this.btnSubir.Size = new System.Drawing.Size(75, 23);
            this.btnSubir.TabIndex = 1;
            this.btnSubir.Text = "↑ Subir";
            this.btnSubir.UseVisualStyleBackColor = false;
            this.btnSubir.Click += new System.EventHandler(this.btnSubir_Click);
            // 
            // lstOrdenPrioridad
            // 
            this.lstOrdenPrioridad.FormattingEnabled = true;
            this.lstOrdenPrioridad.IntegralHeight = false;
            this.lstOrdenPrioridad.Location = new System.Drawing.Point(7, 20);
            this.lstOrdenPrioridad.Name = "lstOrdenPrioridad";
            this.lstOrdenPrioridad.Size = new System.Drawing.Size(174, 84);
            this.lstOrdenPrioridad.TabIndex = 0;
            // 
            // btnProcesar
            // 
            this.btnProcesar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnProcesar.Location = new System.Drawing.Point(248, 322);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(131, 23);
            this.btnProcesar.TabIndex = 13;
            this.btnProcesar.Text = "Procesar priorización";
            this.btnProcesar.UseVisualStyleBackColor = false;
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // 
            // FormPriorizacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 345);
            this.Controls.Add(this.btnProcesar);
            this.Controls.Add(this.gpbOrden);
            this.Controls.Add(this.lblTotalSeleccionado);
            this.Controls.Add(this.dgvReclamos);
            this.Controls.Add(this.grpFiltros);
            this.Controls.Add(this.grpFondos);
            this.Controls.Add(this.cmbPartida);
            this.Controls.Add(this.lblPartida);
            this.Controls.Add(this.lblTitulo);
            this.Name = "FormPriorizacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Priorización";
            this.Load += new System.EventHandler(this.FormPriorizacion_Load);
            this.grpFondos.ResumeLayout(false);
            this.grpFondos.PerformLayout();
            this.grpFiltros.ResumeLayout(false);
            this.grpFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReclamos)).EndInit();
            this.gpbOrden.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblTitulo;
    private System.Windows.Forms.Label lblPartida;
    private System.Windows.Forms.ComboBox cmbPartida;
    private System.Windows.Forms.GroupBox grpFondos;
    private System.Windows.Forms.Label lblMontoEjecutado;
    private System.Windows.Forms.Label lblMontoAsignado;
    private System.Windows.Forms.Label lblMontoComprometido;
    private System.Windows.Forms.Label lblFondosDisponibles;
    private System.Windows.Forms.Label lblPartidaSeleccionada;
    private System.Windows.Forms.GroupBox grpFiltros;
    private System.Windows.Forms.Label lblLocalidad;
    private System.Windows.Forms.Label lblEstado;
    private System.Windows.Forms.ComboBox cmbEstado;
    private System.Windows.Forms.Button btnBuscar;
    private System.Windows.Forms.Button btnLimpiar;
    private System.Windows.Forms.ComboBox cmbLocalidad;
    private System.Windows.Forms.DataGridView dgvReclamos;
    private System.Windows.Forms.DataGridViewCheckBoxColumn colSeleccionar;
    private System.Windows.Forms.DataGridViewTextBoxColumn colCosto;
    private System.Windows.Forms.DataGridViewTextBoxColumn colNumeroReclamo;
    private System.Windows.Forms.DataGridViewTextBoxColumn colDireccion;
    private System.Windows.Forms.DataGridViewTextBoxColumn colBarrio;
    private System.Windows.Forms.DataGridViewTextBoxColumn colEstado;
    private System.Windows.Forms.Label lblTotalSeleccionado;
    private System.Windows.Forms.GroupBox gpbOrden;
    private System.Windows.Forms.Button btnBajar;
    private System.Windows.Forms.Button btnSubir;
    private System.Windows.Forms.ListBox lstOrdenPrioridad;
    private System.Windows.Forms.Button btnProcesar;


    private void grpFiltros_Enter(
    object sender,
    EventArgs e)
    {
    }
  }
}
