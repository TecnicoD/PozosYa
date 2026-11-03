namespace PY.Presentacion.Forms
{
  partial class FormLogin
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
            this.btnIdiomas = new System.Windows.Forms.Button();
            this.cmsIdiomas = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuEspanol = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuIngles = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPortugues = new System.Windows.Forms.ToolStripMenuItem();
            this.btnIngresar = new System.Windows.Forms.Button();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnRecuperarContraseña = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.cmsIdiomas.SuspendLayout();
            this.SuspendLayout();
            //
            // btnIdiomas
            //
            this.btnIdiomas.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnIdiomas.Location = new System.Drawing.Point(297, 2);
            this.btnIdiomas.Name = "btnIdiomas";
            this.btnIdiomas.Size = new System.Drawing.Size(80, 22);
            this.btnIdiomas.TabIndex = 13;
            this.btnIdiomas.Text = "Idiomas";
            this.btnIdiomas.UseVisualStyleBackColor = false;
            this.btnIdiomas.Click += new System.EventHandler(this.btnIdiomas_Click);
            //
            // cmsIdiomas
            //
            this.cmsIdiomas.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEspanol,
            this.mnuIngles,
            this.mnuPortugues});
            this.cmsIdiomas.Name = "cmsIdiomas";
            this.cmsIdiomas.Size = new System.Drawing.Size(130, 70);
            //
            // mnuEspanol
            //
            this.mnuEspanol.Name = "mnuEspanol";
            this.mnuEspanol.Size = new System.Drawing.Size(129, 22);
            this.mnuEspanol.Text = "Español";
            this.mnuEspanol.Click += new System.EventHandler(this.mnuEspanol_Click);
            //
            // mnuIngles
            //
            this.mnuIngles.Name = "mnuIngles";
            this.mnuIngles.Size = new System.Drawing.Size(129, 22);
            this.mnuIngles.Text = "English";
            this.mnuIngles.Click += new System.EventHandler(this.mnuIngles_Click);
            //
            // mnuPortugues
            //
            this.mnuPortugues.Name = "mnuPortugues";
            this.mnuPortugues.Size = new System.Drawing.Size(129, 22);
            this.mnuPortugues.Text = "Português";
            this.mnuPortugues.Click += new System.EventHandler(this.mnuPortugues_Click);
            //
            // btnIngresar
            // 
            this.btnIngresar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnIngresar.Location = new System.Drawing.Point(146, 429);
            this.btnIngresar.Name = "btnIngresar";
            this.btnIngresar.Size = new System.Drawing.Size(75, 23);
            this.btnIngresar.TabIndex = 0;
            this.btnIngresar.Text = "Ingresar";
            this.btnIngresar.UseVisualStyleBackColor = false;
            this.btnIngresar.Click += new System.EventHandler(this.btnIngresar_Click);
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(133, 353);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(100, 20);
            this.txtUsuario.TabIndex = 0;
            this.txtUsuario.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUsuario_KeyDown);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(133, 401);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(100, 20);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(367, 26);
            this.label1.TabIndex = 5;
            this.label1.Text = "MUNICIPIO DE LOMAS DE ZAMORA";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(71, 301);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(247, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Portal Interno de Gestión Vial";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(129, 337);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Usuario";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(130, 385);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Contaseña";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 539);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(301, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Dirección de Sistemas | Lomas de Zamora | PP3 - ISFT N° 172";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(121, 472);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(131, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "¿Olvidaste tu contraseña?";
            // 
            // btnRecuperarContraseña
            // 
            this.btnRecuperarContraseña.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRecuperarContraseña.Location = new System.Drawing.Point(118, 489);
            this.btnRecuperarContraseña.Name = "btnRecuperarContraseña";
            this.btnRecuperarContraseña.Size = new System.Drawing.Size(139, 26);
            this.btnRecuperarContraseña.TabIndex = 11;
            this.btnRecuperarContraseña.Text = "Recuperar Contraseña";
            this.btnRecuperarContraseña.UseVisualStyleBackColor = false;
            this.btnRecuperarContraseña.Click += new System.EventHandler(this.btnRecuperarContraseña_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(85, 54);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(213, 231);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // FormLogin
            // 
            this.AcceptButton = this.btnIngresar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(384, 561);
            this.Controls.Add(this.btnIdiomas);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnRecuperarContraseña);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.btnIngresar);
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.cmsIdiomas.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnIngresar;
    private System.Windows.Forms.TextBox txtUsuario;
    private System.Windows.Forms.TextBox txtPassword;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Button btnRecuperarContraseña;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Button btnIdiomas;
    private System.Windows.Forms.ContextMenuStrip cmsIdiomas;
    private System.Windows.Forms.ToolStripMenuItem mnuEspanol;
    private System.Windows.Forms.ToolStripMenuItem mnuIngles;
    private System.Windows.Forms.ToolStripMenuItem mnuPortugues;
  }
}
