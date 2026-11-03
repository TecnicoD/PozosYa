namespace PY.Presentacion.Forms
{
  partial class FormProveedores
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
            this.lblCantidad = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.dgvProveedores = new System.Windows.Forms.DataGridView();
            this.colProveedorId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCuit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTelefono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDireccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProveedores)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(15, 73);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(100, 20);
            this.txtBuscar.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nombre";
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Location = new System.Drawing.Point(12, 108);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(49, 13);
            this.lblCantidad.TabIndex = 4;
            this.lblCantidad.Text = "Cantidad";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(131, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Estado";
            // 
            // cmbEstado
            // 
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Location = new System.Drawing.Point(134, 72);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(121, 21);
            this.cmbEstado.TabIndex = 6;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnBuscar.Location = new System.Drawing.Point(286, 70);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 7;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnNuevo.Location = new System.Drawing.Point(547, 70);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(75, 23);
            this.btnNuevo.TabIndex = 8;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // dgvProveedores
            // 
            this.dgvProveedores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProveedores.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProveedorId,
            this.colNombre,
            this.colCuit,
            this.colTelefono,
            this.colMail,
            this.colDireccion,
            this.colActivo});
            this.dgvProveedores.Location = new System.Drawing.Point(12, 124);
            this.dgvProveedores.Name = "dgvProveedores";
            this.dgvProveedores.Size = new System.Drawing.Size(610, 207);
            this.dgvProveedores.TabIndex = 9;
            this.dgvProveedores.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProveedores_CellDoubleClick);
            this.dgvProveedores.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvProveedores_CellFormatting);
            // 
            // colProveedorId
            // 
            this.colProveedorId.DataPropertyName = "ProveedorId";
            this.colProveedorId.HeaderText = "ID";
            this.colProveedorId.Name = "colProveedorId";
            this.colProveedorId.Visible = false;
            // 
            // colNombre
            // 
            this.colNombre.DataPropertyName = "Nombre";
            this.colNombre.HeaderText = "Nombre";
            this.colNombre.Name = "colNombre";
            // 
            // colCuit
            // 
            this.colCuit.DataPropertyName = "Cuit";
            this.colCuit.HeaderText = "CUIT";
            this.colCuit.Name = "colCuit";
            // 
            // colTelefono
            // 
            this.colTelefono.DataPropertyName = "Telefono";
            this.colTelefono.HeaderText = "Teléfono";
            this.colTelefono.Name = "colTelefono";
            // 
            // colMail
            // 
            this.colMail.DataPropertyName = "Mail";
            this.colMail.HeaderText = "Mail";
            this.colMail.Name = "colMail";
            // 
            // colDireccion
            // 
            this.colDireccion.DataPropertyName = "Direccion";
            this.colDireccion.HeaderText = "Dirección";
            this.colDireccion.Name = "colDireccion";
            // 
            // colActivo
            // 
            this.colActivo.DataPropertyName = "Activo";
            this.colActivo.HeaderText = "Estado";
            this.colActivo.Name = "colActivo";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnLimpiar.Location = new System.Drawing.Point(374, 70);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiar.TabIndex = 10;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(242, 42);
            this.label2.TabIndex = 11;
            this.label2.Text = "Proveedores";
            // 
            // FormProveedores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 345);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.dgvProveedores);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.cmbEstado);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblCantidad);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBuscar);
            this.Name = "FormProveedores";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Proveedores";
            ((System.ComponentModel.ISupportInitialize)(this.dgvProveedores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtBuscar;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label lblCantidad;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.ComboBox cmbEstado;
    private System.Windows.Forms.Button btnBuscar;
    private System.Windows.Forms.Button btnNuevo;
    private System.Windows.Forms.DataGridView dgvProveedores;
    private System.Windows.Forms.DataGridViewTextBoxColumn colProveedorId;
    private System.Windows.Forms.DataGridViewTextBoxColumn colNombre;
    private System.Windows.Forms.DataGridViewTextBoxColumn colCuit;
    private System.Windows.Forms.DataGridViewTextBoxColumn colTelefono;
    private System.Windows.Forms.DataGridViewTextBoxColumn colMail;
    private System.Windows.Forms.DataGridViewTextBoxColumn colDireccion;
    private System.Windows.Forms.DataGridViewTextBoxColumn colActivo;
    private System.Windows.Forms.Button btnLimpiar;
    private System.Windows.Forms.Label label2;
  }
}
