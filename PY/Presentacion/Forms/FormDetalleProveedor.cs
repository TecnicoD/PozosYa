using Negocio.Models;
using Negocio.Security;
using Negocio.Services;
using System;
using System.Windows.Forms;
using PY.Presentacion.Idiomas;

namespace PY.Presentacion.Forms
{
  public partial class FormDetalleProveedor : Form
  {
    private readonly ProveedorService proveedorService =
        new ProveedorService();

    private readonly bool esNuevo;

    private readonly int proveedorId;

    private Proveedor proveedor;

    // =========================================================
    // NUEVO
    // =========================================================

    public FormDetalleProveedor()
    {
      InitializeComponent();
      Traductor.Traducir(this);

      esNuevo = true;

      Load += FormDetalleProveedor_Load;
    }

    // =========================================================
    // EDITAR
    // =========================================================

    public FormDetalleProveedor(int proveedorId)
    {
      InitializeComponent();
      Traductor.Traducir(this);

      esNuevo = false;

      this.proveedorId = proveedorId;

      Load += FormDetalleProveedor_Load;
    }

    // =========================================================
    // LOAD
    // =========================================================

    // La validación va en Load: en el constructor Close() no cierra el formulario.
    private void FormDetalleProveedor_Load(object sender, EventArgs e)
    {
      if (esNuevo)
      {
        if (!SesionActual.TienePermiso(Permisos.PROVEEDORES_CREAR))
        {
          MessageBox.Show(
              Traductor.T("No posee permisos para crear proveedores."),
              Traductor.T("Acceso denegado"),
              MessageBoxButtons.OK,
              MessageBoxIcon.Warning);

          Close();
          return;
        }

        proveedor = new Proveedor();

        txtID.Text = Traductor.T("Nuevo");
        chkActivo.Checked = true;

        CambiarModoEdicion(true);

        Text = Traductor.T("Nuevo Proveedor");
        return;
      }

      if (!SesionActual.TienePermiso(Permisos.PROVEEDORES_EDITAR))
      {
        MessageBox.Show(
            Traductor.T("No posee permisos para editar proveedores."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        Close();
        return;
      }

      proveedor =
          proveedorService.ObtenerPorId(proveedorId);

      if (proveedor == null)
      {
        MessageBox.Show(
            Traductor.T("No se encontró el proveedor."),
            Traductor.T("Error"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);

        Close();
        return;
      }

      CargarDatos();
      CambiarModoEdicion(false);

      Text = Traductor.T("Detalle del Proveedor");
    }

    // =========================================================
    // DATOS
    // =========================================================

    private void CargarDatos()
    {
      txtID.Text = proveedor.ProveedorId.ToString();

      txtNombre.Text = proveedor.Nombre;
      txtCuit.Text = proveedor.Cuit;
      txtTelefono.Text = proveedor.Telefono;
      txtMail.Text = proveedor.Mail;
      txtDireccion.Text = proveedor.Direccion;

      chkActivo.Checked = proveedor.Activo;
    }

    // =========================================================
    // MODO EDICIÓN
    // =========================================================

    private void CambiarModoEdicion(bool editar)
    {
      txtNombre.ReadOnly = !editar;
      txtCuit.ReadOnly = !editar;
      txtTelefono.ReadOnly = !editar;
      txtMail.ReadOnly = !editar;
      txtDireccion.ReadOnly = !editar;

      txtID.ReadOnly = true;

      chkActivo.Enabled = editar;

      btnGuardar.Visible = editar;
      btnCancelar.Visible = editar;
      btnModificar.Visible = !editar;
    }

    // =========================================================
    // VALIDACIÓN
    // =========================================================

    private bool ValidarDatos()
    {
      if (string.IsNullOrWhiteSpace(txtNombre.Text))
      {
        MessageBox.Show(
            Traductor.T("Debe ingresar el nombre del proveedor."),
            Traductor.T("Validación"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtNombre.Focus();
        return false;
      }

      if (string.IsNullOrWhiteSpace(txtCuit.Text))
      {
        MessageBox.Show(
            Traductor.T("Debe ingresar el CUIT."),
            Traductor.T("Validación"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtCuit.Focus();
        return false;
      }

      if (proveedorService.ExisteCuit(
          txtCuit.Text.Trim(),
          esNuevo ? 0 : proveedor.ProveedorId))
      {
        MessageBox.Show(
            Traductor.T("Ya existe un proveedor con ese CUIT."),
            Traductor.T("Validación"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtCuit.Focus();
        return false;
      }

      return true;
    }

    // =========================================================
    // LOG
    // =========================================================

    private void RegistrarLog(
        string accion,
        int proveedorId,
        string descripcion)
    {
      LogSistemaService logService =
          new LogSistemaService();

      logService.Registrar(
          SesionActual.Usuario.UsuarioId,
          accion,
          "PROVEEDORES",
          proveedorId,
          descripcion);
    }

    // =========================================================
    // MODIFICAR
    // =========================================================

    private void btnModificar_Click(object sender, EventArgs e)
    {
      if (!SesionActual.TienePermiso(Permisos.PROVEEDORES_EDITAR))
      {
        MessageBox.Show(
            Traductor.T("No posee permisos para editar proveedores."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      CambiarModoEdicion(true);
    }

    // =========================================================
    // CANCELAR
    // =========================================================

    private void btnCancelar_Click(object sender, EventArgs e)
    {
      if (!esNuevo)
      {
        CargarDatos();
        CambiarModoEdicion(false);
        return;
      }

      Close();
    }

    // =========================================================
    // GUARDAR
    // =========================================================

    private void btnGuardar_Click(object sender, EventArgs e)
    {
      if (!SesionActual.TienePermiso(Permisos.PROVEEDORES_GUARDAR))
      {
        MessageBox.Show(
            Traductor.T("No posee permisos para guardar proveedores."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      if (!ValidarDatos())
        return;

      proveedor.Nombre = txtNombre.Text.Trim();
      proveedor.Cuit = txtCuit.Text.Trim();
      proveedor.Telefono = txtTelefono.Text.Trim();
      proveedor.Mail = txtMail.Text.Trim();
      proveedor.Direccion = txtDireccion.Text.Trim();
      proveedor.Activo = chkActivo.Checked;

      if (esNuevo)
      {
        int proveedorId =
            proveedorService.Agregar(proveedor);

        proveedor.ProveedorId = proveedorId;

        RegistrarLog(
            "CREAR",
            proveedorId,
            "Se creó el proveedor " + proveedor.Nombre);
      }
      else
      {
        proveedorService.Actualizar(proveedor);

        RegistrarLog(
            "MODIFICAR",
            proveedor.ProveedorId,
            "Se modificó el proveedor " + proveedor.Nombre);
      }

      MessageBox.Show(
          Traductor.T("El proveedor se guardó correctamente."),
          Traductor.T("Proveedor"),
          MessageBoxButtons.OK,
          MessageBoxIcon.Information);

      DialogResult = DialogResult.OK;
      Close();
    }
  }
}
