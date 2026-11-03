using System;
using System.Windows.Forms;
using Negocio.Security;
using Negocio.Services;
using PY.Presentacion.Forms;
using PY.Presentacion.Idiomas;

namespace PY.Presentacion.Components
{
  public partial class Sidebar : UserControl
  {
    // =========================
    // CONSTRUCTOR
    // =========================

    public Sidebar()
    {
      InitializeComponent();
      Traductor.Traducir(this);
    }


    // =========================
    // NAVEGACIÓN
    // =========================

    private void btnReclamos_Click(object sender, EventArgs e)
    {
      AbrirFormulario(new FormInicioReclamos());
    }

    private void btnPriorizacion_Click(object sender, EventArgs e)
    {
      AbrirFormulario(new FormPriorizacion());
    }

    private void btnPartidasPresupuestarias_Click(object sender, EventArgs e)
    {
      AbrirFormulario(new FormPartidas());
    }

    private void btnProveedores_Click(object sender, EventArgs e)
    {
      AbrirFormulario(new FormProveedores());
    }

    private void btnConfiguracion_Click(object sender, EventArgs e)
    {
      AbrirFormulario(new FormConfiguracion());
    }

    private void btnSeguridad_Click(object sender, EventArgs e)
    {
      AbrirFormulario(new FormUsuarios());
    }

    private void btnLogs_Click(object sender, EventArgs e)
    {
      AbrirFormulario(new FormLogs());
    }

    private void btnAdministrador_Click(object sender, EventArgs e)
    {
      AbrirFormulario(new FormAdministrador());
    }


        // =========================
        // SESIÓN
        // =========================

        private void btnCerrarSesion_Click(object sender, EventArgs e)
    {
      DialogResult respuesta = MessageBox.Show(
          Traductor.T("¿Desea cerrar la sesión?"),
          Traductor.T("Cerrar sesión"),
          MessageBoxButtons.YesNo,
          MessageBoxIcon.Question
      );

      if (respuesta != DialogResult.Yes)
        return;

      LogSistemaService logService = new LogSistemaService();

      logService.Registrar(
          SesionActual.Usuario.UsuarioId,
          "LOGOUT",
          "USUARIOS",
          SesionActual.Usuario.UsuarioId,
          "Cierre de sesión del usuario " +
          SesionActual.Usuario.Username
      );

      SesionActual.CerrarSesion();

      FormPrincipal principal = (FormPrincipal)FindForm();

      principal.DialogResult = DialogResult.Retry;
      principal.Close();
    }


    // =========================
    // PERMISOS
    // =========================

    public void AplicarPermisos()
    {
      btnReclamos.Visible =
          SesionActual.TienePermiso("RECLAMOS_VER");

      btnPriorizacion.Visible =
          SesionActual.TienePermiso("PRIORIZACION_VER");

      btnPartidasPresupuestarias.Visible =
          SesionActual.TienePermiso("PARTIDAS_PRESUPUESTARIAS_VER");

      btnProveedores.Visible =
          SesionActual.TienePermiso("PROVEEDORES_VER");

      btnConfiguracion.Visible =
          SesionActual.TienePermiso("CONFIGURACION_VER");

      btnSeguridad.Visible =
          SesionActual.TienePermiso("SEGURIDAD_VER");

      btnLogs.Visible =
          SesionActual.TienePermiso("LOGS_VER");

      btnAdministrador.Visible =
          SesionActual.TienePermiso("ADMINISTRADOR_VER");
      }


        // =========================
        // MÉTODOS AUXILIARES
        // =========================

        private void AbrirFormulario(Form formulario)
    {
      FormPrincipal principal = (FormPrincipal)FindForm();

      principal.AbrirContenido(formulario);
    }
    }
}
