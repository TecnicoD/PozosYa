namespace PY.Presentacion.Forms
{
  partial class FormDetalleUsuario
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
            this.lblID = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ckbOperador = new System.Windows.Forms.CheckBox();
            this.ckbGerente = new System.Windows.Forms.CheckBox();
            this.ckbPriorizador = new System.Windows.Forms.CheckBox();
            this.ckbEstimador = new System.Windows.Forms.CheckBox();
            this.ckbVerificador = new System.Windows.Forms.CheckBox();
            this.ckbAdmin = new System.Windows.Forms.CheckBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtContraseña = new System.Windows.Forms.TextBox();
            this.btnCambiarContraseña = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnPermisosIndividuales = new System.Windows.Forms.Button();
            this.chkActivo = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(16, 18);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(343, 42);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Detalle de Usuario";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(23, 85);
            this.lblID.Name = "lblID";
            this.lblID.Visible = false;
            this.lblID.Size = new System.Drawing.Size(18, 13);
            this.lblID.TabIndex = 2;
            this.lblID.Text = "ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Nombre";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Apellido";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Username";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 275);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Contraseña";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 246);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Estado";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 212);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Email";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 25);
            this.label1.TabIndex = 9;
            this.label1.Text = "Roles Asignados";
            // 
            // ckbOperador
            // 
            this.ckbOperador.AutoSize = true;
            this.ckbOperador.Location = new System.Drawing.Point(11, 37);
            this.ckbOperador.Name = "ckbOperador";
            this.ckbOperador.Size = new System.Drawing.Size(162, 17);
            this.ckbOperador.TabIndex = 16;
            this.ckbOperador.Text = "Operador de Contact Center.";
            this.ckbOperador.UseVisualStyleBackColor = true;
            // 
            // ckbGerente
            // 
            this.ckbGerente.AutoSize = true;
            this.ckbGerente.Location = new System.Drawing.Point(11, 142);
            this.ckbGerente.Name = "ckbGerente";
            this.ckbGerente.Size = new System.Drawing.Size(64, 17);
            this.ckbGerente.TabIndex = 17;
            this.ckbGerente.Text = "Gerente";
            this.ckbGerente.UseVisualStyleBackColor = true;
            // 
            // ckbPriorizador
            // 
            this.ckbPriorizador.AutoSize = true;
            this.ckbPriorizador.Location = new System.Drawing.Point(11, 116);
            this.ckbPriorizador.Name = "ckbPriorizador";
            this.ckbPriorizador.Size = new System.Drawing.Size(75, 17);
            this.ckbPriorizador.TabIndex = 18;
            this.ckbPriorizador.Text = "Priorizador";
            this.ckbPriorizador.UseVisualStyleBackColor = true;
            // 
            // ckbEstimador
            // 
            this.ckbEstimador.AutoSize = true;
            this.ckbEstimador.Location = new System.Drawing.Point(11, 90);
            this.ckbEstimador.Name = "ckbEstimador";
            this.ckbEstimador.Size = new System.Drawing.Size(72, 17);
            this.ckbEstimador.TabIndex = 19;
            this.ckbEstimador.Text = "Estimador";
            this.ckbEstimador.UseVisualStyleBackColor = true;
            // 
            // ckbVerificador
            // 
            this.ckbVerificador.AutoSize = true;
            this.ckbVerificador.Location = new System.Drawing.Point(11, 66);
            this.ckbVerificador.Name = "ckbVerificador";
            this.ckbVerificador.Size = new System.Drawing.Size(76, 17);
            this.ckbVerificador.TabIndex = 20;
            this.ckbVerificador.Text = "Verificador";
            this.ckbVerificador.UseVisualStyleBackColor = true;
            // 
            // ckbAdmin
            // 
            this.ckbAdmin.AutoSize = true;
            this.ckbAdmin.Location = new System.Drawing.Point(11, 167);
            this.ckbAdmin.Name = "ckbAdmin";
            this.ckbAdmin.Size = new System.Drawing.Size(151, 17);
            this.ckbAdmin.TabIndex = 21;
            this.ckbAdmin.Text = "Administrador del Sistemas";
            this.ckbAdmin.UseVisualStyleBackColor = true;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(96, 77);
            this.txtID.Name = "txtID";
            this.txtID.Visible = false;
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(100, 20);
            this.txtID.TabIndex = 22;
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(96, 113);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(100, 20);
            this.txtNombre.TabIndex = 23;
            // 
            // txtApellido
            // 
            this.txtApellido.Location = new System.Drawing.Point(96, 150);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(100, 20);
            this.txtApellido.TabIndex = 25;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(96, 179);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(100, 20);
            this.txtUsername.TabIndex = 26;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(96, 212);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(100, 20);
            this.txtEmail.TabIndex = 27;
            // 
            // txtContraseña
            // 
            this.txtContraseña.Location = new System.Drawing.Point(96, 272);
            this.txtContraseña.Name = "txtContraseña";
            this.txtContraseña.Size = new System.Drawing.Size(100, 20);
            this.txtContraseña.TabIndex = 29;
            // 
            // btnCambiarContraseña
            // 
            this.btnCambiarContraseña.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCambiarContraseña.Location = new System.Drawing.Point(26, 311);
            this.btnCambiarContraseña.Name = "btnCambiarContraseña";
            this.btnCambiarContraseña.Size = new System.Drawing.Size(140, 23);
            this.btnCambiarContraseña.TabIndex = 30;
            this.btnCambiarContraseña.Text = "Cambiar Contraseña";
            this.btnCambiarContraseña.UseVisualStyleBackColor = false;
            this.btnCambiarContraseña.Click += new System.EventHandler(this.btnCambiarContraseña_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnModificar.Location = new System.Drawing.Point(23, 380);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(85, 23);
            this.btnModificar.TabIndex = 31;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = false;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnGuardar.Location = new System.Drawing.Point(124, 380);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(85, 23);
            this.btnGuardar.TabIndex = 32;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCancelar.Location = new System.Drawing.Point(226, 380);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(85, 23);
            this.btnCancelar.TabIndex = 33;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnPermisosIndividuales
            // 
            this.btnPermisosIndividuales.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnPermisosIndividuales.Location = new System.Drawing.Point(325, 358);
            this.btnPermisosIndividuales.Name = "btnPermisosIndividuales";
            this.btnPermisosIndividuales.Size = new System.Drawing.Size(125, 45);
            this.btnPermisosIndividuales.TabIndex = 34;
            this.btnPermisosIndividuales.Text = "Administrar Permisos Individuales";
            this.btnPermisosIndividuales.UseVisualStyleBackColor = false;
            this.btnPermisosIndividuales.Click += new System.EventHandler(this.btnPermisosIndividuales_Click);
            // 
            // chkActivo
            // 
            this.chkActivo.AutoSize = true;
            this.chkActivo.Location = new System.Drawing.Point(96, 249);
            this.chkActivo.Name = "chkActivo";
            this.chkActivo.Size = new System.Drawing.Size(15, 14);
            this.chkActivo.TabIndex = 35;
            this.chkActivo.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox1.Controls.Add(this.ckbEstimador);
            this.groupBox1.Controls.Add(this.ckbOperador);
            this.groupBox1.Controls.Add(this.ckbGerente);
            this.groupBox1.Controls.Add(this.ckbPriorizador);
            this.groupBox1.Controls.Add(this.ckbVerificador);
            this.groupBox1.Controls.Add(this.ckbAdmin);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(250, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 201);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            // 
            // FormDetalleUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(472, 419);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkActivo);
            this.Controls.Add(this.btnPermisosIndividuales);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.btnCambiarContraseña);
            this.Controls.Add(this.txtContraseña);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtApellido);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.lblTitulo);
            this.Name = "FormDetalleUsuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detalle del Usuario";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblTitulo;
    private System.Windows.Forms.Label lblID;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.CheckBox ckbOperador;
    private System.Windows.Forms.CheckBox ckbGerente;
    private System.Windows.Forms.CheckBox ckbPriorizador;
    private System.Windows.Forms.CheckBox ckbEstimador;
    private System.Windows.Forms.CheckBox ckbVerificador;
    private System.Windows.Forms.CheckBox ckbAdmin;
    private System.Windows.Forms.TextBox txtID;
    private System.Windows.Forms.TextBox txtNombre;
    private System.Windows.Forms.TextBox txtApellido;
    private System.Windows.Forms.TextBox txtUsername;
    private System.Windows.Forms.TextBox txtEmail;
    private System.Windows.Forms.TextBox txtContraseña;
    private System.Windows.Forms.Button btnCambiarContraseña;
    private System.Windows.Forms.Button btnModificar;
    private System.Windows.Forms.Button btnGuardar;
    private System.Windows.Forms.Button btnCancelar;
    private System.Windows.Forms.Button btnPermisosIndividuales;
    private System.Windows.Forms.CheckBox chkActivo;
    private System.Windows.Forms.GroupBox groupBox1;
  }
}
