using Negocio.Models;
using Negocio.Security;
using Negocio.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PY.Presentacion.Idiomas;

namespace PY.Presentacion.Forms
{
  public partial class FormConfiguracion : Form
  {
    private readonly ConfiguracionService service =
        new ConfiguracionService();

    private List<Configuracion> configuraciones;


    // =========================================================
    // CONSTRUCTOR
    // =========================================================

    public FormConfiguracion()
    {
      InitializeComponent();
      Traductor.Traducir(this);
    }


    // =========================================================
    // LOAD
    // =========================================================

    private void FormConfiguracion_Load(
    object sender,
    EventArgs e)
    {
      if (!ValidarAcceso())
        return;

      ConfigurarModoConsulta();
      CargarConfiguracion();
    }


    // =========================================================
    // SEGURIDAD
    // =========================================================

    private bool ValidarAcceso()
    {
      if (!SesionActual.TienePermiso(
          Permisos.CONFIGURACION_VER))
      {
        MessageBox.Show(
            Traductor.T("No tiene permisos para acceder a configuración."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        Close();

        return false;
      }

      return true;
    }


    // =========================================================
    // CARGAR
    // =========================================================

    private void CargarConfiguracion()
    {
      configuraciones =
          service.ObtenerTodos();

      txtNombre.Text =
          ObtenerValor("MUNICIPALIDAD_NOMBRE");

      txtDireccion.Text =
          ObtenerValor("MUNICIPALIDAD_DIRECCION");

      txtTelefono.Text =
          ObtenerValor("MUNICIPALIDAD_TELEFONO");

      txtEmail.Text =
          ObtenerValor("MUNICIPALIDAD_EMAIL");
    }


    private string ObtenerValor(
        string clave)
    {
      Configuracion configuracion =
          configuraciones.Find(
              x => x.Clave.Equals(
                  clave,
                  StringComparison.OrdinalIgnoreCase));

      if (configuracion == null)
        return "";

      return configuracion.Valor;
    }


    // =========================================================
    // MODO CONSULTA
    // =========================================================

    private void ConfigurarModoConsulta()
    {
      txtNombre.ReadOnly = true;
      txtDireccion.ReadOnly = true;
      txtTelefono.ReadOnly = true;
      txtEmail.ReadOnly = true;

      btnModificar.Visible =
          SesionActual.TienePermiso(
              Permisos.CONFIGURACION_EDITAR);

      btnGuardar.Visible = false;
      btnCancelar.Visible = false;
    }


    // =========================================================
    // MODIFICAR
    // =========================================================

    private void btnModificar_Click(
        object sender,
        EventArgs e)
    {
      if (!SesionActual.TienePermiso(
          Permisos.CONFIGURACION_EDITAR))
      {
        MessageBox.Show(
            Traductor.T("No tiene permisos para modificar la configuración."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      txtNombre.ReadOnly = false;
      txtDireccion.ReadOnly = false;
      txtTelefono.ReadOnly = false;
      txtEmail.ReadOnly = false;

      btnModificar.Visible = false;

      btnGuardar.Visible =
          SesionActual.TienePermiso(
              Permisos.CONFIGURACION_GUARDAR);

      btnCancelar.Visible = true;
    }


    // =========================================================
    // GUARDAR
    // =========================================================

    private void btnGuardar_Click(
        object sender,
        EventArgs e)
    {
      if (!SesionActual.TienePermiso(
          Permisos.CONFIGURACION_GUARDAR))
      {
        MessageBox.Show(
            Traductor.T("No tiene permisos para guardar la configuración."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      if (!ValidarDatos())
        return;

      service.Actualizar(
          "MUNICIPALIDAD_NOMBRE",
          txtNombre.Text.Trim());

      service.Actualizar(
          "MUNICIPALIDAD_DIRECCION",
          txtDireccion.Text.Trim());

      service.Actualizar(
          "MUNICIPALIDAD_TELEFONO",
          txtTelefono.Text.Trim());

      service.Actualizar(
          "MUNICIPALIDAD_EMAIL",
          txtEmail.Text.Trim());

      RegistrarLog();

      MessageBox.Show(
          Traductor.T("La configuración se guardó correctamente."),
          Traductor.T("Configuración"),
          MessageBoxButtons.OK,
          MessageBoxIcon.Information);

      CargarConfiguracion();
      ConfigurarModoConsulta();
    }


    // =========================================================
    // VALIDACIÓN
    // =========================================================

    private bool ValidarDatos()
    {
      if (string.IsNullOrWhiteSpace(
          txtNombre.Text))
      {
        MessageBox.Show(
            Traductor.T("Debe ingresar el nombre de la municipalidad."),
            Traductor.T("Validación"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtNombre.Focus();

        return false;
      }

      if (string.IsNullOrWhiteSpace(
          txtDireccion.Text))
      {
        MessageBox.Show(
            Traductor.T("Debe ingresar la dirección."),
            Traductor.T("Validación"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtDireccion.Focus();

        return false;
      }

      if (string.IsNullOrWhiteSpace(
          txtTelefono.Text))
      {
        MessageBox.Show(
            Traductor.T("Debe ingresar el teléfono."),
            Traductor.T("Validación"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtTelefono.Focus();

        return false;
      }

      if (string.IsNullOrWhiteSpace(
          txtEmail.Text))
      {
        MessageBox.Show(
            Traductor.T("Debe ingresar el correo electrónico."),
            Traductor.T("Validación"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtEmail.Focus();

        return false;
      }

      return true;
    }


    // =========================================================
    // CANCELAR
    // =========================================================

    private void btnCancelar_Click(
        object sender,
        EventArgs e)
    {
      CargarConfiguracion();
      ConfigurarModoConsulta();
    }


    // =========================================================
    // CERRAR
    // =========================================================

    private void btnCerrar_Click(
        object sender,
        EventArgs e)
    {
      Close();
    }


    // =========================================================
    // LOG
    // =========================================================

    private void RegistrarLog()
    {
      LogSistemaService logService =
          new LogSistemaService();

      logService.Registrar(
          SesionActual.Usuario.UsuarioId,
          "MODIFICAR",
          "CONFIGURACIONES",
          0,
          "Se modificaron los datos de la municipalidad.");
    }
  }
}
