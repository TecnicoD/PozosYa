using Negocio.Security;
using Negocio.Services;
using System;
using System.Windows.Forms;
using PY.Presentacion.Idiomas;

namespace PY.Presentacion.Forms
{
  public partial class FormCambiarPassword : Form
  {

    private readonly UsuarioService usuarioService = new UsuarioService();

    private readonly int usuarioId;

    public FormCambiarPassword(int usuarioId)
    {
      InitializeComponent();
      Traductor.Traducir(this);

      this.usuarioId = usuarioId;
    }

    private void btnGuardar_Click(object sender, EventArgs e)
    {
      if (txtNuevaPassword.Text != txtConfirmarPassword.Text)
      {
        MessageBox.Show(
            Traductor.T("Las contraseñas no coinciden."),
            Traductor.T("Error"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);

        return;
      }

      if (string.IsNullOrWhiteSpace(txtNuevaPassword.Text))
      {
        MessageBox.Show(
            Traductor.T("Debe ingresar una contraseña."),
            Traductor.T("Error"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);

        return;
      }

      usuarioService.CambiarPassword(
          usuarioId,
          txtNuevaPassword.Text);

      new LogSistemaService().Registrar(
          SesionActual.Usuario.UsuarioId,
          "MODIFICAR",
          "USUARIOS",
          usuarioId,
          "Se cambió la contraseña del usuario.");

      MessageBox.Show(
        Traductor.T("Contraseña actualizada correctamente."),
        Traductor.T("Información"),
        MessageBoxButtons.OK,
        MessageBoxIcon.Information);

      DialogResult = DialogResult.OK;
      Close();
    }

    private void btnCancelar_Click(object sender, EventArgs e)
    {
      Close();
    }
  }
}
