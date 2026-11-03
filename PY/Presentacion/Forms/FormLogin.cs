using System;
using System.Windows.Forms;
using Negocio.Services;
using Negocio.Models;
using PY.Presentacion.Idiomas;

namespace PY.Presentacion.Forms
{
  public partial class FormLogin : Form
  {
    // =========================
    // CONSTRUCTOR
    // =========================

    public FormLogin()
    {
      InitializeComponent();
      Traductor.Traducir(this);
    }


    // =========================
    // EVENTOS
    // =========================

    private void btnIngresar_Click(object sender, EventArgs e)
    {
      SeguridadService seguridadService = new SeguridadService();

      bool ingreso = seguridadService.Login(
          txtUsuario.Text,
          txtPassword.Text
      );

      if (!ingreso)
      {
        MessageBox.Show(
            Traductor.T("Usuario o contraseña incorrectos."),
            Traductor.T("Error"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        );

        return;
      }

      Hide();

      FormPrincipal principal = new FormPrincipal();

      DialogResult resultado = principal.ShowDialog();

      // Si el usuario cerró sesión, volvemos a mostrar el login
      // para permitir el ingreso de otro usuario.
      if (resultado == DialogResult.Retry)
      {
        txtUsuario.Clear();
        txtPassword.Clear();

        Show();
        txtUsuario.Focus();

        return;
      }

      Close();
    }

    private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Right)
        return;

      // Evita que la tecla derecha mueva el cursor dentro del TextBox.
      e.SuppressKeyPress = true;

      txtPassword.Focus();
    }

    private void btnRecuperarContraseña_Click(
    object sender,
    EventArgs e)
    {
      FormRecuperarContraseña formulario =
          new FormRecuperarContraseña();

      formulario.ShowDialog();
    }


    // =========================
    // IDIOMAS
    // =========================

    private void btnIdiomas_Click(object sender, EventArgs e)
    {
      mnuEspanol.Checked = Traductor.IdiomaActual == Idioma.Espanol;
      mnuIngles.Checked = Traductor.IdiomaActual == Idioma.Ingles;
      mnuPortugues.Checked = Traductor.IdiomaActual == Idioma.Portugues;

      // El menú se despliega debajo del botón.
      cmsIdiomas.Show(btnIdiomas, 0, btnIdiomas.Height);
    }

    private void mnuEspanol_Click(object sender, EventArgs e)
    {
      CambiarIdioma(Idioma.Espanol);
    }

    private void mnuIngles_Click(object sender, EventArgs e)
    {
      CambiarIdioma(Idioma.Ingles);
    }

    private void mnuPortugues_Click(object sender, EventArgs e)
    {
      CambiarIdioma(Idioma.Portugues);
    }

    // El idioma elegido queda para toda la sesión:
    // las pantallas que se abran después ya salen traducidas.
    private void CambiarIdioma(Idioma idioma)
    {
      Traductor.IdiomaActual = idioma;

      Traductor.Retraducir(this);
    }
  }
}
