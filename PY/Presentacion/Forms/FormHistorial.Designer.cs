namespace PY.Presentacion.Forms
{
  partial class FormHistorial
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
            this.label2 = new System.Windows.Forms.Label();
            this.lblNumeroReclamo = new System.Windows.Forms.Label();
            this.dgvHistorial = new System.Windows.Forms.DataGridView();
            this.colFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstadoAnterior = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstadoNuevo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFechaModificacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCerrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Historial del Reclamo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Reclamo:";
            // 
            // lblNumeroReclamo
            // 
            this.lblNumeroReclamo.AutoSize = true;
            this.lblNumeroReclamo.Location = new System.Drawing.Point(72, 57);
            this.lblNumeroReclamo.Name = "lblNumeroReclamo";
            this.lblNumeroReclamo.Size = new System.Drawing.Size(10, 13);
            this.lblNumeroReclamo.TabIndex = 2;
            this.lblNumeroReclamo.Text = "-";
            // 
            // dgvHistorial
            // 
            this.dgvHistorial.AllowUserToAddRows = false;
            this.dgvHistorial.AllowUserToDeleteRows = false;
            this.dgvHistorial.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistorial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistorial.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFecha,
            this.colEstadoAnterior,
            this.colEstadoNuevo,
            this.colUsuario,
            this.colFechaModificacion});
            this.dgvHistorial.Location = new System.Drawing.Point(17, 84);
            this.dgvHistorial.Name = "dgvHistorial";
            this.dgvHistorial.ReadOnly = true;
            this.dgvHistorial.RowHeadersVisible = false;
            this.dgvHistorial.Size = new System.Drawing.Size(451, 150);
            this.dgvHistorial.TabIndex = 3;
            // 
            // colFecha
            // 
            this.colFecha.HeaderText = "Fecha";
            this.colFecha.Name = "colFecha";
            // 
            // colEstadoAnterior
            // 
            this.colEstadoAnterior.HeaderText = "Estado Anterior";
            this.colEstadoAnterior.Name = "colEstadoAnterior";
            //
            // colEstadoNuevo
            //
            this.colEstadoNuevo.HeaderText = "Estado Nuevo";
            this.colEstadoNuevo.Name = "colEstadoNuevo";
            //
            // colUsuario
            // 
            this.colUsuario.HeaderText = "Usuario";
            this.colUsuario.Name = "colUsuario";
            // 
            // colFechaModificacion
            // 
            this.colFechaModificacion.HeaderText = "Fecha de Modificación";
            this.colFechaModificacion.Name = "colFechaModificacion";
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCerrar.Location = new System.Drawing.Point(393, 24);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(75, 23);
            this.btnCerrar.TabIndex = 4;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // FormHistorial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(495, 257);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.dgvHistorial);
            this.Controls.Add(this.lblNumeroReclamo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormHistorial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Historial";
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label lblNumeroReclamo;
    private System.Windows.Forms.DataGridView dgvHistorial;
    private System.Windows.Forms.Button btnCerrar;
    private System.Windows.Forms.DataGridViewTextBoxColumn colFecha;
    private System.Windows.Forms.DataGridViewTextBoxColumn colEstadoAnterior;
    private System.Windows.Forms.DataGridViewTextBoxColumn colEstadoNuevo;
    private System.Windows.Forms.DataGridViewTextBoxColumn colUsuario;
    private System.Windows.Forms.DataGridViewTextBoxColumn colFechaModificacion;
  }
}