using Negocio.Models;
using Negocio.Security;
using Negocio.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PY.Presentacion.Idiomas;

namespace PY.Presentacion.Forms
{
  public partial class FormPartidas : Form
  {
    // =========================================================
    // VARIABLES
    // =========================================================

    private readonly PartidaPresupuestariaService partidaService =
        new PartidaPresupuestariaService();


    // =========================================================
    // CONSTRUCTOR
    // =========================================================

    public FormPartidas()
    {
      InitializeComponent();
      Traductor.Traducir(this);

      dgvPartidas.AutoGenerateColumns = false;

      Load += FormPartidas_Load;
    }

    // La validación va en Load: en el constructor Close() no cierra el formulario.
    private void FormPartidas_Load(object sender, EventArgs e)
    {
      if (!ValidarAcceso())
        return;

      CargarFiltros();
      ConfigurarPermisos();
      CargarPartidas();
    }


    // =========================================================
    // SEGURIDAD
    // =========================================================

    private bool ValidarAcceso()
    {
      if (!SesionActual.TienePermiso(
          Permisos.PARTIDAS_PRESUPUESTARIAS_VER))
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
      bool puedeCrear =
          SesionActual.TienePermiso(
              Permisos.PARTIDAS_PRESUPUESTARIAS_CREAR);

      // No existe un permiso específico para buscar.
      // Cualquier usuario que pueda VER puede utilizar los filtros.

      btnNuevo.Visible = puedeCrear;

      btnBuscar.Enabled = true;
      btnLimpiar.Enabled = true;
    }


    // =========================================================
    // CARGA INICIAL
    // =========================================================

    private void CargarFiltros()
    {
      cmbAnio.Items.Clear();
      cmbAnio.Items.Add(Traductor.T("Todos"));

      int anioActual = DateTime.Now.Year;

      for (int anio = anioActual - 2;
           anio <= anioActual + 2;
           anio++)
      {
        cmbAnio.Items.Add(anio);
      }

      cmbAnio.SelectedIndex = 0;


      cmbTrimestre.Items.Clear();

      cmbTrimestre.Items.Add(Traductor.T("Todos"));
      cmbTrimestre.Items.Add("1");
      cmbTrimestre.Items.Add("2");
      cmbTrimestre.Items.Add("3");
      cmbTrimestre.Items.Add("4");

      cmbTrimestre.SelectedIndex = 0;


      cmbEstado.Items.Clear();

      cmbEstado.Items.Add(Traductor.T("Todos"));
      cmbEstado.Items.Add(Traductor.T("Activas"));
      cmbEstado.Items.Add(Traductor.T("Inactivas"));

      cmbEstado.SelectedIndex = 0;
    }

    private void CargarPartidas()
    {
      List<PartidaPresupuestaria> lista =
          partidaService.ObtenerTodos();

      dgvPartidas.DataSource = lista;

      lblCantidad.Text =
          string.Format(Traductor.T("Mostrando {0} partidas."), lista.Count);
    }


    // =========================================================
    // BUSCAR
    // =========================================================

    private void btnBuscar_Click(object sender, EventArgs e)
    {
      List<PartidaPresupuestaria> lista =
          partidaService.ObtenerTodos();

      if (cmbAnio.SelectedIndex > 0)
      {
        int anio =
            Convert.ToInt32(cmbAnio.SelectedItem);

        lista = lista
            .Where(p => p.Anio == anio)
            .ToList();
      }

      if (cmbTrimestre.SelectedIndex > 0)
      {
        int trimestre =
            Convert.ToInt32(cmbTrimestre.SelectedItem);

        lista = lista
            .Where(p => p.Trimestre == trimestre)
            .ToList();
      }

      if (cmbEstado.SelectedIndex == 1)
      {
        lista = lista
            .Where(p => p.Activo)
            .ToList();
      }
      else if (cmbEstado.SelectedIndex == 2)
      {
        lista = lista
            .Where(p => !p.Activo)
            .ToList();
      }

      dgvPartidas.DataSource = lista;

      lblCantidad.Text =
          string.Format(Traductor.T("Mostrando {0} partidas."), lista.Count);
    }


    // =========================================================
    // LIMPIAR
    // =========================================================

    private void btnLimpiar_Click(object sender, EventArgs e)
    {
      cmbAnio.SelectedIndex = 0;
      cmbTrimestre.SelectedIndex = 0;
      cmbEstado.SelectedIndex = 0;

      CargarPartidas();
    }


    // =========================================================
    // NUEVA PARTIDA
    // =========================================================

    private void btnNuevo_Click(object sender, EventArgs e)
    {
      if (!SesionActual.TienePermiso(
          Permisos.PARTIDAS_PRESUPUESTARIAS_CREAR))
      {
        MessageBox.Show(
            Traductor.T("No tiene permisos para crear partidas presupuestarias."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      FormDetallePartida formulario =
          new FormDetallePartida();

      if (formulario.ShowDialog() == DialogResult.OK)
      {
        CargarPartidas();
      }
    }


    // =========================================================
    // DETALLE / MODIFICAR
    // =========================================================

    private void dgvPartidas_CellDoubleClick(
        object sender,
        DataGridViewCellEventArgs e)
    {
      if (e.RowIndex < 0)
        return;

      if (!SesionActual.TienePermiso(
          Permisos.PARTIDAS_PRESUPUESTARIAS_GUARDAR))
      {
        MessageBox.Show(
            Traductor.T("No tiene permisos para modificar partidas presupuestarias."),
            Traductor.T("Acceso denegado"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      PartidaPresupuestaria partida =
          (PartidaPresupuestaria)
          dgvPartidas.Rows[e.RowIndex].DataBoundItem;

      FormDetallePartida formulario =
          new FormDetallePartida(
              partida.PartidaPresupuestariaId);

      if (formulario.ShowDialog() == DialogResult.OK)
      {
        CargarPartidas();
      }
    }


    // =========================================================
    // FORMATO
    // =========================================================

    private void dgvPartidas_CellFormatting(
        object sender,
        DataGridViewCellFormattingEventArgs e)
    {
      string propiedad =
          dgvPartidas.Columns[e.ColumnIndex]
          .DataPropertyName;

      if (propiedad == "MontoAsignado" ||
          propiedad == "MontoComprometido" ||
          propiedad == "MontoEjecutado")
      {
        if (e.Value != null)
        {
          decimal monto =
              Convert.ToDecimal(e.Value);

          e.Value = monto.ToString("N2");

          e.FormattingApplied = true;
        }
      }

      if (propiedad == "FechaAsignacion")
      {
        if (e.Value != null)
        {
          DateTime fecha =
              Convert.ToDateTime(e.Value);

          e.Value =
              fecha.ToString("dd/MM/yyyy HH:mm");

          e.FormattingApplied = true;
        }
      }

      if (propiedad == "Activo")
      {
        if (e.Value != null)
        {
          bool activo =
              Convert.ToBoolean(e.Value);

          e.Value =
              activo ? Traductor.T("Activa") : Traductor.T("Inactiva");

          e.FormattingApplied = true;
        }
      }
    }
  }
}
