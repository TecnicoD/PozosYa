namespace PY.Presentacion.Forms
{
  partial class FormLogs
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
            this.dgvLogs = new System.Windows.Forms.DataGridView();
            this.colLogId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTabla = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(320, 42);
            this.label1.TabIndex = 0;
            this.label1.Text = "Logs del Sistema";
            // 
            // dgvLogs
            // 
            this.dgvLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLogs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colLogId,
            this.colFecha,
            this.colUsuario,
            this.colAccion,
            this.colTabla,
            this.colRegistro,
            this.colDescripcion});
            this.dgvLogs.Location = new System.Drawing.Point(12, 57);
            this.dgvLogs.Name = "dgvLogs";
            this.dgvLogs.Size = new System.Drawing.Size(610, 276);
            this.dgvLogs.TabIndex = 1;
            // 
            // colLogId
            // 
            this.colLogId.DataPropertyName = "LogsSistemaId";
            this.colLogId.HeaderText = "ID";
            this.colLogId.Name = "colLogId";
            this.colLogId.Visible = false;
            // 
            // colFecha
            // 
            this.colFecha.DataPropertyName = "Fecha";
            this.colFecha.HeaderText = "Fecha";
            this.colFecha.Name = "colFecha";
            // 
            // colUsuario
            // 
            this.colUsuario.DataPropertyName = "Usuario";
            this.colUsuario.HeaderText = "Usuario";
            this.colUsuario.Name = "colUsuario";
            // 
            // colAccion
            // 
            this.colAccion.DataPropertyName = "Accion";
            this.colAccion.HeaderText = "Acción";
            this.colAccion.Name = "colAccion";
            // 
            // colTabla
            // 
            this.colTabla.DataPropertyName = "TablaAfectada";
            this.colTabla.HeaderText = "Tabla";
            this.colTabla.Name = "colTabla";
            // 
            // colRegistro
            // 
            this.colRegistro.DataPropertyName = "RegistroId";
            this.colRegistro.HeaderText = "Registro";
            this.colRegistro.Name = "colRegistro";
            this.colRegistro.Visible = false;
            // 
            // colDescripcion
            // 
            this.colDescripcion.DataPropertyName = "Descripcion";
            this.colDescripcion.HeaderText = "Descripción";
            this.colDescripcion.Name = "colDescripcion";
            // 
            // FormLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 345);
            this.Controls.Add(this.dgvLogs);
            this.Controls.Add(this.label1);
            this.Name = "FormLogs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Logs";
            this.Load += new System.EventHandler(this.FormLogs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.DataGridView dgvLogs;
    private System.Windows.Forms.DataGridViewTextBoxColumn colLogId;
    private System.Windows.Forms.DataGridViewTextBoxColumn colFecha;
    private System.Windows.Forms.DataGridViewTextBoxColumn colUsuario;
    private System.Windows.Forms.DataGridViewTextBoxColumn colAccion;
    private System.Windows.Forms.DataGridViewTextBoxColumn colTabla;
    private System.Windows.Forms.DataGridViewTextBoxColumn colRegistro;
    private System.Windows.Forms.DataGridViewTextBoxColumn colDescripcion;
  }
}