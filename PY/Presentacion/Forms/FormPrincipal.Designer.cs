namespace PY.Presentacion.Forms
{
  partial class FormPrincipal
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
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.panelContenido = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.LightSkyBlue;
            this.panelSuperior.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(800, 66);
            this.panelSuperior.TabIndex = 0;
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.LightSkyBlue;
            this.panelSidebar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 66);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(150, 395);
            this.panelSidebar.TabIndex = 1;
            // 
            // panelContenido
            // 
            this.panelContenido.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(150, 66);
            this.panelContenido.Margin = new System.Windows.Forms.Padding(0);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Size = new System.Drawing.Size(650, 395);
            this.panelContenido.TabIndex = 2;
            this.panelContenido.Paint += new System.Windows.Forms.PaintEventHandler(this.panelContenido_Paint);
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 461);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.panelSidebar);
            this.Controls.Add(this.panelSuperior);
            this.Name = "FormPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Principal";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPrincipal_FormClosing);
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panelSuperior;
    private System.Windows.Forms.Panel panelSidebar;
    private System.Windows.Forms.Panel panelContenido;
  }
}
