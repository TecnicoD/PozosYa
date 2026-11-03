using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Negocio.Models;
using Negocio.Services;
using PY.Presentacion.Reportes;
using PY.Presentacion.Idiomas;

namespace PY.Presentacion.Forms
{
  public partial class FormInicioReclamos : Form
  {
    // =========================
    // VARIABLES
    // =========================

    // Lo que se está mostrando en la grilla y los filtros
    // con los que se obtuvo (se usan para el reporte PDF).
    private List<ReclamoListado> reclamosMostrados =
        new List<ReclamoListado>();

    private string filtrosAplicados = Traductor.T("Sin filtros");


    // =========================
    // CONSTRUCTOR
    // =========================

    public FormInicioReclamos()
    {
      InitializeComponent();
      Traductor.Traducir(this);

      CargarFiltros();
      CargarReclamos();
    }


    // =========================
    // CARGA DE DATOS
    // =========================

    private void CargarReclamos()
    {
      ReclamoService reclamoService = new ReclamoService();

      List<ReclamoListado> lista = reclamoService.ObtenerListado();

      filtrosAplicados = Traductor.T("Sin filtros");

      MostrarReclamos(lista);
    }

    private void CargarFiltros()
    {
      DetalleClaveValorService detalleService =
          new DetalleClaveValorService();

      CargarComboBarrio(
          detalleService.ObtenerPorTabla("BARRIO")
      );

      CargarComboEstado(
          detalleService.ObtenerPorTabla("ESTADO")
      );
    }

    private void CargarComboBarrio(List<DetalleClaveValor> barrios)
    {
      barrios.Insert(0, new DetalleClaveValor
      {
        DetalleId = 0,
        Valor = Traductor.T("Todos")
      });

      cmbBarrio.DataSource = barrios;
      cmbBarrio.DisplayMember = "Valor";
      cmbBarrio.ValueMember = "DetalleId";
    }

    private void CargarComboEstado(List<DetalleClaveValor> estados)
    {
      estados.Insert(0, new DetalleClaveValor
      {
        DetalleId = 0,
        Valor = Traductor.T("Todos")
      });

      cmbEstado.DataSource = estados;
      cmbEstado.DisplayMember = "Valor";
      cmbEstado.ValueMember = "DetalleId";
    }

    private void MostrarReclamos(List<ReclamoListado> lista)
    {
      reclamosMostrados = lista;

      dgvReclamos.AutoGenerateColumns = false;
      dgvReclamos.DataSource = lista;

      lblCantidad.Text =
          string.Format(Traductor.T("Mostrando {0} reclamos."), lista.Count);
    }


    // =========================
    // FILTROS
    // =========================

    private void btnBuscar_Click(object sender, EventArgs e)
    {
      ReclamoFiltro filtro = ObtenerFiltro();

      ReclamoService reclamoService =
          new ReclamoService();

      List<ReclamoListado> lista =
          reclamoService.Buscar(filtro);

      filtrosAplicados = DescribirFiltros();

      MostrarReclamos(lista);
    }

    // Arma el texto de los filtros para el encabezado del PDF.
    private string DescribirFiltros()
    {
      List<string> partes = new List<string>();

      if (!string.IsNullOrWhiteSpace(txtDNI.Text))
        partes.Add(Traductor.T("DNI: ") + txtDNI.Text.Trim());

      if (Convert.ToInt32(cmbBarrio.SelectedValue) != 0)
        partes.Add(Traductor.T("Barrio: ") + cmbBarrio.Text);

      if (Convert.ToInt32(cmbEstado.SelectedValue) != 0)
        partes.Add(Traductor.T("Estado: ") + cmbEstado.Text);

      if (dtpDesde.Checked)
        partes.Add(Traductor.T("Desde: ") + dtpDesde.Value.ToString("dd/MM/yyyy"));

      if (dtpHasta.Checked)
        partes.Add(Traductor.T("Hasta: ") + dtpHasta.Value.ToString("dd/MM/yyyy"));

      if (partes.Count == 0)
        return Traductor.T("Sin filtros");

      return string.Join(" | ", partes);
    }


    // =========================
    // REPORTE PDF
    // =========================

    private void btnExportarPdf_Click(object sender, EventArgs e)
    {
      if (reclamosMostrados.Count == 0)
      {
        MessageBox.Show(
            Traductor.T("No hay reclamos para exportar."),
            Traductor.T("Reporte"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);

        return;
      }

      using (SaveFileDialog dialogo = new SaveFileDialog())
      {
        dialogo.Title = Traductor.T("Guardar reporte de reclamos");
        dialogo.Filter = Traductor.T("Archivo PDF|*.pdf");
        dialogo.FileName =
            "Reclamos_" +
            DateTime.Now.ToString("yyyyMMdd_HHmm") +
            ".pdf";

        if (dialogo.ShowDialog() != DialogResult.OK)
          return;

        try
        {
          ReporteReclamosPdf.Generar(
              dialogo.FileName,
              reclamosMostrados,
              filtrosAplicados);

          System.Diagnostics.Process.Start(
              new System.Diagnostics.ProcessStartInfo
              {
                FileName = dialogo.FileName,
                UseShellExecute = true
              });
        }
        catch (Exception ex)
        {
          MessageBox.Show(
              Traductor.T("No se pudo generar el reporte.\n\n") +
              Traductor.T(ex.Message),
              Traductor.T("Error"),
              MessageBoxButtons.OK,
              MessageBoxIcon.Error);
        }
      }
    }

    private ReclamoFiltro ObtenerFiltro()
    {
      ReclamoFiltro filtro = new ReclamoFiltro
      {
        DNI = txtDNI.Text.Trim()
      };

      if (Convert.ToInt32(cmbBarrio.SelectedValue) != 0)
      {
        filtro.BarrioId =
            Convert.ToInt32(cmbBarrio.SelectedValue);
      }

      if (Convert.ToInt32(cmbEstado.SelectedValue) != 0)
      {
        filtro.EstadoId =
            Convert.ToInt32(cmbEstado.SelectedValue);
      }

      if (dtpDesde.Checked)
      {
        filtro.FechaDesde =
            dtpDesde.Value.Date;
      }

      if (dtpHasta.Checked)
      {
        filtro.FechaHasta =
            dtpHasta.Value.Date;
      }

      return filtro;
    }

    private void btnLimpiar_Click(object sender, EventArgs e)
    {
      txtDNI.Clear();

      cmbBarrio.SelectedIndex = 0;
      cmbEstado.SelectedIndex = 0;

      dtpDesde.Checked = false;
      dtpHasta.Checked = false;

      CargarReclamos();
    }


    // =========================
    // NAVEGACIÓN
    // =========================

    private void btnNuevoReclamo_Click(object sender, EventArgs e)
    {
      FormNuevoReclamo formulario =
          new FormNuevoReclamo();

      if (formulario.ShowDialog() == DialogResult.OK)
      {
        CargarReclamos();
      }
    }

    private void dgvReclamos_CellDoubleClick(
        object sender,
        DataGridViewCellEventArgs e)
    {
      if (e.RowIndex < 0)
        return;

      int reclamoId = Convert.ToInt32(
          dgvReclamos.Rows[e.RowIndex]
              .Cells["colReclamoId"]
              .Value
      );

      FormDetalleReclamo formulario =
          new FormDetalleReclamo(reclamoId);

      formulario.ShowDialog();
    }


    // =========================
    // EVENTOS DEL DATAGRIDVIEW
    // =========================

    private void dgvReclamos_CellContentClick(
        object sender,
        DataGridViewCellEventArgs e)
    {
    }
  }
}
