using Negocio.Services;
using System;
using System.Windows.Forms;
using PY.Presentacion.Idiomas;

namespace PY.Presentacion.Forms
{
  public partial class FormRecuperarContraseña : Form
  {
    private readonly RecuperacionService recuperacionService =
        new RecuperacionService();

    public FormRecuperarContraseña()
    {
      InitializeComponent();
      Traductor.Traducir(this);
    }

    private void FormRecuperarContraseña_Load(
        object sender,
        EventArgs e)
    {
      txtEmail.Focus();
    }

    private void btnEnviarCodigo_Click(
        object sender,
        EventArgs e)
    {
      string email =
          txtEmail.Text.Trim();

      if (string.IsNullOrWhiteSpace(email))
      {
        MessageBox.Show(
            Traductor.T("Debe ingresar su correo electrónico."),
            Traductor.T("Recuperación"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtEmail.Focus();
        return;
      }

      try
      {
        bool enviado =
            recuperacionService.SolicitarRecuperacion(
                email);

        if (!enviado)
        {
          MessageBox.Show(
              Traductor.T("No existe un usuario activo asociado a ese correo."),
              Traductor.T("Recuperación"),
              MessageBoxButtons.OK,
              MessageBoxIcon.Warning);

          return;
        }

        MessageBox.Show(
            Traductor.T("Se envió un código de recuperación a su correo. ") +
            Traductor.T("El código tiene una vigencia de 10 minutos."),
            Traductor.T("Recuperación"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);

        FormValidarCodigo formulario =
            new FormValidarCodigo(email);

        Hide();

        formulario.ShowDialog();

        Close();
      }
      catch (Exception ex)
      {
        MessageBox.Show(
            Traductor.T("No se pudo enviar el código.\n\n") +
            Traductor.T(ex.Message),
            Traductor.T("Error"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
      }
    }

    private void btnCancelar_Click(
        object sender,
        EventArgs e)
    {
      Close();
    }
  }
}
