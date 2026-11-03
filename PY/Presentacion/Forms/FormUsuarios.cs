using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Negocio.Models;
using Negocio.Security;
using Negocio.Services;
using PY.Presentacion.Idiomas;

namespace PY.Presentacion.Forms
{
  public partial class FormUsuarios : Form
  {
    // =========================
    // VARIABLES
    // =========================

    private readonly UsuarioService usuarioService =
        new UsuarioService();


    // =========================
    // CONSTRUCTOR
    // =========================

    public FormUsuarios()
    {
      InitializeComponent();
      Traductor.Traducir(this);

      Load += FormUsuarios_Load;
    }

    // La validación va en Load: en el constructor Close() no cierra el formulario.
    private void FormUsuarios_Load(object sender, EventArgs e)
    {
      if (!ValidarAcceso())
        return;

      CargarCombos();
      ConfigurarPermisos();
      CargarUsuarios();
    }


    // =========================
    // INICIALIZACIÓN
    // =========================

    private void CargarCombos()
    {
      cmbEstado.Items.Clear();

      cmbEstado.Items.Add(Traductor.T("Todos"));
      cmbEstado.Items.Add(Traductor.T("Activos"));
      cmbEstado.Items.Add(Traductor.T("Inactivos"));

      cmbEstado.SelectedIndex = 0;

      List<Rol> roles =
          new RolService().ObtenerTodos();

      roles.Insert(0, new Rol
      {
        RolId = 0,
        Nombre = Traductor.T("Todos")
      });

      cmbRol.DataSource = roles;
      cmbRol.DisplayMember = "Nombre";
      cmbRol.ValueMember = "RolId";
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

    private void ConfigurarPermisos()
    {
      bool puedeBuscar =
          SesionActual.TienePermiso(Permisos.USUARIOS_BUSCAR);

      txtBuscar.Enabled = puedeBuscar;
      cmbEstado.Enabled = puedeBuscar;
      cmbRol.Enabled = puedeBuscar;

      btnBuscar.Enabled = puedeBuscar;
      btnLimpiar.Enabled = puedeBuscar;

      btnNuevo.Visible =
          SesionActual.TienePermiso(Permisos.USUARIOS_CREAR);
    }


    // =========================
    // USUARIOS
    // =========================

    private void CargarUsuarios()
    {
      dgvUsuarios.AutoGenerateColumns = false;

      dgvUsuarios.DataSource =
          usuarioService.ObtenerTodos("", null, null);
    }

    private int? ObtenerRolSeleccionado()
    {
      if (cmbRol.SelectedIndex <= 0)
        return null;

      return (int?)cmbRol.SelectedValue;
    }

    private bool? ObtenerEstadoSeleccionado()
    {
      if (cmbEstado.SelectedIndex == 1)
        return true;

      if (cmbEstado.SelectedIndex == 2)
        return false;

      return null;
    }


    // =========================
    // BÚSQUEDA
    // =========================

    private void btnBuscar_Click(object sender, EventArgs e)
    {
      dgvUsuarios.DataSource =
          usuarioService.ObtenerTodos(
              txtBuscar.Text.Trim(),
              ObtenerEstadoSeleccionado(),
              ObtenerRolSeleccionado());
    }

    private void btnLimpiar_Click(object sender, EventArgs e)
    {
      txtBuscar.Clear();

      cmbEstado.SelectedIndex = 0;
      cmbRol.SelectedIndex = 0;

      CargarUsuarios();
    }


    // =========================
    // USUARIOS - ABM
    // =========================

    private void btnNuevo_Click(object sender, EventArgs e)
    {
      if (!SesionActual.TienePermiso(Permisos.USUARIOS_CREAR))
      {
        MessageBox.Show(
            Traductor.T("No posee permisos para crear usuarios."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      FormDetalleUsuario formulario =
          new FormDetalleUsuario();

      if (formulario.ShowDialog() == DialogResult.OK)
      {
        CargarUsuarios();
      }
    }


    // =========================
    // EVENTOS DEL GRID
    // =========================

    private void dgvUsuarios_CellDoubleClick(
        object sender,
        DataGridViewCellEventArgs e)
    {
      if (e.RowIndex < 0)
        return;

      if (!SesionActual.TienePermiso(Permisos.USUARIOS_EDITAR))
      {
        MessageBox.Show(
            Traductor.T("No posee permisos para editar usuarios."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      Usuario usuario =
          (Usuario)dgvUsuarios.Rows[e.RowIndex].DataBoundItem;

      FormDetalleUsuario formulario =
          new FormDetalleUsuario(usuario.UsuarioId);

      if (formulario.ShowDialog() == DialogResult.OK)
      {
        CargarUsuarios();
      }
    }

    private void dgvUsuarios_CellFormatting(
        object sender,
        DataGridViewCellFormattingEventArgs e)
    {
      if (dgvUsuarios.Columns[e.ColumnIndex].DataPropertyName == "Activo")
      {
        bool activo = (bool)e.Value;

        e.Value = activo
            ? Traductor.T("Activo")
            : Traductor.T("Inactivo");

        e.FormattingApplied = true;
      }
    }
  }
}
