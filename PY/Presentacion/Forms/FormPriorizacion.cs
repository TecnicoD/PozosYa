using Negocio.Models;
using Negocio.Security;
using Negocio.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PY.Presentacion.Idiomas;

namespace PY.Presentacion.Forms
{
  public partial class FormPriorizacion : Form
  {
    // =========================================================
    // VARIABLES
    // =========================================================
    // Orden de prioridad de la partida: primero los reclamos que
    // ya estaban asignados y al final los nuevos tildados en la grilla.
    private readonly List<ReclamoPriorizacionDTO> ordenPrioridad = new List<ReclamoPriorizacionDTO>();

    private readonly PartidaPresupuestariaService partidaService =
        new PartidaPresupuestariaService();

    private readonly ReclamoService reclamoService =
        new ReclamoService();

    private PartidaPresupuestaria partidaSeleccionada;


    private readonly ReclamoPartidaService reclamoPartidaService =
    new ReclamoPartidaService();

    private readonly LogSistemaService logService =
        new LogSistemaService();

    // =========================================================
    // CONSTRUCTOR
    // =========================================================

    public FormPriorizacion()
    {
      InitializeComponent();
      Traductor.Traducir(this);
    }


    // =========================================================
    // LOAD
    // =========================================================

    private void FormPriorizacion_Load(
        object sender,
        EventArgs e)
    {
      if (!ValidarAcceso())
        return;

      ConfigurarPermisos();
      CargarPartidas();
      CargarFiltros();
      LimpiarFondos();
    }


    // Sincroniza la lista de orden con los reclamos tildados en la
    // grilla, sin perder el orden que ya armó el usuario.
    private void ActualizarOrdenYTotal()
    {
      List<ReclamoPriorizacionDTO> tildados =
          new List<ReclamoPriorizacionDTO>();

      foreach (DataGridViewRow fila in dgvReclamos.Rows)
      {
        if (fila.IsNewRow)
          continue;

        bool seleccionado =
            Convert.ToBoolean(
                fila.Cells["colSeleccionar"].Value ?? false);

        if (!seleccionado)
          continue;

        ReclamoPriorizacionDTO reclamo =
            fila.DataBoundItem as ReclamoPriorizacionDTO;

        if (reclamo != null)
          tildados.Add(reclamo);
      }

      // Quitar los nuevos que se destildaron
      // (los ya asignados nunca se quitan).
      ordenPrioridad.RemoveAll(
          r => !r.YaAsignado &&
               !tildados.Exists(t => t.ReclamoId == r.ReclamoId));

      // Agregar al final los nuevos tildados.
      foreach (ReclamoPriorizacionDTO reclamo in tildados)
      {
        if (!ordenPrioridad.Exists(r => r.ReclamoId == reclamo.ReclamoId))
          ordenPrioridad.Add(reclamo);
      }

      lblTotalSeleccionado.Text =
          Traductor.T("Total seleccionado: $") +
          CalcularTotalNuevos().ToString("N2");

      ActualizarListaOrden();
    }

    // Solo los reclamos nuevos consumen fondos de la partida;
    // los ya asignados ya están en el monto comprometido.
    private decimal CalcularTotalNuevos()
    {
      decimal total = 0;

      foreach (ReclamoPriorizacionDTO reclamo in ordenPrioridad)
      {
        if (!reclamo.YaAsignado)
          total += reclamo.MontoPresupuesto;
      }

      return total;
    }

    private void ActualizarListaOrden()
    {
      lstOrdenPrioridad.Items.Clear();

      for (int i = 0;
           i < ordenPrioridad.Count;
           i++)
      {
        ReclamoPriorizacionDTO reclamo =
            ordenPrioridad[i];

        lstOrdenPrioridad.Items.Add(
            (i + 1) +
            ". " +
            reclamo.NumeroReclamo +
            " | $" +
            reclamo.MontoPresupuesto.ToString("N2") +
            (reclamo.YaAsignado ? "" : Traductor.T(" (nuevo)")));
      }

      if (lstOrdenPrioridad.Items.Count > 0)
        lstOrdenPrioridad.SelectedIndex = 0;
    }

    // =========================================================
    // SEGURIDAD
    // =========================================================

    private bool ValidarAcceso()
    {
      if (!SesionActual.TienePermiso(
          Permisos.PRIORIZACION_VER))
      {
        MessageBox.Show(
            Traductor.T("No tiene permisos para acceder a la priorización."),
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
      cmbPartida.Enabled =
          SesionActual.TienePermiso(
              Permisos.PRIORIZACION_SELECCIONAR_TRIMESTRE);

      btnBuscar.Enabled =
          SesionActual.TienePermiso(
              Permisos.PRIORIZACION_BUSCAR);

      btnLimpiar.Enabled = true;

      dgvReclamos.Enabled =
          SesionActual.TienePermiso(
              Permisos.PRIORIZACION_SELECCIONAR_RECLAMOS);

      btnSubir.Enabled =
          SesionActual.TienePermiso(
              Permisos.PRIORIZACION_ORDENAR);

      btnBajar.Enabled =
          SesionActual.TienePermiso(
              Permisos.PRIORIZACION_ORDENAR);

      btnProcesar.Enabled =
          SesionActual.TienePermiso(
              Permisos.PRIORIZACION_PROCESAR);
    }


    // =========================================================
    // PARTIDAS
    // =========================================================

    private void CargarPartidas()
    {
      List<PartidaPresupuestaria> partidas =
          partidaService.ObtenerTodasDisponibles();

      cmbPartida.DataSource = partidas;

      cmbPartida.DisplayMember =
          "TextoPartida";

      cmbPartida.ValueMember =
          "PartidaPresupuestariaId";

      cmbPartida.SelectedIndex = -1;
    }


    // =========================================================
    // FILTROS
    // =========================================================

    private void CargarFiltros()
    {
      cmbEstado.Items.Clear();

      cmbEstado.Items.Add(Traductor.T("Todos"));
      cmbEstado.Items.Add(Traductor.T("Presupuestado"));

      cmbEstado.SelectedIndex = 1;


      cmbLocalidad.Items.Clear();

      cmbLocalidad.Items.Add(Traductor.T("Todos"));
      cmbLocalidad.SelectedIndex = 0;
    }


    // =========================================================
    // PARTIDA SELECCIONADA
    // =========================================================

    private void cmbPartida_SelectedIndexChanged(
        object sender,
        EventArgs e)
    {
      if (cmbPartida.SelectedIndex < 0)
      {
        partidaSeleccionada = null;

        LimpiarFondos();
        dgvReclamos.DataSource = null;
        CargarAsignados();

        return;
      }

      partidaSeleccionada =
          cmbPartida.SelectedItem
          as PartidaPresupuestaria;

      if (partidaSeleccionada == null)
        return;

      MostrarFondos();
      CargarReclamos();
      CargarAsignados();
    }


    // Carga en la lista de orden los reclamos que ya están
    // priorizados en la partida, respetando su prioridad actual.
    private void CargarAsignados()
    {
      ordenPrioridad.Clear();

      if (partidaSeleccionada != null)
      {
        ordenPrioridad.AddRange(
            reclamoPartidaService.ObtenerPriorizadosPorPartida(
                partidaSeleccionada.PartidaPresupuestariaId));
      }

      ActualizarOrdenYTotal();
    }


    // =========================================================
    // MOSTRAR FONDOS
    // =========================================================

    private void MostrarFondos()
    {
      lblPartidaSeleccionada.Text =
          Traductor.T("Partida: ") +
          partidaSeleccionada.Anio +
          Traductor.T(" - Trimestre ") +
          partidaSeleccionada.Trimestre;

      lblMontoAsignado.Text =
          Traductor.T("Asignado: $") +
          partidaSeleccionada.MontoAsignado
              .ToString("N2");

      lblMontoComprometido.Text =
          Traductor.T("Comprometido: $") +
          partidaSeleccionada.MontoComprometido
              .ToString("N2");

      lblMontoEjecutado.Text =
          Traductor.T("Ejecutado: $") +
          partidaSeleccionada.MontoEjecutado
              .ToString("N2");

      decimal fondosDisponibles =
          partidaSeleccionada.MontoAsignado -
          partidaSeleccionada.MontoComprometido;

      lblFondosDisponibles.Text =
          Traductor.T("Disponible: $") +
          fondosDisponibles.ToString("N2");
    }


    private void LimpiarFondos()
    {
      lblPartidaSeleccionada.Text =
          Traductor.T("Partida: -");

      lblMontoAsignado.Text =
          Traductor.T("Asignado: $0,00");

      lblMontoComprometido.Text =
          Traductor.T("Comprometido: $0,00");

      lblMontoEjecutado.Text =
          Traductor.T("Ejecutado: $0,00");

      lblFondosDisponibles.Text =
          Traductor.T("Disponible: $0,00");
    }


    // =========================================================
    // RECLAMOS
    // =========================================================

    private void CargarReclamos()
    {
      List<ReclamoPriorizacionDTO> lista =
          reclamoService.ObtenerParaPriorizacion();

      dgvReclamos.AutoGenerateColumns = false;

      dgvReclamos.DataSource = lista;

      // La grilla se recargó sin tildes: se quitan los nuevos
      // de la lista de orden y quedan solo los ya asignados.
      ActualizarOrdenYTotal();
    }


    // =========================================================
    // BOTONES TEMPORALES
    // =========================================================

    private void btnBuscar_Click(
        object sender,
        EventArgs e)
    {
      if (!SesionActual.TienePermiso(
          Permisos.PRIORIZACION_BUSCAR))
      {
        MessageBox.Show(
            Traductor.T("No tiene permisos para buscar reclamos."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      CargarReclamos();
    }

    private void btnLimpiar_Click(
    object sender,
    EventArgs e)
    {
      cmbEstado.SelectedIndex = 1;
      cmbLocalidad.SelectedIndex = 0;

      dgvReclamos.DataSource = null;

      // Quedan en la lista solo los ya asignados a la partida.
      ActualizarOrdenYTotal();
    }


    private void dgvReclamos_CellContentClick(
    object sender,
    DataGridViewCellEventArgs e)
    {
      if (e.RowIndex < 0)
        return;

      if (e.ColumnIndex !=
          dgvReclamos.Columns["colSeleccionar"].Index)
        return;

      if (!SesionActual.TienePermiso(
          Permisos.PRIORIZACION_SELECCIONAR_RECLAMOS))
      {
        MessageBox.Show(
            Traductor.T("No tiene permisos para seleccionar reclamos."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      DataGridViewRow fila =
          dgvReclamos.Rows[e.RowIndex];

      bool seleccionado =
          Convert.ToBoolean(
              fila.Cells["colSeleccionar"].Value ?? false);

      fila.Cells["colSeleccionar"].Value =
          !seleccionado;

      ActualizarOrdenYTotal();
    }

    private void btnSubir_Click(
    object sender,
    EventArgs e)
    {
      if (!SesionActual.TienePermiso(
          Permisos.PRIORIZACION_ORDENAR))
      {
        MessageBox.Show(
            Traductor.T("No tiene permisos para ordenar la priorización."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      int indice =
          lstOrdenPrioridad.SelectedIndex;

      if (indice <= 0)
        return;

      ReclamoPriorizacionDTO temporal =
          ordenPrioridad[indice];

      ordenPrioridad[indice] =
          ordenPrioridad[indice - 1];

      ordenPrioridad[indice - 1] =
          temporal;

      ActualizarListaOrden();

      lstOrdenPrioridad.SelectedIndex =
          indice - 1;
    }

    private void btnBajar_Click(object sender, EventArgs e)
    {
      if (!SesionActual.TienePermiso(
          Permisos.PRIORIZACION_ORDENAR))
      {
        MessageBox.Show(
            Traductor.T("No tiene permisos para ordenar la priorización."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      int indice =
          lstOrdenPrioridad.SelectedIndex;

      if (indice < 0 ||
          indice >= ordenPrioridad.Count - 1)
        return;

      ReclamoPriorizacionDTO temporal =
          ordenPrioridad[indice];

      ordenPrioridad[indice] =
          ordenPrioridad[indice + 1];

      ordenPrioridad[indice + 1] =
          temporal;

      ActualizarListaOrden();

      lstOrdenPrioridad.SelectedIndex =
          indice + 1;
    }

    private void btnProcesar_Click(
    object sender,
    EventArgs e)
    {
      // =========================================================
      // PERMISO
      // =========================================================

      if (!SesionActual.TienePermiso(
          Permisos.PRIORIZACION_PROCESAR))
      {
        MessageBox.Show(
            Traductor.T("No tiene permisos para procesar la priorización."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }


      // =========================================================
      // PARTIDA
      // =========================================================

      if (partidaSeleccionada == null)
      {
        MessageBox.Show(
            Traductor.T("Debe seleccionar una partida presupuestaria."),
            Traductor.T("Priorización"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }


      // =========================================================
      // RECLAMOS
      // =========================================================

      if (ordenPrioridad.Count == 0)
      {
        MessageBox.Show(
            Traductor.T("Debe seleccionar al menos un reclamo."),
            Traductor.T("Priorización"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      List<ReclamoPriorizacionDTO> nuevos =
          ordenPrioridad.FindAll(r => !r.YaAsignado);

      int cantidadAsignados =
          ordenPrioridad.Count - nuevos.Count;


      // =========================================================
      // REVISAR ESTADO (solo los nuevos)
      // =========================================================

      foreach (ReclamoPriorizacionDTO reclamoSeleccionado
          in nuevos)
      {
        ReclamoDetalleDTO reclamoActual =
            reclamoService.ObtenerPorId(
                reclamoSeleccionado.ReclamoId);

        if (reclamoActual == null)
        {
          MessageBox.Show(
              Traductor.T("No se encontró uno de los reclamos seleccionados."),
              Traductor.T("Error"),
              MessageBoxButtons.OK,
              MessageBoxIcon.Error);

          return;
        }

        if (!reclamoActual.Estado.Equals(
            "Presupuestado",
            StringComparison.OrdinalIgnoreCase))
        {
          MessageBox.Show(
              Traductor.T("El reclamo ") +
              reclamoActual.NumeroReclamo +
              Traductor.T(" ya no está en estado PRESUPUESTADO."),
              Traductor.T("Priorización"),
              MessageBoxButtons.OK,
              MessageBoxIcon.Warning);

          return;
        }
      }


      // =========================================================
      // TOTAL (solo los nuevos consumen fondos)
      // =========================================================

      decimal totalSeleccionado =
          CalcularTotalNuevos();


      // =========================================================
      // FONDOS
      // =========================================================

      decimal fondosDisponibles =
          partidaSeleccionada.MontoAsignado -
          partidaSeleccionada.MontoComprometido;

      if (totalSeleccionado > fondosDisponibles)
      {
        MessageBox.Show(
            Traductor.T("Los fondos disponibles no son suficientes.\n\n") +
            Traductor.T("Total seleccionado: $") +
            totalSeleccionado.ToString("N2") +
            Traductor.T("\nFondos disponibles: $") +
            fondosDisponibles.ToString("N2"),
            Traductor.T("Saldo insuficiente"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }


      // =========================================================
      // CONFIRMACIÓN
      // =========================================================

      DialogResult respuesta =
          MessageBox.Show(
              Traductor.T("¿Está seguro de procesar la priorización?\n\n") +
              Traductor.T("Partida: ") +
              partidaSeleccionada.TextoPartida +
              Traductor.T("\nReclamos nuevos: ") +
              nuevos.Count +
              Traductor.T("\nReclamos ya asignados (se actualiza su orden): ") +
              cantidadAsignados +
              Traductor.T("\nMonto total nuevos: $") +
              totalSeleccionado.ToString("N2") +
              Traductor.T("\nFondos disponibles: $") +
              fondosDisponibles.ToString("N2"),
              Traductor.T("Confirmar priorización"),
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Question);

      if (respuesta != DialogResult.Yes)
        return;


      // =========================================================
      // PROCESAR
      // =========================================================

      try
      {
        for (int i = 0;
             i < ordenPrioridad.Count;
             i++)
        {
          ReclamoPriorizacionDTO seleccionado =
              ordenPrioridad[i];

          // -----------------------------------------
          // YA ASIGNADO: SOLO SE ACTUALIZA EL ORDEN
          // -----------------------------------------

          if (seleccionado.YaAsignado)
          {
            reclamoPartidaService.ActualizarPrioridad(
                seleccionado.ReclamoId,
                partidaSeleccionada.PartidaPresupuestariaId,
                i + 1);

            continue;
          }

          // -----------------------------------------
          // RELACIÓN RECLAMO - PARTIDA
          // -----------------------------------------

          reclamoPartidaService.Agregar(
              seleccionado.ReclamoId,
              partidaSeleccionada.PartidaPresupuestariaId,
              seleccionado.MontoPresupuesto,
              SesionActual.Usuario.UsuarioId,
              i + 1);


          // -----------------------------------------
          // CAMBIAR ESTADO
          // -----------------------------------------

          ReclamoDetalleDTO reclamo =
              reclamoService.ObtenerPorId(
                  seleccionado.ReclamoId);

          reclamo.EstadoId =
              ObtenerEstadoId("Priorizado");

          reclamoService.Actualizar(
              reclamo);


          // -----------------------------------------
          // LOG
          // -----------------------------------------

          logService.Registrar(
              SesionActual.Usuario.UsuarioId,
              "PRIORIZAR",
              "RECLAMOS",
              seleccionado.ReclamoId,
              "El reclamo " +
              seleccionado.NumeroReclamo +
              " fue priorizado. Partida: " +
              partidaSeleccionada.TextoPartida +
              ". Prioridad: " +
              (i + 1));
        }


        // =======================================================
        // ACTUALIZAR COMPROMETIDO
        // =======================================================

        if (nuevos.Count > 0)
        {
          partidaService.AgregarComprometido(
              partidaSeleccionada.PartidaPresupuestariaId,
              totalSeleccionado);
        }

        if (cantidadAsignados > 0)
        {
          logService.Registrar(
              SesionActual.Usuario.UsuarioId,
              "PRIORIZAR",
              "PARTIDAS_PRESUPUESTARIAS",
              partidaSeleccionada.PartidaPresupuestariaId,
              "Se actualizó el orden de priorización de la partida " +
              partidaSeleccionada.TextoPartida + ".");
        }


        // =======================================================
        // MENSAJE
        // =======================================================

        MessageBox.Show(
            Traductor.T("La priorización fue procesada correctamente."),
            Traductor.T("Priorización"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);


        // =======================================================
        // RECARGAR
        // =======================================================

        // Se recargan las partidas para que el monto
        // comprometido quede actualizado en el combo.
        dgvReclamos.DataSource = null;

        CargarPartidas();

        LimpiarFondos();
      }
      catch (Exception ex)
      {
        MessageBox.Show(
            Traductor.T("Ocurrió un error al procesar la priorización.\n\n") +
            Traductor.T(ex.Message),
            Traductor.T("Error"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
      }
    }


    private int ObtenerEstadoId(string valor)
    {
      DetalleClaveValorService service =
          new DetalleClaveValorService();

      var estados =
          service.ObtenerPorTabla("ESTADO");

      var estado =
          estados.Find(
              x => x.Clave.Equals(
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

  }
}
