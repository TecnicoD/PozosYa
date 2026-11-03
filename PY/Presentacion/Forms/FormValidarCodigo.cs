using Negocio.Models;
using Negocio.Services;
using System;
using System.Windows.Forms;
using PY.Presentacion.Idiomas;

namespace PY.Presentacion.Forms
{
  public partial class FormValidarCodigo : Form
  {
    private readonly string email;

    private readonly UsuarioService usuarioService =
        new UsuarioService();

    private readonly TokenRecuperacionService tokenService =
        new TokenRecuperacionService();


    // =========================================================
    // CONSTRUCTOR
    // =========================================================

    public FormValidarCodigo(string email)
    {
      InitializeComponent();
      Traductor.Traducir(this);

      this.email = email;
    }


    // =========================================================
    // RESTABLECER
    // =========================================================

    private void btnRestablecer_Click(
        object sender,
        EventArgs e)
    {
      string codigo =
          txtCodigo.Text.Trim();

      string nuevaPassword =
          txtNuevaPassword.Text;

      string confirmarPassword =
          txtConfirmarPassword.Text;


      // =======================================================
      // VALIDACIONES BÁSICAS
      // =======================================================

      if (string.IsNullOrWhiteSpace(codigo))
      {
        MessageBox.Show(
            Traductor.T("Debe ingresar el código de recuperación."),
            Traductor.T("Recuperación"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtCodigo.Focus();

        return;
      }

      if (string.IsNullOrWhiteSpace(nuevaPassword))
      {
        MessageBox.Show(
            Traductor.T("Debe ingresar una nueva contraseña."),
            Traductor.T("Recuperación"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtNuevaPassword.Focus();

        return;
      }

      if (string.IsNullOrWhiteSpace(confirmarPassword))
      {
        MessageBox.Show(
            Traductor.T("Debe confirmar la nueva contraseña."),
            Traductor.T("Recuperación"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtConfirmarPassword.Focus();

        return;
      }

      if (nuevaPassword != confirmarPassword)
      {
        MessageBox.Show(
            Traductor.T("Las contraseñas no coinciden."),
            Traductor.T("Recuperación"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtConfirmarPassword.Focus();

        return;
      }


      // =======================================================
      // BUSCAR USUARIO
      // =======================================================

      Usuario usuario =
          usuarioService.ObtenerPorEmail(email);

      if (usuario == null ||
          !usuario.Activo)
      {
        MessageBox.Show(
            Traductor.T("No se encontró un usuario activo asociado al correo."),
            Traductor.T("Recuperación"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);

        return;
      }


      // =======================================================
      // VALIDAR CÓDIGO
      // =======================================================

      bool codigoValido =
          tokenService.Validar(
              usuario.UsuarioId,
              codigo);

      if (!codigoValido)
      {
        MessageBox.Show(
            Traductor.T("El código es incorrecto, ya fue utilizado o está vencido."),
            Traductor.T("Recuperación"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtCodigo.Focus();

        return;
      }


      // =======================================================
      // CAMBIAR CONTRASEÑA
      // =======================================================

      usuarioService.CambiarPassword(
          usuario.UsuarioId,
          nuevaPassword);

      // No hay sesión iniciada: el log queda a nombre del propio usuario.
      new LogSistemaService().Registrar(
          usuario.UsuarioId,
          "MODIFICAR",
          "USUARIOS",
          usuario.UsuarioId,
          "El usuario restableció su contraseña con un código de recuperación.");


      // =======================================================
      // MARCAR TOKEN COMO USADO
      // =======================================================

      tokenService.MarcarUsado(
          usuario.UsuarioId,
          codigo);


      // =======================================================
      // FINALIZAR
      // =======================================================

      MessageBox.Show(
          Traductor.T("La contraseña se restableció correctamente."),
          Traductor.T("Recuperación"),
          MessageBoxButtons.OK,
          MessageBoxIcon.Information);

      Close();
    }


    // =========================================================
    // CANCELAR
    // =========================================================

    private void btnCancelar_Click(
        object sender,
        EventArgs e)
    {
      Close();
    }
  }
}
