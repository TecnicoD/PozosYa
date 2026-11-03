using System;
using System.Windows.Forms;
using Negocio.Security;
using Negocio.Services;
using PY.Presentacion.Components;
using PY.Presentacion.Idiomas;

namespace PY.Presentacion.Forms
{
  public partial class FormPrincipal : Form
  {
    // =========================
    // VARIABLES
    // =========================

    private Sidebar sidebar;


    // =========================
    // CONSTRUCTOR
    // =========================

    public FormPrincipal()
    {
      InitializeComponent();
      Traductor.Traducir(this);

      InicializarSidebar();
      InicializarHeader();

      // Pantalla inicial: el listado de reclamos, solo si tiene permiso.
      if (SesionActual.TienePermiso(Permisos.RECLAMOS_VER))
        AbrirContenido(new FormInicioReclamos());

      sidebar.AplicarPermisos();
    }


    // =========================
    // INICIALIZACIÓN
    // =========================

    private void InicializarSidebar()
    {
      sidebar = new Sidebar();
      sidebar.Dock = DockStyle.Fill;

      panelSidebar.Controls.Add(sidebar);
    }

    private void InicializarHeader()
    {
      Header header = new Header();
      header.Dock = DockStyle.Fill;

      panelSuperior.Controls.Add(header);
    }


    // =========================
    // NAVEGACIÓN
    // =========================

    public void AbrirContenido(Form formulario)
    {
      panelContenido.Controls.Clear();

      formulario.TopLevel = false;
      formulario.FormBorderStyle = FormBorderStyle.None;
      formulario.Dock = DockStyle.Fill;

      panelContenido.Controls.Add(formulario);
      formulario.Show();
    }


    // =========================
    // EVENTOS
    // =========================

    private void FormPrincipal_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (SesionActual.Usuario == null)
        return;

      LogSistemaService logService = new LogSistemaService();

      logService.Registrar(
          SesionActual.Usuario.UsuarioId,
          "LOGOUT",
          "USUARIOS",
          SesionActual.Usuario.UsuarioId,
          "El usuario cerró la aplicación"
      );

      SesionActual.CerrarSesion();
    }

    private void panelContenido_Paint(object sender, PaintEventArgs e)
    {
    }
  }
}
