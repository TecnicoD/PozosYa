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
  public partial class FormDetalleUsuario : Form
  {
    // =========================
    // VARIABLES
    // =========================

    private readonly UsuarioService usuarioService =
        new UsuarioService();

    private readonly RolService rolService =
        new RolService();

    private readonly bool esNuevo;

    private readonly int usuarioId;

    private Usuario usuario;


    // =========================
    // CONSTRUCTORES
    // =========================

    // Modo edición
    public FormDetalleUsuario(int usuarioId)
    {
      InitializeComponent();
      Traductor.Traducir(this);

      esNuevo = false;

      this.usuarioId = usuarioId;

      Load += FormDetalleUsuario_Load;
    }


    // Modo creación
    public FormDetalleUsuario()
    {
      InitializeComponent();
      Traductor.Traducir(this);

      esNuevo = true;

      Load += FormDetalleUsuario_Load;
    }


    // =========================
    // LOAD
    // =========================

    // La validación va en Load: en el constructor Close() no cierra el formulario.
    private void FormDetalleUsuario_Load(object sender, EventArgs e)
    {
      if (!ValidarAcceso())
        return;

      if (esNuevo)
      {
        usuario = new Usuario();

        CambiarModoEdicion(true);

        txtID.Text = Traductor.T("Nuevo");
        chkActivo.Checked = true;

        ConfigurarModoNuevo();
        return;
      }

      usuario = usuarioService.ObtenerPorId(usuarioId);

      if (usuario == null)
      {
        MessageBox.Show(
            Traductor.T("No se encontró el usuario."),
            Traductor.T("Error"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);

        Close();
        return;
      }

      CargarDatos();
      CargarRoles();
      CambiarModoEdicion(false);
    }


    // =========================
    // CONFIGURACIÓN
    // =========================

    private void ConfigurarModoNuevo()
    {
      Text = Traductor.T("Nuevo Usuario");

      txtContraseña.ReadOnly = false;
      txtContraseña.Text = "";

      btnModificar.Visible = false;
      btnCambiarContraseña.Visible = false;
      btnPermisosIndividuales.Visible = false;
    }

    private void ConfigurarPermisos()
    {
      btnModificar.Enabled =
          SesionActual.TienePermiso(Permisos.USUARIOS_EDITAR);

      btnGuardar.Enabled =
          SesionActual.TienePermiso(Permisos.USUARIOS_GUARDAR);

      btnCambiarContraseña.Enabled =
          SesionActual.TienePermiso(
              Permisos.USUARIOS_CAMBIAR_CONTRASENA);

      btnPermisosIndividuales.Enabled =
          SesionActual.TienePermiso(
              Permisos.USUARIOS_ADMINISTRAR_PERMISOS);

      bool puedeAdministrarRoles =
          SesionActual.TienePermiso(
              Permisos.USUARIOS_ASIGNAR_ROLES) ||
          SesionActual.TienePermiso(
              Permisos.USUARIOS_QUITAR_ROLES);

      ckbAdmin.Enabled = puedeAdministrarRoles;
      ckbGerente.Enabled = puedeAdministrarRoles;
      ckbPriorizador.Enabled = puedeAdministrarRoles;
      ckbEstimador.Enabled = puedeAdministrarRoles;
      ckbVerificador.Enabled = puedeAdministrarRoles;
      ckbOperador.Enabled = puedeAdministrarRoles;
    }

    private bool ValidarAcceso()
    {
      if (!SesionActual.TienePermiso(Permisos.SEGURIDAD_VER) ||
          !SesionActual.TienePermiso(Permisos.USUARIOS_VER))
      {
        MessageBox.Show(
            Traductor.T("No tiene permisos para acceder a esta pantalla."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        Close();

        return false;
      }

      return true;
    }


    // =========================
    // CARGA DE DATOS
    // =========================

    private void CargarDatos()
    {
      if (usuario == null)
        return;

      txtID.Text = usuario.UsuarioId.ToString();
      txtNombre.Text = usuario.Nombre;
      txtApellido.Text = usuario.Apellido;
      txtUsername.Text = usuario.Username;
      txtEmail.Text = usuario.Email;
      chkActivo.Checked = usuario.Activo;
    }

    private void CargarRoles()
    {
      if (usuario == null)
        return;

      var roles =
          rolService.ObtenerRolesPorUsuario(
              usuario.UsuarioId);

      ckbAdmin.Checked =
          roles.Any(r => r.Nombre == "SysAdmin");

      ckbGerente.Checked =
          roles.Any(r => r.Nombre == "Gerente");

      ckbPriorizador.Checked =
          roles.Any(r => r.Nombre == "Priorizador");

      ckbEstimador.Checked =
          roles.Any(r => r.Nombre == "Estimador");

      ckbVerificador.Checked =
          roles.Any(r => r.Nombre == "Verificador");

      ckbOperador.Checked =
          roles.Any(r => r.Nombre == "Operador");
    }


    // =========================
    // ROLES
    // =========================

    // Nombres de los roles tildados (para el log).
    private List<string> ObtenerNombresRolesSeleccionados()
    {
      List<string> nombres = new List<string>();

      if (ckbAdmin.Checked) nombres.Add("SysAdmin");
      if (ckbOperador.Checked) nombres.Add("Operador");
      if (ckbVerificador.Checked) nombres.Add("Verificador");
      if (ckbEstimador.Checked) nombres.Add("Estimador");
      if (ckbPriorizador.Checked) nombres.Add("Priorizador");
      if (ckbGerente.Checked) nombres.Add("Gerente");

      if (nombres.Count == 0)
        nombres.Add("sin roles");

      return nombres;
    }


    // =========================
    // VALIDACIÓN
    // =========================

    private bool ValidarDatos()
    {
      if (string.IsNullOrWhiteSpace(txtNombre.Text))
        return MostrarValidacion("Debe ingresar el nombre.", txtNombre);

      if (string.IsNullOrWhiteSpace(txtApellido.Text))
        return MostrarValidacion("Debe ingresar el apellido.", txtApellido);

      if (string.IsNullOrWhiteSpace(txtUsername.Text))
        return MostrarValidacion("Debe ingresar el nombre de usuario.", txtUsername);

      if (string.IsNullOrWhiteSpace(txtEmail.Text))
        return MostrarValidacion("Debe ingresar el correo electrónico.", txtEmail);

      int idActual = esNuevo ? 0 : usuario.UsuarioId;

      if (usuarioService.ExisteUsername(txtUsername.Text.Trim(), idActual))
        return MostrarValidacion("Ya existe un usuario con ese nombre de usuario.", txtUsername);

      if (usuarioService.ExisteEmail(txtEmail.Text.Trim(), idActual))
        return MostrarValidacion("Ya existe un usuario con ese correo electrónico.", txtEmail);

      return true;
    }

    private bool MostrarValidacion(string mensaje, Control control)
    {
      MessageBox.Show(
          Traductor.T(mensaje),
          Traductor.T("Validación"),
          MessageBoxButtons.OK,
          MessageBoxIcon.Warning);

      control.Focus();

      return false;
    }


    // =========================
    // LOG
    // =========================

    private void RegistrarLog(string accion, string descripcion)
    {
      new LogSistemaService().Registrar(
          SesionActual.Usuario.UsuarioId,
          accion,
          "USUARIOS",
          usuario.UsuarioId,
          descripcion);
    }


    private List<int> ObtenerRolesSeleccionados()
    {
      List<int> roles = new List<int>();

      if (ckbAdmin.Checked)
        roles.Add(1);

      if (ckbOperador.Checked)
        roles.Add(2);

      if (ckbVerificador.Checked)
        roles.Add(3);

      if (ckbEstimador.Checked)
        roles.Add(4);

      if (ckbPriorizador.Checked)
        roles.Add(5);

      if (ckbGerente.Checked)
        roles.Add(6);

      return roles;
    }


    // =========================
    // MODO EDICIÓN
    // =========================

    private void CambiarModoEdicion(bool editar)
    {
      txtNombre.ReadOnly = !editar;
      txtApellido.ReadOnly = !editar;
      txtUsername.ReadOnly = !editar;
      txtEmail.ReadOnly = !editar;

      txtID.ReadOnly = true;
      txtContraseña.ReadOnly = true;

      chkActivo.Enabled = editar;

      bool puedeAdministrarRoles =
          SesionActual.TienePermiso(
              Permisos.USUARIOS_ASIGNAR_ROLES) ||
          SesionActual.TienePermiso(
              Permisos.USUARIOS_QUITAR_ROLES);

      ckbAdmin.Enabled =
          editar && puedeAdministrarRoles;

      ckbGerente.Enabled =
          editar && puedeAdministrarRoles;

      ckbPriorizador.Enabled =
          editar && puedeAdministrarRoles;

      ckbEstimador.Enabled =
          editar && puedeAdministrarRoles;

      ckbVerificador.Enabled =
          editar && puedeAdministrarRoles;

      ckbOperador.Enabled =
          editar && puedeAdministrarRoles;

      btnGuardar.Visible = editar;
      btnCancelar.Visible = editar;
      btnModificar.Visible = !editar;

      btnCambiarContraseña.Enabled =
          editar &&
          SesionActual.TienePermiso(
              Permisos.USUARIOS_CAMBIAR_CONTRASENA);

      btnPermisosIndividuales.Enabled =
          editar &&
          SesionActual.TienePermiso(
              Permisos.USUARIOS_ADMINISTRAR_PERMISOS);
    }


    // =========================
    // GUARDAR
    // =========================

    private void btnGuardar_Click(object sender, EventArgs e)
    {
      if (!SesionActual.TienePermiso(
              Permisos.USUARIOS_GUARDAR))
      {
        MessageBox.Show(
            Traductor.T("No tiene permisos para guardar cambios."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      if (esNuevo &&
          string.IsNullOrWhiteSpace(txtContraseña.Text))
      {
        MessageBox.Show(
            Traductor.T("Debe ingresar una contraseña."),
            Traductor.T("Validación"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtContraseña.Focus();
        return;
      }

      if (!ValidarDatos())
        return;

      usuario.Nombre = txtNombre.Text.Trim();
      usuario.Apellido = txtApellido.Text.Trim();
      usuario.Username = txtUsername.Text.Trim();
      usuario.Email = txtEmail.Text.Trim();
      usuario.Activo = chkActivo.Checked;

      if (esNuevo)
      {
        usuario.PasswordHash =
            HashHelper.CalcularHash(
                txtContraseña.Text);

        usuario.FechaCreacion = DateTime.Now;
        usuario.DigitoVerificador = "";

        usuario.UsuarioId =
            usuarioService.Agregar(usuario);

        RegistrarLog(
            "CREAR",
            "Se creó el usuario " + usuario.Username + ".");
      }
      else
      {
        usuarioService.Actualizar(usuario);

        RegistrarLog(
            "MODIFICAR",
            "Se modificaron los datos del usuario " + usuario.Username + ".");
      }

      List<int> roles =
          ObtenerRolesSeleccionados();

      rolService.ActualizarRolesUsuario(
          usuario.UsuarioId,
          roles);

      RegistrarLog(
          "ROLES",
          "Roles del usuario " + usuario.Username + ": " +
          string.Join(", ", ObtenerNombresRolesSeleccionados()) + ".");

      DialogResult = DialogResult.OK;
      Close();
    }


    // =========================
    // ACCIONES
    // =========================

    private void btnModificar_Click(
        object sender,
        EventArgs e)
    {
      CambiarModoEdicion(true);
    }

    private void btnCancelar_Click(
        object sender,
        EventArgs e)
    {
      Close();
    }

    private void btnCambiarContraseña_Click(
        object sender,
        EventArgs e)
    {
      if (!SesionActual.TienePermiso(
              Permisos.USUARIOS_CAMBIAR_CONTRASENA))
      {
        MessageBox.Show(
            Traductor.T("No tiene permisos para cambiar contraseñas."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      FormCambiarPassword formulario =
          new FormCambiarPassword(usuario.UsuarioId);

      formulario.ShowDialog();
    }

    private void btnPermisosIndividuales_Click(
        object sender,
        EventArgs e)
    {
      if (!SesionActual.TienePermiso(
              Permisos.USUARIOS_ADMINISTRAR_PERMISOS))
      {
        MessageBox.Show(
            Traductor.T("No tiene permisos para administrar permisos individuales."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      FormPermisosUsuario formulario =
          new FormPermisosUsuario(usuario.UsuarioId);

      formulario.ShowDialog();
    }
  }
}
