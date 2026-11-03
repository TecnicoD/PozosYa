using Negocio.Models;
using Negocio.Security;
using Negocio.Services;
using System;
using System.IO;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using PY.Presentacion.Idiomas;

namespace PY.Presentacion.Forms
{
  public partial class FormVerificacion : Form
  {
    // =========================================================
    // VARIABLES
    // =========================================================

    private readonly int reclamoId;

    private ReclamoDetalleDTO reclamo;

    private readonly ReclamoService reclamoService =
        new ReclamoService();

    private readonly ArchivoService archivoService =
    new ArchivoService();

    private string rutaArchivoSeleccionado;

    private List<Archivo> evidencias =
    new List<Archivo>();

    // =========================================================
    // CONSTRUCTOR
    // =========================================================

    public FormVerificacion(int reclamoId)
    {
      InitializeComponent();
      Traductor.Traducir(this);

      this.reclamoId = reclamoId;
    }


    // =========================================================
    // LOAD
    // =========================================================

    private void FormVerificacion_Load(
        object sender,
        EventArgs e)
    {
      if (!ValidarAcceso())
        return;

      CargarReclamo();
    }


    // =========================================================
    // SEGURIDAD
    // =========================================================

    private bool ValidarAcceso()
    {
      if (!SesionActual.TienePermiso(
          Permisos.VERIFICACION_VER))
      {
        MessageBox.Show(
            Traductor.T("No tiene permisos para acceder a la verificación."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        Close();

        return false;
      }

      return true;
    }


    // =========================================================
    // CARGAR RECLAMO
    // =========================================================

    private void CargarReclamo()
    {
      reclamo = reclamoService.ObtenerPorId(reclamoId);

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

      // -------------------------------------------------------
      // CABECERA
      // -------------------------------------------------------

      lblNumeroReclamo.Text =
          Traductor.T("Detalle del reclamo #") +
          reclamo.NumeroReclamo;

      lblEstado.Text =
          Traductor.T("Estado actual: ") +
          reclamo.Estado;


      // -------------------------------------------------------
      // DATOS
      // -------------------------------------------------------

      lblFecha.Text =
          Traductor.T("Fecha de alta: ") +
          reclamo.FechaCreacion.ToString(
              "dd/MM/yyyy HH:mm");

      lblVecino.Text =
          Traductor.T("Vecino: ") +
          reclamo.Vecino;

      lblDni.Text =
          Traductor.T("DNI: ") +
          reclamo.DNI;

      lblDireccion.Text =
          Traductor.T("Dirección: ") +
          reclamo.Direccion;

      lblBarrio.Text =
          Traductor.T("Barrio: ") +
          reclamo.Barrio;

      lblTipoDanio.Text =
          Traductor.T("Tipo de daño: ") +
          reclamo.TipoDanio;

      lblSeveridad.Text =
          Traductor.T("Severidad: ") +
          reclamo.Severidad;

      txtDescripcion.Text =
          reclamo.Descripcion;


      // -------------------------------------------------------
      // ACCIONES
      // -------------------------------------------------------

      ConfigurarAcciones();
      CargarEvidencias();
    }


    private void CargarEvidencias()
    {
      lstEvidencias.Items.Clear();

      evidencias =
          archivoService.ObtenerPorReclamo(reclamoId);

      foreach (Archivo archivo in evidencias)
      {
        lstEvidencias.Items.Add(
            archivo.NombreOriginal);
      }

      if (evidencias.Count == 0)
      {
        lstEvidencias.Items.Add(
            Traductor.T("No hay evidencias cargadas."));
      }
    }



    // =========================================================
    // ACCIONES SEGÚN ESTADO
    // =========================================================

    private void ConfigurarAcciones()
    {
      // Ocultar todo primero
      btnVerificacion.Visible = false;
      btnIniciarObra.Visible = false;
      btnFinalizar.Visible = false;
      btnCancelarReclamo.Visible = false;

      btnSeleccionarArchivo.Visible = false;

      lblMensajeEstado.Text = "";


      if (reclamo == null)
        return;


      if (EsEstado("Cancelado"))
      {
        lblMensajeEstado.Text =
            Traductor.T("El reclamo fue cancelado.");

        return;
      }


      // =======================================================
      // CANCELAR (cualquier estado salvo Finalizado)
      // =======================================================
      if (!EsEstado("Finalizado") &&
          SesionActual.TienePermiso(
              Permisos.VERIFICACION_CANCELAR_RECLAMO))
      {
        btnCancelarReclamo.Visible = true;
      }


      // =======================================================
      // INICIADO
      // =======================================================
      if (EsEstado("Iniciado"))
      {
        if (SesionActual.TienePermiso(
            Permisos.VERIFICACION_MARCAR_INICIADO))
        {
          btnVerificacion.Visible = true;
        }

        if (SesionActual.TienePermiso(
            Permisos.VERIFICACION_SUBIR_EVIDENCIA))
        {
          btnSeleccionarArchivo.Visible = true;
        }

        return;
      }


      // =======================================================
      // VERIFICADO
      // =======================================================

      if (EsEstado("Verificado"))
      {
        lblMensajeEstado.Text =
            Traductor.T("El reclamo fue verificado correctamente.");

        return;
      }


      // =======================================================
      // PRESUPUESTADO
      // =======================================================

      if (EsEstado("Presupuestado"))
      {
        lblMensajeEstado.Text =
            Traductor.T("El reclamo ya fue presupuestado.");

        return;
      }


      // =======================================================
      // PRIORIZADO
      // =======================================================

      if (EsEstado("Priorizado"))
      {
        if (SesionActual.TienePermiso(
            Permisos.VERIFICACION_MARCAR_EN_EJECUCION))
        {
          btnIniciarObra.Visible = true;
        }

        if (SesionActual.TienePermiso(
            Permisos.VERIFICACION_SUBIR_EVIDENCIA))
        {
          btnSeleccionarArchivo.Visible = true;
        }

        return;
      }


      // =======================================================
      // EN EJECUCIÓN
      // =======================================================

      if (EsEstado("En Ejecución"))
      {
        if (SesionActual.TienePermiso(
            Permisos.VERIFICACION_MARCAR_FINALIZADO))
        {
          btnFinalizar.Visible = true;
        }

        if (SesionActual.TienePermiso(
            Permisos.VERIFICACION_SUBIR_EVIDENCIA))
        {
          btnSeleccionarArchivo.Visible = true;
        }

        return;
      }


      // =======================================================
      // FINALIZADO
      // =======================================================

      if (EsEstado("Finalizado"))
      {
        lblMensajeEstado.Text =
            Traductor.T("La obra ya fue finalizada.");

        return;
      }


      lblMensajeEstado.Text =
          Traductor.T("No hay acciones disponibles para el estado actual.");
    }


    // =========================================================
    // COMPARAR ESTADO
    // =========================================================

    private bool EsEstado(string estado)
    {
      if (string.IsNullOrWhiteSpace(reclamo.Estado))
        return false;

      return reclamo.Estado.Trim()
          .Equals(
              estado,
              StringComparison.OrdinalIgnoreCase);
    }


    // =========================================================
    // VERIFICAR
    // =========================================================

    private void btnVerificacion_Click(
        object sender,
        EventArgs e)
    {
      CambiarEstado(
          "Verificado",
          Permisos.VERIFICACION_MARCAR_INICIADO,
          "¿Está seguro de que desea verificar este reclamo?");
    }


    // =========================================================
    // INICIAR OBRA
    // =========================================================

    private void btnIniciarObra_Click(
        object sender,
        EventArgs e)
    {
      CambiarEstado(
          "En Ejecución",
          Permisos.VERIFICACION_MARCAR_EN_EJECUCION,
          "¿Está seguro de que desea registrar el inicio de la obra?");
    }


    // =========================================================
    // FINALIZAR OBRA
    // =========================================================

    private void btnFinalizar_Click(
        object sender,
        EventArgs e)
    {
      CambiarEstado(
          "Finalizado",
          Permisos.VERIFICACION_MARCAR_FINALIZADO,
          "¿Está seguro de que desea finalizar la obra?");
    }


    // =========================================================
    // CAMBIAR ESTADO
    // =========================================================

    private void CambiarEstado(
        string estadoDestino,
        string permiso,
        string mensaje)
    {
      // -------------------------------------------------------
      // SEGURIDAD
      // -------------------------------------------------------

      if (!SesionActual.TienePermiso(permiso))
      {
        MessageBox.Show(
            Traductor.T("No tiene permisos para realizar esta acción."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }


      // -------------------------------------------------------
      // CONFIRMACIÓN
      // -------------------------------------------------------

      DialogResult respuesta =
          MessageBox.Show(
              Traductor.T(mensaje),
              Traductor.T("Confirmación"),
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Question);

      if (respuesta != DialogResult.Yes)
        return;


      // -------------------------------------------------------
      // BUSCAR ESTADO
      // -------------------------------------------------------

      DetalleClaveValorService claveValorService =
          new DetalleClaveValorService();

      var estados =
          claveValorService.ObtenerPorTabla("ESTADO");

      var estado =
          estados.Find(
              x => x.Valor.Trim()
                  .Equals(
                      estadoDestino,
                      StringComparison.OrdinalIgnoreCase));

      if (estado == null)
      {
        MessageBox.Show(
            Traductor.T("No se encontró el estado '") +
            estadoDestino +
            "'.",
            Traductor.T("Error"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);

        return;
      }


      // -------------------------------------------------------
      // CAMBIAR ESTADO
      // -------------------------------------------------------

      reclamo.EstadoId =
          estado.DetalleId;

      reclamoService.Actualizar(reclamo);


      // -------------------------------------------------------
      // LOG
      // -------------------------------------------------------

      RegistrarLog(
          "MODIFICAR",
          "El reclamo pasó al estado '" +
          estadoDestino +
          "'.");


      // -------------------------------------------------------
      // RECARGAR
      // -------------------------------------------------------

      CargarReclamo();
    }


    // =========================================================
    // SELECCIONAR ARCHIVO
    // =========================================================

    private void btnSeleccionarArchivo_Click(
    object sender,
    EventArgs e)
    {
      if (!SesionActual.TienePermiso(
          Permisos.VERIFICACION_SUBIR_EVIDENCIA))
      {
        MessageBox.Show(
            Traductor.T("No tiene permisos para subir evidencia."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      using (OpenFileDialog dialogo =
          new OpenFileDialog())
      {
        dialogo.Title =
            Traductor.T("Seleccionar evidencia fotográfica");

        dialogo.Filter =
            Traductor.T("Imágenes|*.jpg;*.jpeg;*.png");

        dialogo.Multiselect = false;

        if (dialogo.ShowDialog() != DialogResult.OK)
          return;

        rutaArchivoSeleccionado =
            dialogo.FileName;

        lblArchivoSeleccionado.Text =
            Path.GetFileName(
                rutaArchivoSeleccionado);

        ArchivoService service =
            new ArchivoService();

        try
        {
          int archivoId =
              service.AgregarEvidencia(
                  reclamoId,
                  rutaArchivoSeleccionado,
                  SesionActual.Usuario.UsuarioId);

          RegistrarLog(
              "CREAR",
              "Se agregó evidencia al reclamo. Archivo: " +
              Path.GetFileName(rutaArchivoSeleccionado));

          MessageBox.Show(
              Traductor.T("La evidencia se cargó correctamente."),
              Traductor.T("Evidencia"),
              MessageBoxButtons.OK,
              MessageBoxIcon.Information);

          rutaArchivoSeleccionado = null;

          lblArchivoSeleccionado.Text =
              Traductor.T("Ningún archivo seleccionado");

          CargarEvidencias();
        }
        catch (Exception ex)
        {
          MessageBox.Show(
              Traductor.T(ex.Message),
              Traductor.T("Error al cargar evidencia"),
              MessageBoxButtons.OK,
              MessageBoxIcon.Error);
        }
      }
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
          "VERIFICACION",
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

    private void btnAbrirEvidencia_Click(
    object sender,
    EventArgs e)
    {
      if (lstEvidencias.SelectedIndex < 0 ||
          lstEvidencias.SelectedIndex >= evidencias.Count)
      {
        MessageBox.Show(
            Traductor.T("Debe seleccionar una evidencia."),
            Traductor.T("Evidencia"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);

        return;
      }

      Archivo archivo =
          evidencias[lstEvidencias.SelectedIndex];

      string rutaCompleta =
          Path.Combine(
              AppDomain.CurrentDomain.BaseDirectory,
              archivo.Ubicacion);

      if (!File.Exists(rutaCompleta))
      {
        MessageBox.Show(
            Traductor.T("No se encontró el archivo de evidencia."),
            Traductor.T("Error"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);

        return;
      }

      try
      {
        System.Diagnostics.ProcessStartInfo inicio =
            new System.Diagnostics.ProcessStartInfo
            {
              FileName = rutaCompleta,
              UseShellExecute = true
            };

        System.Diagnostics.Process.Start(inicio);
      }
      catch (Exception ex)
      {
        MessageBox.Show(
            Traductor.T("No se pudo abrir la imagen.\n\n") +
            Traductor.T(ex.Message),
            Traductor.T("Error"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
      }
    }

    private void btnCancelarReclamo_Click(
    object sender,
    EventArgs e)
    {
      CambiarEstado(
          "Cancelado",
          Permisos.VERIFICACION_CANCELAR_RECLAMO,
          "¿Está seguro de que desea cancelar el reclamo?");
    }
  }
}
