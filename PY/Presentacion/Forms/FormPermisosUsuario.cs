using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Negocio.Models;
using Negocio.Security;
using Negocio.Services;
using PY.Presentacion.Idiomas;

namespace PY.Presentacion.Forms
{
  public partial class FormPermisosUsuario : Form
  {
    // =========================
    // VARIABLES
    // =========================

    private readonly PermisoService permisoService =
        new PermisoService();

    private readonly int usuarioId;


    // =========================
    // CONSTRUCTOR
    // =========================

    public FormPermisosUsuario(int usuarioId)
    {
      InitializeComponent();
      Traductor.Traducir(this);

      this.usuarioId = usuarioId;

      Load += FormPermisosUsuario_Load;
    }

    // La validación va en Load: en el constructor Close() no cierra el formulario.
    private void FormPermisosUsuario_Load(object sender, EventArgs e)
    {
      if (!ValidarAcceso())
        return;

      ConfigurarPermisos();
      CargarPermisos();
    }


    // =========================
    // SEGURIDAD
    // =========================

    private bool ValidarAcceso()
    {
      bool puedeOtorgar =
          SesionActual.TienePermiso(
              Permisos.USUARIOS_OTORGAR_PERMISO);

      bool puedeRevocar =
          SesionActual.TienePermiso(
              Permisos.USUARIOS_REVOCAR_PERMISO);

      if (!puedeOtorgar && !puedeRevocar)
      {
        MessageBox.Show(
            Traductor.T("No posee permisos para administrar permisos individuales."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        Close();

        return false;
      }

      return true;
    }

    private void ConfigurarPermisos()
    {
      bool puedeOtorgar =
          SesionActual.TienePermiso(
              Permisos.USUARIOS_OTORGAR_PERMISO);

      bool puedeRevocar =
          SesionActual.TienePermiso(
              Permisos.USUARIOS_REVOCAR_PERMISO);

      btnGuardar.Enabled =
          puedeOtorgar || puedeRevocar;
    }


    // =========================
    // PERMISOS
    // =========================

    private void CargarPermisos()
    {
      flpPermisos.Controls.Clear();

      List<PermisoUsuario> permisos =
          permisoService.ObtenerVistaPermisosUsuario(
              usuarioId);

      bool puedeOtorgar =
          SesionActual.TienePermiso(
              Permisos.USUARIOS_OTORGAR_PERMISO);

      bool puedeRevocar =
          SesionActual.TienePermiso(
              Permisos.USUARIOS_REVOCAR_PERMISO);

      var grupos =
          permisos.GroupBy(
              p => p.Nombre.Split('_')[0]);

      foreach (var grupo in grupos)
      {
        GroupBox grupoPermisos =
            CrearGrupoPermisos(grupo);

        flpPermisos.Controls.Add(
            grupoPermisos);
      }
    }

    private GroupBox CrearGrupoPermisos(
        IEnumerable<PermisoUsuario> permisos)
    {
      GroupBox grupo =
          new GroupBox();

      grupo.Text =
          permisos.First().Nombre.Split('_')[0];

      grupo.AutoSize = true;
      grupo.AutoSizeMode =
          AutoSizeMode.GrowAndShrink;
      grupo.Width = 700;

      FlowLayoutPanel panel =
          new FlowLayoutPanel();

      panel.FlowDirection =
          FlowDirection.TopDown;

      panel.WrapContents = false;
      panel.AutoSize = true;
      panel.Dock = DockStyle.Fill;

      foreach (PermisoUsuario permiso in permisos)
      {
        CheckBox checkBox =
            CrearCheckBoxPermiso(permiso);

        panel.Controls.Add(checkBox);
      }

      grupo.Controls.Add(panel);

      return grupo;
    }

    private CheckBox CrearCheckBoxPermiso(
        PermisoUsuario permiso)
    {
      CheckBox checkBox =
          new CheckBox();

      checkBox.AutoSize = true;

      string[] partes =
          permiso.Nombre.Split('_');

      checkBox.Text =
          partes.Length > 1
              ? string.Join(
                  " ",
                  partes.Skip(1))
              : permiso.Nombre;

      checkBox.Checked =
          permiso.Concedido;

      bool puedeOtorgar =
          SesionActual.TienePermiso(
              Permisos.USUARIOS_OTORGAR_PERMISO);

      bool puedeRevocar =
          SesionActual.TienePermiso(
              Permisos.USUARIOS_REVOCAR_PERMISO);

      checkBox.Enabled =
          permiso.Concedido
              ? puedeRevocar
              : puedeOtorgar;

      checkBox.Tag = permiso;

      return checkBox;
    }


    // =========================
    // GUARDAR
    // =========================

    private void btnGuardar_Click(
        object sender,
        EventArgs e)
    {
      bool puedeOtorgar =
          SesionActual.TienePermiso(
              Permisos.USUARIOS_OTORGAR_PERMISO);

      bool puedeRevocar =
          SesionActual.TienePermiso(
              Permisos.USUARIOS_REVOCAR_PERMISO);

      if (!puedeOtorgar && !puedeRevocar)
      {
        MessageBox.Show(
            Traductor.T("No tiene permisos para guardar cambios."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      List<PermisoUsuario> permisos =
          ObtenerPermisosFormulario();

      permisoService.GuardarPermisosUsuario(
          usuarioId,
          permisos);

      new LogSistemaService().Registrar(
          SesionActual.Usuario.UsuarioId,
          "PERMISOS",
          "USUARIOS",
          usuarioId,
          "Se modificaron los permisos individuales del usuario.");

      MessageBox.Show(
          Traductor.T("Permisos guardados correctamente."),
          Traductor.T("Información"),
          MessageBoxButtons.OK,
          MessageBoxIcon.Information);

      Close();
    }

    private List<PermisoUsuario> ObtenerPermisosFormulario()
    {
      List<PermisoUsuario> permisos =
          new List<PermisoUsuario>();

      foreach (GroupBox grupo in flpPermisos.Controls)
      {
        FlowLayoutPanel panel =
            (FlowLayoutPanel)grupo.Controls[0];

        foreach (CheckBox checkBox in panel.Controls)
        {
          PermisoUsuario permiso =
              (PermisoUsuario)checkBox.Tag;

          permiso.Concedido =
              checkBox.Checked;

          permisos.Add(permiso);
        }
      }

      return permisos;
    }


    // =========================
    // EVENTOS
    // =========================

    private void btnCancelar_Click(
        object sender,
        EventArgs e)
    {
      Close();
    }
  }
}
