using Negocio.Models;
using Negocio.Security;
using Negocio.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using PY.Presentacion.Idiomas;

namespace PY.Presentacion.Forms
{
  public partial class FormPresupuesto : Form
  {
    // =========================================================
    // VARIABLES
    // =========================================================

    private readonly int reclamoId;

    private ReclamoDetalleDTO reclamo;

    private Presupuesto presupuesto;

    private string rutaPdfSeleccionado;

    private readonly ReclamoService reclamoService =
        new ReclamoService();

    private readonly PresupuestoService presupuestoService =
        new PresupuestoService();

    private readonly ProveedorService proveedorService =
        new ProveedorService();


    // =========================================================
    // CONSTRUCTOR
    // =========================================================

    public FormPresupuesto(int reclamoId)
    {
      InitializeComponent();
      Traductor.Traducir(this);

      this.reclamoId = reclamoId;
    }


    // =========================================================
    // LOAD
    // =========================================================

    private void FormPresupuesto_Load(
        object sender,
        EventArgs e)
    {
      if (!ValidarAcceso())
        return;

      CargarReclamo();
      CargarProveedores();
      CargarPresupuesto();
    }


    // =========================================================
    // SEGURIDAD
    // =========================================================

    private bool ValidarAcceso()
    {
      if (!SesionActual.TienePermiso(
          Permisos.PRESUPUESTO_VER))
      {
        MessageBox.Show(
            Traductor.T("No tiene permisos para acceder al presupuesto."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        Close();

        return false;
      }

      return true;
    }


    // =========================================================
    // RECLAMO
    // =========================================================

    private void CargarReclamo()
    {
      reclamo =
          reclamoService.ObtenerPorId(reclamoId);

      if (reclamo == null)
      {
        MessageBox.Show(
            Traductor.T("No se encontró el reclamo."),
            Traductor.T("Error"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);

        Close();
        return;
      }

      lblNumeroReclamo.Text =
          Traductor.T("Detalle del reclamo #") +
          reclamo.NumeroReclamo;

      lblEstado.Text =
          Traductor.T("Estado actual: ") +
          reclamo.Estado;
    }


    // =========================================================
    // PROVEEDORES
    // =========================================================

    private void CargarProveedores()
    {
      List<Proveedor> proveedores =
          proveedorService.ObtenerTodos();

      cmbProveedor.DataSource =
          proveedores;

      cmbProveedor.DisplayMember =
          "Nombre";

      cmbProveedor.ValueMember =
          "ProveedorId";

      cmbProveedor.SelectedIndex = -1;
    }


    // =========================================================
    // PRESUPUESTO
    // =========================================================

    private void CargarPresupuesto()
    {
      presupuesto =
          presupuestoService.ObtenerPorReclamo(reclamoId);

      rutaPdfSeleccionado = null;

      if (presupuesto == null)
      {
        ConfigurarNuevo();
        return;
      }

      CargarDatosPresupuesto();
      ConfigurarModoConsulta();
    }


    private void CargarDatosPresupuesto()
    {
      cmbProveedor.SelectedValue =
          presupuesto.ProveedorId;

      txtMontoEstimado.Text =
          presupuesto.MontoEstimado.ToString("N2");

      txtMontoFinal.Text =
          presupuesto.MontoFinal.HasValue
              ? presupuesto.MontoFinal.Value.ToString("N2")
              : "";

      lblArchivoSeleccionado.Text =
          string.IsNullOrWhiteSpace(
              presupuesto.ArchivoPdf)
              ? Traductor.T("Ningún archivo cargado")
              : Path.GetFileName(
                  presupuesto.ArchivoPdf);

      txtObservaciones.Clear();
    }


    // =========================================================
    // NUEVO PRESUPUESTO
    // =========================================================

    private void ConfigurarNuevo()
    {
      // -------------------------------------------------------
      // SOLO SE PUEDE CREAR SI ESTÁ VERIFICADO
      // -------------------------------------------------------

      if (!EsEstado("Verificado"))
      {
        BloquearEdicion();

        return;
      }

      cmbProveedor.SelectedIndex = -1;

      txtMontoEstimado.Clear();
      txtMontoFinal.Clear();
      txtObservaciones.Clear();

      rutaPdfSeleccionado = null;

      lblArchivoSeleccionado.Text =
          Traductor.T("Ningún archivo seleccionado");

      cmbProveedor.Enabled =
          SesionActual.TienePermiso(
              Permisos.PRESUPUESTO_SELECCIONAR_PROVEEDOR);

      txtMontoEstimado.Enabled =
          SesionActual.TienePermiso(
              Permisos.PRESUPUESTO_CARGAR_MONTO);

      txtMontoFinal.Enabled =
          SesionActual.TienePermiso(
              Permisos.PRESUPUESTO_CARGAR_MONTO);

      txtObservaciones.Enabled =
          SesionActual.TienePermiso(
              Permisos.PRESUPUESTO_GUARDAR);

      btnSeleccionarArchivo.Enabled =
          SesionActual.TienePermiso(
              Permisos.PRESUPUESTO_ADJUNTAR_ARCHIVO);

      // NO existe Guardar en este modo.
      btnGuardar.Visible = false;

      btnModificar.Visible = false;

      // Confirmar es la acción final.
      btnConfirmar.Visible =
          SesionActual.TienePermiso(
              Permisos.PRESUPUESTO_CONFIRMAR);

      ConfigurarBotonCancelar();
    }


    // =========================================================
    // MODO CONSULTA
    // =========================================================

    private void ConfigurarModoConsulta()
    {
      BloquearEdicion();

      // -------------------------------------------------------
      // MODIFICAR
      // -------------------------------------------------------

      btnModificar.Visible =
          SesionActual.TienePermiso(
              Permisos.PRESUPUESTO_MODIFICAR);


      // -------------------------------------------------------
      // CONFIRMAR
      // -------------------------------------------------------

      // Si ya está presupuestado, no se vuelve a confirmar.
      btnConfirmar.Visible = false;

      ConfigurarBotonCancelar();
    }


    private void BloquearEdicion()
    {
      cmbProveedor.Enabled = false;

      txtMontoEstimado.Enabled = false;
      txtMontoFinal.Enabled = false;
      txtObservaciones.Enabled = false;

      btnSeleccionarArchivo.Enabled = false;

      btnGuardar.Visible = false;
    }


    // =========================================================
    // MODIFICAR
    // =========================================================

    private void btnModificar_Click(
        object sender,
        EventArgs e)
    {
      if (!SesionActual.TienePermiso(
          Permisos.PRESUPUESTO_MODIFICAR))
      {
        MessageBox.Show(
            Traductor.T("No tiene permisos para modificar el presupuesto."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      // Solo se modifica un presupuesto existente.
      if (presupuesto == null)
        return;

      // -------------------------------------------------------
      // HABILITAR CAMPOS SEGÚN PERMISOS
      // -------------------------------------------------------

      cmbProveedor.Enabled =
          SesionActual.TienePermiso(
              Permisos.PRESUPUESTO_SELECCIONAR_PROVEEDOR);

      txtMontoEstimado.Enabled =
          SesionActual.TienePermiso(
              Permisos.PRESUPUESTO_CARGAR_MONTO);

      txtMontoFinal.Enabled =
          SesionActual.TienePermiso(
              Permisos.PRESUPUESTO_CARGAR_MONTO);

      txtObservaciones.Enabled =
          SesionActual.TienePermiso(
              Permisos.PRESUPUESTO_GUARDAR);

      btnSeleccionarArchivo.Enabled =
          SesionActual.TienePermiso(
              Permisos.PRESUPUESTO_ADJUNTAR_ARCHIVO);

      // -------------------------------------------------------
      // GUARDAR CAMBIOS
      // -------------------------------------------------------

      btnGuardar.Text = Traductor.T("Guardar cambios");

      btnGuardar.Visible =
          SesionActual.TienePermiso(
              Permisos.PRESUPUESTO_GUARDAR);

      btnModificar.Visible = false;

      btnConfirmar.Visible = false;
    }


    // =========================================================
    // GUARDAR CAMBIOS
    // =========================================================

    private void btnGuardar_Click(
        object sender,
        EventArgs e)
    {
      if (!SesionActual.TienePermiso(
          Permisos.PRESUPUESTO_GUARDAR))
      {
        MessageBox.Show(
            Traductor.T("No tiene permisos para guardar cambios."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      if (presupuesto == null)
        return;

      if (!ValidarDatos())
        return;

      Presupuesto datos =
          ObtenerDatosFormulario();

      datos.PresupuestoId =
          presupuesto.PresupuestoId;

      try
      {
        presupuestoService.Actualizar(
            datos);
      }
      catch (Exception ex)
      {
        MostrarErrorGuardado(ex);
        return;
      }

      presupuesto = datos;

      RegistrarLog(
          "MODIFICAR",
          "Se modificó el presupuesto.");

      MessageBox.Show(
          Traductor.T("Los cambios se guardaron correctamente."),
          Traductor.T("Presupuesto"),
          MessageBoxButtons.OK,
          MessageBoxIcon.Information);

      CargarPresupuesto();
    }


    // =========================================================
    // CONFIRMAR PRESUPUESTO
    // =========================================================

    private void btnConfirmar_Click(
    object sender,
    EventArgs e)
    {
      // =========================================================
      // PERMISO
      // =========================================================

      if (!SesionActual.TienePermiso(
          Permisos.PRESUPUESTO_CONFIRMAR))
      {
        MessageBox.Show(
            Traductor.T("No tiene permisos para confirmar el presupuesto."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }


      // =========================================================
      // ESTADO DEL RECLAMO
      // =========================================================

      if (!EsEstado("Verificado"))
      {
        MessageBox.Show(
            Traductor.T("Solo se puede crear y confirmar un presupuesto ") +
            Traductor.T("cuando el reclamo está en estado VERIFICADO."),
            Traductor.T("Presupuesto"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }


      // =========================================================
      // VALIDAR DATOS
      // =========================================================

      if (!ValidarDatos())
        return;


      // -------------------------------------------------------
      // CONFIRMACIÓN
      // -------------------------------------------------------

      DialogResult respuesta =
          MessageBox.Show(
              Traductor.T("¿Está seguro de que desea confirmar el presupuesto?\n\n") +
              Traductor.T("El reclamo pasará al estado PRESUPUESTADO."),
              Traductor.T("Confirmar presupuesto"),
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Question);

      if (respuesta != DialogResult.Yes)
        return;


      // -------------------------------------------------------
      // CREAR PRESUPUESTO
      // -------------------------------------------------------

      Presupuesto datos =
          ObtenerDatosFormulario();

      int presupuestoId;

      try
      {
        presupuestoId =
            presupuestoService.Agregar(datos);
      }
      catch (Exception ex)
      {
        MostrarErrorGuardado(ex);
        return;
      }

      datos.PresupuestoId =
          presupuestoId;

      presupuesto = datos;


      // -------------------------------------------------------
      // CAMBIAR ESTADO
      // -------------------------------------------------------

      int estadoId =
          ObtenerEstadoId("Presupuestado");

      if (estadoId == 0)
        return;

      reclamo.EstadoId =
          estadoId;

      reclamoService.Actualizar(
          reclamo);


      // -------------------------------------------------------
      // LOG
      // -------------------------------------------------------

      RegistrarLog(
          "CONFIRMAR",
          "Se creó y confirmó el presupuesto. " +
          "El reclamo pasó a PRESUPUESTADO.");


      MessageBox.Show(
          Traductor.T("El presupuesto fue confirmado correctamente."),
          Traductor.T("Presupuesto"),
          MessageBoxButtons.OK,
          MessageBoxIcon.Information);


      // -------------------------------------------------------
      // RECARGAR
      // -------------------------------------------------------

      CargarReclamo();
      CargarPresupuesto();
    }


    // =========================================================
    // CANCELAR RECLAMO
    // =========================================================

    private void btnCancelarReclamo_Click(
        object sender,
        EventArgs e)
    {
      if (!SesionActual.TienePermiso(
          Permisos.PRESUPUESTO_CANCELAR_RECLAMO))
      {
        MessageBox.Show(
            Traductor.T("No tiene permisos para cancelar el reclamo."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      if (EsEstado("Cancelado"))
      {
        MessageBox.Show(
            Traductor.T("El reclamo ya está cancelado."),
            Traductor.T("Presupuesto"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);

        return;
      }

      if (EsEstado("Finalizado"))
      {
        MessageBox.Show(
            Traductor.T("No se puede cancelar un reclamo finalizado."),
            Traductor.T("Presupuesto"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      DialogResult respuesta =
          MessageBox.Show(
              Traductor.T("¿Está seguro de que desea cancelar el reclamo?"),
              Traductor.T("Cancelar reclamo"),
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Warning);

      if (respuesta != DialogResult.Yes)
        return;

      int estadoId =
          ObtenerEstadoId("Cancelado");

      if (estadoId == 0)
        return;

      reclamo.EstadoId =
          estadoId;

      reclamoService.Actualizar(
          reclamo);

      RegistrarLog(
          "CANCELAR",
          "Se canceló el reclamo desde presupuesto.");

      MessageBox.Show(
          Traductor.T("El reclamo fue cancelado."),
          Traductor.T("Presupuesto"),
          MessageBoxButtons.OK,
          MessageBoxIcon.Information);

      CargarReclamo();
      CargarPresupuesto();
    }


    // =========================================================
    // BOTÓN CANCELAR
    // =========================================================

    private void ConfigurarBotonCancelar()
    {
      btnCancelarReclamo.Visible =
          !EsEstado("Cancelado") &&
          !EsEstado("Finalizado") &&
          SesionActual.TienePermiso(
              Permisos.PRESUPUESTO_CANCELAR_RECLAMO);
    }


    // =========================================================
    // PDF
    // =========================================================

    private void btnSeleccionarArchivo_Click(
        object sender,
        EventArgs e)
    {
      if (!SesionActual.TienePermiso(
          Permisos.PRESUPUESTO_ADJUNTAR_ARCHIVO))
      {
        MessageBox.Show(
            Traductor.T("No tiene permisos para adjuntar el PDF."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      using (OpenFileDialog dialogo =
          new OpenFileDialog())
      {
        dialogo.Title =
            Traductor.T("Seleccionar presupuesto PDF");

        dialogo.Filter =
            Traductor.T("Archivo PDF|*.pdf");

        dialogo.Multiselect = false;

        if (dialogo.ShowDialog() !=
            DialogResult.OK)
        {
          return;
        }

        rutaPdfSeleccionado =
            dialogo.FileName;

        lblArchivoSeleccionado.Text =
            Path.GetFileName(
                rutaPdfSeleccionado);
      }
    }


    // =========================================================
    // VALIDACIÓN
    // =========================================================

    private bool ValidarDatos()
    {
      // -------------------------------------------------------
      // PROVEEDOR
      // -------------------------------------------------------

      if (cmbProveedor.SelectedValue == null)
      {
        MessageBox.Show(
            Traductor.T("Debe seleccionar un proveedor."),
            Traductor.T("Validación"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        cmbProveedor.Focus();

        return false;
      }


      // -------------------------------------------------------
      // MONTO
      // -------------------------------------------------------

      if (!SesionActual.TienePermiso(
          Permisos.PRESUPUESTO_CARGAR_MONTO))
      {
        MessageBox.Show(
            Traductor.T("No tiene permisos para cargar el monto."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return false;
      }

      decimal montoEstimado;

      if (!decimal.TryParse(
          txtMontoEstimado.Text.Trim(),
          out montoEstimado))
      {
        MessageBox.Show(
            Traductor.T("El monto estimado no es válido."),
            Traductor.T("Validación"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtMontoEstimado.Focus();

        return false;
      }

      if (montoEstimado <= 0)
      {
        MessageBox.Show(
            Traductor.T("El monto estimado debe ser mayor a cero."),
            Traductor.T("Validación"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtMontoEstimado.Focus();

        return false;
      }


      // -------------------------------------------------------
      // PDF
      // -------------------------------------------------------

      if (string.IsNullOrWhiteSpace(
          ObtenerArchivoPdf()))
      {
        MessageBox.Show(
            Traductor.T("Debe adjuntar el presupuesto en PDF."),
            Traductor.T("Validación"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return false;
      }


      // -------------------------------------------------------
      // MONTO FINAL
      // -------------------------------------------------------

      if (!string.IsNullOrWhiteSpace(
          txtMontoFinal.Text))
      {
        decimal montoFinal;

        if (!decimal.TryParse(
            txtMontoFinal.Text.Trim(),
            out montoFinal))
        {
          MessageBox.Show(
              Traductor.T("El monto final no es válido."),
              Traductor.T("Validación"),
              MessageBoxButtons.OK,
              MessageBoxIcon.Warning);

          txtMontoFinal.Focus();

          return false;
        }

        if (montoFinal < 0)
        {
          MessageBox.Show(
              Traductor.T("El monto final no puede ser negativo."),
              Traductor.T("Validación"),
              MessageBoxButtons.OK,
              MessageBoxIcon.Warning);

          txtMontoFinal.Focus();

          return false;
        }
      }

      return true;
    }


    // =========================================================
    // DATOS
    // =========================================================

    private Presupuesto ObtenerDatosFormulario()
    {
      decimal montoEstimado;

      decimal.TryParse(
          txtMontoEstimado.Text.Trim(),
          out montoEstimado);

      decimal? montoFinal = null;

      if (!string.IsNullOrWhiteSpace(
          txtMontoFinal.Text))
      {
        decimal valorFinal;

        if (decimal.TryParse(
            txtMontoFinal.Text.Trim(),
            out valorFinal))
        {
          montoFinal = valorFinal;
        }
      }

      return new Presupuesto
      {
        ReclamoId = reclamoId,

        ProveedorId =
            Convert.ToInt32(
                cmbProveedor.SelectedValue),

        MontoEstimado =
            montoEstimado,

        MontoFinal =
            montoFinal,

        ArchivoPdf =
            ObtenerArchivoPdf(),

        UsuarioId =
            SesionActual.Usuario.UsuarioId,

        DigitoVerificador = ""
      };
    }


    private void MostrarErrorGuardado(Exception ex)
    {
      MessageBox.Show(
          Traductor.T("No se pudo guardar el presupuesto.") +
          "\n\n" +
          Traductor.T(ex.Message),
          Traductor.T("Error"),
          MessageBoxButtons.OK,
          MessageBoxIcon.Error);
    }

    private string ObtenerArchivoPdf()
    {
      if (!string.IsNullOrWhiteSpace(
          rutaPdfSeleccionado))
      {
        return rutaPdfSeleccionado;
      }

      if (presupuesto != null)
      {
        return presupuesto.ArchivoPdf;
      }

      return null;
    }


    // =========================================================
    // ESTADO
    // =========================================================

    private bool EsEstado(string estado)
    {
      if (reclamo == null ||
          string.IsNullOrWhiteSpace(
              reclamo.Estado))
      {
        return false;
      }

      return reclamo.Estado.Trim()
          .Equals(
              estado,
              StringComparison.OrdinalIgnoreCase);
    }


    private int ObtenerEstadoId(
        string valor)
    {
      DetalleClaveValorService service =
          new DetalleClaveValorService();

      var estados =
          service.ObtenerPorTabla("ESTADO");

      var estado =
          estados.Find(
              x => x.Valor.Trim()
                  .Equals(
                      valor,
                      StringComparison.OrdinalIgnoreCase));

      if (estado == null)
      {
        MessageBox.Show(
            Traductor.T("No se encontró el estado '") +
            valor +
            "'.",
            Traductor.T("Error"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);

        return 0;
      }

      return estado.DetalleId;
    }


    // =========================================================
    // LOG
    // =========================================================

    private void RegistrarLog(
        string accion,
        string descripcion)
    {
      LogSistemaService logService =
          new LogSistemaService();

      logService.Registrar(
          SesionActual.Usuario.UsuarioId,
          accion,
          "PRESUPUESTOS",
          reclamoId,
          descripcion);
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

    private void lblEstado_Click(object sender, EventArgs e)
    {

    }
  }
}
