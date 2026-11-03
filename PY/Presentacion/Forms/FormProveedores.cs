using Negocio.Models;
using Negocio.Security;
using Negocio.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PY.Presentacion.Idiomas;

namespace PY.Presentacion.Forms
{
  public partial class FormProveedores : Form
  {
    private readonly ProveedorService proveedorService =
        new ProveedorService();

    public FormProveedores()
    {
      InitializeComponent();
      Traductor.Traducir(this);

      dgvProveedores.AutoGenerateColumns = false;

      Load += FormProveedores_Load;
    }

    // La validación va en Load: en el constructor Close() no cierra el formulario.
    private void FormProveedores_Load(object sender, EventArgs e)
    {
      if (!ValidarAcceso())
        return;

      CargarFiltros();
      ConfigurarPermisos();
      CargarProveedores();
    }

    // =========================================================
    // ACCESO
    // =========================================================

    private bool ValidarAcceso()
    {
      if (!SesionActual.TienePermiso(Permisos.PROVEEDORES_VER))
      {
        MessageBox.Show(
            Traductor.T("No tiene permisos para acceder a proveedores."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        Close();
        return false;
      }

      return true;
    }

    // =========================================================
    // PERMISOS
    // =========================================================

    private void ConfigurarPermisos()
    {
      bool puedeBuscar =
          SesionActual.TienePermiso(Permisos.PROVEEDORES_BUSCAR);

      bool puedeCrear =
          SesionActual.TienePermiso(Permisos.PROVEEDORES_CREAR);

      bool puedeEditar =
          SesionActual.TienePermiso(Permisos.PROVEEDORES_EDITAR);

      txtBuscar.Enabled = puedeBuscar;
      cmbEstado.Enabled = puedeBuscar;

      btnBuscar.Enabled = puedeBuscar;
      btnLimpiar.Enabled = puedeBuscar;

      btnNuevo.Visible = puedeCrear;
    }

    // =========================================================
    // FILTROS
    // =========================================================

    private void CargarFiltros()
    {
      cmbEstado.Items.Clear();

      cmbEstado.Items.Add(Traductor.T("Todos"));
      cmbEstado.Items.Add(Traductor.T("Activos"));
      cmbEstado.Items.Add(Traductor.T("Inactivos"));

      cmbEstado.SelectedIndex = 0;
    }

    // =========================================================
    // CARGAR
    // =========================================================

    private void CargarProveedores()
    {
      dgvProveedores.DataSource =
          proveedorService.ObtenerTodos();

      lblCantidad.Text =
          string.Format(Traductor.T("Mostrando {0} proveedores."), dgvProveedores.Rows.Count);
    }

    // =========================================================
    // FORMATO
    // =========================================================

    private void dgvProveedores_CellFormatting(
        object sender,
        DataGridViewCellFormattingEventArgs e)
    {
      if (dgvProveedores.Columns[e.ColumnIndex]
          .DataPropertyName == "Activo")
      {
        if (e.Value != null)
        {
          bool activo = (bool)e.Value;

          e.Value = activo
              ? Traductor.T("Activo")
              : Traductor.T("Inactivo");

          e.FormattingApplied = true;
        }
      }
    }

    // =========================================================
    // DETALLE / EDITAR
    // =========================================================

    private void dgvProveedores_CellDoubleClick(
        object sender,
        DataGridViewCellEventArgs e)
    {
      if (e.RowIndex < 0)
        return;

      if (!SesionActual.TienePermiso(Permisos.PROVEEDORES_EDITAR))
      {
        MessageBox.Show(
            Traductor.T("No posee permisos para editar proveedores."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      Proveedor proveedor =
          (Proveedor)dgvProveedores
              .Rows[e.RowIndex]
              .DataBoundItem;

      FormDetalleProveedor formulario =
          new FormDetalleProveedor(proveedor.ProveedorId);

      if (formulario.ShowDialog() == DialogResult.OK)
      {
        CargarProveedores();
      }
    }

    // =========================================================
    // NUEVO
    // =========================================================

    private void btnNuevo_Click(object sender, EventArgs e)
    {
      if (!SesionActual.TienePermiso(Permisos.PROVEEDORES_CREAR))
      {
        MessageBox.Show(
            Traductor.T("No posee permisos para crear proveedores."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      FormDetalleProveedor formulario =
          new FormDetalleProveedor();

      if (formulario.ShowDialog() == DialogResult.OK)
      {
        CargarProveedores();
      }
    }

    // =========================================================
    // BUSCAR
    // =========================================================

    private void btnBuscar_Click(object sender, EventArgs e)
    {
      if (!SesionActual.TienePermiso(Permisos.PROVEEDORES_BUSCAR))
        return;

      string texto = txtBuscar.Text.Trim();

      List<Proveedor> lista =
          proveedorService.ObtenerTodos();

      if (!string.IsNullOrWhiteSpace(texto))
      {
        lista = lista.FindAll(p =>
            p.Nombre.IndexOf(
                texto,
                StringComparison.OrdinalIgnoreCase) >= 0
            ||
            p.Cuit.IndexOf(
                texto,
                StringComparison.OrdinalIgnoreCase) >= 0);
      }

      if (cmbEstado.SelectedIndex == 1)
      {
        lista = lista.FindAll(p => p.Activo);
      }
      else if (cmbEstado.SelectedIndex == 2)
      {
        lista = lista.FindAll(p => !p.Activo);
      }

      dgvProveedores.DataSource = lista;

      lblCantidad.Text =
          string.Format(Traductor.T("Mostrando {0} proveedores."), lista.Count);
    }

    // =========================================================
    // LIMPIAR
    // =========================================================

    private void btnLimpiar_Click(object sender, EventArgs e)
    {
      if (!SesionActual.TienePermiso(Permisos.PROVEEDORES_BUSCAR))
        return;

      txtBuscar.Clear();
      cmbEstado.SelectedIndex = 0;

      CargarProveedores();
    }
  }
}
